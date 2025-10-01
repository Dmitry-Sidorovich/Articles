using Articles.Infrastructure.ComponentRegistrar;
using Articles.Infrastructure.DataAccess;
using Articles.Infrastructure.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterAppServices();
builder.Services.RegisterRepositories();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("AcademyDb"));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var xml = Path.ChangeExtension(typeof(Program).Assembly.Location, "xml");
    if (File.Exists(xml))
        o.IncludeXmlComments(xml, includeControllerXmlComments: true);
    
    var contractsXml = Path.Combine(AppContext.BaseDirectory, "Articles.Contracts.xml");
    if (File.Exists(contractsXml))
        o.IncludeXmlComments(contractsXml);
});

// builder.Services.AddAuthentication("Bearer").AddJwtBearer(...); Добавление схемы аутентификации 
// builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();
    }
// }

// app.UseAuthentication(); использование схемы аутентификации 
// app.UseAuthorization();

app.MapControllers();

app.Run();