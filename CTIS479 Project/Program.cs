using BLL.DAL;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
//using BLL.Models;
//using BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Inversion of Control Container
var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<DB>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IAuthorService, AuthorServices>();
builder.Services.AddScoped<IBookService, BookServices>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IGenreService, GenreService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
