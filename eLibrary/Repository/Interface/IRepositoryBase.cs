using eLibrary.Modal;

namespace eLibrary.Repository.Interface
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        Task<ApplicationUser> GetUser(string email);
    }
}
