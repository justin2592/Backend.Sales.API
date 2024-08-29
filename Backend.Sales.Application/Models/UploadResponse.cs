using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Sales.Application.Models
{
    public class UploadResponse
    {
        public int TotalUploaded { get; set; }
        public int TotalSuccess { get; set; }
        public int TotalFailed { get; set; }

    }
}
