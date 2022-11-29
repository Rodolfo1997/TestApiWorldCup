using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiWorldCup
{
    public class TeamDTO
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public int Titles { get; set; }
        public string Shield { get; set; }
        public PlayerDTO BestPlayer { get; set; }
        public List<PlayerDTO> Players { get; set; }
    }
}
