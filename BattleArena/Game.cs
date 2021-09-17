﻿using System;
using System.Collections.Generic;
using System.Text;

//the is the commint  for update
//this is a test
//the is the commint  for update

namespace BattleArena
{
    /// <summary>
    /// Represents any entity that exists in game
    /// </summary>

    public struct Item
    {
        public string Name;
        public float StatBoost;
    }
    class Game
    {

        string playerName = "none";
        bool gameOver = false;
        public int CurrentScene = 0;

        public int CurrentEnemyIndex = 0;
        public Enemy _currentEnemy;

        string input = Console.ReadLine();

        private Player _player;
        private Player[];

        private Entity[] _enemies;
        Enemy Claud;
        Enemy Rob;
        Enemy Wompus;
        Enemy Theo;
        Enemy[] enemies;


        private Item[] _gunnerItems;
        private Item[] _raiderItems;

        void ResetCurrentEnemies()
        {
            CurrentEnemyIndex = 0;

            _currentEnemy = enemies[CurrentEnemyIndex];
            CurrentEnemyIndex++;
        }

        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
            Start();
            while (!gameOver)
            {
                Update();
            }

            End();
        }

        public void InitilizeItems()
        {
            //Gunner items
            Item bigGun = new Item { Name = "Big Gun", StatBoost = 5 };
            Item bigShield = new Item { Name = "Big Shield", StatBoost = 15 };

            //Raider Items
            Item axe = new Item { Name = "Axe", StatBoost = 18 };
            Item forceShied = new Item { Name = "Force Shield", StatBoost = 3 };

            //Initialize arrays
            _gunnerItems = new Item[] { bigGun, bigShield };
            _raiderItems = new Item[] { axe, forceShied };
        }

        public void InitilizeEnemy()
        {
            Entity claud = new Entity("claud", 15, 15, 20);

            Entity rob = new Entity("Rob", 15, 15, 20);

            Entity wompus = new Entity("Wompus", 15, 20, 15);

            Entity theo = new Entity("Theo", 15, 20, 15);

        }
        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        public void Start()
        {
            _player = new Player(_player, 50, 80, 25, _gunnerItems);


            _player = new Player(_player, 20, 45, 25, _raiderItems,);


            enemies = new Enemy[] { Rob, Wompus, Theo, Claud };

            ResetCurrentEnemies();
        }

        Enemy EnemyIndex(int currentEnemyIndex)
        {
            Enemy enemy;
            enemy.name = "None";
            enemy.attack = 1;
            enemy.defense = 1;
            enemy.health = 1;


            if (currentEnemyIndex == 0)
            {
                enemy = Claud;
            }

            else if (currentEnemyIndex == 1)
            {
                enemy = Rob;
            }
            else if (currentEnemyIndex == 2)
            {
                enemy = Wompus;
            }
            else if (currentEnemyIndex == 3)
            {
                enemy = Theo;
            }

            return enemy;
        }
        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {

            DisplayCurrentScene();
            InitilizeEnemy();
        }

        void End()
        {
            Console.WriteLine("GoodBye :] c u later");
        }
        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        bool TheAtemptAtEnd()
        {
            bool theEnd = CurrentEnemyIndex >= enemies.Length;

            if (theEnd)
            {
                CurrentScene = 2;
            }

            return theEnd;
        }

        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the player can choose</param>
        /// <param name="option2">The second option the player can choose</param>
        /// <returns></returns>
        int GetInput(string description, params string[] options)
        {

            string input = "";
            int inputRecieved = -1;

            while (inputRecieved == -1)
            {
                //Print options that are there
                Console.WriteLine(description);

                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + options[i]);
                }

                Console.WriteLine("> ");

                //gets the input form player
                input = Console.ReadLine();

