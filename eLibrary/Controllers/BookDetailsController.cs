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
using System.Net.Http.Headers;
using eLibrary.Repository.Interface;
using eLibrary.Modal.ViewModels;
using eLibrary.Shared;

namespace eLibrary.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BookDetailsController : ControllerBase
    {
        private readonly DataContext _context;
        private IRepositoryWrapper _repository;
        #region public method
        #region constructor
        public BookDetailsController(DataContext context, IRepositoryWrapper repository)
        {
            _context = context;
            _repository = repository;
        }

        #endregion

        #region GetBookDetails
        // GET: api/BookDetailsModals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDetailsModal>>> GetBookDetails()
        {
            try
            {
                return await _repository.BookDetails.GetAll();
            }
            catch(Exception)
            {
                ErrorModal error = new ErrorModal();
                error.StatusCode = 400;
                error.Message = Constant.GetAllBookError;
                return BadRequest(error);
            }
        }
        #endregion

      

        #region GetBookDetailsByID

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsModal>> GetBookDetails(int id)
        {
            try
            {
                if (_context.BookDetails == null)
                {
                    return NotFound();
                }
                var bookDetailsModal = await _repository.BookDetails.Get(id);

                if (bookDetailsModal == null)
                {
                    return NotFound();
                }

                return bookDetailsModal;
            }
            catch (Exception)
            {
                ErrorModal error = new ErrorModal();
                error.StatusCode = 400;
                error.Message = Constant.GetByIdError;
                return BadRequest(error);
            }
        }
        #endregion

        #region UpdateBookDetails

        // PUT: api/BookDetailsModals/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Author")]
        public async Task<IActionResult> UpdateBookDetails(int id, [FromBody] BookDetailsModal bookDetailsModal)
        {
            try
            {
                bookDetailsModal.Id = id;
                await _repository.BookDetails.Update(bookDetailsModal);
                return Ok();
            }
            catch (Exception ex)
            {
                ErrorModal error = new ErrorModal();
                error.StatusCode = 400;
                error.Message = Constant.UpdateBookError;
                return BadRequest(error);
            }
        }
        #endregion

        #region InserBookDetails

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Author")]
        public async Task<IActionResult> InserBookDetails([FromBody] BookDetailsModal bookDetailsModal)
        {
            try
            {
                if (_context.BookDetails == null)
                {
                    return BadRequest();
                }
                await _repository.BookDetails.Add(bookDetailsModal);
                return Ok();
            }
            catch (Exception)
            {
                ErrorModal error = new ErrorModal();
                error.StatusCode = 400;
                error.Message = Constant.InsertBookDetailslError;
                return BadRequest(error);
            }
        }

        #endregion

        #region DeleteBookDetails

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookDetails(int id)
        {
            try
            {
                if (_context.BookDetails == null)
                {
                    return NotFound();
                }
                var bookDetailsModal = await _repository.BookDetails.Get(id);
                if (bookDetailsModal == null)
                {
                    return NotFound();
                }
                await _repository.BookDetails.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                ErrorModal error = new ErrorModal();
                error.StatusCode = 400;
                error.Message = Constant.DelateBookError;
                return BadRequest(error);
            }
        }
        #endregion

        #region image upload

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
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
