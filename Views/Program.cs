using InventoryManagement.Repositories;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagement;

try
{
    IProductRepository db = ConfigurationsFile.Read("appsettings.json");
    db.SetupDB();
    Menu(db);
}
catch
{
    Console.WriteLine("Invalid configuration");
}


void Menu(IProductRepository db)
{
    Console.Clear();
    string answer, value, name, searchedName;
    int quantity;
    double price;

    while (true)
    {
        Console.WriteLine("Choose an option:\n1-Add a product\n2-View all products\n3-Edit a product\n4-Delete a product\n5-Search for a product\n6-Exit");
        answer = Console.ReadLine();

        switch (answer)
        {
            case "1":
                Console.Clear();
                Console.Write("Product Name=");
                name = Console.ReadLine();
                Console.Write("\nProduct Price=");
                price = double.Parse(Console.ReadLine());
                Console.Write("\nProduct Quantity=");
                quantity = int.Parse(Console.ReadLine());
                Product product = new(name, quantity, price);

                try
                {
                    db.InsertProduct(product);
                    Console.WriteLine("Product added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                break;

            case "2":
                Console.Clear();
                IEnumerable<Product> products = db.GetProducts();
                if (products.Count() == 0) Console.WriteLine("No products yet");
                foreach (var prod in products)
                {
                    Console.WriteLine("Name={0}\tQuantity={1}\tPrice={2}$", prod.Name, prod.Quantity, prod.Price);
                }
                Console.WriteLine("\n\nPress Enter to return");
                Console.ReadLine();
                break;

            case "3":
                Console.Clear();
                Console.WriteLine("Enter the name of the product to edit:");
                searchedName = Console.ReadLine();
                product = db.RetrieveProductByName(searchedName);

                if (product != null)
                {
                    Console.Write("Product Name(" + product.Name + "): ");
                    value = Console.ReadLine();
                    if (!string.IsNullOrEmpty(value))
                    {
                        product.Name = value;
                    }

                    Console.Write("\nProduct Price(" + product.Price + "): ");
                    value = Console.ReadLine();
                    if (!string.IsNullOrEmpty(value))
                    {
                        product.Price = double.Parse(value);
                    }

                    Console.Write("\nProduct Quantity(" + product.Quantity + "): ");
                    value = Console.ReadLine();
                    if (!string.IsNullOrEmpty(value))
                    {
                        product.Quantity = int.Parse(value);
                    }

                    db.UpdateProduct(searchedName, product);
                    Console.WriteLine("Product updated successfully.");
                }
                else
                {
                    Console.WriteLine("No product found with this name.");
                }
                Console.WriteLine("\n\nPress Enter to return");
                Console.ReadLine();
                break;

            case "4":
                Console.Clear();
                Console.WriteLine("Enter the name of the product to remove:");
                name = Console.ReadLine();

                if (db.DeleteProduct(name))
                {
                    Console.WriteLine("Product removed successfully.");
                }
                else
                {
                    Console.WriteLine("No product found with this name.");
                }
                Console.WriteLine("\n\nPress Enter to return");
                Console.ReadLine();
                break;

            case "5":
                Console.Clear();
                Console.WriteLine("Enter the name of the product to search:");
                name = Console.ReadLine();
                product = db.RetrieveProductByName(name);

                if (product != null)
                {
                    Console.WriteLine(product.ToString());
                }
                else
                {
                    Console.WriteLine("No product found with this name.");
                }
                Console.WriteLine("\n\nPress Enter to return");
                Console.ReadLine();
                break;

            case "6":
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Invalid option, please try again.");
                break;
        }

        Console.Clear();
    }
}
