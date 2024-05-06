USE [Kindergartens1]
GO
/****** Object:  Table [dbo].[Admin_Auth]    Script Date: 06.05.2024 9:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin_Auth](
	[ID_employee] [int] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_employee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Childrens]    Script Date: 06.05.2024 9:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Childrens](
	[ID_children] [int] IDENTITY(1,1) NOT NULL,
	[ID_parent] [int] NULL,
	[FIO] [nvarchar](100) NULL,
	[date_of_birth] [date] NULL,
	[Information] [nvarchar](100) NULL,
	[Entrance] [date] NULL,
	[ID_group] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_children] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 06.05.2024 9:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[ID_group] [int] IDENTITY(1,1) NOT NULL,
	[ID_sad] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Older_group] [int] NULL,
	[Level_group] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_group] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kindergartens]    Script Date: 06.05.2024 9:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kindergartens](
	[ID_Sad] [int] IDENTITY(1,1) NOT NULL,
	[ID_direktora] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Adres] [nvarchar](100) NULL,
	[Kontakts] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Sad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parents]    Script Date: 06.05.2024 9:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parents](
	[ID_parent] [int] IDENTITY(1,1) NOT NULL,
	[FIO] [nvarchar](100) NULL,
	[Kontakts] [nvarchar](100) NULL,
	[Informations] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_parent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personal]    Script Date: 06.05.2024 9:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personal](
	[ID_employee] [int] IDENTITY(1,1) NOT NULL,
	[FIO] [nvarchar](100) NULL,
	[Post] [nvarchar](100) NULL,
	[Kontakts] [nvarchar](100) NULL,
	[Experience] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_employee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Admin_Auth] ([ID_employee], [Username], [Password]) VALUES (1029, N'231', N'1211')
INSERT [dbo].[Admin_Auth] ([ID_employee], [Username], [Password]) VALUES (3083, N'111', N'111')
GO
SET IDENTITY_INSERT [dbo].[Childrens] ON 

INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8086, 1003, N'Иванов Иван Иванович', CAST(N'2018-05-15' AS Date), N'Здоров', CAST(N'2024-12-12' AS Date), 2)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8087, 1004, N'Петров Петр Петрович', CAST(N'2017-10-20' AS Date), N'Аллергия на цветы', CAST(N'2021-07-01' AS Date), 1)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8088, 1005, N'Смирнова Ольга Сергеевна', CAST(N'2019-02-12' AS Date), N'Любит рисовать', CAST(N'2022-07-01' AS Date), 4)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8089, 1006, N'Козлова Анна Владимировна', CAST(N'2018-08-25' AS Date), N'Не ест морковь', CAST(N'2022-07-01' AS Date), 2)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8090, 1007, N'Морозов Игорь Александрович', CAST(N'2018-12-30' AS Date), N'Любит играть в футбол', CAST(N'2023-07-01' AS Date), 3)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8091, 1008, N'Попова Екатерина Дмитриевна', CAST(N'2018-07-05' AS Date), N'Часто болеет', CAST(N'2022-07-01' AS Date), 4)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8092, 1009, N'Николаев Владимир Сергеевич', CAST(N'2019-04-10' AS Date), N'Любит собак', CAST(N'2022-07-01' AS Date), 5)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8093, 1010, N'Сидорова Мария Ивановна', CAST(N'2019-11-18' AS Date), N'Хорошо рисует', CAST(N'2022-07-01' AS Date), 6)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8094, 1011, N'Васильев Алексей Николаевич', CAST(N'2019-01-22' AS Date), N'Боится темноты', CAST(N'2023-07-01' AS Date), 7)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8095, 1012, N'Кузнецова Ольга Дмитриевна', CAST(N'2018-09-08' AS Date), N'Любит плавать', CAST(N'2021-07-01' AS Date), 3)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8096, 1013, N'Лебедев Игорь Еленович', CAST(N'2020-06-25' AS Date), N'Любит рисовать', CAST(N'2023-07-01' AS Date), 2)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8097, 1014, N'Королева Анна Артемовна', CAST(N'2021-09-12' AS Date), N'Слабовидящая', CAST(N'2024-07-01' AS Date), 4)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8098, 1015, N'Крылов Илья Олегович', CAST(N'2018-03-17' AS Date), N'Обожает мороженое', CAST(N'2022-07-01' AS Date), 5)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8099, 1016, N'Голубева Ева Денисовна', CAST(N'2020-01-30' AS Date), N'Любит петь', CAST(N'2024-07-01' AS Date), 6)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8100, 1017, N'Белов Дмитрий Алексеевич', CAST(N'2020-11-08' AS Date), N'Хорошо говорит', CAST(N'2024-07-01' AS Date), 3)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8101, 1018, N'Макарова Юлия Павловна', CAST(N'2019-05-22' AS Date), N'Боится птиц', CAST(N'2023-07-01' AS Date), 2)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8102, 1019, N'Зайцев Иван Петрович', CAST(N'2018-10-10' AS Date), N'Любит танцевать', CAST(N'2021-07-01' AS Date), 1)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8103, 1020, N'Рыжова Вера Алексеевна', CAST(N'2019-04-05' AS Date), N'Любит читать книги', CAST(N'2022-07-01' AS Date), 7)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8104, 1021, N'Лисичкин Артем Владимирович', CAST(N'2018-08-14' AS Date), N'Любит играть на гитаре', CAST(N'2021-07-01' AS Date), 7)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8105, 1003, N'Иванова Мария Ивановна', CAST(N'2018-05-15' AS Date), N'Здоровый ребенок', CAST(N'2024-04-28' AS Date), 1)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8106, 1004, N'Петров Сергей Сергеевич', CAST(N'2017-10-20' AS Date), N'Аллергия на пыльцу', CAST(N'2024-04-28' AS Date), 2)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8107, 1005, N'Смирнова Анна Сергеевна', CAST(N'2019-02-12' AS Date), N'Любит рисовать', CAST(N'2024-04-28' AS Date), 3)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8108, 1004, N'Козлов Дмитрий Владимирович', CAST(N'2018-08-25' AS Date), N'Любит играть в футбол', CAST(N'2024-04-28' AS Date), 4)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8109, 1005, N'Соколова Елена Владимировна', CAST(N'2018-12-30' AS Date), N'Любит кататься на велосипеде', CAST(N'2024-04-28' AS Date), 5)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8110, 1006, N'Морозов Артем Александрович', CAST(N'2018-07-05' AS Date), N'Любит рисовать', CAST(N'2024-04-28' AS Date), 6)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8111, 1007, N'Попова Ольга Артемьевна', CAST(N'2019-04-10' AS Date), N'Любит танцевать', CAST(N'2024-04-28' AS Date), 7)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8112, 1008, N'Новиков Илья Николаевич', CAST(N'2019-11-18' AS Date), N'Любит петь', CAST(N'2024-04-28' AS Date), 8)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8113, 1009, N'Федорова Екатерина Игоревна', CAST(N'2019-01-22' AS Date), N'Боится пауков', CAST(N'2024-04-28' AS Date), 9)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8114, 1010, N'Михайлов Даниил Васильевич', CAST(N'2018-09-08' AS Date), N'Любит футбол', CAST(N'2024-04-28' AS Date), 10)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8115, 1011, N'Васильева Алиса Петровна', CAST(N'2020-06-25' AS Date), N'Хорошо учится в школе', CAST(N'2024-04-28' AS Date), 1)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8116, 1012, N'Кузнецов Максим Артемович', CAST(N'2021-09-12' AS Date), N'Любит читать книги', CAST(N'2024-04-28' AS Date), 2)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8117, 1013, N'Петрова Анастасия Сергеевна', CAST(N'2018-03-17' AS Date), N'Хорошо плавает', CAST(N'2024-04-28' AS Date), 3)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8118, 1014, N'Соколов Михаил Игоревич', CAST(N'2020-01-30' AS Date), N'Любит решать головоломки', CAST(N'2024-04-28' AS Date), 4)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8119, 1015, N'Иванов Артем Алексеевич', CAST(N'2020-11-08' AS Date), N'Любит заниматься спортом', CAST(N'2024-04-28' AS Date), 5)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8120, 1016, N'Козлова Виктория Ивановна', CAST(N'2019-05-22' AS Date), N'Любит играть на компьютере', CAST(N'2024-04-28' AS Date), 6)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8121, 1017, N'Смирнов Иван Дмитриевич', CAST(N'2018-10-10' AS Date), N'Любит рисовать', CAST(N'2024-04-28' AS Date), 7)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8122, 1018, N'Попов Владислав Станиславович', CAST(N'2019-04-05' AS Date), N'Хорошо танцует', CAST(N'2024-04-28' AS Date), 8)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8123, 1019, N'Сидорова Наталья Петровна', CAST(N'2018-08-14' AS Date), N'Любит готовить', CAST(N'2024-04-28' AS Date), 9)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8124, 1020, N'Новикова Анна Дмитриевна', CAST(N'2020-04-12' AS Date), N'Любит фотографировать', CAST(N'2024-04-28' AS Date), 10)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8125, 1005, N'Федоров Денис Сергеевич', CAST(N'2020-01-22' AS Date), N'Любит кататься на роликах', CAST(N'2024-04-28' AS Date), 1)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8126, 1021, N'Михайлова Полина Васильевна', CAST(N'2018-09-08' AS Date), N'Хорошо поет', CAST(N'2024-04-28' AS Date), 2)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8127, 1011, N'Васильев Артем Михайлович', CAST(N'2019-04-22' AS Date), N'Любит играть в шахматы', CAST(N'2024-04-28' AS Date), 3)
INSERT [dbo].[Childrens] ([ID_children], [ID_parent], [FIO], [date_of_birth], [Information], [Entrance], [ID_group]) VALUES (8128, 1021, N'Кузнецова Валентина Андреевна', CAST(N'2018-05-15' AS Date), N'Любит путешествовать', CAST(N'2024-04-28' AS Date), 4)
SET IDENTITY_INSERT [dbo].[Childrens] OFF
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 

INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (1, 1002, N'Зайчики', 1024, N'Старшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (2, 1003, N'Медвежата', 1034, N'Младшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (3, 1004, N'Солнышко', 1039, N'Старшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (4, 1005, N'Радуга', 1025, N'Младшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (5, 1006, N'Ромашки', 1026, N'Старшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (6, 1007, N'Ласточки', 1034, N'Младшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (7, 1008, N'Дружок', 1035, N'Старшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (8, 1009, N'Ёжики', 1035, N'Младшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (9, 1010, N'Колобки', 1038, N'Старшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (10, 1011, N'Смешарики', 1039, N'Младшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (11, 1012, N'Пингвинчики', 1040, N'Младшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (12, 1013, N'Львята', 1024, N'Старшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (13, 1014, N'Мишутки', 1025, N'Младшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (14, 1015, N'Сказочники', 1026, N'Старшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (15, 1016, N'Пчелки', 1034, N'Младшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (16, 1017, N'Дельфины', 1035, N'Старшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (17, 1018, N'Лягушата', 1037, N'Младшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (18, 1019, N'Котята', 1038, N'Старшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (19, 1020, N'Звёздочки', 1039, N'Младшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (20, 1021, N'Зайцы', 1040, N'Старшая группа')
INSERT [dbo].[Groups] ([ID_group], [ID_sad], [Name], [Older_group], [Level_group]) VALUES (42, 1002, N'ывф', NULL, N'Средняя группа')
SET IDENTITY_INSERT [dbo].[Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[Kindergartens] ON 

INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1002, 1045, N'Детский сад "Радуга"', N'ул. Пушкина, д. 10', N'89999876543')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1003, 1044, N'Детский сад "Звездочка"', N'ул. Лермонтова, д. 5', N'89992223344')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1004, 1044, N'Детский сад "Сказка"', N'пр. Победы, д. 20', N'89994445566')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1005, 1046, N'Детский сад "Солнышко"', N'ул. Гагарина, д. 15', N'89296667788')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1006, 1044, N'Детский сад "Веселка"', N'пл. Пушкина, д. 18', N'89398889900')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1007, 1044, N'Детский сад "Ромашка"', N'ул. Кирова, д. 7', N'89290001122')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1008, 1045, N'Детский сад "Ласточка"', N'ул. Чехова, д. 25', N'89391121314')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1009, 1046, N'Детский сад "Дружба"', N'пр. Ленина, д. 30', N'89291415161')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1010, 1046, N'Детский сад "Ёжик"', N'ул. Толстого, д. 11', N'89275162636')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1011, 1046, N'Детский сад "Колобок"', N'пр. Горького, д. 8', N'89276237181')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1012, 1045, N'Детский сад "Смешарики"', N'ул. Пушкина, д. 13', N'89277311929')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1013, 1046, N'Детский сад "Малинка"', N'ул. Садовая, д. 10', N'8999)543210')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1014, 1045, N'Детский сад "Дружок"', N'пр. Пионеров, д. 15', N'89991104365')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1015, 1046, N'Детский сад "Радуга"', N'ул. Цветочная, д. 20', N'89999876532')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1016, 1044, N'Детский сад "Зайчик"', N'ул. Луначарского, д. 5', N'89995321098')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1017, 1046, N'Детский сад "Сказочка"', N'пр. Солнечный, д. 30', N'89991010101')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1018, 1045, N'Детский сад "Улыбка"', N'ул. Детства, д. 3', N'89996789010')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1019, 1045, N'Детский сад "Веселый мир"', N'пл. Радости, д. 8', N'89994321098')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1020, 1044, N'Детский сад "Детство"', N'пр. Будущего, д. 12', N'89990987654')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1021, 1044, N'Детский сад "Лучик"', N'ул. Просветления, д. 4', N'89995643210')
INSERT [dbo].[Kindergartens] ([ID_Sad], [ID_direktora], [Name], [Adres], [Kontakts]) VALUES (1022, 1045, N'Детский сад "Солнечный круг"', N'пр. Счастья, д. 7', N'89993214567')
SET IDENTITY_INSERT [dbo].[Kindergartens] OFF
GO
SET IDENTITY_INSERT [dbo].[Parents] ON 

INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1003, N'Иванова Ольга Петровна', N'89192223344', N'Ребенка может забрать муж Иванов Алексей Аркадьевич')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1004, N'Петров Сергей Иванович', N'89284445566', N'Забирают ребенка после 18:00')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1005, N'Смирнова Анна Васильевна', N'89676667788', N'Родители согласились на дополнительные занятия')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1006, N'Козлов Владимир Александрович', N'89998889900', N'Ребенок имеет аллергию на орехи')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1007, N'Морозова Наталья Игоревна', N'89990001122', N'Родители требуют уведомления об изменениях в расписании')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1008, N'Попов Артем Дмитриевич', N'89991121314', N'Близнецы, несмотря на сходство, имеют разные привычки')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1009, N'Николаева Екатерина Сергеевна', N'89991415161', N'Ребенка забирает бабушка по вторникам')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1010, N'Сидоров Игорь Алексеевич', N'89995162636', N'Родители просят не давать сладкое после 18:00')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1011, N'Васильева Мария Николаевна', N'89996237181', N'Ребенок уходит на дополнительные занятия')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1012, N'Кузнецов Дмитрий Павлович', N'89937311929', N'Родители зарегистрированы в группе поддержки')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1013, N'Лебедева Елена Андреевна', N'89991112233', N'Родители платят за дополнительные услуги')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1014, N'Королев Артем Семенович', N'89913334455', N'Ребенок астматик, имеет индивидуальный ингалятор')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1015, N'Крылова Ольга Игоревна', N'89295556677', N'Родители редко появляются на родительских собраниях')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1016, N'Голубев Денис Валерьевич', N'89997778899', N'Родители пожелали уведомления по SMS')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1017, N'Белова Анастасия Павловна', N'89999990011', N'Родители требуют ежедневный рапорт о питании')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1018, N'Макаров Павел Владимирович', N'89991234567', N'Родители переехали, новый адрес: ул. Новая, д. 5, кв. 10')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1019, N'Зайцева Мария Александровна', N'89992345678', N'Родители платят только наличными')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1020, N'Рыжов Алексей Иванович', N'89993456789', N'Родители возят ребенка на дополнительные кружки')
INSERT [dbo].[Parents] ([ID_parent], [FIO], [Kontakts], [Informations]) VALUES (1021, N'Лисичкина Ольга Викторовна', N'89994567890', N'Ребенок боится собак')
SET IDENTITY_INSERT [dbo].[Parents] OFF
GO
SET IDENTITY_INSERT [dbo].[Personal] ON 

INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1024, N'Иванов Иван Иванович', N'Педагог', N'89991234567', N'5')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1025, N'Петрова Мария Сергеевна', N'Воспитатель', N'89991112233', N'10')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1026, N'Смирнов Алексей Николаевич', N'Воспитатель', N'8994445566', N'6')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1027, N'Козлова Ольга Петровна', N'Медсестра', N'89997778899', N'4')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1028, N'Иванова Екатерина Алексеевна', N'Повар', N'89490001122', N'7')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1029, N'Сидоров Владимир Игоревич', N'Администратор', N'89492223344', N'9')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1030, N'Кузнецова Наталья Васильевна', N'Уборщица', N'89495556677', N'2')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1032, N'Морозова Анна Павловна', N'Бухгалтер', N'89391234567', N'21')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1033, N'Васильев Артем Сергеевич', N'Психолог', N'89292345678', N'1')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1034, N'Сквиртачев Ярослав Иванович', N'Педагог', N'89493258789', N'3')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1035, N'Тамаев Игорь Юсуфович', N'Воспитатель', N'89313255739', N'4')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1036, N'Кукурузин Шамсил Вячеславович', N'Бухгалтер', N'89114257769', N'6')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1037, N'Чумачев Асхаб Юнуфович', N'Воспитатель', N'89313255739', N'2')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1038, N'Саралиев Игорь Юсуфович', N'Воспитатель', N'89313255739', N'6')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1039, N'Песков Артемий Владимирович', N'Педагог', N'89241453769', N'4')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1040, N'Игнатьева Ольга Владимировна', N'Воспитатель', N'89998765432', N'7')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1041, N'Федорова Елена Петровна', N'Медсестра', N'89996458721', N'16')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1042, N'Кудряшова Анна Александровна', N'Педиатр', N'89991476532', N'15')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1043, N'Алексеев Алексей Артемович', N'Помощник воспитателя', N'89995432165', N'14')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1044, N'Апаев Константин Дмитриевич', N'Директор', N'89345674323', N'11')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1045, N'Гавривол Дмитрий Александрович', N'Директор', N'89675425764', N'15')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (1046, N'Астачева Алекстандра Николаевна', N'Директор', N'89271345487', N'12')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (3083, N'Воеводов Арсен Эльдарович', N'Администратор', N'89997346453', N'2')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (4093, N'Федоров Виктор Евгеньевич', N'Директор', N'89654567865', N'7')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (4094, N'Евгенич', N'Администратор', N'89272456876', N'21')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (4095, N'ывы', N'Педагог', N'ц', N'2')
INSERT [dbo].[Personal] ([ID_employee], [FIO], [Post], [Kontakts], [Experience]) VALUES (4096, N'выф', N'Директор', N'231', N'32')
SET IDENTITY_INSERT [dbo].[Personal] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Admin_Au__536C85E417D55E2F]    Script Date: 06.05.2024 9:38:30 ******/
ALTER TABLE [dbo].[Admin_Auth] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin_Auth]  WITH CHECK ADD FOREIGN KEY([ID_employee])
REFERENCES [dbo].[Personal] ([ID_employee])
GO
ALTER TABLE [dbo].[Childrens]  WITH CHECK ADD FOREIGN KEY([ID_parent])
REFERENCES [dbo].[Parents] ([ID_parent])
GO
ALTER TABLE [dbo].[Childrens]  WITH CHECK ADD FOREIGN KEY([ID_parent])
REFERENCES [dbo].[Parents] ([ID_parent])
GO
ALTER TABLE [dbo].[Childrens]  WITH CHECK ADD  CONSTRAINT [FK_Childrens_Groups] FOREIGN KEY([ID_group])
REFERENCES [dbo].[Groups] ([ID_group])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Childrens] CHECK CONSTRAINT [FK_Childrens_Groups]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD FOREIGN KEY([ID_sad])
REFERENCES [dbo].[Kindergartens] ([ID_Sad])
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD FOREIGN KEY([ID_sad])
REFERENCES [dbo].[Kindergartens] ([ID_Sad])
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [ID_GroupsPersonal] FOREIGN KEY([Older_group])
REFERENCES [dbo].[Personal] ([ID_employee])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [ID_GroupsPersonal]
GO
ALTER TABLE [dbo].[Kindergartens]  WITH CHECK ADD FOREIGN KEY([ID_direktora])
REFERENCES [dbo].[Personal] ([ID_employee])
GO
ALTER TABLE [dbo].[Kindergartens]  WITH CHECK ADD FOREIGN KEY([ID_direktora])
REFERENCES [dbo].[Personal] ([ID_employee])
GO
