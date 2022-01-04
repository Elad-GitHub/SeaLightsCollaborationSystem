using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsCollaborationAPI.Utilities.Contracts
{
    public interface IValidator
    {
        Tuple<bool, string> Validate();
    }
}
