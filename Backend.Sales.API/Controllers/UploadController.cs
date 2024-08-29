using Backend.Sales.Application.Interface;
using Backend.Sales.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
public class UploadController : ControllerBase
{
    private readonly IUploadService _uploadService;

    public UploadController(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }
    /// <summary>
    /// Uploads a CSV file and processes it based on the specified entity type.
    /// </summary>
    /// <param name="file">The CSV file to upload.</param>
    /// <param name="fileType">The type of entity represented by the CSV file. 
    /// Possible values:
    /// 1 - Pizza,
    /// 2 - PizzaType,
    /// 3 - OrderDetail,
    /// 4 - Order.</param>
    /// <returns>Returns a response indicating the success or failure of the operation.</returns>
    [HttpPost("upload/{fileType}")]
    [SwaggerOperation(Summary = "Upload and process CSV file",
                      Description = "Uploads a CSV file and processes it based on the specified entity type. Possible values for `fileType` are: 1 for Pizza, 2 for Order, 3 for OrderDetail, and 4 for PizzaType.")]
    [SwaggerResponse(200, "File uploaded and data inserted successfully.")]
    [SwaggerResponse(400, "No file uploaded.")]
    public async Task<IActionResult> UploadCsv(IFormFile file, EntityType fileType)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using (var stream = file.OpenReadStream())
        {
            await _uploadService.ProcessCsvFileAsync(stream, fileType);
        }

        return Ok("File uploaded and data inserted successfully.");
    }
}