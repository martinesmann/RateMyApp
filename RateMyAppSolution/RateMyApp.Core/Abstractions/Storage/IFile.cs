using System.Threading.Tasks;

namespace RateMyApp.Core.Abstractions.Storage
{
    public interface IFile
    {
        //void Initialize(IFile Instance);
        Task<string> WriteTextFile(string filename, string contents);
        Task<string> ReadTextFile(string filename);
    }
}
