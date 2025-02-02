using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_1.Models;

namespace Homework_1.Models
{
    public class Cart
    {
        public int Id { get; set; }             //for getting the ID number
        public string? Name { get; set; }        //same as writing setter and getter functions, done itself
        public double? Price { get; set; }
        public int? Quanity { get; set; }
        //public int AmtInventory { get; set; }
        public string? Display
        {
            get
            {
                return $"{Id}. {Name}  priced at ${Price},  amount in cart: {Quanity}";     //makes whole line one string
            }
        }
        public override string? ToString()
        {
            return Display ?? string.Empty;
        }
        public Cart()       //default constructor
        {
            Name = string.Empty;        //knows we want setter
        }
    }
}
