using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AdminUWP.Interfaces;
using AdminUWP.Model;
using AdminUWP.Models;
using AdminUWP.ViewModels;
using Windows.UI.Xaml.Media.Imaging;

namespace AdminUWP.BL
{
    public class DataService : IDataService
    {

        private string _token;
        private Uri _apiUrl;

        public async Task<List<Pizza>> GetPizzaListAsync()
        {
            return await Task.Run(() =>
            {

                using (var client = new HttpClient())
                {

                    var Result = new List<Pizza>();

                    for (int i = 1; i <= 100; i++)
                    {
                        Result.Add(new Pizza { Name = $"Pizza{i}", Ingredients = new List<Ingredient> { new Ingredient { Name = "Ingedient" }, new Ingredient { Name = "Ingedient2" }, new Ingredient { Name = "Ingedient3" } } });
                    }

                    return Result;

                    //var res = client.GetAsync(APP_PATH + "/api/ref/getPizzas").Result;

                    //var res2 = res.Content.ReadAsAsync<List<Pizza>>().Result;

                    //return res2;

                }

            });
        }

        public async Task<Pizza> SavePizzaAsync(Pizza pizza)
        {
            return await Task.Run(() =>
            {
                return new Pizza();

            });
        }

        public async Task<List<Ingredient>> GetIngredientListAsync()
        {

            return await Task.Run(() =>
            {
                var Result = new List<Ingredient>();

                for (int i = 1; i <= 20; i++)
                {
                    Result.Add(new Ingredient { Name = $"Ingredient{i}" });
                }

                return Result;
            });

        }

        public async Task<Ingredient> SaveIngredientAsync(Ingredient ingredient)
        {
            return await Task.Run(() =>
            {
                return new Ingredient();
            });
        }

        public async Task<bool> DeletePizzaAsync(Pizza pizza)
        {
            return await Task.Run(() =>
            {
                return false;
            });
        }

        public async Task<bool> DeleteIngredientAsync(Ingredient ingredient)
        {
            return await Task.Run(() =>
            {
                return false;
            });
        }

        public Task<BitmapImage> GetImage(Uri url)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Authorise(string login, string password, Uri apiUrl)
        {
            return await Task.Run(() =>
            {
                return true;
            });
        }
    }

}
