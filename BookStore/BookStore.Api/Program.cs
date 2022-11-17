using BookStore.Entities.Entities;
using BookStore.Persistence.Contracts.Contracts;
using BookStore.Persistence.Persistence.Repositories;
using BookStore.Services.contracts.Contracts;
using BookStore.Services.Services;
using Microsoft.EntityFrameworkCore;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7088");
                          policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});


//repositories

//repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

//services
builder.Services.AddScoped<IBookService, BookServices>();
builder.Services.AddScoped<IAuthorService, AuthorServices>();
builder.Services.AddScoped<IGenreServices, GenreServices>();
// Add services to the container.

builder.Services.AddControllers();

//Add connection string

var connectionString = builder.Configuration.GetConnectionString("BookStoreDb");

builder.Services.AddDbContextPool<BookStoreDbContext>(options => options.UseSqlServer(connectionString
    ,serviceOpt => serviceOpt.MigrationsAssembly("BookStore.Persistence.Migrations")
    ));
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

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();


