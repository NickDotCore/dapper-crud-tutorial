CREATE TABLE PersonCRUD
(
	personID INT IDENTITY(1,1) PRIMARY KEY,
	personFirstName NVARCHAR(100) NOT NULL,
	personLastName NVARCHAR(100) NOT NULL,
	personEmail NVARCHAR(100) NOT NULL,
	personPhone NVARCHAR(10) NOT NULL,
)

CREATE OR ALTER PROCEDURE spPersonCRUD_Create
	@personFirstName NVARCHAR(100),
	@personLastName NVARCHAR(100),
	@personEmail NVARCHAR(100),
	@personPhone NVARCHAR(100)
AS
BEGIN
	BEGIN TRANSACTION
		--  inserts person
		INSERT INTO PersonCRUD(personFirstName, personLastName, personEmail, personPhone)
		VALUES (@personFirstName, @personLastName, @personEmail, @personPhone)

		DECLARE @personID INT
		SET @personID = SCOPE_IDENTITY() -- returns the last identity created in the same session
	COMMIT TRANSACTION
END
GO

CREATE OR ALTER PROCEDURE spPersonCRUD_Update
	@personID INT,
	@personFirstName NVARCHAR(100),
	@personLastName NVARCHAR(100),
	@personEmail NVARCHAR(100),
	@personPhone NVARCHAR(100)
AS
BEGIN
	BEGIN TRANSACTION
		UPDATE PersonCRUD
		SET personFirstName = @personFirstName, personLastName = @personLastName, personEmail = @personEmail, personPhone = @personPhone
		WHERE personID = @personID
	COMMIT TRANSACTION
END
GO

CREATE OR ALTER PROCEDURE spPersonCRUD_Delete
	@personID INT
AS
BEGIN
	BEGIN TRANSACTION
		DELETE FROM PersonCRUD WHERE personID = @personID
	COMMIT TRANSACTION
END
GO

CREATE OR ALTER PROCEDURE spPersonCRUD_ReadAll
AS
BEGIN
	SELECT * FROM PersonCRUD
END
GO

CREATE OR ALTER PROCEDURE spPersonCRUD_SearchFirstName
	@SearchFirstName NVARCHAR(100)
AS
BEGIN
	SELECT * FROM PersonCRUD WHERE @SearchFirstName = '' OR personFirstName LIKE '%'+@SearchFirstName+'%'
END
GO