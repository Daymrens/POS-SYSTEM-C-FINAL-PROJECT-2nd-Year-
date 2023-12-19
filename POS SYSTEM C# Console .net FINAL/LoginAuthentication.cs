using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisplaMenu_Test
{
    public class LoginAuthentication
    {
        

        public string Password { get; set; }
        public bool IsAdminLogin { get; set; } = false;

        public bool Authenticate(Inventory inventory)
        {

            if (IsAdmin())
            {
                Console.Clear();
                Console.SetCursorPosition(0,0);
                Console.WriteLine(" - Login successful as admin!");
                IsAdminLogin = true;
                inventory.Admin = true;
                Thread.Sleep(1000);
                Console.Clear();
                return true;
            }
            else
            {
                Console.SetCursorPosition(0, 2);
                Console.WriteLine("Invalid login credentials! Please try again.");
                ExecuteFunctions.ClearLines(12, 20, 12);
                return false;
            }
        }

        public bool IsAdmin()
        {            
            return Password == "admin";

        }

        public bool cAdmin()
        {
            if(IsAdmin())
            {
                return true;
            }
            else { return false; }
        }

        public void Logout()
        {
            Console.WriteLine("Logging out...");
            Thread.Sleep(1000);
            Console.Clear();
            
        }
    }
}

