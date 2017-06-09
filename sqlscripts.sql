USE [hair_salon]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 6/9/17 9:32:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_name] [varchar](100) NULL,
	[details] [varchar](255) NULL,
	[stylist_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stylists]    Script Date: 6/9/17 9:32:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stylists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[stylist_name] [varchar](100) NULL,
	[specialty] [varchar](255) NULL
) ON [PRIMARY]
GO
