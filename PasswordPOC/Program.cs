using System;
using System.Collections.Generic;
using PasswordPOC.Helpers;
using PasswordPOC.Entities;
using PasswordPOC.Service;

namespace PasswordPOC
{
    class Program
    {
        static KeyFileGenerator kfg = new KeyFileGenerator();
        static IService service = new Service.Service();
        static byte[] keyfile;

        static void Main(string[] args)
        {
            if (!HandleUserKeyFile())
                return;

            MainMenu();
        }

        static bool HandleUserKeyFile()
        {
            Console.WriteLine("Do you have a key file? (y/n)");
            var response = Console.ReadLine();

            if (response == "n")
            {
                Console.Clear();
                kfg.SaveToFile("keyfile.key");
                Console.WriteLine("Key file generated and saved to keyfile.key");
                Console.ReadLine();
                return true;
            }
            else if (response == "y")
            {
                Console.Clear();
                Console.WriteLine("Please provide the file:");
                keyfile = kfg.ReadKeyFromFile(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Welcome back!");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid input. Exiting...");
                return false;
            }
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("press 1 for seeing your credentials");
                Console.WriteLine("press 2 for adding a new credential to manage");
                Console.WriteLine("press 3 to exit");

                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        DisplayCredentials();
                        break;
                    case "2":
                        AddCredentials();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }

        static void DisplayCredentials()
        {
            var credentials = service.getCredentials(keyfile);
            foreach (var item in credentials)
            {
                Console.WriteLine("Domain: " + item.domain);
                Console.WriteLine("Username: " + item.Username);
                Console.WriteLine("Password: " + item.Password);
                Console.WriteLine("_____________________________");
            }
            Console.ReadLine();
        }

        static void AddCredentials()
        {
            Console.WriteLine("Please provide the domain:");
            var domain = Console.ReadLine();
            Console.WriteLine("Please provide the username:");
            var username = Console.ReadLine();
            Console.WriteLine("Please provide the password:");
            var password = Console.ReadLine();

            service.addCredentials(new Credentials
            {
                domain = domain,
                Username = username,
                Password = password
            }, keyfile);

            Console.WriteLine("Credential added successfully!");
            Console.ReadLine();
        }
    }
}
