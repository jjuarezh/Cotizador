using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ResponseService<T>
    {
        public bool Error { get; set; }
        private int _errorCode;
        public string Message { get; set; }
        public IEnumerable<T> ResulList { get; set; }
        public T ResultObject { get; set; }

        public int ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }

        public ResponseService()
        {
            this.Error = false;
            this.Message = string.Empty;
            this.ResulList = null;
        }
    }
}
