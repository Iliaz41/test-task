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
                "http://localhost:3000",     // React dev server
                "https://localhost:3000",    // HTTPS версия
                "http://localhost:5000",     // Альтернативный порт
                "https://localhost:5000"     // HTTPS альтернативный
            )
            .AllowAnyMethod()                 // Разрешаем все HTTP методы (GET, POST, PUT, DELETE)
            .AllowAnyHeader()                  // Разрешаем все заголовки
            .AllowCredentials();               // Разрешаем куки/авторизацию
    });

    // Для разработки можно добавить более свободную политику
    options.AddPolicy("Development", policy =>
    {
        policy.AllowAnyOrigin()                 // ВНИМАНИЕ: только для разработки!
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