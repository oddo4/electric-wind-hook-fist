using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper.Model
{
    public class JsonHelper
    {
        private string fileName;
        private string extension = ".json";

        public JsonHelper(string fileName)
        {
            this.fileName = fileName;
        }

        public ObservableCollection<ObservableCollection<Score>> ReadFile()
        {
            ObservableCollection<ObservableCollection<Score>> col = new ObservableCollection<ObservableCollection<Score>>();
            try
            {
                string fileString = File.ReadAllText(fileName + extension);
                var result = JsonConvert.DeserializeObject<List<List<Score>>>(fileString);

                foreach (List<Score> rowData in result)
                {
                    ObservableCollection<Score> row = new ObservableCollection<Score>();
                    foreach (Score data in rowData)
                    {
                        row.Add(data);
                    }
                    col.Add(row);
                }

                return col;
            }
            catch
            {
                return null;
            }
        }

        public bool WriteFile(ObservableCollection<ObservableCollection<Score>> col)
        {
            try
            {
                string json = JsonConvert.SerializeObject(col);
                File.WriteAllText(fileName + extension, json);

                return true;
            }
            catch
            {
                
            }

            return false;
        }
    }
}
