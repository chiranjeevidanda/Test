
namespace NEC.Fulf3PL.Core.Common
{
    public class InternalServierException : Exception
    {
        public InternalServierException() { }

        public InternalServierException(string errorcode)
            : base(String.Format("Internal server error with code: {0}", errorcode))
        {

        }
    }
}
