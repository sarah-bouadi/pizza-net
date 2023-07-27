namespace pizza_net;

public class IngredientQuantitySpoon : IngredientQuantity
{
    public decimal Value { get; }
    
    public IngredientQuantitySpoon(decimal value)
    {
        Value = value;
    }
    public string DisplayQuantity()
    {
        string manySpoon = "";
        if (Value > 1)
        {
            manySpoon = "s";
        }
        return $"{Value} cuillière{manySpoon} à soupe";    
    }
    
    public string getQuantityType()
    {
        return "cuillière à soupe";
    }
}