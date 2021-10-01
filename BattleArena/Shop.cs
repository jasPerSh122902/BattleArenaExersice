using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    class Shop : Game
    {
        private float _Gold;
        private Item[] _inventory;
        private Game _game;
        private Player _player;

        public Shop()
        {
            _Gold = 100;
            _inventory = new Item[0];
        }
        public Shop(params Item[] items)
        {
            _Gold = 1000;
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

        /// <summary>
        /// The bool purches is there to make sure the transaction is happening...
        /// then checks the Index that is there to make sure if the player wants to leave or buy something...
        /// Then gets the players current Gold and compares it to the inventory items Cost
        /// sets purches to true and increament gold..
        /// Then gos to buy.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="_ItemIndex"></param>
        /// <returns></returns>
        public bool Sell(Player player, int _ItemIndex)
        {
            //Sets that there has not be a sell to false
            bool purches = false;
            
            //the first check to see if ItemIndex is 4 then alto matiky go out of the loop.
            if (_ItemIndex == 4)
            {
                purches = true;
            }

            //if players current gold is greater or equal to the items index cost. . . 
            else if (player.currentGold >= _inventory[_ItemIndex].Cost)
            {
                //. . .Percues has been done 
                purches = true;
                //. . .Store has gained gold in return 
                _Gold += _inventory[_ItemIndex].Cost;
                //player took the item placing it in its inventory
                player.Buy(_inventory[_ItemIndex]);
            }

            // returns false if no sell was done true is transaction was succsessful
            return purches;
        }
        /// <summary>
        /// makes a name for a new array and then gets all the item names and puts them in the array.
        /// </summary>
        /// <returns></returns>
        public string[] GetItemNames()
        {
            //creats new array to collect the item names
            string[] names = new string[_inventory.Length];

            //for every item in that index. . .
            for (int i = 0; i < _inventory.Length; i++)
                //. . . put the name of that index in the name array
                names[i] = _inventory[i].Name;
            // returns The names of all the items in the store
            return names;

        }
    }
}