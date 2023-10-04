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

        // GET: api/Documents
        /// <summary>
        /// Get the document list from database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Document>> GetDocuments()
        {
            return await _mediator.Send(new GetDocumentListQuery());
        }

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

        // PUT: api/Documents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> PutDocument(string id, Document updatedDocument)
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

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDocument(Document document)
        {
            await _mediator.Send(new CreateDocumentCommand(
                            document.PublisherName,
                            document.ReleaseQuantity,
                            document.Book,
                            document.Magazine,
                            document.Newspaper));
            return CreatedAtAction(nameof(GetDocuments), new { id = document.Id }, document);
        }

        // DELETE: api/Documents/5
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
