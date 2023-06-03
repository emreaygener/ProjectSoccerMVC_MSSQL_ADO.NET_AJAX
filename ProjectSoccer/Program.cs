using ProjectSoccer;
using ProjectSoccer.DataAccessLayer.Repositories;
using ProjectSoccer.Middlewares;
using ProjectSoccer.Services;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ClubRepository>(service => new(new SqlConnection(builder.Configuration.GetConnectionString("DefaultSqlConnection"))));
builder.Services.AddScoped<PlayerRepository>(service => new(new SqlConnection(builder.Configuration.GetConnectionString("DefaultSqlConnection"))));
builder.Services.AddSingleton<ILoggerService, LoggerService>();

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

app.UseCustomExceptionMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
