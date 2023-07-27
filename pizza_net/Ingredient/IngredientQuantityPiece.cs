namespace pizza_net;

public class IngredientQuantityPiece : IngredientQuantity
{
    public decimal Value { get; }
    
    public IngredientQuantityPiece(decimal value)
    {
        Value = value;
    }
    public string DisplayQuantity()
    {
        string manyPieces = "";
        if (Value > 1)
        {
            manyPieces = "s";
        }
        return $"{Value} {getQuantityType()}{manyPieces}";
    }

    public string getQuantityType()
    {
        return "pièce";
    }
}