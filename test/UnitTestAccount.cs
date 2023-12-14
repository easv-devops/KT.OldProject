using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using test;

namespace Tests;

[TestFixture]
public class Tests
{
    // Test case for successfully creating a user.
    [TestCase("Boo", "bob@cool.com", "123456789" )]
    [TestCase("Ibb", "Ib@cool.com", "123456789" )]
    public async Task UserCanSuccessfullyBeCreated(string full_name, string email, string password)
    {
        
        // Trigger a rebuild.
        Helper.TriggerRebuild();
        
        // Create a test user with the given parameters.
        var testUser = new UserModel
        {
            full_name = full_name,
            email = email,
            password = password
        };
        
        // Send a POST request to register the test user.
        var httpResponse  =await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);
        
        // Deserialize the response and validate the email of the created user.
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<UserModel>>(responseBodyString);

        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.email.Should().BeEquivalentTo(testUser.email);
        }
    }
    
    // Test case for attempting to create a user with invalid characters.
    [TestCase("t", "bobyugbhnj@cool.com", "123456789" )]
    [TestCase("Benter", "l.com", "123456789" )]
    [TestCase("Benter", "bobyugbhnj@cool.com", "7" )]
    public async Task CanNotCreateUserWithInvalidCharacter(string full_name, string email, string password)
    {
        // Trigger a rebuild.
        Helper.TriggerRebuild();
        
        // Create a test user with the given parameters.
        var testUser = new UserModel
        {
            full_name = full_name,
            email = email,
            password = password
        };
        
        // Send a POST request to register the test user.
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);
       
        // Validate that the response status code is BadRequest.
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            
           httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
    // Test case for successfully logging in a user.
    [TestCase("bobyugbhnj@cool.com", "123456789" )]
    public async Task UserCanSuccessfullyLogin( string email, string password)
    {
        // Trigger a rebuild.
        Helper.TriggerRebuild();
        
        // Create a test user with the given parameters.
        var testUser = new UserModel
        {
            full_name = "Bent",
            email = email,
            password = password
        };

        // Register the test user before attempting to log in.
        await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);
        
        // Send a POST request to log in the test user.
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/login", testUser);
        
        // Deserialize the response and validate the welcome message.
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<UserModel>>(responseBodyString);
        
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.MessageToClient.Should().Be("Welcome Bent");
        }
    }
}