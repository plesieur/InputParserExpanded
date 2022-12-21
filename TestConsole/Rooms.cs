using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    public class Room
    {
        private string _name;
        private string _description;
        private List<string> _items = new List<string>();
        private int[] _direction;

        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public List<string> Items
        {
            get { return _items; }
            set
            {
                foreach (string item in value)
                {
                    _items.Add(item.ToLower());
                }
            }
        }

        public int[] Dir {
            get { return _direction; }
            set {
                _direction = value;
            }
        }

        public void movement(string direction, Player player1)
        {
            List<string> directions = new List<string>() { "NORTH", "EAST", "SOUTH", "WEST", "UP", "DOWN" };
            int location = Player.RoomIndex;

            int dir = directions.IndexOf(direction);
            if (this.Dir[dir] != -1)
            {
                location = this.Dir[dir];
                Player.RoomIndex = location;
                Player.CurrentRoom = Environment.Scene[location];
            } else
            {
                Console.WriteLine("I don't know how to go that way!\n");
            }
        }

        public void take(string item, Player player1)
        {

            if (this.Items.Contains(item))
            {
                player1.Inventory.Add(item);
                this.Items.Remove(item);
                Console.WriteLine("You have taken {0}", item);
            } else
            {
                Console.WriteLine("I don't see the {0}", item);
            }

        }
    }

    public class Environment {

        private static List<Room> _scene = new List<Room>();
        public Environment()
        {
            _scene = CreateRooms();
        }

        public static List<Room> Scene { get { return _scene; } } 

        public Room CurrentRoom()
        {
            return _scene [Player.RoomIndex];
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
                Items = new List<string> { },
                Dir = new int[6] { -1, -1, -1, -1, -1, 2 }
            });

            rooms.Add(new Room()
            {
                Name = "Cellar",
                Description = "The cellar of the house.  \nThere are empty wine racks along the walls.\nThere is a staircase going up.",
                Items = new List<string> { },
                Dir = new int[6] { -1, -1, -1, -1, 2, -1 }
            });

            return rooms;
        }

    }

}
