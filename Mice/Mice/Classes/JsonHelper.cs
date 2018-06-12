using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.Classes
{
    class JsonHelper
    {
        private string fileName;
        private string extension = ".json";

        public JsonHelper(string fileName)
        {
            this.fileName = fileName;
        }

        public ObservableCollection<MouseSettings> ReadFile()
        {
            try
            {
                ObservableCollection<MouseSettings> col = new ObservableCollection<MouseSettings>();
                string fileString = File.ReadAllText(fileName + extension);
                var result = JsonConvert.DeserializeObject<List<MouseSettings>>(fileString);

                foreach (MouseSettings item in result)
                {
                    col.Add(item);
                }

                return col;
            }
            catch
            {

            }
            return null;
        }

        public bool WriteFile(ObservableCollection<MouseSettings> list)
        {
            try
            {
                string fileString = JsonConvert.SerializeObject(list);
                File.WriteAllText(fileName + extension, fileString);

                return true;
            }
            catch
            {

            }

            return false;
        }
    }
}
