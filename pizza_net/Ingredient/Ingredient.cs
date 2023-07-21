namespace pizza_net;

public class Ingredient
{
    public string _name;
    public IngredientQuantity _IngredientQuantity;

    public Ingredient(string name, IngredientQuantity ingredientQuantity)
    {
        _name = name;
        _IngredientQuantity = ingredientQuantity;
    }

    public void DisplayIngredient()
    {
        Console.WriteLine($"{_name} {_IngredientQuantity.DisplayQuantity()}");
    }
}