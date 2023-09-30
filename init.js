'use BankDeposits'

db.Bank.insertOne({
    "_id": ObjectId(),
    "LastName": "Doe",
    "FirstName": "John",
    "Patronymic": "Jasper",
    "PassportSeries": "1234",
    "PassportNumber": "567890",
    "HomeAddress": "Kyiv, Ukraine, Home Street, 1",
    "Accounts": [
        {
            "AccountNumber": "1234567890",
            "Amount": NumberDecimal("100.00"),
            "Deposits": [
                {
                    "Amount": NumberDecimal("50.00"),
                    "Date": ISODate("2020-01-01T00:00:00Z"),
                }
            ]
        }
    ]
})

db.Bank.insertOne({
    "_id": ObjectId(),
    "LastName": "Doe",
    "FirstName": "Jane",
    "Patronymic": "Jasper",
    "PassportSeries": "1234",
    "PassportNumber": "567891",
    "HomeAddress": "Kyiv, Ukraine, Home Street, 2",
    "Accounts": [
        {
            "AccountNumber": "1234567891",
            "Amount": NumberDecimal("200.00"),
            "Deposits": [
                {
                    "Amount": NumberDecimal("100.00"),
                    "Date": ISODate("2020-01-01T00:00:00Z"),
                },
                {
                    "Amount": NumberDecimal("100.00"),
                    "Date": ISODate("2020-02-01T00:00:00Z"),
                },
                
            ]
        }
    ]
})

for (let i = 1; i <= 10; i++) {
    let depositorId = ObjectId(); // equivalent to NEWID()
    let lastName = 'LastName' + i;
    let firstName = 'FirstName' + i;
    let patronymic = 'Patronymic' + i;
    let passportSeries = 'AB';
    let passportNumber = '1234' + i;
    let homeAddress = 'Some Address';
    let accountNumber = 'ACC' + i;
    let amount = 1000 * i;

    let depositor = {
        _id: depositorId,
        LastName: lastName,
        FirstName: firstName,
        Patronymic: patronymic,
        PassportSeries: passportSeries,
        PassportNumber: passportNumber,
        HomeAddress: homeAddress,
        Accounts: [
            {
                AccountNumber: accountNumber,
                Amount: NumberDecimal(amount.toFixed(2)),
                Deposits: []
            }
        ]
    };

    let randomDeposits = Math.floor(Math.random() * 3) + 1; // equivalent to CAST(RAND() * 3 AS INT) + 1

    for (let j = 1; j <= randomDeposits; j++) {
        let depositAmount = Math.random() * 1000;
        let deposit = {
            Amount: NumberDecimal(depositAmount.toFixed(2)),
            Date: ISODate()
        };

        depositor.Accounts[0].Deposits.push(deposit);
    }

    db.Bank.insertOne(depositor);
}
