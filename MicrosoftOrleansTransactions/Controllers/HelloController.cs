using AccountTransfer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Threading.Tasks;

namespace MicrosoftOrleansWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly IGrainFactory _grainFactory;
        private readonly ITransactionClient _transactionClient;

        public HelloController(IGrainFactory grainFactory, ITransactionClient transactionClient)
        {
            _grainFactory = grainFactory;
            _transactionClient = transactionClient;
        }

        [HttpGet("transfer/{fromUser}/{toUser}")]
        public async Task MakeTransfer(string fromUser, string toUser)
        {
            var transferAmount = Random.Shared.Next(200);

            var fromAccount = _grainFactory.GetGrain<IAccountGrain>(fromUser);
            var toAccount = _grainFactory.GetGrain<IAccountGrain>(toUser);

            await _transactionClient.RunTransaction(
                TransactionOption.Create,
                async () =>
                {
                    await fromAccount.Withdraw(transferAmount);
                    await toAccount.Deposit(transferAmount);
                });

            var fromBalance = await fromAccount.GetBalance();
            var toBalance = await toAccount.GetBalance();

            Console.WriteLine(
                $"We transferred {transferAmount} credits from {fromUser} to " +
                $"{toUser}.\n{fromUser} balance: {fromBalance}\n{toUser} balance: {toBalance}\n");
        }
    }
}