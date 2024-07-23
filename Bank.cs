using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;



namespace Bank
{
    public class CreateAccount
    {
        private string[] user = new string[6];
        private string user_file = "account.txt";

        public void SetAccount()
        {
            //Create user Account
            Console.WriteLine("Welcome\nHere you will create your Bank account\nPlease fill out the form\n\n");
            string[] field = new string[6] { "Username:", "name:", "Age:", "password:", "account type:", "Initial deposit must be >=500\n$:" };

            int controller = 0;
            int max_try = 3; // this is for retrying when user enters deposit < 500
            for (controller = 0; controller < 6; controller++)
            {
                Console.Write("{0}", field[controller]);
                user[controller] = Console.ReadLine();
            }
            if (Convert.ToInt32(user[5]) < 500)
            {
                Console.WriteLine("Sorry Enter deposit >= 500");
                user[5] = "0";
                while (Convert.ToInt32(user[5]) < 500 && max_try > 0)
                {
                    Console.Write("{0}", field[5]);
                    user[5] = Console.ReadLine(); // Here passing a string because the array is a string array
                    max_try--;
                }
            }
            
            if (Convert.ToInt32(user[5]) < 500 && max_try <= 0)
            {
                Console.WriteLine("Sorry your account could not be created\n try again later!");
                Console.ReadKey();
            }
            else
            {
                if (Convert.ToInt32(user[5]) >= 500)
                {
                    Console.WriteLine("Congradulations!!\nYour account has been created successfully");
                }
            }
            
            if (File.Exists(user_file))
            {
                try
                {
                    File.WriteAllLines(user_file, user);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
            else
            {
                try
                {
                    File.WriteAllLines(user_file, user);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
        }
    
public static bool ValidatePassword(string password)    
   {{    
      int validConditions = 0;     
      foreach(char c in password)    
      {    
         if (c >= 'a' && c <= 'z')    
         {    
            validConditions++;    
            break;    
         }     
      }     
      foreach(char c in password)    
      {    
         if (c >= 'A' && c <= 'Z')    
         {    
            validConditions++;    
            break;    
         }     
      }     
      if (validConditions == 0) return false;     
      foreach(char c in password)    
      {    
         if (c >= '0' && c <= '9')    
         {    
            validConditions++;    
            break;    
         }     
      }     
      if (validConditions == 1) return false;     
      if(validConditions == 2)    
      {    
         char[] special = {'@', '#', '$', '%', '^', '&', '+', '='}; // or whatever    
         if (password.IndexOfAny(special) == -1) return false;    
      }     
      return true;    
   }}
    class Account
    {
        //string user_file = "account.txt";
        private string[] user_info = File.ReadAllLines("account.txt");
        private string[] options = new string[] { "1-> Account Enquiry", "2-> Deposit funds", "3-> Withdraw funds", "4-> Change password", "5-> Transfer funds", "6-> Exit" };
        private int opt_view = 0;
        private int limit = 1200;


        public void UserAcount()
        {
            Console.WriteLine("Login");
            Console.Write("User name:");
            string user_name = Console.ReadLine();
            Console.Write("Password:");
            string password = Console.ReadLine();
            // Console.WriteLine(ValidatePassword(password));
            Console.WriteLine("\n\n");

            if (user_name == user_info[0] && password == user_info[3])
            {
                menu();
            }
            else
            {
                Console.WriteLine("Could not login\nWrong credentials!\n{0}\n{1}\n------------------", user_info[0], user_info[3]);
            }
        }

        private int AccountFunds()
        {
            int funds = Convert.ToInt32(user_info[5]);
            Console.Write("Account Funds\n$:{0}\n\n", funds);
            menu(); //return to main menu
            return funds;
        }
        private void Deposit()
        {
            //deposit funds to your accounnt
            Console.WriteLine("------------------\nCurrent funds in account:\n$ {0}\n---------------------\n", user_info[5]);
            Console.Write("Deposit amount:$ ");
            string funds = Console.ReadLine();
            if (Convert.ToInt32(funds) > 0)
            {
                try
                {
                    if (File.Exists("account.txt"))
                    {
                        int new_funds = Convert.ToInt32(user_info[5]) + Convert.ToInt32(funds);
                        try
                        {
                            user_info[5] = Convert.ToString(new_funds);
                            File.WriteAllLines("account.txt", user_info);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                }
                catch (Exception error)
                {
                    Console.WriteLine("------------------------\n{0}\n---------------------", error.Message);
                }
            }
            else
            {
                Console.WriteLine("-----------------------\nEnter amount > 0");
            }
            Console.WriteLine("Your funds have deposited successfully!");
            Console.WriteLine("New ballance:\n$ {0}\n", user_info[5]);
            menu();
        }

        private void Withdraw()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Current funds:$ {0}\nLimit:$ {1}", user_info[5], limit);
            Console.WriteLine("----------------------------------");
            int current_funds = Convert.ToInt32(user_info[5]);

            Console.Write("Withdraw:$ ");
            int withdraw = Convert.ToInt32(Console.ReadLine());

            if (withdraw < limit && Convert.ToInt32(user_info[5]) - withdraw > 0)
            {
                int new_funds = current_funds - withdraw;
                user_info[5] = Convert.ToString(new_funds);
                try
                {
                    File.WriteAllLines("account.txt", user_info);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
            else
            {
                Console.Write("--------------------------------------\n");
                Console.WriteLine("Your request is above the limit\nRequest:$ {0}\nCurrent balance:$ {1}\nLimit:$ {2}", withdraw, user_info[5], limit);
                Console.Write("--------------------------------------\n\n");
                withdraw = 0; //reseting request
                Withdraw();
            }
            Console.WriteLine("Funds successfully withdrawn!\nNew Account ballance:$ {0}\n\n", user_info[5]);
            menu();
        }
private void transfer()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Current funds:$ {0}\nLimit:$ {1}", user_info[5], limit);
            Console.WriteLine("----------------------------------");
            int current_funds = Convert.ToInt32(user_info[5]);

            Console.Write("$ ");
            
            
            int withdraw = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter reciever account number:");
            string reciever = Console.ReadLine();

            if (withdraw < limit && Convert.ToInt32(user_info[5]) - withdraw > 0)
            {
                int new_funds = current_funds - withdraw;
                user_info[5] = Convert.ToString(new_funds);
                try
                {
                    File.WriteAllLines("account.txt", user_info);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
            else
            {
                Console.Write("--------------------------------------\n");
                Console.WriteLine("Your request is above the limit\nRequest:$ {0}\nCurrent balance:$ {1}\nLimit:$ {2}", withdraw, user_info[5], limit);
                Console.Write("--------------------------------------\n\n");
                withdraw = 0; //reseting request
                transfer();
            }
            Console.WriteLine("Funds successfully Transfered\nNew Account ballance:$ {0}\n\n", user_info[5]);
            menu();
        }
        private void ChangePassword()
        {
            Console.Write("Change your password here\nKeep your password safe\n\n");
            string current_password = user_info[3];
            Console.Write("Confirm old password: ");
            string confirm = Console.ReadLine();
            if (confirm == current_password)
            {
                Console.Write("\nEnter new password: ");
                string password = Console.ReadLine();
                if (password != current_password)
                {
                    user_info[3] = password;
                }
                else
                {
                    Console.Write("------------------------------------------------\n");
                    Console.Write("New password can not be the same as old password\n");
                    Console.Write("--------------------------------------------------\n");
                    ChangePassword();
                }
            }
            else
            {
                Console.Write("\n----------------\nPassword mismatched\n--------------------\n");
                ChangePassword();
            }
            try
            {
                File.WriteAllLines("account.txt", user_info);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            Console.WriteLine("Your password has been changed successfully!\n");
            menu();
        }


        public void menu()
        {
            //Account Main Menu
            for (opt_view = 0; opt_view < 6; opt_view++)
            {
                Console.WriteLine("{0}", options[opt_view]);
            }
            Console.Write(">>");
            string option = Console.ReadLine();
            if (Convert.ToInt32(option) == 1)
            {
                AccountFunds();
            }
            else
            {
                if (Convert.ToInt32(option) == 2)
                {
                    Deposit();
                }
            }
            if (Convert.ToInt32(option) == 3)
            {
                Withdraw();
            }
            if (Convert.ToInt32(option) == 4)
            {
                ChangePassword();
            }
            if (Convert.ToInt32(option) == 5)
            {
                transfer();
            }
            if (Convert.ToInt32(option) == 6)
            {
                Environment.Exit(6);
            }     
        }
    }

    class Run
    {
        static void Main(String[] args)
        {
            Console.Title = "Bank";
            CreateAccount signup = new CreateAccount();
            Account user = new Account();
            Console.Write("\n\n1-> login\n2->Register\npress 0 to exit..\n>>");
            int option = Convert.ToInt32(Console.ReadLine());
            if (option == 1)
            {
                user.UserAcount();
            }
            else if(option == 2)
            
            {
                signup.SetAccount();
            }
            else{
            Environment.Exit(0);
        }
        }
    }
}}