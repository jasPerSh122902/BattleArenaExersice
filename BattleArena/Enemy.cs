using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    class Enemy : Entity
    {

        public Enemy()
        {
            _currentEnemyGold = 0;
        }
        public int _currentEnemyGold;
        public Enemy(string name, float health, float attackPower, int currentGold, int defensePower) : base(name, health, currentGold, attackPower, defensePower)
        {
            _currentEnemyGold = currentGold;
        }

        
    }
}
