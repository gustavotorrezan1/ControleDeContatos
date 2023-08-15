using ControleDeContatos.Data;
using ControleDeContatos.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// String de conecção

//builder.Services.AddDbContext<BancoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

string mySqlConnection =
    builder.Configuration.GetConnectionString("Online");

builder.Services.AddDbContextPool<BancoContext>(options =>
    options.UseMySql(mySqlConnection,
        ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

//builder.Services.AddDbContext<BancoContext>(options => options.UseSqlServer("Server=aws.connect.psdb.cloud;Database=db_sistemacontatos;user=civ1axn795le27p5dlgc;password=pscale_pw_M6eTZwXsFB4mBnyXoyv9e2fIXGfn0g1dk8vdfNUfKns;S"));
//builder.Services.AddScoped<IContatoRepository, ContatoRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews();

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