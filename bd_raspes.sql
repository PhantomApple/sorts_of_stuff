USE [Raspesanie]
GO
/****** Object:  Table [dbo].[Discipline]    Script Date: 12.06.2024 20:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discipline](
	[id_discipline] [int] IDENTITY(1,1) NOT NULL,
	[id_kafedra] [int] NULL,
	[id_teach] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Сode] [nvarchar](100) NULL,
	[number_hours] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_discipline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 12.06.2024 20:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[id_group] [int] IDENTITY(1,1) NOT NULL,
	[id_kafedra] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Сode] [nvarchar](100) NULL,
	[Rykovod] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_group] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kabinet]    Script Date: 12.06.2024 20:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kabinet](
	[id_office] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NULL,
	[Korpus] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_office] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kafedra]    Script Date: 12.06.2024 20:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kafedra](
	[id_kafedra] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Сode] [nvarchar](100) NULL,
	[Zaveduchi] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_kafedra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prepods]    Script Date: 12.06.2024 20:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prepods](
	[id_teach] [int] IDENTITY(1,1) NOT NULL,
	[id_kafedra] [int] NULL,
	[Familia] [nvarchar](100) NULL,
	[Name] [nvarchar](100) NULL,
	[Otchestvo] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_teach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Raspes]    Script Date: 12.06.2024 20:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Raspes](
	[id_raspes] [int] IDENTITY(1,1) NOT NULL,
	[id_discipline] [int] NULL,
	[id_teach] [int] NULL,
	[id_group] [int] NULL,
	[id_office] [int] NULL,
	[id_user] [int] NULL,
	[DayNedel] [nvarchar](100) NULL,
	[hours_passed] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_raspes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12.06.2024 20:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](100) NULL,
	[Pass] [nvarchar](100) NULL,
	[Familia] [nvarchar](100) NULL,
	[Name] [nvarchar](100) NULL,
	[Otchestvo] [nvarchar](100) NULL,
	[Post] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Discipline] ON 

INSERT [dbo].[Discipline] ([id_discipline], [id_kafedra], [id_teach], [Name], [Сode], [number_hours]) VALUES (1, 1, 1, N'Дисциплина 1', N'D001', 10)
INSERT [dbo].[Discipline] ([id_discipline], [id_kafedra], [id_teach], [Name], [Сode], [number_hours]) VALUES (2, 1, 2, N'Дисциплина 2', N'D002', 15)
INSERT [dbo].[Discipline] ([id_discipline], [id_kafedra], [id_teach], [Name], [Сode], [number_hours]) VALUES (3, 1, 3, N'Дисциплина 3', N'D003', 12)
SET IDENTITY_INSERT [dbo].[Discipline] OFF
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 

INSERT [dbo].[Groups] ([id_group], [id_kafedra], [Name], [Сode], [Rykovod]) VALUES (1, 1, N'Группа 1', N'G001', N'Руководитель1')
INSERT [dbo].[Groups] ([id_group], [id_kafedra], [Name], [Сode], [Rykovod]) VALUES (2, 2, N'Группа 2', N'G002', N'Руководитель2')
INSERT [dbo].[Groups] ([id_group], [id_kafedra], [Name], [Сode], [Rykovod]) VALUES (3, 3, N'Группа 3', N'G003', N'Руководитель3')
SET IDENTITY_INSERT [dbo].[Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[Kabinet] ON 

INSERT [dbo].[Kabinet] ([id_office], [Number], [Korpus]) VALUES (1, 101, N'Корпус 1')
INSERT [dbo].[Kabinet] ([id_office], [Number], [Korpus]) VALUES (2, 202, N'Корпус 2')
INSERT [dbo].[Kabinet] ([id_office], [Number], [Korpus]) VALUES (3, 303, N'Корпус 3')
SET IDENTITY_INSERT [dbo].[Kabinet] OFF
GO
SET IDENTITY_INSERT [dbo].[Kafedra] ON 

INSERT [dbo].[Kafedra] ([id_kafedra], [Name], [Сode], [Zaveduchi]) VALUES (1, N'Кафедра 1', N'001', N'Заведующий1')
INSERT [dbo].[Kafedra] ([id_kafedra], [Name], [Сode], [Zaveduchi]) VALUES (2, N'Кафедра 2', N'002', N'Заведующий2')
INSERT [dbo].[Kafedra] ([id_kafedra], [Name], [Сode], [Zaveduchi]) VALUES (3, N'Кафедра 3', N'003', N'Заведующий3')
SET IDENTITY_INSERT [dbo].[Kafedra] OFF
GO
SET IDENTITY_INSERT [dbo].[Prepods] ON 

INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (1, 1, N'Ткачук', N'Н.', N'В.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (2, 1, N'Никитина', N'Э.', N'В.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (3, 1, N'Ольховикова', N'Н.', N'Д.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (4, 1, N'Казарова', N'Ю.', N'И.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (5, 1, N'Аксенова', N'Д.', N'А.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (6, 1, N'Конкина', N'Д.', N'В.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (7, 1, N'Тимошина', N'О.', N'В.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (8, 1, N'Гузь', N'Д.', N'В.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (9, 1, N'Прозорова', N'М.', N'И.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (10, 1, N'Мищенкова', N'Ю.', N'А.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (11, 1, N'Лохомонов', N'Е.', N'А.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (12, 1, N'Синельникова', N'Е.', N'Ю.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (13, 1, N'Гаврилова', N'М.', N'В.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (14, 1, N'Бобкова', N'О.', N'В.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (15, 1, N'Леонтьева', N'М.', N'С.')
INSERT [dbo].[Prepods] ([id_teach], [id_kafedra], [Familia], [Name], [Otchestvo]) VALUES (16, 1, N'Батурин', N'В.', N'Ю.')
SET IDENTITY_INSERT [dbo].[Prepods] OFF
GO
SET IDENTITY_INSERT [dbo].[Raspes] ON 

INSERT [dbo].[Raspes] ([id_raspes], [id_discipline], [id_teach], [id_group], [id_office], [id_user], [DayNedel], [hours_passed]) VALUES (2, 1, 1, 1, 1, 1, N'Понедельник', 6)
INSERT [dbo].[Raspes] ([id_raspes], [id_discipline], [id_teach], [id_group], [id_office], [id_user], [DayNedel], [hours_passed]) VALUES (3, 2, 2, 2, 2, 2, N'Вторник', 8)
INSERT [dbo].[Raspes] ([id_raspes], [id_discipline], [id_teach], [id_group], [id_office], [id_user], [DayNedel], [hours_passed]) VALUES (4, 3, 3, 3, 3, 3, N'Среда', 9)
SET IDENTITY_INSERT [dbo].[Raspes] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id_user], [Login], [Pass], [Familia], [Name], [Otchestvo], [Post]) VALUES (1, N'user1', N'password1', N'Иванов', N'Иван', N'Иванович', N'Пост1')
INSERT [dbo].[Users] ([id_user], [Login], [Pass], [Familia], [Name], [Otchestvo], [Post]) VALUES (2, N'user2', N'password2', N'Петров', N'Петр', N'Петрович', N'Пост2')
INSERT [dbo].[Users] ([id_user], [Login], [Pass], [Familia], [Name], [Otchestvo], [Post]) VALUES (3, N'user3', N'password3', N'Сидоров', N'Сидор', N'Сидорович', N'Пост3')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Discipline]  WITH CHECK ADD FOREIGN KEY([id_kafedra])
REFERENCES [dbo].[Kafedra] ([id_kafedra])
GO
ALTER TABLE [dbo].[Discipline]  WITH CHECK ADD FOREIGN KEY([id_teach])
REFERENCES [dbo].[Prepods] ([id_teach])
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD FOREIGN KEY([id_kafedra])
REFERENCES [dbo].[Kafedra] ([id_kafedra])
GO
ALTER TABLE [dbo].[Prepods]  WITH CHECK ADD FOREIGN KEY([id_kafedra])
REFERENCES [dbo].[Kafedra] ([id_kafedra])
GO
ALTER TABLE [dbo].[Raspes]  WITH CHECK ADD FOREIGN KEY([id_discipline])
REFERENCES [dbo].[Discipline] ([id_discipline])
GO
ALTER TABLE [dbo].[Raspes]  WITH CHECK ADD FOREIGN KEY([id_group])
REFERENCES [dbo].[Groups] ([id_group])
GO
ALTER TABLE [dbo].[Raspes]  WITH CHECK ADD FOREIGN KEY([id_office])
REFERENCES [dbo].[Kabinet] ([id_office])
GO
ALTER TABLE [dbo].[Raspes]  WITH CHECK ADD FOREIGN KEY([id_teach])
REFERENCES [dbo].[Prepods] ([id_teach])
GO
ALTER TABLE [dbo].[Raspes]  WITH CHECK ADD FOREIGN KEY([id_user])
REFERENCES [dbo].[Users] ([id_user])
GO
