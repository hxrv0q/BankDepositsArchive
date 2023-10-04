using BankDeposits.Mvc.Models;

namespace BankDeposits.Mvc.Services;

public class DepositorService : BaseService<Depositor>
{
    public DepositorService(BankDepositsContext context) : base(context) { }
}