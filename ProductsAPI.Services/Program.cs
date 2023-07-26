using MediatR;
using ProductsAPI.Application.Interfaces.Services;
using ProductsAPI.Application.Services;
using ProductsAPI.Services.Extensions;
using ProductsAPI.Infra.IoC.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDoc();
builder.Services.AddDependencyInjection(); //Serviços
builder.Services.AddSqlServerConfig(builder.Configuration); //SqlServer
builder.Services.AddMongoDBConfig(builder.Configuration); //MongoDB
builder.Services.AddMediatRConfig(); //MediatR
builder.Services.AddJwtBearer(builder.Configuration); //JWT

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSwaggerDoc();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
