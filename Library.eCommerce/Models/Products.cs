using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework_1.Models
{
    public class Products
    {
        public int Id { get; set; }             //for getting the ID number
        public string? Name { get; set; }        //same as writing setter and getter functions, done itself
        
        public string? Display
        {
            get
            {
                return $"{Id}. {Name}  ${Price}  ({Quanity})";     //makes whole line one string
            }
        }

        public override string? ToString()
        {
            return Display;
        }

        public Products()       //default constructor
        {
            Name = string.Empty;        //knows we want setter
        }

        /*                                                    I ADDED BELOW                                                  */
        //how many are in inventory
        //price
        public double? Price { get; set; }      //to set and recieve the price of item
        public int Quanity { get; set; }       //to set and recieve number of items

        public int AmtInventory {  get; set; }
    }
    
    /*public class Cart
    {
        //public int Size_Cart { get; set; }        //size of inventory
        public string? CartName { get; set; }        //same as writing setter and getter functions, done itself
        public double? CartPrice { get; set; }      //to set and recieve the price of item
        //create models for shopping car
        //make functions for adding, printing recept, how much we have cart, make it into a service proxy
        public Cart()
        {
            //Size_Cart = 0;
        }
        public string? Display
        {
            get
            {
                return $"{CartName} priced at ${CartPrice})";     //makes whole line one string
            }
        }

        public override string? ToString()
        {
            return Display;
        }

    }*/
}
