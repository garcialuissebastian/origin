USE [ATM]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarIntentos]    Script Date: 10/06/2021 7:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure  [dbo].[ActualizarIntentos]
@nro varchar(150) ,
@tipo INT, -- 1 (INGRESO), 0 (NO INGRESO)
 @estado INT OUTPUT 
as 
begin
declare
@v_intentos int  

set @estado = 0; -- no bloqueado
set @v_intentos = 0;
if ( @tipo = 0)
begin
  
 
 SELECT  @v_intentos = (Intentos + 1)  from Tarjetas where   Nro= @nro;

 update Tarjetas  set Intentos= @v_intentos where   Nro= @nro;

 if @v_intentos > 3  -- cantidad de intentos para bloquearlo
 begin 
 update Tarjetas  set Bloqueo='S' where   Nro= @nro;
 set @estado =	1; -- bloqueado
 end;
 
 end;

 if ( @tipo = 1)
begin   
 update Tarjetas  set Intentos=0 where   Nro= @nro; 
 end;

END

GO
/****** Object:  StoredProcedure [dbo].[Retiro]    Script Date: 10/06/2021 7:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery18.sql|7|0|C:\Users\user\AppData\Local\Temp\~vsCD21.sql
CREATE  procedure  [dbo].[Retiro]
@Id_Tarjeta INT ,
@Monto numeric(15,2) ,  
 @estado INT OUTPUT 
as 
begin
declare
@v_monto numeric(15,2)  

set @estado = 0; -- sin fondos
set @v_monto = 0;
 
begin
   
 SELECT @v_monto = Balance  from Tarjetas where Id_Tarjeta = @Id_Tarjeta; 

 if  @v_monto >=  @Monto
 begin 

 set @v_monto  = @v_monto - @Monto  ;
 update Tarjetas  set Balance= @v_monto where  Id_Tarjeta = @Id_Tarjeta; 
 set @estado =	1; -- Actulizado
 insert into Operaciones (Id_Tarjeta,Fecha_Op,Cod_Op,Monto) 
 values (@Id_Tarjeta,GETDATE(),2,@Monto); -- registro el retiro
 end;
 
 end;

 

END

GO
/****** Object:  Table [dbo].[Operaciones]    Script Date: 10/06/2021 7:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operaciones](
	[Id_Operacion] [int] IDENTITY(1,1) NOT NULL,
	[Id_Tarjeta] [int] NULL,
	[Fecha_Op] [datetime] NULL,
	[Cod_Op] [int] NULL,
	[Monto] [numeric](15, 2) NULL,
 CONSTRAINT [PK_Operaciones] PRIMARY KEY CLUSTERED 
(
	[Id_Operacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tarjetas]    Script Date: 10/06/2021 7:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tarjetas](
	[Id_Tarjeta] [int] IDENTITY(1,1) NOT NULL,
	[Nro] [numeric](16, 0) NULL,
	[Fecha_Vto] [date] NULL,
	[Balance] [numeric](15, 2) NULL,
	[Bloqueo] [char](1) NULL,
	[Intentos] [int] NULL,
	[Aud] [datetime] NULL,
	[Id_Cliente] [int] NULL,
	[Pin] [varchar](200) NULL,
 CONSTRAINT [PK_Tarjetas] PRIMARY KEY CLUSTERED 
(
	[Id_Tarjeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tipo_Operacion]    Script Date: 10/06/2021 7:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tipo_Operacion](
	[Id_op] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](150) NULL,
	[Aud] [datetime] NULL,
 CONSTRAINT [PK_Tipo_Operacion] PRIMARY KEY CLUSTERED 
(
	[Id_op] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Operaciones] ON 

INSERT [dbo].[Operaciones] ([Id_Operacion], [Id_Tarjeta], [Fecha_Op], [Cod_Op], [Monto]) VALUES (25, 4, CAST(0x0000AD4300710E32 AS DateTime), 2, CAST(0.10 AS Numeric(15, 2)))
INSERT [dbo].[Operaciones] ([Id_Operacion], [Id_Tarjeta], [Fecha_Op], [Cod_Op], [Monto]) VALUES (26, 4, CAST(0x0000AD430071764B AS DateTime), 2, CAST(0.10 AS Numeric(15, 2)))
INSERT [dbo].[Operaciones] ([Id_Operacion], [Id_Tarjeta], [Fecha_Op], [Cod_Op], [Monto]) VALUES (27, 4, CAST(0x0000AD4300720D8F AS DateTime), 1, CAST(0.00 AS Numeric(15, 2)))
INSERT [dbo].[Operaciones] ([Id_Operacion], [Id_Tarjeta], [Fecha_Op], [Cod_Op], [Monto]) VALUES (28, 4, CAST(0x0000AD430072A45F AS DateTime), 1, CAST(0.00 AS Numeric(15, 2)))
INSERT [dbo].[Operaciones] ([Id_Operacion], [Id_Tarjeta], [Fecha_Op], [Cod_Op], [Monto]) VALUES (29, 4, CAST(0x0000AD430072D241 AS DateTime), 2, CAST(1.00 AS Numeric(15, 2)))
INSERT [dbo].[Operaciones] ([Id_Operacion], [Id_Tarjeta], [Fecha_Op], [Cod_Op], [Monto]) VALUES (30, 5, CAST(0x0000AD430077FA20 AS DateTime), 2, CAST(12.00 AS Numeric(15, 2)))
SET IDENTITY_INSERT [dbo].[Operaciones] OFF
SET IDENTITY_INSERT [dbo].[Tarjetas] ON 

INSERT [dbo].[Tarjetas] ([Id_Tarjeta], [Nro], [Fecha_Vto], [Balance], [Bloqueo], [Intentos], [Aud], [Id_Cliente], [Pin]) VALUES (3, CAST(1234567891234567 AS Numeric(16, 0)), CAST(0x9C420B00 AS Date), CAST(2121.34 AS Numeric(15, 2)), N'S', 0, NULL, NULL, N'81DC9BDB52D04DC20036DBD8313ED055')
INSERT [dbo].[Tarjetas] ([Id_Tarjeta], [Nro], [Fecha_Vto], [Balance], [Bloqueo], [Intentos], [Aud], [Id_Cliente], [Pin]) VALUES (4, CAST(1111111111111111 AS Numeric(16, 0)), CAST(0x9C420B00 AS Date), CAST(907.50 AS Numeric(15, 2)), N'N', 0, NULL, NULL, N'81DC9BDB52D04DC20036DBD8313ED055')
INSERT [dbo].[Tarjetas] ([Id_Tarjeta], [Nro], [Fecha_Vto], [Balance], [Bloqueo], [Intentos], [Aud], [Id_Cliente], [Pin]) VALUES (5, CAST(1111111111111112 AS Numeric(16, 0)), CAST(0x7A420B00 AS Date), CAST(4988.00 AS Numeric(15, 2)), N'N', 0, CAST(0x0000AD430077675E AS DateTime), NULL, N'81DC9BDB52D04DC20036DBD8313ED055')
SET IDENTITY_INSERT [dbo].[Tarjetas] OFF
SET IDENTITY_INSERT [dbo].[Tipo_Operacion] ON 

INSERT [dbo].[Tipo_Operacion] ([Id_op], [Descripcion], [Aud]) VALUES (1, N'BALANCE', CAST(0x0000AD4100735B40 AS DateTime))
INSERT [dbo].[Tipo_Operacion] ([Id_op], [Descripcion], [Aud]) VALUES (2, N'RETIRO', CAST(0x0000AD4100A4CB80 AS DateTime))
SET IDENTITY_INSERT [dbo].[Tipo_Operacion] OFF
ALTER TABLE [dbo].[Operaciones] ADD  CONSTRAINT [DF_Operaciones_Fecha_Op]  DEFAULT (getdate()) FOR [Fecha_Op]
GO
ALTER TABLE [dbo].[Operaciones] ADD  CONSTRAINT [DF_Operaciones_Monto]  DEFAULT ((0)) FOR [Monto]
GO
ALTER TABLE [dbo].[Tarjetas] ADD  CONSTRAINT [DF_Tarjetas_Bloqueo]  DEFAULT ('N') FOR [Bloqueo]
GO
ALTER TABLE [dbo].[Tarjetas] ADD  CONSTRAINT [DF_Tarjetas_Intentos]  DEFAULT ((0)) FOR [Intentos]
GO
ALTER TABLE [dbo].[Tarjetas] ADD  CONSTRAINT [DF_Tarjetas_Aud]  DEFAULT (getdate()) FOR [Aud]
GO
ALTER TABLE [dbo].[Tipo_Operacion] ADD  CONSTRAINT [DF_Tipo_Operacion_Aud]  DEFAULT (getdate()) FOR [Aud]
GO
ALTER TABLE [dbo].[Operaciones]  WITH CHECK ADD  CONSTRAINT [FK_Operaciones_Tipo_Operacion] FOREIGN KEY([Cod_Op])
REFERENCES [dbo].[Tipo_Operacion] ([Id_op])
GO
ALTER TABLE [dbo].[Operaciones] CHECK CONSTRAINT [FK_Operaciones_Tipo_Operacion]
GO
ALTER TABLE [dbo].[Operaciones]  WITH CHECK ADD  CONSTRAINT [FK_Tarjetas] FOREIGN KEY([Id_Tarjeta])
REFERENCES [dbo].[Tarjetas] ([Id_Tarjeta])
GO
ALTER TABLE [dbo].[Operaciones] CHECK CONSTRAINT [FK_Tarjetas]
GO
