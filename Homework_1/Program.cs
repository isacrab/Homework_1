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
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Xml.Linq;
using Homework_1.Models;
using Library.eCommerce.Services;
//models = data
namespace Homework_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int lastKey = 0;    //for id kelp

            InventoryMenu();     //call menu

            char choice;
            List<Products?> inventorylist = ProductsServiceProxy.Current.Products;      //create stuff here
            List<Cart?> shoppingCart = CartServiceProxy.Current.shoppingCart;
            //make a service proxy for shopping cart
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];
                switch (choice)
                {
                    case 'c':       //creates new item in inventory
                    case 'C':
                        Console.WriteLine("Enter name of product, quanity and price");  //find index
                        ProductsServiceProxy.Current.AddOrUpdate(new Products
                        {
                            Name = Console.ReadLine(),
                            Quanity = int.Parse(Console.ReadLine() ?? "-1"),   //read in, convert string to int
                            Price = double.Parse(Console.ReadLine() ?? "-1"),   //read in, convert string to int
                        });
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
                        int select = int.Parse(Console.ReadLine() ?? "-1");  //parse is static cast for strings
                        var selectedProductupdate = inventorylist[select - 1];    //-1 so indexs match
                        Console.WriteLine("Enter name of product, quanity and price");
                        if (selectedProductupdate != null)
                        {
                            selectedProductupdate.Name = Console.ReadLine() ?? "Error";
                            selectedProductupdate.Quanity = int.Parse(Console.ReadLine());
                            selectedProductupdate.Price = double.Parse(Console.ReadLine());
                            ProductsServiceProxy.Current.AddOrUpdate(selectedProductupdate);
                        }
                            break;
                    
                    case 'd':       //delete an inventory item
                    case 'D':
                        Console.WriteLine("Which product do you want to delete?");
                        /*int delete*/select = int.Parse(Console.ReadLine() ?? "-1");
                        select= select - 1;
                        ProductsServiceProxy.Current.Delete(select);
                        var selectedProductdelete = inventorylist[select];    //-1 so indexs match
                        inventorylist.Remove(selectedProductdelete);    //removes index 
                        break;

                    case 'A':           //add to cart
                    case 'a':
                        Console.WriteLine("Which item would you like to add?");
                        inventorylist.ForEach(Console.WriteLine);                   //print out what we have
                        
                        var itemSelect = int.Parse(Console.ReadLine() ?? "-1");     //get item they want to get rid of
                        var thing = inventorylist.FirstOrDefault(p => p?.Id == itemSelect);
                        
                        Console.WriteLine("How many would you like to buy?");
                        var numBuy = int.Parse(Console.ReadLine() ?? "-1");
                        
                        if(thing!=null)
                        {
                            ProductsServiceProxy.Current.AddOrUpdate(
                                CartServiceProxy.Current.AddOrUpdate(new Cart       //adding
                                {                   //set what we need
                                    Name = thing.Name,
                                    Price = thing.Price,
                                    Quanity = 0
                                }, thing, numBuy));
                        }


                        break;
                    
                    case 'S':           //show cart
                    case 's':
                        shoppingCart.ForEach(Console.WriteLine);
                        break;

                    case 'P':       //update carts quanity of an item
                    case 'p':
                        Console.WriteLine("Which item would you like to update");
                        shoppingCart.ForEach(Console.WriteLine);
                        select = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProductupdate = inventorylist.FirstOrDefault(p => p?.Id == select);         //get thing
                        var selectedCart = shoppingCart.FirstOrDefault(p => p?.Name == selectedProductupdate.Name);

                        if(selectedCart!=null)      //go into shopping cart and update
                        {
                            Console.WriteLine("What do you want new quanity to be?");
                            select = int.Parse(Console.ReadLine() ?? "-1");

                            ProductsServiceProxy.Current.AddOrUpdate(
                                CartServiceProxy.Current.AddOrUpdate(
                                    selectedCart, selectedProductupdate, select));
                        }
                        else
                        {
                            Console.WriteLine("This doesn't exist!");
                        }
                        break;

                    case 'E':           //for returning
                    case 'e':
                        Console.WriteLine("Which item do you want to return to inventory?");
                        select = int.Parse (Console.ReadLine() ?? "-1");
                        selectedCart = shoppingCart.FirstOrDefault(p => p?.Id == select);       //find stuff, then return it
                        selectedProductdelete = inventorylist.FirstOrDefault(p => p?.Name == selectedCart?.Name);
                        
                        if(selectedCart != null)
                        {
                            ProductsServiceProxy.Current.AddOrUpdate(
                                CartServiceProxy.Current.Return(select, inventorylist));
                        }

                        break;
                    
                    case 'q':          //quit program and checkout
                    case 'Q':
                        CartServiceProxy.Current.CheckOut();
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
        static void InventoryMenu()     //for simplicity
        {
            Console.WriteLine("Welcome to your inventory!");
            Console.WriteLine("C.   Create new inventory item");
            Console.WriteLine("R.   Read all inventory items");
            Console.WriteLine("U.   Update all inventory items");
            Console.WriteLine("D.   Delete an inventory item");
            Console.WriteLine();
            Console.WriteLine("A.   Add inventory item to cart");
            Console.WriteLine("S.   Read all shopping cart item");
            Console.WriteLine("P.   Update quanity in shopping cart");
            Console.WriteLine("E.   Delete an item from shopping cart");
            Console.WriteLine();
            Console.WriteLine("Q.   Quit and check out");
            Console.WriteLine("M.   See Menu");
        }

    }
}
