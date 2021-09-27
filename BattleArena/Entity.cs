using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BattleArena
{
    class Entity
    {
        private Item[] _inventory;
        private Shop _shop;
        private Player _player;
        private Enemy _enemy;
        private string _name;
        private float _health;
        private float _attackPower;
        private float _defensePower;
        public int _currentGold;


        public string Name
        {
            get { return _name; }
        }

        public float Health
        {
            get { return _health; }
        }

        public virtual float AttackPower
        {
            get { return _attackPower; }
        }

        public virtual float DefensePower
        {
            get { return _defensePower; }
        }

        public virtual int currentGold
        {
            get { return _currentGold; }
        }

        /// <summary>
        /// makes a instrence of Entity for a original idea...
        /// </summary>
        public Entity()
        {

            _name = "Default";
            _health = 0;
            _attackPower = 0;
            _defensePower = 0;
            _currentGold = 0;
        }

        /// <summary>
        /// uses the Entity instrence to then add on to it for the following varables.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="attackPower"></param>
        /// <param name="defensePower"></param>
        /// /// <param name= "currentGold"></param>
        public Entity(string name, float health, float attackPower, float defensePower, int currentGold)
        {
            _name = name;
            _health = health;
            _attackPower = attackPower;
            _defensePower = defensePower;
            _currentGold = currentGold;
            _inventory = new Item[0];
        }

        public float TakeDamage(float damageAmount)
        {
            //made a varable damage taken then make it equial..
            float damageTaken = damageAmount - DefensePower;

            //just in case the damage taken is less than 0 to not heal the player or enemy it go to zero.
            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            //incraments health from damage.
            _health -= damageTaken;

            //returns the damage.
            return damageTaken;
        }


        //this is the attack function it makes the defender take the damage...
        public float Attack(Entity defender)
        {
            //then the attackers attackpower is put in with the takeDamage function.
            return defender.TakeDamage(AttackPower);
        }


        public float TakeGold(int enemyGold)
        {
            int goldTaken = enemyGold;

            //incraments gold
            _currentGold += goldTaken;

            return goldTaken;
        }
 
        /// <summary>
        /// is meant to save all states that the player currently has and puts them in a file to save.
        /// </summary>
        /// <param name="writer"></param>

        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_health);
            writer.WriteLine(_attackPower);
            writer.WriteLine(_defensePower);
        }

        /// <summary>
        /// is meant to take all of the states from Save and give them back to player.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public virtual bool Load(StreamReader reader)
        {
            _name = reader.ReadLine();

            if (!float.TryParse(reader.ReadLine(), out _health))
                return false;

            if (!float.TryParse(reader.ReadLine(), out _attackPower))
                return false;

            if (!float.TryParse(reader.ReadLine(), out _defensePower))
                return false;

            return true;
        }
    }
}