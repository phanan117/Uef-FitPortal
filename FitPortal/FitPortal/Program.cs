using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using FitPortal.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
//for Identity
builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
        .AddEntityFrameworkStores<DatabaseContext>()
        .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAuthentication/Login");
//Authentication
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
        googleOptions.ClientId = googleAuthNSection["ClientId"];
        googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
        googleOptions.CallbackPath = "/sign-in-google";

    });
//Session for login
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
//Inject
builder.Services.AddTransient<IWorkRepository, WorkRepository>();
builder.Services.AddTransient<ITeacherPositionRepository, TeacherPositionRepository>();
builder.Services.AddTransient<ITeacherUserRepository, TeacherUserRepository>();
builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
builder.Services.AddTransient<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();


//Build
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
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=AdminHome}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
