using BankDeposits.Mvc.Data;
using BankDeposits.Mvc.Models;

namespace BankDeposits.Mvc.Services;

public class DepositorService : AbstractService<Depositor>
{
    public DepositorService(AppDbContext context) : base(context) { }
}