using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zion1.Common.API.Consumer.Logger
{
    public interface IErrorLogger
    {
        void LogError(Exception ex, string infoMessage);
    }
}
