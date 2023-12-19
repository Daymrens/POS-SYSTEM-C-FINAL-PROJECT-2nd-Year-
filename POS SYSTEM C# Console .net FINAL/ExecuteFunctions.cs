using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using POS___POS;
using System.CodeDom.Compiler;
using System.Security.Claims;
using static DisplaMenu_Test.Program;
using System.Net.Http.Headers;

namespace DisplaMenu_Test
{
    public class ExecuteFunctions : Inventory
    {
        private Inventory inventory;
        private LoginAuthentication authentication;
        public int selectedNumber = 0;
        public int outNumberforQuantity;
        int indexProd;
        public int index;
        public int IndexInCart;
        int outSelectedItemCart;
        

        public ExecuteFunctions(Inventory inventory, LoginAuthentication authentication)
        {
            this.inventory = inventory;
            this.authentication = authentication;
        }

        #region ADD
        public void ProcessAddItem(int quantityy)
        {
            int selectedIndex = inventory.CurrentLinesProducts;
            string prod = inventory.IsSearching ? inventory.matchingProducts[selectedIndex] : inventory.ProductNames[selectedIndex];
            int index = inventory.ProductNames.IndexOf(prod);
            double price = inventory.Price[index];


            if (IsValidIndex(selectedIndex))
            {
                double selectedQuantity = inventory.Quantity[selectedIndex];

                if (quantityy <= selectedQuantity)
                {
                    if (inventory.IsSearching == false)
                    {
                        AddToCart(inventory.ProductNames[index], quantityy, price);
                    }
                    else
                    {
                        AddToCart(inventory.matchingProducts[selectedIndex], quantityy, price);
                    }

                }
                else
                {
                    Console.WriteLine("Insufficient quantity!");
                    Console.SetCursorPosition(3, 14);
                    Console.WriteLine("Press any key to continue.... or wait 5 sec.");
                    Wait();
                }
            }
            else
            {
                Console.WriteLine("Invalid item selection!");
                Console.SetCursorPosition(3, 14);
                Console.WriteLine("Press any key to continue.... or wait 5 sec.");
                Wait();
            }

        }
        public void AddItems(Inventory inventory, LoginAuthentication authentication)
        {
            int startingY = 3;
            if (inventory.Admin)
            {

                Console.SetCursorPosition(startingY, 3);
                Console.WriteLine("Add Items");
                Console.SetCursorPosition(startingY, 4);
                Console.Write("Enter Procuct Name: ");
                string prodName = Console.ReadLine();

                Console.SetCursorPosition(startingY, 5);
                Console.Write("Price: ");
                int price;
                while (!int.TryParse(Console.ReadLine(), out price))
                {
                    Console.SetCursorPosition(startingY, 6);
                    Console.WriteLine("Invalid input. \n   Please enter a valid price.");
                    Thread.Sleep(1000);
                    ClearLines(5, 7, startingY);
                    Display.DisplayProducts(inventory, authentication);
                    Console.SetCursorPosition(startingY, 5);
                    Console.Write("Price: ");
                }

                // Call the method to add the new item to the inventory
                int newQuant = 1;
                AddNewItemstoMenu(prodName, newQuant, price);
                Console.SetCursorPosition(startingY, 6);
                Console.WriteLine($"Item '{prodName}' added to inventory.");
                Thread.Sleep(800);
                ClearLines(3, 8, startingY);
                Display.DisplayProducts(inventory, authentication);

                inventory.PageSize = 11;
            }
            else
            {

                Console.SetCursorPosition(3, 3);
                Console.WriteLine("Add Items to Cart");
                // Implement logic to add items                
                Console.SetCursorPosition(3, 3);
                ProcessAddItem(1);
                Display.DisplayCart(inventory);
                Console.SetCursorPosition(3, 4);
                Thread.Sleep(100);
                if (inventory.CartItems.Count != 0)
                {
                    Console.WriteLine("Item added!");
                }

                Thread.Sleep(100);
                ClearLines(3, 4, 0);
                Display.DisplayProducts(inventory, authentication);
            }
        }
        public void AddNewItemstoMenu(string prodName, int newQuant, int price)
        {
            inventory.ProductNames.Add(prodName);
            inventory.Quantity.Add(newQuant);
            inventory.Price.Add(price);
        }
        public void AddToCart(string productName, int quantityy, double price)
        {
            indexProd = inventory.ProductNames.IndexOf(productName);
            if (indexProd != -1 && quantityy <= this.inventory.Quantity[indexProd])
            {
                this.inventory.Quantity[indexProd] -= quantityy;

                if (inventory.CartItems == null)
                {
                    inventory.CartItems = new List<(string, int, double price)>();
                }

                var cartItem = inventory.CartItems.FirstOrDefault(item => item.productName == productName);
                if (cartItem != default)
                {
                    inventory.CartItems.Remove(cartItem);
                    cartItem.quantityInCart += quantityy;
                    inventory.CartItems.Add(cartItem);
                }
                else
                {
                    inventory.CartItems.Add((productName, quantityy, price));
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please try again.");
            }
        }

        #endregion


        #region SEARCH
        public void SearchProductsByFirstLetter()
        {
            inventory.IsSearching = true;
            int startingY = 3;
            int selectedIndex = inventory.CurrentLinesProducts;
            Console.SetCursorPosition(startingY, 3);
            Console.WriteLine("Search Products by First Letter");
            Console.SetCursorPosition(startingY, 4);
            Console.Write("Enter the first letter: ");

            char searchLetter;
            if (char.TryParse(Console.ReadLine()?.ToUpper(), out searchLetter))
            {
                inventory.matchingProducts = inventory.ProductNames
                    .Where(product => product.ToUpper().StartsWith(searchLetter.ToString()))
                    .ToList();
                
                // Populate the list with details of matching products
                foreach (var productName in matchingProducts)
                {
                    int index = inventory.ProductNames.IndexOf(productName);
                    double price = inventory.Price[index];
                    int quantity = inventory.Quantity[index];

                    inventory.matchingProductsDetails.Add((productName, quantity, price));
                }

               
                foreach(var prod in matchingProducts )
                {
                     index = inventory.ProductNames.IndexOf(prod);
                }

                

                if (inventory.matchingProducts.Any())
                {
                    // Handle the case where there are matching products
                    // You might want to display the matching products or perform additional actions.
                    Console.SetCursorPosition(startingY, 5);
                    Console.WriteLine("Matching products found!");
                    // ... (additional actions)
                }
                else
                {
                    // Handle the case where there are no matching products
                    Console.SetCursorPosition(startingY, 5);
                    Console.WriteLine("No matching products found.");
                    inventory.IsSearching = false;
                }
            }
            else
            {
                inventory.IsSearching = false;
                Console.WriteLine("Invalid input. Please enter a valid letter.");
            }
            Thread.Sleep(1000);
            ClearLines(3, 5, 0);
            inventory.CurrentPage = 1;
            inventory.CurrentLinesProducts = 0;
            Display.DisplayProducts(inventory, authentication);

        }
        #endregion


        #region EDIT
        public void EditItems()
        {
            int startingY = 3;
            ConsoleKeyInfo keyInfo;

            do
            {
                Display.DisplayProducts(inventory, authentication);
                Console.SetCursorPosition(startingY, 3);
                Console.WriteLine("Edit Product");

                int selectedIndex = inventory.CurrentLinesProducts;

                bool isValidChoice = false;

                do
                {
                    Display.DisplayProducts(inventory, authentication);
                    Console.SetCursorPosition(startingY, 4);
                    Console.Write("Choose what to edit \n  1. Product Name \n  2. Price\n: ");
                    string editOption = Console.ReadLine();

                    if (int.TryParse(editOption, out int choice) && choice >= 1 && choice <= 2)
                    {
                        // Valid choice
                        isValidChoice = true;

                        switch (choice)
                        {
                            case 1:
                                // Code for editing product name
                                Console.SetCursorPosition(startingY, 8);
                                Console.Write("Enter new Product Name:\n  - ");
                                string newProductName = Console.ReadLine();

                                // Update the product name
                                if (IsValidIndex(selectedIndex))
                                {
                                    if (inventory.IsSearching == false)
                                    {
                                        inventory.ProductNames[selectedIndex] = newProductName;
                                    }
                                    else
                                    {
                                        inventory.matchingProducts[selectedIndex] = newProductName;
                                    }

                                    Console.SetCursorPosition(startingY, 10);
                                    Console.WriteLine("Product Name updated successfully.");
                                    Thread.Sleep(1000);
                                }
                                else
                                {
                                    Console.SetCursorPosition(startingY, 6);
                                    Console.WriteLine("Invalid index. Cannot update Product Name.");
                                    Thread.Sleep(1000);
                                }
                                break;

                            case 2:
                                // Code for editing price
                                do
                                {
                                    Console.SetCursorPosition(startingY, 8);
                                    Console.Write("Enter new Price: \n  -");
                                    if (double.TryParse(Console.ReadLine(), out double newPrice) && newPrice >= 0)
                                    {
                                        // Update the price
                                        if (IsValidIndex(selectedIndex))
                                        {
                                            if (inventory.IsSearching == false)
                                            {
                                                inventory.Price[selectedIndex] = newPrice;
                                            }
                                            else
                                            {
                                                inventory.Price[selectedIndex] = newPrice;
                                            }
                                            Console.SetCursorPosition(startingY, 10);
                                            Console.WriteLine("Price updated successfully.");
                                            Thread.Sleep(1000);
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(startingY, 6);
                                            Console.WriteLine("Invalid index. Cannot update Price.");
                                            Thread.Sleep(1000);
                                        }
                                        break; // Exit the price loop
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(startingY, 9);
                                        Console.WriteLine("Invalid price. \n   Please enter a non-negative decimal value.");
                                        Thread.Sleep(1000);
                                        ClearLines(8, 10, startingY);
                                        Display.DisplayProducts(inventory, authentication);
                                    }
                                } while (true);
                                break;
                        }
                    }
                    else
                    {
                        // Invalid choice
                        Console.SetCursorPosition(startingY, 8);
                        Console.WriteLine("Invalid choice. \n   Please enter a valid option (1-2).");
                        Thread.Sleep(1000);
                    }

                    // Clear the input line
                    ClearLines(8, 9, startingY);

                } while (!isValidChoice);
                ClearLines(3, 11, 0);
                Display.DisplayProducts(inventory, authentication);
                Console.SetCursorPosition(startingY, 3);
                Console.WriteLine("Press ESC to exit or\n   any other key to continue editing.");
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    ClearLines(3, 4, 0);
                    return; // Exit the method
                }
                ClearLines(3, 4, 0);

            } while (true);
        }

