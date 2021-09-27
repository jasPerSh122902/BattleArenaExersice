using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    class Shop : Game
    {
        private Item[] _items;
        private Player _player;
        private Game _game;


        public Shop()
        {
            Item[] items;
        }
        public void DisplayShopMenuOptions()
        {
            int choice = _game.GetInput(" This is the Shop Hi and I hope you stay. ", GetShopMenuOptions());
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
        //this is meant to see if the item in the shop can be bought
        /// public bool Buy()

        //this is meant to find that ideam

        // public float Sell()

        // this is meant to allow the player to give the item back to the shop
        //this is a probeble issue tho be warned.



    }


}
