using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    class Entity
    {
        private string _name;
        private float _health;
        private float _attackPower;
        private float _defensePower;


        public Entity()
        {
            _name = "Default";
            _health = 0;
            _attackPower = 0;
            _defensePower = 0;
        }

        public Entity(string name, float health, float attackPower, float defensePower)
        {
            _name = name;
            _health = health;
            _attackPower = attackPower;
            _defensePower = defensePower;
        }
    }
}
