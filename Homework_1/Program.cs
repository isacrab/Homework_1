/*
 There must be a menu that allows a person to take the following actions on either the inventory or their shopping cart:
1.Create an item (or add an item from the inventory to the shopping cart)
2.Read all items in the inventory or the shopping cart
3.Update the product details in the inventory (or the number of items in the shopping cart)
4.Delete the product from the inventory or return all of a product from the shopping cart to the inventory
5.When a user adds from the inventory to the cart, the number of a product in the inventory and 
shopping cart must reflect the correct number
6.A user may not purchase more of a product than is available in the inventory
7.When a user wants to quit the program, they should check out, which should present a formatted, 
itemized receipt including the total of their purchase with 7% sales tax included
 */
using System;
using System.Collections.Generic;
using Homework_1.Models;
using Library.eCommerce.Services;
//models = data
namespace Homework_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int lastKey = 0;    //for id kelp

            InventoryMenu();     //call menu

            char choice;
            List<Products?> inventorylist = ProductsServiceProxy.Current.Products;
            //make a service proxy for shopping car
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];
                switch (choice)
                {
                    case 'c':       //creates new item in inventory
                    case 'C':
                        //Console.WriteLine("Choose 1 for add to inventory, 2 for add something to cart");

                        //int Cchoice = int.Parse(Console.ReadLine() ?? "-1");
                        
                        //if (Cchoice == 1)
                        //{
                            Console.WriteLine("Name of item:");
                            var name = Console.ReadLine();  //cin.getline
                            var prod = new Products         //same as initalizing array
                            {
                                Id = ++lastKey,     //each Product object sets an Id, name, quanity,Price
                                Name = name,
                                Quanity = 0,        //have them do quanity when they enter it
                                Price = 0.0,
                            };
                            inventorylist.Add(prod);

                            UpdateQuanity(prod);        //my functions to change quanity and prices
                            UpdatePrice(prod);
                        //}
                        //else if(Cchoice == 2)
                        //{
                        //    Console.WriteLine("Current inventory to choose from:");
                        //    inventorylist.ForEach(Console.WriteLine);
                        //    Console.WriteLine("Which item do you want to add to cart?");
                        //    int choose = int.Parse(Console.ReadLine() ?? "-1");
                        //    while (choose <= 0)
                        //    {
                        //        Console.WriteLine("Invalid number, which item do you want to add to cart?");
                        //        choose = int.Parse(Console.ReadLine() ?? "-1");
                        //    }
                        //}
                        break;
                    
                    case 'r':       //reads what we have in inventory
                    case 'R':
                        inventorylist.ForEach(Console.WriteLine);
                        //foreach (var prod in inventorylist)   //this is same as above
                        //    Console.WriteLine(prod);
                        break;
                    
                    case 'u':       //can update an inventory item
                    case 'U':
                        Console.WriteLine("Which product do you want to update?");  //find index
                        int updateselect = int.Parse(Console.ReadLine() ?? "-1");  //parse is static cast for strings

                        Console.WriteLine("1. Replace item completely");    //give options for what to change
                        Console.WriteLine("2. Change item's price");
                        Console.WriteLine("3. Change quanity of item in inventory");
                        
                        int UpdChoice = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProductupdate = inventorylist[updateselect - 1];    //-1 so indexs match
                        
                        if (UpdChoice == 1)
                        {
                            Console.WriteLine("Name of item:");
                            //var selectedProductupdate = inventorylist[updateselect - 1];    //-1 so indexs match
                            if (selectedProductupdate != null)
                                selectedProductupdate.Name = Console.ReadLine() ?? "Error";

                            UpdateQuanity(selectedProductupdate);
                            UpdatePrice(selectedProductupdate);
                        }
                        else if(UpdChoice==2)
                        {
                            UpdatePrice(selectedProductupdate);
                        }
                        else if(UpdChoice==3)
                        {
                            UpdateQuanity(selectedProductupdate);
                        }
                            break;
                    
                    case 'd':       //delete an inventory item
                    case 'D':
                        Console.WriteLine("Which product do you want to delete?");
                        int deleteselect = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProductdelete = inventorylist[deleteselect - 1];    //-1 so indexs match
                        inventorylist.Remove(selectedProductdelete);    //removes index 
                        break;
                    
                    case 'q':          //quit program
                    case 'Q':
                        break;

                    case 'M':
                    case 'm':
                        InventoryMenu();
                        break;

                    default:
                        Console.WriteLine("Error: Unknown Entry");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');

            Console.ReadLine();
        }

        static void InventoryMenu()
        {
            Console.WriteLine("Welcome to your inventory!");
            Console.WriteLine("C.   Create new inventory item");
            Console.WriteLine("R.   Read all inventory items");
            Console.WriteLine("U.   Update all inventory items");
            Console.WriteLine("D.   Delete an inventory item");
            Console.WriteLine("Q.   Quit and check out");
            Console.WriteLine("S.   Switch to shopping cart interface");
            Console.WriteLine("M.   See Menu");
        }

        static void ShoppingMenu()
        {
            Console.WriteLine("Welcome to the shopping interface");
            Console.WriteLine("A.   Add inventory item to cart");
            Console.WriteLine("R.   Read all shopping cart item");
            Console.WriteLine("U.   Update quanity in shopping cart");
            Console.WriteLine("D.   Delete an item from shopping cart");
            Console.WriteLine("Q.   Quit and check out");
            Console.WriteLine("V.   View your cart");
            Console.WriteLine("M.   See Menu");
        }
        static void UpdateQuanity(Products f)
        {
            Console.WriteLine("Quanity of this item: ");        //quanity stuff here
            do
            {
                f.Quanity = int.Parse(Console.ReadLine() ?? "-1");   //read in, convert string to int
                if (f.Quanity <= 0)
                    Console.WriteLine("Invalid number, try again.");
            } while (f.Quanity <= 0);
        }
        static void UpdatePrice(Products f)
        {
            Console.WriteLine("Enter the price of this item: ");    //price stuff here
            do
            {
                f.Price = double.Parse(Console.ReadLine() ?? "-1");   //read in, convert string to int
                if (f.Price <= 0.0)
                    Console.WriteLine("Invalid price, try again.");
            } while (f.Price <= 0.0);
        }
    }
}
