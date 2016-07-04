using System;
using Windows.Data.Json;
using Windows.Web.Http.Filters;

namespace BackgroundSwitcher.Model
{
    public static class RedditParser
    {
        private static bool exist = false;
        private static JsonObject jo = null;
        public static bool subredditExist(string subreddit)
        {
            parseJSON("https://www.reddit.com/r/" + subreddit + ".json?limit=1");
            // TODO: Implementation
            return exist;
        }

        private static void refresh()
        {
            exist = false;
            jo = null;
        }

        private static async void parseJSON(string url)
        {
            refresh();
            var filter = new HttpBaseProtocolFilter();
            filter.AllowAutoRedirect = false;
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient(filter);

            Uri requestUri = new Uri(url);

            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";
            exist = true;

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                JsonValue jv = JsonValue.Parse(httpResponseBody);
                jo = jv.GetObject();
            }
            catch (Exception ex)
            {
                // Url does not exist
                exist = false;
            }
        }
    }
}
