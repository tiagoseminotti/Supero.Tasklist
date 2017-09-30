using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Supero.Tasklist.WebApp.Controllers
{
    /// <summary>
    /// Controller that access the API published
    /// </summary>
    public class TaskController : Controller
    {
        //WebAPI URL
        private string _serviceURL = WebConfigurationManager.AppSettings.Get("superoTaskAPIURL");

        /// <summary>
        /// GET: Returns all the tasks
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            List<Models.Task> TaskInfo = new List<Models.Task>();

            using (var client = new HttpClient())
            {
                //Uses API URL
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Clear();

                //Defines request data format
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Executes an assynchronous GET to receive all tasks
                HttpResponseMessage httpResponse = await client.GetAsync("api/Tasks");

                //Checks if the request executed successfully
                if (httpResponse.IsSuccessStatusCode)
                {
                    //Reads the content as a string
                    var taskResponse = httpResponse.Content.ReadAsStringAsync().Result;

                    //Deserializes the response
                    TaskInfo = JsonConvert.DeserializeObject<List<Models.Task>>(taskResponse);
                }
            }

            return View(TaskInfo);
        }

        /// <summary>
        /// GET: Shows the creation view
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View(new Models.Task());
        }
        
        /// <summary>
        /// POST: Sends information for the creation of a new user
        /// </summary>
        /// <param name="pTask">Model with task data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(Models.Task pTask)
        {
            using (var client = new HttpClient())
            {
                //Uses API URL
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage responseMessage = await client.PostAsJsonAsync("api/Tasks", pTask);
                //Checks for success
                if (responseMessage.IsSuccessStatusCode)
                {
                    //Returns to the listing view
                    return RedirectToAction("Index");
                }
            }
            //Error
            return RedirectToAction("Error");
        }

        /// <summary>
        /// GET: Returns data from the task to be edited
        /// </summary>
        /// <param name="pId">Task Id</param>
        /// <returns></returns>
        public async Task<ActionResult> Edit(Guid pId)
        {
            using (var client = new HttpClient())
            {
                //Uses API URL
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage responseMessage = await client.GetAsync("/api/tasks/" + pId);
                //Checks for success
                if (responseMessage.IsSuccessStatusCode)
                {
                    //Reads content
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    //Deserializes response
                    var task = JsonConvert.DeserializeObject<Models.Task>(responseData);
                    //Returns task data 
                    return View(task);
                }
            }
            return View("Error");
        }

        /// <summary>
        /// PUT: Updates task data
        /// </summary>
        /// <param name="pId">Task ID</param>
        /// <param name="pTask">Task model data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(Guid pId, Models.Task pTask)
        {
            using (var client = new HttpClient())
            {
                //Uses API URL
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage responseMessage = await client.PutAsJsonAsync("/api/tasks/" + pId, pTask);
                //Checks for success
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Error");
        }

        /// <summary>
        /// Returns data from the task to be deleted
        /// </summary>
        /// <param name="pId">Task id</param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(Guid pId)
        {
            using (var client = new HttpClient())
            {
                //Uses API URL
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage responseMessage = await client.GetAsync("/api/tasks/" + pId);
                //Checks for success
                if (responseMessage.IsSuccessStatusCode)
                {
                    //Reads response content
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    //Deserializes data
                    var task = JsonConvert.DeserializeObject<Models.Task>(responseData);
                    //Returns task data
                    return View(task);
                }
            }

            return View("Error");
        }

        /// <summary>
        /// Deletes a task
        /// </summary>
        /// <param name="pId">Task Id</param>
        /// <param name="pTask">Task data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid pId, Models.Task pTask)
        {
            using (var client = new HttpClient())
            {
                //Uses API URL
                client.BaseAddress = new Uri(_serviceURL);
                client.DefaultRequestHeaders.Clear();

                //Posts data and grabs response
                HttpResponseMessage responseMessage = await client.DeleteAsync("/api/tasks/" + pId);
                //Checks for success
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Error");
        }
    }
}