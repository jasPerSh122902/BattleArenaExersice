Jasper Thibodeaux
s218005
Text bases game

# The Battle Areana
 *Text based game documention*
 
 # The game:

$User Interface$ : 

The game opens up as a text pannal which will ask you the player do you want to leave or player. This is to ensure the player did not open the tab or appliction with out wanting to and have that option. 

$User Input$:

The panal will update and will have options but the only input form the player that will work is the $Numbers$ The first few options of the game is to choose your class between raider and gunner. Each will allow the player to get to the end of the game.

$II. Design$

1.*System function*

All object inherit from the game class and other varables that are ingerited form other classes like *Entity, Player, Shop, Enemy*. Each have varables that are inherited from each other and will track the options the player is inputing. But the game class is were all other classes conjorn and work together.

2.*Object infromation*

+ $File Name$: game.cs
>+ *Name* :  Shop_shop
>>+ Description : takes the game class Shop and makes it a usable instence.

>+ *Name* : Player _player
>>+ Description : takes the game class Player and makes it a usable instence.

>+ *Name* :  Entity[] _enemies
>>+ Description : This is a  calss that is assined in game as a array with the name _enemies and is meant to hold my oposing side of NPC's.

>+ *Name* :  Scene _currentScene
>>+ Description : Made Scene in the game.cs and made it equal to the encounters and this keep track of the players pusition in the story.

>+ *Name* :  _Entity _currentEnemy
>>+ Description : Keeps track of the enemy in the _enemies array.

>+ *Name* :  Item _item
>>+ Description : makes a instrence of the struct Item and makes it into a item.

>+ *Name* :  Item[] _Shopitem
>>+ Description : makes a array of Item and calls it _shopItems.

>+ *Name* : _currentItemIndex
>>+ Description : Holds the current Item in the form of a int or a number.

>+ *Name* :  CurrentEnemyIndex
>>+ Description : Holds the current Enemy in the form of a int or a number and acts like a nother increament for enmey.
>+ *Name* :  _gameOver
>>+ Description : Is a bool that controls if the game is over or not.

>+ *Name* : _playerName
>>+ Description : Holds the players name in a string.

>+ *Name* : Item[] _gunnerItems
>>+ Description : Is a array of Items for the current class of Gunner.

>+ *Name* : _raiderItems
>>+ Description : Is a array of Items for the current Class of Raider.

>+ *Name* : Run() void
>>+ Description : Only is meant to load things at the start of the applicaiton but here is meant to constrol _gameOver, Start(), Update(), and End().

>+ *Name* : Start() void
>>+ Description : Sets _gameOver to false and set the currentSecen to STARTMENU as well as starts the process of InitializeEnemies(),InitializeItems() and makes _shop a instrence of Shop(_shopItems), as well as makes _player a instence of Player().

>+ *Name* :CharacterSelction() void
>>+ Description : Gives the player the choice between the class of Raider and Gunner and askes to choose and this has to happen.

>+ *Name* : InitializeItems() void
>>+ Description : Hold the array for the items of classes and _shopItem.

>+ *Name* :InitializeEnemies() void
>>+ Description : Hold the array for the Enemies and makes a instence of _enemies with the Entity class.

>+ *Name* : Print(Item[]_Invenotry) void
>>+ Description : prints the invenotry of eny thing.

>+ *Name* : Update() void
>>+ Description : holds the function Display Current Scene().

>+ *Name* :GetShopMenuOptions() string[]
>>+ Description : Gites the string or array for the shop and makes the shop menu that include the ablity to leave, save, and quit form the menu and mekes a arrya called menu Options.

>+ *Name* : DisplayShopMenuOptions() void
>>+ Description : gives player a few chooses in the shop and gives them visual input of what they want. Then proseds to Sell if they pick a choice that is a item and then one they leave and it gos back to battle or subtracts gold. And if nether give a error to the player.

>+ *Name* : GetInput(string Description, params strig[] options) int
>>+ Description : This is only meant to get the options that i want and gets the input form the player. And gives a error if the player picks something that is out of the choicees range.

>+ *Name* : DisplayCurrentScene() void
>>+ Description : Is a holder for the Scenes and allow the player to go between Scenes.

>+ *Name* : DisplayMainMenu() void
>>+ Description : Gives the player visual input if they want to player again or no if yes StartMenu if no GameOver = true.

>+ *Name* : DisplayerStartMenu() void
>>+ Description : This is the start menu that askes the player for there name or if they want to load a current save. If they have no save error.

>+ *Name* : GetPlayerName() void
>>+ Description : Gets the players name and asks if thy like that name.

>+ *Name* : DisplayStat(Entity Character) void
>>+ Description : Displays the stats of player using the Entity class.

>+ *Name* : DisplayEquipItemMenu() void
>>+ Description : Get the ItemNames and askes what do you want that is *in* the inventory of player. And gives Viusla input.

>+ *Name* : Battle() void
>>+ Description : uses the DisplayStats for the player and currentEnemy and uses the attack fuction to deal damage but askes the player if they want to go to shop, deal damage. equip a item, or leave/ save.

