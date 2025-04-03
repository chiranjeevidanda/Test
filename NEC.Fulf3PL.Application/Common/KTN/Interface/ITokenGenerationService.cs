using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Common.KTN.Interface
{
    public interface ITokenGenerationService<T>
    {
        Task<ExecuteResult<T>> GenerateToken();

    }
}
