using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    class Player : Entity
    {
        private Item[] _items;
        private Item _currentItem;

        public Item CurrentItem
        {

            get
            {
                return _currentItem;
            }
        }

        public Player(string name, float health, float attackPower, float defensePower, Item[] items) : base(name, health, attackPower, defensePower)
        {
            _items = items;
            _currentItem.Name = "Nothing";
        }

        /// <summary>
        /// This allow the player to get a current item...
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public bool TryEquipItem(int Index)
        {
            //this keeps tabs on the item and the index(or were it is at in the index)...
            if (Index >= _items.Length || Index < 0)
                return false;

            //then sets item
            _currentItem = _items[Index];

            return true;
        }

        /// <summary>
        /// allow the player to know if they have a current item by...
        /// </summary>
        /// <returns></returns>
        public bool TryRemoveCurrentItem()
        {
            //looking for the current item name then if there is "Nothing" then false do it again..
            if (CurrentItem.Name == "Nothing")
                return false;

            //other wise look for current item then set it so Nothing...
            _currentItem = new Item();
            _currentItem.Name = "Nothing";

            //then return
            return true;
        }

        //returns the name of all items that player could have
        public  string[] GetItemNames()
        {
            string[] itemNames = new string[_items.Length];

            for (int i = 0; i < _items.Length; i++)
            {
                itemNames[i] = _items[i].Name;
            }

            return itemNames;
        }
    }
}
