using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace Supero.Tasklist.WebApp.Controllers
{
    public class TaskServiceController : Controller
    {
        //WebAPI URL
        private string _serviceURL = "localhost";

        // GET: Task
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
    }
}