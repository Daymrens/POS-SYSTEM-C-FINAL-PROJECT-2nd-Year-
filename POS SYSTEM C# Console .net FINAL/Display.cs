using DisplaMenu_Test;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;

using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DisplaMenu_Test.Program;


namespace POS___POS
{

    public class Display
    {
        private LoginAuthentication authentication;
        private ExecuteFunctions execute;
        private Inventory inventory;
        public static int i = 0;
        private static CancellationTokenSource cancellationTokenSource;



        public Display(LoginAuthentication authentication, ExecuteFunctions execute, Inventory inventory)
        {
            this.authentication = authentication;
            this.execute = execute;
            this.inventory = inventory;
        }

        #region Display Logo Section
        public void DisplayLogo()
        {
            #region LOGO
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetWindowSize(135, 30);
            int startingY = 16;
            Console.SetCursorPosition(startingY, 7);
            Console.WriteLine("██████╗  ██████╗ ██╗███╗   ██╗████████╗     ██████╗ ███████╗    ███████╗ █████╗ ██╗     ███████╗███████╗");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY, 8);
            Console.WriteLine("██╔══██╗██╔═══██╗██║████╗  ██║╚══██╔══╝    ██╔═══██╗██╔════╝    ██╔════╝██╔══██╗██║     ██╔════╝██╔════╝");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY, 9);
            Console.WriteLine("██████╔╝██║   ██║██║██╔██╗ ██║   ██║       ██║   ██║█████╗      ███████╗███████║██║     █████╗  ███████╗");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY, 10);
            Console.WriteLine("██╔═══╝ ██║   ██║██║██║╚██╗██║   ██║       ██║   ██║██╔══╝      ╚════██║██╔══██║██║     ██╔══╝  ╚════██║");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY, 11);
            Console.WriteLine("██║     ╚██████╔╝██║██║ ╚████║   ██║       ╚██████╔╝██║         ███████║██║  ██║███████╗███████╗███████║");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY, 12);
            Console.WriteLine("╚═╝      ╚═════╝ ╚═╝╚═╝  ╚═══╝   ╚═╝        ╚═════╝ ╚═╝         ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY, 13);
            Console.WriteLine("                                                                                                        ");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(startingY + 6, 14);
            Console.WriteLine("                                  ██╗ ██████╗ ██╗ ██╗ ██╗");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY + 6, 15);
            Console.WriteLine("                                 ██╔╝██╔════╝████████╗╚██╗");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY + 6, 16);
            Console.WriteLine("                                 ██║ ██║     ╚██╔═██╔╝ ██║");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY + 6, 17);
            Console.WriteLine("                                 ██║ ██║     ████████╗ ██║");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY + 6, 18);
            Console.WriteLine("                                 ╚██╗╚██████╗╚██╔═██╔╝██╔╝");
            Thread.Sleep(100);
            Console.SetCursorPosition(startingY + 6, 19);
            Console.WriteLine("                                  ╚═╝ ╚═════╝ ╚═╝ ╚═╝ ╚═╝");
            Thread.Sleep(100);
            ConsoleKeyInfo keyInfo;
            Console.ResetColor();

            #endregion 

            do
            {
                string[] prompt = { "Opening System", "Stacking Items", "Preparing Store" };
                Console.SetCursorPosition(45, 21);
                Console.Write("Press [Enter] to open system  [Esc] to exit");


                Console.ResetColor(); // Reset the console color to the default

                keyInfo = Console.ReadKey();
                Console.ResetColor();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(0, 22);
                    DisplayLoadingScreen(prompt, 0, 20);
                    ExecuteFunctions.ClearLines(22, 23, 0);
                    Console.SetCursorPosition(0, 22);
                    DisplayLoadingScreen(prompt, 1, 20);
                    ExecuteFunctions.ClearLines(22, 23, 0);
                    Console.SetCursorPosition(0, 22);
                    DisplayLoadingScreen(prompt, 2, 20);
                    ExecuteFunctions.ClearLines(22, 23, 0);
                    // Continue with the rest of your program or main logic
                    Console.SetCursorPosition(0, 23);
                    Console.WriteLine("System is ready. Welcome to the Point of Sale!");
                    Console.WriteLine("Press any key to continue!");
                    Console.ReadKey();
                    return;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(Constants.ExitApplicationCode);
                }
                else
                {
                    Console.WriteLine("Invalid!");
                    ExecuteFunctions.ClearLines(21, 22, 0);

                }

            } while (keyInfo.Key != ConsoleKey.Enter || keyInfo.Key != ConsoleKey.Escape);



        }


        public static void DisplayLoadingScreen(string[] message, int index, int dotsCount)
        {
            Console.WriteLine($"Please wait: {message[index]} ");
            for (int i = 0; i < dotsCount; i++)
            {
                Console.Write(".");
                Thread.Sleep(100); // Adjust the delay duration as needed

            }

            Console.WriteLine(); // Move to the next line after the loading dots
        }


        #endregion


        #region Display AdminBox
        public static void DisplayAdminBox()
        {
            int startingY = 0;
            int startingX = 0;

            startingY = 15;
            startingX = 5;
            //top
            Console.SetCursorPosition(startingY, startingX);
            for (int i = 0; i < 65; i++)
            {

                Console.Write("═");
            }

            startingX = 5;
            startingY = 14;
            Console.SetCursorPosition(startingY, startingX);
            Console.Write("   POS Control Center");

            //bottom
            startingY = 15;
            startingX = 10;
            Console.SetCursorPosition(startingY, startingX);
            for (int i = 0; i < 65; i++)
            {

                Console.Write("═");
            }

            //left
            startingY = 15;
            startingX = 5;
            Console.SetCursorPosition(startingY, startingX);
            for (int i = 0; i < 5; i++, startingX++)
            {

                Console.SetCursorPosition(startingY, startingX);
                Console.WriteLine("║");
            }


            //right
            startingX = 5;
            startingY = 80;
            for (int i = 0; i < 5; i++, startingX++)
            {

                Console.SetCursorPosition(startingY, startingX);
                Console.WriteLine("║");
            }


            //corners

            Console.SetCursorPosition(15, 5);
            Console.Write("╔");
            Console.SetCursorPosition(15, 10);
            Console.Write("╚");
            Console.SetCursorPosition(80, 5);
            Console.Write("╗");
            Console.SetCursorPosition(80, 10);
            Console.Write("╝");


        }
        #endregion


        #region Display Main Menu
        public static void DisplayMenu(Inventory product)
        {
            if (product.MainMenuEn == true)
            {
                string[] currentMenu = product.CurrentLinesSubMenu == 0 ? product.MainMenu : product.ListMenuEmployee;
                int startingY = 15;

                Console.SetCursorPosition(startingY, 3);
                Console.WriteLine("                    CONSOLE - POS - SYSTEM - C# ");
                Console.ResetColor();

                for (int i = 0; i < 65; i++)
                {
                    Console.SetCursorPosition(startingY + i, 4);
                    Console.Write("═");
                    Console.SetCursorPosition(startingY + i, 6);
                    Console.Write("═");
                    Console.SetCursorPosition(startingY + i, 12);
                    Console.Write("═");
                }

                Console.SetCursorPosition(startingY, 5);
                Console.WriteLine("                M   A   I   N    *    M   E   N   U              ");
                Console.ResetColor();

                Console.SetCursorPosition(startingY, 5);
                for (int k = 0; k < 8; k++)
                {

                    Console.WriteLine("║");
                    Console.SetCursorPosition(startingY, 5 + k);
                    Console.WriteLine("║");
                    Console.SetCursorPosition(startingY + 65, 5 + k);
                }
                Console.ResetColor();

                for (int j = 0; j < product.MainMenu.Length; j++)
                {

                    if (j == product.CurrentLinesMenu)
                    {

                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.SetCursorPosition(startingY + 5, 7 + j); // Set cursor position before writing the "[-]"
                    Console.Write("[-]");
                    Console.WriteLine(product.MainMenu[j]);
                    Console.ResetColor();

                }
                Console.SetCursorPosition(startingY, 4);
                Console.Write("╔");
                Console.SetCursorPosition(startingY, 12);
                Console.Write("╚");
                Console.SetCursorPosition(startingY + 65, 4);
                Console.Write("╗");
                Console.SetCursorPosition(startingY + 65, 12);
                Console.Write("╝");


            }


        }
        #endregion


        #region Display Products Section
        public static void DisplayProducts(Inventory product, LoginAuthentication authentication)
        {



            int startingY = 55; // Starting position for Y-coordinate
            //int totalPages = (int)Math.Ceiling((double)product.ProductNames.Count / 10);
            Console.ForegroundColor = ConsoleColor.White;

            if (authentication.IsAdminLogin == true)
            {
                Console.SetWindowSize(140, 30);
                Console.SetCursorPosition(startingY, 3);
                Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════╗");
                Console.SetCursorPosition(startingY, 4);
                Console.WriteLine("║                 P     R     O     D     U     C     T     S               ║");
                Console.SetCursorPosition(startingY, 5);
                Console.WriteLine("╠════════╤═══════════════════════════════════════╤═══════════╤══════════════╣");
                Console.SetCursorPosition(startingY, 6);
                Console.WriteLine("║   No.  │              Product Name             │   Stock   │    Price     ║");
                Console.SetCursorPosition(startingY, 7);
                Console.WriteLine("╠════════╪═══════════════════════════════════════╪═══════════╪══════════════╣");
                Console.SetCursorPosition(startingY, 8 + 10);
                Console.WriteLine("╠════════╧═══════════════════════════════════════╧═══════════╧══════════════╣");
                Console.SetCursorPosition(startingY, 9 + 10);
                Console.WriteLine($"║ Page: {product.CurrentPage,-2} of {product.PageSize}                                                            ║");
                Console.SetCursorPosition(startingY, 10 + 10);
                Console.WriteLine("║ Press ← and → to navigate, ESC to return to Main Menu.                    ║");
                Console.SetCursorPosition(startingY, 11 + 10);
                Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════╣");
                Console.SetCursorPosition(startingY, 12 + 10);
                Console.WriteLine("║ Press 'A' - Add product     Press 'R' - Remove product                    ║");
                Console.SetCursorPosition(startingY, 13 + 10);
                Console.WriteLine("║ Press 'E' - Edit product    Press 'S' - Search product                    ║");
                Console.SetCursorPosition(startingY, 14 + 10);
                Console.WriteLine("║ Press 'B' - Go back to Default                                            ║");
                Console.SetCursorPosition(startingY, 15 + 10);
                Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════╝");
                Console.WriteLine("");
            }
            //inventory
            else if (product.CurrentLinesMenu == 2)
            {
                Console.SetWindowSize(166, 38);
                Console.SetCursorPosition(startingY, 3);
                Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════╗");
                Console.SetCursorPosition(startingY, 4);
                Console.WriteLine("║                 P     R     O     D     U     C     T     S               ║");
                Console.SetCursorPosition(startingY, 5);
                Console.WriteLine("╠════════╤═══════════════════════════════════════╤═══════════╤══════════════╣");
                Console.SetCursorPosition(startingY, 6);
                Console.WriteLine("║   No.  │              Product Name             │   Stock   │    Price     ║");
                Console.SetCursorPosition(startingY, 7);
                Console.WriteLine("╠════════╪═══════════════════════════════════════╪═══════════╪══════════════╣");
                Console.SetCursorPosition(startingY, 8 + 10);
                Console.WriteLine("╠════════╧═══════════════════════════════════════╧═══════════╧══════════════╣");
                Console.SetCursorPosition(startingY, 9 + 10);
                Console.WriteLine($"║ Page: {product.CurrentPage,-2} of {product.PageSize}                                                            ║");
                Console.SetCursorPosition(startingY, 10 + 10);
                Console.WriteLine("║ Press ← and → to navigate, ESC to return to Main Menu.                    ║");
                Console.SetCursorPosition(startingY, 11 + 10);
                Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════╣");
                Console.SetCursorPosition(startingY, 12 + 10);
                Console.WriteLine("║ Press 'I' - Stock in                                                      ║");
                Console.SetCursorPosition(startingY, 13 + 10);
                Console.WriteLine("║ Press 'S' - Search product        Press 'B' - Go back to Default          ║");
                Console.SetCursorPosition(startingY, 14 + 10);
                Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════╝");
                Console.WriteLine("");
            }
            else if (product.CurrentLinesMenu == 0)
            {
                Console.SetWindowSize(166, 38);
                Console.SetCursorPosition(startingY, 3);
                Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
                Console.SetCursorPosition(startingY, 4);
                Console.WriteLine("║                 P   R   O   D   U   C   T   S                 ║");
                Console.SetCursorPosition(startingY, 5);
                Console.WriteLine("╠═══════════╤══════════════════════════════╤════════════════════╣");
                Console.SetCursorPosition(startingY, 6);
                Console.WriteLine("║    No.    │         Product Name         │        Price       ║");
                Console.SetCursorPosition(startingY, 7);
                Console.WriteLine("╠═══════════╪══════════════════════════════╪════════════════════╣");
                Console.SetCursorPosition(startingY, 8 + 10);
                Console.WriteLine("╠═══════════╧══════════════════════════════╧════════════════════╣");
                Console.SetCursorPosition(startingY, 9 + 10);
                Console.WriteLine($"║ Page: {product.CurrentPage,-2} of {product.PageSize}                                                ║");
                Console.SetCursorPosition(startingY, 10 + 10);
                Console.WriteLine("║ Press ← and → to navigate, ESC to return to Main Menu.        ║");
                Console.SetCursorPosition(startingY, 11 + 10);
                Console.WriteLine("╠═══════════════════════════════════════════════════════════════╣");
                Console.SetCursorPosition(startingY, 12 + 10);
                Console.WriteLine("║ Press 'A' - Add to Cart     Press 'S' - Search product        ║");
                Console.SetCursorPosition(startingY, 13 + 10);
                Console.WriteLine("║ Press 'B' - Go back         Press 'V' - View Cart             ║");
                Console.SetCursorPosition(startingY, 14 + 10);
                Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            }


            int itemsOnPage = 0;
            int startIndex = (product.CurrentPage - 1) * 10;
            if (product.IsSearching == false)
            {
                // Calculate the end index based on the page size
                int endIndex = Math.Min(startIndex + 10, product.ProductNames.Count);

                itemsOnPage = endIndex - startIndex;
            }
            else
            {
                // Calculate the end index based on the page size
                int endIndex = Math.Min(startIndex + 10, product.matchingProducts.Count);

                itemsOnPage = endIndex - startIndex;
            }

            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(startingY, 8 + i);

                if (i < itemsOnPage)
                {
                    int itemIndex = startIndex + i;
                    int itemNumber = itemIndex + 1; // Add 1 to convert from zero-based index to 1-based item number

                    // Check if the current line is the one that needs a background color change
                    if (itemIndex == product.CurrentLinesProducts)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    // Display information for each product in matchingProducts
                    string prod = product.IsSearching ? product.matchingProducts[itemIndex] : product.ProductNames[itemIndex];
                    int index = product.ProductNames.IndexOf(prod);
                    double price = product.Price[index];
                    int quantity = product.Quantity[index];

                    // Update the lines below to remove the background color of side lines
                    if (authentication.IsAdminLogin == true)
                    {
                        Console.Write($"║   {itemNumber,-3}  │     {prod,-34}│     {quantity,-6}│   Php{(price),-8:0.00}║");
                    }
                    else if (product.CurrentLinesMenu == 2)
                    {
                        Console.Write($"║   {itemNumber,-3}  │     {prod,-34}│     {quantity,-6}│   Php{(price),-8:0.00}║");
                    }
                    ///POS
                    else
                    {
                        Console.Write($"║ {itemNumber,5}     │     {prod,-24} │      Php{(price),-6:0.00}     ║");
                    }

                    // Reset colors to default after writing the line
                    Console.ResetColor();
                }
                else
                {
                    // If there are no more items to display, fill the remaining lines with blanks
                    if (authentication.IsAdminLogin == true)
                    {
                        Console.Write("║".PadRight(77));
                    }
                    else if (product.CurrentLinesMenu == 0)
                    {
                        Console.Write("║".PadRight(59));
                    }
                    else
                    {
                        Console.Write("║".PadRight(75));
                    }

                    // Reset colors to default after writing the line
                    Console.ResetColor();
                }
            }
        }
        #endregion


        #region Display Cart Section
        public static void DisplayCartDetails(Inventory inventory)
        {
            inventory.InCart = true;
            Display.DisplayHeader(inventory);
            Console.SetBufferSize(340, 200);
            Console.SetCursorPosition(140, 42);
            int startingY = 50;
            Console.SetCursorPosition(startingY, 3);
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════╗");
            Console.SetCursorPosition(startingY, 4);
            Console.WriteLine("║         C    A    R    T           D    E    T    A    I    L    S         ║");
            Console.SetCursorPosition(startingY, 5);
            Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════╣");
            Console.SetCursorPosition(startingY, 6);
            Console.WriteLine("║ {0,-10} {1,-35} {2,-11} {3,-15} ║", " No.", "   Product Name", "Quantity", "     Price");
            Console.SetCursorPosition(startingY, 7);
            Console.WriteLine("╠═════════╤══════════════════════════════════╤═══════════════╤═══════════════╣");


            for (int i = 0; i < inventory.CartItems.Count; i++)
            {
                var cartItem = inventory.CartItems[i];
                //int itemIndex = inventory.ProductNames.IndexOf(cartItem.productName);

                int startIndex = 0;
                int endIndex = startIndex + inventory.ProductNames.Count;


                int itemIndex = startIndex + i;
                if (endIndex == inventory.CurrentLineCart)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(startingY, i + 8);
                string line = "│";
                Console.Write("{0, -10}", line);
                Console.SetCursorPosition(startingY, i + 8);
                Console.Write("║  {0,-10}  {1, -37} {2,-13} {3,-9} ║",
                i + 1, cartItem.productName, cartItem.quantityInCart, cartItem.price);
            }
            Console.SetCursorPosition(startingY, inventory.CartItems.Count + 8);
            Console.WriteLine("╠═════════╧══════════════════════════════════╧═══════════════╧═══════════════╣");
            Console.SetCursorPosition(startingY, inventory.CartItems.Count + 9);
            Console.WriteLine("║ ESC to return to Main Menu.                                                ║");
            Console.SetCursorPosition(startingY, inventory.CartItems.Count + 10);
            Console.WriteLine("║════════════════════════════════════════════════════════════════════════════║");
            Console.SetCursorPosition(startingY, inventory.CartItems.Count + 11);
            Console.WriteLine("║ Press 'M' - Modify Quantity   Press 'R' - Remove Item                      ║");
            Console.SetCursorPosition(startingY, inventory.CartItems.Count + 12);
            Console.WriteLine("║ Press 'C' - Proceed to Checkout                                            ║");
            Console.SetCursorPosition(startingY, inventory.CartItems.Count + 13);
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════╝");

        }
        public static void DisplayCart(Inventory product)
        {
            if (product.CurrentLinesMenu == 0)
            {


                Console.SetCursorPosition(136, 3);
                Console.WriteLine("ITEMS IN CART");
                Console.SetCursorPosition(126, 4);
                Console.WriteLine("╔══════════════════════════════╗");

                for (int i = product.CartItems.Count - 1; i >= 0; i--)
                {
                    var cartItem = product.CartItems[i];
                    int itemIndex = product.ProductNames.IndexOf(cartItem.productName);
                    if (product.CartItems[i].quantityInCart > 0)
                    {

                        if (i < 9)
                        {
                            Console.SetCursorPosition(126, i + 5);
                            Console.Write($"║ [{i + 1}] ".PadRight(5));
                        }
                        else
                        {
                            Console.SetCursorPosition(124, i + 5);
                            Console.Write($"  ║ [{i + 1}]".PadRight(6));
                        }


                        var item = product.CartItems[i];

                        // Display only the productName and default quantity of 1
                        Console.WriteLine($"║ {item.productName,-10} x 1".PadRight(25) + "║ ");



                    }
                    else
                    {
                        // Remove item from cart
                        product.CartItems.RemoveAt(i);
                    }
                }

                Console.SetCursorPosition(126, product.CartItems.Count + 5);
                Console.WriteLine("╚══════════════════════════════╝");
            }

        }

        #endregion


        #region Display Other/s Section
        public static void DisplayHint()
        {
            Console.SetCursorPosition(3, 3);
            Console.Write("After pressing Options below, \n   prompts appear here.");
        }
        public static void DisplayHeader(Inventory inventory)
        {
            if (Inventory.InAboutUs == false || inventory.IsCheckout == true)
            {
                string bName = "DKDKD";
                Console.ForegroundColor = ConsoleColor.White; // Adjust color as needed
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Welcome to {bName} POS System");

                // Add mode indicator
                Console.SetCursorPosition(Console.WindowWidth - 15, 0);
                ////Console.WriteLine("Mode: " + (inventory.Admin ? "Admin" : "Cashier"));
                if (inventory.Admin == true)
                {
                    Console.WriteLine("Mode: Admin");
                }
                else
                {
                    Console.WriteLine("Mode: Cashier");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.SetCursorPosition(0, 1);
                Console.WriteLine(new string('█', Console.WindowWidth));
                //Task.Run(() => ContinuousDisplay());

               Console.ResetColor();
            }

        }
        //private static void ContinuousDisplay()
        //{
        //    int i = 0;
        //    while (true)
        //    {

        //        string first = "P";
        //        string second = "O";
        //        string third = "S";
        //        Console.ForegroundColor = ConsoleColor.White;
                
        //        Console.SetCursorPosition(0 + i, 1);
        //        Console.Write("{0}{1}{2}", first, second, third);
        //        Thread.Sleep(100);
        //        Console.ResetColor();
        //        ExecuteFunctions.ClearLines(1, 1, 1);

        //        i = (i + 1) % 95; // Wrap around when reaching the end
        //    }
        //}
        #endregion


        #region DisplayReceipt

        public static void DisplayReceipt(Inventory inventory, string cashierName)
        {
            Console.Clear();
            Console.SetBufferSize(340, 200);
            if (inventory.IsCheckout == true || inventory.OnCheckout == true)
            {
                inventory.OnCheckout = true;
                Console.SetWindowSize(68, 38);

                // Constants for shop details
                const string shopName = "Asian College of Technology";
                const string shopAddress = "Pantaleon del Rosario St, Cebu City, 6000 Cebu";
                
                Console.SetCursorPosition(0, 4);
                Console.WriteLine("╔" + new string('═', 66) + "╗");
                Thread.Sleep(50);
                Console.SetCursorPosition(0, 5);
                Console.WriteLine("                                                                    ");
                Thread.Sleep(50);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(14, 6);
                Console.WriteLine("██████╗ ██╗  ██╗██████╗ ██╗  ██╗██████╗");
                Thread.Sleep(50);
                Console.SetCursorPosition(14, 7);
                Console.WriteLine("██╔══██╗██║ ██╔╝██╔══██╗██║ ██╔╝██╔══██╗");
                Thread.Sleep(50);
                Console.SetCursorPosition(14, 8);
                Console.WriteLine("██║  ██║█████╔╝ ██║  ██║█████╔╝ ██║  ██║");
                Thread.Sleep(50);
                Console.SetCursorPosition(14, 9);
                Console.WriteLine("██║  ██║██╔═██╗ ██║  ██║██╔═██╗ ██║  ██║");
                Thread.Sleep(50);
                Console.SetCursorPosition(14, 10);
                Console.WriteLine("██████╔╝██║  ██╗██████╔╝██║  ██╗██████╔╝");
                Thread.Sleep(50);
                Console.SetCursorPosition(14, 11);
                Console.WriteLine("╚═════╝ ╚═╝  ╚═╝╚═════╝ ╚═╝  ╚═╝╚═════╝");
                Thread.Sleep(50);
                Console.ResetColor();
                for (int i = 0; i < 7; i++)
                {
                    Console.SetCursorPosition(0, 5 + i);
                    Console.WriteLine("║");
                    Console.SetCursorPosition(67, 5 + i);
                    Console.WriteLine("║");
                    Thread.Sleep(50);
                }


                Console.SetCursorPosition(0, 12);
                Console.WriteLine($"║                   {shopName,-47}║");
                Thread.Sleep(50);
                Console.SetCursorPosition(0, 13);
                Console.WriteLine($"║           {shopAddress,-51}    ║");
                Thread.Sleep(50);
                Console.SetCursorPosition(0, 14);
                Console.WriteLine("║" + "╔" + new string('═', 64) + "╗" + "║");
                Thread.Sleep(50);

                if (inventory.CartItems.Count > 0)
                {
                    Console.SetCursorPosition(3, 15);
                    Console.Write("Product");
                    Thread.Sleep(50);
                    Console.SetCursorPosition(46, 15);
                    Console.Write("  Price    SubTotal  ║");
                    Thread.Sleep(50);
                }

                Console.SetCursorPosition(0, 16);
                double total = 0;
                for (int y = 0; y < 13 + inventory.CartItems.Count; y++)
                {
                    Console.SetCursorPosition(0, 15 + y);
                    Console.WriteLine("║");
                    Console.SetCursorPosition(66, 15 + y);
                    Console.WriteLine("║");
                    Console.SetCursorPosition(1, 15 + y);
                    Console.WriteLine("║");
                    Console.SetCursorPosition(67, 15 + y);
                    Console.WriteLine("║");
                    Thread.Sleep(50);
                }
                for (int i = 0; i < inventory.CartItems.Count; i++)
                {
                    Console.SetCursorPosition(1, 16 + i);
                    var item = inventory.CartItems[i];
                    double itemTotal = item.quantityInCart * inventory.Price[i]; // Assuming Price is a list corresponding to each item
                    total += itemTotal;

                    Console.WriteLine($"║ {item.quantityInCart} x {item.productName,-38}  P{inventory.Price[i],-10:0.00}P{itemTotal,4:0.00}");
                    Thread.Sleep(50);
                }

                Console.SetCursorPosition(2, inventory.CartItems.Count + 16);
                Console.WriteLine(new string('═', 64));
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 17);
                Console.WriteLine($"| Subtotal: {total,50:0.00} |");
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 18);
                Console.WriteLine($"| VATABLE Sales: {total * 0.12,45:0.00} |");
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 19);
                Console.WriteLine($"| VAT EXEMPT Sales: {0,42:0.00} |"); // Placeholder, replace with actual value
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 20);
                Console.WriteLine($"| ZERO RATED Sales: {0,42:0.00} |"); // Placeholder, replace with actual value
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 21);
                Console.WriteLine($"| VAT Amount: {total * 0.12,48:0.00} |");
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 22);
                Console.WriteLine($"| Total: {total * 1.12,53:0.00} |");
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 23);

                // Additional detail
                Console.WriteLine(new string('═', 64));
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 24);
                Console.WriteLine($"| Cashier: {cashierName,-52}|");
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 25);
                Console.WriteLine($"| Date Issued: {DateTime.Now,-48:yyyy-MM-dd HH:mm:ss}|");
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 26);
                Console.WriteLine($"| Valid Until: {DateTime.Now.AddHours(1),-48:yyyy-MM-dd HH:mm:ss}|");
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 27);
                Console.WriteLine(new string('═', 64));
                Thread.Sleep(50);
                Console.SetCursorPosition(2, inventory.CartItems.Count + 28);
            }
        }



        static string GenerateReceiptNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        #endregion



        #region Display About Us

        public static void StopDisplay()
        {
            cancellationTokenSource?.Cancel();
        }
        public static void DisplayAnimationAbout(CancellationTokenSource cancellationToken)
        {
            ConsoleKeyInfo keyInfo;
            Console.ForegroundColor = ConsoleColor.Magenta;

            string[] dAbout = new string[]
            {
            " █████  ██████   ██████  ██    ██ ████████     ██    ██ ███████",
            "██   ██ ██   ██ ██    ██ ██    ██    ██        ██    ██ ██     ",
            "███████ ██████  ██    ██ ██    ██    ██        ██    ██ ███████",
            "██   ██ ██   ██ ██    ██ ██    ██    ██        ██    ██      ██",
            "██   ██ ██████   ██████   ██████     ██         ██████  ███████"
            };

            int i = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.SetCursorPosition(0 + i, 0);
                Console.Write(dAbout[0]);
                Console.SetCursorPosition(0 + i, 1);
                Console.Write(dAbout[1]);
                Console.SetCursorPosition(0 + i, 2);
                Console.Write(dAbout[2]);
                Console.SetCursorPosition(0 + i, 3);
                Console.Write(dAbout[3]);
                Console.SetCursorPosition(0 + i, 4);
                Console.Write(dAbout[4]);
                Thread.Sleep(100);
                ExecuteFunctions.ClearLines(0, 4, 0);

                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        StopDisplay();
                        break; // Exit the loop if 'Escape' key is pressed
                        
                    }
                }

                i = (i + 1) % 95; // Adjusted to prevent i from going beyond 95
            }
        }

        public static void DisplayAboutUs()
        {
            Inventory.InAboutUs = true;

            Console.ResetColor();
            Console.WriteLine("=============================================");
            Console.WriteLine("\n\n\n\n\n\n\nWelcome to our Point of Sale (POS) System!");
            Console.WriteLine("This system is developed by the following individuals:");

            // Display developer information and contributions
            DisplayDeveloperInformation("Dime Renz Apor", "LEAD DEVELOPER / DEBUGGER / CODER");
            DisplayDeveloperInformation("Keisha Lyn Tampus", "PROJECT MANAGER / DEBUGGER / CODER");
            DisplayDeveloperInformation("Drew Xanarie Baroro", "BACKEND DEVELOPER / DEBUGGER / CODER\t");
            DisplayDeveloperInformation("Ken Marande", "FRONTENT DEVELOPER / LAYOUT DESIGN / CODER");
            DisplayDeveloperInformation("Daryl Generalao", "TESTER / QUALITY ASSURANCE  / CODER");

            Console.WriteLine("\nThank you for using our POS System!");
            Console.WriteLine("Press 'ESC' to return to the Main Menu...");
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => DisplayAnimationAbout(cancellationTokenSource));
        }

        private static void DisplayDeveloperInformation(string developerName, string role)
            {
                Console.WriteLine($"- {developerName}: {role}");
            }
            #endregion
        }

    }

