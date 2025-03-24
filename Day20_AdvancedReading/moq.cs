[HttpPost]
public ActionResult<string> CheckOut(Order order)
{
    var result = _cartService.ValidateCart(order);
    return Ok(result);
}

public class CartService : ICartService
{
    IPaymentService _paymentService;
    public CartService(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public string ValidateCart(Order order)
    {
        if (order.CartItems.Count < 1)
            return "Invalid Cart";
        if (order.CartItems.Any(x => x.Quantity < 0 || x.Quantity > 10))
            return "Invalid Product Quantity";

        return _paymentService.ChargeAndShip(order);
    }
}