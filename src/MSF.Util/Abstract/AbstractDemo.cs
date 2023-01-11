using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Abstract
{
    public abstract class AbstractDemo
    {
        public virtual string DownloadData(string sql)
        {
            Console.WriteLine("Fazendo o download do arquivo...");
            return "98%";
        }

        public abstract void LoadData(string sql);

        public abstract void SaveData(string sql);
    }
}
