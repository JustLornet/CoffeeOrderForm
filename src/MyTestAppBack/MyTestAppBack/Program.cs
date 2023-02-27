using Microsoft.EntityFrameworkCore;
using MyTestAppBack.DataAccess;

var builder = WebApplication.CreateBuilder(args);

var CoffeeOrigins = "_coffeeOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CoffeeOrigins,
        policy =>
        {
            // переделать для релиза
            //policy.WithOrigins("http://localhost:3001").AllowAnyHeader().AllowAnyMethod();
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
}) ;
builder.Services.AddControllers();
builder.Services.AddDbContext<Db>(options => options.UseSqlite(DbConfig.DbConnectionString));
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseCors(CoffeeOrigins);

app.Run();