        #endregion


        #region MODIFY
        public void ModifyQuantity(Inventory inventory)
        {
            int startingY = 3;
            Console.SetCursorPosition(startingY, 3);
            Console.Write("Enter the item number to modify quantity:");

            if (int.TryParse(Console.ReadLine(), out selectedNumber) && selectedNumber >= 1 && selectedNumber <= inventory.CartItems.Count)
            {
                var selectedItem = inventory.CartItems[selectedNumber - 1]; // Adjust index
                int originalQuantity = selectedItem.quantityInCart; // Save the original quantity

                Console.SetCursorPosition(startingY, 4);
                Console.WriteLine($"Current quantity for item {selectedNumber}: {originalQuantity}");
                Console.SetCursorPosition(startingY, 5);
                Console.Write("Enter the new quantity:");

                if (int.TryParse(Console.ReadLine(), out int newQuantity) && newQuantity >= 0)
                {

                    // Modify the quantity in the cart
                    selectedItem.quantityInCart = newQuantity;

                    // Update the item in the inventory.CartItems list
                    inventory.CartItems[selectedNumber - 1] = selectedItem;

                    // Decrease the quantity in the inventory
                    string productName = selectedItem.productName; // Assuming there's a productName property in your CartItems
                    DecreaseInventoryQuantity(inventory, productName, newQuantity);

                    Console.SetCursorPosition(startingY, 6);
                    Console.WriteLine($"Quantity for item {selectedNumber} modified successfully \n   from {originalQuantity} to {newQuantity}!");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.SetCursorPosition(startingY, 6); // Adjust line number
                    Console.WriteLine("Invalid quantity. Please enter a non-negative integer.");
                }
            }
            else
            {
                Console.SetCursorPosition(startingY, 5); // Adjust line number
                Console.WriteLine("Invalid item number. Please enter a valid item number.");
            }
            ClearLines(3, 7, startingY);
        }

