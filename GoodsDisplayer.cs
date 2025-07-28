public abstract class GoodsDisplayer
{
    public abstract void ShowGoods();

    protected void Display(Dictionary<Good, int> goods, string place)
    {
        Console.WriteLine($"Товары {place}:");

        foreach (KeyValuePair<Good, int> good in goods)
            Console.WriteLine($"{good.Key.Name} в количестве {good.Value} шт.");
    }
}