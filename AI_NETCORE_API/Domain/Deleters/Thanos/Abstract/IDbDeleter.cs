using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Deleters.Thanos.Abstract
{
    public interface IDbDeleter
    {
        long Clear();
        long Purge();
    }
}
