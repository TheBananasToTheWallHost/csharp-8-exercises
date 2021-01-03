using System;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Security.Claims;
using Packt.Cryptography;
using static System.Console;

namespace SecureApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Protector.Register("Alice", "password", new string[]{"Admins"});
            Protector.Register("Bob", "password", new string[] {"Sales", "TeamLeads"});
            Protector.Register("Eve", "password");

            Write("Enter your username: ");
            string username = ReadLine();
            Write("Enter your password");
            string password = ReadLine();

            Protector.LogIn(username, password);

            if(Thread.CurrentPrincipal == null){
                WriteLine("Log in failed");
                return;
            }

            var p = Thread.CurrentPrincipal;

            WriteLine($"IsAuthenticated: {p.Identity.IsAuthenticated}");
            WriteLine($"AuthenticationType: {p.Identity.AuthenticationType}");
            WriteLine($"Name: {p.Identity.Name}");
            WriteLine($"Is in role (\"Admins\"): {p.IsInRole("Admins")}");
            WriteLine($"Is in role (\"Sales\"): {p.IsInRole("Sales")}");

            if(p is ClaimsPrincipal){
                WriteLine($"{p.Identity.Name} has the following claims: ");

                foreach(Claim claim in (p as ClaimsPrincipal).Claims){
                    WriteLine($"{claim.Type}: {claim.Value}");
                }
            }
        }

        private static void SecureFeature(){
            if(Thread.CurrentPrincipal == null){
                throw new SecurityException("A user must be logged in to use this feature");
            }

            if(!Thread.CurrentPrincipal.IsInRole("Admins")){
                throw new SecurityException("A user must be a member of the admins to access this feature");
            }

            WriteLine("You have access to this secure feature");
        }
    }
}
