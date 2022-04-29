# BankingAPI
by: Xing Tao Shi

###	What are the different endpoints that a client can use? 
##### GET: api/Customers
Gets all Customers
##### GET: api/Customers/{id}
Gets all Customers by CustomerId
##### POST: api/Customers
Inserts a new Customer
##### DELETE: api/Customers/{id}
Deletes Customer by CustomerId

###	Sample request bodies, if applicable? 
##### POST: api/Customers
{
    "FullName": "Xing Tao Shi",
    "Email": "xshi600@gmail.com",
    "Transaction": {
        "TransactionName": "Groceries",
        "Amount": 500.32
    }
}

###	Sample response body? 
{
  "statusCode": 200,
  "statusDescription": "Call was successful. Returned existing customers.",
  "customer": null,
  "customers": [
    {
      "customerId": 7,
      "fullName": "Xing Tao Shi",
      "email": xshi603@gmail.com,
      "transaction": {
        "transactionId": 3,
        "transactionName": "May Groceries",
        "amount": 500,
        "customerId": 7
      }
    }
  ]
}

###	Changes Made
The original idea was to have a one to many relationship between Customers and Transactions but for the sake of simplicity I changed it to one to one.
Additionally, it didn't really make sense logically to have a PUT request because transactions should be immutable after being created.
