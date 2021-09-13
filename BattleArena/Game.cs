﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    /// <summary>
    /// Represents any entity that exists in game
    /// </summary>
    struct Enemy
    {
        public string name;
        public float health;
        public float attack;
        public float defense;
    }
    struct Character
    {
        public string name;
        public float health;
        public float attack;
        public float defense;
        public string job;
    }

    class Game
    {

        string playerName = "none";
        bool gameOver = false;
        public int currentScene = 0;

        public int currentEnemyIndex = 0;
        public Enemy currentEnemy;

        string input = Console.ReadLine();

        Character character;
        Enemy Claud;
        Enemy Rob;
        Enemy Wompus;
        Enemy Theo;
        Enemy[] enemies;


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

        void ResetCurrentEnemies()
        {
            currentEnemyIndex = 0;

            currentEnemy = enemies[currentEnemyIndex];
            currentEnemyIndex++;
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

        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        public void Start()

        {
            Claud.name = "Claud";
            Claud.attack = 15;
            Claud.defense = 20;
            Claud.health = 15;

            Rob.name = "Rob";
            Rob.attack = 15;
            Rob.defense = 20;
            Rob.health = 15;

            Wompus.name = "Wompus";
            Wompus.attack = 15;
            Wompus.defense = 20;
            Wompus.health = 15;

            Theo.name = "Theo";
            Theo.attack = 15;
            Theo.defense = 20;
            Theo.health = 15;

            character.job = "Gunner";
            character.attack = 80;
            character.defense = 15;
            character.health = 15;

            character.job = "Raider";
            character.attack = 40;
            character.defense = 25;
            character.health = 20;

            enemies = new Enemy[] { Rob, Wompus, Theo, Claud };
            
            ResetCurrentEnemies();
        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            
            DisplayCurrentScene();
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
            bool theEnd = currentEnemyIndex >= enemies.Length;

            if (theEnd)
            {
                currentScene = 2;
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
        int GetInput(string description, string option1, string option2)
        {
            string input = "";
            int inputReceived = 0;

            while (inputReceived != 1 && inputReceived != 2)
            {//Print options
                Console.WriteLine(description);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If player selected the first option...
                if (input == "1" || input == option1)
                {
                    //Set input received to be the first option
                    inputReceived = 1;
                }
                //Otherwise if the player selected the second option...
                else if (input == "2" || input == option2)
                {
                    //Set input received to be the second option
                    inputReceived = 2;
                }
                //If neither are true...
                else
                {
                    //...display error message
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey();
                }
                Console.Clear();
            }
            return inputReceived;
        }

        /// <summary>
        /// Calls the appropriate function(s) based on the current scene index
        /// </summary>
        void WompustheFith()
        {
            switch(currentScene)
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
                case 2:
                    Battle2();
                    UpdateCurrentEnemy();
                    Console.ReadKey(true);
                    break;
                case 3:
                    Battle3();
                    UpdateCurrentEnemy();
                    Console.ReadKey(true);
                    break;
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
                currentScene = 1;
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
                currentScene = 0;
            }
            else if (choice == 2)
            {
                //the game over
                gameOver = true;
            }
        }

        void DisplayCurrentScene()
        
        
        {
            if (currentScene == 0)
            {
                DisplayMainMenu();
                CharacterSelection();
            }
            if (currentScene == 1)
            {
                Battle1();
            }
            if(currentScene == 2)
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
            playerName = Console.ReadLine();
            Console.WriteLine("Hello, " + playerName);
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            Character character;
            bool characterselected = true;
            Console.WriteLine("You martch on to the areana with pride and look around");
            Console.WriteLine("You see familar faces but all look exited or grim for the upcoming event");
            Console.WriteLine("Attention all contendents this is a battle arena and so we shal watch you fight");
            
            Console.WriteLine("All of you pick up your weapons and begin");
            while (characterselected = true)
            {
                
                int input = GetInput("choose your caractor", "1. Raider", "2. Gunner");
                {
                    if (input == 1)
                    {
                        character.job = "Raider";
                        character.attack = 40;
                        character.defense = 25;
                        character.health = 20;
                        characterselected = false;
                        currentScene = 1;
                    }
                    else if (input == 2)
                    {
                        character.job = "Gunner";
                        character.attack = 80;
                        character.defense = 15;
                        character.health = 15;
                        characterselected = false;
                        currentScene = 1;
                    }
                    else
                    {
                        Console.WriteLine("NO invalid input go it again");
                    }
                }

                break;

            }
        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Character character)
        {
            Console.WriteLine("Name " + character.name);
            Console.WriteLine("Class" + character.job);
            Console.WriteLine("Health " + character.health);
            Console.WriteLine("Damage " + character.attack);
            Console.WriteLine("Defence " + character.defense);

        }
         void DisplayStatsEnemy(Enemy enemy)
        {
            Console.WriteLine("Name " + enemy.name);
            Console.WriteLine("Health " + enemy.health);
            Console.WriteLine("Damage " + enemy.attack);
            Console.WriteLine("Defence " + enemy.defense);
        }
        /// <summary>
        /// Calculates the amount of damage that will be done to a character
        /// </summary>
        /// <param name="attackPower">The attacking character's attack power</param>
        /// <param name="defensePower">The defending character's defense power</param>
        /// <returns>The amount of damage done to the defender</returns>
        float CalculateDamage(float attack, float defense)
        {
            float damage = attack - defense;
            if (damage <= 0)
            {
                damage = 0;
            }

            return damage;
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
        /// <summary>
        /// Simulates one turn in the current fight
        /// </summary>
        public void Battle1()
        {

            Console.WriteLine("luckely you were preparyed and had your weapon in hand");
            Console.WriteLine("Two people charge you and one gets close to swing at you");
            Console.WriteLine("1. attack ");
            Console.WriteLine("2. move out of the way");

            while (character.health > 0 || currentEnemy.health > 0)
            {

                //Print 
                DisplayStats(character);
                //Print 
                DisplayStatsEnemy(currentEnemy);

                //
                float damageTaken = PlayerAttack(ref character, ref Claud);
                currentEnemy.health -= damageTaken;
                Console.WriteLine(Claud.name + "has taken " + damageTaken);

                //
                damageTaken = EnemyAtack(ref Claud, ref character);
                character.health -= damageTaken;
                Console.WriteLine(playerName + "has taken " + damageTaken);

             
                Console.ReadKey();
                Console.Clear();

                CheckBattleResults();
                UpdateCurrentEnemy();
                currentScene = 2;
                break;
                
            }
           
        }

        public void Battle2()
        {

        }

        public void Battle3()
        {

        }


        void UpdateCurrentEnemy()
        {
            if (currentEnemy.health <= 0)
            {

                currentEnemyIndex++;

                if (TheAtemptAtEnd())
                {
                    return;
                }

                currentEnemy = enemies[currentEnemyIndex];
            }
        }
        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        string CheckBattleResults()
        {

            string matachEnd = "Next Enemy";


            if (character.health <= 0 && currentEnemy.health <= 0)
            {
                matachEnd = "Draw";
            }
            if (character.health < 0)
            {
                Console.WriteLine("Disapointing");
                matachEnd = character.name;
            }
            else if (currentEnemy.health < 0)
            {
                Console.WriteLine("You win this time");
                matachEnd = currentEnemy.name;
            }

            return matachEnd;
        }

    }
}
