using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Activities;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            });
            // register Mediator
            /*
             when our application starts app this  service gets registered and then it takes a look

            inside the assembly where this class is located and it's going to register all of our mediator handlers

            so it knows where to send the notifications or activities that we're getting mediator to take care of.
            */
            services.AddMediatR(typeof(List.Handler));


            // add autoMapper
            /*
             we're going to tell it to use the assembly to locate all of the mapping profiles that we're

            using inside our project.

            Same deal as mediator.

            When our application starts.

            Auto mapper is going to be registered as a service.

            It's going to take a look inside this assembly that contains mapping profiles and register all of our

            mapping profiles.

            So it can be used when it comes across them inside our code here.
            */
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;

        }
    }
}