>+ *Name* : CheckBatttleResults() void
>>+ Description : Chicks if the player is alive or is enemy is alive if eny is false then continue if player is not alive game over and if the Enemy is not alive give gold and add to enemy index.

>+ *Name* : Save() bool
>>+ Description : Saves the current data to teh SaveData.Txt and closes the writer or save loction.

>+ *Name* : load() bool
>>+ Description : Loads the current Data of player from the SaveData.Txt.

+ $File Name$ : Enemy.cs
>+ *Name* : Enemy()
>>+ Description : makes instence of Enemy.

>+ *Name* : _currentenemy GOld_ int
>>+ Description : Makes 
a counter for the gold or is a counter.

>+ *Name* : Enemy(string name, float health, float attackPower, int currentGold, int defensePower) : base(name, health, currentGold, attackPower, defensePower)
>>+ Description : Makes a instence of enemy and _currentGold.


+ $File Name$ : Entity.cs
>+ *Name* : _name string
>>+ Description : Get the name of the Entity and make a constructor for name.

>+ *Name* : _health float
>>+ Description : Get the health of the Entity made a constructor for health.

>+ *Name* : _attackPower flaot
>>+ Description : gets the attackPower or the Entity and make a constructor for attackPower.

>+ *Name* : _defensePower flaot
>>+ Description : gets the defensePoweror the Entity and make a constructor for defensePower.

>+ *Name* : _currentGold int
>>+ Description : gets the currentGOld or the Entity and make a constructor for currentGOld.

>+ *Name* : Entity()
>>+ Description : Made entity with the name, health, attackpower, defensePower, curretnGold.

>+ *Name* : Entity(string name, float Health, float attackPower defensePower, int currentGold )
>>+ Description : Makes the instence of Entity.

>+ *Name* : TakeDamage(float damageAmount) float
>>+ Description : makes player or enemy take the damage on how is recieving and dealing Then decrements health.

>+ *Name* : Attack(Entity defender) flaot
>>+ Description : uses the TakeDamage fuctnion but usues the Entitys attackPower do deal the damage do the fucntion is netral for both sides.

>+ *Name* : TakeGold(int enemyGold) float
>>+ Description : Takes the enemygold after defeat or attack.

>+ *Name* : Save() void
>>+ Description : Saves the player and enemy.

>+ *Name* : Load() bool
>>+ Description : Loads the eneney and player

+ $File Name$ : Player.cs
>+ *Name* : Item[] _invenotry
>>+ Description : Gets a struct call Iitem and names it _inventory

>+ *Name* : Item _currentItem
>>+ Description : Get the item from Item struct/ array

>+ *Name* : _crruetnItemIndex int
>>+ Description : makes incrementing throw the curretnITemIndex through this int.

>+ *Name* : Gold {get {return _currentGOld; } }
>>+ Description : get the current gold

>+ *Name* : Item[] GetIventory()
>>+ Description : returns the inventory

>+ *Name* : DefesePower orride float
>>+ Description : Get the defense power

>+ *Name* : attakcPower override float
>>+ Description : Get the attack power

>+ *Name* : DefesePower orride float
>>+ Description : Get the defense power

>+ *Name* : currentITemorride float
>>+ Description : Get the currentItem

>+ *Name* : job {get; set} string 
>>+ Description :get the job of the player.

>+ *Name* :Player()
>>+ Description : make instence of player.

>+ *Name* : Player(Item[] items) : base()
>>+ Description : makes instece of player with Item and a array to hold.

>+ *Name* : Player(string name, float health, flaot attackpower, float currentgold , int defensePower, Item[] items, string job) : base(name, health, curretnGOld, attackpower, defensePower)
>>+ Description : Get the instece of player

>+ *Name* : TryequipItem(int Index) bool
>>+ Description : makes sure the player is trying to equip a item

>+ *Name* : Buy(Item item) void
>>+ Description : allow the player to buy a item while makeing a array and sliding the array into inventory.

>+ *Name* : TryRemoverCurrentItem() bool
>>+ Description : makes sure the item is there to remove.

>+ *Name* : GetITemName() string[]
>>+ Description : Get the item names with a string array.


+ $File Name$ : Shop.cs
>+ *Name* : gold float
>>+ Description : is meant to make a float called gold

>+ *Name* : Item[] Inventory
>>+ Description : Makes a inventory array from Item.

>+ *Name* : game
>>+ Description : allows the game class to have a instence.

>+ *Name* : Player
>>+ Description : Makes instence of player

>+ *Name* : Shop()
>>+ Description : makes instece of Shop with gold and invenotory.

>+ *Name* : Shop(param Item[] items)
>>+ Description : makes a instence of shop but allows numbers to be inside Item.

>+ *Name* : GetSHopMenuOptions() string[]
>>+ Description : Get the shop menu options in the form of a array and increase the invenotry array with a new arrya clled itemNames.

>+ *Name* : Sell(player player, int _ItemIndex) bool
>>+ Description : makes sure that the player wantes to buy form the shop or the shop want to sell to the player.

>+ *Name* : GetItemNames() string[]
>>+ Description : Gets the item names and makes then in order.