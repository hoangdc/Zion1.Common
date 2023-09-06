using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zion1.Common.API.Consumer.Logger
{
    public class ErrorLogger : IErrorLogger
    {
        public void LogError(Exception ex, string infoMessage)
        {
            //Log the error to your error database
        }
    }
}