        private void DecreaseInventoryQuantity(Inventory inventory, string productName, int quantityToDecrease)
        {
            // Find the index of the product in the inventory based on its name
            int productIndex = inventory.ProductNames.FindIndex(name => name.Equals(productName));

            if (productIndex != -1)
            {

                // Decrease the quantity in the inventory
                inventory.Quantity[productIndex] -= quantityToDecrease;
            }
        }

        #endregion


        #region STOCK IN

        public void StockIn()
        {
            int startingY = 3;
            int selectedIndex = 0;
            Display.DisplayProducts(inventory, authentication);
            Console.SetCursorPosition(startingY, 3);
            Console.WriteLine("Stock In Quantity");

            selectedIndex = inventory.CurrentLinesProducts;

            // Validate the current lines products
            if (IsValidIndex(selectedIndex))
            {
                Display.DisplayProducts(inventory, authentication);
                Console.SetCursorPosition(startingY, 4);

                string itemName = inventory.IsSearching
                    ? inventory.matchingProducts[selectedIndex]
                    : inventory.ProductNames[selectedIndex];

                Console.Write($"Enter quantity to stock in for {itemName}: ");

                if (int.TryParse(Console.ReadLine(), out int quantityToStock) && quantityToStock > 0)
                {
                    if (inventory.IsSearching)
                    {
                        StockInQuantity(itemName, quantityToStock, selectedIndex);
                    }
                    else
                    {
                        StockInQuantity(itemName, quantityToStock);
                    }

                    Display.DisplayProducts(inventory, authentication);
                }
                else
                {
                    Console.SetCursorPosition(startingY, 5);
                    Console.WriteLine("Invalid quantity. \n   Please enter a positive integer.");
                    Thread.Sleep(1000);
                    
                }
            }
            else
            {
                Console.SetCursorPosition(startingY, 5);
                Console.WriteLine("Invalid item index for stock in.");
                Thread.Sleep(1000);
                
            }
            ClearLines(3, 6, 0);
        }

