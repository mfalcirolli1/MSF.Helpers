using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Abstract
{
    public class OverrideDemo : AbstractDemo
    {
        public override string DownloadData(string sql)
        {
            string output = base.DownloadData(sql);
            output += "(from SQL Lite)";
            return output;
        }

        public override void LoadData(string sql)
        {
            Console.WriteLine("Carregando o arquivo...");
        }

        public override void SaveData(string sql)
        {
            Console.WriteLine("Arquivo Salvo!");
        }
    }
}
