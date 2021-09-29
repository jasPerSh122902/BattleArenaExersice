using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    class Shop : Game
    {
        private float _currentGold;
        private Item[] _inventory;

        
        public Shop()
        {
            _currentGold = 100;
            _inventory = new Item[4];
        }
        public Shop(Item[] items)
        {
            _currentGold = 1000;
            _inventory = items;
        }

       
        public string[] GetShopMenuOptions()
        {
            //Create a new array with one more slot than the old array
            string[] itemNames = new string[_inventory.Length];

            //Copy the values from the old array into the new array
            for (int i = 0; i < _inventory.Length; i++)
            {
                itemNames[i] = _inventory[i].Name;

            }

            return itemNames;

        }

        //this is meant to find that ideam

         public bool Sell(Player player, int itemIndex, int playerIndex)
         {
            Item itemToBuy = _inventory[itemIndex];

            if (player.Buy(itemToBuy, playerIndex))
            {
                _currentGold -= itemToBuy.ItemCost;
                return true;
            }
            return false;
         }
    }
}
