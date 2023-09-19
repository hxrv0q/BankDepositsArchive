using BankDeposits.App.Http;
using BankDeposits.App.Configuration;
using BankDeposits.App.Database;
using BankDeposits.App.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = AppConfig.LoadConfiguration();
var connectionString = configuration.GetConnectionString("BankDepositsDatabase");

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(connectionString);

var dbContext = new AppDbContext(optionsBuilder.Options);
var service = new AppService(dbContext);

var server = new WebServer(service);
var host = server.BuildWebHost();

host.Run();