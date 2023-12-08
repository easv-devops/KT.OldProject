using System.Net;
using System.Net.Http.Json;
using Dapper;
using FluentAssertions;

using Newtonsoft.Json;
using NUnit.Framework;
using test;

namespace Tests;

[TestFixture]
public class Tests
{
    
    [TestCase("Boo", "bob@cool.com", "123456789" )]
    [TestCase("Ibb", "Ib@cool.com", "123456789" )]
    public async Task UserCanSuccessfullyBeCreated(string full_name, string email, string password)
    {
        Helper.TriggerRebuild();
        var testUser = new UserModel
        {
            full_name = full_name,
            email = email,
            password = password
        };
        
        var httpResponse  =await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<UserModel>>(responseBodyString);

        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.email.Should().BeEquivalentTo(testUser.email);
        }
    }
    
    [TestCase("t", "bobyugbhnj@cool.com", "123456789" )]
    [TestCase("Benter", "l.com", "123456789" )]
    [TestCase("Benter", "bobyugbhnj@cool.com", "7" )]
    public async Task CanNotCreateUserWithInvalidCharacter(string full_name, string email, string password)
    {
        Helper.TriggerRebuild();
        var testUser = new UserModel
        {
            full_name = full_name,
            email = email,
            password = password
        };
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);
       
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            
           httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
    [TestCase("bobyugbhnj@cool.com", "123456789" )]
    public async Task UserCanSuccessfullyLogin( string email, string password)
    {
        Helper.TriggerRebuild();
        var testUser = new UserModel
        {
            
            email = email,
            password = password,
            full_name = "Bent"
        };

        await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);
        
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/login", testUser);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<UserModel>>(responseBodyString);
        
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.MessageToClient.Should().Be("Welcome Bent");
        }
    }
}