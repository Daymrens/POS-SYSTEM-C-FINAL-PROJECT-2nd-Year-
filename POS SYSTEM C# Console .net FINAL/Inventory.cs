using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplaMenu_Test
{
    public class Inventory
    {
        #region FIELDS

        public List<string> matchingProducts { get; set; }
        public List<string> ProductNames { get; set; }
        public List<int> Quantity { get; set; }
        public List<double> Price { get; set; }       
        public List<string> productNamesInCart { get; set; }
        
        public List<(string productName, int quantityInCart, double price)> CartItems { get; set; } = new List<(string, int, double)>();
        public List<(string productName, int quantity, double price)> matchingProductsDetails = new List<(string, int, double)>();
        public string[] ListMenuEmployee { get; private set; }
        public string[] ListMenuAdmin { get; private set; }
        public string[] MainMenu { get; private set; }


        public int CurrentLinesMenu { get; set; } = 0;
        public int CurrentLinesSubMenu { get; set; } = 0;
        public int CurrentPage { get;  set; }
        public int PageSize { get; set; } = 10; // Set the default page size to 10
        public int CurrentLinesProducts { get; set; }
        public int CurrentLineCart { get; set; }
        public int IndexProdNames { get; set; }

        public bool Logout = false;
        public bool Admin { get; set; }
        public bool MainMenuEn { get; set; }        
        public bool InSubMenu { get; set; }
        public bool IsSearching {  get; set; }
        public bool IsCheckout { get; set; }
        public bool OnCheckout { get; set; }
        public bool InCart { get; set; }
        public static bool InAboutUs {  get; set; }
        #endregion


        // Constructor to initialize the fields
        public Inventory()
        {
            ListMenuAdmin = new string[] { "Add", "Edit", "Remove", "Search", "Stock In" };
            ListMenuEmployee = new string[] { "Add to Cart", "Search" };
            MainMenu = new string[] { "P O S", "Product Management","Inventory", "About Us", "Log Out" };
            matchingProducts = new List<string>();

            CurrentLineCart = 0;
            CurrentLinesProducts = 0;
            CurrentPage = 1;
            

            Admin = false;
            IsSearching = true;                                    
            MainMenuEn = true;
            IsCheckout = true;
            OnCheckout = true;
            InCart = false;
            InAboutUs = false;

            ProductNames = new List<string>()
            {
            "Milk",
            "Hatdog",
            "Chocolate",
            "Candy",
            "Sour Candy",
            "Bubble Gum",
            "Snow Bear",
            "Max",
            "Presto",
            "Sky Flakes",
            "Bingo",
            "Chocomucho",
            "Cloud 9",
            "Rebisco Crackers",
            "Fresh Milk",
            "Pineaple Juice",
            "C2 Large",
            "C2 Medium",
            "C2 Small",
            "Sprite Can",
            "Coke Can",
            "Royal Can",
            "Trust",
            "Robust",
            "Lighter",
            "Milo",
            "Orange Juice",
            "Tang",
            "Eight Oclock",
            "Bread",
            "Mayonnaise",
            "Happy",
            "Sunflower Seeds",
"Sparkling Water",
"Avocado",
"Peanut Butter",
"Salsa",
"Granola",
"Cheddar",
"Quinoa",
"Popcorn",
"Coffee",
"Honey",
"Blueberries",
"Chips",
"Almonds",
"Yogurt",
"Pasta",
"Sausage",
"Cereal",
"Tofu",
"Hummus",
"Kale",
"Pesto",
"Tuna",
"Salmon",
"Tea",
"Coconut",
"Olive",
"Soy Milk",
"Peaches",
"Ginger",
"Syrup",
"Grapes",
"Mustard",
"Pickle",
"Jam",
"Eggs",
"Bacon",
"Tomato",
"Cheese",
"Lemonade",
"Melon",
"Cherries",
"Paprika",
"Sorbet",
"Sardines",
"Cucumber",
"Carrot",
"Radish",
"Oregano",
"Basil",
"Vinegar",
"Sesame",
"Pumpkin",
"Cinnamon",
"Pepper",
"Cashews",
"Protein Bar",
"Brownie",
"Tofurky",
"Snack Mix",
"Mango",
"Rice",
"Bean",
"Smartwatch",
"Facial Serum",
"Air Purifier",
"Coffee Blend",
"Backpack",

            };
            Quantity = new List<int>()
            {
            10,
            20,
            50,
            80,
            35,
            10,
            60,
            30,
            30,
            20,
            40,
            20,
            90,
            85,
            30,
            10,
            60,
            70,
            90,
            20,
            40,
            10,
            50,
            50,
            30,
            10,
            60,
            74,
            93,
            23,
            43,
            10,
            10,
            37,
            47,
            34,
            20,
            43,
            15,
            40,
            28,
            11,
            48,
            32,
            26,
            42,
            13,
            36,
            31,
            20,
            10,
            40,
            10,
            50,
            30,
            20,
            30,
            16,
            44,
            12,
            47,
            30,
            20,
            40,
            29,
            30,
            20,
            40,
            10,
            40,
            20,
            60,
            30,
            10,
            60,
            10,
            20,
            80,
            40,
            70,
            50,
            90,
            30,
            30,
            10,
            40,
            10,
            70,
            30,
            20,
            80,
            20,
            30,
            90,
            10,
            40,
            10,
            40,
            30,
            20,
                                               
            };
            Price = new List<double>()
            {
            200.95,
            100.15,
            200.10,
            200.36,
            200.76,
            200.26,
            200.90,
            200.99,
            200.10,
            200.25,
            200.19,
            100.10,
            200.10,
            200.12,
            200.15,
            200.17,
            200.80,
            200.90,
            200.87,
            200.18,
            200.89,
            100.99,
            200.90,
            200.90,
            200.70,
            200.35,
            200.30,
            200.10,
            200.70,
            200.10,
            100.10,
            190,
            370,
            470,
            340,
            200,
            430,
            150,
            400,
            280,
            110,
            480,
            320,
            260,
            420,
            130,
            360,
            310,
            240,
            180,
            450,
            170,
            500,
            390,
            230,
            300,
            160,
            440,
            120,
            470,
            350,
            210,
            480,
            290,
            380,
            250,
            420,
            140,
            470,
            320,
            260,
            430,
            190,
            360,
            310,
            240,
            180,
            450,
            170,
            500,
            390,
            230,
            300,
            160,
            440,
            120,
            470,
            350,
            210,
            480,
            290,
            380,
            200,
            100.15,
            200.10,
            200.36,
            200.76,
            200.26,
            200.90,
            200.99,                                    
            };
            CartItems = new List<(string productName, int quantityInCart, double price)>();
            matchingProductsDetails = new List<(string productName, int quantity, double price)>();
            
        }
              
    }

}

