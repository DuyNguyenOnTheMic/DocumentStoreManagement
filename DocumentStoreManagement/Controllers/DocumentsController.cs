using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DocumentStoreManagement.Controllers
{
    /// <summary>
    /// Document Management API Controller - MongoDB database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        /// <summary>
        /// Add dependencies to controller
        /// </summary>
        /// <param name="documentService"></param>
        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <summary>
        /// Gets the document list from database
        /// </summary>
        /// <returns>A list of all documents</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/documents
        ///
        /// </remarks>
        [HttpGet]
        public async Task<IEnumerable<Document>> GetDocuments()
        {
            // Get list of documents
            return await _documentService.GetAll();
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
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Document>> GetDocument(string id)
        {
            // Get document by id
            var document = await _documentService.GetById(id);
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
        ///         "releaseQuantity": 12,
        ///         "book": {
        ///           "authorName": "J. K. Rowling",
        ///           "pageNumber": 218
        ///         },
        ///         "magazine": {
        ///           "releaseNumber": 200,
        ///           "releaseMonth": "07/2023"
        ///         },
        ///         "newspaper": {
        ///           "releaseDate": "2023-10-04T08:44:20.351Z"
        ///         }
        ///     }
        ///
        /// ***NOTES***: Either book, magazine or newspaper is updated, the others will have null values.
        ///
        /// </remarks>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> PutDocument(string id, [FromBody] Document updatedDocument)
        {
            // Return bad request if ids don't match
            if (id != updatedDocument.Id)
            {
                return BadRequest();
            }

            // Check if document exists
            var document = await _documentService.GetById(id);
            if (document == null)
            {
                return NotFound();
            }

            // Update document
            await _documentService.Update(updatedDocument);

            return NoContent();
        }

        /// <summary>
        /// Creates a document
        /// </summary>
        /// <param name="newDocument"></param>
        /// <returns>A newly created document</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/documents
        ///     {
        ///         "publisherName": "Example Name",
        ///         "releaseQuantity": 12,
        ///         "book": {
        ///           "authorName": "J. K. Rowling",
        ///           "pageNumber": 218
        ///         },
        ///         "magazine": {
        ///           "releaseNumber": 200,
        ///           "releaseMonth": "07/2023"
        ///         },
        ///         "newspaper": {
        ///           "releaseDate": "2023-10-04T08:44:20.351Z"
        ///         }
        ///     }
        ///
        /// ***NOTES***: Either book, magazine or newspaper is added to this document object, the others will have null values.
        ///
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> PostDocument([FromBody] Document newDocument)
        {
            try
            {
                // Add a new document
                await _documentService.Create(newDocument);
            }
            catch (MongoWriteException)
            {
                // Check if document exists
                var document = await _documentService.GetById(newDocument.Id);
                if (document != null)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetDocuments), new { id = newDocument.Id }, newDocument);
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
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteDocument(string id)
        {
            // Get document by id
            var document = await _documentService.GetById(id);
            if (document == null)
            {
                return NotFound();
            }

            // Delete document
            await _documentService.Delete(id);

            return NoContent();
        }
    }
}
