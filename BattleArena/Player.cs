using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BattleArena
{
    class Player : Entity
    {
        private Item[] _items;
        private Item _currentItem;
        private int _currentItemIndex;
        private string _job;

        public override float DefensePower
        {
            get
            {
                if (_currentItem.Type == ItemType.DEFENSE)
                    return base.DefensePower + CurrentItem.StatBoost;
                return base.DefensePower;
            }
        }
        public override float AttackPower
        {
            get
            {
                if (_currentItem.Type == ItemType.ATTACK)
                    return base.AttackPower + CurrentItem.StatBoost;
                return base.AttackPower;
            }
        }

        public Item CurrentItem
        {

            get
            {
                return _currentItem;
            }
        }

        public string Job
        {
            get
            {
                return _job;
            }
            set
            {
                _job = value;
            }
               
        }

        public Player()
        {
            _items = new Item[0];
            _currentItem.Name = "Nothing";
            _currentItemIndex = -1;
        }

        public Player(Item[] items): base()
        {
            _currentItem.Name = "Nothing";
            _items = items;
            _currentItemIndex = -1;
        }

        public Player(string name, float health, float attackPower, float defensePower, Item[] items, string job) : base(name, health, attackPower, defensePower)
        {
            _items = items;
            _currentItem.Name = "Nothing";
            _job = job;
            _currentItemIndex = -1;
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


            _currentItemIndex = Index;
            //then sets item
            _currentItem = _items[_currentItemIndex];

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

            _currentItemIndex = -1;
            //then return
            return true;
        }

        //returns the name of all items that player could have
        public string[] GetItemNames()
        {
            string[] itemNames = new string[_items.Length];

            for (int i = 0; i < _items.Length; i++)
            {
                itemNames[i] = _items[i].Name;
            }

            return itemNames;
        }

        public override void Save(StreamWriter writer)
        {
            writer.WriteLine(_job);
            base.Save(writer);
            writer.WriteLine(_currentItemIndex);
        }

        public override bool Load(StreamReader reader)
        {
            //if the base loading function dos not load return false..
            if (!base.Load(reader))
                return false;

            //if the loading function works then gos to CurrentItemIndex if that dos not load return false...
            if(!int.TryParse(reader.ReadLine(), out _currentItemIndex))
                return false;


            //then return the Item Index wether the top two were successful.
            //This one returns wether the item was equipped or not.
           return TryEquipItem(_currentItemIndex);
        }
    }
}
