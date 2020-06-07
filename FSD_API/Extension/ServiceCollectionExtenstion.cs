using BO.Models.Mongo;
using BO.ViewModels;
using DAL;
using DAL_Layer;
using FSD_API.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceLB;
using System;
using System.Text;

namespace FSD_API.Extension
{
    public static class ServiceCollectionExtenstion
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<MessageResult>();
            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            services.AddTransient<ICacheService, RedisService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IWebScraper, WebScraper>();
            services.AddTransient<ILineMessageService, LineMessageService>();
            services.AddTransient<IPhoneRankingService, PhoneRankingService>();
            services.AddTransient<ISleepInformationService, SleepInformationService>();
            services.AddTransient<IBrushingInformationService, BrushingInformationService>();
            services.AddTransient<IActionService, ActionService>();
            services.AddScoped<IEmailService, EmailService>();

            // Add Cronjob
            services.AddCronJob<BrushingInformationCronJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Utc;
                //c.CronExpression = @"0 23-17 */6 * *";
                //for test
                c.CronExpression = @"* * * * *";
            });
        }

        public static void ConfigureServiceCollection(this IServiceCollection services, string conectionString)
        {
            services.AddControllers(opt =>
                {
                    // Add ExceptionFilter
                    opt.Filters.Add(typeof(ExceptionFilter));
                });

            // Add Ef
            services.AddDbContext<SystemContext>(x => x.UseNpgsql(conectionString

                , b => b.MigrationsAssembly("FSD_API")).ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.TransactionError))
                , ServiceLifetime.Transient)
               ;

            services.AddHealthChecks().AddNpgSql(conectionString);
        }

        public static void ConfigureMongoService(this IServiceCollection services, string conectionString)
        {
            MongSettings bookSetting = new MongSettings() { ConnectionString = conectionString, DatabaseName = "ZeallyStudio" };

            services.AddSingleton<IDatabaseSettings>(bookSetting);
            services.AddSingleton<IMongoUnitOfWork, MongoUnitOfWork>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Awesome API",
                    Version = "v1 ",
                    Description = "FSD .Net Core API " + services.GetType().Assembly.GetName().Version.ToString(),

                    Contact = new OpenApiContact
                    {
                        Name = "Chanchai Jeimvijack",
                        Email = "kewell5@live.com",
                    },
                });

                // Add Jwt
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration, string secret)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
            });
        }

        private static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }
            var config = new ScheduleConfig<T>();
            options.Invoke(config);
            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");
            }

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}