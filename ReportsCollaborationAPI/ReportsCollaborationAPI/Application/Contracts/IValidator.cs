using System;

namespace ReportsCollaborationAPI.Application
{
    public interface IValidator
    {
        Tuple<bool, string> Validate();
    }
}
