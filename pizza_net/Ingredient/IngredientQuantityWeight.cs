namespace pizza_net;

public class IngredientQuantityWeight : IngredientQuantity
{
    public IngredientQuantityWeight(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }
    public string DisplayQuantity()
    {
        return $"{Value} g";
    }
}