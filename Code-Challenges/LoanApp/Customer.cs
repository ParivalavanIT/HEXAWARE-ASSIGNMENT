using System;



namespace LoanApp{
public class Customer
{
    private int customerId;
    private string name;
    private string emailAddress;
    private string phoneNumber;
    private string address;
    private int creditScore;

    // Default constructor
    public Customer()
    {
    }

    // Overloaded constructor with parameters
    public Customer(int customerId, string name, string emailAddress, string phoneNumber, string address, int creditScore)
    {
        this.customerId = customerId;
        this.name = name;
        this.emailAddress = emailAddress;
        this.phoneNumber = phoneNumber;
        this.address = address;
        this.creditScore = creditScore;
    }

    // Getters and Setters
    public int CustomerId
    {
        get { return customerId; }
        set { customerId = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string EmailAddress
    {
        get { return emailAddress; }
        set { emailAddress = value; }
    }

    public string PhoneNumber
    {
        get { return phoneNumber; }
        set { phoneNumber = value; }
    }

    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    public int CreditScore
    {
        get { return creditScore; }
        set { creditScore = value; }
    }

    // Method to print all information of attributes
    public void PrintInformation()
    {
        Console.WriteLine("Customer Information:");
        Console.WriteLine($"Customer ID: {customerId}");
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Email: {emailAddress}");
        Console.WriteLine($"Phone: {phoneNumber}");
        Console.WriteLine($"Address: {address}");
        Console.WriteLine($"Credit Score: {creditScore}");
    }
}

}