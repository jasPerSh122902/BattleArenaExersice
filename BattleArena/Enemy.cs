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

        /// <summary>
        /// made enemy with all of players states.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="attackPower"></param>
        /// <param name="currentGold"></param>
        /// <param name="defensePower"></param>
        public Enemy(string name, float health, float attackPower, int currentGold, int defensePower) : base(name, health, currentGold, attackPower, defensePower)
        {
            _currentEnemyGold = currentGold;
        }

        
    }
}
