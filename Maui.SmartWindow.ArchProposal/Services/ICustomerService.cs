namespace Maui.SmartWindow.ArchProposal.Services;

public interface ICustomerService
{
    string LastName { get; }
    string GetCustomerName();
    void SetLastName(string lastName);
}
