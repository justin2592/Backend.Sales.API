USE [SMS]
GO
/****** Object:  UserDefinedTableType [dbo].[OrderDetailTableType]    Script Date: 8/29/2024 9:32:32 PM ******/
CREATE TYPE [dbo].[OrderDetailTableType] AS TABLE(
	[OrderDetailId] [int] NULL,
	[PizzaId] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[OrderId] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[OrderTableType]    Script Date: 8/29/2024 9:32:32 PM ******/
CREATE TYPE [dbo].[OrderTableType] AS TABLE(
	[OrderId] [int] NULL,
	[Date] [date] NULL,
	[Time] [time](7) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[PizzaTableType]    Script Date: 8/29/2024 9:32:32 PM ******/
CREATE TYPE [dbo].[PizzaTableType] AS TABLE(
	[PizzaId] [nvarchar](50) NULL,
	[PizzaTypeId] [nvarchar](50) NULL,
	[Size] [nvarchar](50) NULL,
	[Price] [decimal](18, 2) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[PizzaTypeTableType]    Script Date: 8/29/2024 9:32:32 PM ******/
CREATE TYPE [dbo].[PizzaTypeTableType] AS TABLE(
	[PizzaTypeId] [nvarchar](50) NULL,
	[Name] [nvarchar](100) NULL,
	[Category] [nvarchar](50) NULL,
	[Ingredients] [nvarchar](max) NULL
)
GO
/****** Object:  Table [dbo].[Order]    Script Date: 8/29/2024 9:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Time] [time](7) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedDateBy] [nvarchar](50) NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
	[LastUpdatedBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 8/29/2024 9:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[PizzaId] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedDateBy] [nvarchar](50) NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
	[LastUpdatedBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pizza]    Script Date: 8/29/2024 9:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pizza](
	[PizzaId] [nvarchar](50) NOT NULL,
	[PizzaTypeId] [nvarchar](50) NOT NULL,
	[Size] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedDateBy] [nvarchar](50) NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
	[LastUpdatedBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PizzaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PizzaType]    Script Date: 8/29/2024 9:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PizzaType](
	[PizzaTypeId] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[Ingredients] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedDateBy] [nvarchar](50) NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
	[LastUpdatedBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PizzaTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([PizzaId])
REFERENCES [dbo].[Pizza] ([PizzaId])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[Pizza]  WITH CHECK ADD FOREIGN KEY([PizzaTypeId])
REFERENCES [dbo].[PizzaType] ([PizzaTypeId])
GO
/****** Object:  StoredProcedure [dbo].[InsertOrderDetails]    Script Date: 8/29/2024 9:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--ALTER PROCEDURE [dbo].[InsertOrderDetails]
--    @OrderDetailTable dbo.OrderDetailTableType READONLY
--AS
--BEGIN
--    SET NOCOUNT ON;

--	SET IDENTITY_INSERT [dbo].[OrderDetail] ON;

--    INSERT INTO OrderDetail (OrderDetailId, PizzaId, Quantity, OrderId)
--    SELECT OrderDetailId, PizzaId, Quantity, OrderId
--    FROM @OrderDetailTable;

--	SET IDENTITY_INSERT [dbo].[OrderDetail] OFF;

--END

CREATE PROCEDURE [dbo].[InsertOrderDetails]
    @OrderDetailTable dbo.OrderDetailTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;
	SET IDENTITY_INSERT [dbo].[OrderDetail] ON;
    -- Use MERGE to either insert or update records
    MERGE INTO [dbo].[OrderDetail] AS target
    USING @OrderDetailTable AS source
    ON target.OrderDetailId = source.OrderDetailId
    WHEN MATCHED THEN
        UPDATE SET
            target.PizzaId = source.PizzaId,
            target.Quantity = source.Quantity,
            target.OrderId = source.OrderId
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (OrderDetailId, PizzaId, Quantity, OrderId)
        VALUES (source.OrderDetailId, source.PizzaId, source.Quantity, source.OrderId);
	
	SET IDENTITY_INSERT [dbo].[OrderDetail] OFF;
    SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertOrders]    Script Date: 8/29/2024 9:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--ALTER PROCEDURE [dbo].[InsertOrders]
--    @OrderTable dbo.OrderTableType READONLY
--AS
--BEGIN
--    SET NOCOUNT ON;
--	SET IDENTITY_INSERT [dbo].[Order] ON;

--    INSERT INTO [Order] (OrderId, Date, Time)
--    SELECT OrderId, Date, Time
--    FROM @OrderTable;

--	SET IDENTITY_INSERT [dbo].[Order] ON;
--END
CREATE PROCEDURE [dbo].[InsertOrders]
    @OrderTable dbo.OrderTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    -- Enable IDENTITY_INSERT to insert explicit values into the identity column
    SET IDENTITY_INSERT [dbo].[Order] ON;

    -- Use MERGE to either insert or update records
    MERGE INTO [dbo].[Order] AS target
    USING @OrderTable AS source
    ON target.OrderId = source.OrderId
    WHEN MATCHED THEN
        UPDATE SET
            target.Date = source.Date,
            target.Time = source.Time
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (OrderId, Date, Time)
        VALUES (source.OrderId, source.Date, source.Time);

    -- Disable IDENTITY_INSERT
    SET IDENTITY_INSERT [dbo].[Order] OFF;

    SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertPizzas]    Script Date: 8/29/2024 9:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--ALTER PROCEDURE [dbo].[InsertPizzas]
--    @PizzaTable dbo.PizzaTableType READONLY
--AS
--BEGIN
--    SET NOCOUNT ON;

--    INSERT INTO Pizza (PizzaId, PizzaTypeId, Size, Price)
--    SELECT PizzaId, PizzaTypeId, Size, Price
--    FROM @PizzaTable;
--END
CREATE PROCEDURE [dbo].[InsertPizzas]
    @PizzaTable dbo.PizzaTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    -- Use MERGE to either insert or update records
    MERGE INTO [dbo].[Pizza] AS target
    USING @PizzaTable AS source
    ON target.PizzaId = source.PizzaId
    WHEN MATCHED THEN
        UPDATE SET
            target.PizzaTypeId = source.PizzaTypeId,
            target.Size = source.Size,
            target.Price = source.Price
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (PizzaId, PizzaTypeId, Size, Price)
        VALUES (source.PizzaId, source.PizzaTypeId, source.Size, source.Price);

    SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertPizzaTypes]    Script Date: 8/29/2024 9:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertPizzaTypes]
    @PizzaTypeTable dbo.PizzaTypeTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    -- Use MERGE to either insert or update records
    MERGE INTO [dbo].[PizzaType] AS target
    USING @PizzaTypeTable AS source
    ON target.PizzaTypeId = source.PizzaTypeId
    WHEN MATCHED THEN
        UPDATE SET
            target.Name = source.Name,
            target.Category = source.Category,
            target.Ingredients = source.Ingredients
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (PizzaTypeId, Name, Category, Ingredients)
        VALUES (source.PizzaTypeId, source.Name, source.Category, source.Ingredients);

    SET NOCOUNT OFF;
END
GO
