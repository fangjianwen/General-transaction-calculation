USE [FruitCalc]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 2024/4/9 8:27:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderName] [nvarchar](50) NOT NULL,
	[SinglePackAgeWeight] [decimal](18, 2) NOT NULL,
	[PackAgeCount] [int] NOT NULL,
	[TotalPackAgeWeight] [decimal](18, 2) NOT NULL,
	[MaoWeight] [decimal](18, 2) NOT NULL,
	[RealWeight] [decimal](18, 2) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[AddTime] [datetime] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
