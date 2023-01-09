using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Generics
{
    public class OperationModel<T>
    {
        public bool Sucess { get; set; } = false;
        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public string ErrorMessage { get; set; }
    }
}
