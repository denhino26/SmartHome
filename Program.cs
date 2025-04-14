var builder = WebApplication.CreateBuilder(args);

// Voeg deze services toe:
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages();

var app = builder.Build();

// ... bestaande middleware ...

app.UseRouting();
app.UseAuthorization();

// Voeg deze toe voor sessieondersteuning
app.UseSession();

app.MapRazorPages();


app.Run();