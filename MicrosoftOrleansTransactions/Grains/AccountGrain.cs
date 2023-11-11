using AccountTransfer.Interfaces;
using Orleans;
using Orleans.Concurrency;
using Orleans.Transactions.Abstractions;
using System;
using System.Threading.Tasks;

namespace AccountTransfer.Grains
{
    [GenerateSerializer]
    public record class Balance
    {
        [Id(0)]
        public int Value { get; set; } = 1_000;
    }

    [Reentrant]
    public sealed class AccountGrain : Grain, IAccountGrain
    {
        private readonly ITransactionalState<Balance> _balance;

        public AccountGrain([TransactionalState("balance")] ITransactionalState<Balance> balance)
        {
            _balance = balance;
        }

        public async Task Deposit(int amount)
        {
            await _balance.PerformUpdate(balance => balance.Value += amount);
        }

        public async Task Withdraw(int amount)
        {
            await _balance.PerformUpdate(balance =>
            {
                if (balance.Value < amount)
                {
                    throw new InvalidOperationException(
                        $"Withdrawing {amount} credits from account " +
                        $"\"{this.GetPrimaryKeyString()}\" would overdraw it." +
                        $" This account has {balance.Value} credits.");
                }

                balance.Value -= amount;
            });
        }

        public Task<int> GetBalance()
        {
            return _balance.PerformRead(balance => balance.Value);
        }
    }
}