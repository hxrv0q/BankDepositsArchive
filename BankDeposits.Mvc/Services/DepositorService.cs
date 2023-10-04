using BankDeposits.Mvc.Models;

namespace BankDeposits.Mvc.Services;

public class DepositorService : AbstractService<Depositor>
{
    public DepositorService(BankDepositsContext context) : base(context) { }
}