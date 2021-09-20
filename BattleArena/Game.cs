using System;
using System.Collections.Generic;
using System.Text;


namespace BattleArena
{

    public enum ItemType
    {
        DEFENSE,
        ATTACK,
        NONE

    }

    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;
    }


    class Game
    {

        private bool _gameOver;
        private int _currentScene = 1;
        private Player _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex;
        private Entity _currentEnemy;
        private string _playerName;
        private Item[] _gunnerItems;
        private Item[] _raiderItems;

        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
            Start();

            while (!_gameOver)
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
            _gameOver = false;
            _currentScene = 1;
            InitializeEnemies();
            InitializeItems();
        }

        public void InitializeItems()
        {
            //Gunner items
            Item bigGun = new Item { Name = "Big Gud", StatBoost = 5, Type = ItemType.ATTACK };
            Item bigShield = new Item { Name = "Big Shield", StatBoost = 15 , Type = ItemType.DEFENSE };

            //Raider items
            Item bigAxe = new Item { Name= "Big Axe ", StatBoost = 15, Type = ItemType.ATTACK };
            Item forceShield = new Item { Name = "Force Shield ", StatBoost = 15, Type = ItemType.DEFENSE };

            //Initialize arrays
            _gunnerItems = new Item[] { bigGun, bigShield };
            _raiderItems = new Item[] { bigAxe, forceShield };
        }

        public void InitializeEnemies()
        {
            _currentEnemyIndex = 0;

            Entity claud = new Entity("Claud", 70, 25, 35);

            Entity chad = new Entity("Chad", 80, 32, 25);

            Entity wompus = new Entity("Wompus", 225, 30, 25);

            _enemies = new Entity[] { claud, chad, wompus };

            _currentEnemy = _enemies[_currentEnemyIndex];
        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            DisplayCurrentScene();
        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {
            Console.WriteLine("Leave");
            Console.ReadKey(true);
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
            int inputReceived = -1;

            while (inputReceived == -1)
            {
                //Print options
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + options[i]);
                }
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If the player typed an int...
                if (int.TryParse(input, out inputReceived))
                {
                    //...decrement the input and check if it's within the bounds of the array
                    inputReceived--;
                    if (inputReceived < 0 || inputReceived >= options.Length)
                    {
                        //Set input received to be the default value
                        inputReceived = -1;
                        //Display error message
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
                //a error if the player put a word
                else
                {
                    //set the inputrecieved back to -1 to restart loop
                    inputReceived = -1;
                    Console.WriteLine("Invalid input");
                    Console.ReadKey(true);

                }

                Console.Clear();
            }
            return inputReceived;
        }

        /// <summary>
        /// Calls the appropriate function(s) based on the current scene index
        /// </summary>
        void DisplayCurrentScene()
        {

            if (_currentScene == 1)
            {
                GetPlayerName();
                CharacterSelection();
            }
            if (_currentScene == 2)
            {
                Battle();
                CheckBattleResults();
            }

            if (_currentScene == 3)
            {
                DisplayMainMenu();
            }

        }

        /// <summary>
        /// Displays the menu that allows the player to start or quit the game
        /// </summary>
        void DisplayMainMenu()
        {
            int choice = GetInput("Play Again?", "Yes", "No");

            if (choice == 0)
            {
                _currentScene = 1;
                InitializeEnemies();
            }
            else if (choice == 1)
            {
                _gameOver = true;
            }
        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            Console.WriteLine("Please enter your name.");
            Console.Write("> ");
            _playerName = Console.ReadLine();

            Console.Clear();

            int choice = GetInput("You've entered " + _playerName + ". Are you sure you want to keep this name?", "Yes", "No");

            if (choice == 0)
            {
                _currentScene++;
            }

        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int choice = GetInput("select you fighter", "Gunner", "Raider");

            if (choice == 0)
            {
                _player = new Player(_playerName, 100, 225, 15, _gunnerItems);
                _currentScene = 2;
            }
            else if (choice == 1)
            {
                _player = new Player(_playerName, 125, 50, 29, _raiderItems);
                _currentScene = 2;
            }
        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Entity character)
        {
            Console.WriteLine("Name: " + character.Name);
            Console.WriteLine("Health: " + character.Health);
            Console.WriteLine("Attack Power: " + character.AttackPower);
            Console.WriteLine("Defense Power: " + character.DefensePower);
            Console.WriteLine();
        }

        public void DisplayEquipItemMenu()
        {
            //gets the item that player wants
            int choice = GetInput("What item do you want. ", _player.GetItemNames());

            //equips item at given index
            if (!_player.TryEquipItem(choice))
                //error message
                Console.WriteLine("You conldn't find that item :[");

            //prints feedback
            Console.WriteLine("You equipped " + _player.CurrentItem.Name + ";]");
        }
        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            float damageDealt = 0;

            DisplayStats(_player);
            DisplayStats(_currentEnemy);

            int choice = GetInput("A " + _currentEnemy.Name + " stands there in frond of reader to attack do you", "Attack", "Equip item", "Remove current item");

            if (choice == 0)
            {
                damageDealt = _player.Attack(_currentEnemy);
                Console.WriteLine("You dealt " + damageDealt + " damage!");
            }
            else if (choice == 1)
            {
                DisplayEquipItemMenu();
                Console.ReadKey(true);
                Console.Clear();
                return;
            }
            else if (choice == 2)
            {

            }

            damageDealt = _currentEnemy.Attack(_player);
            Console.WriteLine("The " + _currentEnemy.Name + " dealt " + damageDealt, " damage!");

            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {
            if (_player.Health <= 0)
            {
                Console.WriteLine("You were slain...");
                Console.ReadKey(true);
                Console.Clear();
                _currentScene = 3;
            }
            else if (_currentEnemy.Health <= 0)
            {
                Console.WriteLine("You slayed the " + _currentEnemy.Name);
                Console.ReadKey();
                Console.Clear();
                _currentEnemyIndex++;

                if (_currentEnemyIndex >= _enemies.Length)
                {
                    _currentScene = 3;
                    Console.WriteLine("You've slain all the enemies! You are vitorious but there will be more.");
                    return;
                }

                _currentEnemy = _enemies[_currentEnemyIndex];
            }

        }

    }
}


