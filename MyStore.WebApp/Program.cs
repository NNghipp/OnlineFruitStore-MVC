using Microsoft.EntityFrameworkCore;
using MyStore.Business;
using MyStore.Repositories;
using MyStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext - Using SQLite for easy testing
builder.Services.AddDbContext<MyStoreContext>(options =>
    options.UseSqlite("Data Source=MyStoreDB.db"));

// Register Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAccountMemberRepository, AccountMemberRepository>();

// Register Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAccountMemberService, AccountMemberService>();

// Configure Session for Shopping Cart
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<MyStore.WebApp.Services.ICartService, MyStore.WebApp.Services.CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

// Custom short routes
app.MapControllerRoute(
    name: "login",
    pattern: "Login",
    defaults: new { controller = "Auth", action = "Login" });

app.MapControllerRoute(
    name: "register",
    pattern: "Register",
    defaults: new { controller = "Auth", action = "Register" });

app.MapControllerRoute(
    name: "logout",
    pattern: "Logout",
    defaults: new { controller = "Auth", action = "Logout" });

app.MapControllerRoute(
    name: "profile",
    pattern: "Profile/{action=Index}",
    defaults: new { controller = "Profile" });

app.MapControllerRoute(
    name: "cart",
    pattern: "Cart/{action=Index}",
    defaults: new { controller = "Cart" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MyStoreContext>();
    context.Database.EnsureCreated();
    
    // Seed data if empty
    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new MyStore.Business.Entities.Category { CategoryName = "Trái Cây Nội Địa" },
            new MyStore.Business.Entities.Category { CategoryName = "Trái Cây Nhập Khẩu" },
            new MyStore.Business.Entities.Category { CategoryName = "Quà Tặng Trái Cây" }
        );
        context.SaveChanges();
    }
    
    if (!context.Products.Any())
    {
        context.Products.AddRange(
            // Táo - Trái cây nhập khẩu (ID 2)
            new MyStore.Business.Entities.Product { 
                ProductName = "Táo Envy Mỹ", 
                CategoryID = 2, 
                UnitPrice = 300000m, 
                UnitsInStock = 50,
                ImageFile = "apple.png"
            },
            // Đào - Trái cây nhập khẩu (ID 2)
            new MyStore.Business.Entities.Product { 
                ProductName = "Đào Tiên Nhật Bản", 
                CategoryID = 2, 
                UnitPrice = 400000m, 
                UnitsInStock = 30,
                ImageFile = "peach.jpg"
            },
            // Cam - Trái cây nội địa (ID 1)
            new MyStore.Business.Entities.Product { 
                ProductName = "Cam Sành Vĩnh Long", 
                CategoryID = 1, 
                UnitPrice = 200000m, 
                UnitsInStock = 100,
                ImageFile = "orange.png"
            },
            // Dâu - Trái cây nhập khẩu (ID 2)
            new MyStore.Business.Entities.Product { 
                ProductName = "Dâu Tây Hàn Quốc", 
                CategoryID = 2, 
                UnitPrice = 1000000m, 
                UnitsInStock = 20,
                ImageFile = "strawberry.png"
            },
            // Chuối - Trái cây nội địa (ID 1)
            new MyStore.Business.Entities.Product { 
                ProductName = "Chuối Laba Đà Lạt", 
                CategoryID = 1, 
                UnitPrice = 50000m, 
                UnitsInStock = 200,
                ImageFile = "banana.png"
            }
        );
        context.SaveChanges();
    }
    
    if (!context.AccountMembers.Any())
    {
        context.AccountMembers.AddRange(
            new MyStore.Business.Entities.AccountMember { MemberPassword = "admin123", FullName = "Admin User", EmailAddress = "admin@mystore.com", MemberRole = "Admin" },
            new MyStore.Business.Entities.AccountMember { MemberPassword = "user123", FullName = "Regular User", EmailAddress = "user@mystore.com", MemberRole = "User" }
        );
        context.SaveChanges();
    }
}

app.Run();
