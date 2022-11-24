using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TextAdventureGameInputParser;
using TextAdventureGameInputParser.WordClass;

namespace TestConsole
{
    internal class Program
    {

        const bool debug = true;
        const int maxInventory = 5;
        private static void Main()
        {

            var parser = CreateParser();
            var scene = CreateRooms();
            int roomIndex = 0;
            Room currenrRoom = scene[0];
            Sentence results;
            List<string>inventory = new List<string>();


            lookCmd(scene[roomIndex]);
            do
            {
                Console.Write("\nParse what?> ");
                var input = Console.ReadLine() ?? "";
                Console.WriteLine();
                if (string.IsNullOrWhiteSpace(input))
                    return;
                results = parser.Parse(input);
                executeCommand(results, parser, ref roomIndex, scene, inventory);
            } while (true);
        }

        private static Parser CreateParser()
        {
            var parser = new Parser();

            parser.AddVerbs("GO", "OPEN", "CLOSE", "GIVE", "SHOW", "LOOK", "INVENTORY", "GET", "TAKE", "DROP", "USE", "EXAMINE", "HELP", "QUIT");
            parser.AddImportantFillers("TO", "ON", "IN");
            parser.AddUnimportantFillers("THE", "A", "AN", "AT");
            parser.AddNouns(
                "NORTH",
                "EAST",
                "WEST",
                "SOUTH",
                "UP",
                "DOWN",
                "GREEN DOOR",
                "BLUE DOOR",
                "SKELETON KEY",
                "GOLD KEY",
                "CHEST"
            );
            parser.Aliases.Add("GO NORTH", "N", "NORTH");
            parser.Aliases.Add("GO EAST", "E", "EAST");
            parser.Aliases.Add("GO SOUTH", "S", "SOUTH");
            parser.Aliases.Add("GO WEST", "W", "WEST");
            parser.Aliases.Add("GO UP", "U", "UP");
            parser.Aliases.Add("GO DOWN", "D", "DOWN");
            parser.Aliases.Add("INVENTORY", "I", "INV");
            parser.Aliases.Add("HELP", "H", "?");
            parser.Aliases.Add("QUIT", "Q", "EXIT");

            return parser;
        }

        private static List<Room> CreateRooms()
        {
            List<Room> rooms = new List<Room>();

            rooms.Add(new Room()
            {
                Name = "Central Hall",
                Description = "Main room of this test house.\nThere are exits to the North, South, East and West.",
                Items = new List<string> { },
                Dir = new int[6] { 1, 2, 3, 4, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "North Room",
                Description = "North chamber of the house.\nThere is one exit to the south.",
                Items = new List<string> { "Gold Key" },
                Dir = new int[6] { -1, -1, 0, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "East Room",
                Description = "East chamber of the house. \nThere is a ladder going up and a staircase going down.\nThere is another exit to the West.",
                Items = new List<string> { },
                Dir = new int[6] { -1, -1, -1, 0, 5, 6 }
            });

            rooms.Add(new Room()
            {
                Name = "South Room",
                Description = "South chamber of the house. \nThere is a Blue Door to the west and a Green Door to the east.\nThere is an exit to the North.",
                Items = new List<string> { "Blue Door", "Green Door" },
                Dir = new int[6] { 0, -1, -1, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "West Room",
                Description = "West chamber of the house'\nThere is an exit to the East.",
                Items = new List<string> { "Skeleton Key" },
                Dir = new int[6] { -1, 0, -1, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Attic",
                Description = "Attic of the house.  \nThe roof is very low and you must stoop to walk through it.\nThere is a ladder going down.",
                Items = new List<string> {  },
                Dir = new int[6] { -1, -1, -1, -1, -1, 2 }
            });

            rooms.Add(new Room()
            {
                Name = "Cellar",
                Description = "The cellar of the house.  \nThere are empty wine racks along the walls.\nThere is a staircase going up.",
                Items = new List<string> { },
                Dir = new int[6] { -1, -1, -1, -1, 0, -1 }
            });

            return rooms;
        }
        private static void executeCommand(Sentence results, Parser parser, ref int roomIndex, List<Room> scene, List<string>inventory)
        {
            Room currentRoom;
            currentRoom = scene[roomIndex];

            if (debug)
            {
                Console.WriteLine(results);   //print debug info about parsed sentence
            }
            if (!results.ParseSuccess)
            {
                Console.WriteLine("Excuse Me?");   //Did not recognize command
            }
            else if (results.Ambiguous)
            {
                Console.WriteLine("Be more specfic with {0}", results.Word4.Value.ToLower());
            } else { 
                switch (results.Word1.Value)
                {
                    case "HELP":
                        Console.WriteLine("COMMANDS\n--------\n");
                        parser.PrintVerbs();
                        break;
                    case "QUIT":
                        Console.WriteLine("See Ya\n");
                        System.Environment.Exit(0);
                        break;
                    case "GO":
                        currentRoom.movement(results.Word4.Value,ref roomIndex);
                        currentRoom = scene[roomIndex];
                        lookCmd(scene[roomIndex]);
                        break;
                    case "LOOK":
                        lookCmd(scene[roomIndex]);
                        break;
                    case "INVENTORY":
                        if (inventory.Count == 0)
                        {
                            Console.WriteLine("You are not carrying anything");
                        } else
                        {
                            Console.WriteLine("You are carrying");
                            foreach(string item in inventory)
                            {
                                Console.WriteLine("  {0}", item);
                            }
                        }
                        break;
                    case "TAKE":
                        if (inventory.Count == maxInventory)
                        {
                            Console.WriteLine("You cannot carry any more items");
                            Console.WriteLine("Drop an item first");
                        } else
                        {
                            string noun = (results.Word4.Value).ToLower();
                            string adjective = (results.Word4.PrecedingAdjective.Value).ToLower();
                            string item;
                            if (adjective != null)
                            {
                                item = adjective + " " + noun;
                            } else
                            {
                                item = noun;
                            }

                            Console.WriteLine("You have taken {0}", item);
                            inventory.Add(item);
                            scene[roomIndex].Items.Remove(item);
                            
                        }
                        break;
                    default:
                        Console.WriteLine("Dunno");
                        break;
                }
            }

        }

        private static void lookCmd(Room room)
        {
            Console.WriteLine("You are in the {0}", room.Name);
            Console.WriteLine(room.Description);
            if (room.Items.Count > 0)
            {
                Console.WriteLine("You see");
                foreach (string item in room.Items) { Console.WriteLine("  {0}", item); }
            }
        }


    }
}