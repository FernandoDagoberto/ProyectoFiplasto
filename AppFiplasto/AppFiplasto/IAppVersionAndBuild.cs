using System;
using System.Collections.Generic;
using System.Text;

namespace AppFiplasto
{
    public interface IAppVersionAndBuild
    {
        string GetVersionNumber();
        string GetBuildNumber();
    }
}
