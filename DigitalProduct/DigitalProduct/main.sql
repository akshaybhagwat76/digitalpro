USE [DigitalProduct]
GO
/****** Object:  Table [dbo].[category]    Script Date: 09/17/2018 18:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](128) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[category] ON
INSERT [dbo].[category] ([Id], [CategoryName], [IsActive], [IsDeleted], [Status]) VALUES (1, N'Google Product', 1, 0, 1)
INSERT [dbo].[category] ([Id], [CategoryName], [IsActive], [IsDeleted], [Status]) VALUES (2, N'Aniseed Syrup', 1, 0, 1)
INSERT [dbo].[category] ([Id], [CategoryName], [IsActive], [IsDeleted], [Status]) VALUES (3, N'Ikura', 1, 0, 1)
SET IDENTITY_INSERT [dbo].[category] OFF
/****** Object:  Table [dbo].[user_role]    Script Date: 09/17/2018 18:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[user_role] ON
INSERT [dbo].[user_role] ([Id], [RoleName]) VALUES (1, N'admin')
INSERT [dbo].[user_role] ([Id], [RoleName]) VALUES (2, N'user')
SET IDENTITY_INSERT [dbo].[user_role] OFF
/****** Object:  Table [dbo].[userprofile]    Script Date: 09/17/2018 18:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[userprofile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](200) NULL,
	[FullName] [nvarchar](200) NULL,
	[Mobile] [nvarchar](12) NULL,
	[Address] [nvarchar](300) NULL,
	[City] [nvarchar](50) NULL,
	[PinCode] [nvarchar](6) NULL,
	[State] [nvarchar](50) NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeactive] [bit] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[EmailVerified] [bit] NOT NULL,
	[ActivationCode] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[userprofile] ON
INSERT [dbo].[userprofile] ([UserId], [UserName], [Password], [FullName], [Mobile], [Address], [City], [PinCode], [State], [CreatedById], [CreatedDate], [IsDeactive], [RoleId], [Email], [EmailVerified], [ActivationCode]) VALUES (1, N'admin', N'123qwe', N'admin', N'123123456', N'surat', N'surat', N'394210', N'gujarat', 1, CAST(0x0000A91D00000000 AS DateTime), 0, 1, N'admin@gmail.com', 1, NULL)
SET IDENTITY_INSERT [dbo].[userprofile] OFF
/****** Object:  Table [dbo].[product]    Script Date: 09/17/2018 18:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](128) NOT NULL,
	[Title] [varchar](256) NULL,
	[Description] [varchar](256) NULL,
	[Price] [float] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NULL,
	[Status] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[product] ON
INSERT [dbo].[product] ([Id], [ProductName], [Title], [Description], [Price], [IsActive], [IsDeleted], [Status], [CategoryId]) VALUES (1, N'Mango', N'mango title', N'Mango description', 23, 1, 1, 1, 1)
INSERT [dbo].[product] ([Id], [ProductName], [Title], [Description], [Price], [IsActive], [IsDeleted], [Status], [CategoryId]) VALUES (2, N'mango', N'mango Title', N'mango description edit', 23, 1, 0, 1, 3)
INSERT [dbo].[product] ([Id], [ProductName], [Title], [Description], [Price], [IsActive], [IsDeleted], [Status], [CategoryId]) VALUES (3, N'Facebook business', N'title product', N'this is description', 2500, 1, 0, 1, 2)
SET IDENTITY_INSERT [dbo].[product] OFF
/****** Object:  ForeignKey [FK_ProductCategory]    Script Date: 09/17/2018 18:42:47 ******/
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[category] ([Id])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_ProductCategory]
GO
/****** Object:  ForeignKey [FK__UserProfi__RoleI__0AD2A005]    Script Date: 09/17/2018 18:42:47 ******/
ALTER TABLE [dbo].[userprofile]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[user_role] ([Id])
GO
