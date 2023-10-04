using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStoreManagement.Controllers
{
    /// <summary>
    /// Document Management API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Add dependencies to controller
        /// </summary>
        /// <param name="mediator"></param>
        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator;
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
            return await _mediator.Send(new GetDocumentListQuery());
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
            var document = await _mediator.Send(new GetDocumentByIdQuery() { Id = id });

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
            var document = await _mediator.Send(new GetDocumentByIdQuery() { Id = id });
            if (document is null)
            {
                return NotFound();
            }
            await _mediator.Send(new UpdateDocumentCommand(
                updatedDocument.Id,
                updatedDocument.PublisherName,
                updatedDocument.ReleaseQuantity,
                updatedDocument.Book,
                updatedDocument.Magazine,
                updatedDocument.Newspaper));
            return NoContent();
        }

        /// <summary>
        /// Creates a document
        /// </summary>
        /// <param name="document"></param>
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
        public async Task<IActionResult> PostDocument([FromBody] Document document)
        {
            await _mediator.Send(new CreateDocumentCommand(
                            document.PublisherName,
                            document.ReleaseQuantity,
                            document.Book,
                            document.Magazine,
                            document.Newspaper));
            return CreatedAtAction(nameof(GetDocuments), new { id = document.Id }, document);
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
            var document = await _mediator.Send(new GetDocumentByIdQuery() { Id = id });
            if (document == null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteDocumentCommand() { Id = id });

            return NoContent();
        }
    }
}
