USE [Hoteles]
GO

/****** Object:  Table [dbo].[CantidadMaximaPersonas]    Script Date: 8/05/2024 11:26:37 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CantidadMaximaPersonas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSede] [int] NULL,
	[Cantidad] [int] NULL,
 CONSTRAINT [PK_CantidadMaximaPersonas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Sede](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[sede] [varchar](50) NOT NULL,
	[IdTipoAlojamineto] [int] NOT NULL,
	[CantidadHabitaciones] [int] NULL,
	[CantidadPersonas] [int] NULL,
 CONSTRAINT [PK_Sede] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Sede]  WITH CHECK ADD  CONSTRAINT [FK_Sede_TpoAlojamiento] FOREIGN KEY([IdTipoAlojamineto])
REFERENCES [dbo].[TpoAlojamiento] ([Id])
GO

ALTER TABLE [dbo].[Sede] CHECK CONSTRAINT [FK_Sede_TpoAlojamiento]
GO


CREATE TABLE [dbo].[Solicitudes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSede] [int] NULL,
	[IdAlojamiento] [int] NULL,
	[IdTemporada] [int] NULL,
	[CantidadHabitaciones] [int] NULL,
	[CantidadPersonas] [int] NULL,
 CONSTRAINT [PK_Solicitudes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD  CONSTRAINT [FK_Solicitudes_Sede] FOREIGN KEY([IdSede])
REFERENCES [dbo].[Sede] ([Id])
GO

ALTER TABLE [dbo].[Solicitudes] CHECK CONSTRAINT [FK_Solicitudes_Sede]
GO

ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD  CONSTRAINT [FK_Solicitudes_Temporada] FOREIGN KEY([IdTemporada])
REFERENCES [dbo].[Temporada] ([Id])
GO

ALTER TABLE [dbo].[Solicitudes] CHECK CONSTRAINT [FK_Solicitudes_Temporada]
GO

CREATE TABLE [dbo].[Temporada](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[temporada] [varchar](50) NULL,
 CONSTRAINT [PK_Temporada] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[TpoAlojamiento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Alojamiento] [nchar](10) NULL,
 CONSTRAINT [PK_TpoAlojamiento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE PROCEDURE [dbo].[ListarSolicitudes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT TOP (1000) s.[Id]
      ,se.sede
      ,t.temporada
	  ,tp.Alojamiento
      ,s.[CantidadHabitaciones]
	  ,s.CantidadPersonas
  FROM [Hoteles].[dbo].[Solicitudes] S
  left join Sede se on se.Id = s.IdSede
  left join TpoAlojamiento tp on tp.Id = s.IdAlojamiento
  left join Temporada t on t.Id = s.IdTemporada

END
GO
