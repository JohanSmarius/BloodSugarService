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



app.MapGet("/bloodsugermeasurements", () =>
{
    var measurements = new List<BloodSugarMeasurement>();
    
    for (int i = 0; i < 5; i++)
    {
        var random = new Random();

        var date = DateTime.Today.AddDays(-i);

        for (int moments = 0; moments < 10; moments++)
        { 
            var value = random.Next(30, 100) / 10.0m;
            var remark = random.Next(0, 10) > 7 ? "Remark" : null;

            var randomHour = random.Next(0, 24);

            var moment = new DateTime(date.Year, date.Month, date.Day, randomHour, 0, 0);

            measurements.Add(new BloodSugarMeasurement(moment, value, remark));

        }
        
    }

    return measurements;
})
.WithName("Bloodsugarmeasurements")
.WithOpenApi();

app.Run();



internal record BloodSugarMeasurement(DateTime Moment, decimal Value, string? Remarks)
{
 
}
