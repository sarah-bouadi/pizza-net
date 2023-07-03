using System.Text;

namespace pizza_net
{
    public class Program
    {
        public static void Main()
        {
            // 3.5 Comportement attendu du programme
            while (true)
            {
                // 3.1 Prise en compte des commandes 
                var currentOrder = new Order();
                var orderDetails = currentOrder.ProcessOrder();

                // 3.2 Edition d'une facture 
                currentOrder.EditInvoice(orderDetails);
                
                // 3.3. Edition des instructions de préparation
                currentOrder.EditInstruction(orderDetails);
                
                // 3.6 Extension du programme : Listing des ingrédients utilisés
                currentOrder.ListUsedIngredient(orderDetails);
            }
        }
    }
}
