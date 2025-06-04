USE [MarketPlace]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 04/06/2025 14:56:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Type] [varchar](1) NOT NULL,
	[Description] [varchar](100) NULL,
	[Country] [varchar](50) NOT NULL,
	[State] [varchar](100) NOT NULL,
	[Street] [varchar](150) NOT NULL,
	[Number] [smallint] NOT NULL,
	[Complement] [varchar](100) NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bios]    Script Date: 04/06/2025 14:56:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[StoreName] [varchar](150) NOT NULL,
	[Description] [varchar](500) NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_Bios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phones]    Script Date: 04/06/2025 14:56:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Type] [varchar](1) NOT NULL,
	[Description] [varchar](100) NULL,
	[PhoneNumber] [varchar](25) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04/06/2025 14:56:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](150) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Password] [varchar](500) NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[IsBlocked] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Users]
GO
ALTER TABLE [dbo].[Bios]  WITH CHECK ADD  CONSTRAINT [FK_Bios_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Bios] CHECK CONSTRAINT [FK_Bios_Users]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK_Phones_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK_Phones_Users]
GO
