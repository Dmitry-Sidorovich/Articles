using Articles.Infrastructure.ComponentRegistrar;
using Articles.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterAppServices();
builder.Services.RegisterRepositories();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseAuthentication();

app.MapControllers();

app.Run();