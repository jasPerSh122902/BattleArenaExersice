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
        public Array[] _inventory;
        

        public string Name;
        public int ItemCost;
        public float StatBoost;
        public ItemType Type;
        private Item[] _raiderItems;
        private Item[] _gunnerItems;

    }


    class Game
    {

        private bool _gameOver;
        private Scene _currentScene;
        public Shop _shop;
        private Player _player;
        private Item[] _invenotry;
        int itemIndex = 0;
        private Entity[] _enemies;
        private int _currentEnemyIndex;
        private Entity _currentEnemy;
        private Item[] _shopItems;
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

            //gives null so have the fix

            _gameOver = false;
            _currentScene = Scene.STARTMENU;
            InitializeEnemies();
            InitializeItems();

            _shop = new Shop(_shopItems);
            _player = new Player();
            _invenotry = new Item[0];
        }

        public void InitializeItems()
        {

            //Gunner items
            Item bigGun = new Item { Name = "Big Gun ", ItemCost = 25, StatBoost = 5, Type = ItemType.ATTACK };
            Item bigShield = new Item { Name = "Big Shield ", ItemCost = 30, StatBoost = 15, Type = ItemType.DEFENSE };

            //Raider items
            Item bigAxe = new Item { Name = "Big Axe ", ItemCost = 25, StatBoost = 15, Type = ItemType.ATTACK };
            Item forceShield = new Item { Name = "Force Shield ", ItemCost = 34, StatBoost = 15, Type = ItemType.DEFENSE };

            //Initialize arrays
            _gunnerItems = new Item[] { bigGun, bigShield };

            _raiderItems = new Item[] { bigAxe, forceShield };

            _shopItems = new Item[] { bigAxe, bigGun, bigShield, forceShield };


            _shop = new Shop(_shopItems);
        }


        public void InitializeEnemies()
        {
            _currentEnemyIndex = 0;

            Entity claud = new Entity("Claud", 70, 25, 45, 100);

            Entity chad = new Entity("Chad", 80, 32, 25, 100);

            Entity wompus = new Entity("Wompus", 225, 30, 25, 100);

            _enemies = new Entity[] { claud, chad, wompus };

            _currentEnemy = _enemies[_currentEnemyIndex];
        }

        //this do not work you should fix this...
        public void PrintInventory(Item[] items)
        {
            for (int i = 1; i < _invenotry.Length; i++)
            {
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


        public void DisplayShopMenuOptions()
        {
            
            int playerIndex = 0;
            Console.WriteLine("You got " + _player.currentGold + " Gold.");
            Console.WriteLine("Your bag: ");


            int choice = GetInput(" This is the Shop Hi and I hope you stay. ", _shop.GetShopMenuOptions());


            if (choice >= 0 && choice < _shop.GetShopMenuOptions().Length)
            {

                

                if (_player.currentGold < _shopItems[itemIndex].ItemCost)
                {
                    Console.WriteLine("You cant afford this.");
                    return;
                }
                else if (_player.currentGold >= _shopItems[itemIndex].ItemCost)
                {
                    
                    if (choice == 0)
                    {
                        Console.WriteLine(" You bought big Axe");
                        itemIndex = 0;
                        playerIndex = 0;
                    }
                    if (choice == 1)
                    {
                        Console.WriteLine(" You bought big Gun");
                        itemIndex = 1;
                        playerIndex = 1;
                    }
                    if (choice == 2)
                    {
                        Console.WriteLine(" You bought big Shield");
                        itemIndex = 2;
                        playerIndex = 2;
                    }
                    if (choice == 3)
                    {
                        Console.WriteLine(" You bought force Shield");
                        itemIndex = 3;
                        playerIndex = 3;
                    }

                    _shop.Sell(_player,itemIndex, playerIndex);
                    _player.GetInventory();


                }
            }
        }

 
        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {
            Console.WriteLine("Leave");
            Console.ReadKey(true);
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



            //figures out if file exists...
            if (!File.Exists("SaveData.txt"))
                //returns false
                loadSuccessful = false;

            //creas a new reader to read from the text file
            StreamReader reader = new StreamReader("SaveData.txt");

            //if the first line can't be converted into an integer...
            if (!int.TryParse(reader.ReadLine(), out _currentEnemyIndex))
                //...returns false
                loadSuccessful = false;

            string job = reader.ReadLine();



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
            int choice = GetInput("What item do you want. ",  _player.GetItemNames());
            

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

            int choice = GetInput("A " + _currentEnemy.Name + " stands there in frond of you do you", "Attack ", "Equip item ", "Remove current item", "Go to the Shop! ","Leave: ", "Save. ");

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
            float enemyGold = 0;

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
                Console.WriteLine("You slayed the " + _currentEnemy.Name + "You stole " + enemyGold + " gold!");
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

    }
}