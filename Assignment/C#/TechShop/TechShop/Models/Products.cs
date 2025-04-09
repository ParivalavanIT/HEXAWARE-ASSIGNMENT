// Class for managing product information and pricing in the TechShop system
public class Products
{
    // Unique ID for this product
    private int product_id { get; set; }

    // Name of the product
    public string product_name { get; set; }

    // Product description
    private string product_description { get; set; }

    // Product category
    private string Category { get; set; }

    // Product base price
    private double product_price;

    // Property to validate and ensure non-negative pricing
    public double productprice
    {
        get { return product_price; }
        set
        {
            if (value < 0)
            {
                throw new InvalidDataException("Product price cannot be negative.");
            }
            product_price = value;
        }
    }

    // Methods to access product data
    public int GetProductId() => product_id;
    public string GetProductName() => product_name;
    public string GetProductDescription() => product_description;
    public string GetCategory() => Category;

    // Initialize product with required information
    public Products(int product_id, string product_name, string product_description, string Category, double product_price)
    {
        this.product_id = product_id;
        this.product_name = product_name;
        this.product_description = product_description;
        this.Category = Category;
        this.productprice = product_price;
    }

    // Get formatted product information
    public string GetProductDetails()
    {
        return $"Product ID: {product_id}\nName: {product_name}\nDescription: {product_description}\nCategory: {Category}\nPrice: {productprice:C}";
    }

    // Update product information with validation
    public void UpdateProduct(string? product_name = null, string? product_description = null, string? Category = null, double? product_price = null)
    {
        if (!string.IsNullOrEmpty(product_name)) this.product_name = product_name;
        if (!string.IsNullOrEmpty(product_description)) this.product_description = product_description;
        if (!string.IsNullOrEmpty(Category)) this.Category = Category;
        if (product_price.HasValue) this.productprice = product_price.Value;
    }

    // Check if product has available stock
    public bool IsProductInStock(int StockQuantity)
    {
        return StockQuantity > 0;
    }
}