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

    [TestCase("Bent-avatar", 4)]
    [TestCase("Klavs-avatar", 5)]
    public async Task AvatarCanSuccessfullyBeCUpdated(string avatar_name, int price)
    {
        Helper.TriggerRebuild();
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.Execute("INSERT INTO account.avatar(avatar_name, price) VALUES ('Erik', 11) RETURNING *;");
        }

        var testAvatar = new Avatar()
        {
            avatar_name = avatar_name,
            price = price
        };
        
        var httpResponse = await new HttpClient().PutAsJsonAsync(Helper.ApiBaseUrl + "avatar/1", testAvatar);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<Avatar>>(responseBodyString);
        
        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(testAvatar);
        }
    }
}