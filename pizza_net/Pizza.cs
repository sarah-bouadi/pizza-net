namespace PizzeriaApp;

public class Pizza
{
    public string Name { get; }
    public decimal Price { get; }
    public Dictionary<string, decimal> Ingredients { get; }

    public Pizza(string name, decimal price, Dictionary<string, decimal> ingredients)
    {
        Name = name;
        Price = price;
        Ingredients = ingredients;
    }
}