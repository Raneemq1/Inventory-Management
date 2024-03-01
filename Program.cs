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

            int answer = -1;
            Console.WriteLine("Choose an Answer\n1-Add a product\n2-View all products" +
                "\n3-Edit a product\n4-Delete a product\n5-Search for a product\n" +
                "6-Exit");
            answer = int.Parse(Console.ReadLine());

            while (answer != 0&&answer!=6)
            {

                switch (answer)
                {
                    case 1:
                        {
                            Console.Clear();
                            string name;
                            double price;
                            int quantity;
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

                    default: { break; }


                }
                Console.Clear();
                Console.WriteLine("Choose an Answer\n1-Add a product\n2-View all products" +
                "3-Edit a product\n4-Delete a product\n5-Search for a product\n" +
                "6-Exit");
                answer = int.Parse(Console.ReadLine());

            }
        }
    }
}
