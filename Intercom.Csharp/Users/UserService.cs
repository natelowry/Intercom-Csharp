﻿using System;
using System.Net;

namespace Intercom.Csharp.Users
{
    public class UserService : RestRequesting
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="username">The API username to use when accessing the API</param>
        /// <param name="password">The API password to use when accessing the API</param>
        public UserService(string username, string password)
            : base(username, password)
        { }

        /// <summary>
        /// Retrieves a paginated list of all users in your application on Intercom.
        /// </summary>
        /// <returns>The paginated object if successful, null otherwise.</returns>
        public UserViewModel<T> All<T>() where T : class, new()
        {
            return GetRequest<UserViewModel<T>>("/users");
        }

        /// <summary>
        /// Retrieves a paginated list of all users in your application on Intercom.
        /// </summary>
        /// <param name="page">Optional, defaults to 1.</param>
        /// <param name="perPage">Optional, defaults to 500 (max of 500)</param>
        /// <returns>The paginated object if successful, null otherwise.</returns>
        public UserViewModel<T> All<T>(int? page, int? perPage) where T : class, new()
        {
            if (page.HasValue && perPage.HasValue)
            {
                return GetRequest<UserViewModel<T>>(String.Format("/users?page={0}&per_page={1}", page.Value, perPage.Value));
            }
            else if (page.HasValue)
            {
                return GetRequest<UserViewModel<T>>(String.Format("/users?page={0}", page.Value));
            }
            else if (perPage.HasValue)
            {
                return GetRequest<UserViewModel<T>>(String.Format("/users?per_page={0}", perPage.Value));
            }
            else
            {
                return All<T>();
            }
        }

        /// <summary>
        /// Retrieves a user. Expects either the email or user_id you used to create the user.
        /// </summary>
        /// <param name="userId">The user_id used to create the user.</param>
        /// <returns>The found user if successful, null otherwise.</returns>
        public User<T> Single<T>(int userId) where T : class, new()
        {
            return GetRequest<User<T>>(String.Format("/users?user_id={0}", userId));
        }

        /// <summary>
        /// Retrieves a user. Expects either the email or user_id you used to create the user.
        /// </summary>
        /// <param name="emailAddress">The email used to create the user.</param>
        /// <returns>The found user if successful, null otherwise.</returns>
        public User<T> Single<T>(string emailAddress) where T : class, new()
        {
            return GetRequest<User<T>>(String.Format("/users?email={0}", WebUtility.UrlEncode(emailAddress)));
        }

        /// <summary>
        /// Creates a user. Expects a JSON object describing the user.
        /// </summary>
        /// <remarks>Social and geo location data is fetched asynchronously, so a secondary call to users will be required to fetch it.</remarks>
        /// <param name="user">The user to create</param>
        /// <returns>The user object if success, null otherwise.</returns>
        public User<T> Create<T>(User<T> user) where T : class, new()
        {
            return PostRequest<User<T>, User<T>>(user, "/users");
        }

        /// <summary>
        /// Deletes a user. Expects the email of the user
        /// </summary>
        /// <param name="emailAddress">The email used to delete the user.</param>
        public void Delete(string emailAddress)
        {
            DeleteRequest(String.Format("/users?email={0}", WebUtility.UrlEncode(emailAddress)));
        }

        /// <summary>
        /// Deletes a user. Expects the user_id of the user
        /// </summary>
        /// <param name="userId">The user_id used to delete the user.</param>
        public void Delete(int userId)
        {
            DeleteRequest(String.Format("/users?user_id={0}", userId));
        }

        /// <summary>
        /// Updates a user. Expects a JSON object describing the user.
        /// </summary>
        /// <param name="user">The new user data to update</param>
        /// <returns>The updated user object if success, null otherwise.</returns>
        public User<T> Update<T>(User<T> user) where T : class, new()
        {
            return PostRequest<User<T>, User<T>>(user, "/users");
        }
    }
}

