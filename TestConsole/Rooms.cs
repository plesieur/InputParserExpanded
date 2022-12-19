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
        //        private Direction _travel;
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

        public void movement(string direction, ref int location)
        {
            List<string> directions = new List<string>() { "NORTH", "EAST", "SOUTH", "WEST", "UP", "DOWN" };

            int dir = directions.IndexOf(direction);
            if (this.Dir[dir] != -1)
            {
                location = this.Dir[dir];
            } else
            {
                Console.WriteLine("I don't know how to go that way!\n");
            }
        }

    }

}
