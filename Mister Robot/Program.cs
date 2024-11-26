using Mister_Robot.Services.Interfaces;
using Mister_Robot.Services;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Repositories;
using Microsoft.AspNetCore.Identity;
using Mister_Robot.Models;
using Mister_Robot.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserAddressRepository, UserAddressRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ICPURepository, CPURepository>();
builder.Services.AddScoped<IGPURepository, GPURepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserAddressService, UserAddressService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ICPUService, CPUService>();
builder.Services.AddScoped<IGPUService, GPUService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<UserManager<User>>();


builder.Services.AddRazorPages();
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<MisterRobotContext>();
builder.Services.AddDbContext<MisterRobotContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MisterRobotDB")));



builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = true;
	options.Password.RequireLowercase = false;
	options.Password.RequiredUniqueChars = 6;

	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
	options.Lockout.MaxFailedAccessAttempts = 10;
	options.Lockout.AllowedForNewUsers = true;

	options.User.RequireUniqueEmail = true;
	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

	options.SignIn.RequireConfirmedPhoneNumber = false;
	options.Stores.MaxLengthForKeys = 128;
});


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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

await SeedRoles(app);

	
app.Run();


async Task SeedRoles(IHost app)
{
	using (var scope = app.Services.CreateScope())
	{
		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
		await ContextSeed.SeedRolesAsync(roleManager);
	}
}

