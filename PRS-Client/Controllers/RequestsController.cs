using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRS_Client.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace PRS_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase {
        private readonly PRSDbContext _context;

        public RequestsController(PRSDbContext context) {
            _context = context;
        }
        //Method returns requests to be reviewed that are not linked to their user id
       // GET: api/Requests/id
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> NeedsToReview(int id) {
            
            var request = _context.Requests.Where(r => r.Status == "REVIEW" && r.UserId != id );
            return await request.ToListAsync();

        }



        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests() {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id) {
            var request = await _context.Requests.FindAsync(id);

            if (request == null) {
                return NotFound();
            }

            return request;
        }


        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests/(id)/Review
        [HttpPost("{id}/Review")]
        public async Task<ActionResult<Request>> SetStatusToReview(int id) {
            var request = await _context.Requests.FindAsync(id);
            if (request == null) {
                return NotFound();
            }
            request.Status = "REVIEW";
            _context.Requests.Update(request);
            _context.SaveChanges();
            return request;
        }
        // POST: api/Requests/(id)/Approve
        [HttpPost("{id}/Approve")]
        public async Task<ActionResult<Request>> SetStatusToApproved(int id) {
            var request = await _context.Requests.FindAsync(id);
            if (request == null) {
                return NotFound();
            }
            request.Status = "APPROVED";
            _context.Requests.Update(request);
            _context.SaveChanges();
            return request;
        }
        // POST: api/Requests/(id)/Reject
        [HttpPost("{id}/Reject")]
        public async Task<ActionResult<Request>> SetStatusToRejected(int id) {
            var request = await _context.Requests.FindAsync(id);
            if (request == null) {
                return NotFound();
            }
            request.Status = "REJECTED";
            _context.Requests.Update(request);
            _context.SaveChanges();
            return request;
        }

        // POST: api/Requests
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            calcTotal(request.Id);

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }

        public decimal calcTotal(int id) {
            var dbRequest = _context.Requests.Find(id);
            
            dbRequest.Total = _context.RequestLines.Where(r => r.RequestId.Equals(id))
                .Sum(rl => rl.Product.Price * rl.Quantity);
            _context.SaveChanges();
            return dbRequest.Total;
           
        }


    }
}
