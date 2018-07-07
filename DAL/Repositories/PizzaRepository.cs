using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PizzaRepository : BaseRepository<Pizza>
    {
        public PizzaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override ICollection<Pizza> GetAll()
        {
            return dbContext.PizzaSet.Include(p => p.Ingredients).ThenInclude(i => i.Ingredient).ToList();
        }


        public override void Create(Pizza item)
        {
            dbContext.Entry<Pizza>(item).State = EntityState.Added;

            foreach (var ingredient in item.Ingredients)
            {
                ingredient.PizzaId = item.Id;

                if (ingredient.Ingredient != null)
                {
                    ingredient.IngredientId = ingredient.Ingredient.Id;
                    ingredient.Ingredient = null;
                }

                dbContext.Entry<PizzaIngredient>(ingredient).State = EntityState.Added;
            }

        }

        public override void Update(Pizza item)
        {
            var dbItem = dbContext.Set<Pizza>().Include(i => i.Ingredients).FirstOrDefault(i => i.Id == item.Id);

            if (dbItem != null)
            {

                dbItem.Ingredients.Clear();

                foreach (var ingredient in item.Ingredients)
                {
                    ingredient.PizzaId = item.Id;

                    if (ingredient.Ingredient != null)
                    {
                        ingredient.IngredientId = ingredient.Ingredient.Id;
                        ingredient.Ingredient = null;
                    }

                    dbItem.Ingredients.Add(ingredient);

                }

                dbContext.Entry<Pizza>(dbItem).CurrentValues.SetValues(item);

            }

        }

    }
}
