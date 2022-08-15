using eLibrary.Data;
using eLibrary.Modal;
using eLibrary.Repository.Interface;
using Microsoft.AspNetCore.Identity;

namespace eLibrary.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DataContext _context;
        private IBookRepository _book;
        private IRoleRepository _role;
        public IBookRepository BookDetails
        {
            get
            {
                if (_book == null)
                {
                    _book = new BookRepository(_context);
                }
                return _book;
            }
        }
        public IRoleRepository RoleRepository
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_context);
                }
                return _role;
            }
        }
        public RepositoryWrapper(DataContext repositoryContext)
        {
            _context = repositoryContext;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
