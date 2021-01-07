
use OnlineStoreManager
go

delete from Categories
SET IDENTITY_INSERT Categories ON
insert into Categories(Id, Name, Description)
values (1, N'Quần nam','Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.'),
(2,N'Áo thun','Aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.'),
(3,N'Áo sơ mi','Irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.'),
(4,N'Nón','Reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.'),
(5,N'Giầy/dép','Voluptate velit esse cillum dolore eu fugiat nulla pariatur.')

SET IDENTITY_INSERT Categories OFF
go

delete from Suppliers
SET IDENTITY_INSERT Suppliers ON
insert into Suppliers(id, Name, Description)
values (1,N'The Ach','Sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.'),
(2,N'Aoh Aoh','Occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.'),
(3,N'Mikoko','Cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.'),
(4,N'Adnoh','Non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.'),
(5,N'Ikuzus','Sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.')
SET IDENTITY_INSERT Suppliers OFF
go

delete from Products
SET IDENTITY_INSERT Products ON

insert into Products(id, Name, Cost,Price, CategoryId,SupplierId)
values 
-- quan
(1, N'Quần tây nam Hàn Quốc',90000, 90000*1.25, 1,1),
(2, N'Quần thun thể thao',70000, 70000*1.25, 1,1),
(3, N'Quần jean nam',75000, 75000*1.25, 1,1),
(4, N'Quần tây đen Hàn Quốc',85000, 85000*1.25, 1,1),
-- ao thun
(5, N'Áo thun basic unisex',50000, 50000*1.25, 2,2),
(6, N'Áo thun Thái BKK',40000, 40000*1.25, 2,2),
(7, N'Áo thun nam Tay lỡ OverTee',30000, 30000*1.25, 2,2),
(8, N'Áo thun Arizona',45000, 45000*1.25, 2,2),
-- so mi
(13, N'Áo sơ mi đen dài tay',90000, 90000*1.5, 3,4),
(14, N'Áo sơ mi trắng',95000, 95000*1.5, 3,4),
(15, N'Áo sơ mi kẻ caro',80000, 80000*1.5, 3,4),
(16, N'Áo sơ mi Verspace',80000, 80000*1.5, 3,4),
-- non
(9, N'Nón lưỡi trai',20000, 20000*1.25, 4,3),
(10, N'Nón Bucket Superman',25000, 25000*1.25, 4,3),
(11, N'Nón Snapback',30000, 30000*1.25, 4,3),
(12, N'Nón cối',35000, 35000*1.25, 4,3),
-- dep
(17, N'Giầy 1',20000, 20000*1.25, 5,5),
(18, N'Giầy 4',20000, 20000*1.25, 5,5),
(19, N'Giầy 2',20000, 20000*1.25, 5,5),
(20, N'Giầy 3',20000, 20000*1.25, 5,5)

SET IDENTITY_INSERT Products OFF
go

update Products 
set ImageUrl = 'default.jpg'
go
