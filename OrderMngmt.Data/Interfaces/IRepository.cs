namespace OrderMngmt.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        IQueryable<T> GetAll(int pageNumber = 0, int pageSize = 0, string? sortField = null, string? sortOrder = null);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<int> ExecuteStoredProc(string storedProcName, Dictionary<string, object> parameters, string outParameterName);
    }
}