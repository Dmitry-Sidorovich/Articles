﻿using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Contexts.Articles.Services;
using Articles.Infrastructure.DataAccess.Contexts.Articles.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Articles.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        
        return services;
    }
    
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IArticleRepository, ArticleRepository>();
        
        return services;
    }
}