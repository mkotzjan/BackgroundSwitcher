using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http.Filters;

namespace BackgroundSwitcher.Model
{
    public static class RedditParser
    {
        private static bool exist = false;
        private static RootObject result = null;

        public static bool subredditExist(string subreddit)
        {
            parseJSON("https://www.reddit.com/r/" + subreddit + ".json?limit=1");
            // TODO: Implementation
            return exist;
        }

        public static List<Subreddit> getSubredditsByName(string querry)
        {
            List<Subreddit> subreddits = new List<Subreddit>();
            parseJSON("https://www.reddit.com/subreddits/search.json?q=" + querry + "&limit=5");

            List<Child> children = result.data.children;
            foreach (Child child in children)
            {
                subreddits.Add(new Subreddit(child.data.display_name));
            }

            return subreddits;
        }

        private static void refresh()
        {
            exist = false;
            result = null;
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
                result = JsonConvert.DeserializeObject<RootObject>(httpResponseBody);
            }
            catch (Exception ex)
            {
                // Url does not exist
                exist = false;
            }
        }
    }
}
