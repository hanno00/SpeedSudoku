using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SudokuLogic
{
    public class SudokuReader
    {
        public string FilePath { get; set; }
        public dynamic JsonFileText { get; set; }
        public Dictionary<int, NumberGrid> SudokuDictionary4 { get; set; }
        public Dictionary<int, NumberGrid> SudokuDictionary6 { get; set; }

        public SudokuReader(string FilePath)
        {
            this.FilePath = FilePath;
            this.SudokuDictionary4 = new Dictionary<int, NumberGrid>();
            this.SudokuDictionary6 = new Dictionary<int, NumberGrid>();
            this.ConvertFileToDynamics();
            CreateDictionaries();
        }

        public bool PrintDeserializedFile()
        {
            JsonTextReader result = new JsonTextReader(new StreamReader(FilePath));
            while(result.Read())
            {
                Console.WriteLine(result.Value);
            }

            return true;
        }

        public bool ConvertFileToDynamics()
        {
            JsonSerializer serializer = new JsonSerializer();
            
            this.JsonFileText = serializer.Deserialize<dynamic>(new JsonTextReader(new StreamReader(FilePath)));

            return true;
        }

        private NumberGrid ConvertDataToGrid(string data)
        {
            int[] numbers = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                numbers[i] = (int)char.GetNumericValue(data.ToCharArray()[i]);
            }


            return new NumberGrid(numbers); 
        }

        #region Helper Methods

        private void CreateDictionaries()
        {
            CreateDictionary4();
            CreateDictionary6();
            //PrintSudokuDict(SudokuDictionary4);
            //PrintSudokuDict(SudokuDictionary6);
        }

        private void CreateDictionary6()
        {
            int IDCount = 0;
            foreach (var item in JsonFileText.six.sudokuData)
            {
                string s = item;
                SudokuDictionary6.Add(IDCount, ConvertDataToGrid(s));
                IDCount++;
            }
        }

        private void CreateDictionary4()
        {
            int IDCount = 0;
            foreach (var item in JsonFileText.four.sudokuData)
            {
                string s = item;
                SudokuDictionary4.Add(IDCount, ConvertDataToGrid(s));
                IDCount++;
            }
        }

        public NumberGrid getRandomSudoku(int size)
        {
            return (size == 4) ? RandomDictEntry(SudokuDictionary4) : RandomDictEntry(SudokuDictionary6);
        }

        private static NumberGrid RandomDictEntry(Dictionary<int, NumberGrid> dictionary)
        {
            List<int> keys = new List<int>(dictionary.Keys);
            Random r = new Random();
            int randKey = keys[r.Next(keys.Count)];
            return dictionary[randKey];
        }

        private void PrintSudokuDict(Dictionary<int, NumberGrid> dictionary)
        {
            foreach(KeyValuePair<int, NumberGrid> kvp in dictionary)
            {
                Console.WriteLine($"ID = {kvp.Key} \nGrid = \n{kvp.Value}");
            }
        }
        #endregion
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
            //list.Add(JsonConvert.DeserializeObject(convertSudokuToJson(g.ID, g)));
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
