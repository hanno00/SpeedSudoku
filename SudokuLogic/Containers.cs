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

        public Row(int[] entries) 
        {
            this.rowNum = entries;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{rowNum[0]} ");
            for(int i = 1; i < rowNum.Length; i++)
            {
                sb.Append($"{rowNum[i]}");
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

    [Serializable]
    public class NumberGrid
    {
        public Row[] completeGrid { get; set; }
        public int gridSize { get; set; }
        public NumberGrid()
        {
            this.gridSize = 4;

            this.completeGrid = new Row[this.gridSize];
        }

        public NumberGrid(int[] data)
        {

            if (data.Length == 16)
                this.gridSize = 4;
            else
                this.gridSize = 6;

            this.completeGrid = new Row[this.gridSize];

            ConvertDataToLines(data, completeGrid);
        }

        public NumberGrid(string dataString) : this(data: dataString.ToCharArray().Select(n => Convert.ToInt32(n)).ToArray())
        {
            // Misschien een beetje onduidelijk/lelijk. Maar ik pak hier de andere constructor die met int[] kan omgaan. 
            // " : this(*dit hier*)" voert meteen de string -> int[] conversion uit met LINQ. 
            // Scheelt weer code. 
        }

        public override string ToString()
        {
            string s = "";

            foreach(Row r in completeGrid)
            {
                foreach (int value in r.rowNum) {
                    s += value;
                }
            }
            return s;
        }

        public void ConvertDataToLines(int[] data, Row[] rowData)
        {
            int stepSize = (data.Length == 16) ? 4 : 6;

            int i = 0;
            while (i != data.Length) { 
                rowData[i/stepSize] = new Row(Logic.SubArray<int>(data, i, stepSize));
                i += stepSize;
            }
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
