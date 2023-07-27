namespace pizza_net;

public class Pizza
{
    public string Name { get; }
    public decimal Price { get; }
    // public Dictionary<string, decimal> Ingredients { get; }
    public List<Ingredient> Ingredients { get; }

    public Pizza(string name, decimal price, List<Ingredient> ingredients)
    {
        Name = name;
        Price = price;
        Ingredients = ingredients;
    }

    public void DisplayAddingIngredients()
    {
        foreach (var ingredient in Ingredients)
        {
            Console.WriteLine($"Ajouter {ingredient._name}");
        }
    }
}