using NUnit.Framework;

namespace AssemblyAI.Test;

[TestFixture]
public class UserAgentTests
{
    [Test]
    public void TestDefaultUserAgent()
    {
        Assert.That(UserAgent.Default, Is.Not.Null);
        var userAgentString = UserAgent.Default.ToAssemblyAIUserAgentString();
        Assert.That(userAgentString, Is.Not.Null);
        Assert.That(userAgentString, Is.Not.Empty);
        Assert.That(userAgentString, Does.StartWith("AssemblyAI/1.0 ("));
        Assert.That(userAgentString, Does.EndWith(")"));
        Assert.That(userAgentString, Does.Contain("sdk=CSharp/"));
        Assert.That(userAgentString, Does.Contain("runtime_env=.NET/8"));
    }
    
    
    [Test]
    public void TestMergeUserAgent()
    {
        var userAgent = new UserAgent(UserAgent.Default, new UserAgent(new Dictionary<string, UserAgentItem?>()
        {
            ["integration"] = new("SemanticKernel", "1.0"),
            ["runtime_env"] = null
        }));
        var userAgentString = userAgent.ToAssemblyAIUserAgentString();
        Assert.That(userAgentString, Is.Not.Null);
        Assert.That(userAgentString, Is.Not.Empty);
        Assert.That(userAgentString, Does.StartWith("AssemblyAI/1.0 ("));
        Assert.That(userAgentString, Does.EndWith(")"));
        Assert.That(userAgentString, Does.Contain("sdk=CSharp/"));
        Assert.That(userAgentString, Does.Not.Contain("runtime_env"));
        Assert.That(userAgentString, Does.Contain("integration=SemanticKernel/1.0"));
    }
}