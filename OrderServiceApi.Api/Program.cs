using Oracle.ManagedDataAccess.Client;
using OrderServiceApi.Api.Data.Repository.Implementation;
using OrderServiceApi.Api.Data.Repository.Interface;
using OrderServiceApi.Api.Endpoints;
using OrderServiceApi.Api.Service.Implementation;
using OrderServiceApi.Api.Service.Interface;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

OracleConfiguration.TnsAdmin = "/Users/mac/Documents/Codes/Wallet_DEVDB";
OracleConfiguration.WalletLocation = "/Users/mac/Documents/Codes/Wallet_DEVDB";
builder.Services.AddOpenApi();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IdRepository, DbRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.OrdersEndpoints();

app.Run();