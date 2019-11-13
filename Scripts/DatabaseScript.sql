/****** Object:  Database [BbDb]    Script Date: 11/13/2019 12:17:49 PM ******/
CREATE DATABASE [BbDb]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 11/13/2019 12:19:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[Id] [bigint] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Quantity] [int] NOT NULL,
	[SaleAmount] [decimal](11, 2) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
