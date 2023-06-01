USE [Indus_fyp]
GO
/****** Object:  Table [dbo].[SignUp_Customer]    Script Date: 01/06/2023 6:14:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SignUp_Customer](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[EmailVerification] [int] NULL,
	[ProfileImage] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmailVerification]    Script Date: 01/06/2023 6:14:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_EmailVerification]
    
	@email NVARCHAR(MAX)

AS
BEGIN
    
	UPDATE [SignUp_Customer] SET EmailVerification = 1 

	WHERE Email =  @email  
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ForLoginWithEmailAndPassword]    Script Date: 01/06/2023 6:14:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_ForLoginWithEmailAndPassword]
    @email NVARCHAR(MAX),
    @password NVARCHAR(MAX)
AS
BEGIN
    
	SELECT TOP 1 userID, Username, Email, Password, ProfileImage FROM 
	[dbo].[SignUp_Customer]

	WHERE Email =  @email  AND Password = @password 
	AND EmailVerification = 1
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetDataOnEmail]    Script Date: 01/06/2023 6:14:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_GetDataOnEmail]
    @email NVARCHAR(MAX)
AS
BEGIN
    
	SELECT TOP 1 userID, Username, Email, Password, ProfileImage FROM 
	[dbo].[SignUp_Customer]

	WHERE Email =  @email 
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertSignUpCustomer]    Script Date: 01/06/2023 6:14:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_InsertSignUpCustomer]
    @Username NVARCHAR(MAX),
    @Email NVARCHAR(MAX),
    @Password NVARCHAR(MAX),
	@ProfileImage nvarchar(max)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO SignUp_Customer (Username, Email, Password ,ProfileImage , EmailVerification )
    VALUES (@Username, @Email, @Password ,@ProfileImage , 0);
    
    SELECT SCOPE_IDENTITY() AS UserID;
END
GO
