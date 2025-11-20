USE [master]
GO
/****** Object:  Database [BDA360]    Script Date: 11/20/2025 3:47:30 PM ******/
CREATE DATABASE [BDA360]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BDA360', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BDA360.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BDA360_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BDA360_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BDA360] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BDA360].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BDA360] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BDA360] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BDA360] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BDA360] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BDA360] SET ARITHABORT OFF 
GO
ALTER DATABASE [BDA360] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BDA360] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BDA360] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BDA360] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BDA360] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BDA360] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BDA360] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BDA360] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BDA360] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BDA360] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BDA360] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BDA360] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BDA360] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BDA360] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BDA360] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BDA360] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BDA360] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BDA360] SET RECOVERY FULL 
GO
ALTER DATABASE [BDA360] SET  MULTI_USER 
GO
ALTER DATABASE [BDA360] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BDA360] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BDA360] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BDA360] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BDA360] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BDA360] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BDA360', N'ON'
GO
ALTER DATABASE [BDA360] SET QUERY_STORE = ON
GO
ALTER DATABASE [BDA360] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BDA360]
GO
/****** Object:  Table [dbo].[area]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[area](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[area] [varchar](255) NOT NULL,
 CONSTRAINT [PK_area] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[responsable]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[responsable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idarea] [int] NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[correo] [varchar](255) NOT NULL,
 CONSTRAINT [PK_responsable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estado]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estado](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[estado] [varchar](50) NOT NULL,
 CONSTRAINT [PK_estado] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[auditoria]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[auditoria](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idarea] [int] NOT NULL,
	[titulo] [varchar](255) NOT NULL,
	[fechainicio] [date] NOT NULL,
	[fechafin] [date] NULL,
	[idresponsable] [int] NOT NULL,
	[idestado] [int] NOT NULL,
 CONSTRAINT [PK_auditoria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[viewauditoria]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewauditoria]
AS
SELECT dbo.auditoria.id, dbo.auditoria.titulo, dbo.responsable.id AS idresponsable, dbo.responsable.nombre AS responsable, dbo.area.id AS idarea, dbo.area.area, dbo.auditoria.fechainicio, dbo.auditoria.fechafin, dbo.estado.id AS idestado, 
                  dbo.estado.estado
FROM     dbo.area INNER JOIN
                  dbo.auditoria ON dbo.area.id = dbo.auditoria.idarea INNER JOIN
                  dbo.estado ON dbo.auditoria.idestado = dbo.estado.id INNER JOIN
                  dbo.responsable ON dbo.auditoria.idresponsable = dbo.responsable.id
GO
/****** Object:  Table [dbo].[tipo]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tipo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[severidad]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[severidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[severidad] [varchar](50) NOT NULL,
 CONSTRAINT [PK_severidad] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hallazgo]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hallazgo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](255) NOT NULL,
	[idauditoria] [int] NOT NULL,
	[idtipo] [int] NOT NULL,
	[idseveridad] [int] NOT NULL,
	[fecha] [date] NOT NULL,
 CONSTRAINT [PK_hallazgo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[viewhallazgo]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewhallazgo]
AS
SELECT dbo.hallazgo.id, dbo.hallazgo.descripcion, dbo.auditoria.id AS idauditoria, dbo.auditoria.titulo, dbo.tipo.id AS idtipo, dbo.tipo.tipo, dbo.severidad.id AS idseveridad, dbo.severidad.severidad, dbo.hallazgo.fecha, dbo.estado.estado
FROM     dbo.auditoria INNER JOIN
                  dbo.hallazgo ON dbo.auditoria.id = dbo.hallazgo.idauditoria INNER JOIN
                  dbo.severidad ON dbo.hallazgo.idseveridad = dbo.severidad.id INNER JOIN
                  dbo.tipo ON dbo.hallazgo.idtipo = dbo.tipo.id INNER JOIN
                  dbo.estado ON dbo.auditoria.idestado = dbo.estado.id
GO
ALTER TABLE [dbo].[auditoria]  WITH CHECK ADD  CONSTRAINT [FK_auditoria_area] FOREIGN KEY([idarea])
REFERENCES [dbo].[area] ([id])
GO
ALTER TABLE [dbo].[auditoria] CHECK CONSTRAINT [FK_auditoria_area]
GO
ALTER TABLE [dbo].[auditoria]  WITH CHECK ADD  CONSTRAINT [FK_auditoria_estado] FOREIGN KEY([idestado])
REFERENCES [dbo].[estado] ([id])
GO
ALTER TABLE [dbo].[auditoria] CHECK CONSTRAINT [FK_auditoria_estado]
GO
ALTER TABLE [dbo].[auditoria]  WITH CHECK ADD  CONSTRAINT [FK_auditoria_responsable] FOREIGN KEY([idresponsable])
REFERENCES [dbo].[responsable] ([id])
GO
ALTER TABLE [dbo].[auditoria] CHECK CONSTRAINT [FK_auditoria_responsable]
GO
ALTER TABLE [dbo].[hallazgo]  WITH CHECK ADD  CONSTRAINT [FK_hallazgo_auditoria] FOREIGN KEY([idauditoria])
REFERENCES [dbo].[auditoria] ([id])
GO
ALTER TABLE [dbo].[hallazgo] CHECK CONSTRAINT [FK_hallazgo_auditoria]
GO
ALTER TABLE [dbo].[hallazgo]  WITH CHECK ADD  CONSTRAINT [FK_hallazgo_severidad] FOREIGN KEY([idseveridad])
REFERENCES [dbo].[severidad] ([id])
GO
ALTER TABLE [dbo].[hallazgo] CHECK CONSTRAINT [FK_hallazgo_severidad]
GO
ALTER TABLE [dbo].[hallazgo]  WITH CHECK ADD  CONSTRAINT [FK_hallazgo_tipo] FOREIGN KEY([idtipo])
REFERENCES [dbo].[tipo] ([id])
GO
ALTER TABLE [dbo].[hallazgo] CHECK CONSTRAINT [FK_hallazgo_tipo]
GO
ALTER TABLE [dbo].[responsable]  WITH CHECK ADD  CONSTRAINT [FK_responsable_area] FOREIGN KEY([idarea])
REFERENCES [dbo].[area] ([id])
GO
ALTER TABLE [dbo].[responsable] CHECK CONSTRAINT [FK_responsable_area]
GO
/****** Object:  StoredProcedure [dbo].[SPDeleteHallazgo]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Balda>
-- Create date: <Create Date,,>
-- Description:	<Eliminar Hallazgo>
-- =============================================
CREATE PROCEDURE [dbo].[SPDeleteHallazgo]
    @id INT
AS
BEGIN
    DELETE hallazgo WHERE id = @id;
END;

GO
/****** Object:  StoredProcedure [dbo].[SPInsertAuditoria]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Balda>
-- Create date: <Create Date,,>
-- Description:	<Insertar Auditoria>
-- =============================================
CREATE PROCEDURE [dbo].[SPInsertAuditoria]
    @idarea INT,
    @titulo NVARCHAR(255),
    @fechainicio DATE,
    @fechafin DATE,
    @idresponsable INT,
    @idestado INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO auditoria (idarea, titulo, fechainicio, fechafin, idresponsable, idestado)
    VALUES (@idarea, @titulo, @fechainicio, @fechafin, @idresponsable, @idestado);
END;

GO
/****** Object:  StoredProcedure [dbo].[SPInsertReponsable]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Balda>
-- Create date: <Create Date,,>
-- Description:	<Insertar Responsable>
-- =============================================
CREATE PROCEDURE [dbo].[SPInsertReponsable]
    @idarea INT,
    @nombre NVARCHAR(100),
    @correo NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO responsable (idarea, nombre, correo)
    VALUES (@idarea, @nombre, @correo);
END;

GO
/****** Object:  StoredProcedure [dbo].[SPListArea]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Balda>
-- Create date: <Create Date,,>
-- Description:	<Listar Area>
-- =============================================
CREATE PROCEDURE [dbo].[SPListArea] 
	
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM area
END
GO
/****** Object:  StoredProcedure [dbo].[SPListEstado]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Balda>
-- Create date: <Create Date,,>
-- Description:	<Listar Estado>
-- =============================================
CREATE PROCEDURE [dbo].[SPListEstado] 
	
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM estado
END
GO
/****** Object:  StoredProcedure [dbo].[SPListResponsable]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Balda>
-- Create date: <Create Date,,>
-- Description:	<Listar Responsables>
-- =============================================
CREATE PROCEDURE [dbo].[SPListResponsable] 
	
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id,nombre FROM responsable
END
GO
/****** Object:  StoredProcedure [dbo].[SPListSeveridad]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Balda>
-- Create date: <Create Date,,>
-- Description:	<Listar Severidad>
-- =============================================
CREATE PROCEDURE [dbo].[SPListSeveridad] 
	
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM severidad
END
GO
/****** Object:  StoredProcedure [dbo].[SPListTipo]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Balda>
-- Create date: <Create Date,,>
-- Description:	<Listar Tipo>
-- =============================================
CREATE PROCEDURE [dbo].[SPListTipo] 
	
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tipo
END
GO
/****** Object:  StoredProcedure [dbo].[SPUpdateAuditoria]    Script Date: 11/20/2025 3:47:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Balda>
-- Create date: <Create Date,,>
-- Description:	<Actualizar Auditoria>
-- =============================================
CREATE PROCEDURE [dbo].[SPUpdateAuditoria]
    @id INT,
    @idarea INT,
    @titulo NVARCHAR(255),
    @fechainicio DATE,
    @fechafin DATE,
    @idresponsable INT,
    @idestado INT
AS
BEGIN
    UPDATE auditoria SET idarea = @idarea, titulo = @titulo, fechainicio = @fechainicio, 
    fechafin = @fechafin, idresponsable = @idresponsable, idestado = @idestado
    WHERE id = @id;
END;

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[3] 2[16] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "area"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 126
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "auditoria"
            Begin Extent = 
               Top = 2
               Left = 496
               Bottom = 165
               Right = 690
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "estado"
            Begin Extent = 
               Top = 146
               Left = 756
               Bottom = 265
               Right = 950
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "responsable"
            Begin Extent = 
               Top = 140
               Left = 248
               Bottom = 303
               Right = 442
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewauditoria'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewauditoria'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewauditoria'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "auditoria"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "hallazgo"
            Begin Extent = 
               Top = 59
               Left = 318
               Bottom = 222
               Right = 512
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "severidad"
            Begin Extent = 
               Top = 188
               Left = 701
               Bottom = 307
               Right = 895
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tipo"
            Begin Extent = 
               Top = 15
               Left = 682
               Bottom = 134
               Right = 876
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "estado"
            Begin Extent = 
               Top = 190
               Left = 134
               Bottom = 309
               Right = 328
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
      ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewhallazgo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'   Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewhallazgo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewhallazgo'
GO
USE [master]
GO
ALTER DATABASE [BDA360] SET  READ_WRITE 
GO
