using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using test;

namespace Tests;

public class UnitTestOrder
{
    [Test]
    public async Task GetAllOrders()
    {
        Helper.TriggerRebuild();
        var testUser = new UserModel
        {
            user_id = 1,
            full_name = "Bent",
            email = "bent@email.com",
            password = "123456789"
        };

        await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);
        var expected = new List<object>();
        for (var i = 1; i < 4; i++)
        {
            var order = new OrderModel()
            {
                order_id = i,
                user_id = 1
            };
            expected.Add(order);
            var sql =
                @"INSERT INTO webshop.order(user_id) VALUES (@user_id);";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, order);
            }
        }
    }

    [TestCase(1,1)]
    [TestCase(1,2)]
    public async Task SuccessfullyCreateAnOrder(int order_id, int user_id)
    {
        Helper.TriggerRebuild();
        for (var i = 1; i <= 2; i++)
        {
            var user = new UserModel()
            {
                user_id = i,
                full_name = "Jens den 1",
                email = "jens" + i + "@email.com",
                password = "123456789"
            };
            await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", user); 
        }

        var testOrder = new OrderModel()
        {
            order_id = order_id,
            user_id = user_id
        };
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/order/post", testOrder);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<OrderModel>>(responseBodyString);

        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(testOrder);
        }
    }
}