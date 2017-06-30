using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using log4net;
using System.ComponentModel;

namespace ArbBetSystem.Api
{
    class DynamicOdds : IApi, IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DynamicOdds));

        public enum ExoticTypes { QQ, EX };

        private Creds creds;
        private string sessionId;
        private HttpClient _client = new HttpClient();

        public Creds LoginDetails
        {
            get { return creds; }
            set { creds = value; }
        }

        public string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

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

        public bool doLogin(string sessionId)
        {
            this.sessionId = sessionId;
            return true;
        }

        public bool doLogin()
        {
            if (this.LoginDetails == null || this.LoginDetails.Username == null || this.LoginDetails.Password == null) { return false; }
            DateTime start = DateTime.Now;
            logger.Debug("Login Request");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                "/xml/data/Login.asp?UserName=" + this.LoginDetails.Username
                + "&Password=" + this.LoginDetails.Password);
            HttpResponseMessage response = _client.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response.Content.ReadAsStringAsync().Result);
                if (doc.DocumentElement.SelectSingleNode("/Login/SessionID").Equals(""))
                {
                    sessionId = null;
                    LogErrorAndThrowHttp("Login failed: " + doc.DocumentElement.SelectSingleNode("/Login/Message").InnerText);
                }
                sessionId = doc.DocumentElement.SelectSingleNode("/Login/SessionID").InnerText;

                logger.Debug("Login Request Time: " + start.ToLongTimeString() + " Time Elapsed: " + (DateTime.Now - start).TotalSeconds);
                return true;
            }
            sessionId = null;
            LogErrorAndThrowHttp("Login failed: Unsuccessful response");
            throw new Exception("???");
        }

        public BindingList<Meeting> GetMeetingsAllSuppressed(BindingList<Meeting> meetings, int meetingType = (int)(Meeting.MeetingTypes.Racing | Meeting.MeetingTypes.Harness | Meeting.MeetingTypes.Greyhound), bool runners = true)
        {
            return GetMeetingsAllSuppressed(meetings, DateTime.Today, meetingType, runners);
        }

        public BindingList<Meeting> GetMeetingsAllSuppressed(BindingList<Meeting> meetings, DateTime date, int meetingType = (int)(Meeting.MeetingTypes.Racing | Meeting.MeetingTypes.Harness | Meeting.MeetingTypes.Greyhound), bool runners = true)
        {
            try {
                return GetMeetingsAll(date, meetingType, runners);
            }
            catch (HttpRequestException e)
            {
                logger.Error("GetMeetingsAll - Suppressed:", e);
                return meetings;
            }
        }

        public BindingList<Meeting> GetMeetingsAll(int meetingType = (int)(Meeting.MeetingTypes.Racing | Meeting.MeetingTypes.Harness | Meeting.MeetingTypes.Greyhound), bool runners = true)
        {
            return GetMeetingsAll(DateTime.Today, meetingType, runners);
        }

        public BindingList<Meeting> GetMeetingsAll(DateTime date, int meetingType = (int)(Meeting.MeetingTypes.Racing | Meeting.MeetingTypes.Harness | Meeting.MeetingTypes.Greyhound), bool runners = true)
        {
            CheckSession();
            DateTime start = DateTime.Now;
            logger.Debug("GetMeetingsAll Request");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(sessionId)
                + addQueryParam(PARAM_METHOD, "GetMeetingsAll")
                + addQueryParam(PARAM_DATE, date.ToString("yyyy-M-d"))
                + addQueryParam(PARAM_TYPES, Meeting.getDOMeetingTypesString(meetingType))
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

                BindingList<Meeting> meetings = new BindingList<Meeting>();
                XmlReader xmlR = XmlReader.Create(response.Content.ReadAsStreamAsync().Result);
                XmlSerializer xmlS = new XmlSerializer(typeof(Meeting));

                while(xmlR.ReadToFollowing("Meeting"))
                {
                    meetings.Add((Meeting)xmlS.Deserialize(xmlR.ReadSubtree()));
                }
                foreach (Meeting m in meetings)
                {
                    m.MapChildren(date);
                }

                logger.Debug("Request Time: " + start.ToLongTimeString() + " Time Elapsed: " + (DateTime.Now - start).TotalSeconds);
                return meetings;
            }
            LogErrorAndThrowHttp("Request failed: Unsuccessful response");
            throw new Exception("???");
        }

        public Meeting GetMeeting(string meetingId, bool runners = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(sessionId)
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
                getDataRequest(sessionId)
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
                getDataRequest(sessionId)
                + addQueryParam(PARAM_METHOD, "GetEventSchedule")
                + addQueryParam(PARAM_DATE, date.ToString("yyyy-M-d"))
                + addQueryParam(PARAM_TYPES, Meeting.getDOMeetingTypesString(meetingType))
                + addQueryParam(PARAM_LIMIT, limit.ToString()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new Meeting();
        }

        public List<Runner> GetRunnersAll(DateTime date, int meetingType = (int)(Meeting.MeetingTypes.Racing | Meeting.MeetingTypes.Harness | Meeting.MeetingTypes.Greyhound), bool showAll = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(sessionId)
                + addQueryParam(PARAM_METHOD, "GetRunnersAll")
                + addQueryParam(PARAM_DATE, date.ToString("yyyy-M-d"))
                + addQueryParam(PARAM_TYPES, Meeting.getDOMeetingTypesString(meetingType))
                + addQueryParam(PARAM_SHOWALL, showAll.ToString().ToLower()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        public List<Runner> GetRunnersMeeting(string meetingId, bool showAll = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(sessionId)
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
                getDataRequest(sessionId)
                + addQueryParam(PARAM_METHOD, "GetRunnersEvent")
                + addQueryParam(PARAM_EVENTID, eventId)
                + addQueryParam(PARAM_SHOWALL, showAll.ToString().ToLower()));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        public RunnerOdds GetRunnerOdds(string eventId)
        {
            CheckSession();
            DateTime start = DateTime.Now;
            logger.Debug("GetRunnerOdds Request: " + eventId);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(sessionId)
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

                logger.Debug("Request Time: " + start.ToLongTimeString() + " Time Elapsed: " + (DateTime.Now - start).TotalSeconds);
                return (RunnerOdds)new XmlSerializer(typeof(RunnerOdds)).Deserialize(xmlR);
            }
            LogErrorAndThrowHttp("Request failed: Unsuccessful response");
            throw new Exception("???");
        }

        public RunnerOdds GetRunnerOdds(Event evt)
        {
            CheckSession();
            DateTime start = DateTime.Now;
            logger.Debug("GetRunnerOdds Request: " + evt);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(sessionId)
                + addQueryParam(PARAM_METHOD, "GetRunnerOdds")
                + addQueryParam(PARAM_EVENTID, evt.ID));
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

                logger.Debug("Request Time: " + start.ToLongTimeString() + " Time Elapsed: " + (DateTime.Now - start).TotalSeconds);
                return (RunnerOdds)new XmlSerializer(typeof(RunnerOdds)).Deserialize(xmlR);
            }
            LogErrorAndThrowHttp("Request failed: Unsuccessful response");
            throw new Exception("???");
        }

        public List<Runner> GetEventResults(string eventId, bool showAll = true)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(sessionId)
                + addQueryParam(PARAM_METHOD, "GetEventResults")
                + addQueryParam(PARAM_EVENTID, eventId));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        public List<Runner> GetExotics(string eventId, bool showAll = true, ExoticTypes exoticType = ExoticTypes.EX)
        {
            CheckSession();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                getDataRequest(sessionId)
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
                getDataRequest(sessionId)
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
                getDataRequest(sessionId)
                + addQueryParam(PARAM_METHOD, "GetBettingAgencies"));
            HttpResponseMessage response = _client.SendAsync(request).Result;

            return new List<Runner>();
        }

        private void CheckSession()
        {
            if (sessionId == null)
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

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
