using AdminUWP.Model;
using AdminUWP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace AdminUWP.Interfaces
{
    public interface IDataService
    {

        Task<bool> Authorise(string login, string password, Uri apiUrl);

        Task<List<Pizza>> GetPizzaListAsync();

        Task<Pizza> SavePizzaAsync(Pizza pizza);

        Task<bool> DeletePizzaAsync(Pizza pizza);

        Task<List<Ingredient>> GetIngredientListAsync();

        Task<Ingredient> SaveIngredientAsync(Ingredient ingredient);

        Task<bool> DeleteIngredientAsync(Ingredient ingredient);

        Task<BitmapImage> GetImage(Uri url);

    }
}