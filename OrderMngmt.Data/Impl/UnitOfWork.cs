using OrderMngmt.Data.Models;
using OrderMngmt.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace OrderMngmt.Data.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderMngmtDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(OrderMngmtDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)];
            }

            var repository = new Repository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public IUserAuthenticationRepository GetUserAuthenticationRepository()
        {
            if (_repositories.ContainsKey(typeof(IUserAuthenticationRepository)))
            {
                return (IUserAuthenticationRepository)_repositories[typeof(IUserAuthenticationRepository)];
            }

            var repository = new UserAuthenticationRepository(_context, _userManager);
            _repositories.Add(typeof(IUserAuthenticationRepository), repository);
            return repository;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}