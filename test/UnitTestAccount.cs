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
    
    [TestCase("Bo", "Bogade 1", 1000, "bob@cool.com", "123456789" )]
    [TestCase("Ib","Bogade 2", 1000, "Ib@cool.com", "123456789" )]
    public async Task UserCanSuccessfullyBeCreated(string full_name, string street, int zip, string email, string password)
    {
        Helper.TriggerRebuild();
        var testUser = new User()
        {
            full_name = full_name,
            street = street,
            zip = zip,
            email = email,
            password = password
        };
        
        var httpResponse  = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "account/register", testUser);
        var userFromResponseBody = JsonConvert.DeserializeObject<ResponseUser>(await httpResponse.Content.ReadAsStringAsync());

        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.QueryFirst<ResponseUser>("SELECT * FROM account.users;").Should().BeEquivalentTo(userFromResponseBody);
        }
    }
}