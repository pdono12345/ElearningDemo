using ElearningDemo.Extensions;
using ElearningDemoRepositories.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    // Add services to the container.
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddDbContext<ElearningDemoContext>(options =>
    {
        var connectString = builder.Configuration.GetConnectionString("ElearningDemoDbContext");
        options.UseMySql(connectString, ServerVersion.AutoDetect(connectString));
    });
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    builder.Services.AddApplicationServices();
    builder.Services.AddAuthentication("User_Schema")
        .AddCookie("User_Schema", option =>
        {
            option.LoginPath = new PathString("/login");
            option.LogoutPath = new PathString("/logout");
            option.ExpireTimeSpan = TimeSpan.FromMinutes(builder.Configuration.GetValue<double>("LoginExpireMinute"));
        })
        .AddCookie("Admin_Schema", option =>
        {
            option.LoginPath = new PathString("/admin/login");
            option.LogoutPath = new PathString("/admin/logout");
            option.ExpireTimeSpan = TimeSpan.FromMinutes(builder.Configuration.GetValue<double>("LoginExpireMinute"));
        });
    builder.Services.AddControllersWithViews();
    builder.Host.UseSerilog();
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

    app.UseSerilogRequestLogging(options =>
    {
        // 如果要自訂訊息的範本格式，可以修改這裡，但修改後並不會影響結構化記錄的屬性
        //options.MessageTemplate = "Handled {RequestPath}";

        // 預設輸出的紀錄等級為 Information，你可以在此修改記錄等級
        // options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        // 你可以從 httpContext 取得 HttpContext 下所有可以取得的資訊！
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            diagnosticContext.Set("UserID", httpContext.User.Identity?.Name);
        };
    });

    app.UseStaticFiles();

    app.UseRouting();

    app.Use(async (context, next) =>
    {
        var principal = new ClaimsPrincipal();
        var userResult = await context.AuthenticateAsync("User_Schema");
        var adminResult = await context.AuthenticateAsync("Admin_Schema");

        if (userResult.Principal != null) { principal.AddIdentities(userResult.Principal.Identities); }
        if (adminResult.Principal != null) { principal.AddIdentities(adminResult.Principal.Identities); }

        context.User = principal;
        await next();
    });

    //app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
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
