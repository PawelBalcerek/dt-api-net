using Data.Models;
using Domain.DTOToBOConverting;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.BaseRepo.Response;
using Domain.Repositories.ConfigurationRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Domain.Repositories.ConfigurationRepo.Concrete
{
    public class ConfigurationRepository : RepositoryBase<Configuration>, IConfigurationRepository
    {
        private readonly IDTOToBOConverter _converter;
        public ConfigurationRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter)
            : base(repositoryContext)
        {
            _converter = converter;
        }

    public RepositoryResponse<bool> UpdateConfiguration(string name, int value)
        {
            bool success;
            Stopwatch timer = Stopwatch.StartNew();

            Configuration configuration = RepositoryContext.Configurations.Find(name);
            if (configuration == null)
            {
                timer.Stop();
                success = false;
            }
            else
            {
                configuration.Value = value;
                RepositoryContext.Update(configuration);
                RepositoryContext.SaveChanges();
                timer.Stop();
                success = true;
            }

            long time = timer.ElapsedMilliseconds;
            return new RepositoryResponse<bool>(success, time);
        }
    }
}
