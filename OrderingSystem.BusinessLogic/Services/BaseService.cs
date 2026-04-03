using Microsoft.Extensions.Logging;
using OrderingSystem.Global.Common;

namespace OrderingSystem.BusinessLogic.Services
{
    public abstract class BaseService
    {
        protected readonly ILogger _logger;

        protected BaseService(ILogger logger)
        {
            _logger = logger;
        }

        // Try-Catch wrapper for all service methods
        protected async Task<Result<T>> ExecuteSafeAsync<T>(Func<Task<Result<T>>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during service method execution.");

                return Result<T>.Failure("An unexpected error occured. Try again later.");
            }
        }
    }
}
