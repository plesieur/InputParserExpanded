using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
/*    public class Direction
    {
        private int[] _dir;

        public Direction(int[] direction)
        {
            _dir = new int[6];
            for (int i = 0; i < 6; i++)
            {
                _dir[i] = direction[i];
            }
        }

        public int North() { return _dir[0]; }
        public int East() { return _dir[1]; }
        public int South() { return _dir[2]; }
        public int West() { return _dir[3]; }
        public int Up() { return _dir[4]; }
        public int Down() { return _dir[5]; }
    }
*/
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

/*      
        public int North() { return _direction[0]; }
        public int East() { return _direction[1]; }
        public int South() { return _direction[2]; }
        public int West() { return _direction[3]; }
        public int Up() { return _direction[4]; }
        public int Down() { return _direction[5]; }
*/
        public void movement(string direction, ref int location)
        {
            List<string> directions = new List<string>() { "NORTH", "EAST", "SOUTH", "WEST", "UP", "DOWN" };

            int dir = directions.IndexOf(direction);
            location = this.Dir[dir];
        }

    }

}
