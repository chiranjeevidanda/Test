using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Framework.Host.Extension
{
    public interface IRateLimitEntity
    {
        /// <summary>
        /// Checks if the call is allowed based on the provided activity parameters.
        /// </summary>
        /// <param name="activityParams">The activity parameters consisting of a string key, int limit, int periodInSeconds.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a boolean indicating whether the call is allowed.</returns>
        Task<bool> IsCallAllowed((string, int, int) activityParams);

        /// <summary>
        /// Gets the current rate limit value.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is an integer representing the current rate limit value.</returns>
        Task<int> Get(string key);
    }
}
