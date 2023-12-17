using POS___POS;
using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
 
namespace DisplaMenu_Test
{
    internal class Program
    {
        
        static void Main(string[] args)
        {            
            var posSystem = new POSSystem();
            posSystem.Run();
        }

        internal class POSSystem
        {
            private Inventory inventory;
            private LoginAuthentication authentication;
            private ExecuteFunctions execute;
            private Display display;
            
           
            public POSSystem()
            {
                inventory = new Inventory();
                authentication = new LoginAuthentication();
                execute = new ExecuteFunctions(inventory, authentication);
                display = new Display(authentication, execute, inventory);
                
            }

            public void Run()
            {
                Console.CursorVisible = false;
                //display.DisplayLogo();
                Console.Clear();
                Console.SetWindowSize(95, 25);                
                Display.DisplayHeader(inventory);
                execute.MainMenuProcess(inventory);
            }            
           
        }
        #region CONSTANTS
        internal static class Constants
        {
            public const int ExitApplicationCode = 100;
            public const int AboutUs = 3;
            public const int ProductManagementMenuOption = 1;
            public const int InventoryMenuOption = 2;
            public const int POSMenuOption = 0;
            public const int ExitMenuOption = 4;
            public const int ProductsPerPage = 10;
        }

        #endregion
    }

}


