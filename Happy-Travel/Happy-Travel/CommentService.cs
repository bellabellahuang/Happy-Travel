﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using Android.Net;
using Android.Content;

namespace HappyTravel
{
    public class CommentService
    {
        // url for GET request
        private const string GET_COMMENTS = "https://my-json-server.typicode.com/bellabellahuang/jsonDB/comments";

        // get the comment list
        public async Task<List<Comment>> GetCommentListAsync()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.GetAsync(GET_COMMENTS);

            if (response != null || response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.Out.WriteLine("Response Body: \r\n {0}", content);

                content = "{\"comments\":" + content + "}";
                var commentListData = new List<Comment>();
                //convert the complete JSON response string to JObject
                JObject jsonResponse = JObject.Parse(content);
                //fetch the values from the JObject and convert them to a list
                IList<JToken> results = jsonResponse["comments"].ToList();
                //convert each JToken in the list to a PieceOfArt object and add it to the poa list
                foreach (JToken token in results)
                {
                    Comment comment = token.ToObject<Comment>();
                    commentListData.Add(comment);
                }
                return commentListData;
            }
            else
            {
                Console.Out.WriteLine("Failed to fetch data. Try again later!");
                return null;
            }
        }

        // check if the network is connected succefully
        public bool isConnected(Context activity)
        {
            var connectivityManager = (ConnectivityManager)activity.GetSystemService(Context.ConnectivityService);
            var activeConnection = connectivityManager.ActiveNetworkInfo;
            return (null != activeConnection && activeConnection.IsConnected);
        }
    }
}
