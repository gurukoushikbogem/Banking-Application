
# Banking Console Application

A console-based banking application built in C#, providing core functionalities such as user registration, login, account management, and transaction handling. This project is a simple demonstration of handling user inputs, managing data structures, and implementing basic financial transactions.

## Key Features

- **User Registration & Login**: 
  - Allows users to create a username and password to register.
  - Login functionality verifies user credentials to access account features.

- **Account Opening**: 
  - Registered users can open a new bank account with a unique account number.
  - Supports saving and checking account types, requiring an initial deposit for new accounts.

- **Transaction Processing**:
  - Enables deposits and withdrawals with validation to prevent overdrafts.
  - Logs each transaction with details like transaction ID, date, type (deposit or withdrawal), and amount.

- **Account Statement**: 
  - Displays the transaction history for an account, listing details such as date, type, and amount of each transaction.

- **Interest Calculation (Savings Accounts)**:
  - Calculates and adds monthly interest to savings account balances.
  - Uses a fixed interest rate, ensuring that interest is added once per month.

- **Balance Check**: 
  - Allows users to view the current balance of their accounts.

## Classes and Services

### User Class
- Manages individual users and holds their login credentials.
- Contains account-related functionalities, including opening new accounts and handling deposits, withdrawals, and balance checks.

### Account Class
- Holds essential account information like account number, type, balance, and transaction history.
- Handles deposit and withdrawal operations with overdraft prevention.

### Transaction Class
- Represents individual transactions, recording a unique ID, date, type (deposit/withdrawal), and amount.

### Interest Calculation
- Applies a fixed monthly interest rate to savings accounts, adding it to the balance only once per month.

## Application Components

### Main Menu
- Entry point for users, allowing them to register, log in, or exit the application.

### User Menu
- Accessible after successful login; offers options like opening an account, viewing balance, making deposits or withdrawals, generating account statements, and logging out.

### Error Handling
- Validates user inputs and provides feedback for invalid actions, like incorrect login credentials or insufficient funds.
