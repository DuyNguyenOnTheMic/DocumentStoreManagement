using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<IEnumerable<Document>> GetDocuments()
        {
            return await _documentService.GetAll();
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDocument(Document document)
        {
            await _documentService.AddNew(document);
            return CreatedAtAction(nameof(GetDocuments), new { id = document.Id }, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            Document document = await _documentService.GetById(id);
            if (document is null)
            {
                return NotFound();
            }
            await _documentService.Delete(document);
            return NoContent();
        }
    }
}
