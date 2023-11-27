using APIReservify.Models;
using APIReservify.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using Firebase.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//MongoDB---------------------
builder.Services.Configure<CitasStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(CitasStoreDatabaseSettings)));

builder.Services.AddSingleton<ICitasStoreDataBaseSettings>(sp =>
    sp.GetRequiredService<IOptions<CitasStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("CitasStoreDatabaseSettings.ConnectionString")));

builder.Services.AddScoped<ICitaService, CitaService>();
//-------------------------------------------

//SQLSERVER
builder.Services.AddDbContext<ReservifyContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("cadena")));
//------------------------

builder.Services.AddSingleton<FirebaseStorage>(new FirebaseStorage("reservify-138a5.appspot.com"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
   
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
