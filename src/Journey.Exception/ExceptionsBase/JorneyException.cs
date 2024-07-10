using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionsBase
{
    public class JorneyException : SystemException
    {
        public JorneyException(string message) : base(message) 
        {
            
        }
    }
}
