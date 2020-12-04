using Catel;
using Catel.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatelMvvmDI.Models
{
    public class Service : ModelBase
    {
        private readonly ILogger<Service> _logger;

        public Service()
        {
        }

        public Service(ILogger<Service> logger)
        {
            Argument.IsNotNull(() => logger);
            _logger = logger;
        }

        public string GenerateGuidAsString()
        {
            if(_logger != null)
                _logger.LogInformation("Generating a guid.");

            return Guid.NewGuid().ToString();
        }
    }
}
