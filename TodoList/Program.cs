
using Amazon.SimpleEmail;
using Amazon.SimpleNotificationService;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Entities;
using TodoList.Mapping;
using TodoList.Repositories;
using TodoList.Repositories.Interfaces;
using TodoList.Services;
using TodoList.Services.Interfaces;

namespace TodoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection")
                ?? throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.");
            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TodoItemMappingProfile());
                cfg.AddProfile(new TodoListMappingProfile());

            }).CreateMapper());



            //Logger

            var config = builder.Configuration.GetAWSLoggingConfigSection();
            //builder.Logging.ClearProviders();
            builder.Logging.AddAWSProvider(config);
            builder.Logging.AddConsole();

            builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
            builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            builder.Services.AddScoped<ITodoListService, TodoListService>();
            builder.Services.AddScoped<ITodoItemService, TodoItemService>();
            builder.Services.AddScoped<IEmailService, EmailAWSService>();
            builder.Services.AddScoped<INotificationService, NotificationAWSService>();

            builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
            builder.Services.AddAWSService<IAmazonSimpleEmailService>();
            builder.Services.AddAWSService<IAmazonSimpleNotificationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}