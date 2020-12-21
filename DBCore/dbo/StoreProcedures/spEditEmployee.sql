CREATE PROCEDURE [dbo].[spEditEmployee]
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
			UPDATE [dbo].[Employee]
			   SET [FullName] = @FullName, [Phone] = @Phone, [Address] = @Address, [Email] = @Email, [BirthDay] = @BirthDay
			 WHERE [Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	SELECT @return;
END

