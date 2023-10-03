using BankDeposits.Mvc.Models;
using BankDeposits.Mvc.Services;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Controllers;

public class DepositorController : BaseController<Depositor, DepositorService>
{
    public DepositorController(DepositorService service) : base(service)
    {
    }
}