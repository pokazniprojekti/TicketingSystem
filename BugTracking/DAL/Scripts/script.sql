USE [master]
GO
/****** Object:  Database [BugTracking]    Script Date: 5.3.2018. 16:36:51 ******/
CREATE DATABASE [BugTracking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BugTracking', FILENAME = N'D:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\BugTracking.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BugTracking_log', FILENAME = N'D:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\BugTracking_log.ldf' , SIZE = 15296KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BugTracking] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BugTracking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BugTracking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BugTracking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BugTracking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BugTracking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BugTracking] SET ARITHABORT OFF 
GO
ALTER DATABASE [BugTracking] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BugTracking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BugTracking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BugTracking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BugTracking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BugTracking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BugTracking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BugTracking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BugTracking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BugTracking] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BugTracking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BugTracking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BugTracking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BugTracking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BugTracking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BugTracking] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BugTracking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BugTracking] SET RECOVERY FULL 
GO
ALTER DATABASE [BugTracking] SET  MULTI_USER 
GO
ALTER DATABASE [BugTracking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BugTracking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BugTracking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BugTracking] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BugTracking] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BugTracking', N'ON'
GO
USE [BugTracking]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Active] [bit] NULL,
	[LastModified] [datetime] NOT NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	[Active] [bit] NOT NULL,
	[LastModified] [datetime] NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Category_Active]  DEFAULT ((1)),
	[LastModified] [datetime] NULL,
	[Section] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Organization](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Location] [varchar](100) NOT NULL,
	[TelephoneNumber] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Organization_Active]  DEFAULT ((1)),
	[LastModified] [datetime] NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Priority]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Priority](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Priority_Active]  DEFAULT ((1)),
	[LastModified] [datetime] NULL,
	[TimeIncrementInMinutes] [int] NULL,
 CONSTRAINT [PK_Priority] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_Prod] [nchar](100) NULL,
	[Description_Prod] [nvarchar](300) NULL,
	[Active] [bit] NULL,
	[LastModified] [datetime] NULL,
 CONSTRAINT [PK_System] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductOrganisation]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOrganisation](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Product] [bigint] NOT NULL,
	[ID_Organisation] [bigint] NOT NULL,
	[Active] [bit] NULL,
	[LastModified] [datetime] NULL,
 CONSTRAINT [PK_SystemOrganisation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Responsible]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Responsible](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Product] [bigint] NOT NULL,
	[ID_User] [bigint] NOT NULL,
	[Active] [bit] NULL,
	[LastModified] [datetime] NULL,
 CONSTRAINT [PK_SystemTechnician] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Status]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Status](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Status_Active]  DEFAULT ((1)),
	[LastModified] [datetime] NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticket](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[CategoryID] [smallint] NOT NULL,
	[PriorityID] [tinyint] NOT NULL,
	[StatusID] [tinyint] NOT NULL,
	[DateCreated] [datetime] NULL,
	[Deadline] [datetime] NULL,
	[Subject] [varchar](max) NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Ticket_Active]  DEFAULT ((1)),
	[LastModified] [datetime] NULL,
	[AssigneeID] [bigint] NULL,
	[ProductID] [bigint] NOT NULL,
	[Body] [varchar](max) NOT NULL,
	[OrganizationID] [bigint] NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 5.3.2018. 16:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_User_Active]  DEFAULT ((1)),
	[LastModified] [datetime] NULL,
	[OrganizationID] [bigint] NOT NULL,
	[TelephoneNumber] [varchar](50) NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Description], [Active], [LastModified]) VALUES (N'17d630a0-0359-487e-895d-735e4fbfda01', N'Technician', N'Technician', 1, CAST(N'2017-03-06 10:24:56.033' AS DateTime))
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Description], [Active], [LastModified]) VALUES (N'6c73c6b4-4862-4fa9-82c6-0a0e1aba6356', N'User', N'Regular User', 1, CAST(N'2017-03-06 10:25:13.913' AS DateTime))
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Description], [Active], [LastModified]) VALUES (N'8', N'Admin', N'Admin', 1, CAST(N'2017-02-22 16:58:04.703' AS DateTime))
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Active], [LastModified]) VALUES (N'2166a021-c541-4e92-887d-9907ecba65d4', N'8', 1, CAST(N'2018-03-03 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Active], [LastModified]) VALUES (N'37f8bf38-80eb-43c2-9148-e51ba291fc66', N'17d630a0-0359-487e-895d-735e4fbfda01', 1, CAST(N'2018-03-03 16:39:53.327' AS DateTime))
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Active], [LastModified]) VALUES (N'4834d289-44fb-4908-83b1-28f62b6a1eab', N'6c73c6b4-4862-4fa9-82c6-0a0e1aba6356', 1, CAST(N'2018-03-03 16:39:44.607' AS DateTime))
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Active], [LastModified]) VALUES (N'6066c9af-7ad9-4b06-921d-6cdb13ae2ada', N'17d630a0-0359-487e-895d-735e4fbfda01', 1, CAST(N'2018-03-03 16:39:59.450' AS DateTime))
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Active], [LastModified]) VALUES (N'7b6d7292-1ecd-4b37-b34e-5814de60d45d', N'6c73c6b4-4862-4fa9-82c6-0a0e1aba6356', 1, CAST(N'2018-03-03 16:39:36.927' AS DateTime))
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Active]) VALUES (N'2166a021-c541-4e92-887d-9907ecba65d4', N'admin@microsoft.com', 0, N'AB8Rpn8C3m1mmWB66tLssDqBqslQsvNdTSLbQJWU3jPZTkaGgVMhIrxr+BNpd6HZiQ==', N'6f969baa-168c-42f2-b732-5578151b480c', NULL, 0, 0, NULL, 1, 0, N'admin@microsoft.com', 1)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Active]) VALUES (N'37f8bf38-80eb-43c2-9148-e51ba291fc66', N'technician1@microsoft.com', 0, N'AOkoekBaAQtpxFcTh15uJXr0t69AMeEhcXL+YcE88gPable6MDs2733ZOkAzFE0faQ==', N'8ccbf995-4883-4cd9-89a3-bcda92d65cf6', NULL, 0, 0, NULL, 1, 0, N'technician1@microsoft.com', 1)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Active]) VALUES (N'4834d289-44fb-4908-83b1-28f62b6a1eab', N'osoba2@tvrtka2.com', 0, N'ABuGP1OVDaksl6/Hnus2n2a+oldQAas6UvFnfzyawn4Xzj4H92uyBzHRwRt1hLA5pg==', N'a0c706cf-10e5-4a36-a90b-afa54e8573be', NULL, 0, 0, NULL, 1, 0, N'osoba2@tvrtka2.com', 1)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Active]) VALUES (N'6066c9af-7ad9-4b06-921d-6cdb13ae2ada', N'technician2@microsoft.com', 0, N'ANlfPB8UVmTyoF1tTBzxYDidumISI/d2A3vSurB1D0hoAOIBs251Y63hWhl1j4d9RQ==', N'55704743-0ce7-4a3a-9b6e-64ebce46b0b3', NULL, 0, 0, NULL, 1, 0, N'technician2@microsoft.com', 1)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Active]) VALUES (N'7b6d7292-1ecd-4b37-b34e-5814de60d45d', N'osoba1@tvrtka1.com', 0, N'AKE5WVIy4ZcHTvFeZr8gSGQPts1lwSLI9vZfuI7UyVH2X4+AKty0GVge1jbXN4xfDQ==', N'e4ae7bd2-3bad-4540-a17c-e9333de3c169', NULL, 0, 0, NULL, 1, 0, N'osoba1@tvrtka1.com', 1)
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

