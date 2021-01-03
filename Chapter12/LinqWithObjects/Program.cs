using System;
using Packt.Shared;
using System.Linq;

namespace LinqWithObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            JoinCategoriesAndProducts();
        }

        private static void JoinCategoriesAndProducts(){
            using(var db = new Northwind()){
                var queryJoin = db.Categories
                        .Join(
                            inner: db.Products, 
                            outerKeySelector: category => category.CategoryID,
                            innerKeySelector: product => product.CategoryID,
                            resultSelector: (c, p) => new{c.CategoryName, p.ProductName, p.ProductID,}
                            );
                foreach(var item in queryJoin){
                    Console.WriteLine("{0}: {1} is in {2}", item.ProductID, item.ProductName, item.CategoryName);
                }
            }
        }

        private static void GroupJoinCategoriesAndProducts(){
            using(var db = new Northwind()){
                var query = db.Categories
                            .AsEnumerable()
                            .GroupJoin(
                                inner: db.Products,
                                outerKeySelector: category => category.CategoryID,
                                innerKeySelector: product => product.CategoryID,
                                resultSelector: (c, matchingProducts) => new{
                                    c.CategoryName,
                                    Products = matchingProducts.OrderBy(p => p.ProductName)
                                }
                            );
            }
        }
    }
}
