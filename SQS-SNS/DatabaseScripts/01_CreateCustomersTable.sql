CREATE TABLE Customer (
    Id UUID PRIMARY KEY,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Email VARCHAR(255),
    TotalMoneySpent NUMERIC(10, 2)
);