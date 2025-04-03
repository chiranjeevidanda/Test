using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Core;
using System.Text;

namespace NEC.Fulf3PL.Infrastructure
{
    public class BlobStorageService : IStorageService
    {
        private readonly string connectionKey;
        private readonly ILogger<BlobStorageService> logger;
        public BlobStorageService(string connectionKey, ILogger<BlobStorageService> logger)
        {
            this.connectionKey = connectionKey;
            this.logger = logger;
        }


        public async Task<bool> DeleteAsync(string blobName, string containerName)
        {
            try
            {
                BlobClient blobClient = await GetBlobClient(blobName, containerName);

                if (blobClient != null)
                {
                    await blobClient.DeleteIfExistsAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while processing the request.");
                throw;
            }
        }


        /// <returns></returns>
        public async Task<string> UploadAsync(string blobName, string jsonData, string containerName)
        {
            try
            {
                BlobClient blobClient = await GetBlobClient(blobName, containerName);

                if (blobClient != null)
                {
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonData)))
                        {
                            await blobClient.UploadAsync(ms, overwrite: true);
                        }

                        return blobClient.Uri.AbsoluteUri;
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while processing the request.");
                throw;
            }
        }

        /// <summary>
        /// Method to fetch content from azure blob
        /// </summary>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public async Task<string> DownloadAsync(string blobName, string containerName)
        {
            try
            {
                BlobClient blobClient = await GetBlobClient(blobName, containerName);

                if (blobClient != null)
                {
                    Response<BlobDownloadResult> result = await blobClient.DownloadContentAsync();
                    return result.Value.Content.ToString();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while processing the request.");
                throw;
            }
        }

        // create funciton for randomnumber

        private int CalculateDatebetweenTwoday(DateTime startDate, DateTime endDate)
        {

            return (endDate - startDate).Days;
        }

        #region Private Methods

        /// <summary>
        /// Method to create blob client with the given blob name
        /// </summary>
        /// <param name="blobName"></param>
        /// <returns></returns>
        private async Task<BlobClient> GetBlobClient(string blobName, string containerName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionKey);

            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName.ToLowerInvariant());
            await blobContainerClient.CreateIfNotExistsAsync();

            return blobContainerClient.GetBlobClient(blobName);
        }

        #endregion
    }
}
