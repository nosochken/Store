public class Order
{
    public string Paylink { get; }
    
    public Order()
    {
        Paylink = CreateLink();
    }
    
    private string CreateLink()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        Random random = new Random();

        char[] result = Enumerable.Range(0, 10)
            .Select(symbol => chars[random.Next(chars.Length)])
            .ToArray();

        return new string(result);
    }
}