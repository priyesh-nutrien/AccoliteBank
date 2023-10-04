# Unit Test

The project is developed with 
> .Net 6, C#, Swagger, SQL LocalDB, Repository Pattern, Data Seeder, MSTest, MOQ

DbContext **AccoliteBank.DataAccess**
> Open *Package Manager Console* and run the below command to generate SQL LocalDB and sample data. SQL LocalDB or SQL Server both can be used. Review *appsettings.json* for connection string.
```
Update-Database
```

**Swagger**
> Review swagger for API endpoints. It could be used for manual api testing.
```
/swagger/index.html
```

**How to use MSTest**
- Goto "View"
- Find "Test Explorer" from the list
- Run the application without debug (ctrl+f5) than run the test. Used MOQ for api, service test. 

**Test**
- ApiControllerTest
- Service Test

**Requirements**
Banking Test for Software Engineer
----------------------------------

We would like the candidate to spend no more than a couple hours of their time on this.
We would like to see test case creation, test data creation, and show how they would trigger the execution of tests with anticipated results.

*Banking System Test*

Create an API to facilitate banking operations, no need to develop a GUI (no need to browser test, etc). Write test cases for the API. 

*System Design:*

System allows for users and user accounts.
- A user can have as many accounts as they want.
- A user can create and delete accounts.
- A user can deposit and withdraw from accounts.
- An account cannot have less than $100 at any time in an account.
- A user cannot withdraw more than 90% of their total balance from an account in a single transaction.
- A user cannot deposit more than $10,000 in a single transaction.

*Notes:*

We will focus our review on coding style, organization, testability and test coverage.
Don't worry about a real database. Feel free to fake it with in-memory data structures.
The completed work can be returned to us in a zipped/compressed package that we can extract, build and run; or through a public repository such as GitHub.  Please setup the project so that we can run the application locally in a container via Docker Compose.

