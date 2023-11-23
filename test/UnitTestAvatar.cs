using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using NUnit.Framework;
using test;

namespace Tests;


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

        await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "avatar", testAvatar);

        await using (var conn = await Helper.DataSource.OpenConnectionAsync())
        {
            conn.ExecuteScalar<int>("SELECT COUNT(*) FROM account.avatar;").Should().Be(1);
        }
    }
}