using ADO.NET_CRUD_MVC.Data;
using ADO.NET_CRUD_MVC.Factory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(serviceOptions =>
{
    var configuration = serviceOptions.GetRequiredService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("DefaultConnection")
                           ?? throw new ApplicationException("Connection string is missing");

    return new SqlConnectionFactory(connectionString);
});

builder.Services.AddScoped<ILeadRepository, LeadRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
