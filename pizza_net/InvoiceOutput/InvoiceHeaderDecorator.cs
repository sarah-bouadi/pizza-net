namespace pizza_net;

public class InvoiceHeaderDecorator : InvoiceOutputDecorator
{
    public InvoiceHeaderDecorator(IInvoiceOutput invoiceOutput) : base(invoiceOutput)
    {
    }

    public override void GenerateInvoice(Dictionary<string, int> orderDetails, string targetFilePath)
    {
        AddHeader();
        base.GenerateInvoice(orderDetails, targetFilePath);
        AddFooter();
    }

    private void AddHeader()
    {
        Console.WriteLine("===== Invoice Header =====");
        Console.WriteLine(" Welcome to Our PizzaNet !");
        Console.WriteLine("===========================\n");
    }
    
    private void AddFooter()
    {
        Console.WriteLine("\n===== Invoice Printed =====");
        Console.WriteLine("===========================\n");
    }
}
