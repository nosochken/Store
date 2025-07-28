public class Cart : GoodsDisplayer
{
    private IReadOnlyDictionary<Good, int> _availableGoods = new Dictionary<Good, int>();
    private Dictionary<Good, int> _selectedGoods = new Dictionary<Good, int>();

    public event Action<Cart,IReadOnlyDictionary<Good, int>>? GoodsOrdered;
    
    public bool IsBusy { get; private set; }
    
    public void Prepare(IReadOnlyDictionary<Good, int> goods)
    {
        if (goods == null)
            throw new ArgumentNullException(nameof(goods));
            
        _availableGoods = goods;
        IsBusy = true;
    }
    
    public override void ShowGoods()
    {
        Display(_selectedGoods, "в корзине");
    }
    
    public void Add(Good good, int amount)
    {
        if (good == null)
            throw new ArgumentNullException(nameof(good));
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (_availableGoods.TryGetValue(good, out int availableAmount))
        {
            if (availableAmount < amount)
                throw new InvalidOperationException(nameof(amount));
        }
        else
        {
            throw new KeyNotFoundException(nameof(good));
        }

        _selectedGoods.Add(good, amount);
    }
    
    public Order Order()
    {
        if (_selectedGoods.Count == 0)
            throw new InvalidOperationException(nameof(Order));

        GoodsOrdered?.Invoke(this, _selectedGoods);
        _selectedGoods.Clear();

        IsBusy = false;

        return new Order();
    }
}