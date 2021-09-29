using System;
using System.Collections.Generic;

namespace DrinkAutomat
{
    class Program
    {
        static Dictionary<int, string> drinkArray = new Dictionary<int, string>();
        static Dictionary<int, float> priceArray = new Dictionary<int, float>();

        static void Main(string[] args)
        {
            SetupAutomat();
            SendMessageToConsole("Wellcome to our Drink automat!");
            SendMessageToConsole("Please enter (r) or (R) to show all our drinks.");
            ReadConsoleForDrinkList();
            SendMessageToConsole("Please enter drink number to recive it:");
            ReadConsoleForDrinkId();
        }
        private static void SetupAutomat()
        {
            drinkArray.Add(36, "Cola");
            drinkArray.Add(24, "Bier");
            drinkArray.Add(18, "Pepsi");
            drinkArray.Add(19, "Water");
            drinkArray.Add(56, "Vodka");

            priceArray.Add(36, 2.10f);
            priceArray.Add(24, 3.20f);
            priceArray.Add(18, 1.00f);
            priceArray.Add(19, 1.20f);
            priceArray.Add(56, 6.20f);
        }

        private static void SendMessageToConsole(string _value)
        {
            Console.WriteLine(_value);
        }

        private static void ReadConsoleForDrinkId()
        {
            do
            {
                string _consoleInput = Console.ReadLine();
                int _key;
                if (int.TryParse(_consoleInput, out _key))
                {
                    if (drinkArray.ContainsKey(_key))
                    {
                        SendMessageToConsole($"Thank you, you did selected {drinkArray[_key]} for {priceArray[_key]}$");
                        break;
                    } 
                    else
                    {
                        SendMessageToConsole("Sorry, no drink found");
                    }
                }

            } while (true);
        }
        private static void ReadConsoleForDrinkList()
        {
            do
            {
                string _consoleInput = Console.ReadLine();
                if (_consoleInput == "R" || _consoleInput == "r")
                {
                    SendMessageToConsole("\nWe have the following drink available:");
                    ReadAutomatDrinks();
                    SendMessageToConsole("\n");
                    break;
                }

            } while (true);
        }

        private static void ReadAutomatDrinks()
        {
            foreach (var drink in drinkArray)
            {
                Console.WriteLine($"({drink.Key}) {drink.Value} for {priceArray[drink.Key]}$");
            }
        }
    }
}
