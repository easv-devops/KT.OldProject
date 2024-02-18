using api.Controllers;
using NUnit.Framework;

namespace Tests;

public class NewTests
{
    
    private readonly AvatarController _avatarController;
    
    public NewTests(AvatarController avatarController)
    {
        _avatarController = avatarController;
    }

    [Test]
    public void test()
    { 
        Assert.That(_avatarController.DoSomething(), Is.EqualTo("Hello World"));
    }
}