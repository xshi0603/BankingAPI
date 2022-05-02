CREATE DATABASE BankDataService;

USE BankDataService;

CREATE TABLE Customers(
    CustomerId INT NOT NULL AUTO_INCREMENT,
    FullName VARCHAR(50),
    Email VARCHAR(50),
    PRIMARY KEY ( CustomerId )
);

ALTER TABLE Customers ADD CONSTRAINT UniqueEmail UNIQUE (Email);

CREATE TABLE Transactions(
    TransactionId INT NOT NULL AUTO_INCREMENT,
    TransactionName VARCHAR(1000),
    Amount FLOAT,
    PRIMARY KEY ( TransactionId )
);

ALTER TABLE Transactions ADD COLUMN CustomerId INT;

ALTER TABLE Transactions ADD CONSTRAINT FK_TransactionCustomer FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId);
