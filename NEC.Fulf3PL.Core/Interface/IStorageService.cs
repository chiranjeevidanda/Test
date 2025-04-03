namespace NEC.Fulf3PL.Core
{
    public interface IStorageService
    {
        Task<string> DownloadAsync(string blobName, string containerName);
        Task<string> UploadAsync(string blobName, string jsonData, string containerName);
        Task<bool> DeleteAsync(string blobName, string containerName);
    }
}
