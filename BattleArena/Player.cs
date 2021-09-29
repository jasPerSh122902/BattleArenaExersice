using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BattleArena
{

    class Player : Entity
    {
        private Item[] _inventory;
        private Item _currentItem;
        private int _currentItemIndex;


        public Item[] GetInventory()
        {
           //its only use is to get the Invenotry of the player
           return _inventory;
        }
     
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

            _currentItem.Name = "Nothing";
            _currentItemIndex = -1;
        }

        //made player but now with item[] array and the base key word
        public Player(Item[] items) : base()
        {
            _currentItem.Name = "Nothing";
            _inventory = items;
            _currentItemIndex = -1;
        }

        //made player using entity as a means to set the states...
        public Player(string name, float health, float attackPower, float currentGold, int defensePower, Item[] items, string job) : base(name, health, currentGold, attackPower, defensePower)
        {
            //stated other varables that are needed

            _inventory = items;
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
            if (Index >= _inventory.Length || Index < 0)
                return false;


            _currentItemIndex = Index;
            //then sets item
            _currentItem = _inventory[_currentItemIndex];

            return true;
        }
        public bool Buy(Item item )
        {
            Entity _entity = new Entity("bob", 100, 15, 225, 100);

            //get the amount of gold the player has and consompares it to cost...
            if (_entity._currentGold >= item.ItemCost)
            {
                //when in loop suptracts the player gold from cost..
                _entity._currentGold -= item.ItemCost;

                _currentItemIndex++;
                //finds item in Index and adds it to inventory
                return true;
            }
            //...returns false if player gold is not enough.
            return false;
        }

        public bool Buy(Item item, int inventoryIndex)
        {
            Entity _entity = new Entity("bob", 100, 15, 225, 100);
            
            //get the amount of gold the player has and consompares it to cost...
            if (_entity._currentGold >= item.ItemCost)
            {
                //when in loop suptracts the player gold from cost..
                _entity._currentGold -= item.ItemCost;

                _currentItemIndex++;
                //finds item in Index and adds it to inventory
                return true;
            }
            //...returns false if player gold is not enough.
            return false;
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
            //gets a string array and call it item names...
            string[] itemNames = new string[_inventory.Length];

            //then made a for loop that gos through the array...
            for (int i = 0; i < _inventory.Length; i++)
            {
                //and gives the items i or items 1,2,3 a name
                itemNames[i] = _inventory[i].Name;
            }

            //return the names of items
            return itemNames;
        }

        //makes sure to save the players progress...
        public override void Save(StreamWriter writer)
        {
            //saves the job which catacgorizes the player
            writer.WriteLine(Job);
            //uses the writer as a save point.
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
