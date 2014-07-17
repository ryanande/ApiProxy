using Rhino.Mocks;

namespace EdFiValidation.ApiProxyTests
{
    public class TestBase
    {
        /// <summary>
        /// Create a mock
        /// </summary>
        /// <typeparam name="T">Type to be mocked</typeparam>
        /// <param name="argumentsForConstructor">Constructor arguments</param>
        /// <returns>TMessage</returns>
        public static T Mock<T>(params object[] argumentsForConstructor) where T : class
        {
            return MockRepository.GenerateMock<T>(argumentsForConstructor);
        }

        /// <summary>
        /// Create a stub
        /// </summary>
        /// <typeparam name="T">Type to be stubbed</typeparam>
        /// <param name="argumentsForConstructor">Constructor arguments</param>
        /// <returns>TMessage</returns>
        public static T Stub<T>(params object[] argumentsForConstructor) where T : class
        {
            return MockRepository.GenerateStub<T>(argumentsForConstructor);
        }
    }
}
