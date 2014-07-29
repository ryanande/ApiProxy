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
        public void ExtractDestination_Properly_Returns_Decoded_UrlSegment_From_Config_Value_Index()
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
        public void BuildDestinationUri_Builds_Correct_Destination_Uri_MidSlashes()
        {
            //arrange
            var incomingUriTrailingSlash = new Uri(
                "http://pseudohost.com:567/sessionId/aHR0cHM6Ly90bi1yZXN0LXByb2R1Y3Rpb24uY2xvdWRhcHAubmV0OjQ0My9hcGkvdjEuMC8yMDE0Lw==/students/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            //encoded part = https://tn-rest-production.cloudapp.net:443/api/v1.0/2014/
            var incomingUriNoTrailingSlash = new Uri(
                "http://pseudohost.com:567/sessionId/aHR0cHM6Ly90bi1yZXN0LXByb2R1Y3Rpb24uY2xvdWRhcHAubmV0OjQ0My9hcGkvdjEuMC8yMDE0/students/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            //encoded part = https://tn-rest-production.cloudapp.net:443/api/v1.0/2014  <--- note the difference in last char. A key breaking point! 
            var expected = new Uri("https://tn-rest-production.cloudapp.net:443/api/v1.0/2014/students/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            //the ed-fi validation API does not care if there are extra slashes between each Uri segment. 
            //So long as we ensure there is at least 1 slash between each segment, their API can handle it properly (as of July 2014)

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(1);
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(2);
            var pathInspector = new ApiTransactionUtility(config);

            // act
            var actualTrailingSlash = pathInspector.BuildDestinationUri(incomingUriTrailingSlash);
            var actualNoTrailingSlash = pathInspector.BuildDestinationUri(incomingUriNoTrailingSlash);
            
            // assert
            Assert.AreEqual(expected, actualTrailingSlash);
            Assert.AreEqual(expected, actualNoTrailingSlash);
        }

        [Test]
        public void BuildDestinationUri_Builds_Correct_Destination_Uri_Query()
        {
            //arrange
            var incomingUriQuery = new Uri(
                "http://pseudohost.com:567/sessionId/aHR0cHM6Ly90bi1yZXN0LXByb2R1Y3Rpb24uY2xvdWRhcHAubmV0OjQ0My9hcGkvdjEuMC8yMDE0Lw==/students?id=aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var incomingUriSlashB4Query = new Uri(
                "http://pseudohost.com:567/sessionId/aHR0cHM6Ly90bi1yZXN0LXByb2R1Y3Rpb24uY2xvdWRhcHAubmV0OjQ0My9hcGkvdjEuMC8yMDE0Lw==/students/?id=aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var incomingUriMultiQuery = new Uri(
                "http://pseudohost.com:567/sessionId/aHR0cHM6Ly90bi1yZXN0LXByb2R1Y3Rpb24uY2xvdWRhcHAubmV0OjQ0My9hcGkvdjEuMC8yMDE0Lw==/students??id=aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            //encoded part for all of the above = https://tn-rest-production.cloudapp.net:443/api/v1.0/2014/
            var expected = new Uri("https://tn-rest-production.cloudapp.net:443/api/v1.0/2014/students?id=aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(1);
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(2);
            var pathInspector = new ApiTransactionUtility(config);

            // act
            var actualQuery = pathInspector.BuildDestinationUri(incomingUriQuery);
            var actualSlashB4Query = pathInspector.BuildDestinationUri(incomingUriSlashB4Query);
            var actualMultiQuery = pathInspector.BuildDestinationUri(incomingUriMultiQuery);

            // assert
            Assert.AreEqual(expected, actualSlashB4Query);
            Assert.AreEqual(expected, actualQuery);
            Assert.AreEqual(expected, actualMultiQuery);
        }

        [Test]
        public void BuildDestinationUri_Throws_When_Decoded_Destination_Uri_Not_Valid()
        {
            //arrange
            var incomingNonUriEncoded = new Uri("http://pseudohost.com:567/sessionId/dG90YWxseSFub3ReYSxVcmk=/destIndex0/destIndex1?query=this");
            //dG90YWxseSFub3ReYSxVcmk= decodes to "totally!not^a,Uri"

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(1);
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(2);
            var pathInspector = new ApiTransactionUtility(config);

            // act and assert
            Assert.Throws<CannotParseUriException>(() => pathInspector.BuildDestinationUri(incomingNonUriEncoded));
        }

        [Test]
        public void BuildDestinationUri_Throws_When_Not_Enough_Segments_For_Destination()
        {
            //arrange
            var incomingNotEnoughSegments = new Uri("http://pseudohost.com:567/sessionId/aHR0cHM6Ly90bi1yZXN0LXByb2R1Y3Rpb24uY2xvdWRhcHAubmV0OjQ0My9hcGkvdjEuMC8yMDE0Lw==");

            var config = Stub<IConfig>();
            config.Stub(c => c.SessionIdSegmentIndex).IgnoreArguments().Return(1);
            config.Stub(c => c.DestinationUrlSegementIndex).IgnoreArguments().Return(2);
            var pathInspector = new ApiTransactionUtility(config);

            // act and assert
            Assert.Throws<CannotParseUriException>(() => pathInspector.BuildDestinationUri(incomingNotEnoughSegments));
        }
    }
}
