using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Cache;
using DocumentStoreManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStoreManagement.Controllers
{
    /// <summary>
    /// Document Management API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : BaseController
    {
        private readonly IDocumentService _documentService;
        private readonly ICacheService _cacheService;
        private static readonly string cacheKey = "document-list-cache";

        /// <summary>
        /// Add dependencies to controller
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="documentService"></param>
        /// <param name="cacheService"></param>
        public DocumentsController(IUnitOfWork unitOfWork, IDocumentService documentService, ICacheService cacheService) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _documentService = documentService;
            _cacheService = cacheService;
        }

        /// <summary>
        /// Searches the document list by type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A document list filtered by type</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/documents/filter/{type}
        ///     
        /// ***NOTES***: To get data by type, enters one of the following values:
        /// * **1**: Gets the book data.
        /// * **2**: Gets the magazine data.
        /// * **3**: Gets the newspaper data.
        ///
        /// </remarks>
        [HttpGet("filter/{type}")]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocumentsByType(int type)
        {
            try
            {
                // Set the expiration of cache
                TimeSpan expiration = TimeSpan.FromSeconds(30);

                // Get list of documents
                return Ok(await _cacheService.GetOrSetAsync(
                    key: $"{cacheKey}-{type}",
                    func: _documentService.GetByType(type),
                    expiration: expiration));
            }
            catch (Exception e)
            {
                // Return error message
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a document bases on document id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A document matches input id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/documents/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(string id)
        {
            // Get document by id
            Document document = await _documentService.GetById(id);
            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        /// <summary>
        /// Updates a document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedDocument"></param>
        /// <returns>An updated document</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/documents/{id}
        ///     {
        ///         "id": "id",
        ///         "publisherName": "Example Name",
        ///         "releaseQuantity": 12
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(string id, [FromBody] Document updatedDocument)
        {
            // Return bad request if ids don't match
            if (id != updatedDocument.Id)
            {
                return BadRequest();
            }

            try
            {
                // Update document
                await _documentService.Update(updatedDocument);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                // Check if document exists
                Document document = await _documentService.GetById(id);
                if (document == null)
                {
                    // Return document not found error
                    return NotFound();
                }
                else
                {
                    // Return error message
                    return BadRequest(e.Message);
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a book
        /// </summary>
        /// <param name="newBook"></param>
        /// <returns>A newly created book</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/documents/book
        ///     {
        ///         "publisherName": "Example Name",
        ///         "releaseQuantity": 12,
        ///         "authorName": "J. K. Rowling",
        ///         "pageNumber": 218
        ///     }
        ///
        /// </remarks>
        [HttpPost("book")]
        public async Task<ActionResult> PostBook([FromBody] Book newBook)
        {
            return await CreateDocument(newBook);
        }

        /// <summary>
        /// Creates a magazine
        /// </summary>
        /// <param name="newMagazine"></param>
        /// <returns>A newly created magazine</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/documents/magazine
        ///     {
        ///         "publisherName": "Example Name",
        ///         "releaseQuantity": 12,
        ///         "releaseNumber": 200,
        ///         "releaseMonth": "07/2023"
        ///     }
        ///
        /// </remarks>
        [HttpPost("magazine")]
        public async Task<ActionResult> PostMagazine([FromBody] Magazine newMagazine)
        {
            return await CreateDocument(newMagazine);
        }

        /// <summary>
        /// Creates a newspaper
        /// </summary>
        /// <param name="newNewspaper"></param>
        /// <returns>A newly created newspaper</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/documents/newspaper
        ///     {
        ///         "publisherName": "Example Name",
        ///         "releaseQuantity": 12,
        ///         "releaseDate": "2023-10-04T08:44:20.351Z"
        ///     }
        ///
        /// </remarks>
        [HttpPost("newspaper")]
        public async Task<ActionResult> PostNewspaper([FromBody] Newspaper newNewspaper)
        {
            return await CreateDocument(newNewspaper);
        }

        /// <summary>
        /// Deletes a document
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Document is deleted</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/documents/{id}
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(string id)
        {
            // Get document by id
            Document document = await _documentService.GetById(id);
            if (document == null)
            {
                return NotFound();
            }

            // Delete document
            await _documentService.Delete(id);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        #region Helpers
        /// <summary>
        /// Generic method to create new document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newDocument"></param>
        private async Task<ActionResult> CreateDocument<T>(T newDocument) where T : BaseEntity
        {
            try
            {
                // Add a new document
                await _documentService.Create(newDocument);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                // Check if document exists
                Document document = await _documentService.GetById(newDocument.Id);
                if (document != null)
                {
                    // Return document already exists error
                    return Conflict();
                }
                else
                {
                    // Return error message
                    return BadRequest(e.Message);
                }
            }
            return CreatedAtAction(nameof(GetDocument), new { id = newDocument.Id }, newDocument);
        }
        #endregion
    }
}
