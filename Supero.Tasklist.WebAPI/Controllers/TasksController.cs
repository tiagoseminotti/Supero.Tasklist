using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Supero.Tasklist.Contexts;

namespace Supero.Tasklist.WebAPI.Controllers
{
    public class TasksController : ApiController
    {
        private SuperoTasklistWebAPIContext db = new SuperoTasklistWebAPIContext();

        // GET: api/Tasks
        public IQueryable<Models.Task> GetTask()
        {
            return db.Task;
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Models.Task))]
        public async Task<IHttpActionResult> GetTask(Guid id)
        {
            Models.Task task = await db.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTask(Guid id, Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.CD_TASK)
            {
                return BadRequest();
            }

            db.Entry(task).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Models.Task))]
        public async Task<IHttpActionResult> PostTask(Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Task.Add(task);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaskExists(task.CD_TASK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = task.CD_TASK }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Models.Task))]
        public async Task<IHttpActionResult> DeleteTask(Guid id)
        {
            Models.Task task = await db.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            db.Task.Remove(task);
            await db.SaveChangesAsync();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(Guid id)
        {
            return db.Task.Count(e => e.CD_TASK == id) > 0;
        }
    }
}