GO
INSERT [dbo].[Category] ([ID], [Name], [Description], [Active], [LastModified], [Section]) VALUES (21, N'Bug', N'Bug', 1, CAST(N'2018-03-02 14:40:09.147' AS DateTime), N'Technical')
GO
INSERT [dbo].[Category] ([ID], [Name], [Description], [Active], [LastModified], [Section]) VALUES (22, N'Feature Request', N'Feature Request', 1, CAST(N'2018-03-02 14:40:28.190' AS DateTime), N'Technical')
GO
INSERT [dbo].[Category] ([ID], [Name], [Description], [Active], [LastModified], [Section]) VALUES (24, N'Sales Question', N'Sales Question', 1, CAST(N'2018-03-02 14:41:31.697' AS DateTime), N'Sales')
GO
INSERT [dbo].[Category] ([ID], [Name], [Description], [Active], [LastModified], [Section]) VALUES (25, N'How To', N'How To', 1, CAST(N'2018-03-02 14:41:44.107' AS DateTime), N'Technical')
GO
INSERT [dbo].[Category] ([ID], [Name], [Description], [Active], [LastModified], [Section]) VALUES (26, N'Technical Issue', N'Technical Issue', 1, CAST(N'2018-03-02 14:41:57.650' AS DateTime), N'Technical')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Organization] ON 

