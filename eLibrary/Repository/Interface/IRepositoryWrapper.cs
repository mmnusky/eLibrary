namespace eLibrary.Repository.Interface
{
    public interface IRepositoryWrapper
    {
        IBookRepository BookDetails { get; }
        IRoleRepository RoleRepository { get; } 
        void Save();
    }
}
