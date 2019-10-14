using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLogic
{
    public class Row
    { 
        public int[] gridNum { get; set; }
        public Row(bool fourByFour)
        {
            if(fourByFour)
            {
                this.gridNum = new int[4];
            }
            else
            {
                this.gridNum = new int[6];
            }
        }

        public Row( bool fourByFour, bool debug) : this(fourByFour: false)
        {
            if(debug)
            {
                for (int i = 0; i < gridNum.Length; i++)
                {
                    gridNum[i] = i + 1;
                }
            }
        }

        public Row(bool fourByFour, int[] entries) : this(false)
        {
            this.gridNum = entries;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{gridNum[0]} ");
            for(int i = 1; i < gridNum.Length; i++)
            {
                sb.Append($"{gridNum[i]} ");
            }
            return sb.ToString();
        }

        public void generateRandomNumbers()
        {
            Random r = new Random();
            for (int i = 0; i < gridNum.Length; i++)
            {
                gridNum[i] = r.Next(1, gridNum.Length + 1);
            }
        }
    }

    public class NumberGrid
    {
        public int ID { get; set; }
        public Row[] completeGrid { get; set; }
        public int gridSize { get; set; }
        public NumberGrid(bool fourByFour)
        {
            if (fourByFour)
            {
                this.gridSize = 4;
            }
            else
            {
                this.gridSize = 6;
            }

            this.completeGrid = new Row[this.gridSize];
        }
    }

    [Flags]
    public enum GridLocation
    {
        TOP     = 0b0000001,
        MIDDLE  = 0b0000010,
        BOTTOM  = 0b0000100,
        LEFT    = 0b0001000,
        RIGHT   = 0b0010000,
        NONE    = 0b0000000  
    }
}
