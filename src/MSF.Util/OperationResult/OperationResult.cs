using MSF.Util.OperationResult;
using System;
using System.Collections.Generic;

namespace APIShell.Services.Utils
{
    [Serializable()]
    public class OperationResult
    {
        private Exception mainException;
        private readonly string _errorMessage;
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        private readonly DateTime? _resultTime = null;
        public int AffectedRows { get; set; }
        public object AuxiliarObject { get; set; }
        public SituationEnum Situation { get; set; } = SituationEnum.Success;

        public Exception MainException
        {
            get
            {
                return mainException;
            }
            set
            {
                mainException = value;
                if (value != null)
                {
                    this.ErrorMessage = value.Message;
                    this.StackTrace = value.StackTrace;
                }
            }
        }

        public bool Success
        {
            get { return Situation.Equals(SituationEnum.Success); }
            set { Situation = value ? SituationEnum.Success : SituationEnum.Error; }
        }


        public OperationResult()
        {
            _resultTime = DateTime.Now;
        }
    }

    [Serializable()]
    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }
        public List<T> DataList { get; set; }
    }

    [Serializable()]
    public class OperationResult<T, U> : OperationResult<T>
    {
        public U Data2 { get; set; }
        public List<U> DataList2 { get; set; }
    }

    [Serializable()]
    public class OperationResult<T, U, V> : OperationResult<T, U>
    {
        public V Data3 { get; set; }
        public List<V> DataList3 { get; set; }
    }

    [Serializable()]
    public class OperationResult<T, U, V, W> : OperationResult<T, U, V>
    {
        public W Data4 { get; set; }
        public List<W> DataList4 { get; set; }
    }

    [Serializable()]
    public class OperationResult<T, U, V, W, X> : OperationResult<T, U, V, W>
    {
        public X Data5 { get; set; }
        public List<X> DataList5 { get; set; }
    }

    [Serializable()]
    public class OperationResult<T, U, V, W, X, Z> : OperationResult<T, U, V, W, X>
    {
        public Z Data6 { get; set; }
        public List<Z> DataList6 { get; set; }
    }
}
