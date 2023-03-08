using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using TextAdventureGameInputParser;
using TextAdventureGameInputParser.WordClass;

namespace TestConsole
{
    internal class Program
    {

        const bool DEBUG = true;
        const int MAX_INVENTORY = 5;
        const bool CHANGE = false;
        private static void Main()
        {

            var parser = CreateParser();
            Environment scene = new Environment();
            Sentence results;
            Player player1 = new Player(0);


            Console.WriteLine("Type 'help' or '?' for a list of commands\n");
            scene.CurrentRoom().lookCmd();
            do
            {
                Console.Write("\nParse what?> ");
                var input = Console.ReadLine() ?? "";
                Console.WriteLine();
                if (string.IsNullOrWhiteSpace(input))
                    return;
                results = parser.Parse(input);
                executeCommand(results, parser, scene, player1);
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
            parser.Aliases.Add("TAKE", "GRAB", "PICK");

            return parser;
        }



        private static void executeCommand(Sentence results, Parser parser, Environment scene, Player player1)
        {

            if (DEBUG)
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
                        Player.CurrentRoom.movement(results.Word4.Value, player1);
                        scene.CurrentRoom().lookCmd();
                        break;
                    case "LOOK":
                        scene.CurrentRoom().lookCmd();
                        break;
                    case "INVENTORY":
                        if (player1.Inventory.Count == 0)
                        {
                            Console.WriteLine("You are not carrying anything");
                        } else
                        {
                            Console.WriteLine("You are carrying");
                            foreach(string item in player1.Inventory)
                            {
                                Console.WriteLine("  {0}", item);
                            }
                        }
                        break;
                    case "TAKE":
                        if (player1.Inventory.Count == MAX_INVENTORY)
                        {
                            Console.WriteLine("You cannot carry any more items");
                            Console.WriteLine("Drop an item first");
                        } else
                        {
                            string item = parseItem((results.Word4.Value).ToLower(),
                                results.Word4.PrecedingAdjective == null ? null : (results.Word4.PrecedingAdjective.Value).ToLower());
                            Player.CurrentRoom.take(item, player1);
                        }
                        break;
                    case "DROP":
                        if (player1.Inventory.Count == 0)
                        {
                            Console.WriteLine("You don't have any items to drop");
                        }
                        else
                        {
                            string item = parseItem((results.Word4.Value).ToLower(), 
                                results.Word4.PrecedingAdjective == null ? null : (results.Word4.PrecedingAdjective.Value).ToLower());
                            Player.CurrentRoom.drop(item, player1);
                        }
                        break;
                    default:
                        Console.WriteLine("Dunno");
                        break;
                }
            }

        }

        static string parseItem (string noun, string adjective) {
            string item;

            if (adjective != null)
            {
                item = adjective + " " + noun;
            }
            else
            {
                item = noun;
            }
            return item;
        }



    }
}