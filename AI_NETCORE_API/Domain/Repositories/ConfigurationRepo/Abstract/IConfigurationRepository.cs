using Data.Models;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories.ConfigurationRepo.Abstract
{
    public interface IConfigurationRepository : IRepositoryBase<Configuration>
    {
        RepositoryResponse<BusinessObject.Configuration> GetConfiguration(string name);
        RepositoryResponse<bool> UpdateConfiguration(string name, int value);
    }
}
