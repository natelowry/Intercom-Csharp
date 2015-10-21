using System;
using System.Collections.Generic;
using RestSharp;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Intercom.Csharp.Configuration;
using RestSharp.Authenticators;
using RestSharp.Serializers;

namespace Intercom.Csharp
{

    /// <summary>
    /// Basic operations to execute rest requests
    /// </summary>
    public class RestRequesting
    {
        #region Properties and Accessors
        protected string AuthUsername { get; private set; }
        protected string AuthPassword { get; private set; }

        private static string userAgent;
        private static string UserAgent
        {
            get
            {
                if (userAgent == null)
                {
                    userAgent = String.Format("Intercom .NET Client v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
                }
                return userAgent;
            }
        }
        #endregion

        #region Constructor
        protected RestRequesting(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username is required. Details can be found at http://docs.intercom.io/api#authentication", "username");

            this.AuthUsername = username;
            this.AuthPassword = password;
        }
        #endregion

        protected T GetRequest<T>(string path) where T : new()
        {
            RestClient client = new RestClient(Config.ApiBaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(this.AuthUsername, this.AuthPassword);
            client.UserAgent = RestRequesting.UserAgent;

            RestRequest request = new RestRequest(path);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute<T>(request);

            HandleBadResponse(response.Content, response.StatusCode);

            return response.Data;
        }

        private void HandleBadResponse(string content, System.Net.HttpStatusCode code)
        {
            if (!(code == HttpStatusCode.Accepted || code == HttpStatusCode.Created || code == HttpStatusCode.OK))
                throw new IntercomException(String.Format("{0}: {1}", code, content));
        }

        protected string GetRequest(string path)
        {
            RestClient client = new RestClient(Config.ApiBaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(this.AuthUsername, this.AuthPassword);
            client.UserAgent = RestRequesting.UserAgent;

            RestRequest request = new RestRequest(path);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);

            HandleBadResponse(response.Content, response.StatusCode);

            return response.Content;
        }

        protected TOutput PutRequest<TOutput, TInput>(TInput obj, string path) where TOutput : new()
        {
            return Request<TOutput, TInput>(Method.PUT, obj, path);
        }

        protected TOutput PostRequest<TOutput, TInput>(TInput obj, string path) where TOutput : new()
        {
            return Request<TOutput, TInput>(Method.POST, obj, path);
        }

        protected bool PostRequest<TInput>(TInput obj, string path) 
        {
            return Request(Method.POST, obj, path);
        }

        protected void DeleteRequest(string path)
        {
            Request<List<object>, object>(Method.DELETE, null, path);
        }

        private bool Request<TInput>(Method method, TInput obj, string path) 
        {
            RestClient client = new RestClient(Config.ApiBaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(this.AuthUsername, this.AuthPassword);
            client.UserAgent = RestRequesting.UserAgent;

            RestRequest request = new RestRequest(path, method);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();

            if (obj != null)
            {
                request.AddBody(obj);
            }

            var response = client.Execute(request);

            HandleBadResponse(response.Content, response.StatusCode);

            return true;
        }

        private TOutput Request<TOutput, TInput>(Method method, TInput obj, string path) where TOutput : new()
        {
            RestClient client = new RestClient(Config.ApiBaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(this.AuthUsername, this.AuthPassword);
            client.UserAgent = RestRequesting.UserAgent;

            RestRequest request = new RestRequest(path, method);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();

            if (obj != null)
            {
                request.AddBody(obj);
            }
            var response = client.Execute<TOutput>(request);

            HandleBadResponse(response.Content, response.StatusCode);

            return response.Data;
        }
    }
}
