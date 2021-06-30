using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutSystemEngine
{
    public class ExecutionResult<TValue>
    {
        public string ErrorMessage { get; private set; }
        public TValue? Result { get; private set; }
        public bool IsSuccess { get; private set; }

        private ExecutionResult(string errorMessage, TValue? result, bool isSuccess)
        {
            ErrorMessage = errorMessage;
            Result = result;
            IsSuccess = isSuccess;
        }

        public static ExecutionResult<TValue> CreateSuccess(TValue value)
            => new(string.Empty, value, true);

        public static ExecutionResult<TValue> CreateFailure(string errorMessage)
            => new(errorMessage, default, false);
    }
}
