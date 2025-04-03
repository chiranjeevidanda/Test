
namespace NEC.Fulf3PL.Core.Common
{
    using System.Collections.Generic;

    public class ExecuteResult<T>
    {
        public ExecuteResult()
        {
            Success = false;
            Messages = new List<ExecuteMessage>();
        }

        public T Result { get; set; }

        public IEnumerable<T> Results { get; set; }

        public bool Success { get; set; }

        public ICollection<ExecuteMessage> Messages { get; set; }

    }

    public class ExecuteMessage
    {
        public Enums.StatusCode Code { get; set; }

        public string? Description { get; set; }

        public System.Net.HttpStatusCode HttpCode { get; set; }
    }
}
