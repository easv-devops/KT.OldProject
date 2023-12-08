using System.Net;
using System.Net.Http.Json;
using System.Threading.Channels;
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
            var avatar = new AvatarModel()
            {
                avatar_id = i,
                avatar_name = "Kajkage",
                avatar_price = 25,
                information = "Dette er en kajkage",
                deleted = false
            };
            expected.Add(avatar);
            var sql =
                @"INSERT INTO webshop.avatar(avatar_name, avatar_price, information) VALUES (@avatar_name, @avatar_price, @information);";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, avatar);
            }
        }
        
        var response = await new HttpClient().GetAsync(Helper.ApiBaseUrl + "avatar/all");
        ResponseDto<List<AvatarModel>> obj = JsonConvert.DeserializeObject<ResponseDto<List<AvatarModel>>>(await response.Content.ReadAsStringAsync());

        using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(expected);
        }
    }
    
    [TestCase("Kaj-avatar", 1000, "q")]
    [TestCase("Andrea-avatar", 2000, "w")]
    public async Task AvatarCanSuccessfullyBeCreated(string avatar_name, int avatar_price, string information)
    {
        Helper.TriggerRebuild();
        var testAvatar = new AvatarModel
        {
            avatar_id = 1,
            avatar_name = avatar_name,
            avatar_price = avatar_price,
            information = information,
            deleted = false
        };

        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "avatar", testAvatar);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<AvatarModel>>(responseBodyString);
        
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(testAvatar);
        }
    }

    [TestCase(null, 100, null)]
    [TestCase("Tom", 0, null)]
    public async Task AvatarShouldRejectCreation(string avatar_name, int avatar_price, string information)
    {
      Helper.TriggerRebuild();
      var testAvatar = new AvatarModel()
      {
          avatar_id = 1,
          avatar_name = avatar_name,
          avatar_price = avatar_price,
          information = information,
          deleted = false
      };

      var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "avatar", testAvatar);

      await using (await Helper.DataSource.OpenConnectionAsync())
      {
          httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      }
    }
    
    [TestCase("Bent-avatar", 4, "This is a picture of Bent")]
    [TestCase( "Klavs-avatar", 5, "This is a picture of Klavs")]
    public async Task AvatarCanSuccessfullyBeUpdated(string avatar_name, int avatar_price, string information)
    {
        Helper.TriggerRebuild();
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.Execute("INSERT INTO webshop.avatar(avatar_id, avatar_name, avatar_price, information, deleted) VALUES ( 1, 'Erik', 11, 'This is a picture of Erik', false) RETURNING *;");
        }

        var testAvatar = new AvatarModel()
        {
            avatar_id = 1,
            avatar_name = avatar_name,
            avatar_price = avatar_price,
            information = information,
            deleted = false
        };
        
        var httpResponse = await new HttpClient().PutAsJsonAsync(Helper.ApiBaseUrl + "avatar/1", testAvatar);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<AvatarModel>>(responseBodyString);
        
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(testAvatar);
        }
    }
    
    [TestCase(null, 200, null)]
    [TestCase("Lille", 0, null)]
    public async Task AvatarShouldRejectUpdate(string avatar_name, int avatar_price, string information)
    {
        Helper.TriggerRebuild();
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.Execute("INSERT INTO webshop.avatar(avatar_name, avatar_price, information) VALUES ('Jerry', 11, 'This is a picture of Jerry') RETURNING *;");
        }

        var testAvatar = new AvatarModel()
        {
            avatar_id = 1,
            avatar_name = avatar_name,
            avatar_price = avatar_price,
            information = information,
            deleted = false
        };
        
        var httpResponse = await new HttpClient().PutAsJsonAsync(Helper.ApiBaseUrl + "avatar/1", testAvatar);
        
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }

    [Test]
    public async Task AvatarCanSuccessfullyBeDeleted()
    {
        Helper.TriggerRebuild();
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.Execute(
                "INSERT INTO webshop.avatar(avatar_name, avatar_price, information) VALUES ('Mathias', 2, 'This is a picture of Mathias') RETURNING *;");
        }

        var httpResponse = await new HttpClient().DeleteAsync(Helper.ApiBaseUrl + "avatar/1");
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<object>>(responseBodyString);
        
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.MessageToClient.Should().Be("Succesfully deleted avatar");
        }
    }
}