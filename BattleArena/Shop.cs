using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    class Shop : Game
    {
        private float _currentGold;
        private Entity _entity;
        private Item[] _items;
        private Player _player;
        private Item[] _inventory;
        

        public Shop(Item[] items)
        {
            _inventory = items;
        }

        public void DisplayShopMenuOptions()
        {
            Console.WriteLine("You got " + _player.currentGold + " Gold.");
            Console.WriteLine("Your bag: ");


            int choice = GetInput(" This is the Shop Hi and I hope you stay. ", GetShopMenuOptions());


            if (choice >= 0 && choice < GetShopMenuOptions().Length)
            {
                if (Sell(_player, choice))
                {
                    Console.WriteLine(" Good choice of  purchise. ");
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine(" You got no gold get more. ");
                    Console.Clear();
                }
            }

        }
        public string[] GetShopMenuOptions()
        {
            //Create a new array with one more slot than the old array
            string[] itemNames = new string[_items.Length + 2];

            //Copy the values from the old array into the new array
            for (int i = 0; i < _items.Length; i++)
            {
                itemNames[i] = _items[i].Name;

            }

            return itemNames;

        }

        //this is meant to find that ideam

         public bool Sell(Player player, int itemIndex)
         {
            if (player.currentGold >= _items[itemIndex].ItemCost)
            {

                _currentGold += _items[itemIndex].ItemCost;
                _entity.Buy(_items[itemIndex]);
                return true;

            }
            return false;
         }

        // this is meant to allow the player to give the item back to the shop
        //this is a probeble issue tho be warned.



    }


}
