USE [master]
GO
/****** Object:  Database [Abab]    Script Date: 19/11/2022 10:45:13 م ******/
CREATE DATABASE [Abab]

GO
CREATE TABLE [dbo].[detials](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idpro] [int] NULL,
	[idinv] [int] NULL,
	[rate] [float] NULL,
	[qtyinv] [int] NULL,
	[amount] [float] NULL,
 CONSTRAINT [PK_detials] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 19/11/2022 10:45:13 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[id] [int] NOT NULL,
	[total] [float] NOT NULL,
	[customerName] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[dateInv] [datetime] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 19/11/2022 10:45:13 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[price] [float] NULL,
	[qty] [int] NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[detials]  WITH CHECK ADD  CONSTRAINT [FK_detials_Invoice] FOREIGN KEY([idinv])
REFERENCES [dbo].[Invoice] ([id])
GO
ALTER TABLE [dbo].[detials] CHECK CONSTRAINT [FK_detials_Invoice]
GO
ALTER TABLE [dbo].[detials]  WITH CHECK ADD  CONSTRAINT [FK_detials_products] FOREIGN KEY([idpro])
REFERENCES [dbo].[products] ([ID])
GO
ALTER TABLE [dbo].[detials] CHECK CONSTRAINT [FK_detials_products]
GO
USE [master]
GO
ALTER DATABASE [Abab] SET  READ_WRITE 
GO
