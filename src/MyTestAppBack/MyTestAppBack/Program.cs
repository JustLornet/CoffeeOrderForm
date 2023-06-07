using Microsoft.EntityFrameworkCore;
using MyTestAppBack.DataAccess;
using MyTestAppBack.Utils;
using MyTestAppBack.DataAccess.Utils;

var builder = WebApplication.CreateBuilder(args);

var CoffeeOrigins = "_coffeeOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CoffeeOrigins,
        policy =>
        {
            // переделать дл€ релиза
            //policy.WithOrigins("http://localhost:3001").AllowAnyHeader().AllowAnyMethod();
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
}) ;

// получение данных о текущей Ѕƒ
var currentDb = ReadAppConfig.GetCurrentDb(out string connectionString);
var dbOptions = DbOptionsFactory.GetOptionsViaDb(currentDb, connectionString);

// добавление сервисов через di контейнер
builder.Services.AddControllers();
builder.Services.AddScoped<Db>((services) => new Db(dbOptions));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseCors(CoffeeOrigins);

app.Run();
