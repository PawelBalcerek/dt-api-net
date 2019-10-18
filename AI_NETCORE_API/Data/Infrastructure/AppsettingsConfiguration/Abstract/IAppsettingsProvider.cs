using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure.AppsettingsConfiguration.Abstract
{
    public interface IAppsettingsProvider
    {
        string GetPasswordRegex();
    }
}
