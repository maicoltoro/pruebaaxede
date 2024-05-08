using HotelesAxede_Api.Clases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//este metodo esta configurando los Cors
builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolice",
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});



builder.Services.AddControllers();
builder.Services.AddScoped<MetodosHoteles>();
builder.Services.AddScoped<sedes>();
builder.Services.AddScoped<Alojamiento>();
builder.Services.AddScoped<Temporada>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("MyPolice");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
