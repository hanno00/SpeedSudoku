using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            
            bool fourByFour = false;
            NumberGrid g = new NumberGrid(fourByFour);

            g.completeGrid[0] = new Row(fourByFour, new int[] { 2, 3, 5, 6, 1, 4 });
            g.completeGrid[1] = new Row(fourByFour, new int[] { 1, 4, 6, 5, 2, 3 });
            g.completeGrid[2] = new Row(fourByFour, new int[] { 6, 1, 2, 4, 3, 5 });
            g.completeGrid[3] = new Row(fourByFour, new int[] { 3, 5, 4, 2, 6, 1 });
            g.completeGrid[4] = new Row(fourByFour, new int[] { 5, 2, 3, 1, 4, 6 });
            g.completeGrid[5] = new Row(fourByFour, new int[] { 4, 6, 1, 3, 5, 2 });

            //g.completeGrid[0] = new Row(fourByFour, new int[] { 1, 3, 2, 4 });
            //g.completeGrid[1] = new Row(fourByFour, new int[] { 2, 4, 1, 3 });
            //g.completeGrid[2] = new Row(fourByFour, new int[] { 3, 1, 4, 2 });
            //g.completeGrid[3] = new Row(fourByFour, new int[] { 4, 2, 3, 1 });

            Console.WriteLine(g);

            Console.WriteLine(Logic.checkCompleteGrid(g, fourByFour) ? $"Grid is correct!" : $"Grid is incorrect!");
            
            SudokuWriter s = new SudokuWriter();
            s.createJsonObject();
            Console.WriteLine(s.JsonObject);
            s.addSudoku6(g);
            Console.WriteLine(s.JsonObject);
            
            Console.ReadKey();
        }
    }

    static class Logic
    {

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private static bool Check(int[] gridArray)
        {
            HashSet<int> intSet = new HashSet<int>();
            foreach (int i in gridArray)
            {
                intSet.Add(i);
            }

            return intSet.Count == gridArray.Length;
        }

        public static bool checkColumn(int column, NumberGrid g)
        {
            int columnNumber = column - 1;

            ArrayList entriesList = new ArrayList();

            //for(int i = 0; i < gridSize; i++)
            //{
            //    entries[i] = r.gridNum[columnNumber];
            //}

            foreach (Row r in g.completeGrid)
            {
                entriesList.Add(r.rowNum[columnNumber]);
            }

            int[] entries = (int[])entriesList.ToArray(typeof(int));

            return Check(entries);
        }

        public static bool checkRow(int row, NumberGrid g)
        {
            int[] entries = g.completeGrid[row].rowNum;
            return Check(entries);
        }

        public static bool checkRow(Row r, NumberGrid g)
        {
            return Check(r.rowNum);
        }

        public static bool checkSmallGrid(NumberGrid grid, GridLocation g)
        {
            // Neem een deel van de array over 
            // Doe dat voor beide rijen
            // Gooi ze samen in 1 array
            // En check ze met de Check() methode
            // Zoek een manier het te laten werken met 4 size én 6 size.
            
            Row r1, r2;
            int[] entries;
            switch (g)
            {
                case (GridLocation) 9:
                    { //Top Left
                        r1 = grid.completeGrid[0];
                        r2 = grid.completeGrid[1];
                        int[] front = SubArray<int>(r1.rowNum, 0, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, 0, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                case (GridLocation) 10:
                    { //Middle Left
                        r1 = grid.completeGrid[2];
                        r2 = grid.completeGrid[3];
                        int[] front = SubArray<int>(r1.rowNum, 0, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, 0, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                case (GridLocation) 12:
                    { //Bottom Left
                        r1 = grid.completeGrid[4];
                        r2 = grid.completeGrid[5];
                        int[] front = SubArray<int>(r1.rowNum, 0, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, 0, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                case (GridLocation) 17:
                    {//Top Right
                        r1 = grid.completeGrid[0];
                        r2 = grid.completeGrid[1];
                        int[] front = SubArray<int>(r1.rowNum, grid.gridSize / 2, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, grid.gridSize / 2, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                case (GridLocation) 18:
                    {//Middle Right
                        r1 = grid.completeGrid[2];
                        r2 = grid.completeGrid[3];
                        int[] front = SubArray<int>(r1.rowNum, grid.gridSize / 2, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, grid.gridSize / 2, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                case (GridLocation) 20:
                    {//Bottom Right
                        r1 = grid.completeGrid[4];
                        r2 = grid.completeGrid[5];
                        int[] front = SubArray<int>(r1.rowNum, grid.gridSize / 2, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, grid.gridSize / 2, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                default:
                    { 
                        r1 = grid.completeGrid[0];
                        r2 = grid.completeGrid[1];
                        entries = new int[] { 0, 0, 0, 0, 0, 0 };
                        break;
                    }
            }

            return Check(entries); //ez pz lemon squeezy
        }

        public static bool checkCompleteGrid(NumberGrid g, bool fourByFour)
        {
            bool[] completeRowCheck = new bool[g.gridSize];
            bool[] completeSmallGridCheck = new bool[g.gridSize];
            //int[] locations = new int[] { 9, 10, 12, 17, 18, 20 };
            int[] locations = fourByFour ? new int[] { 9, 10, 17, 18 } : new int[] { 9, 10, 12, 17, 18, 20};
            //foreach(Row r in g.completeGrid)
            //{
            //    checkRow(r, g);
            //}

            for (int i = 0; i < g.gridSize; i++)
            {
                completeRowCheck[i] = checkRow(g.completeGrid[i], g);
                completeSmallGridCheck[i] = checkSmallGrid(g, (GridLocation)locations[i]);
            }

            Console.WriteLine();
            int truthCount = 0;
            for (int j = 0; j < g.gridSize; j++)
            {
                //if (completeRowCheck[j])
                //{
                //    Console.WriteLine($"Row {j + 1}: Correct");
                //}
                //else
                //{
                //    Console.WriteLine($"Row {j + 1}: Incorrect");
                //}

                //if (completeSmallGridCheck[j])
                //{
                //    Console.WriteLine($"Grid {(GridLocation)locations[j]}: Correct");
                //}
                //else
                //{
                //    Console.WriteLine($"Grid {(GridLocation)locations[j]}: Incorrect");
                //}

                if (completeRowCheck[j] && completeSmallGridCheck[j])
                {
                    truthCount++;
                }
                
            }

            return truthCount == g.gridSize;
        }
    }
}
