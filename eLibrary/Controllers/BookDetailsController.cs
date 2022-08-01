using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eLibrary.Data;
using eLibrary.Modal;
using Microsoft.AspNetCore.Authorization;

namespace eLibrary.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BookDetailsController : ControllerBase
    {
        private readonly DataContext _context;
        #region public method
        #region constructor
        public BookDetailsController(DataContext context)
        {
            _context = context;
        }

        #endregion

        #region GetBookDetails
        // GET: api/BookDetailsModals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDetailsModal>>> GetBookDetails()
        {
          if (_context.BookDetails == null)
          {
              return NotFound();
          }
            return await _context.BookDetails.ToListAsync();
        }
        #endregion

        #region GetBookDetailsByID

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsModal>> GetBookDetails(int id)
        {
          if (_context.BookDetails == null)
          {
              return NotFound();
          }
            var bookDetailsModal = await _context.BookDetails.FindAsync(id);

            if (bookDetailsModal == null)
            {
                return NotFound();
            }

            return bookDetailsModal;
        }
        #endregion

        #region UpdateBookDetails

        // PUT: api/BookDetailsModals/5
        [HttpPut("{id}")]
        public async Task<string> UpdateBookDetails(int id, [FromBody] BookDetailsModal bookDetailsModal)
        {
            bookDetailsModal.Id = id;

            _context.Entry(bookDetailsModal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookDetailsExists(id))
                {
                    return "Failed to update book details";
                }
                else
                {
                    throw;
                }
            }

            return "Book details updated successfully";
        }
        #endregion

        #region InserBookDetails

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<string> InserBookDetails([FromBody] BookDetailsModal bookDetailsModal)
        {
          if (_context.BookDetails == null)
          {
                return "Book details is set to empty";
          }
            _context.BookDetails.Add(bookDetailsModal);
            await _context.SaveChangesAsync();

            return "Book details inserted successfully";
        }

        #endregion

        #region DeleteBookDetails
         
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookDetails(int id)
        {
            if (_context.BookDetails == null)
            {
                return NotFound();
            }
            var bookDetailsModal = await _context.BookDetails.FindAsync(id);
            if (bookDetailsModal == null)
            {
                return NotFound();
            }

            _context.BookDetails.Remove(bookDetailsModal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #endregion public method

        #region private method
        private bool BookDetailsExists(int id)
        {
            return (_context.BookDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion
    }
}
