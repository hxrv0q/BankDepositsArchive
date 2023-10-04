CREATE
DATABASE BankDeposits;
GO

USE BankDeposits;
GO

CREATE TABLE Depositor
(
    ID             UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    LastName       NVARCHAR(50),
    FirstName      NVARCHAR(50),
    Patronymic     NVARCHAR(50),
    PassportSeries NVARCHAR(10),
    PassportNumber NVARCHAR(10),
    HomeAddress    NVARCHAR(255),
    UNIQUE (PassportSeries, PassportNumber)
);


CREATE TABLE Account
(
    ID          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    DepositorID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Depositor (ID) ON DELETE CASCADE ON UPDATE CASCADE,
    Number      NVARCHAR(20) NOT NULL,
    Amount      MONEY NOT NULL,
    UNIQUE (Number)
);

CREATE TABLE Deposit
(
    ID        UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    AccountID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Account (ID) ON DELETE CASCADE ON UPDATE CASCADE,
    Amount    MONEY    NOT NULL,
    Date      DATETIME NOT NULL
);


BEGIN
TRANSACTION;

BEGIN TRY
    -- Declare variables
    DECLARE
@counter INT;
    DECLARE
@depositorID UNIQUEIDENTIFIER;
    DECLARE
@accountID UNIQUEIDENTIFIER;
    
    SET
@counter = 1;

    -- Loop to insert 10 depositors
    WHILE
@counter <= 10
BEGIN
        SET
@depositorID = NEWID();
INSERT INTO Depositor (ID, LastName, FirstName, Patronymic, PassportSeries, PassportNumber, HomeAddress)
VALUES (@depositorID, CONCAT('LastName', @counter), CONCAT('FirstName', @counter), CONCAT('Patronymic', @counter), 'AB',
        CONCAT('1234', @counter), 'Some Address');

-- Insert 1 account for each depositor
SET
@accountID = NEWID();
INSERT INTO Account (ID, DepositorID, AccountNumber, Amount)
VALUES (@accountID, @depositorID, CONCAT('ACC', @counter), 1000 * @counter);

-- Insert 1-3 deposits randomly for each account
DECLARE
@depositCounter INT = 1;
        DECLARE
@randomDeposits INT = CAST(RAND() * 3 AS INT) + 1;

        WHILE
@depositCounter <= @randomDeposits
BEGIN
INSERT INTO Deposit (ID, AccountID, Amount, Date)
VALUES (NEWID(), @accountID, CAST(RAND() * 1000 AS MONEY), GETDATE());

SET
@depositCounter = @depositCounter + 1;
END;
        
        SET
@counter = @counter + 1;
END;

COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    -- Rollback any changes if something went wrong
ROLLBACK TRANSACTION;

    -- Log or re-throw the error for further investigation
    THROW;
END CATCH;
