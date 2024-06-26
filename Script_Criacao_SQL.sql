USE [master]
GO
CREATE DATABASE [dbRadar]
USE [dbRadar]
GO
CREATE TABLE [dbo].[RadarData](
	[concessionaria] [varchar](50) NULL,
	[ano_do_pnv_snv] [int] NULL,
	[tipo_de_radar] [varchar](50) NULL,
	[rodovia] [varchar](50) NULL,
	[uf] [varchar](50) NULL,
	[km_m] [varchar](50) NULL,
	[municipio] [varchar](50) NULL,
	[tipo_pista] [varchar](50) NULL,
	[sentido] [varchar](50) NULL,
	[situacao] [varchar](50) NULL,
	[data_da_inativacao] [varchar](50) NULL,
	[latitude] [varchar](20) NULL,
	[longitude] [varchar](20) NULL,
	[velocidade_leve] [int] NULL
) ON [PRIMARY]
GO