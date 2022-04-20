using producer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace producer.Utils
{
    public class Validat
    {
        public static APIresult CheckUserInfo(Userinfo user)
        {
            using (var client = new HttpClient())
            {
                Uri uri = new Uri("https://reqres.in/api/login");
                client.DefaultRequestHeaders.Accept.Clear();
                APIresult apiResult = new APIresult();

                try
                {
                    var userinfo = JsonSerializer.Serialize(user);
                    var requestContent = new StringContent(userinfo, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(uri, requestContent).Result;

                    response.EnsureSuccessStatusCode();

                    if(response.IsSuccessStatusCode)
                    {
                        apiResult.hasError = false;
                        apiResult.email = user.email;
                    }
                    else
                    {
                        apiResult.hasError = true;
                        apiResult.email = "";
                    }
                    return apiResult;
                }
                catch (Exception ex)
                {
                    apiResult.hasError = true;
                    apiResult.email = "";
                    return apiResult;
                }
            }
        }
    }
}
