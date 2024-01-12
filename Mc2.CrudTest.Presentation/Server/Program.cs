using FluentValidation;
using Mc2.CrudTest.Core.ApplicationServices.C_DomainEvent.Events;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.CreateCustomer;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.DeleteCustomer;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.UpdateCustomer;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Contracts;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetAllCustomers;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetCustomerById;
using Mc2.CrudTest.Core.Domain;
using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using Mc2.CrudTest.Core.Domain.Customers.Events;
using Mc2.CrudTest.Infrastructures.Command;
using Mc2.CrudTest.Infrastructures.Command.Customers;
using Mc2.CrudTest.Infrastructures.Command.Interceptors;
using Mc2.CrudTest.Infrastructures.Query;
using Mc2.CrudTest.Infrastructures.Query.Customers;
using Mc2.CrudTest.ServerHelper;
using Mc2.CrudTest.ServerHelper.IoC;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace Mc2.CrudTest.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            #region cntString
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbSAPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var conncetionString = $"Server={dbHost};Database={dbName};User ID=sa; Password={dbSAPassword};Persist Security Info=True;TrustServerCertificate=True";
            #endregion

            builder.Services
                   .AddDbContext<CommandDBContext>(o => o.UseSqlServer(conncetionString))
                   .AddDbContext<QueryDBContext>(o => o.UseSqlServer(conncetionString));

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(node: new Uri(context.Configuration["ElasticConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8, // Depending on your Elasticsearch version
                    IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                    //IndexFormat = $"{builder.Environment.ApplicationName}-logs-{builder.Environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                    NumberOfShards = 2,
                    NumberOfReplicas = 1
                })
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .ReadFrom.Configuration(context.Configuration);
            });

            //builder.Services.AddApiClientService(x => x.ApiBaseAddress = builder.Configuration.GetValue<string>("ApiBaseAddress"));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder
                            .WithOrigins("https://localhost:9081")
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .AllowAnyHeader();
                    });
            });

            builder.Services.AddHttpClient<CrudTestClientService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:9081");
            }).AddTypedClient<CrudTestClientService>();

            // MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Registering Notification Handlers
            builder.Services.AddScoped<INotificationHandler<CustomerCreated>, DummyCustomerCreatedHandler>();
            builder.Services.AddScoped<INotificationHandler<CustomerUpdated>, DummyCustomerUpdatedHandler>();
            builder.Services.AddScoped<INotificationHandler<CustomerDeleted>, DummyCustomerDeletedHandler>();

            // Registering Event Publisher Interceptor
            builder.Services.AddScoped<PublishDomainEventsInterceptor>();

            // Registering Interfaces and Implementations
            builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
            builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
            builder.Services.AddScoped<ICustomerUnitOfWork, CustomerUnitOfWork>();
            // Registering handlers
            builder.Services.AddTransient<IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>, GetAllCustomersQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetCustomerByIdQuery, CustomerDto>, GetCustomerByIdQueryHandler>();
            builder.Services.AddScoped<IRequestHandler<CreateCustomerCommand, long>, CreateCustomerCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<UpdateCustomerCommand, long>, UpdateCustomerCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<DeleteCustomerCommand, long>, DeleteCustomerCommandHandler>();
            // Registering Command Validators (FluentValidation used)
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateCustomerValidator>();
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");


            ApplyMigrations(app);
            app.Run();
        }

        static void ApplyMigrations(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<CommandDBContext>();

                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Program>() // Reference to your Program class
                              .UseUrls("https://localhost:9081");
                });
    }
}