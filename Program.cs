using System;
using System.Collections.Generic;

namespace Bank_Application
{
    class Program
    {
        // List to store all registered users
        static List<User> users = new List<User>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Console Banking Application");
            MainMenu();
        }

        // Main menu for user options
        static void MainMenu()
        {
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Register();
                    break;
                case 2:
                    Login();
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    MainMenu();
                    break;
            }
        }

        // Method to register a new user
        static void Register()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            // Check if the username is already taken
            if (users.Exists(u => u.Username == username))
            {
                Console.WriteLine("Username already exists. Please try a different one.");
                Register(); // Restart registration if user is already registered before
                return;
            }

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            // Create a new user and add them to the list
            User newUser = new User(username, password);
            users.Add(newUser);

            Console.WriteLine("Registration successful!");
            MainMenu(); // Return to main menu after successful registration
        }

        // Method to log in an existing user
        static void Login()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            // Verify the username and password
            User user = users.Find(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                Console.WriteLine("Login successful!");
                UserMenu(user); // Proceed to user menu on successful login
            }
            else
            {
                Console.WriteLine("Invalid credentials. Please try again.");
                MainMenu();
            }
        }

        // User menu with banking options for a logged-in user
        static void UserMenu(User user)
        {
            Console.WriteLine("1. Open Account");
            Console.WriteLine("2. View Balance");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. Generate Statement");
            Console.WriteLine("6. Calculate Interest for Savings Account");
            Console.WriteLine("7. Log Out");

            Console.Write("Choose an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    user.OpenAccount();
                    break;
                case 2:
                    user.CheckBalance();
                    break;
                case 3:
                    user.Deposit();
                    break;
                case 4:
                    user.Withdraw();
                    break;
                case 5:
                    user.GenerateStatement();
                    break;
                case 6:
                    user.CalculateInterest();
                    break;
                case 7:
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    UserMenu(user);
                    break;
            }
            UserMenu(user); // Loop back to user menu after each operation is successfully completed
        }
    }

    // User Class
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Account> Accounts { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            Accounts = new List<Account>(); 
        }

        // Method to open a new account
        public void OpenAccount()
        {
            Console.Write("Enter Account Holder's Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Account Type (savings/checking): ");
            string type = Console.ReadLine().ToLower();
            Console.Write("Enter Initial Deposit Amount: ");
            double balance = double.Parse(Console.ReadLine());

            Account account = new Account(name, type, balance);
            Accounts.Add(account); // Adding new account
            Console.WriteLine($"Account created with Account Number: {account.AccountNumber}");
        }

        // Method to check balance 
        public void CheckBalance()
        {
            var account = SelectAccount();
            if (account != null)
            {
                Console.WriteLine($"Current Balance for Account {account.AccountNumber}: ${account.Balance}");
            }
        }

        //Method to Add interest
        public void CalculateInterest()
        {
            foreach (var account in Accounts)
            {
                if (account.AccountType == "savings")
                {
                    account.AddMonthlyInterest();
                }
            }
        }

        // Method to deposit money 
        public void Deposit()
        {
            var account = SelectAccount();
            if (account != null)
            {
                Console.Write("Enter deposit amount: ");
                double amount = double.Parse(Console.ReadLine());
                account.Deposit(amount);
                Console.WriteLine("Deposit successful.");
            }
        }

        // Method to withdraw money 
        public void Withdraw()
        {
            var account = SelectAccount();
            if (account != null)
            {
                Console.Write("Enter withdrawal amount: ");
                double amount = double.Parse(Console.ReadLine());
                if (account.Withdraw(amount))
                {
                    Console.WriteLine("Withdrawal successful.");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
        }

        // Method to display transaction history 
        public void GenerateStatement()
        {
            var account = SelectAccount();
            if (account != null)
            {
                Console.WriteLine($"Statement for Account {account.AccountNumber}:");
                foreach (var transaction in account.Transactions)
                {
                    Console.WriteLine($"{transaction.Date}: {transaction.Type} - ${transaction.Amount}");
                }
            }
        }

        private Account SelectAccount()
        {
            Console.Write("Enter Account Number: ");
            int accNumber = int.Parse(Console.ReadLine());
            return Accounts.Find(a => a.AccountNumber == accNumber);
        }
    }

    // Account Class
    public class Account
    {
        private static int accountno = 1000;
        private static readonly double InterestRate = 0.02; 
        public int AccountNumber { get; }
        public string AccountHolderName { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; private set; }
        public List<Transaction> Transactions { get; private set; }
        private DateTime? LastInterestDate { get; set; }

        public Account(string holderName, string type, double initialBalance)
        {
            AccountNumber = accountno++;
            AccountHolderName = holderName;
            AccountType = type;
            Balance = initialBalance;
            Transactions = new List<Transaction>();
            Transactions.Add(new Transaction("Deposit", initialBalance));
        }

        public void Deposit(double amount)
        {
            Balance += amount;
            Transactions.Add(new Transaction("Deposit", amount));
        }

        public bool Withdraw(double amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Transactions.Add(new Transaction("Withdrawal", amount));
                return true;
            }
            return false;
        }

        public void AddMonthlyInterest()
        {
            if (LastInterestDate == null || LastInterestDate.Value.AddMonths(1) <= DateTime.Now)
            {
                double interest = Balance * InterestRate;
                Balance += interest;
                Transactions.Add(new Transaction("Interest", interest));
                LastInterestDate = DateTime.Now;
                Console.WriteLine($"Monthly interest of ${interest:F2} added to Account {AccountNumber}.");
            }
            else
            {
                Console.WriteLine("Interest already added for this month.");
            }
        }
    }

    // Transaction Class
    public class Transaction
    {
        private static int transactionno= 1; 
        public int TransactionID { get; }
        public DateTime Date { get; }
        public string Type { get; }
        public double Amount { get; }

        public Transaction(string type, double amount)
        {
            TransactionID = transactionno++;
            Date = DateTime.Now;
            Type = type;
            Amount = amount;
        }
    }
}
