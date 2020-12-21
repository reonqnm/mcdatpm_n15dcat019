CREATE PROCEDURE [dbo].[spCreateEmployee]
	@Id INT,
	@FullName NVARCHAR(250), 
    @Phone VARCHAR(15), 
    @Address NVARCHAR(250),
	@Email VARCHAR(250),
	@Password VARCHAR(50),
	@BirthDay SMALLDATETIME
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 1
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[Employee] ([FullName], [Phone], [Address], [Email], [Password], [BirthDay])
			VALUES (@FullName, @Phone, @Address, @Email, @Password, @BirthDay)
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	SELECT @return;
END

