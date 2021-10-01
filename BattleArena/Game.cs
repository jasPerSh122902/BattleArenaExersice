using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace BattleArena
{
    public enum ItemType
    {
        DEFENSE,
        ATTACK,
        NONE
    }


    public enum Scene
    {
        STARTMENU,
        NAMECREATION,
        CHARACTORSELECTION,
        BATTLE,
        RESTARTMENU
    }

    public struct Item
    {
        public string Name;
        public int Cost;
        public float StatBoost;
        public ItemType Type;
    }


    class Game
    {
        private Scene _currentScene;
        public Shop _shop;
        private Player _player;
        

        //states Entity...
        private Entity[] _enemies;
        private Entity _currentEnemy;

        //states Item...
        private Item _item;
        private Item[] _shopItems;

        //random variables...
        private int _currentItemIndex = 0;
        private int _currentEnemyIndex;
        private bool _gameOver;
        private string _playerName;

        //the players Items

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
            _currentScene = Scene.STARTMENU;
            InitializeEnemies();
            InitializeItems();

            //made intences...
            _shop = new Shop(_shopItems);
            _player = new Player();
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
                _player = new Player(_playerName, 100, 15, 225, 100, _gunnerItems, "gunner");
                _currentScene = Scene.BATTLE;
            }
            else if (choice == 1)
            {
                _player = new Player(_playerName, 125, 225, 100, 200, _raiderItems, "raider");
                _currentScene = Scene.BATTLE;
            }
        }
        public void InitializeItems()
        {
            
            //Gunner items
            Item bigGun = new Item { Name = "Big Gun ", Cost = 100, StatBoost = 5, Type = ItemType.ATTACK };
            Item bigShield = new Item { Name = "Big Shield ", Cost = 100, StatBoost = 15, Type = ItemType.DEFENSE };

            //Raider items
            Item bigAxe = new Item { Name = "Big Axe ", Cost = 100, StatBoost = 15, Type = ItemType.ATTACK };
            Item forceShield = new Item { Name = "Force Shield ", Cost = 100, StatBoost = 15, Type = ItemType.DEFENSE };

            //Initialize arrays
            _gunnerItems = new Item[] { bigGun };

            _raiderItems = new Item[] { bigAxe };

            _shopItems = new Item[] { bigAxe, bigGun, bigShield, forceShield };


            _shop = new Shop(_shopItems);
        }

        /// <summary>
        /// gets the enemies and makes a instence...
        /// </summary>
        public void InitializeEnemies()
        {
            //makes currentEnemyIndex and states the enemies...
            _currentEnemyIndex = 0;

            Entity claud = new Entity("Claud", 70, 25, 45, 100);

            Entity chad = new Entity("Chad", 80, 32, 25, 100);

            Entity wompus = new Entity("Wompus", 225, 30, 25, 100);

            //makes the enemy instence.
            _enemies = new Entity[] { claud, chad, wompus };

            _currentEnemy = _enemies[_currentEnemyIndex];
        }

        //Prints the invenotry or get the length of the inventory...
        public void PrintInventory(Item[] _invenotry)
        {
            for (int i = 0; i < _invenotry.Length; i++)
            {
                //and then prints there names.
                Console.WriteLine((i + 1) + ". " + _invenotry[i].Name);
            }
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
        /// allow to Save and quit at eny time in the shop.
        /// </summary>
        /// <returns></returns>
        private string[] GetShopMenuOptions()
        {
            //gets the options for the menu to show up and allows...
            string[] shopItems = _shop.GetItemNames();
            string[] menuOptions = new string[shopItems.Length + 3];

            //increment throw the shop items .
            for (int i = 0; i < shopItems.Length; i++)
            {
                menuOptions[i] = shopItems[i];
            }
            //to laeve , save and ,quit in the shop at eny time.
            menuOptions[shopItems.Length] = "Leave Shop";
            menuOptions[shopItems.Length + 1] = "Save Game";
            menuOptions[shopItems.Length + 2] = "Quit Game";

            return menuOptions;
        }

        /// <summary>
        /// gets the options of the shop then gets the players money to see if they have enough...
        /// then get the item Index then procedes to allow the player to choose the place to replace it.
        /// </summary>
        public void DisplayShopMenuOptions()
        {
            // Sets a int variable to be the total size of items it want to be desplayed minus 3
            int totalInventorySize = GetShopMenuOptions().Length - 3;
            //print the players current gold holding
            Console.WriteLine("Your gold: " + _player.Gold);
            //LEt the user know where there inventory will be displayed at 
            Console.WriteLine("Your Inventory:");

            //For every item the player has in there inventory. . . 
            for (int i = 0; i < _player.GetItemNames().Length; i++)
                //Dis play that items name to the screen 
                Console.WriteLine(_player.GetItemNames()[i]);

            //Get the players decision to what they would like to purchase
            int choice = GetInput("\n What would you like to purchase?", GetShopMenuOptions());

            //If the choice is less then the size between the options. . .
            if (choice <= totalInventorySize)
            {
                //If the Shops Sells the item. . . 
                if (_shop.Sell(_player, choice))
                {
                    if(choice == 4)
                    {
                        //gives input to the player...
                        Console.WriteLine("You choose to leave shop and are now heading to battle . ");
                        //set the scene.
                        _currentScene = Scene.BATTLE;
                    }
                    else
                    {
                        //. . .Displays to the users that the shop just sold them that item
                        Console.WriteLine("You purchased the " + _shop.GetItemNames()[choice]);

                        _player._currentGold -= 100;
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
                //else they cant buy the item
                else
                {
                    //. . .Displays to them they can't purches said item
                    Console.WriteLine("You don't have enough for that.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            // if the choice happens to bt the size plus 1. . .
            else if (choice == (totalInventorySize + 1))
            {
                //. . .Data Gets Saved 
                Save();
                //. . .Tells the user they had saved successfully
                Console.WriteLine("Save was succsessful");
                Console.ReadLine();
                Console.Clear();
            }
            // if the choice happens to bt the size plus 2. . .
            else if (choice == (totalInventorySize + 2))
                //The Update Loop Ends and the Game is Over
                _gameOver = true;

        }

        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the player can choose</param>
        /// <param name="option2">The second option the player can choose</param>
        /// <returns></returns>
        public int GetInput(string description, params string[] options)
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

            switch (_currentScene)
            {
                case Scene.STARTMENU:
                    DisplayStartMenu();
                    break;
                case Scene.NAMECREATION:
                    GetPlayerName();
                    break;
                case Scene.CHARACTORSELECTION:
                    CharacterSelection();
                    break;
                case Scene.BATTLE:
                    Battle();
                    CheckBattleResults();
                    break;
                case Scene.RESTARTMENU:
                    DisplayMainMenu();
                    break;
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
                _currentScene = Scene.STARTMENU;
                InitializeEnemies();
            }
            else if (choice == 1)
            {
                _gameOver = true;
            }
        }

        /// <summary>
        /// gets the start menu and then askes player to start a  new or load save file.
        /// </summary>
        public void DisplayStartMenu()
        {
            int choice = GetInput("Welcome to the Areana!", "Start New Game", "Load Game");

            if (choice == 0)
            {
                //sest current scene to ask for there name...
                _currentScene = Scene.NAMECREATION;
            }
            else if (choice == 1)
            {
                //loads the players save and then battles the enemy..
                if (Load())
                {
                    Console.WriteLine("Load Successful!");
                    Console.ReadKey(true);
                    Console.Clear();
                    _currentScene = Scene.BATTLE;
                }
                else
                {
                    //if load failes.
                    Console.WriteLine("Load failed.");
                    Console.ReadKey(true);
                    Console.Clear();
                }

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

            //intro with player name.
            int choice = GetInput("You've entered " + _playerName + ". Are you sure you want to keep this name?", "Yes", "No");

            if (choice == 0)
            {
                _currentScene++;
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
            Console.WriteLine("Gold amount: " + character.currentGold);
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

            int choice = GetInput("A " + _currentEnemy.Name + " stands there in frond of you do you", "Attack ", "Equip item ", "Remove current item", "Go to the Shop! ", "Leave: ", "Save. ");

            if (choice == 0)
            {
                //the player is dealing damage in there turn
                damageDealt = _player.Attack(_currentEnemy);
                //input for the player to know they did damage
                Console.WriteLine("You dealt " + damageDealt + " damage!");

                //enemy turn to deal the damage and ...
                damageDealt = _currentEnemy.Attack(_player);
                //this is the input for that.
                Console.WriteLine("The " + _currentEnemy.Name + " dealt " + damageDealt, " damage!");
            }
            else if (choice == 1)
            {
                //gives the player the chance to equip a item of there choice.
                DisplayEquipItemMenu();
                Console.ReadKey(true);
                Console.Clear();
                return;
            }
            else if (choice == 2)
            {
                //makes sure the player has full controle of the entity they chose so they can...
                //remove the item or just leave it there....
                if (!_player.TryRemoveCurrentItem())
                    Console.WriteLine("You dont know enthing equipped good luck. ");
                //if they have nothing equiped...
                else
                    Console.WriteLine("you plced the item on the ground. ");

                Console.ReadKey(true);
                Console.Clear();
                return;
            }
            else if (choice == 3)
            {
                //the shop option
                DisplayShopMenuOptions();
            }
            else if (choice == 4)
            {
                //this is to leave the game.
                _gameOver = true;
            }
            else if (choice == 5)
            {
                //this is to save your game.
                Save();
                Console.WriteLine("Save Game!");
                Console.ReadKey(true);
                Console.Clear();
                return;
            }


            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {
            //has to specifi the gold form the enemy.
            int enemyGold = 100;

            if (_player.Health <= 0)
            {
                //player dies.
                Console.WriteLine("You were slain...");
                Console.ReadKey(true);
                Console.Clear();
                _currentScene = Scene.RESTARTMENU;
            }
            else if (_currentEnemy.Health <= 0)
            {
                //if the enemy dies then there gold is given to the player but only 100...
                _player._currentGold += 100;
                Console.WriteLine("You slayed the " + _currentEnemy.Name + " You stole " + _player.TakeGold(enemyGold) + " gold!");
                Console.ReadKey();
                Console.Clear();
                //update the current enemy till completion.
                _currentEnemyIndex++;

                //scans to see if there is no enemies is there is keep going if not then ...
                if (_currentEnemyIndex >= _enemies.Length)
                {
                    //update current scene.
                    _currentScene = Scene.RESTARTMENU;
                    Console.WriteLine("You've slain all the enemies! You are vitorious but there will be more.");
                    return;
                }

                _currentEnemy = _enemies[_currentEnemyIndex];
            }

        }


        public void Save()
        {
            //create a new stream below
            StreamWriter writer = new StreamWriter("SaveData.txt");

            //save enemies...
            writer.WriteLine(_currentEnemyIndex);

            //saves player...
            _player.Save(writer);
            _currentEnemy.Save(writer);

            //closes the writer when done saving.
            writer.Close();
        }

        public bool Load()
        {
            bool loadSuccessful = true;

            //figures out if file exists then if not load false.
            if (!File.Exists("SaveData.txt"))
                loadSuccessful = false;


            //creas a new reader to read from the text file
            StreamReader reader = new StreamReader("SaveData.txt");

            //if the first line can't be converted into an integer returns false.
            if (!int.TryParse(reader.ReadLine(), out _currentEnemyIndex))
                loadSuccessful = false;

            string job = reader.ReadLine();
            //load the items of the class that is choosen
            if (job == "gunner")
                _player = new Player(_gunnerItems);
            else if (job == "raider")
                _player = new Player(_raiderItems);
            else
                loadSuccessful = false;

            _player.Job = job;

            if (!_player.Load(reader))
                loadSuccessful = false;

            //created a new instance and try to load the enemy
            _currentEnemy = new Entity();

            if (!_currentEnemy.Load(reader))
                loadSuccessful = false;

            //Updated the array to match the current enemey stats
            _enemies[_currentEnemyIndex] = _currentEnemy;

            _currentScene = Scene.BATTLE;

            //make the reader cloase when finished
            reader.Close();

            return loadSuccessful;
        }
    }
}