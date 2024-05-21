using InventoryManagement.Controllers;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;


Console.WriteLine("Inventory Management");
Menu();

void Menu()
{
    SqlDataBase db = new("Inventory");
    MongoDataBase mongoDB =new();
    Inventory inventory =new();

    string answer, value;
    string name;
    double price;
    int quantity;

    Console.WriteLine("Choose an Answer\n1-Add a product\n2-View all products" +
        "\n3-Edit a product\n4-Delete a product\n5-Search for a product\n" +
        "6-Exit");

    answer = Console.ReadLine();

    while (answer != "6")
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
                    try
                    {
                        db.InsertProduct(product);
                        mongoDB.InsertProduct(product);
                    }
                    catch(Exception ex) { Console.WriteLine(ex.ToString());}


                    break;
                }
            case "2":
                {
                    Console.Clear();
                    List<Product> products = inventory.ViewAllProducts();
                    foreach (var product in products)
                    {
                        Console.WriteLine("Name={0}\tQuantity={1}\tPrice={2}$", product.Name, product.Quantity, product.Price);
                    }
                    Console.WriteLine("\n\nPress anything to return");
                    value = Console.ReadLine();

                    break;
                }

            case "3":
                {
                    Console.Clear();
                    Console.WriteLine("Write a name to check the product");
                    name = Console.ReadLine();
                    Product product = inventory.SearchProduct(name);
                    if (product is not null)
                    {
                        Console.Write("Product Name(" + product.Name + "):");
                        value = Console.ReadLine();

                        if (!string.IsNullOrEmpty(value))
                        {
                            name = value;
                            product.Name = name;
                        }
                        Console.Write("\nProduct Price(" + product.Price + "):");
                        value = Console.ReadLine();
                        if (!string.IsNullOrEmpty(value))
                        {
                            price = double.Parse(value);
                            product.Price = price;
                        }
                        Console.Write("\nProduct Quantity(" + product.Quantity + "):");
                        value = Console.ReadLine();
                        if (!string.IsNullOrEmpty(value))
                        {
                            quantity = int.Parse(value);
                            product.Quantity = quantity;
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
            case "4":
                {
                    Console.Clear();
                    Console.WriteLine("Put the name of a product to remove it");
                    name = Console.ReadLine();
                    if (inventory.CheckProduct(name))
                    {
                        Product product = inventory.SearchProduct(name);
                        inventory.RemoveProduct(product);
                        Console.WriteLine("Removed Sucessfully");
                    }
                    else
                    {
                        Console.WriteLine("No product with this name found");
                    }
                    Console.WriteLine("\n\nPress anything to return");
                    value = Console.ReadLine();
                    break;
                }
            case "5":
                {
                    Console.Clear();
                    Console.WriteLine("Put the name of a product to search if it's in the inventory");
                    name = Console.ReadLine();
                    if (inventory.CheckProduct(name))
                    {
                        Product product = inventory.SearchProduct(name);
                        Console.WriteLine(product.ToString());
                    }
                    else
                    {
                        Console.WriteLine("No product found with this name");
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




