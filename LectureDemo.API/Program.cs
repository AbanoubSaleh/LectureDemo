using LectureDemo.DAL.Data;
using LectureDemo.DAL.Repositories;
using LectureDemo.BLL.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Configure SQL Server
builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnection"),
        sqlOptions => sqlOptions.CommandTimeout(180) // Set the timeout to 180 seconds (3 minutes)
    ));


// Configure PostgreSQL
builder.Services.AddDbContext<PostgresDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgresConnection"),
        npgsqlOptions => npgsqlOptions.CommandTimeout(180) // Set the timeout to 180 seconds (3 minutes)
    ));


builder.Services.AddScoped<ILectureRepository, LectureRepository>();
builder.Services.AddScoped<ILectureService, LectureService>();
builder.Services.AddScoped<IIndexedLectureRepository, IndexedLectureRepository>();

builder.Services.AddControllers();
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
app.UseAuthorization();
app.MapControllers();
app.Run();

