CREATE PROCEDURE [dbo].[spLogin]
@Email VARCHAR(150),
@Password VARCHAR(150)
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[Employee]
		WHERE  [Email] = @Email AND [Password] = @Password
	END TRY
	BEGIN CATCH
		THROW;
	END  CATCH
END

