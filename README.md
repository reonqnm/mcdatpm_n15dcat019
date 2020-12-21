# mcdatpm_n15dcat019
Các Lưu Ý :
- Code được sử dụng kết hợp các công nghệ như ASP 2.0 và SQL server 2019 , Visual Studio 2019
- Publish Database ở phần DB.Core .
- Chỉnh sửa các thông số kết nối cho phù hợp ở mục "ConnectionStrings" ở file appsettings.json ( API.Core , API.Authentication , API.JWT.Authentication ).
- Import file DBCore.bacpac vào DB hoặc Tạo mới Database DBCore và bảng DB.Employee ( với các thông tin ID , FUllName ,Phone ,Email ,Password, Birthday ).
+ Sever Xác thực ( Server Auth ) : API.Authentication , API.JWT.Authentication 
+ Client : WebInside
+ API cần bảo vệ ( Resource Server ): API.Core
###
Để test ta click phải Solution 'WebInside' chọn Properties ( Alt + Enter )
Chọn mục Startup Project - > Multiple startup projects rồi chọn các file

+ JWT : API.Core, API.JWT.Authentication , WebInside.
+ IdentityServer4 : API.Core , API.Authentication , WebInside .

Chọn nút Run trên trong chương trình hoặc nhấn F5 để thao tác chạy.
