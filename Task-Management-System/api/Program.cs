var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};



app.MapGet("/", () =>
{
   
    return "API is Working";
})
.WithName("GetHome");

//Implement a new endpoitn "/{price}/{tax}/"
//return JSON

//{
        //price: 0.00
        //tax: 0%
        //final: price + tax
//}

app.MapGet("{price}/{tax}", (decimal price, decimal tax) => 
{
    var finalPrice = price + (price * tax / 100);
    return Results.Json(new 
    {
        price = price,
        tax = tax,
        final = finalPrice
    });
})
.WithName("GetPriceWithTax");


// Weather Forecase "http//localhost<PORT>/weatherforecast"
app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
