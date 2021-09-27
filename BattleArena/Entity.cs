using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BattleArena
{
    class Entity
    {
        private string _name;
        private float _health;
        private float _attackPower;
        private float _defensePower;
        public float _currentGold;


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

        public  float currentGold
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
        public Entity(string name, float health, float attackPower, float defensePower, float currentGold)
        {
            _name = name;
            _health = health;
            _attackPower = attackPower;
            _defensePower = defensePower;
            _currentGold = currentGold;
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


        public float Attack(Entity defender)
        {
            //meant to take a defender place holder or varable then returns the damage from the take damage function.
            return defender.TakeDamage(AttackPower);
        }

        public float TakeGold(float enemyGold)
        {
            float goldTaken = enemyGold - currentGold;

            if (goldTaken < 0)
            {
                goldTaken = 0;
                Console.WriteLine("");
            }

            _currentGold += goldTaken;

            return goldTaken;
        }

        public float Gold(Entity attacker)
        {
            return attacker.TakeGold(_currentGold);
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