using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public delegate void AccountStateHadler(object sender, AccountEventArgs e);
    public class AccountEventArgs
    {
       public string Massage { get; private set; }
       public decimal Sum { get; private set; } 

        public AccountEventArgs (string _mes,decimal sum)
        {
            Massage = _mes;
            Sum = sum;
        }
    }
}
