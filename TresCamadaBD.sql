USE [TresCamadas]
GO
/****** Object:  UserDefinedFunction [dbo].[splitstring]    Script Date: 30/07/2021 17:28:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[splitstring] ( @stringToSplit VARCHAR(MAX) )
RETURNS
 @returnList TABLE ([Name] [nvarchar] (500),[Morada] [nvarchar] (500))

AS
BEGIN

set @stringToSplit=RTRIM(@stringToSplit);
set @stringToSplit=Replace(@stringToSplit,'  ',';')


 DECLARE @name NVARCHAR(255)
 DECLARE @cliente NVARCHAR(255)
 DECLARE @morada NVARCHAR(255)
 DECLARE @pos INT

 --WHILE CHARINDEX(';', @stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX(';', @stringToSplit) 
  SELECT @name = RTRIM(LTRIM(SUBSTRING(@stringToSplit, 0, @pos)))


  
  SELECT @stringToSplit = RTRIM(LTRIM(Replace(SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos),';',' ')))
  
  INSERT INTO @returnList
SELECT @name,@stringToSplit

 END

 RETURN
END
GO
/****** Object:  Table [dbo].[Enderecos]    Script Date: 30/07/2021 17:28:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enderecos](
	[IDEndereco] [int] IDENTITY(1,1) NOT NULL,
	[Morada] [varchar](50) NOT NULL,
	[Localidade] [varchar](50) NOT NULL,
	[IDUsuarios] [int] NULL,
 CONSTRAINT [PK_Enderecos] PRIMARY KEY CLUSTERED 
(
	[IDEndereco] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fornecedores]    Script Date: 30/07/2021 17:28:05 ******/
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
/****** Object:  Table [dbo].[Usuarios]    Script Date: 30/07/2021 17:28:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IDUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[BI] [varchar](50) NOT NULL,
	[Telefone] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IDUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Enderecos]  WITH CHECK ADD  CONSTRAINT [FK_Enderecos_Usuarios] FOREIGN KEY([IDUsuarios])
REFERENCES [dbo].[Usuarios] ([IDUsuario])
GO
ALTER TABLE [dbo].[Enderecos] CHECK CONSTRAINT [FK_Enderecos_Usuarios]
GO
