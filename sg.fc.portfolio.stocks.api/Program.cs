using Sg.Fc.Alphavantage.Sqprovider;
using Sg.Fc.Alphavantage.Sqprovider.Interfaces;
using Sg.Fc.Alphavantage.Sqprovider.Mapper;
using Sg.Fc.Portfolio.Stocks.Api.Entity;
using Sg.Fc.Portfolio.Stocks.Api.Services;
using Sg.Fc.Portfolio.Stocks.Datasource;

var builder = WebApplication.CreateBuilder(args);

#region Configuration
// Add services to the container.
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("av-endpoints.json",
                       optional: true,
                       reloadOnChange: false);
});

builder.Services.Configure<APIEndPointOptions>(
    builder.Configuration.GetSection(APIEndPointOptions.APIEndPoint));
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Services

builder.Services.AddScoped<IDataSource, XmlDataSource>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStockQuoteProvider, AVStockQuoteProvider>();
#endregion

#region Mapper
builder.Services.AddAutoMapper(typeof(AlphaVantageProfile));
#endregion
builder.Services.AddHttpClient<PortfolioService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
