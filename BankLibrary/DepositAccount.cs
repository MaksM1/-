using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DepositAccount:Account
    {
        public DepositAccount(decimal sum,int percentage) : base(sum, percentage)
        {
        }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Новый депозитный счет открыт!Id счета:{this.Id}", this.Sum));
        }
        public override void Put(decimal sum)
        {
            
            if (_days%30>=0 && _days%30 <= 5)
            {
                base.Put(sum);
            }
            else OnAdded(new AccountEventArgs($"Вы можете положить деньги после 30-ти дневного периода в течении 5 дней.", 0));
        }

        public override decimal Withdraw(decimal sum)
        {
            if (_days % 60 >= 0 && _days % 60 <= 5)
                return base.Withdraw(sum);
            else OnWithdraw(new AccountEventArgs($"Вы можете cнять деньги после 60-ти дневного периода в течении 5 дней.", 0));
            return 0;
        }
    }
}
