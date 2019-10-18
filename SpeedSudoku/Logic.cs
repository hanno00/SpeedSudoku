using SudokuLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedSudoku
{
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
            HashSet<int> checkSet = new HashSet<int>();

            int[] set = (gridArray.Length == 4) ? new int[] { 1, 2, 3, 4 } : new int[] { 1, 2, 3, 4, 5, 6 };

            checkSet.UnionWith(set);

            HashSet<int> intSet = new HashSet<int>();
            foreach (int i in gridArray)
            {
                intSet.Add(i);
            }

            return intSet.Count == gridArray.Length && intSet.SetEquals(checkSet);
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
                case (GridLocation)9:
                    { //Top Left
                        r1 = grid.completeGrid[0];
                        r2 = grid.completeGrid[1];
                        int[] front = SubArray<int>(r1.rowNum, 0, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, 0, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                case (GridLocation)10:
                    { //Middle Left
                        r1 = grid.completeGrid[2];
                        r2 = grid.completeGrid[3];
                        int[] front = SubArray<int>(r1.rowNum, 0, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, 0, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                case (GridLocation)12:
                    { //Bottom Left
                        r1 = grid.completeGrid[4];
                        r2 = grid.completeGrid[5];
                        int[] front = SubArray<int>(r1.rowNum, 0, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, 0, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                case (GridLocation)17:
                    {//Top Right
                        r1 = grid.completeGrid[0];
                        r2 = grid.completeGrid[1];
                        int[] front = SubArray<int>(r1.rowNum, grid.gridSize / 2, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, grid.gridSize / 2, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                case (GridLocation)18:
                    {//Middle Right
                        r1 = grid.completeGrid[2];
                        r2 = grid.completeGrid[3];
                        int[] front = SubArray<int>(r1.rowNum, grid.gridSize / 2, grid.gridSize / 2);
                        int[] back = SubArray<int>(r2.rowNum, grid.gridSize / 2, grid.gridSize / 2);
                        entries = front.Concat(back).ToArray();
                        break;
                    }
                case (GridLocation)20:
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

        public static bool checkCompleteGrid(NumberGrid g)
        {
            bool[] completeRowCheck = new bool[g.gridSize];
            bool[] completeSmallGridCheck = new bool[g.gridSize];

            int[] locations = (g.gridSize == 4) ? new int[] { 9, 10, 17, 18 } : new int[] { 9, 10, 12, 17, 18, 20 };


            for (int i = 0; i < g.gridSize; i++)
            {
                completeRowCheck[i] = checkRow(g.completeGrid[i], g);
                completeSmallGridCheck[i] = checkSmallGrid(g, (GridLocation)locations[i]);
            }

            Console.WriteLine();
            int truthCount = 0;
            for (int j = 0; j < g.gridSize; j++)
            {
                if (completeRowCheck[j] && completeSmallGridCheck[j])
                {
                    truthCount++;
                }
            }
            return truthCount == g.gridSize;
        }
    }
}
