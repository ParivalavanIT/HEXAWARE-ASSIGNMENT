using TechShop.Exceptions;
using TechShop.Models;
using System.Data.SqlClient;

namespace TechShop
{
    public class DatabaseConnector : IDisposable
    {
        private static readonly string connectionString = "data source = RAVEN\\SQLEXPRESS01;initial catalog = TechShop;integrated security = true;";
        private SqlConnection connection;
        private bool disposed = false;

        public DatabaseConnector()
        {
            connection = new SqlConnection(connectionString);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (connection != null)
                    {
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Dispose();
                        connection = null;
                    }
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DatabaseConnector()
        {
            Dispose(false);
        }

        public SqlConnection GetConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        //Inserting customer details method...

        public bool InsertCustomer(Customers customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            using (var transaction = GetConnection().BeginTransaction())
            {
                try
                {
                    // Check if email already exists
                    string checkquery = "Select count(*) From Customers Where cust_email = @Email";
                    using (var checkcmd = new SqlCommand(checkquery, GetConnection()))
                    {
                        checkcmd.Transaction = transaction;
                        checkcmd.Parameters.AddWithValue("@Email", customer.GetEmail());
                        int count = (int)checkcmd.ExecuteScalar();

                        if (count > 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    string query = "INSERT into Customers (cust_id, first_name, last_name, cust_email, cust_phone, cust_address) VALUES (@CustomerID, @FirstName, @LastName, @Email, @Phone, @Address)";
                    using (var cmd = new SqlCommand(query, GetConnection()))
                    {
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@CustomerID", customer.GetCustomerId());
                        cmd.Parameters.AddWithValue("@FirstName", customer.GetFirstName());
                        cmd.Parameters.AddWithValue("@LastName", customer.GetLastName());
                        cmd.Parameters.AddWithValue("@Email", customer.GetEmail());
                        cmd.Parameters.AddWithValue("@Phone", customer.GetPhone());
                        cmd.Parameters.AddWithValue("@Address", customer.GetAddress());

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            transaction.Commit();
                            return true;
                        }
                        transaction.Rollback();
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new DatabaseSqlException($"Database error while inserting customer: {ex.Message}");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Unexpected error while inserting customer: {ex.Message}");
                }
            }
        }

        //Inserting products details method..
        public bool InsertProduct(Products product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using (var transaction = GetConnection().BeginTransaction())
            {
                try
                {
                    // Check if product name already exists
                    string checkquery = "Select count(*) From Products Where prod_name = @ProductName";

                    using (var checkcmd = new SqlCommand(checkquery, GetConnection()))
                    {
                        checkcmd.Transaction = transaction;
                        checkcmd.Parameters.AddWithValue("@ProductName", product.GetProductName());
                        int count = (int)checkcmd.ExecuteScalar();

                        if (count > 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    string query = "INSERT INTO Products (prod_id, prod_name, prod_description, prod_price) " +
                                   "VALUES (@ProductId, @ProductName, @ProductDesc, @ProductPrice)";

                    using (var cmd = new SqlCommand(query, GetConnection()))
                    {
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@ProductId", product.GetProductId());
                        cmd.Parameters.AddWithValue("@ProductName", product.GetProductName());
                        cmd.Parameters.AddWithValue("@ProductDesc", product.GetProductDesc());
                        cmd.Parameters.AddWithValue("@ProductPrice", product.GetProductPrice());

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            transaction.Commit();
                            return true;
                        }
                        transaction.Rollback();
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new DatabaseSqlException($"Database error while inserting product: {ex.Message}");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Unexpected error while inserting product: {ex.Message}");
                }
            }
        }

        // Updating proudcts details 
        public bool UpdateProduct(Products product)
        {
            try
            {
                using (var conn = GetConnection())
                using (var transaction = conn.BeginTransaction())
                {
                    string query = "UPDATE Products SET prod_name = @ProductName, " +
                                   "prod_description = @ProductDesc, Category = @ProductCateg, prod_price = @ProductPrice " +
                                   "WHERE prod_id = @ProductId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", product.GetProductId());
                        cmd.Parameters.AddWithValue("@ProductName", product.GetProductName());
                        cmd.Parameters.AddWithValue("@ProductDesc", product.GetProductDesc());
                        cmd.Parameters.AddWithValue("@ProductCateg", product.GetCategory());
                        cmd.Parameters.AddWithValue("@ProductPrice", product.GetProductPrice());

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException sqlex)
            {
                throw new DatabaseSqlException("Sql Connection Error!");
                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Order Processing Failed: " + ex.Message);
                return false;
            }
        }

        // To display all orders

        public List<Orders> GetAllOrders()
        {
            List<Orders> orders = new List<Orders>();

            try
            {
                using (var con = GetConnection())
                {
                    con.Open();
                    string query = @"SELECT o.order_id, o.customer_id, o.order_date, o.total_amount, o.order_status,
                                    c.first_name, c.last_name, c.email, c.phone, c.address
                             FROM Orders o
                             JOIN Customers c ON o.customer_id = c.customer_id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int orderId = reader.GetInt32(0);
                                int customerId = reader.GetInt32(1);
                                DateTime orderDate = reader.GetDateTime(2);
                                double totalAmount = reader.GetDouble(3);
                                string orderStatus = reader.GetString(4);

                                // Create customer object
                                string firstName = reader.GetString(5);
                                string lastName = reader.GetString(6);
                                string email = reader.GetString(7);
                                string phone = reader.GetString(8);
                                string address = reader.GetString(9);

                                Customers customer = new Customers(customerId, firstName, lastName, email, phone, address);

                                // Create order object
                                Orders order = new Orders(orderId, customer, orderDate, totalAmount);
                                order.SetOrderStatus(orderStatus);

                                orders.Add(order);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("SQL error while fetching orders: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Unexpected error while fetching orders: " + ex.Message);
            }

            return orders;
        }

        //Inserting orders method after processing the order...

        public bool ProcessingOrder(Orders order)
        {
            try
            {
                using (var conn = GetConnection())
                using (var transaction = conn.BeginTransaction())
                {


                    // 🔹 1. Calculate total amount from order details
                    double total_amnt = 0;

                    foreach (OrderDetails detail in order.GetOrderDetailsList)
                    {
                        double price = detail.GetProduct().GetProductPrice();
                        total_amnt += price * detail.GetQuantity();
                    }

                    // 🔹 2. Set the total amount in the order object (optional depending on your class logic)
                    order.SetTotalAmount(total_amnt); // Make sure you have this method in your Orders class

                    // 🔹 3. Insert into Orders table
                    string orderQuery = "INSERT INTO Orders (order_id, cust_id, order_date, total_amnt, order_status) " +
                                        "VALUES (@OrderId, @CustomerId, @OrderDate, @TotalAmount, @OrderStatus)";

                    using (SqlCommand orderCmd = new SqlCommand(orderQuery, conn))
                    {
                        orderCmd.Parameters.AddWithValue("@OrderId", order.GetOrderId());
                        orderCmd.Parameters.AddWithValue("@CustomerId", order.GetCustomer().GetCustomerId());
                        orderCmd.Parameters.AddWithValue("@OrderDate", order.GetOrderDate());
                        orderCmd.Parameters.AddWithValue("@TotalAmount", total_amnt);
                        orderCmd.Parameters.AddWithValue("@OrderStatus", order.GetOrderStatus());

                        orderCmd.ExecuteNonQuery();
                    }

                    // 🔹 4. Insert OrderDetails and Update Inventory
                    foreach (OrderDetails detail in order.GetOrderDetailsList)
                    {
                        string detailQuery = "Insert Into order_details (order_detail_id, order_id, prod_id, quantity) " +
                                             "Values (@DetailId, @OrderId, @ProductId, @Quantity)";

                        using (SqlCommand detailCmd = new SqlCommand(detailQuery, conn))
                        {
                            detailCmd.Parameters.AddWithValue("@DetailId", detail.GetOrderDetailId());
                            detailCmd.Parameters.AddWithValue("@OrderId", detail.GetOrderId());
                            detailCmd.Parameters.AddWithValue("@ProductId", detail.GetProductId());
                            detailCmd.Parameters.AddWithValue("@Quantity", detail.GetQuantity());

                            detailCmd.ExecuteNonQuery();
                        }

                        string inventoryQuery = "Update Inventory SET quantityInStock = quantityInStock - @Quantity " +
                                                "Where prod_id = @ProductId";

                        using (SqlCommand inventoryCmd = new SqlCommand(inventoryQuery, conn))
                        {
                            inventoryCmd.Parameters.AddWithValue("@Quantity", detail.GetQuantity());
                            inventoryCmd.Parameters.AddWithValue("@ProductId", detail.GetProductId());

                            inventoryCmd.ExecuteNonQuery();
                        }
                    }


                    return true;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Order Processing Failed: " + ex.Message);
                return false;
            }
        }


        // To get all products list

        public List<Products> GetAllProducts()
        {
            List<Products> products = new List<Products>();

            try
            {
                using (var conn = GetConnection())
                {
                    string query = "SELECT * FROM Products";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Products product = new Products(
                                    Convert.ToInt32(reader["prod_id"]),
                                    reader["prod_name"].ToString(),
                                    reader["prod_description"].ToString(),
                                    reader["Category"].ToString(),
                                    Convert.ToDouble(reader["prod_price"])
                                );

                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving products: " + ex.Message);
            }

            return products;
        }


        // To get product by the specific ID

        public Products GetProductById(int productId)
        {
            Products prod = null;

            using (var conn = GetConnection())
            {


                string query = "SELECT * From Products WHERE prod_id = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ProductId"]);
                        string name = reader["Name"].ToString();
                        string desc = reader["Description"].ToString();
                        string category = reader["Category"].ToString();
                        double price = Convert.ToDouble(reader["Price"]);

                        prod = new Products(id, name, desc, category, price);
                    }

                    reader.Close();
                }


            }

            return prod;
        }

        // To get all Inventory lists..

        public void ViewInventory()
        {
            Console.WriteLine("==== Inventory List ====");

            try
            {
                using (var conn = GetConnection())
                {
                    string query = @"Select i.prod_id, p.prod_name, i.quantityInStock
                             From Inventory i
                             JOIN Products p ON i.prod_id = p.prod_id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("Inventory is empty.");
                            return;
                        }

                        while (reader.Read())
                        {
                            int productId = Convert.ToInt32(reader["prod_id"]);
                            string name = reader["prod_name"].ToString();
                            int stock = Convert.ToInt32(reader["quantityInStock"]);

                            Console.WriteLine($"Product ID: {productId}");
                            Console.WriteLine($"Product Name: {name}");
                            Console.WriteLine($"Quantity in Stock: {stock}");
                            Console.WriteLine("---------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error viewing inventory: " + ex.Message);
            }
        }


        // To check order status...Paid, Failed or Pending..

        public List<string> GetCustomerOrderStatuses(int cust_id)
        {
            List<string> statuses = new List<string>();

            try
            {
                using (var conn = GetConnection())
                {
                    string query = "Select order_id, order_status From Orders Where cust_id = @CustomerId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", cust_id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int order_id = dr.GetInt32(0);
                                string order_status = dr.GetString(1);
                                statuses.Add($"Order {order_id}: {order_status}");
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                // this method should always need to return a list so adding this...
                statuses.Add("Error retrieving order statuses.");
            }

            return statuses;
        }



        //Inserting orderdetails method

        public bool InsertOrderDetail(OrderDetails detail)
        {
            using (var conn = GetConnection())
            using (var transaction = conn.BeginTransaction())
            {
                string query = "INSERT INTO OrderDetails (order_detail_id, order_id, prod_id, quantity) " +
                               "VALUES (@OrderDetailId, @OrderId, @ProductId, @Quantity)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderDetailId", detail.GetOrderDetailId());
                    cmd.Parameters.AddWithValue("@OrderId", detail.GetOrderId());
                    cmd.Parameters.AddWithValue("@ProductId", detail.GetProductId());
                    cmd.Parameters.AddWithValue("@Quantity", detail.GetQuantity());

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        //Inserting inventory details for database connection

        public bool InsertInventory(Inventory inventory)
        {
            try
            {
                using (var conn = GetConnection())
                using (var transaction = conn.BeginTransaction())
                {
                    string query = "INSERT INTO Inventory (inventory_id, product_id, quantity_in_stock, last_stock_update) " +
                                   "VALUES (@InventoryId, @ProductId, @Quantity, @LastUpdate)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@InventoryId", inventory.GetInventoryId());
                        cmd.Parameters.AddWithValue("@ProductId", inventory.GetProductId());
                        cmd.Parameters.AddWithValue("@Quantity", inventory.GetQuantityInStock());
                        cmd.Parameters.AddWithValue("@LastUpdate", inventory.GetLastStockUpdate());

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                return false;
            }
        }


        //To update stock in the inventory

        public static bool UpdateStock(int productId, int newQuantity)
        {
            try
            {
                using (SqlConnection conn = getConnection())
                {
                    string query = "UPDATE Inventory SET quantityInStock = quantityInStock + @Quantity, lastStockUpdate = @Updated " +
                                   "where prod_id = @ProductId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                        cmd.Parameters.AddWithValue("@Updated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ProductId", productId);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error updating stock: " + ex.Message);
                return false;
            }
        }

        // setting a product to discontinued

        public static bool DiscontinueProduct(int productId)
        {
            try
            {
                using (SqlConnection conn = getConnection())
                {
                    string query = "Update Products SET is_active = 0 where prod_id = @ProductId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error discontinuing product: " + ex.Message);
                return false;
            }
        }

        //To remove cancelled orders..

        public static void RemoveCancelledOrders()
        {
            try
            {
                using (SqlConnection conn = getConnection())
                {
                    conn.Open();

                    string query = "DELETE From Orders WHERE order_status = 'Cancelled'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} cancelled orders removed from the database.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error removing cancelled orders: " + ex.Message);
            }
        }

        //Inserting payment record method for database

        public static bool InsertPaymentRecord(PaymentRecord payment)
        {
            using (SqlConnection conn = getConnection())
            {
                string query = "INSERT into PaymentRecord (payment_id, order_id, amount, status) " +
                               "Values (@PaymentId, @OrderId, @Amount, @Status)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PaymentId", payment.GetPaymentId());
                    cmd.Parameters.AddWithValue("@OrderId", payment.GetOrderId());
                    cmd.Parameters.AddWithValue("@Amount", payment.GetAmount());
                    cmd.Parameters.AddWithValue("@Status", payment.GetStatus());

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        // Sales reporting

        public static void GenerateSalesReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (SqlConnection conn = getConnection())
                {
                    string query = @"SELECT order_id, order_date, total_amnt
                             From Orders 
                             Where order_date Between @StartDate AND @EndDate";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("Sales Report from {0} to {1}", startDate, endDate);
                            Console.WriteLine("-------------------------------------------");

                            decimal totalSales = 0;

                            while (reader.Read())
                            {
                                int orderId = reader.GetInt32(0);
                                DateTime orderDate = reader.GetDateTime(1);
                                decimal amount = reader.GetDecimal(2);

                                Console.WriteLine($"Order ID: {orderId}, Date: {orderDate}, Amount: {amount}");
                                totalSales += amount;
                            }

                            Console.WriteLine("----------------------------------------------------");
                            Console.WriteLine($"Total Sales: {totalSales}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating sales report: " + ex.Message);
            }
        }

        // to update customer informations

        public static bool UpdateCustomerInfo(Customers customer)
        {
            try
            {
                using (SqlConnection conn = getConnection())
                {
                    // Check for duplicate email (excluding current customer's own email)
                    string checkQuery = "SELECT count(*) From Customers where cust_id != @CustomerId";

                    // Proceed to update
                    string updateQuery = @"UPDATE Customers 
                                   SET first_name = @FirstName, 
                                       last_name = @LastName,
                                       cust_email = @Email,
                                       cust_phone = @Phone,
                                       cust_address = @Address 
                                   WHERE cust_id = @CustomerId";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", customer.GetCustomerId());
                        cmd.Parameters.AddWithValue("@FirstName", customer.GetFirstName());
                        cmd.Parameters.AddWithValue("@LastName", customer.GetLastName());
                        cmd.Parameters.AddWithValue("@Email", customer.GetEmail());
                        cmd.Parameters.AddWithValue("@Phone", customer.GetPhone());
                        cmd.Parameters.AddWithValue("@Address", customer.GetAddress());

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating customer info: " + ex.Message);
                return false;
            }
        }

        // To search products..

        public static List<Products> SearchProducts(string keyword)
        {
            List<Products> products = new List<Products>();

            try
            {
                using (SqlConnection conn = getConnection())
                {
                    string query = @"SELECT * From Products 
                                     where prod_name LIKE @Keyword OR prod_description LIKE @Keyword";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Products product = new Products(
                                    Convert.ToInt32(reader["prod_id"]),
                                    reader["prod_name"].ToString(),
                                    reader["prod_description"].ToString(),
                                    reader["Category"].ToString(),
                                    Convert.ToDouble(reader["prod_price"])
                                );

                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Search Error: " + ex.Message);
            }

            return products;
        }
        // Record Payment

        public static bool RecordPayment(PaymentRecord payment)
        {
            try
            {
                using (SqlConnection conn = getConnection())
                {
                    conn.Open();

                    string query = @"INSERT INTO PaymentRecords 
                            (payment_id, order_id, amount) 
                             VALUES (@PaymentId, @OrderId, @Amount)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PaymentId", payment.GetPaymentId());
                        cmd.Parameters.AddWithValue("@OrderId", payment.GetOrderId());

                        cmd.Parameters.AddWithValue("@Amount", payment.GetAmount());


                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error recording payment: " + ex.Message);
                return false;
            }
        }

    }
}
