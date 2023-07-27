namespace pizza_net;

public class IngredientQuantitySome: IngredientQuantity
{
    public decimal Value { get; }
    
    public IngredientQuantitySome(decimal value)
    {
        Value = value;
    }
    public string DisplayQuantity()
    {
        return getQuantityType();
    }
    
    public string getQuantityType()
    {
        return "quelques";
    }
}