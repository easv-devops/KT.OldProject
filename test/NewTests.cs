using api.Controllers;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class NewTests
{

    [Test]
    public void test()
    { 
        Assert.That(AvatarController.DoSomething(), Is.EqualTo("Hello World3"));
    }
}