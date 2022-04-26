using System;

namespace CSharp.LabExercise3
{
    internal class Program
    {
        class InputValidation
        {
            public bool HasInput(string userInput)
            {
                if (userInput == null || userInput.Trim().Length == 0)
                {
                    Console.WriteLine("\n!!You Entered A Blank Value\n");
                    return false;
                }
                return true;
            }

            public bool IsNumber(string userInput)
            {
                if (!int.TryParse(userInput, out int number))
                {
                    Console.WriteLine("\n!!Please Enter A Valid Whole Number\n");
                    return false;
                }

                return true;
            }

            public bool IsDecimal(string userInput)
            {
                if (!decimal.TryParse(userInput, out decimal number))
                {
                    Console.WriteLine("\n!!Please Enter A Valid Amount\n");
                    return false;
                }

                return true;
            }
        }

        class Account
        {
            public decimal Balance { get; set; }
            public Account()
            {
                this.Balance = 0;
            }
        }

        class Withdraw
        {
            Account account;
            public Withdraw(Account account)
            {
                this.account = account;
            }
            public void WithdrawBalance(decimal amount)
            {
                this.account.Balance -= amount;
                Console.WriteLine("\nWithdraw Transaction Successful\n");
            }
        }
        class Deposit
        {
            Account account;
            public Deposit(Account account)
            {
                this.account = account;
            }

            public void DepositBalance(decimal amount)
            {
                this.account.Balance += amount;
                Console.WriteLine("\nDeposit Transaction Successful\n");
            }
        }

        class StartTransaction
        {
            Account account;
            InputValidation validation;
            public StartTransaction()
            {
                this.account = new Account();
                this.validation = new InputValidation();
            }

            public void DisplayBalance()
            {
                Console.WriteLine("\n**************View Balance**************");
                Console.WriteLine($"\nYOUR BALANCE IS {this.account.Balance}\n");
            }

            public void Withdraw()
            {
                Console.WriteLine("\n************Withdraw Balance************");
                Console.Write("\nEnter Amount (Minimum 100) To Withdraw: ");
                string rawUserInput = Console.ReadLine();

                if (validateWithdrawUserInput(rawUserInput))
                {
                    Withdraw withdraw = new Withdraw(account);
                    withdraw.WithdrawBalance(Convert.ToDecimal(rawUserInput));
                    Console.WriteLine($"YOUR NEW BALANCE IS {this.account.Balance}\n");
                }
            }

            public void Deposit()
            {
                Console.WriteLine("\n************Deposit Balance*************");
                Console.Write("\nEnter Amount (Minimum 100) To Deposit: ");
                string rawUserInput = Console.ReadLine();

                if (validateDepositUserInput(rawUserInput))
                {
                    Deposit deposit = new Deposit(account);
                    deposit.DepositBalance(Convert.ToDecimal(rawUserInput));
                    Console.WriteLine($"YOUR NEW BALANCE IS {this.account.Balance}\n");
                }
            }

            public bool validateMainUserInput(string userInput)
            {
                if (!validation.HasInput(userInput) || !validation.IsNumber(userInput)) { return false; }

                if (Convert.ToDecimal(userInput) == 0 || Convert.ToDecimal(userInput) > 4)
                {
                    Console.WriteLine("\n!!Please Choose From 1 to 4\n");
                    return false;
                }

                return true;
            }

            public bool validateWithdrawUserInput(string userInput)
            {
                if (!validation.HasInput(userInput) || !validation.IsDecimal(userInput)) { return false; }

                decimal decimalUserInput = Convert.ToDecimal(userInput);

                if (decimalUserInput > this.account.Balance)
                {
                    Console.WriteLine("\n!!Insufficient Funds\n");
                    return false;
                }

                if (decimalUserInput < 100)
                {
                    Console.WriteLine("\n!!Please Enter Minimum of 100\n");
                    return false;
                }

                return true;
            }

            public bool validateDepositUserInput(string userInput)
            {
                if (!validation.HasInput(userInput) || !validation.IsDecimal(userInput)) { return false; }

                decimal decimalUserInput = Convert.ToDecimal(userInput);

                if (decimalUserInput < 100)
                {
                    Console.WriteLine("\n!!Please Enter Minimum of 100\n");
                    return false;
                }

                return true;
            }
        }

        static void Main(string[] args)
        {
            StartTransaction transaction = new StartTransaction();
            bool active = true;

            do
            {
                Console.WriteLine("*********Welcome To ATM Service*********\n");
                Console.WriteLine("1. Check Balance\n");
                Console.WriteLine("2. Withdraw Cash\n");
                Console.WriteLine("3. Deposit Cash\n");
                Console.WriteLine("4. Quit\n");
                Console.Write("Enter Your Choice: ");
                string rawUserInput = Console.ReadLine();
                if (transaction.validateMainUserInput(rawUserInput))
                {
                    switch (rawUserInput)
                    {
                        case "1": transaction.DisplayBalance(); break;

                        case "2": transaction.Withdraw(); break;

                        case "3": transaction.Deposit(); break;

                        case "4": Console.WriteLine("\n**************Thank You!****************"); active = false; break;
                    }
                }

            } while (active);
        }
    }
}
