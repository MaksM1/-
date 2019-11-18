using System;
using BankLibrary;
namespace BankAplicatoin
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account> ( "МойБанк" );

            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("1.Открыть счет в банке \t 2.ААААААААААААВывести средства \t 3.Добавить счет");
                Console.WriteLine("4.Закрыть счет \t 5.Пропустить день \t 6.Выйти из програмы");
                Console.WriteLine("Введите номер пункта");
                Console.ForegroundColor = color;
                try
                {
                    int command = int.Parse(Console.ReadLine());
                    switch (command)
                    {
                        case 1: OpenAccount(bank);
                            break;
                        case 2: Withdraw(bank);
                            break;
                        case 3:Put(bank);
                            break;
                        case 4:CloseAccount(bank);
                            break;
                        case 5:break;
                        case 6: alive = false;
                            continue;
                    }
                    bank.CalculatePersantage();
                }
                catch(Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }        
            }    
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для создания счета");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Выберите тип счета:1.До востребования 2.Депозит");
            AccountType accountType;
            int type = Convert.ToInt32(Console.ReadLine());
            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open(accountType, sum, AddSumHandler, WithdrawHandler, (o, e) => Console.WriteLine(e.Massage),
                CloseAccountHandler, OpenAccountHandler);
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для выыода средств");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Укажите Id");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму которую хотите положить на счет");
            decimal sum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("ВВедите Id счета");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Put(sum, id);

        }
        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Введите id счета которое нужно закрить");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Close(id);
        }

        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Massage);
        }

        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Massage);
        }

        private static void WithdrawHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Massage);
            if (e.Sum > 0)
                Console.WriteLine("Идем тратить деньги");
        }

        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Massage);

        }
    }
}
