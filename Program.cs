using System;
using System.Collections.Generic;

namespace DrinkAutomat
{
    // Set a class of drink.
    public class Drink
    {
        public int DrinkId;
        public string DrinkName;
        public float DrinkPrice;
        public int DrinkCount;

        public Drink(int _id, string _name, float _price, int _count)
        {
            DrinkId = _id;
            DrinkName = _name;
            DrinkPrice = _price;
            DrinkCount = _count;
        }
    }

    class Program
    {
        static List<Drink> drinks = new List<Drink>();
        
        static Drink currentSelectedDrink;
        static float payedSumm = 0f;
        
        static void Main(string[] args)
        {
            SetupWindow();
            FillMachine();
            UseMachine();
        }

        private static void SetupWindow()
        {
            Console.Title = "Vending Machine";
        }

        private static void UseMachine()
        {
            ResetMachine();
            SendMessageToConsole("Wellcome to our Vending Machine!");
            SendMessageToConsole("\nWe have the following drink available:");
            ReadMachineDrinks();
            SendMessageToConsole("\n");
            SendMessageToConsole("Please enter drink number to select it:");
            ReadConsoleForDrinkId();
            SendMessageToConsole($"\nPlease pay {currentSelectedDrink.DrinkPrice}$ to recive your selected drink:");
            ReadConsoleForDrinkPrice();
            SendMessageToConsole("Do you wana buy one other drink (y/n)?");
            ReadConsoleForRestart();
        }

        private static void ResetMachine()
        {
            Console.Clear();
            currentSelectedDrink = null;
            payedSumm = 0f;
        }

        private static void FillMachine()
        {
            drinks.Add(new Drink(36, "Cola", 2.2f, 5));
            drinks.Add(new Drink(24, "Bier", 3.2f, 8));
            drinks.Add(new Drink(18, "Pepsi", 1.8f, 1));
            drinks.Add(new Drink(49, "Water", 2.0f, 7));
            drinks.Add(new Drink(53, "IceTea", 2.15f, 4));
            drinks.Add(new Drink(61, "Caffe", 2.8f, 3));
        }

        private static void ReadConsoleForDrinkId()
        {
            do
            {
                string _consoleInput = Console.ReadLine();
                int _id;
                if (int.TryParse(_consoleInput, out _id))
                {
                    int _arryId = SearchArrayId(_id);
                    if (_arryId != -1)
                    {
                        currentSelectedDrink = drinks[_arryId];
                        if(currentSelectedDrink.DrinkCount > 0)
                        {
                            SendMessageToConsole($"You did selected {currentSelectedDrink.DrinkName} for {currentSelectedDrink.DrinkPrice}$!");
                            break;
                        }
                        else
                        {
                            SendMessageToConsole("Sorry, no drink found. Please enter another number:");
                            ReadConsoleForDrinkId();
                            break;
                        }
                    }
                    else
                    {
                        SendMessageToConsole("Sorry, no drink found. Please enter another number:");
                    }
                } 
                else
                {
                    SendMessageToConsole("\nNot a number, please enter number only.");
                }
            } while (true);
        }
        private static void ReadConsoleForDrinkPrice()
        {
            do
            {
                string _consoleInput = Console.ReadLine();
                float _pay;
                if (float.TryParse(_consoleInput, out _pay))
                {
                    float rest = (float)decimal.Round((decimal)(currentSelectedDrink.DrinkPrice - _pay - payedSumm), 2);
                    if (rest > 0)
                    {
                        payedSumm += _pay;
                        SendMessageToConsole($"You did pay {payedSumm}$ and need {rest}$ more to pay.");
                        ReadConsoleForDrinkPrice();
                        break;
                    }
                    else if (rest == 0)
                    {
                        SendMessageToConsole($"All done. Please take your {currentSelectedDrink.DrinkName}.");
                        currentSelectedDrink.DrinkCount--;
                        break;
                    }
                    else
                    {
                        SendMessageToConsole($"All done. Please take your {currentSelectedDrink.DrinkName} and your {-(rest)}$ change.");
                        currentSelectedDrink.DrinkCount--;
                        break;
                    }
                } 
                else
                {
                    SendMessageToConsole("\nNot a number, please enter number only.");
                }
            } while (true);
        }

        private static void ReadConsoleForRestart()
        {
            do
            {
                string _consoleInput = Console.ReadLine();
                if (_consoleInput == "y" || _consoleInput == "Y")
                {
                    UseMachine();
                    break;
                } 
                else if (_consoleInput == "n" || _consoleInput == "N")
                {
                    break;
                }
            } while (true);
        }

        private static void ReadMachineDrinks()
        {
            foreach (var drink in drinks)
            {
                if(drink.DrinkCount > 0)
                {
                    SendMessageToConsole($"({drink.DrinkId}) {drink.DrinkName} for {drink.DrinkPrice}$ [{drink.DrinkCount}]bottles left");
                }
            }
        }

        private static void SendMessageToConsole(string _value)
        {
            Console.WriteLine(_value);
        }
        
        /// <summary>
        /// Convert drink_id to array_id of object and return it. if no object found returns -1;
        /// </summary>
        /// <param name="_id"></param> entered drink id
        /// <returns></returns>
        private static int SearchArrayId(int _id)
        {
            for (int i = 0; i < drinks.Count; i++)
            {
                if(drinks[i].DrinkId == _id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}