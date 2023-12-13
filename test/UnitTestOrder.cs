using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using test;

namespace Tests;

public class UnitTestOrder
{
    // Test method for retrieving all orders from the schema webshop.
    [Test]
    public async Task GetAllOrders()
    {
        // Trigger rebuild for a fresh start.
        Helper.TriggerRebuild();
        
        // Create a test user for registration.
        var testUser = new UserModel
        {
            user_id = 1,
            full_name = "Bent",
            email = "bent@email.com",
            password = "123456789"
        };

        // Register the test user.
        await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", testUser);
        
        // Prepare expected orders and insert them into the database.
        var expected = new List<object>();
        for (var i = 1; i < 4; i++)
        {
            var order = new OrderModel()
            {
                order_id = i,
                user_id = 1
            };
            expected.Add(order);
            
            // SQL command to insert orders into the database.
            var sql =
                @"INSERT INTO webshop.order(user_id) VALUES (@user_id);";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, order);
            }
        }
    }

    // Test case for successfully creating an order in the schema webshop.
    [TestCase(1,1)]
    [TestCase(1,2)]
    public async Task SuccessfullyCreateAnOrder(int order_id, int user_id)
    {
        // Trigger rebuild for a fresh start.
        Helper.TriggerRebuild();
        
        // Register two test users.
        for (var i = 1; i <= 2; i++)
        {
            var user = new UserModel()
            {
                user_id = i,
                full_name = "Jens den 1",
                email = "jens" + i + "@email.com",
                password = "123456789"
            };
            
            // Register the test user.
            await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/account/register", user); 
        }

        // Create a test order.
        var testOrder = new OrderModel()
        {
            order_id = order_id,
            user_id = user_id
        };
        
        // Post the test order and validate the response.
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "api/order/post", testOrder);
        var responseBodyString = await httpResponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<OrderModel>>(responseBodyString);

        // Validate the response data against the test order.
        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.Should().BeEquivalentTo(testOrder);
        }
    }
}