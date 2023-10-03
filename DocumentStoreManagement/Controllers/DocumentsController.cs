using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocument _document;

        public DocumentsController(IDocument document)
        {
            _document = document;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<IEnumerable<Document>> GetDocuments()
        {
            return await _document.GetAll();
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDocument(Document document)
        {
            await _document.AddNew(document);
            return CreatedAtAction(nameof(GetDocuments), new { id = document.Id }, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            Document document = await _document.GetById(id);
            if (document is null)
            {
                return NotFound();
            }
            await _document.Delete(document);
            return NoContent();
        }
    }
}
