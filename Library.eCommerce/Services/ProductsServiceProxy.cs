
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_1.Models;

namespace Library.eCommerce.Services
{
    ////for CartServiceProxy.cs
    ////9:40, copy this service proxy, change so it manages cart stuff

    public class ProductsServiceProxy   //container class
    {
        private ProductsServiceProxy()
        {
           Products = new List<Products?>();      //makes list of product objects, happens to every service proxy
        }
        private int LastKey //so no one can mess with it
        {
            get
            {
                //cold start of list should be empty, not null
                if(!Products.Any())
                    return 0;
                return Products.Select(p => p?.Id ?? 0).Max();    //finds max Id in list
            }
        }
        private static ProductsServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductsServiceProxy Current      //makes new proxy so everyone has one
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new ProductsServiceProxy();
                }
                return instance;
            }

        }
        private List<Products?> inventorylist = new List<Products?>();

        public List<Products?> Products     //limiting access
        {
            get
            {
                return inventorylist;
            }
            private set
            { }
        }

        public Products AddOrUpdate(Products products)
        {
            if (products.Id == 0)
            {
                products.Id = LastKey + 1;
                Products.Add(products);
            }
            return products;
        }
        
        public Products? Delete(int id)
        {
            if (id == 0)
                return null;
            Products? products = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(products);
            return products;
        }
        //how to use in program.cs
    }
}
