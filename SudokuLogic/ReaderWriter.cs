using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SudokuLogic
{
    class SudokuReader
    {
        public string FilePath { get; set; }
    }


    class SudokuWriter
    {
        public string JsonObject { get; set; }


        public void createJsonObject()
        {
            JsonObject = JsonConvert.SerializeObject(new
            {
                four = new[]
                {
                    new { }

                } ,
                six = new []
                {
                    new { }
                }

            });
        }

        public void addSudoku4(NumberGrid g)
        {
            dynamic deserialized = JsonConvert.DeserializeObject(this.JsonObject);
            var list = deserialized.four;
            list.Add(g.ToString());
            var updatedList = JsonConvert.SerializeObject(list, Formatting.Indented);

        }

        public void addSudoku6(NumberGrid g)
        {
            dynamic deserialized = JsonConvert.DeserializeObject(this.JsonObject);
            var list = deserialized.six;
            //Console.WriteLine(list);
            list.Add(JsonConvert.DeserializeObject(convertSudokuToJson(g.ID, g)));
            //Console.WriteLine(list);
            var updatedList = JsonConvert.SerializeObject(deserialized.six, Formatting.Indented);
        }

        static string convertSudokuToJson(int id, NumberGrid g)
        {
            string sudokuData = rewriteSudoku(g);

            return JsonConvert.SerializeObject(new
            {
                sudokuID = id,
                sudokuData = sudokuData,
            });
        }

        private static string rewriteSudoku(NumberGrid g)
        {
            string s = "";
            foreach (Row r in g.completeGrid)
            {
                s += r;
            }
            return s;
        }
    }
}
