namespace pizza_net;

public class IngredientQuantityWrist : IngredientQuantity
{
    public decimal Value { get; }
    
    public IngredientQuantityWrist(decimal value)
    {
        Value = value;
    }
    public string DisplayQuantity()
    {
        string manyWrist = "";
        if (Value > 1)
        {
            manyWrist = "s";
        }
        return $"{Value} {getQuantityType()}{manyWrist}";
        
    }
    
    public string getQuantityType()
    {
        return "poignée";
    }
}