-- Create Database
CREATE DATABASE TechShopDB;
USE TechShopDB;

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

-- OrderDetails Table
CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);

-- Inventory Table
CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    QuantityInStock INT NOT NULL CHECK (QuantityInStock >= 0),
    LastStockUpdate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);

-- Insert into Customers (with Tamil Actors' Names)
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

-- Insert into Products (Tech Items)
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

-- Insert into OrderDetails (Corrected Product References)
INSERT INTO OrderDetails (OrderID, ProductID, Quantity) VALUES
(1, 1, 1),  
(2, 6, 1),
(3, 3, 1), 
(4, 4, 1),  
(5, 5, 1),  
(6, 7, 1),  
(7, 8, 1),  
(8, 9, 1),  
(9, 2, 1), 
(10, 10, 1); 

-- Insert into Inventory
INSERT INTO Inventory (ProductID, QuantityInStock, LastStockUpdate) VALUES
(1, 25, '2025-03-01 08:00:00'),  
(2, 30, '2025-03-02 09:00:00'),  
(3, 50, '2025-03-03 10:00:00'),  
(4, 20, '2025-03-04 11:00:00'),  
(5, 40, '2025-03-05 12:00:00'),  
(6, 60, '2025-03-06 13:00:00'),  
(7, 45, '2025-03-07 14:00:00'),  
(8, 55, '2025-03-08 15:00:00'), 
(9, 15, '2025-03-09 16:00:00'),  
(10, 35, '2025-03-10 17:00:00'); 


--================================
--TASK 2 
--================================

-- Retrieve Customer Names and Emails

SELECT FirstName, LastName, Email 
FROM Customers;


-- List Orders with Order Dates and Customer Names

SELECT o.OrderID, o.OrderDate, c.FirstName, c.LastName
FROM Orders o
JOIN Customers c ON o.CustomerID = c.CustomerID;


-- Insert a New Customer Record

INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) 
VALUES ('Arya', 'Stark', 'arya@example.com', '9876543211', 'Winterfell, Westeros');


-- Increase Prices of Electronic Gadgets by 10%

UPDATE Products 
SET Price = Price * 1.10;


--  Delete a Specific Order and Its Order Details
SELECT * FROM Orders
DELETE FROM OrderDetails WHERE OrderID = '5';
DELETE FROM Orders WHERE OrderID = '5';


--  Insert a New Order

INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) 
VALUES (3, GETDATE(), 150000.00);


--  Update Contact Information of a Specific Customer


UPDATE Customers 
SET Email = 'kamal_updated@example.com', Address = 'New Alwarpet, Chennai'
WHERE CustomerID =2;


-- Recalculate and Update Total Cost of Each Order

UPDATE Orders 
SET TotalAmount = (
    SELECT SUM(p.Price * od.Quantity) 
    FROM OrderDetails od
    JOIN Products p ON od.ProductID = p.ProductID
    WHERE od.OrderID = Orders.OrderID
);


--  Delete All Orders for a Specific Customer

DELETE FROM OrderDetails WHERE OrderID IN (SELECT OrderID FROM Orders WHERE CustomerID = 4);
DELETE FROM Orders WHERE CustomerID = 4;


--  Insert a New Electronic Gadget Product

INSERT INTO Products (ProductName, Description, Price) 
VALUES ('OnePlus 12', 'Flagship Android smartphone', 85000.00);


-- Update Order Status

ALTER TABLE Orders ADD OrderStatus NVARCHAR(50) DEFAULT 'Pending';


UPDATE Orders 
SET OrderStatus = 'Shipped'
WHERE OrderID = 6;

--Calculate and Update Number of Orders per Customer

ALTER TABLE Customers ADD NumberOfOrders INT DEFAULT 0;


UPDATE Customers 
SET NumberOfOrders = (
    SELECT COUNT(*) FROM Orders 
    WHERE Orders.CustomerID = Customers.CustomerID
);


-- ============================
--TASK 3
-- ============================


-- Retrieve Orders with Customer Information

SELECT 
    o.OrderID, 
    o.OrderDate, 
    c.FirstName + ' ' + c.LastName AS CustomerName, 
    o.TotalAmount
FROM Orders o
JOIN Customers c ON o.CustomerID = c.CustomerID
ORDER BY o.OrderDate DESC;  


--  Calculate Total Revenue for Each Product

SELECT 
    p.ProductName, 
    SUM(od.Quantity * p.Price) AS TotalRevenue
FROM OrderDetails od
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY p.ProductName
HAVING SUM(od.Quantity * p.Price) > 0  
ORDER BY TotalRevenue DESC; 


--  List Customers Who Made At Least One Purchase

SELECT DISTINCT 
    c.CustomerID, 
    c.FirstName + ' ' + c.LastName AS CustomerName, 
    c.Email, 
    c.Phone, 
    c.Address
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID;


--  Find the Most Popular Electronic Gadget (Highest Quantity Ordered)

SELECT TOP 1 
    p.ProductName, 
    SUM(od.Quantity) AS TotalQuantityOrdered
FROM OrderDetails od
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY p.ProductName
ORDER BY TotalQuantityOrdered DESC;

--================================================
-- List Electronic Gadgets with Their Categories 

SELECT ProductName
FROM Products;
--==================================================

