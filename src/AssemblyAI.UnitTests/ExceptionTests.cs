using System.Net;
using NUnit.Framework;

namespace AssemblyAI.UnitTests;

[TestFixture]
public class ExceptionTests
{
    [Test]
    public void ApiException_Should_Extract_ErrorMessage()
    {
        const string error = """
                             {
                                 "error": "An error occurred.",
                                 "status": "error"
                             }
                             """;
        var exception = new ApiException(
            "Error with HTTP status code 400",
            (int)HttpStatusCode.BadRequest,
            error
        );
        Assert.That(exception.Message, Is.EqualTo("An error occurred."));
        Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(exception.ResponseContent, Is.EqualTo(error));
    }
    
    [Test]
    public void ApiException_Should_Extract_ResponseContent()
    {
        const string error = "Not Found";
        var exception = new ApiException(
            "Error with HTTP status code 404",
            (int)HttpStatusCode.NotFound,
            error
        );
        Assert.That(exception.Message, Is.EqualTo(error));
        Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.That(exception.ResponseContent, Is.EqualTo(error));
    }
    
    [Test]
    public void ApiException_Should_Fallback_To_Given_Message_When_Empty_String()
    {
        const string error = "";
        var exception = new ApiException(
            "Error with HTTP status code 404",
            (int)HttpStatusCode.NotFound,
            error
        );
        Assert.That(exception.Message, Is.EqualTo("Error with HTTP status code 404"));
        Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.That(exception.ResponseContent, Is.EqualTo(error));
    }
    
    [Test]
    public void ApiException_Should_Fallback_To_Given_Message_When_Null()
    {
        const string error = null!;
        var exception = new ApiException(
            "Error with HTTP status code 404",
            (int)HttpStatusCode.NotFound,
            error!
        );
        Assert.That(exception.Message, Is.EqualTo("Error with HTTP status code 404"));
        Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.That(exception.ResponseContent, Is.EqualTo(""));
    }
}