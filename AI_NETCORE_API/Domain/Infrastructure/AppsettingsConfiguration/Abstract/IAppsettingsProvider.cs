using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.AppsettingsConfiguration.Abstract
{
    public interface IAppsettingsProvider
    {
        string GetPasswordRegex();
    }
}
