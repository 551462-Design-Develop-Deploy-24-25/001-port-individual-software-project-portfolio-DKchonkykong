using NUnit.Framework;
namespace DDD_Tester;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestUserName()
    {
     string expectedName = "JohnDoe";
     string actualName = Environment.UserName;
     Assert.AreEqual(expectedName, actualName);    
    }
}