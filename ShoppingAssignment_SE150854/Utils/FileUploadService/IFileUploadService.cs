using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ShoppingAssignment_SE150854.Utils.FileUploadService
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
