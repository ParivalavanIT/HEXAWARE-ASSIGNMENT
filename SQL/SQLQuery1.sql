CREATE DATABASE TechShopDB

use TechShopDB

-- Customers Table
CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Phone NVARCHAR(15) NOT NULL,
    Address NVARCHAR(255) NOT NULL
);

-- Products Table
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(10,2) NOT NULL
);


-- Orders Table
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE CASCADE
);



-- Inventory Table
CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    QuantityInStock INT NOT NULL CHECK (QuantityInStock >= 0),
    LastStockUpdate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);

-- Insert into Customers 
INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) VALUES
('Rajinikanth', 'Sivaji', 'rajini@example.com', '9876543201', 'Poes Garden, Chennai'),
('Kamal', 'Haasan', 'kamal@example.com', '9876543202', 'Alwarpet, Chennai'),
('Vijay', 'Joseph', 'vijay@example.com', '9876543203', 'Panaiyur, Chennai'),
('Ajith', 'Kumar', 'ajith@example.com', '9876543204', 'ECR Road, Chennai'),
('Suriya', 'Sivakumar', 'suriya@example.com', '9876543205', 'T Nagar, Chennai'),
('Dhanush', 'K Raja', 'dhanush@example.com', '9876543206', 'Mandaveli, Chennai'),
('Vikram', 'Kennedy', 'vikram@example.com', '9876543207', 'Besant Nagar, Chennai'),
('Sivakarthikeyan', 'Doss', 'siva@example.com', '9876543208', 'Valasaravakkam, Chennai'),
('Karthi', 'Sivakumar', 'karthi@example.com', '9876543209', 'Kodambakkam, Chennai'),
('Jayam', 'Ravi', 'ravi@example.com', '9876543210', 'Adyar, Chennai');

-- Insert into Products with Tech Items
INSERT INTO Products (ProductName, Description, Price) VALUES
('MacBook Pro', 'Apple M2 Chip Laptop', 180000.00),
('iPhone 15 Pro', 'Apple smartphone with A17 chip', 140000.00),
('Samsung Galaxy S24', 'Android flagship smartphone', 120000.00),
('Sony WH-1000XM5', 'Wireless Noise Cancelling Headphones', 35000.00),
('Apple Watch Ultra', 'Smartwatch with health tracking', 95000.00),
('Dell XPS 15', 'High-performance Windows laptop', 160000.00),
('Logitech MX Master 3', 'Wireless ergonomic mouse', 12000.00),
('Razer BlackWidow V4', 'RGB mechanical gaming keyboard', 18000.00),
('PlayStation 5', 'Sony gaming console', 60000.00),
('Nvidia RTX 4090', 'High-end gaming graphics card', 200000.00);



-- Insert into Orders
INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) VALUES
(1, '2025-03-01 10:00:00', 250000.00),
(2, '2025-03-02 11:30:00', 185000.00),
(3, '2025-03-03 13:45:00', 95000.00),
(4, '2025-03-04 09:15:00', 120000.00),
(5, '2025-03-05 16:20:00', 35000.00),
(6, '2025-03-06 12:10:00', 9000.00),
(7, '2025-03-07 14:25:00', 15000.00),
(8, '2025-03-08 15:40:00', 27000.00),
(9, '2025-03-09 18:05:00', 180000.00),
(10, '2025-03-10 20:30:00', 25000.00);


SELECT * FROM ORDERS
SELECT OrderID FROM Orders;



-- OrderDetails Table
CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);


-- Insert into OrderDetails
INSERT INTO OrderDetails (OrderID, ProductID, Quantity) VALUES
(41, 1, 1),  
(42, 6, 1), 
(43, 3, 1),  
(44, 4, 1),  
(45, 5, 1),  
(46, 7, 1), 
(47, 8, 1),  
(48, 9, 1),  
(49, 10, 1), 
(50, 2, 1); 
SELECT * FROM OrderDetails


-- Insert into Inventory
INSERT INTO Inventory (ProductID, QuantityInStock, LastStockUpdate) VALUES
(1, 25, '2025-03-01 08:00:00'),  -- MacBook Pro
(2, 30, '2025-03-02 09:00:00'),  -- Dell XPS 15
(3, 50, '2025-03-03 10:00:00'),  -- Samsung Galaxy S24
(4, 20, '2025-03-04 11:00:00'),  -- iPad Pro
(5, 40, '2025-03-05 12:00:00'),  -- Sony WH-1000XM5
(6, 60, '2025-03-06 13:00:00'),  -- Logitech MX Master 3
(7, 45, '2025-03-07 14:00:00'),  -- Razer BlackWidow V4
(8, 55, '2025-03-08 15:00:00'),  -- AirPods Pro 2
(9, 15, '2025-03-09 16:00:00'),  -- Sony 65-inch 4K TV
(10, 35, '2025-03-10 17:00:00'); -- Seagate 2TB SSD

DELETE FROM Inventory

SELECT * FROM ORDERS

--Write an SQL query to retrieve the names and emails of all customers.

SELECT FirstName+ ' '+LastName AS Name, Email FROM CUSTOMERS

--Write an SQL query to list all orders with their order dates and corresponding customer names.

SELECT c.FirstName + ' ' + c.LastName AS CustomerName,o.OrderID, o.OrderDate FROM Orders o
JOIN Customers c ON o.CustomerID = c.CustomerID;

--Write an SQL query to insert a new customer record into the "Customers" table. Include customer information such as name, email, and address.
INSERT INTO Customers (FirstName, LastName, Email, Phone, Address)  
VALUES ('Arun', 'Kumar', 'arun.kumar@example.com', '9876543211', 'Anna Nagar, Chennai');  

--Write an SQL query to update the prices of all electronic gadgets in the "Products" table by increasing them by 10%.

UPDATE Products SET  Price = Price*1.10


--Write an SQL query to delete a specific order and its associated order details from the
--"Orders" and "OrderDetails" tables. Allow users to input the order ID as a parameter.





--Write an SQL query to insert a new order into the "Orders" table. Include the customer ID,
--order date, and any other necessary information.