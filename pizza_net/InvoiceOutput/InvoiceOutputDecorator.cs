namespace pizza_net;

public abstract class InvoiceOutputDecorator : IInvoiceOutput
{
    protected IInvoiceOutput _invoiceOutput;

    public InvoiceOutputDecorator(IInvoiceOutput invoiceOutput)
    {
        _invoiceOutput = invoiceOutput;
    }

    public virtual void GenerateInvoice(Dictionary<string, int> orderDetails, string targetFilePath)
    {
        _invoiceOutput.GenerateInvoice(orderDetails, targetFilePath);
    }
}
