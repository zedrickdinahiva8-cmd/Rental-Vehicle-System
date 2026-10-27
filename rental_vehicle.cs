using System;
using System.Collections.Generic;


class Vehicle 
{
    protected string vehicleId;
    protected string brand;
    protected double dailyRate;

    public Vehicle(string vehicleId, string brand, double dailyRate) 
    {
        this.vehicleId = vehicleId;
        this.brand = brand;
        this.dailyRate = dailyRate;
    }

   
    public virtual double CalculateRentalCost(int days) 
    {
        return dailyRate * days;
    }

    public virtual void DisplayInfo() 
    {
        Console.WriteLine($"Vehicle [ID={vehicleId}, Brand={brand}]");
    }
}


class Car : Vehicle 
{
    private int numSeats;

    public Car(string id, string brand, double rate, int seats) : base(id, brand, rate) 
    {
        this.numSeats = seats;
    }

    public override double CalculateRentalCost(int days) 
    {
        return base.CalculateRentalCost(days) * 1.10;
    }

    public override void DisplayInfo() 
    {
        Console.Write($"Car [ID={vehicleId}, Brand={brand}, Seats={numSeats}]");
    }
}

class Motorcycle : Vehicle 
{
    private int engineCapacity;

    public Motorcycle(string id, string brand, double rate, int cc) : base(id, brand, rate) 
    {
        this.engineCapacity = cc;
    }

    public override double CalculateRentalCost(int days) 
    {
        return base.CalculateRentalCost(days) * 0.95;
    }

    public override void DisplayInfo() 
    {
        Console.Write($"Motorcycle [ID={vehicleId}, Brand={brand}, CC={engineCapacity}]");
    }
}

class Customer 
{
    private string name;
    private string licenseNumber;
    private Vehicle rentedVehicle;

    public Customer(string name, string licenseNumber) 
    {
        this.name = name;
        this.licenseNumber = licenseNumber;
    }

    public void RentVehicle(Vehicle vehicle) 
    {
        rentedVehicle = vehicle;
    }

    public void ReturnVehicle() 
    {
        rentedVehicle = null;
    }

    public void PrintRentalSummary(int days) 
    {
        Console.WriteLine("=== Customer Rental Summary ===");
        Console.WriteLine($"Customer: {name} (License: {licenseNumber})");
        Console.WriteLine();
        
        if (rentedVehicle != null) 
        {
            Console.WriteLine("Rented:");
            rentedVehicle.DisplayInfo();
            Console.WriteLine();
            
            double cost = rentedVehicle.CalculateRentalCost(days);
            Console.WriteLine($"Total Cost for {days} days: ${cost:F2}");
        } 
        else 
        {
            Console.WriteLine("No vehicle currently rented.");
        }
    }
}


class Program 
{
    static void Main(string[] args) 
    {
        List<Vehicle> vehicles = new List<Vehicle>();
        vehicles.Add(new Car("101", "Toyota", 100.0, 5));
        vehicles.Add(new Motorcycle("102", "Yamaha", 50.0, 150));

        Console.WriteLine("=== Vehicle Rental Costs ===");
        foreach (Vehicle vehicle in vehicles) 
        {
            vehicle.DisplayInfo();
            Console.WriteLine($" -> Cost for 3 days: ${vehicle.CalculateRentalCost(3):F2}");
        }
        Console.WriteLine();

        Customer customer = new Customer("Alice", "AB1234");
        
        Car rentedCar = new Car("101", "Toyota", 100.0, 5);
        customer.RentVehicle(rentedCar);
        customer.PrintRentalSummary(5);
    }
}
