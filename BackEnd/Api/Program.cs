using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuring EF Core to use SQL Server with the connection string from configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Registering repository and service dependencies for dependency injection
builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped<IUrlShorteningService, UrlShorteningService>();

// Adding CORS policy to allow requests from the specified origin (React app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
         policy =>
         {
             policy.WithOrigins("http://localhost:5173") // Add the React app's origin
                   .AllowAnyHeader()
                   .AllowAnyMethod();
         });
});

// Configuring Serilog as the logging provide
builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();


var app = builder.Build();

// Applying the CORS policy to the app
app.UseCors("AllowSpecificOrigin");

//// Ensure the database is created and migrations are applied
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//    // Apply any pending migrations and create the database if not exists
//    dbContext.Database.Migrate();
//}

// Enable Serilog request logging for HTTP requests
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
