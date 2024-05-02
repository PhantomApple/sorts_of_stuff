USE [Kindergartens1]
GO
/****** Object:  Table [dbo].[Admin_Auth]    Script Date: 26.04.2024 10:16:15 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Childrens]    Script Date: 26.04.2024 10:16:15 ******/
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
/****** Object:  Table [dbo].[Groups]    Script Date: 26.04.2024 10:16:15 ******/
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
/****** Object:  Table [dbo].[Kindergartens]    Script Date: 26.04.2024 10:16:15 ******/
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
/****** Object:  Table [dbo].[Parents]    Script Date: 26.04.2024 10:16:15 ******/
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
/****** Object:  Table [dbo].[Personal]    Script Date: 26.04.2024 10:16:15 ******/
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
