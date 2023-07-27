using MediatR;
using ProductsAPI.Application.Interfaces.Services;
using ProductsAPI.Application.Services;
using ProductsAPI.Infra.IoC.Extensions;
using ProductsAPI.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerDoc(); //Swagger
builder.Services.AddSqlServerConfig(builder.Configuration); //SqlServer
builder.Services.AddMongoDBConfig(builder.Configuration); //MongoDB
builder.Services.AddDependencyInjection(); //Serviços
builder.Services.AddMediatRConfig(); //MediatR
builder.Services.AddJwtBearer(builder.Configuration); //JWT   
builder.Services.AddCorsPolicy(); //CORS

var app = builder.Build();

app.UseSwaggerDoc(); //Swagger
app.UseCorsPolicy(); //CORS

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