--  Calculate Average Order Value for Each Customer

SELECT 
    c.CustomerID, 
    c.FirstName + ' ' + c.LastName AS CustomerName, 
    AVG(o.TotalAmount) AS AverageOrderValue
FROM Orders o
JOIN Customers c ON o.CustomerID = c.CustomerID
GROUP BY c.CustomerID, c.FirstName, c.LastName
ORDER BY AverageOrderValue DESC;


--  Find the Order with the Highest Total Revenue

SELECT TOP 1 
    o.OrderID, 
    c.FirstName + ' ' + c.LastName AS CustomerName, 
    c.Email, 
    o.TotalAmount AS TotalRevenue
FROM Orders o
JOIN Customers c ON o.CustomerID = c.CustomerID
ORDER BY TotalRevenue DESC;


--  List Electronic Gadgets and the Number of Times Each Product Has Been Ordered

SELECT 
    p.ProductName, 
    COUNT(od.OrderID) AS NumberOfTimesOrdered
FROM OrderDetails od
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY p.ProductName
ORDER BY NumberOfTimesOrdered DESC;


--  Find Customers Who Purchased a Specific Electronic Gadget



SELECT DISTINCT 
    c.CustomerID, 
    c.FirstName + ' ' + c.LastName AS CustomerName, 
    c.Email, 
    c.Phone, 
    c.Address
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
JOIN OrderDetails od ON o.OrderID = od.OrderID
JOIN Products p ON od.ProductID = p.ProductID
WHERE p.ProductName ='MacBook Pro';


--  Calculate Total Revenue for Orders Within a Specific Time Period

SELECT 
    SUM(TotalAmount) AS TotalRevenue
FROM Orders
WHERE OrderDate BETWEEN '2025-03-01' AND '2025-03-10';



--  Find Customers Who Have Not Placed Any Orders

SELECT CustomerID, FirstName + ' ' + LastName AS CustomerName, Email 
FROM Customers 
WHERE CustomerID NOT IN (SELECT DISTINCT CustomerID FROM Orders);


--  Find the Total Number of Products Available for Sale

SELECT COUNT(*) AS TotalProductsAvailable FROM Products;


--  Calculate Total Revenue Generated by TechShop

SELECT SUM(TotalAmount) AS TotalRevenue FROM Orders;

--  Calculate the Average Quantity Ordered for a Specific Product


SELECT AVG(od.Quantity) AS AverageQuantityOrdered
FROM OrderDetails od
WHERE od.ProductID = (
    SELECT ProductID 
    FROM Products 
    WHERE ProductName = 'MacBook Pro'
);



-- Calculate the Total Revenue Generated by a Specific Customer

SELECT SUM(TotalAmount) AS CustomerTotalRevenue
FROM Orders
WHERE CustomerID = 5;


--  Find Customers Who Have Placed the Most Orders

SELECT CustomerID, FirstName + ' ' + LastName AS CustomerName, OrderCount
FROM (
    SELECT c.CustomerID, c.FirstName, c.LastName, COUNT(o.OrderID) AS OrderCount
    FROM Customers c
    JOIN Orders o ON c.CustomerID = o.CustomerID
    GROUP BY c.CustomerID, c.FirstName, c.LastName
) AS OrderSummary
WHERE OrderCount = (SELECT MAX(OrderCount) FROM (
    SELECT COUNT(OrderID) AS OrderCount FROM Orders GROUP BY CustomerID) AS MaxOrders);


--  Find the Most Popular Product Category
SELECT ProductName, TotalQuantityOrdered
FROM (
    SELECT p.ProductName, SUM(od.Quantity) AS TotalQuantityOrdered
    FROM OrderDetails od
    JOIN Products p ON od.ProductID = p.ProductID
    GROUP BY p.ProductName
) AS ProductSummary
WHERE TotalQuantityOrdered = (
    SELECT MAX(TotalQuantityOrdered) 
    FROM (
        SELECT SUM(od.Quantity) AS TotalQuantityOrdered
        FROM OrderDetails od
        JOIN Products p ON od.ProductID = p.ProductID
        GROUP BY p.ProductName
    ) AS MaxProduct
);

--  Find the Customer Who Spent the Most Money

SELECT CustomerID, FirstName + ' ' + LastName AS CustomerName, TotalSpent
FROM (
    SELECT c.CustomerID, c.FirstName, c.LastName, SUM(o.TotalAmount) AS TotalSpent
    FROM Customers c
    JOIN Orders o ON c.CustomerID = o.CustomerID
    GROUP BY c.CustomerID, c.FirstName, c.LastName
) AS SpendingSummary
WHERE TotalSpent = (SELECT MAX(TotalSpent) FROM (
    SELECT SUM(TotalAmount) AS TotalSpent FROM Orders GROUP BY CustomerID) AS MaxSpending);


--  Calculate the Average Order Value for All Customers

SELECT (SELECT SUM(TotalAmount) FROM Orders) / 
       (SELECT COUNT(DISTINCT OrderID) FROM Orders) AS AverageOrderValue;



--  Find Total Number of Orders by Each Customer


SELECT 
    c.FirstName + ' ' + c.LastName AS CustomerName, 
    COUNT(o.OrderID) AS TotalOrders
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerID, c.FirstName, c.LastName
ORDER BY TotalOrders DESC; 
