using System.Configuration;
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
        private const string DecodedUrl = "https://www.google.com/";

        [Test]
        public void GetDestination_Properly_Decodes_String()
        {
            // arrange
            const string expected = DecodedUrl;

            var config = Stub<IConfig>();

            var pathInspector = new ApiTransactionUtility(config);
            
            // act
            var actual = pathInspector.DecodeDestination(EncodedUrl);
            
            // assert
            Assert.IsTrue(expected == actual);
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
        public void ExtractSessionId_Properly_Throws_When_Index_Less_Than_Segment_Zero()
        {
            //arrange
            var uri = new Uri("http://www.teste.com/index0/index1/index2/index3");
            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(-1);
            var pathInspector = new ApiTransactionUtility(config);

            // act and assert
            Assert.Throws<ConfigurationErrorsException>(() => pathInspector.ExtractSessionId(uri));
        }

        [Test]
        public void ExtractDestination_Properly_Returns_UrlSegment_Index_From_Config_Value()
        {
            //arrange
            const string expected = DecodedUrl;
            var encodedUri = new Uri(string.Format("http://www.teste.com/index0/{0}/index2/index3", EncodedUrl));

            var config = Stub<IConfig>();
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(2);
            var pathInspector = new ApiTransactionUtility(config);

            // act
            var actual = pathInspector.ExtractDestination(encodedUri);

            // assert
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void ExtractDestination_Properly_Throws_When_Index_Greater_Than_Segment_Count()
        {
            //arrange
            var uri = new Uri("http://www.teste.com/index0/index1/index2/index3");

            var config = Stub<IConfig>();
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(5);
            var pathInspector = new ApiTransactionUtility(config);

            // act and assert
            Assert.Throws<CannotParseUriException>(() => pathInspector.ExtractDestination(uri));
        }

        [Test]
        public void ExtractDestination_Properly_Throws_When_Index_Less_Than_Segment_Zero()
        {
            //arrange
            var uri = new Uri("http://www.teste.com/index0/index1/index2/index3");

            var config = Stub<IConfig>();
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(-1);
            var pathInspector = new ApiTransactionUtility(config);

            // act and assert
            Assert.Throws<ConfigurationErrorsException>(() => pathInspector.ExtractDestination(uri));
        }
        /////////////////////////////////////////////////////////

        [Test]
        public void BuildDestinationUri_Builds_Correct_Destination_Uri()
        {
            //arrange
            var expected =new Uri("http://www.unitTestsRule.com/destIndex0/destIndex1?query=this");
            var incomingUri = new Uri("http://pseudohost.com:567/sessionId/aHR0cDovL3d3dy51bml0VGVzdHNSdWxlLmNvbQ==/destIndex0/destIndex1?query=this");

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(1);
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(2);
            var pathInspector = new ApiTransactionUtility(config);

            // act
            var actual = pathInspector.BuildDestinationUri(incomingUri);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BuildDestinationUri_Throws_When_Decoded_Destination_Uri_Not_Valid()
        {
            //arrange
            var incomingUri = new Uri("http://pseudohost.com:567/sessionId/dG90YWxseSFub3ReYSxVcmk=/destIndex0/destIndex1?query=this");
            //dG90YWxseSFub3ReYSxVcmk= decodes to "totally!not^a,Uri"

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(1);
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(2);
            var pathInspector = new ApiTransactionUtility(config);

            // act and assert
            Assert.Throws<CannotParseUriException>(() => pathInspector.BuildDestinationUri(incomingUri));
        }
    }
}
