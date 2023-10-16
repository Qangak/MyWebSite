use Webbatdongsan 
go
CREATE TABLE [dbo].[Category] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [IDCate]   NCHAR (20)     NOT NULL,
    [NameCate] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([IDCate] ASC)
);

CREATE TABLE [dbo].[Customer] (
   IdNguoiDung INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
   username VARCHAR(255),
   password VARCHAR(255),
   email VARCHAR(255),
   HoTen VARCHAR(255),
   Phone NVARCHAR(15),
   NgaySinh DATE,
   Role VARCHAR(10)
);

CREATE TABLE [dbo].[Products] (
    [ProductID]     INT             IDENTITY (1, 1) NOT NULL,
    [NamePro]       NVARCHAR (MAX)  NULL,
    [DecriptionPro] NVARCHAR (MAX)  NULL,
    [Category]      NCHAR (20)      NULL,
    [Price]         INT NULL,
    [ImagePro]      NVARCHAR (MAX)  NULL,
	[ImagePro1]      NVARCHAR (MAX)  NULL,
	[ImagePro2]      NVARCHAR (MAX)  NULL,
    [Area]          FLOAT NULL,
    [Address]       NVARCHAR(MAX)   NULL,
    [Phone]         INT NULL,
    [LienHe]        VARCHAR(12),
    [NgayDang]      DATETIME,
    [IdNguoiDung]   INT FOREIGN KEY REFERENCES [Customer](IdNguoiDung),
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [FK_Pro_Category] FOREIGN KEY ([Category]) REFERENCES [dbo].[Category] ([IDCate])
);

CREATE TABLE [dbo].[YeuThich] (
   idYeuThich INT PRIMARY KEY,
   IdNguoiDung INT,
   ProductID INT,
   FOREIGN KEY (IdNguoiDung) REFERENCES [dbo].[Customer](IdNguoiDung),
   FOREIGN KEY (ProductID) REFERENCES [dbo].[Products](ProductID)
);

select * from Customer

alter table [dbo].[Products]
add [WhiteList] int null,
	[IDCate] int null