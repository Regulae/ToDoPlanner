using System;
using System.Collections.ObjectModel;
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

        public TokenResponse Authenticate(string username, string password)
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
                return JsonConvert.DeserializeObject<TokenResponse>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Get tasks from api
        public ObservableCollection<ToDoTask> GetTasks(TokenResponse tokenResponse)
        {
            string endpoint = this.baseUrl + "/tasks.json";
            string access_token = tokenResponse.token;
            
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;
            
            ObservableCollection<ToDoTask> toDoTasks = new ObservableCollection<ToDoTask>();

            try
            {
                string response = wc.DownloadString(endpoint);
                toDoTasks = JsonConvert.DeserializeObject<ObservableCollection<ToDoTask>>(response);
                return toDoTasks;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Exception: " + ex);
                return null;
            }
        }
    }
    
}