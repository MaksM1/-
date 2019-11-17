using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {

        protected internal event AccountStateHadler Withdrawed;

        protected internal event AccountStateHadler Added;

        protected internal event AccountStateHadler Opened;

        protected internal event AccountStateHadler Calculated;
        protected internal event AccountStateHadler Closed;

        static int counter = 0;
        protected int _days = 0;

        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;

        }
        public decimal Sum { get; private set; }
        public int Percentage { get; private set; }
        public int Id { get; private set; }

        private void CallEvent (AccountEventArgs e,AccountStateHadler hadler)
        {
            if(e != null)
            {
                hadler?.Invoke(this, e);
            }
        }

        protected virtual void OnOpened (AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnClosed (AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }

        protected virtual void OnAdded (AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnWithdraw(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        } 
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }
        public virtual void Put (decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs($"На счет поступило:{sum}", sum));
        }
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum > sum)
            {
                Sum -= sum;
                result = Sum;
                OnWithdraw(new AccountEventArgs($"Сумм {sum} снята со счета {Id}", sum));
            }else
            {
                OnWithdraw(new AccountEventArgs($"Недостаточно средств на счету {Id}", sum));
            }
            return result;
        }
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Открыт новый счет!Id счета {Id}", Sum));
        }
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Cчет закрыт!Ваша текущая сумма {Sum}", Sum));
        }
        protected internal void IncrementDay()
        {
            _days++;
        }
        protected internal virtual  void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum += increment;
            OnCalculated(new AccountEventArgs($"Начислены проценты в размере {increment}", increment));
        }
    }
}
