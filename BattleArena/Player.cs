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

        public Player(string name, float health, float atackPower, float defensePower, Item[] items, float attackPower) : base(name, health, attackPower, defensePower)
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
    }
}
