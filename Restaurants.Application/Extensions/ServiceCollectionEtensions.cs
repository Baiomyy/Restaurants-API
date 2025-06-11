using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        //services.AddScoped<IRetsaurantsServices, RetsaurantsServices>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly)); //Look in this assembly, find MediatR handlers, and set everything up for me. 
        services.AddAutoMapper(applicationAssembly); //Look in this assembly, find AutoMapper profiles, and set everything up for me.
        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor(); // This is needed to access the current HTTP context in UserContext
    }
}
