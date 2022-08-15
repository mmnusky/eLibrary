using eLibrary.Data;
using eLibrary.Modal;
using eLibrary.Repository.Interface;
using Microsoft.AspNetCore.Identity;

namespace eLibrary.Repository
{
    public class BookRepository : RepositoryBase<BookDetailsModal>, IBookRepository
    {
        public BookRepository(DataContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
