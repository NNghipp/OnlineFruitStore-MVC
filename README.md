# MyStore - Cửa Hàng Trái Cây Trực Tuyến

Dự án website quản lý cửa hàng bán trái cây được xây dựng bằng **ASP.NET Core MVC**, tuân theo kiến trúc 3 lớp (3-Layer Architecture).

## 🚀 Công Nghệ Sử Dụng (Tech Stack)

*   **Backend**: C# .NET 8, ASP.NET Core MVC
*   **Database**: SQLite (Entity Framework Core Code-First)
*   **Frontend**: HTML5, CSS3 (Bootstrap 5), JavaScript (jQuery)
*   **Architecture**: 3-Layer (Business, Repositories, Services) + Repository Pattern
*   **Tools**: Visual Studio Code, .NET CLI

## 📂 Cấu Trúc Dự Án

```
📂 MyStore.sln
├── 📂 MyStore.Business          # Layer 1: Entities + DbContext
│   ├── 📂 Entities
│   │   ├── AccountMember.cs
│   │   ├── Category.cs
│   │   └── Product.cs
│   └── MyStoreContext.cs
├── 📂 MyStore.Repositories      # Layer 2: Repository Pattern
│   ├── IRepository.cs
│   ├── Repository.cs
│   ├── IProductRepository.cs
│   ├── ProductRepository.cs
│   ├── ICategoryRepository.cs
│   ├── CategoryRepository.cs
│   ├── IAccountMemberRepository.cs
│   └── AccountMemberRepository.cs
├── 📂 MyStore.Services          # Layer 3: Business Logic
│   ├── IProductService.cs
│   ├── ProductService.cs
│   ├── ICategoryService.cs
│   ├── CategoryService.cs
│   ├── IAccountMemberService.cs
│   └── AccountMemberService.cs
├── 📂 MyStore.WebApp            # Presentation Layer (MVC)
│   ├── 📂 Controllers
│   │   ├── ProductsController.cs
│   │   ├── CategoriesController.cs
│   │   └── AccountMembersController.cs
│   ├── 📂 Views
│   │   ├── Products (Index, Create, Edit, Details, Delete)
│   │   ├── Categories (Index, Create, Edit, Details, Delete)
│   │   └── AccountMembers (Index, Create, Edit, Details, Delete)
│   ├── appsettings.json
│   └── Program.cs
└── 📂 Database
    └── create_database.sql      # SQL script tạo DB
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
© 2024 MyStore Fruit Shop. All rights reserved.
