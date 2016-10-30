using System;
using System.Security;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ArbBetSystem
{
    class DynamicOdds
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DynamicOdds));

        public enum ExoticTypes { QQ, EX };

        private string _sessionId;
        private HttpClient _client = new HttpClient();

        // Query string static parameter names
        private static string PARAM_MEETINGID = "MeetingId";
        private static string PARAM_EVENTID = "EventId";
        private static string PARAM_METHOD = "Method";
        private static string PARAM_DATE = "Date";
        private static string PARAM_TYPES = "Types";
        private static string PARAM_RUNNERS = "Runners";
        private static string PARAM_SHOWALL = "ShowAll";
        private static string PARAM_LIMIT = "Limit";
        private static string PARAM_EXOTICTYPE = "ExoticType";
        private static string PARAM_BAID = "BAID";

        public DynamicOdds(string url)
        {
            if (url.EndsWith("/")) { url = url.Substring(0, url.Length - 1); }
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.8,en-AU;q=0.6");
            logger.Debug("Created with URL " + url);
        }

        public DynamicOdds(HttpClient client)
        {
            _client = client;
        }

        public bool Login(string sessionId)
        {
            _sessionId = sessionId;
            return true;
        }

        public bool Login(Creds creds)
        {
            if (creds == null || creds.Username == null || creds.Password == null) { return false; }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                "/xml/data/Login.asp?UserName=" + creds.Username
                + "&Password=" + creds.Password);
            HttpResponseMessage response = _client.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response.Content.ReadAsStringAsync().Result);
                if (doc.DocumentElement.SelectSingleNode("/Login/SessionID").Equals(""))
                {
                    _sessionId = null;
                    LogErrorAndThrowHttp("Login failed: " + doc.DocumentElement.SelectSingleNode("/Login/Message").InnerText);
                }
                _sessionId = doc.DocumentElement.SelectSingleNode("/Login/SessionID").InnerText;
                return true;
            }
            _sessionId = null;
            LogErrorAndThrowHttp("Login failed: Unsuccessful response");
            throw new Exception("???");
        }

        public List<Meeting> GetMeetingsAll(int meetingType = (int)(Meeting.MeetingTypes.Racing | Meeting.MeetingTypes.Harness | Meeting.MeetingTypes.Greyhound), bool runners = true)
        {
            return GetMeetingsAll(DateTime.Today, meetingType, runners);
        }

        public List<Meeting> GetMeetingsAll(DateTime date, int meetingType = (int)(Meeting.MeetingTypes.Racing | Meeting.MeetingTypes.Harness | Meeting.MeetingTypes.Greyhound), bool runners = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetMeetingsAll")
                + addQueryParam(PARAM_DATE, date.ToString("yyyy-M-d"))
                + addQueryParam(PARAM_TYPES, Meeting.getMeetingTypesString(meetingType))
                + addQueryParam(PARAM_RUNNERS, runners.ToString().ToLower()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                if (!doc.DocumentElement.GetElementsByTagName("Error").Item(0).InnerText.Equals("")
                    || doc.DocumentElement.GetElementsByTagName("GetMeetingsAll").Count == 0)
                {
                    LogErrorAndThrowHttp("Response contains error: \"" + doc.DocumentElement.SelectSingleNode("/Data/Error/ErrorTxt").InnerText + "\"");
                }

                List<Meeting> meetings = new List<Meeting>();
                XmlReader xmlR = XmlReader.Create(response.Content.ReadAsStreamAsync().Result);
                XmlSerializer xmlS = new XmlSerializer(typeof(Meeting));

                while(xmlR.ReadToFollowing("Meeting"))
                {
                    meetings.Add((Meeting)xmlS.Deserialize(xmlR.ReadSubtree()));
                }
                
                return meetings;
            }
            LogErrorAndThrowHttp("Request failed: Unsuccessful response");
            throw new Exception("???");
        }

        public Meeting GetMeeting(string meetingId, bool runners = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetMeetings")
                + addQueryParam(PARAM_MEETINGID, meetingId)
                + addQueryParam(PARAM_RUNNERS, runners.ToString().ToLower()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new Meeting();
        }

        public Meeting GetEvent(string eventId, bool runners = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetEvent")
                + addQueryParam(PARAM_EVENTID, eventId)
                + addQueryParam(PARAM_RUNNERS, runners.ToString().ToLower()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new Meeting();
        }

        public Meeting GetEventSchedule(DateTime date, int meetingType = (int)(Meeting.MeetingTypes.Racing | Meeting.MeetingTypes.Harness | Meeting.MeetingTypes.Greyhound), int limit = 999)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetEventSchedule")
                + addQueryParam(PARAM_DATE, date.ToString("yyyy-M-d"))
                + addQueryParam(PARAM_TYPES, Meeting.getMeetingTypesString(meetingType))
                + addQueryParam(PARAM_LIMIT, limit.ToString()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new Meeting();
        }

        public List<Runner> GetRunnersAll(DateTime date, int meetingType = (int)(Meeting.MeetingTypes.Racing | Meeting.MeetingTypes.Harness | Meeting.MeetingTypes.Greyhound), bool showAll = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetRunnersAll")
                + addQueryParam(PARAM_DATE, date.ToString("yyyy-M-d"))
                + addQueryParam(PARAM_TYPES, Meeting.getMeetingTypesString(meetingType))
                + addQueryParam(PARAM_SHOWALL, showAll.ToString().ToLower()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        public List<Runner> GetRunnersMeeting(string meetingId, bool showAll = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetRunnersMeeting")
                + addQueryParam(PARAM_MEETINGID, meetingId)
                + addQueryParam(PARAM_SHOWALL, showAll.ToString().ToLower()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        public List<Runner> GetRunnersEvent(string eventId, bool showAll = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetRunnersEvent")
                + addQueryParam(PARAM_EVENTID, eventId)
                + addQueryParam(PARAM_SHOWALL, showAll.ToString().ToLower()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        public RunnerOdds GetRunnerOdds(string eventId)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetRunnerOdds")
                + addQueryParam(PARAM_EVENTID, eventId));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                if (!doc.DocumentElement.GetElementsByTagName("Error").Item(0).InnerText.Equals("")
                    || doc.DocumentElement.GetElementsByTagName("RunnerOdds").Count == 0)
                {
                    LogErrorAndThrowHttp("Response contains error: \"" + doc.DocumentElement.SelectSingleNode("/Data/Error/ErrorTxt").InnerText + "\"");
                }

                XmlReader xmlR = XmlReader.Create(response.Content.ReadAsStreamAsync().Result);
                xmlR.ReadToFollowing("RunnerOdds");
                return (RunnerOdds)new XmlSerializer(typeof(RunnerOdds)).Deserialize(xmlR);
            }
            LogErrorAndThrowHttp("Request failed: Unsuccessful response");
            throw new Exception("???");
        }

        public List<Runner> GetEventResults(string eventId, bool showAll = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetEventResults")
                + addQueryParam(PARAM_EVENTID, eventId));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        public List<Runner> GetExotics(string eventId, bool showAll = true, ExoticTypes exoticType = ExoticTypes.EX)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetExotics")
                + addQueryParam(PARAM_EVENTID, eventId)
                + addQueryParam(PARAM_EXOTICTYPE, exoticType.ToString()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        public List<Runner> GetBookmakerFlucs(string eventId, string baid, bool showAll = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetBookmakerFlucs")
                + addQueryParam(PARAM_EVENTID, eventId)
                + addQueryParam(PARAM_BAID, baid));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        public List<Runner> GetBettingAgencies()
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(_sessionId)
                + addQueryParam(PARAM_METHOD, "GetBettingAgencies"));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        private void CheckSession()
        {
            if (_sessionId == null)
            {
                LogErrorAndThrowHttp("Not logged in");
            }
        }

        private void LogErrorAndThrowHttp(string Error)
        {
            logger.Error(Error);
            throw new HttpRequestException(Error);
        }

        // Query string helpers
        private static string getDataRequest(string sessionId) { return "/xml/data/GetData.asp?SessionID=" + sessionId; }
        private static string addQueryParam(string param, string value) { return "&" + param + "=" + value; }
    }
}
