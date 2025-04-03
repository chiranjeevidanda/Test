using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Outbound.Interface;
using NEC.Fulf3PL.Core.Common;
using Microsoft.Extensions.Configuration;
using NEC.Fulf3PL.Core.Interface;
using System.Net;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.COMMON
{
    public class LineMaterialService : ILineMaterialService<string>
    {
        private readonly ILogger<LineMaterialService> logger;
        private readonly IRequestHandler requestHandler;

        public LineMaterialService(ILogger<LineMaterialService> logger, IRequestHandler requestHandler)
        {
            this.logger = logger;
            this.requestHandler = requestHandler;
        }

        public async Task<ExecuteResult<bool>> PostData(string requestData, string requestURL)
        {
            logger.LogInformation("Posting Line Material Service Data.....");

            var result = new ExecuteResult<bool>();
            var providerResponse = new ExecuteResult<string>();

            var externalRequest = new ExternalRequest();
            externalRequest.ContentType = "application/json";
            externalRequest.URL = requestURL;
            externalRequest.Body = requestData;

            try
            {
                providerResponse = await requestHandler.PostAsync(externalRequest);
            }
            catch (Exception ex)
            {
                providerResponse.Success = false;

                throw;
            }
            if (providerResponse.Success && providerResponse.Result != null)
            {
                result.Success = providerResponse.Success;
                result.Result = providerResponse.Success;

            }
            else if (!providerResponse.Success)
            {
                var httpStatusCode = providerResponse.Messages.FirstOrDefault().HttpCode;
                result.Success = providerResponse.Success;
                result.Result = providerResponse.Success;

                if (httpStatusCode == HttpStatusCode.BadRequest)
                {
                    result.Success = providerResponse.Success;
                }
                else if (httpStatusCode == HttpStatusCode.Gone)
                {
                    throw new InternalServierException(Constants.ErrorWebhookGone);
                }
                else if (httpStatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new InternalServierException(httpStatusCode.ToString());
                }
                else
                {
                    throw new Exception(httpStatusCode.ToString());
                }
            }
            else
            {
                result.Success = false;
                result.Result = false;

                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
            }

            return result;
        }
    }
}
