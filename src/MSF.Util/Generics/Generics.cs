using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSF.Util.Generics
{
    public class Generics
    {
        public OperationModel<string> GenerateDemo(string demo, List<string> listDemo)
        {
            var retorno = new OperationModel<string>();

            try
            {
                retorno.Sucess = true;
                retorno.Data = demo;
                retorno.DataList = listDemo;
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucess = false;
                retorno.ErrorMessage = ex.Message;
                return retorno;
            }
        }

        public OperationModel<T> ReadFile<T>(string filePath) where T : class, new()
        {
            var retorno = new OperationModel<T>() { DataList = new List<T>() };

            try
            {
                T obj = new T();

                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                var columns = obj.GetType().GetProperties();

                if (lines.Count < 2)
                {
                    retorno.Sucess = false;
                    retorno.ErrorMessage = "Arquivo está vazio. Verifique";
                    return retorno;
                }

                var headers = lines[0].Split(",");
                lines.RemoveAt(0);

                foreach (var row in lines)
                {
                    obj = new T();
                    var values = row.Split(",");

                    var counter = 0;

                    foreach (var column in columns)
                    {
                        if (column.Name == headers[counter].Trim())
                        {
                            column.SetValue(obj, Convert.ChangeType(values[counter], column.PropertyType));
                            counter++;
                        }
                    }

                    retorno.DataList.Add(obj);
                }

                retorno.Sucess = true;
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.ErrorMessage = ex.Message;
                return retorno;
            }
        }
    }
}
