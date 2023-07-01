namespace pizza_net;

public class IngredientQuantitySlice : IngredientQuantity
{
    public decimal Value { get; }
    
    public IngredientQuantitySlice(decimal value)
    {
        Value = value;
    }
    public string DisplayQuantity()
    {
        string manySlices = "";
        if (Value > 1)
        {
            manySlices = "s";
        }
        return $"{Value} tranche{manySlices}";
    }
}