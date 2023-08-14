using API_testing2.Context;
using API_testing2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      builderCors =>
                      {
                          builderCors.WithOrigins("http://127.0.0.1:5500")
                                     .AllowAnyHeader()
                                     .AllowAnyMethod();
                      });
});

// Configuraci�n de controladores y JSON
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

// Configuraci�n Swagger
builder.Services.AddSwaggerGen();

// Configuraci�n de enrutamiento
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

// Configuraci�n de la base de datos
var isLocalConnectionString = builder.Configuration.GetValue<bool>("ConnectionString_isLocal");
var connectionStringKey = isLocalConnectionString ? "ConnectionString_apitesting2db_local" : "ConnectionString_apitesting2db_remote";
builder.Services.AddDbContext<ContextDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(connectionStringKey)));

// Registro de servicios
builder.Services.AddTransient<ServiceVilla>();

var app = builder.Build();

// Configuraci�n del middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyAllowSpecificOrigins");
app.UseAuthorization();
app.MapControllers();

app.Run();
