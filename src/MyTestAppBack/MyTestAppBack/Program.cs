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
            // ���������� ��� ������
            //policy.WithOrigins("http://localhost:3001").AllowAnyHeader().AllowAnyMethod();
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
}) ;

// ��������� ������ � ������� ��
var currentDb = ReadAppConfig.GetCurrentDb(out string connectionString);
var dbOptions = DbOptionsFactory.GetOptionsViaDb(currentDb, connectionString);

// ���������� �������� ����� di ���������
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
