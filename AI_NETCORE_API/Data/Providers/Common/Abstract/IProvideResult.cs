using Data.Providers.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Common.Abstract
{
    public interface IProvideResult
    {
        ProvideEnumResult ProvideResult { get; }
    }
}
