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
        var testUser = new User
        {
            full_name = full_name,
            street = street,
            zip = zip,
            email = email,
            password = password
        };
        
        await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);

        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.ExecuteScalar<int>("SELECT COUNT(*) FROM account.users;").Should().Be(1);
        }
    }
}