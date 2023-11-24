using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using NUnit.Framework;
using test;

namespace Tests;

[TestFixture]
public class UnitTestAvatar
{

    [Test]
    public async Task GetAllAvatars()
    { 
        Helper.TriggerRebuild();
        var expected = new List<object>();
        for (var i = 1; i < 3; i++)
        {
            var avatar = new Avatar()
            {
                avatar_name = "Kajkage",
                avatar_price = 25,
                information = "Dette er en kajkage"
            };
            expected.Add(avatar);
            var sql =
                @"INSERT INTO account.avatar(avatar_name, avatar_price, information) VALUES (@avatar_name, @avatar_price, @information);";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, avatar);
            }
        }

        var response = await new HttpClient().GetAsync(Helper.ApiBaseUrl + "avatar/all");
        ResponseDto<List<Avatar>> obj = JsonConvert.DeserializeObject<ResponseDto<List<Avatar>>>(await response.Content.ReadAsStringAsync());

        using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(expected);
        }
    }
    
    
    [TestCase("Kaj-avatar", 1000, "", false )]
    [TestCase("Andrea-avatar", 2000, "", false )]
    public async Task AvatarCanSuccessfullyBeCreated(string avatar_name, int avatar_price, string information, bool deleted)
    {
        Helper.TriggerRebuild();
        var testAvatar = new Avatar
        {
            avatar_name = avatar_name,
            avatar_price = avatar_price,
            information = information,
            deleted = deleted
        };

        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "avatar", testAvatar);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<Avatar>>(responseBodyString);
        
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(testAvatar);
        }
    }

    [TestCase("Bent-avatar", 4, "This is a picture of Bent")]
    [TestCase("Klavs-avatar", 5, "This is a picture of Klavs")]
    public async Task AvatarCanSuccessfullyBeUpdated(string avatar_name, int avatar_price, string information)
    {
        Helper.TriggerRebuild();
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.Execute("INSERT INTO account.avatar(avatar_name, avatar_price, information) VALUES ('Erik', 11, 'This is a picture of Erik') RETURNING *;");
        }

        var testAvatar = new Avatar()
        {
            avatar_name = avatar_name,
            avatar_price = avatar_price,
            information = information
        };
        
        var httpResponse = await new HttpClient().PutAsJsonAsync(Helper.ApiBaseUrl + "avatar/1", testAvatar);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<Avatar>>(responseBodyString);
        
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(testAvatar);
        }
    }
}