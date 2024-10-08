USE [master]
GO
/****** Object:  Database [OnlineCateringDB]    Script Date: 8/24/2024 8:15:47 AM ******/
CREATE DATABASE [OnlineCateringDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineCateringDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.HOANNC\MSSQL\DATA\OnlineCateringDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnlineCateringDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.HOANNC\MSSQL\DATA\OnlineCateringDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [OnlineCateringDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineCateringDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineCateringDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineCateringDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineCateringDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OnlineCateringDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineCateringDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET RECOVERY FULL 
GO
ALTER DATABASE [OnlineCateringDB] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineCateringDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineCateringDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineCateringDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineCateringDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnlineCateringDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnlineCateringDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OnlineCateringDB', N'ON'
GO
ALTER DATABASE [OnlineCateringDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [OnlineCateringDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [OnlineCateringDB]
GO
/****** Object:  Table [dbo].[Caterer]    Script Date: 8/24/2024 8:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Caterer](
	[CatererId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[PinCode] [nvarchar](10) NULL,
	[Phone] [nvarchar](15) NULL,
	[Mobile] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[CatererId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/24/2024 8:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[PinCode] [nvarchar](10) NULL,
	[Phone] [nvarchar](15) NULL,
	[Mobile] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerInvoice]    Script Date: 8/24/2024 8:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerInvoice](
	[InvoiceNo] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[OrderNo] [int] NULL,
	[CustomerId] [int] NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustOrder]    Script Date: 8/24/2024 8:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustOrder](
	[OrderNo] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[CustomerId] [int] NULL,
	[CatererId] [int] NULL,
	[CostPerPlate] [decimal](18, 2) NULL,
	[MinPeople] [int] NULL,
	[MaxPeople] [int] NULL,
	[DeliveryDate] [datetime] NULL,
	[DeliveryAddress] [nvarchar](255) NULL,
	[OrderStatus] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustOrderChild]    Script Date: 8/24/2024 8:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustOrderChild](
	[OrderNo] [int] NOT NULL,
	[MenuItemNo] [int] NOT NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderNo] ASC,
	[MenuItemNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FavoriteCaterer]    Script Date: 8/24/2024 8:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FavoriteCaterer](
	[FavoriteId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[CatererId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[FavoriteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginMaster]    Script Date: 8/24/2024 8:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginMaster](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[UserType] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 8/24/2024 8:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuItemNo] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](255) NOT NULL,
	[Price] [decimal](18, 2) NULL,
	[CategoryId] [int] NULL,
	[CatererId] [int] NULL,
	[Describe] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MenuItemNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuCategory]    Script Date: 8/24/2024 8:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuCategory](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 8/24/2024 8:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NULL,
	[ReceiverId] [int] NULL,
	[MessageText] [nvarchar](max) NULL,
	[SentDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Caterer] ON 

INSERT [dbo].[Caterer] ([CatererId], [Name], [Address], [PinCode], [Phone], [Mobile]) VALUES (1, N'hoannb', N'NInh BInh', N'18001920', N'2031203', N'1203210')
INSERT [dbo].[Caterer] ([CatererId], [Name], [Address], [PinCode], [Phone], [Mobile]) VALUES (2, N'caterer1', N'LonDon', N'1293909', N'1293021', N'1230211')
INSERT [dbo].[Caterer] ([CatererId], [Name], [Address], [PinCode], [Phone], [Mobile]) VALUES (3, N'caterer2', N'UK', N'1930121', N'1230123', N'2131023')
SET IDENTITY_INSERT [dbo].[Caterer] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerId], [Name], [Address], [PinCode], [Phone], [Mobile]) VALUES (1, N'customer1', N'LonDon', N'19201', N'102931023', N'1293012931')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerInvoice] ON 

INSERT [dbo].[CustomerInvoice] ([InvoiceNo], [InvoiceDate], [OrderNo], [CustomerId], [TotalAmount]) VALUES (1, CAST(N'2024-08-23T23:41:31.003' AS DateTime), 2, 1, CAST(150.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[CustomerInvoice] OFF
GO
SET IDENTITY_INSERT [dbo].[CustOrder] ON 

INSERT [dbo].[CustOrder] ([OrderNo], [OrderDate], [CustomerId], [CatererId], [CostPerPlate], [MinPeople], [MaxPeople], [DeliveryDate], [DeliveryAddress], [OrderStatus]) VALUES (1, CAST(N'2024-08-23T23:33:24.423' AS DateTime), 1, 1, CAST(15.50 AS Decimal(18, 2)), 50, 100, CAST(N'2024-09-01T15:30:00.000' AS DateTime), N'123 Catering St, Food City', N'INIT')
INSERT [dbo].[CustOrder] ([OrderNo], [OrderDate], [CustomerId], [CatererId], [CostPerPlate], [MinPeople], [MaxPeople], [DeliveryDate], [DeliveryAddress], [OrderStatus]) VALUES (2, CAST(N'2024-08-23T23:41:30.793' AS DateTime), 1, 1, CAST(15.50 AS Decimal(18, 2)), 50, 100, CAST(N'2024-09-01T15:30:00.000' AS DateTime), N'123 Catering St, Food City', N'INIT')
SET IDENTITY_INSERT [dbo].[CustOrder] OFF
GO
INSERT [dbo].[CustOrderChild] ([OrderNo], [MenuItemNo], [Quantity]) VALUES (2, 4, 5)
INSERT [dbo].[CustOrderChild] ([OrderNo], [MenuItemNo], [Quantity]) VALUES (2, 5, 10)
GO
SET IDENTITY_INSERT [dbo].[LoginMaster] ON 

INSERT [dbo].[LoginMaster] ([UserId], [Name], [Password], [UserType]) VALUES (1, N'admin', N'6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', N'ADMIN')
INSERT [dbo].[LoginMaster] ([UserId], [Name], [Password], [UserType]) VALUES (2, N'hoannbnc', N'3753e29051c7244fc6983cfe473dcde431a14969de7e3102ce41442058f817fc', N'Customer')
INSERT [dbo].[LoginMaster] ([UserId], [Name], [Password], [UserType]) VALUES (3, N'hoannb', N'3753e29051c7244fc6983cfe473dcde431a14969de7e3102ce41442058f817fc', N'Caterer')
INSERT [dbo].[LoginMaster] ([UserId], [Name], [Password], [UserType]) VALUES (4, N'customer1', N'6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', N'Customer')
INSERT [dbo].[LoginMaster] ([UserId], [Name], [Password], [UserType]) VALUES (5, N'caterer1', N'6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', N'Caterer')
INSERT [dbo].[LoginMaster] ([UserId], [Name], [Password], [UserType]) VALUES (6, N'caterer2', N'6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', N'Caterer')
SET IDENTITY_INSERT [dbo].[LoginMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (4, N'Spinach soup', CAST(10.00 AS Decimal(18, 2)), 1, 1, N'Milk, eggs, spinach, garlic, onion, potato, chicken stock, lemon')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (5, N'Tomato soup', CAST(10.00 AS Decimal(18, 2)), 1, 1, N'Tomatos, onion, carrot, celery, olive oil, tomato puree, sugar, vegetable soup')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (6, N'Mushroom soup', CAST(25.00 AS Decimal(18, 2)), 1, 1, N'Mushrooms, olive oil, garlic, onion, sea salt, black pepper, persley, cheese')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (7, N'Cracking cobb salad', CAST(15.00 AS Decimal(18, 2)), 2, 1, N'Chicken thighs, pepper, olive oil, salt, pancetta, avocado, tomato, cheese, buttermilk')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (8, N'Waldorf salad', CAST(12.00 AS Decimal(18, 2)), 2, 1, N'Grapes, lemon, olive oil, sea salt, black pepper, walnuts, yoghurt, celery')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (9, N'Spring quiche', CAST(17.00 AS Decimal(18, 2)), 2, 1, N'Asparagus, spinach, onion, milk, cheese, black pepper')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (10, N'Grilled chicken kebabs', CAST(25.00 AS Decimal(18, 2)), 3, 1, N'Skinless chicken breast, black pepper, red pepper, passata, ginger, garlic')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (11, N'BBQ beef short ribs', CAST(15.00 AS Decimal(18, 2)), 3, 1, N'Short ribs, olive oil, caraway seeds, yoghurt, carrots, onions, white cabbage')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (12, N'Southern fired chicken', CAST(15.00 AS Decimal(18, 2)), 3, 1, N'Chicken thighs, chicken drumsticks, potatoes, ayenne pepper')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (13, N'Sicilian-style tuna carpaccio', CAST(15.00 AS Decimal(18, 2)), 3, 1, N'Tuna steak, rose vine, red chilli, lemon, fresh basil, fresh dill, garlic, olive oil')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (14, N'Turkey tonnato', CAST(15.00 AS Decimal(18, 2)), 3, 1, N'Turkey breast, red chilli, olive oil, tinned tuna, lemon, black pepper, red wine vinegar')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (15, N'Next level steak', CAST(15.00 AS Decimal(18, 2)), 3, 1, N'Flank skirt steak, onion, butter, olive oil, red wine vinegar')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (16, N'Chocolate & orange plate', CAST(11.00 AS Decimal(18, 2)), 4, 1, N'Dark melted chocolate, juicy orange, sugar, lemon juice, vanilla sticks')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (17, N'Jamaican strawberry cake', CAST(11.00 AS Decimal(18, 2)), 4, 1, N'Strawberry, golden syrup, brown sugar, cream cheese, cinnamon')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (18, N'Ile flottante', CAST(11.00 AS Decimal(18, 2)), 4, 1, N'Vanilla pod, rhubard, golden caster sugar, orange, milk, double cream')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (19, N'Panna cotta', CAST(11.00 AS Decimal(18, 2)), 4, 1, N'Milk, powdered gelatin, whipping cream, honey, sugar, salt, fresh berries')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (20, N'Tasty tiramisu cake', CAST(11.00 AS Decimal(18, 2)), 4, 1, N'Egg yolks, sugar, milk, mascarpone cheese, coffee, brandy, cocoa powder, chocolate')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (21, N'Tangerine & vanilla cup', CAST(11.00 AS Decimal(18, 2)), 4, 1, N'Tangerine, vanilla sticks, milk, brown sugar, double cream')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (22, N'Spinach soup, roast pork', CAST(30.00 AS Decimal(18, 2)), 5, 1, N'Milk, eggs, spinach, garlic, onion, potato, chicken stock, lemon')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (23, N'Tomato soup, fried chicken', CAST(30.00 AS Decimal(18, 2)), 5, 1, N'Tomatos, onion, carrot, celery, olive oil, tomato puree, sugar, vegetable soup')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (24, N'Mushroom soup, beef steak', CAST(30.00 AS Decimal(18, 2)), 5, 1, N'Mushrooms, olive oil, garlic, onion, sea salt, black pepper, persley, cheese')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (25, N'Royal chicken salad', CAST(25.00 AS Decimal(18, 2)), 6, 1, N'Chicken thighs, pepper, olive oil, salt, pancetta, avocado, tomato, cheese, buttermilk')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (26, N'Royal beef steak', CAST(25.00 AS Decimal(18, 2)), 6, 1, N'Grapes, lemon, olive oil, sea salt, black pepper, walnuts, yoghurt, celery')
INSERT [dbo].[Menu] ([MenuItemNo], [ItemName], [Price], [CategoryId], [CatererId], [Describe]) VALUES (27, N'Chicken mushroom sauce', CAST(25.00 AS Decimal(18, 2)), 6, 1, N'Asparagus, spinach, onion, milk, cheese, black pepper')
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[MenuCategory] ON 

INSERT [dbo].[MenuCategory] ([CategoryId], [Category]) VALUES (1, N'Soups')
INSERT [dbo].[MenuCategory] ([CategoryId], [Category]) VALUES (2, N'Salads')
INSERT [dbo].[MenuCategory] ([CategoryId], [Category]) VALUES (3, N'Main dishes')
INSERT [dbo].[MenuCategory] ([CategoryId], [Category]) VALUES (4, N'Deserts')
INSERT [dbo].[MenuCategory] ([CategoryId], [Category]) VALUES (5, N'Lunch menus')
INSERT [dbo].[MenuCategory] ([CategoryId], [Category]) VALUES (6, N'Dinner menus')
SET IDENTITY_INSERT [dbo].[MenuCategory] OFF
GO
ALTER TABLE [dbo].[Messages] ADD  DEFAULT (getdate()) FOR [SentDate]
GO
ALTER TABLE [dbo].[Messages] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[CustomerInvoice]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerInvoice]  WITH CHECK ADD FOREIGN KEY([OrderNo])
REFERENCES [dbo].[CustOrder] ([OrderNo])
GO
ALTER TABLE [dbo].[CustOrder]  WITH CHECK ADD FOREIGN KEY([CatererId])
REFERENCES [dbo].[Caterer] ([CatererId])
GO
ALTER TABLE [dbo].[CustOrder]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustOrderChild]  WITH CHECK ADD FOREIGN KEY([MenuItemNo])
REFERENCES [dbo].[Menu] ([MenuItemNo])
GO
ALTER TABLE [dbo].[CustOrderChild]  WITH CHECK ADD FOREIGN KEY([OrderNo])
REFERENCES [dbo].[CustOrder] ([OrderNo])
GO
ALTER TABLE [dbo].[FavoriteCaterer]  WITH CHECK ADD FOREIGN KEY([CatererId])
REFERENCES [dbo].[Caterer] ([CatererId])
GO
ALTER TABLE [dbo].[FavoriteCaterer]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[MenuCategory] ([CategoryId])
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD FOREIGN KEY([CatererId])
REFERENCES [dbo].[Caterer] ([CatererId])
GO
USE [master]
GO
ALTER DATABASE [OnlineCateringDB] SET  READ_WRITE 
GO
