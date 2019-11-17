using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public enum AccountType
    {
        Ordinary,
        Deposit,
    }
    public class Bank<T> where T:Account
    {
        T[] accounts;
        public string Name { get; private set; }

        public Bank (string name)
        {
            this.Name = name;
        }

        public void Open(AccountType accountType,decimal sum,AccountStateHadler addSumHandler,AccountStateHadler withdrawHadler,
            AccountStateHadler calculationHandler,AccountStateHadler closeAccountHandler,AccountStateHadler openAccountHandler)
        {
            T newAccount = null;
            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 1) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 30) as T;
                    break;
            }
            if (newAccount == null)
            {
                throw new Exception("Ошибка создания счета.");
            }
            if (accounts == null)
            {
                accounts = new T[] { newAccount };

            }else
            {
                T[] tempAccount = new T[accounts.Length + 1];
                for(int i = 0;i<accounts.Length;i++)
                {
                    tempAccount[i] = accounts[i];
                    tempAccount[tempAccount.Length - 1] = newAccount;
                }
            }

            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawHadler;
            newAccount.Calculated += calculationHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;

            newAccount.Open();
        }
        public void Put(decimal sum,int id)
        {
            T newAccount = FindAccount(id);
            if (accounts == null)
            {
                throw new Exception("Счет не найден");
            }
            newAccount.Put(sum);
        }
        public void Withdraw(decimal sum, int id)
        {
            T newAccount = FindAccount(id);
            if (accounts == null)
            {
                throw new Exception("Счет не найден");
            }
            newAccount.Withdraw(sum);
        }
            public void Close (int id)
        {
            int index;
            T account = FindAccount(id, out index);
            if (account == null)
            {
                throw new Exception("Счет не найдено.");
            }
            account.Close();

            if (accounts.Length <= 1)           
                account = null;
            else
            {
                T[] newTempAcc = new T [accounts.Length - 1];
                for(int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if(index != i)
                    {
                        newTempAcc[j++] = accounts[i];

                    }
                    
                }
                accounts = newTempAcc;

            }
        }

        public void CalculatePersantage()
        {
            if (accounts == null)
            {
                return;
            }
            for(int i = 0; i < accounts.Length; i++)
            {
                T account = accounts[i];
                account.IncrementDay();
                account.Calculate();
            }
        }


        public T FindAccount(int id)
        {
            for(int i = 0; i < accounts.Length; i++)
            {
                if(accounts[i].Id == id)
                {
                    return accounts[i];
                }
                
            }
            return null;
        }
        public T FindAccount(int id,out int index)
        {
            for(int i= 0; i < accounts.Length;i++)
            {
                if(accounts[i].Id == id)
                {
                    index = i;
                    return accounts[i];
                } 
            }
            index = -1;
            return null;
        }
    }
}
