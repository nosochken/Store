public class Warehouse : GoodsDisplayer
{
    private Dictionary<Good, int> _goods = new Dictionary<Good, int>();

    public IReadOnlyDictionary<Good, int> Goods => _goods;

    public override void ShowGoods()
    {
        Display(_goods, "на складе");
    }

    public void Delive(Good good, int amount)
    {
        if (good == null)
            throw new ArgumentNullException(nameof(good));
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (_goods.TryGetValue(good, out int availableAmount))
            _goods[good] = availableAmount + amount;
        else
            _goods.Add(good, amount);
    }

    public void PickUpOrderedGoods(IReadOnlyDictionary<Good, int> orderedGoods)
    {
        if (orderedGoods == null)
            throw new ArgumentNullException(nameof(orderedGoods));

        foreach (KeyValuePair<Good, int> orderedGood in orderedGoods)
        {
            if (_goods.TryGetValue(orderedGood.Key, out int availableAmount))
            {
                _goods[orderedGood.Key] = availableAmount - orderedGood.Value;

                if (_goods[orderedGood.Key] <= 0)
                    _goods.Remove(orderedGood.Key);
            }
        }
    }
}