namespace Maui.SmartWindow.ArchProposal.Services;

internal class CustomerService : ICustomerService
{
    public string LastName { get; private set; }

    public string GetCustomerName()
    {
        return this.GetHashCode().ToString();
    }

    public void SetLastName(string lastName)
    {
        this.LastName = lastName;
    }
}
