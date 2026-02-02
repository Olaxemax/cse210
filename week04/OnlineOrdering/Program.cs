using System;
using System.Collections.Generic;

class Product
{
    private string name;
    private string productId;
    private double price;
    private int quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public double GetTotalCost()
    {
        return price * quantity;
    }

    public string GetPackingLabel()
    {
        return $"Product: {name}, ID: {productId}";
    }
}

class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }

    public string GetShippingLabel()
    {
        return $"Customer: {name}\n{address.GetFullAddress()}";
    }
}

class Order
{
    private List<Product> products = new List<Product>();
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetTotalPrice()
    {
        double total = 0;
        foreach (Product p in products)
        {
            total += p.GetTotalCost();
        }
        total += customer.LivesInUSA() ? 5 : 35;
        return total;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product p in products)
        {
            label += p.GetPackingLabel() + "\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return "Shipping Label:\n" + customer.GetShippingLabel();
    }
}

class Program
{
    static void Main()
    {
        // First order
        Address addr1 = new Address("123 Main St", "Rexburg", "ID", "USA");
        Customer cust1 = new Customer("John Doe", addr1);
        Order order1 = new Order(cust1);
        order1.AddProduct(new Product("Widget A", "A123", 3.00, 5));
        order1.AddProduct(new Product("Widget B", "B456", 10.00, 2));

        // Second order
        Address addr2 = new Address("456 Elm St", "Toronto", "ON", "Canada");
        Customer cust2 = new Customer("Jane Smith", addr2);
        Order order2 = new Order(cust2);
        order2.AddProduct(new Product("Gadget X", "X789", 15.00, 1));
        order2.AddProduct(new Product("Gadget Y", "Y101", 7.50, 4));

        // Display results
        List<Order> orders = new List<Order> { order1, order2 };
        foreach (Order o in orders)
        {
            Console.WriteLine(o.GetPackingLabel());
            Console.WriteLine(o.GetShippingLabel());
            Console.WriteLine($"Total Price: ${o.GetTotalPrice()}\n");
        }
    }
}