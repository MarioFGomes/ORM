USE [master]
GO
/****** Object:  Database [TresCamadas]    Script Date: 27/07/2021 17:29:57 ******/
CREATE DATABASE [TresCamadas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TresCamadas', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\TresCamadas.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TresCamadas_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\TresCamadas_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TresCamadas] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TresCamadas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TresCamadas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TresCamadas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TresCamadas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TresCamadas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TresCamadas] SET ARITHABORT OFF 
GO
ALTER DATABASE [TresCamadas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TresCamadas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TresCamadas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TresCamadas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TresCamadas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TresCamadas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TresCamadas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TresCamadas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TresCamadas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TresCamadas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TresCamadas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TresCamadas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TresCamadas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TresCamadas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TresCamadas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TresCamadas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TresCamadas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TresCamadas] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TresCamadas] SET  MULTI_USER 
GO
ALTER DATABASE [TresCamadas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TresCamadas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TresCamadas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TresCamadas] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [TresCamadas]
GO
/****** Object:  UserDefinedFunction [dbo].[splitstring]    Script Date: 27/07/2021 17:29:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[splitstring] ( @stringToSplit VARCHAR(MAX) )
RETURNS
 @returnList TABLE ([Name] [nvarchar] (500),[Morada] [nvarchar] (500))

AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @cliente NVARCHAR(255)
 DECLARE @morada NVARCHAR(255)
 DECLARE @pos INT

 --WHILE CHARINDEX(';', @stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX(';', @stringToSplit) 
  SELECT @name = RTRIM(LTRIM(SUBSTRING(@stringToSplit, 0, @pos)))

  --INSERT INTO @returnList  '' 
  --SELECT @name WHERE len(@name)>1
  --  INSERT INTO @returnList   
  --SELECT @stringToSplit,@name
  
  SELECT @stringToSplit = RTRIM(LTRIM(Replace(SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos),';',' ')))
  
  INSERT INTO @returnList
SELECT @name,@stringToSplit

 END

 --INSERT INTO @returnList
 --select @cliente,@morada
 --SELECT @stringToSplit WHERE len(@stringToSplit)>1

 RETURN
END
GO
/****** Object:  Table [dbo].[Enderecos]    Script Date: 27/07/2021 17:29:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enderecos](
	[IDEndereco] [int] IDENTITY(1,1) NOT NULL,
	[Morada] [varchar](50) NOT NULL,
	[Localidade] [varchar](50) NOT NULL,
	[BI] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Enderecos] PRIMARY KEY CLUSTERED 
(
	[IDEndereco] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fornecedores]    Script Date: 27/07/2021 17:29:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fornecedores](
	[IDFornecedor] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Telefone] [varchar](50) NULL,
	[Produto] [varchar](50) NOT NULL,
	[NIF] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Fornecedores] PRIMARY KEY CLUSTERED 
(
	[IDFornecedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 27/07/2021 17:29:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[BI] [varchar](50) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Telefone] [varchar](50) NULL,
 CONSTRAINT [PK_Usuarios_1] PRIMARY KEY CLUSTERED 
(
	[BI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Enderecos] ON 

INSERT [dbo].[Enderecos] ([IDEndereco], [Morada], [Localidade], [BI]) VALUES (1, N'Tala-Hady casaNº23', N'Cazenga', N'A0044234LA00567')
INSERT [dbo].[Enderecos] ([IDEndereco], [Morada], [Localidade], [BI]) VALUES (2, N'Vila de Viana Rua Salvador', N'Viana', N'A0044324567890')
INSERT [dbo].[Enderecos] ([IDEndereco], [Morada], [Localidade], [BI]) VALUES (3, N'Rua do Parana', N'Rangel', N'A004445LB6789')
SET IDENTITY_INSERT [dbo].[Enderecos] OFF
SET IDENTITY_INSERT [dbo].[Fornecedores] ON 

INSERT [dbo].[Fornecedores] ([IDFornecedor], [Nome], [Telefone], [Produto], [NIF]) VALUES (1, N'Ecount', N'99876543', N'Serviços de Contabilidade', N'A0078543221678')
INSERT [dbo].[Fornecedores] ([IDFornecedor], [Nome], [Telefone], [Produto], [NIF]) VALUES (2, N'Wakulo', N'92345678', N'Prestação de Serviço', N'A0032456782895')
INSERT [dbo].[Fornecedores] ([IDFornecedor], [Nome], [Telefone], [Produto], [NIF]) VALUES (3, N'Fluxo', N'95837324', N'Computadores', N'A0033456789328')
SET IDENTITY_INSERT [dbo].[Fornecedores] OFF
INSERT [dbo].[Usuarios] ([BI], [Nome], [Telefone]) VALUES (N'A0044234LA00567', N'Mário Gomes', N'932041319')
INSERT [dbo].[Usuarios] ([BI], [Nome], [Telefone]) VALUES (N'A0044324567890', N'Benilson João', N'992823456')
INSERT [dbo].[Usuarios] ([BI], [Nome], [Telefone]) VALUES (N'A004445LB6789', N'Dario Ferreira', N'949822470')
ALTER TABLE [dbo].[Enderecos]  WITH CHECK ADD  CONSTRAINT [FK_Enderecos_Usuarios] FOREIGN KEY([BI])
REFERENCES [dbo].[Usuarios] ([BI])
GO
ALTER TABLE [dbo].[Enderecos] CHECK CONSTRAINT [FK_Enderecos_Usuarios]
GO
USE [master]
GO
ALTER DATABASE [TresCamadas] SET  READ_WRITE 
GO
