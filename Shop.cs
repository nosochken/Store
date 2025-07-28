public class Shop
{
    private readonly Warehouse _warehouse;

    private List<Cart> _carts = new List<Cart>();

    public Shop(Warehouse warehouse)
    {
        if (warehouse == null)
            throw new ArgumentNullException(nameof(warehouse));

        _warehouse = warehouse;
    }

    public Cart Cart()
    {
        Cart? freeCart = _carts.FirstOrDefault(cart => cart.IsBusy == false);

        if (freeCart == null)
        {
            freeCart = new Cart();
            _carts.Add(freeCart);
        }

        freeCart.Prepare(_warehouse.Goods);
        freeCart.GoodsOrdered += PickUpGoodFromWarehouse;

        return freeCart;
    }

    private void PickUpGoodFromWarehouse(Cart cart, IReadOnlyDictionary<Good, int> goods)
    {
        if (cart == null)
            throw new ArgumentNullException(nameof(cart));

        if (goods == null)
            throw new ArgumentNullException(nameof(goods));

        cart.GoodsOrdered -= PickUpGoodFromWarehouse;

        _warehouse.PickUpOrderedGoods(goods);
    }
}