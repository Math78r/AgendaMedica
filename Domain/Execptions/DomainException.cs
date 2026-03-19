using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Execptions
{
    public  class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
