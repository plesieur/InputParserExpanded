using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestConsole
{
    public class Player
    {
        private Room _currentRoom;
        private List<string> _inventory;

        public Player()
        {
            _currentRoom = new Room();
            _inventory = new List<string>();
        }
        public Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }
        public List<string> Inventory { get { return _inventory; } }
    }
}
