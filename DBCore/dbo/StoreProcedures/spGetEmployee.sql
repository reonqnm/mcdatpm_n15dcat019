CREATE PROCEDURE [dbo].[spGetEmployee]
@Search NVARCHAR(150)
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		IF(@Search <> '')
		BEGIN
			SELECT *
			FROM [dbo].[Employee] e
			WHERE  e.[FullName] LIKE '%'+@Search+'%'
		END
		ELSE
		BEGIN
			SELECT *
			FROM [Employee]
		END
		
	END TRY
	BEGIN CATCH
		THROW;
	END  CATCH
END