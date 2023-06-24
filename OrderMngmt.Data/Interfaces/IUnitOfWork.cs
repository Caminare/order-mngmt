namespace OrderMngmt.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        IUserAuthenticationRepository GetUserAuthenticationRepository();
        Task<int> SaveChanges();
    }
}