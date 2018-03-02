using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DelayedPoster.Code.Authentification;
using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Web;
using Newtonsoft.Json;
using TwitterDemo.Code;
using TwitterDemo.Code.WebWorkers;

namespace DelayedPoster.Code.WebWorkers
{
    public class TwitterWebWorker:WebWorker
    {
        /// <summary>
        /// Credentials that will be used to make requests to twitter api
        /// </summary>
        public TwitterAuthCredentials TwitterCredentials { get; set; }

        private OAuthCredentials _oauthCredentials;
        public TwitterWebWorker(TwitterAuthCredentials _authCredentials)
        {
            TwitterCredentials = _authCredentials;
        }
        public async Task<string> GetProgress(string mediaID)
        {
            var statusResponse = await SendSTATUS( mediaID);
            if (statusResponse.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<dynamic>(statusResponse.Content)["processing_info"][
                    "progress_percent"].ToString();
            }
            var code = (WebExceptionStatus) ((int) statusResponse.StatusCode);
            throw new WebException(statusResponse.StatusDescription, code);
        }
        public async Task<string> UploadAndGetMediaID(string pathToFile)
        {
            var initResponse = await SendINIT(pathToFile);
            var id = JsonConvert.DeserializeObject<dynamic>(initResponse.Content)["media_id"].ToString();
            var appendResponse = await SendAPPEND(pathToFile, id);
            RestResponse finalizeResponse = await SendFINALIZE(id);
            return JsonConvert.DeserializeObject<dynamic>(finalizeResponse.Content)["media_id"].ToString();
        }
        //TODO:Refactor ,disgusting to read
        public async void Post(string status,List<Attachement> attachements)
        {
            if (!TwitterCredentials.LoggedIn)
                throw new Exception("You have to be logged in to be able to post to your feed");
            if (attachements.Count> 4)
                throw new ArgumentException("can`t add more than 4 pics to one post");

            _oauthCredentials = CreateOAuthCredentials(TwitterCredentials);
            var restClient = PrepareClient("https://api.twitter.com", "1.1");
            var media_ids = new List<string>();
            foreach (var attachement in attachements)
            {
                media_ids.Add(await UploadAndGetMediaID(attachement.File.FullName));
            }
            var request = PrepareRequest(_oauthCredentials, "statuses/update.json", WebMethod.Post);
            var ids = String.Join(",", media_ids);
            if(media_ids.Count>0)
            { request.AddParameter("media_ids", ids);}
            request.AddParameter("status", status);
            var response = await MakeRequest(restClient, request);//for debugging purposes only
        }
        #region Commands
        private async Task<RestResponse> SendAPPEND(string path, string mediaId)
        {
            var restClient = PrepareClient("https://upload.twitter.com", "1.1");
            var appendRequest = PrepareRequest(_oauthCredentials, "media/upload.json", WebMethod.Post);
            appendRequest.AddParameter("command", "APPEND");
            appendRequest.AddParameter("segment_index", "0");
            appendRequest.AddParameter("media_id", mediaId);
            appendRequest.AddFile("media", path, new FileStream(path, FileMode.Open));
            var response=await MakeRequest(restClient, appendRequest);//for debugging purposes only
            return response;
        }
        private async Task<RestResponse> SendINIT(string path)
        {
            var mediaType = GetMediaType(path);
            var client = PrepareClient("https://upload.twitter.com", "1.1");
            var request = PrepareRequest(_oauthCredentials, "media/upload.json",
                WebMethod.Post);
            request.AddParameter("media_type", mediaType);
            request.AddParameter("command", "INIT");
            request.AddParameter("total_bytes", GetBytesCount(path).ToString());
            var response=await MakeRequest(client, request);//for debugging purposes only
            return response;
        }
        private async Task<RestResponse> SendFINALIZE(string mediaID)
        {
            var restClient = PrepareClient("https://upload.twitter.com", "1.1");
            var finalizeRequest = PrepareRequest(_oauthCredentials, "media/upload.json",
                WebMethod.Post);
            finalizeRequest.AddParameter("command", "FINALIZE");
            finalizeRequest.AddParameter("media_id", mediaID);
            var response=await MakeRequest(restClient, finalizeRequest);//for debugging purposes only
            return response;
        }
        private async Task<RestResponse> SendSTATUS(string mediaID)
        {
            var client = PrepareClient("https://upload.twitter.com", "1.1");
            var request = PrepareRequest(_oauthCredentials, "media/upload.json",
                WebMethod.Post);
            request.AddParameter("command", "STATUS");
            request.AddParameter("media_id", mediaID);
            var response=await MakeRequest(client, request);//for debugging purposes only
            return response;
        }
        #endregion
        #region Helper Methods
        private static string GetMediaType(string path)
        {
            string mediaType = string.Empty;
            var fileExtension = path.Split('.').Reverse().ToArray()[0];
            if (fileExtension.IsOneOf("png", "jpg", "webp", "gif"))
                mediaType = $"image/{fileExtension}";
            else if (fileExtension == "mp4")
                mediaType = $"video/{fileExtension}";
            else
                throw new ArgumentException(nameof(path));
            return mediaType;
        }
        private static int GetBytesCount(string path)
        {
            return File.ReadAllBytes(path).Length;
        }
        private  OAuthCredentials CreateOAuthCredentials(TwitterAuthCredentials creds)
        {
            return new OAuthCredentials
            {
                Token = creds.Token,
                TokenSecret = creds.TokenSecret,
                ConsumerKey = creds.ConsumerKey,
                ConsumerSecret = creds.ConsumerSecret
            };
        }
        #endregion
    }
}
