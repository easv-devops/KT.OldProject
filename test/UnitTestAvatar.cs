using System.Net;
using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using test;

namespace Tests;

[TestFixture]
public class UnitTestAvatar
{
    // Test method to retrieve all avatars.
    [Test]
    public async Task GetAllAvatars()
    { 
        // Trigger rebuild for setup.
        Helper.TriggerRebuild();
        
        // Set up expected avatar data.
        var expected = new List<object>();
        for (var i = 1; i <= 2; i++)
        {
            
            // Create avatar and add to expected list.
            var avatar = new AvatarModel()
            {
                avatar_id = i,
                avatar_name = "Kajkage",
                avatar_price = 25,
                information = "Dette er en kajkage",
                deleted = false
            };
            expected.Add(avatar);
            
            // SQL query to insert avatar into the database.
            var sql =
                @"INSERT INTO webshop.avatar(avatar_name, avatar_price, information) VALUES (@avatar_name, @avatar_price, @information);";
            // Execute SQL query to insert avatar.
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, avatar);
            }
        }
        
        // Make HTTP request to get all avatars.
        var response = await new HttpClient().GetAsync(Helper.ApiBaseUrl + "avatar/all");
        // Deserialize response and assert equivalence with expected data.
        ResponseDto<List<AvatarModel>> obj = JsonConvert.DeserializeObject<ResponseDto<List<AvatarModel>>>(await response.Content.ReadAsStringAsync());

        // Assert equivalence of response data with expected data.
        using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(expected);
        }
    }
    
    // Test case for successfully creating an avatar.
    [TestCase("Kaj-avatar", 1000, "q")]
    [TestCase("Andrea-avatar", 2000, "w")]
    public async Task AvatarCanSuccessfullyBeCreated(string avatar_name, int avatar_price, string information)
    {
        // Trigger rebuild for setup.
        Helper.TriggerRebuild();
        
        // Create test avatar.
        var testAvatar = new AvatarModel
        {
            avatar_id = 1,
            avatar_name = avatar_name,
            avatar_price = avatar_price,
            information = information,
            deleted = false
        };

        // Make HTTP request to create avatar.
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "avatar", testAvatar);
        
        // Deserialize response and assert equivalence with test avatar.
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<AvatarModel>>(responseBodyString);
        
        // Assert equivalence of response data with test avatar.
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(testAvatar);
        }
    }

    // Test case for rejecting avatar creation.
    [TestCase(null, 100, null)]
    [TestCase("Tom", 0, null)]
    public async Task AvatarShouldRejectCreation(string avatar_name, int avatar_price, string information)
    {
        // Trigger rebuild for setup.
      Helper.TriggerRebuild();
      // Create test avatar.
      var testAvatar = new AvatarModel()
      {
          avatar_id = 1,
          avatar_name = avatar_name,
          avatar_price = avatar_price,
          information = information,
          deleted = false
      };

      // Make HTTP request to create avatar and assert for BadRequest status.
      var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "avatar", testAvatar);

      // Assert that the HTTP response status is BadRequest.
      await using (await Helper.DataSource.OpenConnectionAsync())
      {
          httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      }
    }
    
    // Test case for successfully updating an avatar.
    [TestCase("Bent-avatar", 4, "This is a picture of Bent")]
    [TestCase( "Klavs-avatar", 5, "This is a picture of Klavs")]
    public async Task AvatarCanSuccessfullyBeUpdated(string avatar_name, int avatar_price, string information)
    {
        // Trigger rebuild for setup.
        Helper.TriggerRebuild();
        // Insert initial avatar data into the database.
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.Execute("INSERT INTO webshop.avatar(avatar_id, avatar_name, avatar_price, information, deleted) VALUES ( 1, 'Erik', 11, 'This is a picture of Erik', false) RETURNING *;");
        }

        // Create test avatar for update.
        var testAvatar = new AvatarModel()
        {
            avatar_id = 1,
            avatar_name = avatar_name,
            avatar_price = avatar_price,
            information = information,
            deleted = false
        };
        
        // Make HTTP request to update avatar.
        var httpResponse = await new HttpClient().PutAsJsonAsync(Helper.ApiBaseUrl + "avatar/1", testAvatar);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        
        // Deserialize response and assert equivalence with test avatar.
        var obj = JsonConvert.DeserializeObject<ResponseDto<AvatarModel>>(responseBodyString);
        
        // Assert equivalence of response data with test avatar.
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(testAvatar);
        }
    }
    
    // Test case for rejecting avatar update.
    [TestCase(null, 0, null)]
    [TestCase(null, 200, null)]
    [TestCase("Lille", 0, null)]
    public async Task AvatarShouldRejectUpdate(string avatar_name, int avatar_price, string information)
    {
        // Trigger rebuild for setup.
        Helper.TriggerRebuild();
        
        // Insert initial avatar data into the database.
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.Execute("INSERT INTO webshop.avatar(avatar_name, avatar_price, information) VALUES ('Jerry', 11, 'This is a picture of Jerry') RETURNING *;");
        }

        // Create test avatar for update.
        var testAvatar = new AvatarModel()
        {
            avatar_id = 1,
            avatar_name = avatar_name,
            avatar_price = avatar_price,
            information = information,
            deleted = false
        };
        
        // Make HTTP request to update avatar and assert for BadRequest status.
        var httpResponse = await new HttpClient().PutAsJsonAsync(Helper.ApiBaseUrl + "avatar/1", testAvatar);
        
        // Assert that the HTTP response status is BadRequest.
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }

    // Test method for successfully deleting an avatar.
    [Test]
    public async Task AvatarCanSuccessfullyBeDeleted()
    {
        // Trigger rebuild for setup.
        Helper.TriggerRebuild();
        
        // Insert initial avatar data into the database.
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.Execute(
                "INSERT INTO webshop.avatar(avatar_name, avatar_price, information) VALUES ('Mathias', 2, 'This is a picture of Mathias') RETURNING *;");
        }

        // Make HTTP request to delete avatar.
        var httpResponse = await new HttpClient().DeleteAsync(Helper.ApiBaseUrl + "avatar/1");
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        
        // Deserialize response and assert success message.
        var obj = JsonConvert.DeserializeObject<ResponseDto<object>>(responseBodyString);
        
        // Assert that the message to the client indicates successful deletion.
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.MessageToClient.Should().Be("Succesfully deleted avatar");
        }
    }

    [Test]
    public void TestApiBase()
    {
        Assert.That(Helper.ApiBaseUrl, Is.EqualTo("http://localhost:5000/"));
    }
}