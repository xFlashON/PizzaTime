using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IDataAccess, IDisposable
    {

        private ApplicationDbContext _dbContext;
        private IRepository<Pizza> _pizzaRepository;
        private IRepository<Ingredient> _ingredientRepository;
        private IRepository<Customer> _customerRepository;
        private IRepository<Order> _orderRepository;
        private IRepository<PizzaPrice> _pizzaPriceRepository;
        private IRepository<IngredientPrice> _ingredientPriceRepository;
        private IRepository<PizzaImage> _pizzaImageRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IRepository<Pizza> Pizzas => _pizzaRepository ?? (_pizzaRepository = new PizzaRepository(_dbContext));

        public IRepository<Ingredient> Ingredients => _ingredientRepository ?? (_ingredientRepository = new IngredientRepository(_dbContext));

        public IRepository<Customer> Customers => _customerRepository ?? (_customerRepository = new CustomerRepository(_dbContext));

        public IRepository<Order> Orders => _orderRepository ?? (_orderRepository = new OrderRepository(_dbContext));

        public IRepository<PizzaPrice> PizzaPrices => _pizzaPriceRepository ?? (_pizzaPriceRepository = new PizzaPriceRepository(_dbContext));

        public IRepository<IngredientPrice> IngredientPrices => _ingredientPriceRepository ?? (_ingredientPriceRepository = new IngredientPriceRepository(_dbContext));

        public IRepository<PizzaImage> PizzaImages => _pizzaImageRepository ?? (_pizzaImageRepository = new PizzaImageRepository(_dbContext));

        public int SaveChanges()
        {
           return _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
