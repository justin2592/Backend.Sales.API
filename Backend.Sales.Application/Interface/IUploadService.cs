using Backend.Sales.Application.Models;
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
        Task<UploadResponse> ProcessCsvFileAsync(Stream csvStream, EntityType entityType, int chunkSize = 1000);
    }
}
