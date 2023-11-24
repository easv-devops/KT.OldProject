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

    public async Task GetAllAvatars()
    { 
        Helper.TriggerRebuild();
        
        
    }
    
    
    [TestCase("Kaj-avatar", 1000 )]
    [TestCase("Andrea-avatar", 2000 )]
    public async Task AvatarCanSuccessfullyBeCreated(string avatar_name, int price)
    {
        Helper.TriggerRebuild();
        var testAvatar = new Avatar
        {
            avatar_name = avatar_name,
            price = price
        };

        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "avatar", testAvatar);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<Avatar>>(responseBodyString);
        
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(testAvatar);
        }
    }

    [TestCase("Bent-avatar", 4, "This is a picture of Bent", true)]
    [TestCase("Klavs-avatar", 5, "This is a picture of Klavs", true)]
    public async Task AvatarCanSuccessfullyBeUpdated(string avatar_name, int price, string information, bool deleted)
    {
        Helper.TriggerRebuild();
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.Execute("INSERT INTO account.avatar(avatar_name, price, information, deleted) VALUES ('Erik', 11, 'This is a picture of Erik', FALSE) RETURNING *;");
        }

        var testAvatar = new Avatar()
        {
            avatar_name = avatar_name,
            price = price,
            information = information,
            deleted =deleted
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