        private void StockInQuantity(string itemName, int quantityToStock, int selectedIndex = -1)
        {
            // Implement the logic to update the inventory for the specified item and quantity
            // For example, you might have something like:
            if (inventory.IsSearching && selectedIndex != -1)
            {
                int originalIndex = inventory.ProductNames.IndexOf(itemName);
                // Check if the item is found in the original list
                if (originalIndex != -1)
                {
                    // Update the quantity at the original index
                    inventory.Quantity[originalIndex] += quantityToStock;
                }
            }
            else
            {
                // Update the quantity for the specified item
                int itemIndex = inventory.ProductNames.IndexOf(itemName);
                if (itemIndex != -1)
                {
                    inventory.Quantity[itemIndex] += quantityToStock;
                }
            }
        }

        #endregion


        #region FIND
        public void FindProduct(Inventory inventory)
        {
            string fName = inventory.ProductNames[selectedNumber - 1];
            foreach (string prod in inventory.ProductNames)
            {

                if (prod.Contains(fName))
                {
                    outNumberforQuantity = prod.IndexOf(fName);
                }
            }

            string findedName = inventory.ProductNames[selectedNumber - 1];
            foreach (var prod in productNamesInCart)
            {
                if (prod.Contains(findedName))
                {
                    outSelectedItemCart = prod.IndexOf(findedName);
                }
            }
        }

        public void AfterFindDecre(int newQuantity)
        {
            inventory.Quantity[outSelectedItemCart] -= newQuantity;
        }

        public void ReturnIndexProducts()
        {

            foreach (var product in inventory.matchingProducts)
            {
                string productName = product;

                // Find the index of the product name in the list
                inventory.IndexProdNames = inventory.ProductNames.IndexOf(productName);
            }


        }

        #endregion


        #region REMOVE
        public void RemoveItemCart(Inventory inventory)
        {

            Console.SetCursorPosition(3, 3);
            Console.Write("Enter the item number to remove:");
            int itemNumber;
            if (int.TryParse(Console.ReadLine(), out itemNumber) && itemNumber >= 1 && itemNumber <= inventory.CartItems.Count)
            {
                inventory.CartItems.RemoveAt(itemNumber - 1);
                Console.WriteLine("Item removed from the cart!");
            }
            else
            {
                Console.WriteLine("Invalid item number. \nPlease enter a valid item number.");
                Wait();
            }
            ClearLines(3, 6, 0);
        }
        public void ProcessRemoveItems()
        {
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("Remove Items");

            int itemIndexToRemove = inventory.CurrentLinesProducts;

            // Ensure the index is valid
            if (!IsValidIndex(itemIndexToRemove))
            {
                Console.SetCursorPosition(3, 4);
                Console.WriteLine("Invalid item index.");
                Thread.Sleep(1000);
                ClearLines(3, 4, 0);
                return;
            }

            // Now, proceed with the removal logic...
            string itemName = "";
            int itemQuantity;
            double itemPrice;
            // Display item details for confirmation
            if (inventory.IsSearching == false)
            {
                itemName = inventory.ProductNames[itemIndexToRemove];
            }
            else
            {
                itemName = inventory.matchingProducts[itemIndexToRemove];
            }

            itemQuantity = inventory.Quantity[itemIndexToRemove];
            itemPrice = inventory.Price[itemIndexToRemove];

            Console.SetCursorPosition(3, 4);
            Console.WriteLine($"Are you sure you want to remove {itemName}?");
            Console.SetCursorPosition(3, 5);
            Console.Write("Press Y to confirm or N to cancel: ");

            ConsoleKeyInfo confirmationKey = Console.ReadKey();

            if (confirmationKey.Key == ConsoleKey.Y)
            {
                // Remove the item from the inventory
                if (inventory.IsSearching == false)
                {
                    inventory.ProductNames.RemoveAt(itemIndexToRemove);
                    inventory.Quantity.RemoveAt(itemIndexToRemove);
                    inventory.Price.RemoveAt(itemIndexToRemove);
                }
                else
                {
                    inventory.matchingProducts.RemoveAt(itemIndexToRemove);
                    inventory.Quantity.RemoveAt(itemIndexToRemove);
                    inventory.Price.RemoveAt(itemIndexToRemove);
                }


                Console.SetCursorPosition(3, 6);
                Console.WriteLine("Item removed successfully.");
                Thread.Sleep(1000);
            }
            else
            {
                Console.SetCursorPosition(3, 6);
                Console.WriteLine("Item removal canceled.");
                Thread.Sleep(1000);
            }
            ClearLines(3, 6, 0);
        }
        #endregion


