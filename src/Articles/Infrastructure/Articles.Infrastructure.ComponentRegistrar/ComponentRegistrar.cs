﻿using Articles.AppServices.Contexts.Articles.Builder;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Contexts.Articles.Services;
using Articles.AppServices.Contexts.Files.Repositories;
using Articles.AppServices.Contexts.Files.Services;
using Articles.Infrastructure.ComponentRegistrar.MapProfiles;
using Articles.Infrastructure.DataAccess.Contexts.Articles.Repositories;
using Articles.Infrastructure.DataAccess.Contexts.Files.Repositories;
using Articles.Infrastructure.DataAccess.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Articles.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IFileService, FileService>();
        services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
        return services;
    }
    
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<IArticlePredicateBuilder, ArticlePredicateBuilder>();
        services.AddScoped<IArticleRepository, ArticleRepository>(); // scoped or singleton ?
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        
        return services;
    }

    private static MapperConfiguration GetMapperConfiguration()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ArticleProfile>();
            cfg.AddProfile<FileProfile>();
        });
        configuration.AssertConfigurationIsValid();
        return configuration;
    }
}