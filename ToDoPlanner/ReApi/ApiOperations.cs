using System;
using System.Net;
using Newtonsoft.Json;
using ToDoPlanner.Model;

namespace ToDoPlanner.Operations
{
    class ApiOperations
    {
        /**
         * Base Url @string
         */

        private string baseUrl;

        public ApiOperations()
        {
            this.baseUrl = "https://todo.re.heisch.ch/api";
        }

        /**
         * Authenticate user with Web Api Endpoint
         * @param string username
         * @param string password
         */
        //private string _username = "regula";

        //private string _password = "fritzli-hansli-greteli";

        public ToDoTask Authenticate(string username, string password)
        {
            string endpoint = this.baseUrl + "/login_check";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password
            });

            System.Diagnostics.Trace.WriteLine("Json " + json);
            
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<ToDoTask>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ToDoTask GetTaskDetails(ToDoTask task)
        {
            string endpoint = this.baseUrl + "tasks/2"; // later "tasks" + task.Id
            string access_token = task.access_token;
            
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = access_token;

            try
            {
                string response = wc.DownloadString(endpoint);
                task = JsonConvert.DeserializeObject<ToDoTask>(response);
                task.access_token = access_token;
                System.Diagnostics.Trace.WriteLine(task);
                return task;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    
}