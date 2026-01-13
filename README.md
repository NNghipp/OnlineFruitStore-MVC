# MyStore - Cửa Hàng Trái Cây Trực Tuyến

Đây là dự án **demo nhỏ** mô phỏng cửa hàng bán trái cây, được xây dựng bằng **ASP.NET Core MVC**.

## 📝 Giới Thiệu
*   **Chức năng chính**: Cho phép xem danh sách và "mua" trái cây theo kiểu "trá hình" (mô phỏng quy trình, không thanh toán thực).
*   **Trạng thái dự án**: 🚧 Hệ thống vẫn đang trong quá trình **cập nhật từ từ** và sửa lỗi, nên có thể vẫn còn tồn tại bugs.
*   **Kiến trúc**: Tuân theo mô hình 3 lớp (3-Layer Architecture) và Repository Pattern.

## 🚀 Công Nghệ Sử Dụng (Tech Stack)

*   **Backend**: C# .NET 8, ASP.NET Core MVC
*   **Database**: SQLite (Entity Framework Core Code-First)
*   **Frontend**: HTML5, CSS3 (Bootstrap 5), JavaScript (jQuery)
*   **Architecture**: 3-Layer (Business, Repositories, Services) + Repository Pattern
*   **Tools**: Visual Studio Code, .NET CLI

## 📂 Cấu Trúc Dự Án

```
MyStore/
├── .gitignore
├── MyStore.sln                          # Solution file
├── README.md
├── requirements.txt
├── Database/
│   └── create_database.sql
│
├── MyStore.Business/                    # Layer 1: Entities + DbContext
│   ├── MyStore.Business.csproj
│   ├── MyStoreContext.cs                # EF Core DbContext
│   └── Entities/
│       ├── AccountMember.cs             # User (ID, Email, Password, Phone, Address)
│       ├── Category.cs                  # Category model
│       └── Product.cs                   # Product model
│
├── MyStore.Repositories/                # Layer 2: Repository Pattern
│   ├── MyStore.Repositories.csproj
│   ├── IRepository.cs                   # Generic interface
│   ├── Repository.cs                    # Generic implementation
│   ├── IProductRepository.cs
│   ├── ProductRepository.cs
│   ├── ICategoryRepository.cs
│   ├── CategoryRepository.cs
│   ├── IAccountMemberRepository.cs
│   └── AccountMemberRepository.cs
│
├── MyStore.Services/                    # Layer 3: Business Logic
│   ├── MyStore.Services.csproj
│   ├── IProductService.cs
│   ├── ProductService.cs
│   ├── ICategoryService.cs
│   ├── CategoryService.cs
│   ├── IAccountMemberService.cs
│   └── AccountMemberService.cs
│
└── MyStore.WebApp/                      # Presentation Layer (MVC)
    ├── MyStore.WebApp.csproj
    ├── Program.cs                       # Startup & DI config
    ├── appsettings.json
    ├── MyStoreDB.db                     # SQLite Database
    │
    ├── Controllers/
    │   ├── HomeController.cs
    │   ├── ProductsController.cs        # + Image upload, Duplicate check, Filter
    │   ├── CategoriesController.cs      # + Duplicate check
    │   ├── CartController.cs            # Shopping cart
    │   ├── AuthController.cs            # Login, Register, Logout
    │   ├── ProfileController.cs         # User profile management
    │   └── AccountMembersController.cs
    │
    ├── Models/
    │   ├── CartItem.cs                  # Shopping cart item
    │   ├── LoginViewModel.cs            # Login form
    │   ├── RegisterViewModel.cs         # Register form
    │   ├── ProfileViewModels.cs         # ChangePassword, UpdateContact
    │   └── ErrorViewModel.cs
    │
    ├── Helpers/
    │   └── FruitImageHelper.cs          # Image mapping by product name
    │
    ├── Services/
    │   └── CartService.cs               # Session-based cart
    │
    ├── Views/
    │   ├── _ViewImports.cshtml
    │   ├── _ViewStart.cshtml
    │   ├── Shared/
    │   │   ├── _Layout.cshtml           # Main layout
    │   │   ├── _Header.cshtml           # Header (cart/user icons)
    │   │   ├── _Nav.cshtml              # Navigation
    │   │   ├── _Footer.cshtml           # Footer
    │   │   └── Error.cshtml
    │   ├── Home/                        # Index, About, Contact, Privacy
    │   ├── Products/                    # CRUD + Category filter
    │   ├── Categories/                  # CRUD
    │   ├── Cart/                        # Index, Checkout
    │   ├── Auth/                        # Login, Register (social buttons)
    │   ├── Profile/                     # Index, ChangePassword, UpdateContact
    │   └── AccountMembers/              # CRUD
    │
    └── wwwroot/                         # Static files
        ├── css/
        │   └── site.css                 # Fruit theme (green/orange)
        ├── js/
        │   └── site.js
        ├── lib/                         # Bootstrap, jQuery
        └── assets/
            └── products/                # Product images
                ├── apple.png
                ├── banana.png
                ├── orange.png
                ├── peach.jpg
                └── strawberry.png
```

## 🛠️ Hướng Dẫn Cài Đặt & Chạy Web

### Lần Đầu Tiên (First Run)
Khi chạy lần đầu, ứng dụng sẽ tự động tạo database (`MyStoreDB.db`) và thêm dữ liệu mẫu (Sản phẩm trái cây, Danh mục, Tài khoản).

1.  Mở terminal tại thư mục gốc của Solution (`d:\MVC - ASP.NET\MyStore`).
2.  Khôi phục các thư viện:
    ```bash
    dotnet restore
    ```
3.  Build dự án:
    ```bash
    dotnet build
    ```
4.  Chạy ứng dụng:
    ```bash
    dotnet run --project MyStore.WebApp
    ```
    *Truy cập vào: `http://localhost:5000`*

### Các Lần Sau
Chỉ cần chạy lệnh sau để khởi động server:
```bash
dotnet run --project MyStore.WebApp
# Hoặc chế độ watch để tự động reload khi sửa code:
dotnet watch run --project MyStore.WebApp
```

## 📖 Hướng Dẫn Sử Dụng

### 1. Trang Chủ (Home)
- Hiển thị banner giới thiệu cửa hàng.
- Danh sách các danh mục trái cây (Nội địa, Nhập khẩu...).
- Danh sách **Sản Phẩm Nổi Bật** với hình ảnh minh họa và giá tiền (VND).

### 2. Quản Lý Sản Phẩm (Products)
- Truy cập menu **Products** để xem danh sách toàn bộ sản phẩm.
- **Thêm mới**: Nhấn nút "Create New".
- **Chỉnh sửa**: Nhấn nút "Edit" trên từng sản phẩm.
- **Xem chi tiết**: Nhấn nút "Details" để xem thông tin chi tiết (Hình ảnh, Giá, Tồn kho).
- **Xóa**: Nhấn nút "Delete" để xóa sản phẩm.

### 3. Quản Lý Danh Mục (Categories)
- Truy cập menu **Categories** để quản lý loại sản phẩm.

### 4. Tài Khoản Thành Viên (Members)
- Truy cập menu **Members** để xem danh sách thành viên (Admin/User).
- **Tài khoản mẫu**:
  - Admin: `admin@mystore.com`
  - User: `user@mystore.com`

---

