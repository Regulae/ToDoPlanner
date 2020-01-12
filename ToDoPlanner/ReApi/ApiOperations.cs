using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Web.UI.WebControls;
using MaterialDesignThemes.Wpf.Converters;
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
         * @param string username = "regula"
         * @param string password = "fritzli-hansli-greteli"
         */
        public TokenResponse Authenticate(string username, string password)
        {
            string endpoint = this.baseUrl + "/login_check";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password
            });

            // Necessary for working with windows 7
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<TokenResponse>(response);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Exception: " + ex);
                return null;
            }
        }

        /*
         * Get TodDoTasks from API
         * @param TokenResponse tokenResponse
         */
        public ObservableCollection<ToDoTask> GetTasks(TokenResponse tokenResponse)
        {
            string endpoint = this.baseUrl + "/tasks.json";
            string accessToken = tokenResponse.token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + accessToken;

            ObservableCollection<ToDoTask> toDoTasks;

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

        /*
         * Add Task to Database via API
         * @param ToDoTask newTask
         * @param TokenResponse tokenResponse
         */
        public ToDoTask PostTask(ToDoTask newTask, TokenResponse tokenResponse)
        {
            string endpoint = this.baseUrl + "/tasks";
            string method = "POST";
            string json = JsonConvert.SerializeObject(
                newTask,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );
            System.Diagnostics.Trace.WriteLine("JSON: " + json);

            string accessToken = tokenResponse.token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + accessToken;

            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<ToDoTask>(response);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Exception: " + ex);
                return null;
            }
        }
    }
}