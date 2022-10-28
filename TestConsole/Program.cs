using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TextAdventureGameInputParser;
using TextAdventureGameInputParser.WordClass;

namespace TestConsole
{
    internal class Program
    {
        const bool debug = false;
        private static void Main()
        {

            var parser = CreateParser();
            Sentence results;
            do
            {
                Console.Write("\nParse what?> ");
                var input = Console.ReadLine() ?? "";
                Console.WriteLine();
                if (string.IsNullOrWhiteSpace(input))
                    return;
                results = parser.Parse(input);
                executeCommand(results, parser);
            } while (true);
        }

        private static Parser CreateParser()
        {
            var parser = new Parser();

            parser.AddVerbs("GO", "OPEN", "CLOSE", "GIVE", "SHOW", "LOOK", "INVENTORY", "GET", "TAKE", "DROP", "USE", "HELP", "QUIT");
            parser.AddImportantFillers("TO", "ON", "IN");
            parser.AddUnimportantFillers("THE", "A", "AN", "AT");
            parser.AddNouns(
                "NORTH",
                "EAST",
                "WEST",
                "SOUTH",
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
            parser.Aliases.Add("INVENTORY", "I", "INV");
            parser.Aliases.Add("HELP", "H", "?");

            return parser;
        }

        private static void executeCommand(Sentence results, Parser parser)
        {
            if (debug)
            {
                Console.WriteLine(results);   //print debug info about parsed sentence
            }
            if (!results.ParseSuccess)
            {
                Console.WriteLine("Excuse Me?");   //Did not recognize command
            }
            else
            {
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
                    default:
                        Console.WriteLine("Dunno");
                        break;
                }
            }

        }
    }
}