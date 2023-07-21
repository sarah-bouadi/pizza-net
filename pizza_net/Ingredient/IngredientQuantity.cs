namespace pizza_net;

public interface IngredientQuantity
{
    public decimal Value { get; }
    string DisplayQuantity();
}