GO
INSERT [dbo].[Organization] ([ID], [Name], [Location], [TelephoneNumber], [Email], [Active], [LastModified]) VALUES (33, N'Microsoft', N'Zeleni trg', N'015657897', N'microsoft@microsoft.com', 1, CAST(N'2018-03-02 13:24:00.173' AS DateTime))
GO
INSERT [dbo].[Organization] ([ID], [Name], [Location], [TelephoneNumber], [Email], [Active], [LastModified]) VALUES (35, N'Tvrtka1', N'Lokacija1', N'014455678', N'tvrtka1@tvrtka1.com', 1, CAST(N'2018-03-02 13:37:19.307' AS DateTime))
GO
INSERT [dbo].[Organization] ([ID], [Name], [Location], [TelephoneNumber], [Email], [Active], [LastModified]) VALUES (36, N'Tvrtka2', N'Lokacija2', N'01452345', N'tvrtka2@tvrtka2.com', 1, CAST(N'2018-03-02 13:37:45.953' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Organization] OFF
GO
SET IDENTITY_INSERT [dbo].[Priority] ON 

GO
INSERT [dbo].[Priority] ([ID], [Name], [Description], [Active], [LastModified], [TimeIncrementInMinutes]) VALUES (10, N'Priority 1', N'Critical business impact', 1, CAST(N'2018-03-02 14:44:02.663' AS DateTime), NULL)
GO
INSERT [dbo].[Priority] ([ID], [Name], [Description], [Active], [LastModified], [TimeIncrementInMinutes]) VALUES (11, N'Priority 2', N'Significant business impact', 1, CAST(N'2018-03-02 14:44:17.890' AS DateTime), NULL)
GO
INSERT [dbo].[Priority] ([ID], [Name], [Description], [Active], [LastModified], [TimeIncrementInMinutes]) VALUES (12, N'Priority 3', N'Limited business impact', 1, CAST(N'2018-03-02 14:44:33.740' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Priority] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

GO
INSERT [dbo].[Product] ([ID], [Name_Prod], [Description_Prod], [Active], [LastModified]) VALUES (29, N'Skype                                                                                               ', N'Skype for conversation', 1, CAST(N'2018-03-02 13:24:44.640' AS DateTime))
GO
INSERT [dbo].[Product] ([ID], [Name_Prod], [Description_Prod], [Active], [LastModified]) VALUES (30, N'Visual Studio                                                                                       ', N'IDE', 1, CAST(N'2018-03-02 13:24:59.117' AS DateTime))
GO
INSERT [dbo].[Product] ([ID], [Name_Prod], [Description_Prod], [Active], [LastModified]) VALUES (32, N'Visual Studio Code                                                                                  ', N'IDE', 1, CAST(N'2018-03-02 13:38:41.277' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductOrganisation] ON 

GO
INSERT [dbo].[ProductOrganisation] ([ID], [ID_Product], [ID_Organisation], [Active], [LastModified]) VALUES (34, 29, 33, 1, CAST(N'2017-03-08 10:10:38.033' AS DateTime))
GO
INSERT [dbo].[ProductOrganisation] ([ID], [ID_Product], [ID_Organisation], [Active], [LastModified]) VALUES (35, 30, 33, 1, CAST(N'2017-03-08 10:10:43.103' AS DateTime))
GO
INSERT [dbo].[ProductOrganisation] ([ID], [ID_Product], [ID_Organisation], [Active], [LastModified]) VALUES (36, 29, 35, 1, CAST(N'2018-03-02 13:47:48.537' AS DateTime))
GO
INSERT [dbo].[ProductOrganisation] ([ID], [ID_Product], [ID_Organisation], [Active], [LastModified]) VALUES (37, 30, 36, 1, CAST(N'2018-03-02 13:48:13.703' AS DateTime))
GO
INSERT [dbo].[ProductOrganisation] ([ID], [ID_Product], [ID_Organisation], [Active], [LastModified]) VALUES (38, 32, 35, 1, CAST(N'2018-03-02 13:48:20.913' AS DateTime))
GO
INSERT [dbo].[ProductOrganisation] ([ID], [ID_Product], [ID_Organisation], [Active], [LastModified]) VALUES (39, 32, 33, 1, CAST(N'2018-03-02 13:48:36.340' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ProductOrganisation] OFF
GO
SET IDENTITY_INSERT [dbo].[Responsible] ON 

GO
INSERT [dbo].[Responsible] ([ID], [ID_Product], [ID_User], [Active], [LastModified]) VALUES (5, 29, 40, 1, CAST(N'2018-03-02 14:24:07.927' AS DateTime))
GO
INSERT [dbo].[Responsible] ([ID], [ID_Product], [ID_User], [Active], [LastModified]) VALUES (6, 30, 40, 1, CAST(N'2018-03-02 14:24:15.523' AS DateTime))
GO
INSERT [dbo].[Responsible] ([ID], [ID_Product], [ID_User], [Active], [LastModified]) VALUES (7, 32, 41, 1, CAST(N'2018-03-02 14:24:34.803' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Responsible] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

GO
INSERT [dbo].[Status] ([ID], [Name], [Description], [Active], [LastModified]) VALUES (24, N'Solved', N'Solved', 1, CAST(N'2018-03-02 14:37:09.477' AS DateTime))
GO
INSERT [dbo].[Status] ([ID], [Name], [Description], [Active], [LastModified]) VALUES (25, N'Closed', N'Closed', 1, CAST(N'2018-03-02 14:37:22.173' AS DateTime))
GO
INSERT [dbo].[Status] ([ID], [Name], [Description], [Active], [LastModified]) VALUES (26, N'In Progress', N'In Progress', 1, CAST(N'2018-03-02 14:37:43.487' AS DateTime))
GO
INSERT [dbo].[Status] ([ID], [Name], [Description], [Active], [LastModified]) VALUES (27, N'New', N'New', 1, CAST(N'2018-03-02 14:37:55.637' AS DateTime))
GO
INSERT [dbo].[Status] ([ID], [Name], [Description], [Active], [LastModified]) VALUES (28, N'On Hold', N'On Hold', 1, CAST(N'2018-03-02 14:39:08.107' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Ticket] ON 

GO
INSERT [dbo].[Ticket] ([ID], [UserID], [CategoryID], [PriorityID], [StatusID], [DateCreated], [Deadline], [Subject], [Active], [LastModified], [AssigneeID], [ProductID], [Body], [OrganizationID]) VALUES (1, N'2166a021-c541-4e92-887d-9907ecba65d4', 21, 12, 27, CAST(N'2018-03-05 16:29:58.013' AS DateTime), NULL, N'Error with Skype', 1, CAST(N'2018-03-05 16:29:58.013' AS DateTime), 40, 29, N'Skype not responding', 35)
GO
INSERT [dbo].[Ticket] ([ID], [UserID], [CategoryID], [PriorityID], [StatusID], [DateCreated], [Deadline], [Subject], [Active], [LastModified], [AssigneeID], [ProductID], [Body], [OrganizationID]) VALUES (2, N'2166a021-c541-4e92-887d-9907ecba65d4', 26, 11, 27, CAST(N'2018-03-05 16:30:37.000' AS DateTime), NULL, N'Error with Visual Studio', 1, CAST(N'2018-03-05 16:31:02.550' AS DateTime), 41, 30, N'Visual Studio bug', 36)
GO
INSERT [dbo].[Ticket] ([ID], [UserID], [CategoryID], [PriorityID], [StatusID], [DateCreated], [Deadline], [Subject], [Active], [LastModified], [AssigneeID], [ProductID], [Body], [OrganizationID]) VALUES (3, N'2166a021-c541-4e92-887d-9907ecba65d4', 25, 10, 27, CAST(N'2018-03-05 16:32:14.790' AS DateTime), NULL, N'Error with Visual Studio Code', 1, CAST(N'2018-03-05 16:32:14.790' AS DateTime), 41, 32, N'Visual Studio Code Bug', 35)
GO
INSERT [dbo].[Ticket] ([ID], [UserID], [CategoryID], [PriorityID], [StatusID], [DateCreated], [Deadline], [Subject], [Active], [LastModified], [AssigneeID], [ProductID], [Body], [OrganizationID]) VALUES (4, N'7b6d7292-1ecd-4b37-b34e-5814de60d45d', 21, 12, 27, CAST(N'2018-03-05 16:33:14.000' AS DateTime), NULL, N'Error with Skype', 1, CAST(N'2018-03-05 16:35:33.403' AS DateTime), 40, 29, N'Not able to connect to Skype!', 35)
GO
INSERT [dbo].[Ticket] ([ID], [UserID], [CategoryID], [PriorityID], [StatusID], [DateCreated], [Deadline], [Subject], [Active], [LastModified], [AssigneeID], [ProductID], [Body], [OrganizationID]) VALUES (5, N'7b6d7292-1ecd-4b37-b34e-5814de60d45d', 21, 12, 27, CAST(N'2018-03-05 16:33:46.973' AS DateTime), NULL, N'Visual Studio', 1, CAST(N'2018-03-05 16:33:46.973' AS DateTime), NULL, 30, N'Visual Studio crashing', 35)
GO
SET IDENTITY_INSERT [dbo].[Ticket] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [Email], [Username], [Active], [LastModified], [OrganizationID], [TelephoneNumber], [UserId]) VALUES (37, N'admin', N'admin', N'admin@microsoft.com', N'admin@microsoft.com', 1, CAST(N'2018-03-02 13:40:28.373' AS DateTime), 33, N'01876543', N'2166a021-c541-4e92-887d-9907ecba65d4')
GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [Email], [Username], [Active], [LastModified], [OrganizationID], [TelephoneNumber], [UserId]) VALUES (38, N'Osoba1', N'Osoba1', N'osoba1@tvrtka1.com', N'osoba1@tvrtka1.com', 1, CAST(N'2018-03-02 13:42:02.347' AS DateTime), 35, N'0912435656', N'7b6d7292-1ecd-4b37-b34e-5814de60d45d')
GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [Email], [Username], [Active], [LastModified], [OrganizationID], [TelephoneNumber], [UserId]) VALUES (39, N'Osoba2', N'Osoba2', N'osoba2@tvrtka2.com', N'osoba2@tvrtka2.com', 1, CAST(N'2018-03-02 13:43:24.873' AS DateTime), 36, N'092345678', N'4834d289-44fb-4908-83b1-28f62b6a1eab')
GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [Email], [Username], [Active], [LastModified], [OrganizationID], [TelephoneNumber], [UserId]) VALUES (40, N'Technician1', N'Technician1', N'technician1@microsoft.com', N'technician1@microsoft.com', 1, CAST(N'2018-03-02 13:46:12.753' AS DateTime), 33, N'0912345432', N'37f8bf38-80eb-43c2-9148-e51ba291fc66')
GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [Email], [Username], [Active], [LastModified], [OrganizationID], [TelephoneNumber], [UserId]) VALUES (41, N'Technician2', N'Technician2', N'technician2@microsoft.com', N'technician2@microsoft.com', 1, CAST(N'2018-03-02 13:45:58.590' AS DateTime), 33, N'0923421234', N'6066c9af-7ad9-4b06-921d-6cdb13ae2ada')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AspNetUserRole]    Script Date: 5.3.2018. 16:36:51 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRole] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AspNetUsers]    Script Date: 5.3.2018. 16:36:51 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers] ON [dbo].[AspNetUsers]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 5.3.2018. 16:36:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_User]    Script Date: 5.3.2018. 16:36:51 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [IX_User] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
GO
ALTER TABLE [dbo].[ProductOrganisation]  WITH CHECK ADD  CONSTRAINT [FK_ProductOrganisation_Organisation] FOREIGN KEY([ID_Organisation])
REFERENCES [dbo].[Organization] ([ID])
GO
ALTER TABLE [dbo].[ProductOrganisation] CHECK CONSTRAINT [FK_ProductOrganisation_Organisation]
GO
ALTER TABLE [dbo].[ProductOrganisation]  WITH CHECK ADD  CONSTRAINT [FK_SystemOrganisation_System] FOREIGN KEY([ID_Product])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[ProductOrganisation] CHECK CONSTRAINT [FK_SystemOrganisation_System]
GO
ALTER TABLE [dbo].[Responsible]  WITH CHECK ADD  CONSTRAINT [FK_Responsible_User] FOREIGN KEY([ID_User])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Responsible] CHECK CONSTRAINT [FK_Responsible_User]
GO
ALTER TABLE [dbo].[Responsible]  WITH CHECK ADD  CONSTRAINT [FK_SystemTechnician_System] FOREIGN KEY([ID_Product])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Responsible] CHECK CONSTRAINT [FK_SystemTechnician_System]
GO
ALTER TABLE [dbo].[Ticket]  WITH NOCHECK ADD  CONSTRAINT [FK_Assignee_User] FOREIGN KEY([AssigneeID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Assignee_User]
GO
ALTER TABLE [dbo].[Ticket]  WITH NOCHECK ADD  CONSTRAINT [FK_Ticket_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Category]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Organization] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([ID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Organization]
GO
ALTER TABLE [dbo].[Ticket]  WITH NOCHECK ADD  CONSTRAINT [FK_Ticket_Person] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Person]
GO
ALTER TABLE [dbo].[Ticket]  WITH NOCHECK ADD  CONSTRAINT [FK_Ticket_Priority] FOREIGN KEY([PriorityID])
REFERENCES [dbo].[Priority] ([ID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Priority]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Product]
GO
ALTER TABLE [dbo].[Ticket]  WITH NOCHECK ADD  CONSTRAINT [FK_Ticket_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([ID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Status]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Organization] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Organization]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User]
GO
USE [master]
GO
ALTER DATABASE [BugTracking] SET  READ_WRITE 
GO
