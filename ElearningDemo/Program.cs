using ElearningDemoRepositories.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    // Add services to the container.
    builder.Services.AddDbContext<ElearningDemoContext>(options =>
    {
        var connectString = builder.Configuration.GetConnectionString("ElearningDemoDbContext");
        options.UseMySql(connectString, ServerVersion.AutoDetect(connectString));
    });
    builder.Services.AddControllersWithViews();

    var app = builder.Build();
    CreateDbIfNotExists(app);

    // 參照官網的寫法: https://docs.microsoft.com/zh-tw/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0
    static void CreateDbIfNotExists(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ElearningDemoContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred creating the DB.");
            //var logger = services.GetRequiredService<ILogger<Program>>();
            //logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
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
        name: "admin",
        pattern: "{admin}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
