namespace pizza_net;

public interface IInvoiceOutput
{
    void GenerateInvoice(Dictionary<string, int> orderDetails, string targetFilePath);
}