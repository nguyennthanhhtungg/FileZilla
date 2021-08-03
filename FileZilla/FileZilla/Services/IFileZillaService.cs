

using System.Threading.Tasks;

namespace FileZilla.Services
{
    public interface IFileZillaService
    {
        Task UploadFile(string fileData, string fileName);
    }
}
