///------------------------------------------------------------------------
/// Namespace:    ToDoPlanner.ReApi
/// Class:        ApiOperations
/// Description:  This class connects the Applicaiton with the database
///               and handles the communication between the app and the
///               api.
/// Author:       Kevin Kessler & Regula Engelhardt
/// Copyright:    (c) Kevin Kessler & Regula Engelhardt
///------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using ToDoPlanner.Model;
using System.Windows;

namespace ToDoPlanner.ReApi
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

        /// <summary>
        /// Authenticate to use database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>TokenResponse</returns>
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
                // return empty string, to avoid NullReferenceExceptions
                return new TokenResponse() { token = "" };
            }
        }

        /// <summary>
        /// Load tasks from database
        /// </summary>
        /// <param name="tokenResponse"></param>
        /// <returns>ObservableCollection<ToDoTask></returns>
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
            catch (WebException ex)
            {
                System.Diagnostics.Trace.WriteLine("Exception: " + ex);

                // Inform user before shuting down application, because of missing internet connection
                MessageBox.Show("There is a problem with the database connection." +
                    "\nCheck your internet connection." +
                    "\nThe application will be closed.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);

                Application.Current.Shutdown();

                return new ObservableCollection<ToDoTask>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Exception: " + ex);
                return new ObservableCollection<ToDoTask>();
            }
        }

        /// <summary>
        /// Post task to database
        /// </summary>
        /// <param name="newTask"></param>
        /// <param name="tokenResponse"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete task from database
        /// </summary>
        /// <param name="task">The task which has to be deleted</param>
        /// <param name="tokenResponse">Database token response</param>
        /// <returns>ToDoTask</returns>
        public ToDoTask DeleteTask(ToDoTask task, TokenResponse tokenResponse)
        {
            int? taskId = task.Id;
            string endpoint = this.baseUrl + "/tasks/" + taskId;
            string method = "DELETE";
            string json = JsonConvert.SerializeObject(
                task,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            string accessToken = tokenResponse.token;

            WebClient wc = new WebClient();
            wc.Headers["Authorization"] = "Bearer " + accessToken;

            try
            {
                string response =
                    Encoding.ASCII.GetString(wc.UploadValues(endpoint, method, new NameValueCollection()));
                return JsonConvert.DeserializeObject<ToDoTask>(response);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Exception: " + ex);
                return null;
            }
        }

        /// <summary>
        /// Update task on database
        /// </summary>
        /// <param name="task"></param>
        /// <param name="tokenResponse"></param>
        /// <returns>ToDoTask</returns>
        public ToDoTask UpdateTask(ToDoTask task, TokenResponse tokenResponse)
        {
            int? taskId = task.Id;
            string endpoint = this.baseUrl + "/tasks/" + taskId;
            string method = "PUT";
            string json = JsonConvert.SerializeObject(
                task,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

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