using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using test;

namespace Tests;

public class Tests 
{
    
    [TestCase("Bob", "Bogade 1", 1000, "bob@cool.com", false)]
    [TestCase("Ib","Bogade 2", 1000, "Ib@cool.com", false )]
    public async Task UserCanSuccessfullyBeCreated(string full_name, string street, int zip, string email, bool admin)
    {
        Helper.TriggerRebuild();
        var testUser = new User()
        {
            user_id = 1,
            full_name = full_name,
            street = street,
            zip = zip,
            email = email,
            admin = admin
        };

        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "register", testUser);
        var userFromResponseBody = JsonConvert.DeserializeObject<User>(await httpResponse.Content.ReadAsStringAsync());

        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.QueryFirst<User>("SELECT * FROM testingSchema.testingDatabase;").Should().BeEquivalentTo(userFromResponseBody);
        }
    }

    [TestCase("Helle", "Hellegade 1", 9000, "helle@mad.com", false)]
    public async Task UserCanSuccessfullyBeUpdated(string full_name, string street, int zip, string email, bool admin)
    {
        Helper.TriggerRebuild();
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.Execute(
                "INSERT INTO testingSchema.testingDatabase (full_name, street, zip, email, admin) VALUES ('PlaceholderName', 'PlaceholderStreet', 6969, 'placeholderEmail', true) RETURNING *;");
        }

        var testUser = new User()
                { user_id = 1, full_name = full_name, street = street, zip = zip, email = email, admin = admin };

        var httpResponse = await new HttpClient().PutAsJsonAsync(Helper.ApiBaseUrl + "/1", testUser);
        var userFromReponseBody = JsonConvert.DeserializeObject<User>(await httpResponse.Content.ReadAsStringAsync());

        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.QueryFirst<User>("SELECT * FROM testingSchema.testingDatabase;").Should().BeEquivalentTo(userFromReponseBody);
        }
    }
}