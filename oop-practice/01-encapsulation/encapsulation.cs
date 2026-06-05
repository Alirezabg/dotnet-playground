public class Product
{
    private string Name { get; set; }
    private decimal Price
    {
        get ;
        set
        {
            if (value < 0)
            { throw new ArgumentException("Price cannot be negative."); }
            else { Price = value; }
        }
    }
    private int Stock
    {
        get ;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Stock cannot be negative.");
            }
            else
            {

                Stock = value;
            }
        }
    }
    public int Id { get; private set; }
}