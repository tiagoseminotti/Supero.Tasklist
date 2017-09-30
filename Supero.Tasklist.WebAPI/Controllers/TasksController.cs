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
    /// <summary>
    /// Controller for the Task entity
    /// </summary>
    public class TasksController : ApiController
    {
        private SuperoTasklistWebAPIContext db = new SuperoTasklistWebAPIContext();

        // GET: api/Tasks
        /// <summary>
        /// Gets all the tasks
        /// </summary>
        /// <returns></returns>
        public IQueryable<Supero.Tasklist.Models.Task> GetTask()
        {
            return db.Task.Where(t => t.ST_REMOVED != true);
        }

        // GET: api/Tasks/5
        /// <summary>
        /// Finds a task by the Id informed
        /// </summary>
        /// <param name="pId">Task Id</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Models.Task))]
        public async Task<IHttpActionResult> GetTask(Guid pId)
        {
            Models.Task task = await db.Task.FindAsync(pId);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        /// <summary>
        /// Updates an existing task
        /// </summary>
        /// <param name="pId">Task Id</param>
        /// <param name="pTask">Task data</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTask(Guid pId, Models.Task pTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pId != pTask.CD_TASK)
            {
                return BadRequest();
            }

            //When any data is updated, the last change date has to be updated
            pTask.DT_LAST_CHANGE = DateTime.UtcNow;

            db.Entry(pTask).State = EntityState.Modified;
            
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(pId))
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
        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="pTask">Task data</param>
        /// <returns></returns>
        [ResponseType(typeof(Models.Task))]
        public async Task<IHttpActionResult> PostTask(Models.Task pTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Creates a new Id
            pTask.CD_TASK = Guid.NewGuid();
            //Sets creation date
            pTask.DT_CREATION = DateTime.UtcNow;
            //A task should never be created with finished or removed as true
            pTask.ST_FINISHED = false;
            pTask.ST_REMOVED = false;

            db.Task.Add(pTask);

            try
            {
                //Saves
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaskExists(pTask.CD_TASK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pTask.CD_TASK }, pTask);
        }

        // DELETE: api/Tasks/5
        /// <summary>
        /// Deletes a task
        /// </summary>
        /// <param name="pId">Task Id</param>
        /// <returns></returns>
        [ResponseType(typeof(Models.Task))]
        public async Task<IHttpActionResult> DeleteTask(Guid pId)
        {
            Models.Task task = await db.Task.FindAsync(pId);
            if (task == null)
            {
                return NotFound();
            }

            //db.Task.Remove(task);
            //Sets the task as removed
            task.ST_REMOVED = true;

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