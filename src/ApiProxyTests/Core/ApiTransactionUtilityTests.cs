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

        [Test]
        public void BuildDestinationUri_Builds_Correct_Destination_Uri()
        {
            //arrange
            var incomingUri_trailingSlash = new Uri(
                "http://pseudohost.com:567/sessionId/aHR0cHM6Ly90bi1yZXN0LXByb2R1Y3Rpb24uY2xvdWRhcHAubmV0OjQ0My9hcGkvdjEuMC8yMDE0Lw==/students/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            //encoded part = https://tn-rest-production.cloudapp.net:443/api/v1.0/2014/
            var incomingUri_noTrailingSlash = new Uri(
                "http://pseudohost.com:567/sessionId/aHR0cHM6Ly90bi1yZXN0LXByb2R1Y3Rpb24uY2xvdWRhcHAubmV0OjQ0My9hcGkvdjEuMC8yMDE0Lw==/students/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            //encoded part = https://tn-rest-production.cloudapp.net:443/api/v1.0/2014  
            var incomingUri_crazySlashes = new Uri(
                "http://pseudohost.com:567/sessionId/aHR0cHM6Ly90bi1yZXN0LXByb2R1Y3Rpb24uY2xvdWRhcHAubmV0OjQ0My8vYXBpLy8vdjEuMC8yMDE0Ly8vLw==/students/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            //encoded part = https://tn-rest-production.cloudapp.net:443//api///v1.0/2014////
           
            var expected = new Uri("https://tn-rest-production.cloudapp.net:443/api/v1.0/2014/students/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(1);
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(2);
            var pathInspector = new ApiTransactionUtility(config);

            // act
            var actual_trailingSlash = pathInspector.BuildDestinationUri(incomingUri_trailingSlash);
            var actual_noTrailingSlash = pathInspector.BuildDestinationUri(incomingUri_noTrailingSlash);
            var actual_crazySlashes = pathInspector.BuildDestinationUri(incomingUri_crazySlashes);

            // assert
            Assert.AreEqual(expected, actual_trailingSlash);
            Assert.AreEqual(expected, actual_noTrailingSlash);
            Assert.AreEqual(expected, actual_crazySlashes);
        }

        [Test]
        public void BuildDestinationUri_Throws_When_Decoded_Destination_Uri_Not_Valid()
        {
            //arrange
            var incomingUri = new Uri("http://pseudohost.com:567/sessionId/dG90YWxseSFub3ReYSxVcmk=/destIndex0/destIndex1?query=this");
            //dG90YWxseSFub3ReYSxVcmk= decodes to "totally!not^a,Uri"
            var incomingUri1 = new Uri("http://pseudohost.com:567/sessionId/YWxtb3N0QS9Vcmk=/destIndex0/destIndex1?query=this");
            //YWxtb3N0QS9Vcmk= decodes to "almostA/Uri

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(1);
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(2);
            var pathInspector = new ApiTransactionUtility(config);

            // act and assert
            Assert.Throws<CannotParseUriException>(() => pathInspector.BuildDestinationUri(incomingUri));
            Assert.Throws<CannotParseUriException>(() => pathInspector.BuildDestinationUri(incomingUri1));
        }
    }
}
