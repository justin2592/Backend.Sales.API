using Backend.Sales.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Sales.Application.Interface
{
    public interface IUploadService
    {
        Task ProcessCsvFileAsync(Stream csvStream, EntityType entityType);
    }
}
