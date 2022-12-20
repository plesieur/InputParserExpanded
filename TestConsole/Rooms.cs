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

}
