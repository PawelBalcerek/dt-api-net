using Domain.Providers.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Common.Abstract
{
    public interface IProvideResult
    {
        ProvideEnumResult ProvideResult { get; }
    }
}
