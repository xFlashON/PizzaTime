using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using AdminUWP.Interfaces;
using AdminUWP.Model;
using AdminUWP.Models;
using AdminUWP.ViewModels;
using Newtonsoft.Json;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace AdminUWP.BL
{
    public class DataService : IDataService
    {

        private string _token;
        private string _apiUrl;

        public async Task<List<Pizza>> GetPizzaListAsync()
        {
            return await Task.Run(() =>
            {

                using (var client = new HttpClient())
                {

                    try
                    {
                        var data = client.GetAsync(_apiUrl + "api/Data/GetMenu").Result;

                        var result = data.Content.ReadAsAsync<List<Pizza>>().Result;

                        return result;

                    }
                    catch (Exception ex)
                    {
                        return new List<Pizza>();
                    }

                }

            });
        }

        public async Task<Pizza> SavePizzaAsync(Pizza pizza)
        {
            return await Task.Run<Pizza>(async () =>
            {

                using (var client = new HttpClient())
                {

                    try
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + _token);

                        var result = await client.PostAsJsonAsync(_apiUrl + "api/Data/SavePizza", pizza);

                        if (result.IsSuccessStatusCode)
                        {
                            return result.Content.ReadAsAsync<Pizza>().Result;
                        }
                        else
                        {
                            return null;
                        }

                    }
                    catch (Exception ex)
                    {
                        return null;
                    }

                }

            });
        }

        public async Task<List<Ingredient>> GetIngredientListAsync()
        {

            return await Task.Run(() =>
            {
                using (var client = new HttpClient())
                {

                    try
                    {
                        var data = client.GetAsync(_apiUrl + "api/Data/GetIngredients").Result;

                        var result = data.Content.ReadAsAsync<List<Ingredient>>().Result;

                        return result;

                    }
                    catch (Exception ex)
                    {
                        return new List<Ingredient>();
                    }

                }
            });

        }

        public async Task<Ingredient> SaveIngredientAsync(Ingredient ingredient)
        {
            return await Task.Run(async () =>
            {

                using (var client = new HttpClient())
                {

                    try
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + _token);

                        var result = await client.PostAsJsonAsync(_apiUrl + "api/Data/SaveIngredient", ingredient);

                        if (result.IsSuccessStatusCode)
                        {
                            return result.Content.ReadAsAsync<Ingredient>().Result;
                        }
                        else
                        {
                            return null;
                        }

                    }
                    catch (Exception ex)
                    {
                        return null;
                    }

                }


            });
        }

        public async Task<bool> DeletePizzaAsync(Pizza pizza)
        {
            return await Task.Run(async () =>
            {
                using (var client = new HttpClient())
                {

                    try
                    {

                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + _token);

                        var formContent = new FormUrlEncodedContent(new[]{
                        new KeyValuePair<string, string>("id", pizza.Id.ToString())});


                        var result = await client.PostAsync(_apiUrl + "api/Data/DeletePizza", formContent);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            });
        }

        public async Task<bool> DeleteIngredientAsync(Ingredient ingredient)
        {
            return await Task.Run(async () =>
            {
                using (var client = new HttpClient())
                {

                    try
                    {

                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + _token);

                        var formContent = new FormUrlEncodedContent(new[]{
                        new KeyValuePair<string, string>("id", ingredient.Id.ToString())});


                        var result = await client.PostAsync(_apiUrl + "api/Data/DeleteIngredient", formContent);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            });
        }

        public async Task<bool> Authorise(string login, string password, string apiUrl)
        {
            return await Task.Run(async () =>
            {

                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

                    var formContent = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("username", login),
                    new KeyValuePair<string, string>("password", password)
            });


                    try
                    {
                        var result = await client.PostAsync(apiUrl + "/api/Authorization/token", formContent);

                        if (result.IsSuccessStatusCode)
                        {
                            _apiUrl = apiUrl;

                            var data = await result.Content.ReadAsStringAsync();

                            var o = new { access_token = string.Empty, role = string.Empty };

                            var responseData = JsonConvert.DeserializeAnonymousType(data, o);

                            if (responseData.role != "Admin")
                                return false;

                            _token = responseData.access_token;

                            return true;

                        }

                        return false;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }

            });
        }

        public Task<byte[]> GetImageData(string imageUrl)
        {

            return Task.Run(async () =>
            {

                using (var client = new HttpClient())
                {

                    try
                    {
                        var data = await client.GetAsync(_apiUrl + imageUrl);

                        var result = await data.Content.ReadAsByteArrayAsync();

                        return result;

                    }
                    catch (Exception ex)
                    {
                        return new byte[] { };
                    }
                }

            });

        }

        public Task<bool> SaveImageAsync(Guid id, byte[] data, string imageType)
        {

            return Task.Run(async () =>
            {

                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + _token);

                    var content = new MultipartFormDataContent();

                    content.Add(new StringContent(id.ToString()), "id");

                    content.Add(new StringContent(imageType), "type");

                    content.Add(new StringContent(Convert.ToBase64String(data)), "imagedata");

                    try
                    {
                        var result = await client.PostAsync(_apiUrl + "api/Data/SaveImage", content);

                        if (result.IsSuccessStatusCode)
                        {

                            return true;

                        }

                        return false;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }


            });


        }

        public string GetApiUrl()
        {
            return _apiUrl;
        }

        public async Task<ICollection<Customer>> GetCustomerListAsync()
        {
            return await Task.Run(() =>
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + _token);

                    try
                    {
                        var data = client.GetAsync(_apiUrl + "api/Data/GetCustomers").Result;

                        var result = data.Content.ReadAsAsync<List<Customer>>().Result;

                        return result;

                    }
                    catch (Exception ex)
                    {
                        return new List<Customer>();
                    }

                }
            });
        }

        public async Task<ICollection<Order>> GetOrderListAsync()
        {
            return await Task.Run(() =>
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + _token);

                    try
                    {
                        var data = client.GetAsync(_apiUrl + "api/Data/GetOrders").Result;

                        var result = data.Content.ReadAsAsync<List<Order>>().Result;

                        return result;

                    }
                    catch (Exception ex)
                    {
                        return new List<Order>();
                    }

                }
            });
        }
    }

}
