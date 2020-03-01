using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake
    {
        public int X { get; set;}
        public int Y { get; set; }
        public int Length { get; set; }

        public Snake()
        {
            //Reseting Location of Snake to 0,0
            X = 0;
            Y = 0;
        }

    }
}
