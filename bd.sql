USE [master]
GO
/****** Object:  Database [Auto_workshop]    Script Date: 29.05.2024 19:11:17 ******/
CREATE DATABASE [Auto_workshop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Auto_workshop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Auto_workshop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Auto_workshop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Auto_workshop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Auto_workshop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Auto_workshop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Auto_workshop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Auto_workshop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Auto_workshop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Auto_workshop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Auto_workshop] SET ARITHABORT OFF 
GO
ALTER DATABASE [Auto_workshop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Auto_workshop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Auto_workshop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Auto_workshop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Auto_workshop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Auto_workshop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Auto_workshop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Auto_workshop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Auto_workshop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Auto_workshop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Auto_workshop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Auto_workshop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Auto_workshop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Auto_workshop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Auto_workshop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Auto_workshop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Auto_workshop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Auto_workshop] SET RECOVERY FULL 
GO
ALTER DATABASE [Auto_workshop] SET  MULTI_USER 
GO
ALTER DATABASE [Auto_workshop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Auto_workshop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Auto_workshop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Auto_workshop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Auto_workshop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Auto_workshop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Auto_workshop', N'ON'
GO
ALTER DATABASE [Auto_workshop] SET QUERY_STORE = ON
GO
ALTER DATABASE [Auto_workshop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Auto_workshop]
GO
/****** Object:  Table [dbo].[Car]    Script Date: 29.05.2024 19:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[ID_car] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](100) NULL,
	[Brand] [nvarchar](max) NULL,
	[Year_release] [date] NULL,
	[FIO_owner] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_car] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Masters]    Script Date: 29.05.2024 19:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Masters](
	[ID_master] [int] IDENTITY(1,1) NOT NULL,
	[FIO_master] [nvarchar](100) NULL,
	[Experience] [int] NULL,
	[Discharge] [int] NULL,
	[Personal_number] [int] NULL,
	[Login] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_master] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 29.05.2024 19:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID_order] [int] IDENTITY(1,1) NOT NULL,
	[ID_car] [int] NULL,
	[ID_master] [int] NULL,
	[Price] [bigint] NULL,
	[date_of_work] [date] NULL,
	[type_of_work] [nvarchar](max) NULL,
	[plannerd_end_date] [date] NULL,
	[real_end_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_order] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Car] ON 

INSERT [dbo].[Car] ([ID_car], [Number], [Brand], [Year_release], [FIO_owner]) VALUES (1, N'А231УК', N'Toyota', CAST(N'2022-01-01' AS Date), N'Валера Серый')
INSERT [dbo].[Car] ([ID_car], [Number], [Brand], [Year_release], [FIO_owner]) VALUES (2, N'И324КУ', N'Matiz', CAST(N'2021-05-15' AS Date), N'Игорь Матизов')
INSERT [dbo].[Car] ([ID_car], [Number], [Brand], [Year_release], [FIO_owner]) VALUES (3, N'Щ487МЛ', N'Ford', CAST(N'2020-09-30' AS Date), N'Alice Johnson')
INSERT [dbo].[Car] ([ID_car], [Number], [Brand], [Year_release], [FIO_owner]) VALUES (4, N'У974ЗС', N'Chevrolet', CAST(N'2019-11-20' AS Date), N'Михаил Рыбов')
INSERT [dbo].[Car] ([ID_car], [Number], [Brand], [Year_release], [FIO_owner]) VALUES (5, N'З098ДК', N'Volkswagen', CAST(N'2018-07-08' AS Date), N'Екатерина Иванова')
INSERT [dbo].[Car] ([ID_car], [Number], [Brand], [Year_release], [FIO_owner]) VALUES (6, N'С999РУ', N'BMW', CAST(N'2017-04-25' AS Date), N'Дмитрий Петров')
INSERT [dbo].[Car] ([ID_car], [Number], [Brand], [Year_release], [FIO_owner]) VALUES (7, N'У305КУ', N'Mercedes-Benz', CAST(N'2016-10-12' AS Date), N'Анна Смирнова')
INSERT [dbo].[Car] ([ID_car], [Number], [Brand], [Year_release], [FIO_owner]) VALUES (8, N'Г707ДН', N'Audi', CAST(N'2015-03-30' AS Date), N'Алексей Козлов')
INSERT [dbo].[Car] ([ID_car], [Number], [Brand], [Year_release], [FIO_owner]) VALUES (9, N'К845УС', N'Hyundai', CAST(N'2014-09-15' AS Date), N'Ольга Васильева')
INSERT [dbo].[Car] ([ID_car], [Number], [Brand], [Year_release], [FIO_owner]) VALUES (10, N'Ж452ЕК', N'Kia', CAST(N'2013-06-02' AS Date), N'Ирина Николаева')
SET IDENTITY_INSERT [dbo].[Car] OFF
GO
SET IDENTITY_INSERT [dbo].[Masters] ON 

INSERT [dbo].[Masters] ([ID_master], [FIO_master], [Experience], [Discharge], [Personal_number], [Login], [Password]) VALUES (1, N'Иванов Петр Сергеевич', 5, 1, 12345, N'vanek23', N'SecurePass123')
INSERT [dbo].[Masters] ([ID_master], [FIO_master], [Experience], [Discharge], [Personal_number], [Login], [Password]) VALUES (2, N'Смирнова Елена Анатольевна', 8, 2, 54321, N'smirnova_master', N'StrongPassword789')
INSERT [dbo].[Masters] ([ID_master], [FIO_master], [Experience], [Discharge], [Personal_number], [Login], [Password]) VALUES (3, N'Козлов Дмитрий Владимирович', 10, 3, 98765, N'kozlikovArt', N'Password1234')
INSERT [dbo].[Masters] ([ID_master], [FIO_master], [Experience], [Discharge], [Personal_number], [Login], [Password]) VALUES (4, N'Петрова Ольга Ивановна', 6, 1, 45678, N'petrovams', N'MasterPass456')
INSERT [dbo].[Masters] ([ID_master], [FIO_master], [Experience], [Discharge], [Personal_number], [Login], [Password]) VALUES (5, N'Николаев Алексей Петрович', 7, 2, 13579, N'nikolaev_master', N'SecretKey246')
INSERT [dbo].[Masters] ([ID_master], [FIO_master], [Experience], [Discharge], [Personal_number], [Login], [Password]) VALUES (6, N'Иванова Мария Сергеевна', 4, 1, 24680, N'ivanova_master', N'AccessGranted789')
INSERT [dbo].[Masters] ([ID_master], [FIO_master], [Experience], [Discharge], [Personal_number], [Login], [Password]) VALUES (7, N'Кузнецов Владимир Александрович', 9, 3, 65432, N'kuznetsov_master', N'MasterKey999')
INSERT [dbo].[Masters] ([ID_master], [FIO_master], [Experience], [Discharge], [Personal_number], [Login], [Password]) VALUES (8, N'Соколова Анна Александровна', 5, 1, 56789, N'sokolaster', N'MasterPass777')
INSERT [dbo].[Masters] ([ID_master], [FIO_master], [Experience], [Discharge], [Personal_number], [Login], [Password]) VALUES (9, N'Попов Игорь Дмитриевич', 3, 1, 98765, N'popovigorean', N'SecureAccess888')
INSERT [dbo].[Masters] ([ID_master], [FIO_master], [Experience], [Discharge], [Personal_number], [Login], [Password]) VALUES (10, N'Лебедева Екатерина Васильевна', 6, 2, 23456, N'lebedevaa', N'Pass123345')
SET IDENTITY_INSERT [dbo].[Masters] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (2, 1, 1, 2000, CAST(N'2024-05-01' AS Date), N'Замена масла', CAST(N'2024-05-02' AS Date), CAST(N'2023-02-02' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (3, 2, 3, 5000, CAST(N'2024-05-03' AS Date), N'Замена тормозных колодок', CAST(N'2023-05-05' AS Date), CAST(N'2023-05-08' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (4, 3, 2, 100000, CAST(N'2024-05-06' AS Date), N'Ремонт двигателя', CAST(N'2024-05-08' AS Date), CAST(N'2024-06-03' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (5, 4, 4, 3000, CAST(N'2024-05-09' AS Date), N'Замена фильтра воздушного', CAST(N'2024-04-10' AS Date), CAST(N'2024-05-10' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (6, 5, 5, 2000, CAST(N'2024-05-11' AS Date), N'Шиномонтаж', CAST(N'2022-02-12' AS Date), CAST(N'2022-02-12' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (7, 6, 3, 90000, CAST(N'2024-05-13' AS Date), N'Покраска кузова', CAST(N'2021-05-15' AS Date), CAST(N'2021-05-20' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (8, 7, 7, 20000, CAST(N'2024-05-16' AS Date), N'Замена передних амортизаторов', CAST(N'2021-04-18' AS Date), CAST(N'2021-05-21' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (9, 8, 9, 30000, CAST(N'2024-05-19' AS Date), N'Ремонт электрики', CAST(N'2021-05-21' AS Date), CAST(N'2021-06-21' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (10, 9, 9, 3000, CAST(N'2024-05-22' AS Date), N'Плановое ТО', CAST(N'2024-05-24' AS Date), CAST(N'2024-05-24' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (11, 10, 9, 4000, CAST(N'2024-05-25' AS Date), N'Замена ремня ГРМ', CAST(N'2022-05-27' AS Date), CAST(N'2022-05-27' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (1002, 7, 5, 30000, CAST(N'2023-04-10' AS Date), N'Ремонт двигателя', CAST(N'2023-04-15' AS Date), CAST(N'2023-04-19' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (1003, 9, 9, 10000, CAST(N'2024-03-05' AS Date), N'Замена расходников', CAST(N'2024-03-06' AS Date), CAST(N'2024-03-06' AS Date))
INSERT [dbo].[Orders] ([ID_order], [ID_car], [ID_master], [Price], [date_of_work], [type_of_work], [plannerd_end_date], [real_end_date]) VALUES (1004, 7, 7, 5000, CAST(N'2024-01-25' AS Date), N'Замена расходников', CAST(N'2024-01-26' AS Date), CAST(N'2024-01-26' AS Date))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
ALTER TABLE [dbo].[Masters]  WITH CHECK ADD  CONSTRAINT [FK_workshop_Masters] FOREIGN KEY([ID_master])
REFERENCES [dbo].[Masters] ([ID_master])
GO
ALTER TABLE [dbo].[Masters] CHECK CONSTRAINT [FK_workshop_Masters]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_workshop_Orders] FOREIGN KEY([ID_car])
REFERENCES [dbo].[Car] ([ID_car])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_workshop_Orders]
GO
USE [master]
GO
ALTER DATABASE [Auto_workshop] SET  READ_WRITE 
GO
