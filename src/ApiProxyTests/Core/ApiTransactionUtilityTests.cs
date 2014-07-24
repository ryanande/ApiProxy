using EdFiValidation.ApiProxy.Core.Utility;
using NUnit.Framework;
using System;
using Rhino.Mocks;

namespace EdFiValidation.ApiProxyTests.Core
{
    [TestFixture]
    public class ApiTransactionUtilityTests : TestBase
    {
        private const string EncodedUrl = "aHR0cHM6Ly93d3cuZ29vZ2xlLmNvbS8=";

        [Test]
        public void GetDestination_Properly_Decodes_String()
        {
            // arrange
            var expected = new Uri("https://www.google.com/");

            var config = Stub<IConfig>();

            var pathInspector = new ApiTransactionUtility(config);
            
            // act
            var actual = pathInspector.DecodeDestination(EncodedUrl);
            
            // assert
            Assert.IsTrue(expected.ToString() == actual);
        }
        

        [Test]
        public void ExtractBody_Returns_Empty_String_With_Null_HttpContext() // lame?
        {
            // arrange
            var expected = string.Empty;
            var config = Stub<IConfig>();
            var pathInspector = new ApiTransactionUtility(config);

            // act
            var actual = pathInspector.ExtractBody(null);

            //assert
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void ExtractSessionId_Properly_Returns_UrlSegment_Index_From_Config_Value()
        {
            //arrange
            const string expected = "index1";
            var uri = new Uri("http://www.teste.com/index0/index1/index2/index3");

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(2);
            var pathInspector = new ApiTransactionUtility(config);


            // act
            var actual = pathInspector.ExtractSessionId(uri);

            // assert
            Assert.AreEqual(expected, actual);

        }


        [Test]
        public void ExtractSessionId_Properly_Returns_Empty_String_When_Index_Greater_Than_Segment_Count()
        {
            //arrange

            var uri = new Uri("http://www.teste.com/index0/index1/index2/index3");

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(5);
            var pathInspector = new ApiTransactionUtility(config);


            // act
            var actual = pathInspector.ExtractSessionId(uri);

            // assert
            Assert.AreEqual(string.Empty, actual);

        }

        [Test]
        public void ExtractSessionId_Properly_Returns_Empty_String_When_Index_Less_Than_Segment_Zero()
        {
            //arrange

            var uri = new Uri("http://www.teste.com/index0/index1/index2/index3");

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(-1);
            var pathInspector = new ApiTransactionUtility(config);


            // act
            var actual = pathInspector.ExtractSessionId(uri);

            // assert
            Assert.AreEqual(string.Empty, actual);

        }
    }
}
