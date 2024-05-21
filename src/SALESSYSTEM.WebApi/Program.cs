using SALESSYSTEM.IOC.Extensions;
using SALESSYSTEM.WebApi.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;

builder.Services.AddHttpClient("IgnoreSSL", client =>
{
    // Opcionalmente, configurar la base address o headers aquí si es necesario
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    };
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInjectionIOC(Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

    

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
