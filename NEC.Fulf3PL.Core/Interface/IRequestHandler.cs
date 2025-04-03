using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Core.Interface
{
    public interface IRequestHandler
    {
        Task<ExecuteResult<string>> PostAsync(ExternalRequest requestData);

        Task<ExecuteResult<string>> PutAsync(ExternalRequest requestData);

        Task<ExecuteResult<string>> GetAsync(ExternalRequest requestData);
        Task<ExecuteResult<string>> PatchAsync(ExternalRequest requestData);
    }
}