                //sees if player has typed a number of eny kind
                if (int.TryParse(input, out inputRecieved))
                {

                    inputRecieved--;
                    if (inputRecieved < 0 || inputRecieved >= options.Length)
                    {
                        //stes iputRevieved to be the default value
                        inputRecieved = -1;
                        //Diplay error message
                        Console.WriteLine("invalid input");
                        Console.ReadKey(true);
                    }
                }
            }

        }

        /// <summary>
        /// Calls the appropriate function(s) based on the current scene index
        /// </summary>
        void WompustheFith()
        {
            switch (CurrentScene)
            {
                case 0:
                    CharacterSelection();
                    DisplayMainMenu();
                    break;

                case 1:
                    Battle1();
                    UpdateCurrentEnemy();
                    Console.ReadKey(true);
                    break;
                //case 2:
                //    Battle2();
                //    UpdateCurrentEnemy();
                //    Console.ReadKey(true);
                //    break;
                //case 3:
                //    Battle3();
                //    UpdateCurrentEnemy();
                //    Console.ReadKey(true);
                //    break;
                case 4:
                    DisplayRestartMenu();
                    break;

                default:
                    Console.WriteLine("Invalid svene index");
                    break;
            }
        }

        /// <summary>
        /// Displays the menu that allows the player to start or quit the game
        /// </summary>
        void DisplayMainMenu()
        {
            int choice = GetInput("Hellow this is the battle arena", "1. To begin", "2. To leave");

            if (choice == 1)
            {
                CurrentScene = 1;
            }

            else if (choice == 2)
            {
                gameOver = true;
            }
        }

        void DisplayRestartMenu()
        {
            //the ending of the game // discription of what the player is seeing when the simulation is ending
            int choice = GetInput("Simulation is endding. Would you like to play again?", "Yes", "No");

            if (choice == 1)
            {
                //the looping of the game it self for the ending
                CurrentScene = 0;
            }
            else if (choice == 2)
            {
                //the game over
                gameOver = true;
            }
        }

        void DisplayCurrentScene()


        {
            if (CurrentScene == 0)
            {
                DisplayMainMenu();
                CharacterSelection();
            }
            if (CurrentScene == 1)
            {
                Battle1();
            }
            if (CurrentScene == 2)
            {
                DisplayRestartMenu();
            }

        }
        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            Console.WriteLine("Please enter your name.");
            Console.WriteLine(">");
            playerName = Console.ReadLine();
            Console.WriteLine("Hello, " + playerName);
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            Character Entity;
            Console.WriteLine("You martch on to the areana with pride and look around");
            Console.WriteLine("You see familar faces but all look exited or grim for the upcoming event");
            Console.WriteLine("Attention all contendents this is a battle arena and so we shal watch you fight");

            Console.WriteLine("All of you pick up your weapons and begin");

            int input = GetInput("choose your caractor", "1. Raider", "2. Gunner");
            {
                if (input == 1)
                {
                    Entity.job = "Raider";
                    Entity.attack = 40;
                    Entity.defense = 25;
                    Entity.health = 20;


                }
                else if (input == 2)
                {
                    Entity.job = "Gunner";
                    Entity.attack = 80;
                    Entity.defense = 15;
                    Entity.health = 15;


                }
                else
                {
                    Console.WriteLine("NO invalid input go it again");
                }
            }
        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="Entity">The character that will have its stats shown</param>
        void DisplayStats(Character Entity)
        {
            Console.WriteLine("Name " + Entity.name);
            Console.WriteLine("Class" + Entity.job);
            Console.WriteLine("Health " + Entity.health);
            Console.WriteLine("Damage " + Entity.attack);
            Console.WriteLine("Defence " + Entity.defense);

        }
        void DisplayStatsEnemy(Enemy Entity)
        {
            Console.WriteLine("Name " + Entity.name);
            Console.WriteLine("Health " + Entity.health);
            Console.WriteLine("Damage " + Entity.attack);
            Console.WriteLine("Defence " + Entity.defense);
        }

        /// <summary>
        /// Simulates one turn in the current fight
        /// </summary>
        public void Battle1()
        {

            Console.WriteLine("luckely you were preparyed and had your weapon in hand");
            Console.WriteLine("Two people charge you and one gets close to swing at you");
            Console.WriteLine("1. attack ");
            Console.WriteLine("2. move out of the way");

                //Print 
                DisplayStats(_player);
                //Print 
                DisplayStatsEnemy(_currentEnemy);

                //look at lodises code for the _player.Attack and _currentEnemy.Attack to work
                //but the curent attack dos work
                //need to make a if statement for damage
                int choice = GetInput("A" + _currentEnemy.Name + "they appear", "Attack", "Equip Item");


                if(choice == 1)
                {
                    damageDealt = _player.Attack(_currentEnemy);
                }
                else if (choice == 2)
                {
                    Console.WriteLine("");
                    Console.ReadKey();
                    Console.Clear();
                }

                damageDealt = _currentEnemy.Attack(_player);
            Console.WriteLine("The" + _currentEnemy.Name + "dealt" + damageDealt, "damage!");

                Console.ReadKey(true);
                Console.Clear();

                Console.ReadKey();
                Console.Clear();

                CheckBattleResults();
                UpdateCurrentEnemy();
                CurrentScene = 2;

            

        }

        public void Battle2()
        {

        }

        public void Battle3()
        {

        }


        void UpdateCurrentEnemy()
        {
            if (_currentEnemy.health <= 0)
            {

                CurrentEnemyIndex++;

                if (TheAtemptAtEnd())
                {
                    return;
                }

                _currentEnemy = enemies[CurrentEnemyIndex];
            }
        }
        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        string CheckBattleResults()
        {

            string matachEnd = "Next Enemy";


            if (_player.Health <= 0 && _currentEnemy.health <= 0)
            {
                matachEnd = "Draw";
            }
            if (_player.Health < 0)
            {
                Console.WriteLine("Disapointing");
                CurrentScene++;
            }
            else if (_currentEnemy.health < 0)
            {
                Console.WriteLine("You win this time");
                Console.ReadKey();
                Console.Clear();
                CurrentEnemyIndex++;

                if (CurrentEnemyIndex >= enemies.Length)
                {
                    CurrentScene = 3;

                }
                _currentEnemy = enemies[CurrentEnemyIndex];
            }

            return matachEnd;
        }


        /// <summary>
        /// Deals damage to a character based on an attacker's attack power
        /// </summary>
        /// <param name="attacker">The character that initiated the attack</param>
        /// <param name="defender">The character that is being attacked</param>
        /// <returns>The amount of damage done to the defender</returns>
        public float PlayerAttack(ref Character attacker, ref Enemy defender)
        {
            return attacker.attack - defender.defense;

        }

        public float EnemyAtack(ref Enemy attacker, ref Character defender)
        {
            return attacker.attack - defender.defense;
        }

    }
}
