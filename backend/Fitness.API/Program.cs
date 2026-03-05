using Fitness.API.Extensions;
using Fitness.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPostgresDbContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.ConfigureAutoMapper();
builder.Services.AddServices();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",     
                "https://localhost:3000",    
                "http://localhost:5000",     
                "https://localhost:5000"     
            )
            .AllowAnyMethod()                 
            .AllowAnyHeader()                 
            .AllowCredentials();              
    });

    
    options.AddPolicy("Development", policy =>
    {
        policy.AllowAnyOrigin()                 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCustomException();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Development");
}
else
{
    app.UseCors("AllowFrontend");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();