﻿namespace Trading.Logic
{
    using Trading.Infrastructure;
    using Trading.Interface;

    public static class Home
    {
        private static readonly IView viewProvider;

        static Home()
        {
            viewProvider = SettingsByProvider.viewProvider;
        }

        public static void Run()
        {
            int UserSelect = viewProvider.IndexMain();
            switch (UserSelect)
            {
                case 1:
                    Transaction.Run();
                    break;
                case 2:
                    viewProvider.PrintAllUsers(User.ListUsers());
                    break;
                case 3:
                    User.AddUser();
                    break;
                case 4:
                    viewProvider.PrintAllStocks(Stock.ListStocks());
                    break;
                case 5:
                    viewProvider.PrintAllStocks(Stock.ListStocks());
                    Stock.ChangeStockPrice();
                    break;
                case 6:
                    viewProvider.PrintOrangeZone(User.Zone(0));
                    break;
                case 7:
                    viewProvider.PrintBlackZone(User.Zone(1));
                    break;
            }
        }
    }
}
