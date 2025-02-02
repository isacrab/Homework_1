using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_1.Models;
using Microsoft.VisualBasic.FileIO;

namespace Library.eCommerce.Services
{
    public class CartServiceProxy
    {
        public List<Cart?> shoppingCart { get; private set; }   //public function returns a list of cart objects called Carts
        
        //private List<Cart?> shoppingCart = new List<Cart?>();       //creates shoppingcart
        private CartServiceProxy() 
        {
            shoppingCart = new List<Cart?>();      //makes list of product objects, happens to every service proxy
        }
        private int LastKey //so no one can mess with it
        {
            get
            {
                //cold start of list should be empty, not null
                if (!shoppingCart.Any())
                    return 0;
                return shoppingCart.Select(p => p?.Id ?? 0).Max();    //finds max Id in list
            }
        }
        private static CartServiceProxy? instance;
        private static object instanceLock = new object();
        public static CartServiceProxy Current      //makes new proxy so everyone has one
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new CartServiceProxy();
                }
                return instance;
            }
        }

        public Products AddOrUpdate(Cart cart, Products prod, int i=0)      //add to our shopping cart, or update what we want, in product, section of array
        {
            //i is thing we want to change it to, where as cart is what we already have
            if(i <= prod.Quanity + cart.Quanity) //if number to buy is smaller than the quanitys
            {
                if(cart.Id == 0)    //if brand new,
                {
                    cart.Id = 1+LastKey;  //make it 1+ last made
                    shoppingCart.Add(cart);    //add it!
                }
                //now subtract everything!
                //cart.Quanity = i;
                prod.Quanity -= i - cart.Quanity;
                cart.Quanity = i;
            }

            if(prod.Quanity == 0)           //if not there get rid of it
                Delete(prod.Id);
            
                return prod;
        }

        public Cart? Delete(int id)     //get rid of an item
        {
            if (id == 0)        //same
                return null;

            Cart? cart = shoppingCart.FirstOrDefault();
            shoppingCart.Remove(cart);

            return cart;
        }
        //how to use in program.cs
    
        public Products? Return(int x, List<Products?> product)
        {
            if(x==0)
                return null;

            Cart? item = shoppingCart.FirstOrDefault(p=> p?.Id == x);
            var prod = product.FirstOrDefault(p => p?.Name == item?.Name);
            prod.Quanity += item.Quanity;
            shoppingCart.Remove(item);

            if (item.Quanity == 0)      //if quanity is 0, get rid of it
                Delete(item.Id);
            return prod;
        }
    
        public void CheckOut()
        {
            double? total = 0;
            Console.WriteLine("Your recipt:");
            //Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-20}", "Item", "Price per item", "Amount in Cart", "Total Price");
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-15}", "Item", "Price per item", "Amount in Cart", "Total Price");
            for (int i = 0; i<shoppingCart.Count; i++)
            {
                Console.WriteLine("{0,-10} {1,-15:F2} {2,-15} {3,-15:F2}", 
                    shoppingCart[i]?.Name ,shoppingCart[i]?.Price, shoppingCart[i]?.Quanity, shoppingCart[i]?.Quanity * shoppingCart[i]?.Price);
                total += shoppingCart[i]?.Price * shoppingCart[i]?.Quanity; 
            }

            total += total * 0.07;
            Console.WriteLine("Your total after tax is: {0:F2}", total);
            //Console.WriteLine(total);
        
        }

    }
}
