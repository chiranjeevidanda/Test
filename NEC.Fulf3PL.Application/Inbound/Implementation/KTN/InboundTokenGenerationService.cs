using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Core.Common;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using NEC.Fulf3PL.Core.Interface;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Common;
using NEC.Fulf3PL.Core.Entities;
using static NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Common.DeliveryProcessMapper;
using NEC.Fulf3PL.Application.Common.KTN.Interface;
using NEC.Fulf3PL.Application.Common.KTN.DTO;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN
{
    public class InboundTokenGenerationService : ITokenGenerationService<GenerateTokenDTO>
    {
        private readonly ILogger<InboundTokenGenerationService> logger;
        private readonly IConfiguration configuration;
        private readonly IRequestHandler requestHandler;
        private readonly IInboundTransactionRepository inboundTransactionRepository;
        public InboundTokenGenerationService(ILogger<InboundTokenGenerationService> logger, IConfiguration configuration, IRequestHandler requestHandler, IInboundTransactionRepository inboundTransactionRepository)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.requestHandler = requestHandler;
            this.inboundTransactionRepository = inboundTransactionRepository;
        }

        public async Task<ExecuteResult<GenerateTokenDTO>> GenerateToken()
        {
            var result = new ExecuteResult<GenerateTokenDTO>();
            var generatedTokens = new List<GenerateTokenDTO>();

            logger.LogInformation("Generate Token.....");
            string tokenKey = configuration["AuthKey1Name"];
            var tokenResponse1 = await GetToken(tokenKey);
            if (tokenResponse1.Success && tokenResponse1.Result != null)
            {
                generatedTokens.Add(tokenResponse1.Result);
            }
            result.Results = generatedTokens;

            return result;
        }

        private async Task<ExecuteResult<GenerateTokenDTO>> GetToken(string key)
        {
            var response = new ExecuteResult<string>();

            var result = new ExecuteResult<GenerateTokenDTO>();
            var externalRequest = new ExternalRequest();
            var queryParamsAll = DeliveryProcessHelper.LoadQueryParams();
            var queryParams = new List<(string name, string value)>();

            var formData = new Dictionary<string, string>();

            if (queryParamsAll.TryGetValue(key, out queryParams))
            {
                foreach (var item in queryParams)
                {
                    formData.Add(item.name, item.value);
                }

                externalRequest.FormData = formData;
                externalRequest.ContentType = "application/x-www-form-urlencoded";
                externalRequest.URL = $"{configuration["KTNTokenURL"]}";

                //log process

                response = await requestHandler.PostAsync(externalRequest);
                if (response.Success)
                {
                    result.Success = response.Success;
                    result.Result = JsonConvert.DeserializeObject<GenerateTokenDTO>(response.Result);
                }
                else
                {
                    var httpStatusCode = response.Messages.FirstOrDefault().HttpCode;

                    //log process
                    await LogPostProcess(Guid.NewGuid().ToString(), "TokenGeneration", externalRequest, response, "Failed while generating KTN token key");

                    if (httpStatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        result.Success = response.Success;
                    }
                    else if (httpStatusCode == System.Net.HttpStatusCode.Gone)
                    {
                        throw new InternalServierException(Constants.ErrorWebhookGone);
                    }
                    else if (httpStatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        throw new InternalServierException(httpStatusCode.ToString());
                    }
                    else
                    {
                        throw new Exception(httpStatusCode.ToString());
                    }
                }
            }
            else
            {
                logger.LogError($"Vendor/partner id {key} not found in the preedefined mappings.");
                response.Success = false;
                response.Result = Constants.ErrorNoBearerTokenInvalidKey;
                await LogPostProcess(Guid.NewGuid().ToString(), "TokenGeneration", externalRequest, response, Constants.ErrorNoBearerTokenInvalidKey);

                result.Messages = new List<ExecuteMessage>()
                {
                    new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = $"Vendor/partner id {key} not found in the preedefined mappings." },
                };
                return result;
            }

            return result;
        }

        private async Task LogPostProcess(string processId, string requestType, ExternalRequest externalRequest, ExecuteResult<string> response, string comments)
        {
            InboundTransactionLogModel inboundTransaction = MapInboundLogDetails(processId, requestType, externalRequest, response, comments);
            await inboundTransactionRepository.CreateAsync(inboundTransaction);
        }

    }
}
