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
        private Item[] _inventory;
        private int _currentGold;
        private Enemy _enemy;


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

        public string Job { get; set; }

        //made a instence of player
        public Player()
        {
            _inventory = new Item[0];

            _currentItem.Name = "Nothing";
            _currentItemIndex = -1;
        }

        //made player but now with item[] array and the base key word
        public Player(Item[] items) : base()
        {
            _currentItem.Name = "Nothing";
            _items = items;
            _currentItemIndex = -1;
        }

        //made player using entity as a means to set the states...
        public Player(string name, float health, float attackPower, float currentGold, int defensePower, Item[] items, string job) : base(name, health, currentGold, attackPower, defensePower)
        {
            //stated other varables that are needed
            _inventory = _items;
            _items = items;
            _currentItem.Name = "Nothing";
            //and the job is set here
            Job = job;
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

        public bool Buy(Item item, int inventoryIndex)
        {
            //get the amount of gold the player has and consompares it to cost...
            if(_currentGold >= item.ItemCost)
            {
                //when in loop suptracts the player gold from cost..
                _currentGold -= item.ItemCost;

                //finds item in Index and adds it to inventory
                _inventory[inventoryIndex] = item;

                return true;
            }
            //...returns false if player gold is not enough.
            return false;
        }
        //alows the attacker to get the gold from the enemy 
        public float Gold(Entity attacker)
        {
            //is meant to take gold
            return attacker.TakeGold(_enemy._currentEnemyGold);
        }

        public Item[] GetInventory()
        {
            //its only use is to get the Invenotry of the player
            return _inventory;
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
            writer.WriteLine(Job);
            base.Save(writer);
            writer.WriteLine(_currentItemIndex);
        }

        public override bool Load(StreamReader reader)
        {
            //if the base loading function dos not load return false..
            if (!base.Load(reader))
                return false;

            //if the loading function works then gos to CurrentItemIndex if that dos not load return false...
            if (!int.TryParse(reader.ReadLine(), out _currentItemIndex))
                return false;

            //then return the Item Index wether the top two were successful.
            //This one returns wether the item was equipped or not.
            return TryEquipItem(_currentItemIndex);
        }
    }
}
