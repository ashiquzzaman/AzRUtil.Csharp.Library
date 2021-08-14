using System;
using System.Collections.Generic;

namespace AzRUtil.Csharp.Library.Abstraction
{

    public interface IUserRequest
    {
        string GetUserIp { get; }
        string GetUserOS { get; }
        string GetUserAgentInfo { get; }
        List<Tuple<string, string>> GetAgent(string agentId);
    }
}
