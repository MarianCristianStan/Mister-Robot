using Microsoft.AspNetCore.Identity;
using Mister_Robot.Data;
using Mister_Robot.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProjectServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Home/Error");
   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

