using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inventory Management");
            Menu();
        }

        public static void Menu()
        {
            Inventory inventory = new Inventory();

            string answer = "";
            string value="";
            string name;
            double price;
            int quantity;
      
            Console.WriteLine("Choose an Answer\n1-Add a product\n2-View all products" +
                "\n3-Edit a product\n4-Delete a product\n5-Search for a product\n" +
                "6-Exit");
            answer = Console.ReadLine();

            while (answer!="6")
            {

                switch (answer)
                {
                    case "1":
                        {
                            Console.Clear();
                            
                            Console.Write("Product Name=");
                            name = Console.ReadLine();
                            Console.Write("\nProduct Price=");
                            price = double.Parse(Console.ReadLine());
                            Console.Write("\nProduct Quantity=");
                            quantity = int.Parse(Console.ReadLine());
                            Product product = new Product(name, quantity, price);
                            inventory.AddProduct(product);


                            break;
                        }
                    case "2":
                        {
                            Console.Clear();
                            List<Product> products=inventory.ViewAllProducts();
                             foreach (var product in products)
            {
                Console.WriteLine("Name={0}\tQuantity={1}\tPrice={2}$", product.Name, product.Quantity, product.Price);
            }
                            Console.WriteLine("\n\nPress anything to return");
                            value=Console.ReadLine();
                           
                            break;
                        }

                    case "3":
                        {
                            Console.Clear();
                            Console.WriteLine("Write a name to check the product");
                            name = Console.ReadLine();
                            
                            if (inventory.CheckProduct(name))
                            {
                                Product product=inventory.SearchProduct(name);
                                name=product.Name;  
                                quantity=product.Quantity;  
                                price=product.Price;
                                Console.Write("Product Name("+product.Name+"):");
                                value=Console.ReadLine();
                               
                                if (value!=name)
                                {
                                    name = value;
                                    product.UpdateProductName(name);
                                }
                                Console.Write("\nProduct Price(" + product.Price + "):");
                                value = Console.ReadLine();
                                if (double.Parse(value) != price)
                                {
                                    price = double.Parse(value);    
                                    product.UpdateProductPrice(price);
                                }
                                Console.Write("\nProduct Quantity(" + product.Quantity + "):");
                                value = Console.ReadLine();
                                if (int.Parse(value) != quantity)
                                {
                                    quantity = int.Parse(value);
                                    product.UpdateProductQuantity(quantity);
                                }

                            }
                            else
                            {

                                Console.WriteLine("No products with this name found");
                            }
                            Console.WriteLine("\n\nPress anything to return");
                            value = Console.ReadLine();

                            break;
                        }

                    default: { break; }


                }
                Console.Clear();
                Console.WriteLine("Choose an Answer\n1-Add a product\n2-View all products" +
                "\n3-Edit a product\n4-Delete a product\n5-Search for a product\n" +
                "6-Exit");
                answer = Console.ReadLine();

            }
        }
    }
}
