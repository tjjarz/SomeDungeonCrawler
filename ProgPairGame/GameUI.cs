﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgPairGame
{
    class GameProgram
    {
        static void Main()
        {
            Player player = new Player();
            player.position = 4;

            List<Room> dungeon = new List<Room>();
            BuildDungeon();

            Console.CursorVisible = false;
            Console.SetWindowSize(120, 31);
            //string greet ="" +
            //    "▐".PadRight(30) + "Welcome to SomeTextAdventure!"+ "▌\n".PadLeft(30) +
            //    "▐".PadRight(30) + "NAME YOUR CHARACTER!!!!" + "▌\n".PadLeft(30);

            bool gameRunning = true;
            SplashScreen();
            player.Name = Console.ReadLine();
            //bool playerhasKey = false;
            //int roomnum = 0;
            Room currentRoom;
            string prompt = "What do you wish to do?";
            //string interactionBox = "1. Open Door, 2. Go into Hallway, 3. pick up key";

            while (gameRunning)
            {

                currentRoom = dungeon[player.position];

                renderer(); //yes, 'renderer'
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":   //should we handle invalid selections here or inside use?
                        if (currentRoom.RoomContents.Count >= 1)
                        { prompt = player.Use(currentRoom.RoomContents[0], player, currentRoom); }
                        break;
                    case "2":
                        if (currentRoom.RoomContents.Count >= 2)
                        { prompt = player.Use(currentRoom.RoomContents[1], player, currentRoom); }
                        break;
                    case "3":
                        if (currentRoom.RoomContents.Count >= 3)
                        { prompt = player.Use(currentRoom.RoomContents[2], player, currentRoom); }
                        break;
                    case "4":
                        if (currentRoom.RoomContents.Count >= 4)
                        { prompt = player.Use(currentRoom.RoomContents[3], player, currentRoom); }
                        break;
                    case "5":
                        if (currentRoom.RoomContents.Count >= 5)
                        { prompt = player.Use(currentRoom.RoomContents[4], player, currentRoom); }
                        break;
                    case "6":
                        if (currentRoom.RoomContents.Count >= 6)
                        { prompt = player.Use(currentRoom.RoomContents[5], player, currentRoom); }
                        break;
                    case "mural":
                        if (player.position == 4) 
                        {
                            player.Use(dungeon[4].HiddenContents[0], player, currentRoom);
                        }
                        break;
                    case "x":
                    case "exit":
                    case "quit":
                        if (player.HasMacguffin == true) { prompt = "You have succeeded where all have failed! HUZZAH";  }

                        else {  prompt = "you are a coward, shame upon your house!"; }
                        player.IsAQuitter = true;
                        break;
                    default:

                        prompt = "What, do you think you can just make up the rules! you can't do that!";
                        break;
                }//
                if (player.IsAQuitter == true)
                    {
                    renderer();
                    Console.ReadLine();
                    gameRunning = false;
                    }
                if (player.IsAlive == false)
                    {
                    renderer();
                    Console.ReadLine();
                    gameRunning = false; }
            }
            SplashScreen();
            Console.ReadLine();



            string GetInteractions(Room room)
            {
                string result = "";
                int i = 1;
                foreach (RoomObject interaction in room.RoomContents)
                {
                    result += $"             {i}. {interaction.Verbage}{interaction.Name}\n";
                    i += 1;
                }
                return result;
            }

            void renderer()
            {
                //should eventually offload this art to a text file maybe!!! could be cool to use special characters to programmatically load the different items
                //for individual coloring... but that would like, that would take a minute I think :P
                //like perhaps the 'renderer' actually foreaches through the characters and draws each one individually on the screen, 
                //                                          changing color based on what character it is?
                string linearttop = "" +
                    @"&╗===╔&__________________________________________________________________________________________________________&╗===╔&" +
                    @" ║ & ║ + | + | + | + | + | + | + | + | + | + | + | + | + |]<>[| + | + | + | + | + | + | + | + | + | + | + | + | + ║ & ║ " +
                    @" ║───║────────────────────────────────────────────────────────────────────────────────────────────────────────────║───║ " +
                    @"&╝===╚&                                                                                                          &╝===╚&";
                string lineartmiddle = "" +
                                    "                                                                                                                        " +
                                    "=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-(}-{-==-}-{)-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=" +
                                    "                                                                                                                        ";
                string lineartbotom = "" +
                    @"@╗===╔@                                                                                                          @╗===╔@" +
                    @" ║___║____________________________________________________________________________________________________________║___║ " +
                    @" ║ & ║ + | + | + | + | + | + | + | + | + | + | + | + | + |]<>[| + | + | + | + | + | + | + | + | + | + | + | + | + ║ & ║ " +
                    @"@╝===╚@──────────────────────────────────────────────────────────────────────────────────────────────────────────@╝===╚@";
                string leftcolumn = " ║ | ║    ";
                string rightcolumn = "    ║ | ║ ";


                //string descriptiontext = currentRoom.Description;
                /*
                string[] description = currentRoom.Description.Split(' ');

                StringBuilder newDescription = new StringBuilder();

                string line = "";
                foreach (string word in description)
                {
                    if ((line+word).Length > 95)
                    {
                        newDescription.Append(line+Environment.NewLine);
                        line = "";
                    }
                    line += word + " ";
                }*/

                Console.Clear();
                Console.Write(linearttop); //lines 1-4
                Console.Write(currentRoom.Description);
                Console.Write($"\n\n\n\n\n{lineartmiddle}\n\n\n");
                Console.Write("             " + prompt);
                Console.Write($"\n{lineartmiddle}\n\n");
                Console.Write(GetInteractions(currentRoom));
                Console.Write(lineartbotom);
                prompt = "";
            }

            void BuildDungeon()
            {
                RoomObject key1 = new RoomObject
                {
                    Name = "Red Key",
                    Type = "key",
                    ID = "redkey",
                    Verbage = "Pick up the "
                };
                RoomObject hallway1 = new RoomObject
                {
                    Name = "hallway", Type = "hallway", ID = "hall1",
                    Verbage = "Enter the ", ConnectionA = 5, ConnectionB = 4,
                };
                RoomObject door1 = new RoomObject
                {
                    Name = "Green Door", Type = "door",  ID = "door1",  Verbage = "Try the ",  
                    isLocked = true,  LockID = "greenkey",  ConnectionA = 3,  ConnectionB = 4,
                };
                RoomObject key3 = new RoomObject
                {
                    Name = "Green Key",
                    Type = "key",
                    ID = "greenkey",
                    Verbage = "Pick up the "
                };

                RoomObject macguffin = new RoomObject { Name = "macguffin", Type = "goober", ID = "goober", Verbage = "Pick up the " };

                RoomObject door2 = new RoomObject
                {
                    Name = "Red Door",
                    Type = "door",
                    ID = "door2",
                    Verbage = "Try the ",
                    isLocked = true,
                    LockID = "redkey",
                    ConnectionA = 2,
                    ConnectionB = 3
                };

                RoomObject key2 = new RoomObject
                {
                    Name = "Blue Key",
                    Type = "key",
                    ID = "bluekey",
                    Verbage = "Pick up "
                };

                RoomObject torch1 = new RoomObject
                {
                    Name ="Torch",
                    Type = "torch",
                    ID = "littorch",
                    Verbage = "Pick up ",
                };

                RoomObject Passageway1 = new RoomObject
                {
                    Name = "Passage",
                    Type = "passageway",
                    ID = "passageway1",
                    Verbage = "Walk down the ",
                    isLocked = true,
                    LockID = "littorch",
                    ConnectionA = 5,
                    ConnectionB = 6
                };

                RoomObject tripwire1 = new RoomObject
                {
                    Name = "Unassuming wire",
                    Type = "tripwire",
                    ID = "tripwire1",
                    Verbage = "Walk through the ",
                };


                RoomObject scroll1 = new RoomObject
                {
                    Name = "Scroll",
                    Type = "scroll",
                    ID = "1234",
                    Verbage = "Read the combination on the ",

                };

                RoomObject combidoor1 = new RoomObject
                {
                    Name = "Door",
                    Type = "door with combination lock",
                    ID = "combidoor1",
                    Verbage = "Try the ",
                    isLocked = true,
                    LockID = "1234",
                    ConnectionA = 1,
                    ConnectionB = 2
                };

                RoomObject chest1 = new RoomObject
                {
                    Name = "Chest",
                    Type = "chest",
                    ID = "chestscroll",
                    Verbage = "Try to open the ",
                    isLocked = true,
                    LockID = "bluekey",
                };

                RoomObject chest2 = new RoomObject
                {
                    Name = "Chest", Type = "chest", ID = "chestgoober", Verbage = "Try to open the ",
                };

                RoomObject mural = new RoomObject
                {
                    Name = "Mural", Type = "door", ID = "muraldoor", Verbage = "pull aside the ", ConnectionA =0, ConnectionB = 4
                };

                RoomObject sweetloot = new RoomObject
                {
                    Name = "Gold and Gems", Type = "loot", ID = "traploot", Verbage = "Cautiously approach the table and gather the ",
                };

                RoomObject realloot = new RoomObject
                {
                    Name = "Gold and Gems", Type = "loot", ID = "safeloot", Verbage = "Run forward with arms out-streched and grab all the ",
                };

                RoomObject secretpassage = new RoomObject { Name = "Passage", Type = "door", ID = "secret passage", Verbage = "Enter the ", ConnectionB = 1 };

                RoomObject bookshelf1 = new RoomObject { Name = "Bookshelf", Type = "door", ID = "secretpassagedoor", Verbage = "Check the ", ConnectionA = 0 };


                RoomObject exit = new RoomObject { Name = "Exit", Type = "exit", ID = "exit", Verbage = "Head for the " }; //verbage should be more verbose

                Room room4 = new Room()
                {
                    Description = "             Directly in front of you is a mural. To the east is a hallway. To the west is a door. \n" +
                                  "             Behind you is a table with a key on it.",
                    RoomContents = { door1, hallway1, key1, exit },
                    HiddenContents = {mural}
                };

                Room room3 = new Room()
                {
                    Description = "             This room is full of rusted suits of armor, poorly attached to armor stands \n" +
                                  "             which are arranged in a very haphazard way throughout the space.",
                    RoomContents = {door1, door2, torch1}
                };

                Room room5 = new Room()
                {
                    Description = "             This room is dark, you cannot see anything, but when you strain your ears you hear \n" +
                                    "             a very slight and extremely unpleasant gurgling sound.",
                    RoomContents = { hallway1, key3, Passageway1 }
                };

                Room room6 = new Room()
                {
                    Description = "             A room WITH SECRETS!!!!",
                                    RoomContents = { key2, Passageway1, chest1},
                                    HiddenContents = { scroll1 }
                };
                Room room1 = new Room()
                {
                    Description = "             This well appointed room features a large, ornate Chest against the west wall. \n" +
                    "             there is a bookshelf in the north side of the room from which several books seem to have \n" +
                    "             long ago fallen off.",
                    RoomContents = {combidoor1, chest2, secretpassage, tripwire1},
                    HiddenContents = { macguffin }
                    
                };
                Room room2 = new Room()
                {
                    Description = "             You seem to have found some kind of dining room. There are tapestries on the walls\n" +
                    "             and a long table set with what was once very stately flatware, long since deteriorated from\n" +
                    "             disuse.",
                    RoomContents = {door2, combidoor1}
                };
                Room room0 = new Room()
                {
                    Description = "             You found a secret room!! before you is a large table covered with gold and gems! \n",
                    RoomContents = {sweetloot, realloot, mural, secretpassage}
                };

                //need to resort this to reflect new dungeon layout!!!
                //List<Room> newDungeon = new List<Room>();
                dungeon.Add(room0); //starts here
                dungeon.Add(room1);
                dungeon.Add(room2);
                dungeon.Add(room3);
                dungeon.Add(room4);
                dungeon.Add(room5);
                dungeon.Add(room6);

                //return newDungeon;
            }

            void SplashScreen()
            {

                string lineset1 =   "&╗===╔&──────────────────────────────────────────────────────────────────────────────────────────────────────────&╗===╔&" +
                                    " ║ & ║ + | + | + | + | + | + | + | + | + | + | + | + | + |]<>[| + | + | + | + | + | + | + | + | + | + | + | + | + ║ & ║ " +
                                    " ║~~~║~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~║~~~║ " +
                                    " ║ & ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ & ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║ & ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ & ║ ";
                string lineset2 =   " ║ % ║░░░░░░░░░▓▓▓▓▓▓▓▓▓▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▓▓▓▓▓▓▓▓▓░░░║ % ║ " +
                                    " ║ & ║░░░░░░░░▒▓▓▒▒▒▒▒▒▒▓▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒▒▒▒▒▒▒▓▓▓░░║ & ║ " +
                                    " ║ % ║░░░░░░░░▒▓▓░░░░░░░▒▓▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓▓░░║ % ║ " +
                                    " ║ & ║░░░░░░░░▒▓▓░░░░░░░░▒▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓▓░░║ & ║ ";
                string lineset3 =   " ║ % ║░░░░░░░░▒▓▓░░░░░░░░▒▓▓░░░▓▓░░░░▒▓▓░░▓▓▓▓▓▓▓▓▓░░░▓▓▓▓▓▓▓▓▓▓░░░▓▓▓▓▓▓▓▓░░░▓▓▓▓▓▓▓▓▓░░▓▓▓▓▓▓▓▓▓░░░░░░░░░░░▒▓▓░░║ % ║ " +
                                    " ║ & ║░░░░░░░░▒▓▓░░░░░░░░▒▓▓░░▒▓▓░░░░▒▓▓▒▒▒▓▓▒▒▒▒▓▓░░▒▓▓▒▒▒▒▒▒▓▓░░▒▓▓▒▒▒▒▒░░░▒▓▓▒▒▒▒▒▓▓▒▒▒▓▓▒▒▒▒▓▓░░░░░░░░░▒▓▓▓░░░║ & ║ " +
                                    " ║ % ║░░░░░░░░▒▓▓░░░░░░░░▒▓▓░░▒▓▓░░░░▒▓▓░░▒▓▓░░░▒▓▓░░▒▓▓░░░░░▒▓▓░░▒▓▓░░░░░░░░▒▓▓░░░░▒▓▓░░▒▓▓░░░▒▓▓░░░░░░░▒▓▓▓░░░░░║ % ║ " +
                                    " ║ & ║░░░░░░░░▒▓▓░░░░░░░▒▓▓▓░░▒▓▓░░░░▒▓▓░░▒▓▓░░░▒▓▓░░▒▓▓░░░░░▒▓▓░░▒▓▓▓▓▓░░░░░▒▓▓░░░░▒▓▓░░▒▓▓░░░▒▓▓░░░░░░▒▓▓░░░░░░░║ & ║ ";
                String lineset4 =   " ║ % ║░░░░░░░░▒▓▓░░░░░░▒▓▓▓░░░▒▓▓░░░░▒▓▓░░▒▓▓░░░▒▓▓░░▒▓▓░░░░░▒▓▓░░▒▓▓▒▒░░░░░░▒▓▓░░░░▒▓▓░░▒▓▓░░░▒▓▓░░░░░░▒▒░░░░░░░░║ % ║ " +
                                    " ║ & ║░░░░░░░░▒▓▓▓▓▓▓▓▓▓▓▓░░░░▒▓▓▓▓▓▓▓▓▓░░▒▓▓░░░▒▓▓░░▒▓▓▓▓▓▓▓▓▓▓░░▒▓▓▓▓▓▓▓▓░░▒▓▓▓▓▓▓▓▓▓░░▒▓▓░░░▒▓▓░░░░░░▒▓▓░░░░░░░║ & ║ " +
                                    " ║ % ║░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒░░░░░▒▒▒▒▒▒▒▒▒░░░▒▒░░░░▒▒░░░▒▒▒▒▒▒▒▒▒▓▓░░▒▒▒▒▒▒▒▒░░░▒▒▒▒▒▒▒▒▒░░░▒▒░░░░▒▒░░░░░░░▒▒░░░░░░░░║ % ║ " +
                                    " ║ & ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ & ║ ";
                string lineset5 =   " ║ % ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║ & ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▓▓▓▓▓▓▓▓▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ & ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒▒▒▒▒▒▒▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ % ║ ";
                 string lineset6 =  " ║ & ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ & ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║ & ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ & ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║~~~║~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~║~~~║ " +
                                    " ║ & ║ + | + | + | + | + | + | + | + | + | + | + | + | + |]<>[| + | + | + | + | + | + | + | + | + | + | + | + | + ║ & ║ " +
                                    "@╝===╚@──────────────────────────────────────────────────────────────────────────────────────────────────────────@╝===╚@";

                string linesetwin = " ║ & ║░░░░░░░░░░░░░░░▓▓░░░░░▓▓░░░░▓▓▓▓▓▓▓▓░░░▓▓░░░░▓▓░░░░░░░░▓▓░░░░░░░░░░▓▓░░░░▓▓░░░▓▓▓░░▓▓░░░▓▓░░░░░░░░░░░░░░░░░░║ & ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░░░▓▓░░░▓▓░░░░░▓▓░░░░▓▓░░░▓▓░░░░▓▓░░░░░░░░░▓▓░░░░░░░░▓▓░░░░░▓▓░░░▓▓▓▓░▓▓░░░▓▓░░░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░░░░▓▓▓▓▓░░░░░░▓▓░░░░▓▓░░░▓▓░░░░▓▓░░░░░░░░░░▓▓░░▓▓░░▓▓░░░░░░▓▓░░░▓▓░▓▓▓▓░░░▓▓░░░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║ & ║░░░░░░░░░░░░░░░░░░▓▓▓░░░░░░░▓▓░░░░▓▓░░░▓▓░░░░▓▓░░░░░░░░░░░▓▓▓▓▓▓▓▓░░░░░░░▓▓░░░▓▓░░▓▓▓░░░░░░░░░░░░░░░░░░░░░░░║ & ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░░░░░▓▓▓░░░░░░░▓▓▓▓▓▓▓▓░░░▓▓▓▓▓▓▓▓░░░░░░░░░░░░▓▓░░▓▓░░░░░░░░▓▓░░░▓▓░░░▓▓░░░▓▓░░░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║~~~║~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~║~~~║ " +
                                    " ║ & ║ + | + | + | + | + | + | + | + | + | + | + | + | + |]<>[| + | + | + | + | + | + | + | + | + | + | + | + | + ║ & ║ " +
                                    "@╝===╚@──────────────────────────────────────────────────────────────────────────────────────────────────────────@╝===╚@";

                string linesetlose =" ║ & ║░░░░░░░░░░░░░▓▓░░░░░▓▓░░░░▓▓▓▓▓▓▓▓░░░▓▓░░░░▓▓░░░░░░░░▓▓▓▓▓▓▓▓░░░▓▓░░░░▓▓░░░▓▓▓▓▓▓▓▓░░░▓▓░░▓▓░░░░░░░░░░░░░░░░║ & ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░▓▓░░░▓▓░░░░░▓▓░░░░▓▓░░░▓▓░░░░▓▓░░░░░░░░▓▓░░░░░░░░░▓▓░░░░▓▓░░░▓▓░░░░░░░░░▓▓░▓▓░░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░░▓▓▓▓▓░░░░░░▓▓░░░░▓▓░░░▓▓░░░░▓▓░░░░░░░░▓▓▓▓▓▓▓▓░░░▓▓░░░░▓▓░░░▓▓░░░░░░░░░▓▓▓▓░░░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║ & ║░░░░░░░░░░░░░░░░▓▓▓░░░░░░░▓▓░░░░▓▓░░░▓▓░░░░▓▓░░░░░░░░░░░░░░▓▓░░░▓▓░░░░▓▓░░░▓▓░░░░░░░░░▓▓░▓▓░░░░░░░░░░░░░░░░░║ & ║ " +
                                    " ║ % ║░░░░░░░░░░░░░░░░▓▓▓░░░░░░░▓▓▓▓▓▓▓▓░░░▓▓▓▓▓▓▓▓░░░░░░░░▓▓▓▓▓▓▓▓░░░▓▓▓▓▓▓▓▓░░░▓▓▓▓▓▓▓▓░░░▓▓░░▓▓░░░░░░░░░░░░░░░░║ % ║ " +
                                    " ║~~~║~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~║~~~║ " +
                                    " ║ & ║ + | + | + | + | + | + | + | + | + | + | + | + | + |]<>[| + | + | + | + | + | + | + | + | + | + | + | + | + ║ & ║ " +
                                    "@╝===╚@──────────────────────────────────────────────────────────────────────────────────────────────────────────@╝===╚@";


                if (gameRunning == false)
                {

                    if (player.HasMacguffin)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(lineset1);
                        Console.Write(lineset2);
                        Console.Write(lineset3);
                        Console.Write(lineset4);
                        Console.Write(lineset5);
                        Console.Write(linesetwin);
                    }
                    else 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(lineset1);
                        Console.Write(lineset2);
                        Console.Write(lineset3);
                        Console.Write(lineset4);
                        Console.Write(lineset5);
                        Console.Write(linesetlose); 
                    }
                }
                else
                {

                    //Console.ForegroundColor = ConsoleColor.White;
                    //Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(lineset1);
                    Console.Write(lineset2);
                    Console.Write(lineset3);
                    Console.Write(lineset4);
                    Console.Write(lineset5);
                    Console.Write(lineset6);
                    //Console.ForegroundColor = ConsoleColor.DarkCyan;
                    //Console.ForegroundColor = ConsoleColor.Cyan;
                    //Console.ForegroundColor = ConsoleColor.DarkCyan;
                    //Console.ForegroundColor = ConsoleColor.DarkBlue;
                    //Console.ForegroundColor = ConsoleColor.Cyan;
                    //Console.ForegroundColor = ConsoleColor.DarkGray;
                    //Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
            }

        }


    }
}