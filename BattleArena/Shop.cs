using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    class Shop : Game
    {
        private float _currentGold;
        private Entity _entity;
        private Item[] _shopItems;
        

        public Shop(Item[] items)
        {
            _currentGold = 1000;
            _shopItems  = items;
        }

       
        public string[] GetShopMenuOptions()
        {
            //Create a new array with one more slot than the old array
            string[] itemNames = new string[_shopItems.Length + 2];

            //Copy the values from the old array into the new array
            for (int i = 0; i < _shopItems.Length; i++)
            {
                itemNames[i] = _shopItems[i].Name;

            }

            return itemNames;

        }

        //this is meant to find that ideam

         public bool Sell(Player player, int itemIndex)
         {
            if (player.currentGold >= _shopItems[itemIndex].ItemCost)
            {
                //incraments current gold of player from the item cost
                _currentGold += _shopItems[itemIndex].ItemCost;
                //uses the buy funtion to find and incrament the shop items.
                _entity.Buy(_shopItems[itemIndex]);
                //...if possible 
                return true;

            }
            return false;
         }

        // this is meant to allow the player to give the item back to the shop
        //this is a probeble issue tho be warned.



    }


}