        #region OTHER
        static void Wait()
        {

            for (int i = 0; i < 5; i++)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(intercept: true);
                    return;
                }

                if (i == 4)
                {
                    return;
                }
                Thread.Sleep(1000);
            }
        }
        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < inventory.ProductNames.Count;
        }        
        public static void ClearLines(int startLine, int endLine, int leftPosition)
        {
            for (int i = startLine; i <= endLine; i++)
            {
                Console.SetCursorPosition(leftPosition, i);
                Console.Write(new string(' ', Console.WindowWidth - leftPosition));
            }
        }
        public static string GetMaskedUserInput(string prompt)
        {
            Console.Write(prompt);

            StringBuilder passwordBuilder = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (!char.IsControl(key.KeyChar))
                {
                    passwordBuilder.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && passwordBuilder.Length > 0)
                {
                    passwordBuilder.Remove(passwordBuilder.Length - 1, 1);
                    Console.Write("\b \b");
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after entering the password

            return passwordBuilder.ToString();
        }
        #endregion


        #region COLOR
        public static void SetConsoleBackgroundColor(int red, int green, int blue)
        {
            // Find the closest ConsoleColor based on RGB components
            ConsoleColor closestColorBackground = FindClosestConsoleColor(red, green, blue);


            // Set the console background color
            Console.BackgroundColor = closestColorBackground;

        }
        public static void SetConsoleForegroundColor(int red, int green, int blue)
        {
            ConsoleColor closestColorForeground = FindClosestConsoleColor(red, green, blue);
            Console.ForegroundColor = closestColorForeground;
        }
        static ConsoleColor FindClosestConsoleColor(int red, int green, int blue)
        {
            // Simple mapping based on the provided RGB values
            // Adjust this based on your preferences and needs

            // You might need more sophisticated logic for accurate color mapping
            // This is just a basic example
            if (red > 128)
            {
                if (green > 128)
                {
                    if (blue > 128)
                        return ConsoleColor.White;
                    else
                        return ConsoleColor.Yellow;
                }
                else
                {
                    if (blue > 128)
                        return ConsoleColor.Magenta;
                    else
                        return ConsoleColor.Red;
                }
            }
            else
            {
                if (green > 128)
                {
                    if (blue > 128)
                        return ConsoleColor.Cyan;
                    else
                        return ConsoleColor.Green;
                }
                else
                {
                    if (blue > 128)
                        return ConsoleColor.Blue;
                    else
                        return ConsoleColor.Black;
                }
            }
        }
        #endregion


        #region NAVIGATION

        #region MENU
        public void MoveUpMenu()
        {
            if (inventory.CurrentLinesMenu > 0)
            {

                inventory.CurrentLinesMenu--;
            }
        }
        public void MoveDownMenu()
        {
            if (inventory.CurrentLinesMenu < inventory.MainMenu.Length)
            {
                inventory.CurrentLinesMenu++;
            }

        }
        #endregion

        #region SUBMENU
        public void MoveUp()
        {
            if (inventory.InSubMenu && inventory.CurrentLinesSubMenu > 0)
            {
                inventory.CurrentLinesSubMenu--;
            }
            else if (inventory.CurrentLinesProducts > 0)
            {
                inventory.CurrentLinesProducts--;

                if (inventory.CurrentLinesProducts < 0)
                {
                    inventory.CurrentLinesProducts = 0;
                }
            }
        }
        public void MoveDown()
        {
            if (inventory.IsSearching == false)
            {
                if (inventory.InSubMenu)
                {
                    if (inventory.Admin && inventory.CurrentLinesSubMenu < inventory.ListMenuAdmin.Length - 1)
                    {
                        inventory.CurrentLinesSubMenu++;
                    }
                    else if (!inventory.Admin && inventory.CurrentLinesSubMenu < inventory.ListMenuEmployee.Length - 1)
                    {
                        inventory.CurrentLinesSubMenu++;
                    }
                }
                else
                {
                    if (inventory.CurrentLinesProducts < inventory.ProductNames.Count - 1)
                    {
                        inventory.CurrentLinesProducts++;
                    }
                }
            }
            else
            {
                if (inventory.CurrentLinesProducts < inventory.matchingProducts.Count - 1)
                {
                    inventory.CurrentLinesProducts++;
                }
            }

          

        }        
        public void NextPageSubMenu()
        {
           
                inventory.CurrentPage = Math.Min(inventory.CurrentPage + 1, inventory.PageSize);
            
           
        }
        public void PrevPageSubMenu()
        {
            inventory.CurrentPage = Math.Max(inventory.CurrentPage - 1, 1);
        }
        #endregion

        #region CART
        public void NextPageCart()
        {
            inventory.CurrentPage = Math.Min(inventory.CurrentPage + 1, inventory.PageSize);
        }
        public void PrevPageCart()
        {
            inventory.CurrentPage = Math.Max(inventory.CurrentPage - 1, 1);
        }


        #endregion

        #endregion


        #region MAINMENU PROCESS
        #region MAINMENU
        public void MainMenuProcess(Inventory inventory)
        {

            ConsoleKeyInfo keyInfo;

            do
            {
                Display.DisplayHeader(inventory);
                Display.DisplayMenu(inventory);
                

                keyInfo = Console.ReadKey();

                if (inventory.MainMenuEn)
                {

                    SelectionMainMenu(keyInfo);
                }
                else
                {
                    SelectionSubMenu(keyInfo);
                }

            } while (keyInfo.Key != ConsoleKey.Tab);
        }

        private void SelectionMainMenu(ConsoleKeyInfo keyInfo)
        {
            inventory.IsCheckout = false;
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveUpMenu();
                    

                    break;
                case ConsoleKey.DownArrow:
                  MoveDownMenu();
                    if (inventory.CurrentLinesMenu == 5)
                    {
                        inventory.CurrentLinesMenu = 0;
                    }

                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    if (inventory.CurrentLinesMenu != Constants.ExitMenuOption)
                    {

                        inventory.IsSearching = false;
                        inventory.MainMenuEn = false;
                        if (inventory.CurrentLinesMenu != Constants.ProductManagementMenuOption)
                        {
                            Console.Clear();
                        }
                       

                    }
                    ProcessSelectionMainMenu();

                    break;
            }
        }

        private void ProcessSelectionMainMenu()
        {
            switch (inventory.CurrentLinesMenu)
            {
                case Constants.POSMenuOption:
                    authentication.IsAdminLogin = false;
                    inventory.Admin = false;
                    if (inventory.Admin == false && inventory.Logout == true)
                    {
                        authentication.Logout();
                    }
                    inventory.Logout = false;

                    Display.DisplayProducts(inventory, authentication);
                    Display.DisplayCart(inventory);
                    Display.DisplayHeader(inventory);
                    Display.DisplayHint();
                    break;
                case Constants.ProductManagementMenuOption:
                    Display.DisplayHeader(inventory);
                    if (inventory.Admin == false)
                    {
                        Display.DisplayAdminBox();
                        Console.SetCursorPosition(23, 7);
                        Console.WriteLine("   W e l c o m e,  A d m i n i s t r a t o r !   ");
                        AdminLogin();
                        Display.DisplayHeader(inventory);
                        Display.DisplayProducts(inventory, authentication);
                        inventory.Admin = true;
                        
                    }
                    else
                    {
                        authentication.cAdmin();
                        Display.DisplayHeader(inventory);
                        Display.DisplayProducts(inventory, authentication);
                    }
                    break;

                case Constants.AboutUs:
                    Display.DisplayAboutUs();
                    break;
                case Constants.InventoryMenuOption:
                    Display.DisplayProducts(inventory, authentication);
                    break;
               
                case Constants.ExitMenuOption:
                    inventory.Admin = false;
                    inventory.InSubMenu = false;
                    Console.Clear();
                    inventory.MainMenuEn = true;

                    Console.SetCursorPosition(50, 12);
                    Console.WriteLine("Thank you for using our system!");
                    Environment.Exit(Constants.ExitApplicationCode);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }


        private void AdminLogin()
        {
            do
            {
                Display.DisplayAdminBox();
                Console.SetCursorPosition(17, 12);
                authentication.Password = ExecuteFunctions.GetMaskedUserInput("Enter Administrator PIN: ");
                ClearLines(2, 2, 12);
            } while (!authentication.Authenticate(inventory));

            inventory.Admin = true;
        }
        #endregion
        #endregion


        #region SELECTION PRODUCTS
        private void SelectionSubMenu(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (inventory.CurrentLinesMenu != 3 && inventory.IsCheckout == false)
                    {
                        if (inventory.CurrentPage == 1 && inventory.CurrentLinesProducts > 0)
                        {
                            MoveUp();
                            Display.DisplayProducts(inventory, authentication);
                        }
                        else if (inventory.CurrentPage > 1)
                        {
                            MoveUp();
                            if (inventory.CurrentLinesProducts == 9)
                            {

                                MoveUp();
                                inventory.CurrentLinesProducts = 19; // Set to the last index
                            }
                            else if (inventory.CurrentLinesProducts == 19)
                            {
                                MoveUp();
                                inventory.CurrentLinesProducts = 29; // Set to the last index
                            }
                            else if (inventory.CurrentLinesProducts == 29)
                            {
                                MoveUp();
                                inventory.CurrentLinesProducts = 39; // Set to the last index

                            }
                            else if (inventory.CurrentLinesProducts == 39)
                            {
                                MoveUp();
                                inventory.CurrentLinesProducts = 49; // Set to the last index
                            }
                            else if (inventory.CurrentLinesProducts == 49)
                            {
                                MoveUp();
                                inventory.CurrentLinesProducts = 59; // Set to the last index
                            }
                            else if (inventory.CurrentLinesProducts == 59)
                            {
                                MoveUp();
                                inventory.CurrentLinesProducts = 69; // Set to the last index
                            }
                            else if (inventory.CurrentLinesProducts == 69)
                            {
                                MoveUp();
                                inventory.CurrentLinesProducts = 79; // Set to the last index
                            }
                            else if (inventory.CurrentLinesProducts == 79)
                            {
                                MoveUp();
                                inventory.CurrentLinesProducts = 89; // Set to the last index
                            }
                            else if (inventory.CurrentLinesProducts == 89)
                            {
                                MoveUp();
                                inventory.CurrentLinesProducts = 99; // Set to the last index
                            }
                            else if (inventory.CurrentPage == 10 && inventory.CurrentLinesProducts > 99)
                            {
                                inventory.CurrentLinesProducts = 99; // Set to the last index

                            }
                            Display.DisplayProducts(inventory, authentication);
                        }
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (inventory.CurrentLinesMenu != 3 && inventory.IsCheckout == false)
                    {
                        int maxLines = Math.Min(inventory.ProductNames.Count, inventory.CurrentPage * 10);
                        if (inventory.CurrentLinesProducts < maxLines - 1)
                        {
                            MoveDown();
                            Display.DisplayProducts(inventory, authentication);
                        }
                        else if (inventory.IsSearching == true && inventory.CurrentPage == 1 && inventory.CurrentLinesProducts == inventory.matchingProducts.Count - 1)
                        {
                            MoveDown();

                            inventory.CurrentLinesProducts = inventory.matchingProducts.Count;
                            Display.DisplayProducts(inventory, authentication);
                        }
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (inventory.CurrentLinesMenu != 3 && inventory.IsCheckout == false)
                    {
                        PrevPageSubMenu();

                        // Decrease the currentLinesProducts value
                        inventory.CurrentLinesProducts -= Constants.ProductsPerPage;

                        // If the currentLinesProducts goes below 0 on the first page, loop around to the last page
                        if (inventory.CurrentLinesProducts < 0 && inventory.CurrentPage == 1)
                        {
                            inventory.CurrentPage = 1;
                            inventory.CurrentLinesProducts = 0;
                        }
                        // If not on the first page, just decrement the current page
                        else if (inventory.CurrentLinesProducts < 0)
                        {
                            inventory.CurrentPage -= 1;
                            inventory.CurrentLinesProducts = Math.Max(0, inventory.ProductNames.Count - 1);
                        }
                        Display.DisplayProducts(inventory, authentication);

                        if (inventory.Admin == false && inventory.CurrentLinesMenu == 0)
                        {
                            Display.DisplayCart(inventory);
                        }
                    }
                    else if (inventory.CurrentLinesMenu == 3)
                    {
                        PrevPageCart();
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (inventory.CurrentLinesMenu != 3 && inventory.IsCheckout == false)
                    {
                        NextPageSubMenu();
                        inventory.CurrentLinesProducts += Constants.ProductsPerPage; // Increase by {page} when moving to the next page
                        Display.DisplayProducts(inventory, authentication);

                        if (inventory.Admin == false && inventory.CurrentLinesMenu == 0)
                        {
                            Display.DisplayCart(inventory);
                        }
                        if (inventory.CurrentPage == 11 && inventory.CurrentLinesProducts > 100)
                        {
                            inventory.CurrentLinesProducts = 101;
                        }                      
                    }
                    else if (inventory.CurrentLinesMenu == 3)
                    {
                        NextPageCart();
                    }
                    break;
                case ConsoleKey.Escape:
                    if (authentication.IsAdminLogin == true)
                    {
                        authentication.cAdmin();
                        inventory.InSubMenu = false;
                        Console.Clear();
                        if (inventory.OnCheckout == true)
                        {
                            inventory.OnCheckout = false;
                        }
                        Display.DisplayHeader(inventory);
                        inventory.MainMenuEn = true;
                        inventory.Logout = true;
                    }
                    else
                    {
                        inventory.InSubMenu = false;
                        Console.Clear();
                        inventory.MainMenuEn = true;
                    }
                    Inventory.InAboutUs = false;
                    Console.SetWindowSize(90, 25);
                    Display.StopDisplay();
                    Display.DisplayMenu(inventory);
                    Display.DisplayHeader(inventory);
                    
                    break;
                case ConsoleKey.E:
                    if (!inventory.IsCheckout && inventory.CurrentLinesMenu != 3)
                    {
                        if (inventory.Admin && inventory.IsSearching == false)
                        {
                            EditItems();
                            Display.DisplayProducts(inventory, authentication);
                        }
                        else if (inventory.Admin && inventory.IsSearching == true)
                        {
                            Console.SetCursorPosition(2, 12);
                            Console.WriteLine("Please go back to default to Edit,\n  Feature under development. Thank You!");
                            Thread.Sleep(1000);
                        }
                    }

                    break;
                case ConsoleKey.A:
                    // Add product logic
                    {
                        if (!inventory.IsCheckout)
                        {
                            if (inventory.CurrentLinesMenu == 3 || inventory.CurrentLinesMenu == 2)
                            {
                                Console.WriteLine("");
                            }
                            else
                            {
                                AddItems(inventory, authentication);
                                if (!inventory.Admin)
                                {
                                    Display.DisplayCart(inventory);
                                }
                                Display.DisplayProducts(inventory, authentication);
                            }

                        }
                    }
                    break;
                case ConsoleKey.R:
                    // Remove product logic
                    if (!inventory.IsCheckout)
                    {
                        if (inventory.Admin)
                        {
                            ProcessRemoveItems();
                            ClearLines(36, 40, 47);
                            Display.DisplayProducts(inventory, authentication);
                        }

                        else if (inventory.InCart == true)
                        {

                            if (inventory.CartItems.Count == 0)
                            {
                                Console.SetCursorPosition(0, 15);
                                Console.WriteLine("Cart is Empty");
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                RemoveItemCart(inventory);
                                Console.Clear();
                                Display.DisplayCartDetails(inventory);
                                Display.DisplayHeader(inventory);


                            }

                        }
                    }
                    break;
                case ConsoleKey.S:
                    if (!inventory.IsCheckout && inventory.CurrentLinesMenu != 3)
                    {
                        inventory.IsSearching = true;
                        SearchProductsByFirstLetter();
                        Display.DisplayCart(inventory);
                        Display.DisplayProducts(inventory, authentication);
                        inventory.CurrentPage = 1;
                        inventory.CurrentLinesProducts = 0;
                    }
                    break;
                case ConsoleKey.I:
                    // Stock in logic
                    if (!inventory.IsCheckout && inventory.CurrentLinesMenu != 3)
                    {
                        if (inventory.CurrentLinesMenu == 2)
                        {
                            StockIn();
                            Display.DisplayProducts(inventory, authentication);
                        }
                    }
                    break;
                case ConsoleKey.M:
                    if (!inventory.IsCheckout)
                    {
                        if (inventory.InCart == true)
                        {
                            if (inventory.CartItems.Count == 0)
                            {
                                Console.SetCursorPosition(0, 15);
                                Console.WriteLine("Cart is Empty");
                            }
                            else
                            {
                                ModifyQuantity(inventory);
                                Display.DisplayCartDetails(inventory);
                            }
                        }
                    }
                    break;
                case ConsoleKey.B:
                    if (!inventory.IsCheckout && inventory.CurrentLinesMenu != 3)
                    {
                        inventory.IsSearching = false;
                        Display.DisplayProducts(inventory, authentication);
                    }
                    break;
                case ConsoleKey.C:
                    if(inventory.CurrentLinesMenu == 0 || inventory.InCart == true)
                    {
                        inventory.IsCheckout = true;
                        Console.Clear();
                        Display.DisplayReceipt(inventory, "Dime");
                    }                    
                    
                    break;
                case ConsoleKey.V:
                    
                    if (!inventory.IsCheckout && inventory.CurrentLinesMenu != 3)
                    {
                        if (inventory.CurrentLinesMenu == 0)
                        {
                            Console.Clear();
                            inventory.CurrentLinesMenu = 3;
                            Display.DisplayCartDetails(inventory);
                        }
                    }
                    break;
            }
        }
        #endregion
       
    }
}
