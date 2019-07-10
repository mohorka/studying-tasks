using System;
using System.Collections.Generic;
namespace CoffeeShop
{
    /// <summary>
    /// Object "coffee" with its characteristics, s.a way of cooking,adds and sugar.
    /// </summary>
    public class Coffee
    {
        public string CookMethod;                       //the way of cooking coffee (ex.,espresso).
        public Dictionary<string, double> Additives;    //all adds put in coffee.
        public int Sugar;                               //amount of sugar.

        public Coffee(string cookMethod, Dictionary<string, double> additives, int sugar)
        {
            CookMethod = cookMethod;
            Additives = additives;
            Sugar = sugar;
        }
        public Coffee(string cookMethod, int sugar)
        {
            CookMethod = cookMethod;
            Sugar = sugar;
            Additives = new Dictionary<string, double>
            {
                {"without additives" ,0}
            };

        }

    }
    /// <summary>
    /// Coffeemachine that produces object "coffee",keeps in memory ways of cooking and additives.
    /// All characteristics can be changed by admin-mode.
    /// </summary>
    public class CoffeeMachine
    {
        private const string Password = "allmem";       //password to enter admin's mode.
        public Dictionary<string, double> Additives;    //all adds able to coffeemachine.
        public Dictionary<string, double> CookMethods;  //all ways to cook coffee able to machine.
        public CoffeeMachine()
        {
            CookMethods = new Dictionary<string, double>
            {
                {"espresso",100 },
                {"filter-coffee",100 },
                {"cappuccino",150},
                {"americano",120 },
                {"flat-white",180 },



            };
            Additives = new Dictionary<string, double>
            {
                {"vanila syrop",20},
                {"almond syrop",20 },
                {"extra milk",20},
                {"cinnamon",5 },
                {"salt-caramel syrop",20}

            };

        }

        /// <summary>
        /// Creates coffee with additives.
        /// </summary>
        /// <param name="cookMethod">Method of cooking particular coffee.</param>
        /// <param name="additives">All additives in particular coffee.</param>
        /// <param name="sugar">Amount of sugar in particular coffee.</param>
        /// <returns></returns>
        public Coffee GetCoffee(string cookMethod, Dictionary<string, double> additives, int sugar)
        {
            Coffee coffee = new Coffee(cookMethod, additives, sugar);
            return coffee;

        }

        /// <summary>
        /// Creates coffee without additives.
        /// </summary>
        /// <param name="cookMethod">Method of cooking particular coffee.</param>
        /// <param name="sugar">Amount of sugar in particular coffee.</param>
        /// <returns></returns>
        public Coffee GetCoffee(string cookMethod, int sugar)
        {
            Coffee coffee = new Coffee(cookMethod, sugar);
            return coffee;
        }

        /// <summary>
        /// Adds new method of cooking coffee and its price. Part of admin-mode.
        /// </summary>
        /// <param name="method">New method of cooking.</param>
        /// <param name="price">Price of new method.</param>
        public void AddMethod(string method, double price)
        {
            try
            {
                CookMethods.Add(method, price);
            }
            catch
            {
                Console.WriteLine("It seems, this method is already into machine");
            }
        }

        /// <summary>
        /// Adds new additive and its price. Part of admin-mode.
        /// </summary>
        /// <param name="add">New additive.</param>
        /// <param name="price">Price of new additive.</param>
        public void AddAdditive(string add, double price)
        {
            try
            {
                Additives.Add(add, price);
            }
            catch
            {
                Console.WriteLine("It seems, this additive is already into machine");
            }
        }

        /// <summary>
        /// Changes the price of particular method. Part of admin-mode.
        /// </summary>
        /// <param name="method">Method of cooking which price has to be changed.</param>
        /// <param name="newPrice">New price.</param>
        public void ChangeMethodPrice(string method, double newPrice) { CookMethods[method] = newPrice; }

        /// <summary>
        /// Changes the price of particular additive. Part of admin-mode.
        /// </summary>
        /// <param name="additive">Additive which price has to be changed.</param>
        /// <param name="newPrice">New price.</param>
        public void ChangeAdditivePrice(string additive, double newPrice) { CookMethods[additive] = newPrice; }

        /// <summary>
        /// Deletes cooking method.
        /// </summary>
        /// <param name="method">Cooking method that has to be deleted.</param>
        public void DeleteMethod(string method)
        {
            try
            {
                CookMethods.Remove(method);
            }
            catch
            {
                Console.WriteLine("It seems, this method is already out machine");
            }
        }

        /// <summary>
        /// Deletes additive.
        /// </summary>
        /// <param name="additive">Additive that has to be deleted.</param>
        public void DeleteAdditive(string additive)
        {
            try
            {
                CookMethods.Remove(additive);
            }
            catch
            {
                Console.WriteLine("It seems, this additive is already out machine");
            }
        }

        /// <summary>
        /// Shows methods of cooking to client.
        /// </summary>
        public void ShowMenu()
        {
            Console.WriteLine("Coffee - Price");
            foreach (var menu in CookMethods)
                Console.WriteLine("{0} - {1}", menu.Key, menu.Value);

        }

