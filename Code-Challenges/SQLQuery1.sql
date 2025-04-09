CREATE DATABASE ECommerceDB;

USE ECommerceDB


CREATE TABLE customers (
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    email NVARCHAR(255) UNIQUE NOT NULL,
    password NVARCHAR(255) NOT NULL
);

CREATE TABLE products (
    product_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    description NVARCHAR(MAX),
    stockQuantity INT NOT NULL CHECK (stockQuantity >= 0)
);


CREATE TABLE cart (
    cart_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id) 
);


CREATE TABLE orders (
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    order_date DATE DEFAULT GETDATE(),
    total_price DECIMAL(10,2) NOT NULL CHECK (total_price >= 0),
    shipping_address NVARCHAR(255) NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id) 
);

CREATE TABLE order_items (
    order_item_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    FOREIGN KEY (order_id) REFERENCES orders(order_id) ,
    FOREIGN KEY (product_id) REFERENCES products(product_id) 
);


INSERT INTO customers (name, email, password) VALUES 
('John Doe', 'johndoe@example.com', 'password123'),
('Jane Smith', 'janesmith@example.com', 'securepass'),
('Robert Johnson', 'robert@example.com', 'robertpass'),
('Sarah Brown', 'sarah@example.com', 'sarahpass'),
('David Lee', 'david@example.com', 'davidpass'),
('Laura Hall', 'laura@example.com', 'laurapass'),
('Michael Davis', 'michael@example.com', 'michaelpass'),
('Emma Wilson', 'emma@example.com', 'emmapass'),
('William Taylor', 'william@example.com', 'williampass'),
('Olivia Adams', 'olivia@example.com', 'oliviapass');

INSERT INTO products (name, description, price, stockQuantity) VALUES 
('Laptop', 'High-performance laptop', 800.00, 10),
('Smartphone', 'Latest smartphone', 600.00, 15),
('Tablet', 'Portable tablet', 300.00, 20),
('Headphones', 'Noise-canceling', 150.00, 30),
('TV', '4K Smart TV', 900.00, 5),
('Coffee Maker', 'Automatic coffee maker', 50.00, 25),
('Refrigerator', 'Energy-efficient', 700.00, 10),
('Microwave Oven', 'Countertop microwave', 80.00, 15),
('Blender', 'High-speed blender', 70.00, 20),
('Vacuum Cleaner', 'Bagless vacuum cleaner', 120.00, 10);

INSERT INTO cart (customer_id, product_id, quantity) VALUES 
(1, 1, 2),  
(1, 3, 1),  
(2, 2, 3),  
(3, 4, 4),  
(3, 5, 2),  
(4, 6, 1),  
(5, 1, 1),  
(6, 10, 2), 
(6, 9, 3),  
(7, 7, 2);  

INSERT INTO orders (customer_id, order_date, total_price, shipping_address) VALUES 
(1, '2023-01-05', 1200.00, '123 Main St, City'),
(2, '2023-02-10', 900.00, '456 Elm St, Town'),
(3, '2023-03-15', 300.00, '789 Oak St, Village'),
(4, '2023-04-20', 150.00, '101 Pine St, Suburb'),
(5, '2023-05-25', 1800.00, '234 Cedar St, District'),
(6, '2023-06-30', 400.00, '567 Birch St, County'),
(7, '2023-07-05', 700.00, '890 Maple St, State'),
(8, '2023-08-10', 160.00, '321 Redwood St, Country'),
(9, '2023-09-15', 140.00, '432 Spruce St, Province'),
(10, '2023-10-20', 1400.00, '765 Fir St, Territory');


INSERT INTO order_items (order_id, product_id, quantity) VALUES 
(1, 1, 2),  
(1, 3, 1),  
(2, 2, 3),  
(3, 5, 2),  
(4, 4, 4),  
(4, 6, 1),  
(5, 1, 1),  
(5, 2, 2),  
(6, 10, 2), 
(6, 9, 3);  

--==============================
--Queries
--==============================


-- Update refrigerator product price to 800.

UPDATE products SET price = 800 where name = 'Refrigerator'

--Remove all cart items for a specific customer.

DELETE FROM cart WHERE customer_id = 3;

--Retrieve Products Priced Below $100.

SELECT * FROM products WHERE price <100;

--Find Products with Stock Quantity Greater Than 5.

SELECT * FROM products WHERE stockQuantity > 5;

--Retrieve Orders with Total Amount Between $500 and $1000.

SELECT * FROM orders WHERE total_price BETWEEN 500 AND 1000;

--Find Products which name end with letter ‘r’.

SELECT name, price FROM products WHERE name LIKE '%r';

--Retrieve Cart Items for Customer 5.

SELECT * FROM cart WHERE customer_id = 5;

-- Find Customers Who Placed Orders in 2023.

SELECT DISTINCT c.customer_id, c.name, c.email, o.order_date FROM customers c
JOIN orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2023;

--Determine the Minimum Stock Quantity for Each Product Category.

SELECT name, stockQuantity AS min_stock FROM products ORDER BY stockQuantity ASC;

--Calculate the Total Amount Spent by Each Customer.

SELECT c.customer_id, c.name, SUM(o.total_price) AS total_spent
FROM customers c
JOIN orders o ON c.customer_id = o.customer_id
GROUP BY c.customer_id, c.name

--Find the Average Order Amount for Each Customer.

SELECT c.customer_id, c.name, AVG(o.total_price) AS avg_order_amount
FROM customers c
JOIN orders o ON c.customer_id = o.customer_id
GROUP BY c.customer_id, c.name

--Count the Number of Orders Placed by Each Customer

SELECT c.customer_id, c.name, COUNT(o.order_id) AS total_orders
FROM customers c
JOIN orders o ON c.customer_id = o.customer_id
GROUP BY c.customer_id, c.name


--Find the Maximum Order Amount for Each Customer

SELECT c.customer_id, c.name, MAX(o.total_price) AS max_order_amount
FROM customers c
JOIN orders o ON c.customer_id = o.customer_id
GROUP BY c.customer_id, c.name

--Get Customers Who Placed Orders Totaling Over $1000

SELECT c.customer_id, c.name, SUM(o.total_price) AS total_spent
FROM customers c
JOIN orders o ON c.customer_id = o.customer_id
GROUP BY c.customer_id, c.name
HAVING SUM(o.total_price) > 1000

--Subquery to Find Products Not in the Cart.
SELECT * FROM products WHERE product_id NOT IN (SELECT DISTINCT product_id FROM cart)

--Subquery to Find Customers Who Haven't Placed Orders.
SELECT * FROM customers WHERE customer_id NOT IN (SELECT DISTINCT customer_id FROM orders)

--Subquery to Calculate the Percentage of Total Revenue for a Product.
SELECT p.product_id, p.name, (SUM(o.quantity * p.price) / (SELECT SUM(total_price) FROM orders)) * 100 AS RevenuePercentage FROM order_items o
JOIN products p ON o.product_id = p.product_id GROUP BY p.product_id, p.name

--Subquery to Find Products with Low Stock.
SELECT * FROM products WHERE stockQuantity < (SELECT AVG(stockQuantity) FROM products)

--Subquery to Find Customers Who Placed High-Value Orders.
SELECT * FROM customers WHERE customer_id IN (SELECT customer_id FROM orders 
WHERE total_price > (SELECT AVG(total_price) FROM orders))

SELECT * FROM customers 
WHERE EXISTS (
    SELECT 'x' FROM orders 
    WHERE orders.customer_id = customers.customer_id
    AND orders.total_price > (SELECT AVG(total_price) FROM orders)
);

--===================================
--End
--===================================
