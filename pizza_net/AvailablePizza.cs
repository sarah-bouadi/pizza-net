namespace pizza_net;

public class AvailablePizza
{
    public static readonly Dictionary<string, Pizza> GetAvailablePizza = new Dictionary<string, Pizza>
    {
        { "Regina", 
            new Pizza("Regina", 8.0m, new List<Ingredient>
            {  
                new Ingredient("tomate", new IngredientQuantityWeight(150)),
                new Ingredient("mozzarella", new IngredientQuantityWeight(125)),
                new Ingredient("fromage râpé", new IngredientQuantityWeight(100)),
                new Ingredient("Jambon", new IngredientQuantitySlice(2)),
                new Ingredient("champignons frais", new IngredientQuantityPiece(4)),
                new Ingredient("huile d'olive", new IngredientQuantitySpoon(2))
            })
        },
        { "4 Saisons", 
            new Pizza("4 Saisons", 9.0m,
                new List<Ingredient>
                {  
                    new Ingredient("tomate", new IngredientQuantityWeight(150)),
                    new Ingredient("mozzarella", new IngredientQuantityWeight(125)),
                    new Ingredient("Jambon", new IngredientQuantitySlice(2)),
                    new Ingredient("champignons frais", new IngredientQuantityWeight(100)),
                    new Ingredient("poivron", new IngredientQuantityPiece(0.5m)),
                    new Ingredient("olives", new IngredientQuantityWrist(1))
                })
        },
        { "Végétarienne", 
            new Pizza("Végétarienne", 7.5m, 
                new List<Ingredient>
                {  
                    new Ingredient("tomate", new IngredientQuantityWeight(150)),
                    new Ingredient("mozzarella", new IngredientQuantityWeight(100)),
                    new Ingredient("courgette", new IngredientQuantityPiece(0.5m)),
                    new Ingredient("poivron jaune", new IngredientQuantityPiece(1)),
                    new Ingredient("tomates cerises", new IngredientQuantityPiece(6)),
                    new Ingredient("olives", new IngredientQuantityWrist(1))
                })
        }
    };
}