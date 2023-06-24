using Microsoft.EntityFrameworkCore;
using OrderMngmt.Data.Interfaces;
using OrderMngmt.Data.Extensions;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace OrderMngmt.Data.Impl
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly OrderMngmtDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public Repository(OrderMngmtDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<T>();
        }

        public async Task<T?> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public IQueryable<T> GetAll(int pageNumber = 0, int pageSize = 0, string? sortField = null, string? sortOrder = null)
        {
            var query = _entities.AsQueryable().Paginate(pageNumber, pageSize).Sort(sortField, sortOrder);
            return query;
        }
    

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public async Task ExecuteStoredProc(string storedProcName, Dictionary<string, object> parameters)
        {
            // convert dictionary to sql parameters
            var sqlParameters = new List<SqlParameter>();
            foreach (var parameter in parameters)
            {
                sqlParameters.Add(new SqlParameter(parameter.Key, parameter.Value));
            }
            var sql = $"{storedProcName} {String.Join(", ", sqlParameters.Select(x => $"{x.ParameterName} = {x.ParameterName}"))}";
            await _dbContext.Database.ExecuteSqlRawAsync(sql, sqlParameters);

        }
    }
}