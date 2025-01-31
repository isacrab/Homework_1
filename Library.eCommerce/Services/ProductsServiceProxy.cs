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

        }
        private static ProductsServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductsServiceProxy Current
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
        private List<Products?> inventorylist = new List<Products?>();      //makes list of product objects, happens to every service proxy

        public List<Products?> Products     //limiting access
        {
            get
            {
                return inventorylist;
            }
        }
        //how to use in program.cs
    }
}
