using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_20180619.Classes
{
    public class JsonHelper
    {
        private string fileName;
        private string extension = ".json";
        
        public JsonHelper(string fileName)
        {
            this.fileName = fileName;
        }

        public ObservableCollection<Person> ReadFile()
        {
            ObservableCollection<Person> col = new ObservableCollection<Person>();
            try
            {
                string fileString = File.ReadAllText(fileName + extension);
                var result = JsonConvert.DeserializeObject<List<Person>>(fileString);

                foreach (Person data in result)
                {
                    col.Add(data);
                }

                return col;
            }
            catch
            {

            }
            return col;
        }

        public bool WriteFile(ObservableCollection<Person> col)
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
