using Microsoft.AspNetCore.Identity;
using Mister_Robot.Data;
using Mister_Robot.Extensions;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProjectServices(builder.Configuration);



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Home/Error");
   app.UseHsts();
   app.UseResponseCompression();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "Sitemap",
	pattern: "sitemap.xml",
	defaults: new { controller = "Home", action = "Sitemap" });


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

