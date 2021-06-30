using Xunit;

namespace CheckoutSystemEngine.Tests
{
    public class ExecutionResultTests
    {
        [Fact]
        public void CreateSuccess_Works()
        {
            ExecutionResult<int> result = ExecutionResult<int>.CreateSuccess(123);

            Assert.Equal(123, result.Result);
            Assert.True(result.IsSuccess);
            Assert.Equal(string.Empty, result.ErrorMessage);
        }

        [Fact]
        public void CreateFailure_Works()
        {
            ExecutionResult<int> result = ExecutionResult<int>.CreateFailure("Some error");

            Assert.Equal(default, result.Result);
            Assert.False(result.IsSuccess);
            Assert.Equal("Some error", result.ErrorMessage);
        }
    }
}
