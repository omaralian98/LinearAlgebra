using LinearAlgebra.Classes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TwoDimensionalArrayConverter<string>());
        options.JsonSerializerOptions.Converters.Add(new TwoDimensionalArrayConverter<decimal>());
        options.JsonSerializerOptions.Converters.Add(new TwoDimensionalArrayConverter<Fraction>());
    });
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServerSideBlazor(); 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.MapBlazorHub();

app.MapFallback("/", () => "Hello World");
app.Run();
