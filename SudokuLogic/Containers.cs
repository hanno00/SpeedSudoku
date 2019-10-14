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
        public int[] rowNum { get; set; }
        public Row(bool fourByFour)
        {
            if(fourByFour)
            {
                this.rowNum = new int[4];
            }
            else
            {
                this.rowNum = new int[6];
            }
        }

        public Row( bool fourByFour, bool debug) : this(fourByFour: false)
        {
            if(debug)
            {
                for (int i = 0; i < rowNum.Length; i++)
                {
                    rowNum[i] = i + 1;
                }
            }
        }

        public Row(bool fourByFour, int[] entries) : this(false)
        {
            this.rowNum = entries;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{rowNum[0]} ");
            for(int i = 1; i < rowNum.Length; i++)
            {
                sb.Append($"{rowNum[i]} ");
            }
            return sb.ToString();
        }

        public void generateRandomNumbers()
        {
            Random r = new Random();
            for (int i = 0; i < rowNum.Length; i++)
            {
                rowNum[i] = r.Next(1, rowNum.Length + 1);
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

        public NumberGrid(bool fourByFour, int id)
        {
            this.ID = id;
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

        public override string ToString()
        {
            string s = "";

            foreach(Row r in completeGrid)
            {
                s += r.ToString() + "\n";
            }
            return s;
        }
    }

    public class Number
    {
        public int Value { get; set; }
        public bool Modifiable { get; set; }

        public Number(int number, bool modifiable) {
            this.Value = number;
            this.Modifiable = modifiable;
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