        /// <summary>
        /// Shows all of able additives to client.
        /// </summary>
        public void ShowAdds()
        {
            Console.WriteLine("Additives - Price");
            foreach (var adds in Additives)
                Console.WriteLine("{0} - {1}", adds.Key, adds.Value);
        }

        /// <summary>
        /// Check for correct answer.
        /// </summary>
        /// <param name="password">Word,which person input to coffeemachine as a password.</param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            return password.Equals(Password);
        }
    }

    class Program
    {
        /// <summary>
        /// Includes all methods for work with client (i.e coffee preparation).
        /// </summary>
        /// <param name="coffeeMachine">Particular coffeemachine.</param>
        public static void ClientMode(CoffeeMachine coffeeMachine)
        {
            string method;
            int sugar;
            int addNumber;
            double summary = 0;

            Console.WriteLine("What kind of coffee would you like?");
            coffeeMachine.ShowMenu();
            method = Console.ReadLine();

            Console.WriteLine("And what about any additives?Choose, how many do you want");
            coffeeMachine.ShowAdds();
            addNumber = Convert.ToInt32(Console.ReadLine());
            Dictionary<string, double> additives = new Dictionary<string, double>(addNumber);
            if (addNumber != 0)
            {
                Console.WriteLine("Great!Now choose them");
                for (int i = 0; i < addNumber; ++i)
                {
                    additives.Add(Console.ReadLine(), i);
                }
            }
            Console.WriteLine("Last question for now-how many sugar cubes you want in your drink?");
            sugar = Convert.ToInt32(Console.ReadLine());

            Coffee coffee = addNumber != 0 ? coffeeMachine.GetCoffee(method, additives, sugar)
                                           : coffeeMachine.GetCoffee(method, sugar);


            summary += coffeeMachine.CookMethods[method];
            if (addNumber != 0)
            {
                foreach (var add in additives)
                    summary += coffeeMachine.Additives[add.Key];
            }

            Console.WriteLine("You coffee's cost is {0}", summary);
            Console.WriteLine("Chosen additives and their price:");
            if (addNumber == 0)
                Console.WriteLine("No additives - 0");
            else
                foreach (var add in additives)
                    Console.WriteLine("{0} - {1}", add.Key, coffeeMachine.Additives[add.Key]);
            Console.WriteLine("Sugar amount:{0}", sugar);
            Console.WriteLine("Thanks for visiting!");



        }

        /// <summary>
        /// Includes all methods for admin's work (i.e change machine's menu).
        /// </summary>
        /// <param name="coffeeMachine">Particular coffeemachine.</param>
        public static void AdminMode(CoffeeMachine coffeeMachine)
        {
            Console.WriteLine(@"Welcome back! Choose the options:
                                        1-add new method;
                                        2-add new additive;
                                        3-delete method;
                                        4-delete additive;
                                        5-change the cost of method;
                                        6-change the cost of additive;");
            int answer = Convert.ToInt32(Console.ReadLine());
            switch (answer)
            {
                case 1:
                    Console.WriteLine("Enter new method and its price");
                    string method = Console.ReadLine();
                    double metPrice = Convert.ToInt32(Console.ReadLine());
                    coffeeMachine.AddMethod(method, metPrice);
                    break;
                case 2:
                    Console.WriteLine("Enter new additive and its price");
                    string additive = Console.ReadLine();
                    double addPrice = Convert.ToInt32(Console.ReadLine());
                    coffeeMachine.AddAdditive(additive, addPrice);
                    break;
                case 3:
                    Console.WriteLine("Enter method");
                    string deleteMethod = Console.ReadLine();
                    coffeeMachine.DeleteMethod(deleteMethod);
                    break;
                case 4:
                    Console.WriteLine("Enter additive");
                    string deleteAdditive = Console.ReadLine();
                    coffeeMachine.DeleteAdditive(deleteAdditive);
                    break;
                case 5:
                    Console.WriteLine("Enter method and its new price");
                    string oldMethod = Console.ReadLine();
                    double newMetPrice = Convert.ToInt32(Console.ReadLine());
                    coffeeMachine.ChangeMethodPrice(oldMethod, newMetPrice);
                    break;
                case 6:
                    Console.WriteLine("Enter method and its new price");
                    string oldAdditive = Console.ReadLine();
                    double newAddPrice = Convert.ToInt32(Console.ReadLine());
                    coffeeMachine.ChangeAdditivePrice(oldAdditive, newAddPrice);
                    break;
                default:
                    Console.WriteLine("Incorrect option!");
                    break;

            }
            Console.WriteLine("Do you want to continue?");
            if (Console.ReadLine().Equals("yes", StringComparison.OrdinalIgnoreCase))
                AdminMode(coffeeMachine);
            else
                Console.WriteLine("Good day!");
        }
        static void Main(string[] args)
        {
            CoffeeMachine PMCoffee = new CoffeeMachine();

            Console.WriteLine("Are you client?");
            string answer = Console.ReadLine();

            if (answer.Equals("yes", StringComparison.OrdinalIgnoreCase))
                ClientMode(PMCoffee);
            else
            {
                Console.WriteLine("If you're admin,please enter the password");
                if (!PMCoffee.CheckPassword(Console.ReadLine()))
                    Console.WriteLine("Incorrect password!");
                else AdminMode(PMCoffee);
            }
            Console.ReadLine();
        }
    }
}
