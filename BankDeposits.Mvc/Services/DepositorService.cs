using BankDeposits.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Services;

public class DepositorService : BaseService<Depositor>
{
    public DepositorService(BankDepositsContext context) : base(context)
    { }

    protected override DbSet<Depositor> Entities => Context.Depositors;
}