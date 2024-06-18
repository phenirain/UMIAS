using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UMAISAPI.Models;

namespace UMAISAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyseDocumentsController : ControllerBase
    {
        private readonly UmiasContext _context;

        public AnalyseDocumentsController(UmiasContext context)
        {
            _context = context;
        }

        // GET: api/AnalyseDocuments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnalyseDocument>>> GetAnalyseDocuments()
        {
            return await _context.AnalyseDocuments.ToListAsync();
        }

        // GET: api/AnalyseDocuments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnalyseDocument>> GetAnalyseDocument(int? id)
        {
            var analyseDocument = await _context.AnalyseDocuments.FindAsync(id);

            if (analyseDocument == null)
            {
                return NotFound();
            }

            return analyseDocument;
        }

        // PUT: api/AnalyseDocuments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnalyseDocument(int? id, AnalyseDocument analyseDocument)
        {
            if (id != analyseDocument.IdAppointment)
            {
                return BadRequest();
            }

            _context.Entry(analyseDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalyseDocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AnalyseDocuments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnalyseDocument>> PostAnalyseDocument(AnalyseDocument analyseDocument)
        {
            _context.AnalyseDocuments.Add(analyseDocument);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AnalyseDocumentExists(analyseDocument.IdAppointment))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAnalyseDocument", new { id = analyseDocument.IdAppointment }, analyseDocument);
        }

        // DELETE: api/AnalyseDocuments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnalyseDocument(int? id)
        {
            var analyseDocument = await _context.AnalyseDocuments.FindAsync(id);
            if (analyseDocument == null)
            {
                return NotFound();
            }

            _context.AnalyseDocuments.Remove(analyseDocument);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnalyseDocumentExists(int? id)
        {
            return _context.AnalyseDocuments.Any(e => e.IdAppointment == id);
        }
    }
}
