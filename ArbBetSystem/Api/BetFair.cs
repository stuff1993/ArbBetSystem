using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using ArbBetSystem.Models.BetFair;
using System.Collections.Specialized;
using System.Net;
using System.Web.Services.Protocols;
using Newtonsoft.Json.Linq;

using ArbBetSystem.Json;

namespace ArbBetSystem.Api
{
    class BetFair : HttpWebClientProtocol, IApi 
    {
        public NameValueCollection CustomHeaders { get; set; }

        #region Auth
        private const string DEFAULT_AUTH_BASEURL = "https://identitysso.betfair.com";

        private Creds creds;
        private X509Certificate2 cert;

        public Creds LoginDetails
        {
            get { return creds; }
            set { creds = value; }
        }

        public X509Certificate2 Certificate
        {
            get { return cert; }
            set { cert = value; }
        }
        #endregion

        #region API
        private const string DEFAULT_API_BASEURL = "https://api.betfair.com/exchange/betting/json-rpc/v1";

        public const string APPKEY_HEADER = "X-Application";
        public const string SESSION_TOKEN_HEADER = "X-Authentication";
        private static readonly string LIST_EVENT_TYPES_METHOD = "SportsAPING/v1.0/listEventTypes";
        private static readonly string LIST_EVENTS_METHOD = "SportsAPING/v1.0/listEvents";
        private static readonly string LIST_VENUES_METHOD = "SportsAPING/v1.0/listVenues";
        private static readonly string LIST_MARKET_TYPES_METHOD = "SportsAPING/v1.0/listMarketTypes";
        private static readonly string LIST_MARKET_CATALOGUE_METHOD = "SportsAPING/v1.0/listMarketCatalogue";
        private static readonly string LIST_MARKET_BOOK_METHOD = "SportsAPING/v1.0/listMarketBook";
        private static readonly string PLACE_ORDERS_METHOD = "SportsAPING/v1.0/placeOrders";
        private static readonly string LIST_MARKET_PROFIT_AND_LOST_METHOD = "SportsAPING/v1.0/listMarketProfitAndLoss";
        private static readonly string LIST_CURRENT_ORDERS_METHOD = "SportsAPING/v1.0/listCurrentOrders";
        private static readonly string LIST_CLEARED_ORDERS_METHOD = "SportsAPING/v1.0/listClearedOrders";
        private static readonly string CANCEL_ORDERS_METHOD = "SportsAPING/v1.0/cancelOrders";
        private static readonly string REPLACE_ORDERS_METHOD = "SportsAPING/v1.0/replaceOrders";
        private static readonly string UPDATE_ORDERS_METHOD = "SportsAPING/v1.0/updateOrders";
        private static readonly string GET_ACCOUNT_FUNDS_METHOD = "AccountAPING/v1.0/getAccountFunds";
        private static readonly string FILTER = "filter";
        private static readonly string LOCALE = "locale";
        private static readonly string WALLET = "wallet";
        private static readonly string CURRENCY_CODE = "currencyCode";
        private static readonly string MARKET_PROJECTION = "marketProjection";
        private static readonly string MATCH_PROJECTION = "matchProjection";
        private static readonly string ORDER_PROJECTION = "orderProjection";
        private static readonly string PRICE_PROJECTION = "priceProjection";
        private static readonly string SORT = "sort";
        private static readonly string MAX_RESULTS = "maxResults";
        private static readonly string MARKET_IDS = "marketIds";
        private static readonly string MARKET_ID = "marketId";
        private static readonly string INSTRUCTIONS = "instructions";
        private static readonly string CUSTOMER_REFERENCE = "customerRef";
        private static readonly string INCLUDE_SETTLED_BETS = "includeSettledBets";
        private static readonly string INCLUDE_BSP_BETS = "includeBspBets";
        private static readonly string NET_OF_COMMISSION = "netOfCommission";
        private static readonly string BET_IDS = "betIds";
        private static readonly string PLACED_DATE_RANGE = "placedDateRange";
        private static readonly string ORDER_BY = "orderBy";
        private static readonly string SORT_DIR = "sortDir";
        private static readonly string FROM_RECORD = "fromRecord";
        private static readonly string RECORD_COUNT = "recordCount";
        private static readonly string BET_STATUS = "betStatus";
        private static readonly string EVENT_TYPE_IDS = "eventTypeIds";
        private static readonly string EVENT_IDS = "eventIds";
        private static readonly string RUNNER_IDS = "runnerIds";
        private static readonly string SIDE = "side";
        private static readonly string SETTLED_DATE_RANGE = "settledDateRange";
        private static readonly string GROUP_BY = "groupBy";
        private static readonly string INCLUDE_ITEM_DESCRIPTION = "includeItemDescription";

        private static readonly IDictionary<string, Type> operationReturnTypeMap = new Dictionary<string, Type>();

        private string sessionId;

        public string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }
        #endregion

        #region Constructor
        public BetFair(string appKey, X509Certificate2 cert, string apiUrl = DEFAULT_API_BASEURL, string authUrl = DEFAULT_AUTH_BASEURL)
        {
            this.cert = cert;
            CustomHeaders = new NameValueCollection();
            CustomHeaders[APPKEY_HEADER] = appKey;
        }
        #endregion

        #region ApiLogin Methods
        public bool doLogin()
        {
            HttpResponseMessage result = initHttpClientInstance(getWebRequestHandlerWithCert(this.Certificate))
                .PostAsync("/api/certlogin", getLoginBodyAsContent(this.LoginDetails.Username, this.LoginDetails.Password))
                .Result;
            result.EnsureSuccessStatusCode();
            DataContractJsonSerializer jsonSerialiser = new DataContractJsonSerializer(typeof(LoginResponse));
            MemoryStream stream = new MemoryStream(result.Content.ReadAsByteArrayAsync().Result);
            LoginResponse resp = (LoginResponse)jsonSerialiser.ReadObject(stream);
            SessionId = resp.SessionToken;
            CustomHeaders[SESSION_TOKEN_HEADER] = SessionId;
            return this.SessionId != null;
        }
        #endregion

        #region Auth Private Methods
        private WebRequestHandler getWebRequestHandlerWithCert(X509Certificate2 cert)
        {
            WebRequestHandler clientHandler = new WebRequestHandler();
            clientHandler.ClientCertificates.Add(cert);
            return clientHandler;
        }

        private HttpClient initHttpClientInstance(WebRequestHandler clientHandler, string baseUrl = DEFAULT_AUTH_BASEURL)
        {
            HttpClient client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add(APPKEY_HEADER, CustomHeaders[APPKEY_HEADER]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private FormUrlEncodedContent getLoginBodyAsContent(string username, string password)
        {
            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("username", username));
            postData.Add(new KeyValuePair<string, string>("password", password));
            return new FormUrlEncodedContent(postData);
        }
        #endregion

        #region API Public Methods
        public IList<EventTypeResult> listEventTypes(MarketFilter marketFilter, string locale = null)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[FILTER] = marketFilter;
            args[LOCALE] = locale;
            return Invoke<List<EventTypeResult>>(LIST_EVENT_TYPES_METHOD, args);
        }

        public IList<EventResult> listEvents(MarketFilter marketFilter, string locale = null) {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[FILTER] = marketFilter;
            args[LOCALE] = locale;
            return Invoke<List<EventResult>>(LIST_EVENTS_METHOD, args);
        }

        public IList<VenueResult> listVenues(MarketFilter marketFilter, string locale = null) {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[FILTER] = marketFilter;
            args[LOCALE] = locale;
            return Invoke<List<VenueResult>>(LIST_VENUES_METHOD, args);
        }

        public IList<MarketCatalogue> listMarketCatalogue(MarketFilter marketFilter, ISet<MarketProjection> marketProjections, MarketSort marketSort, string maxResult = "1", string locale = null)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[FILTER] = marketFilter;
            args[MARKET_PROJECTION] = marketProjections;
            args[SORT] = marketSort;
            args[MAX_RESULTS] = maxResult;
            args[LOCALE] = locale;
            return Invoke<List<MarketCatalogue>>(LIST_MARKET_CATALOGUE_METHOD, args);
        }

        public IList<MarketTypeResult> listMarketTypes(MarketFilter marketFilter, string stringLocale)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[FILTER] = marketFilter;
            args[LOCALE] = stringLocale;
            return Invoke<List<MarketTypeResult>>(LIST_MARKET_TYPES_METHOD, args);
        }

        public IList<MarketBook> listMarketBook(IList<string> marketIds, PriceProjection priceProjection, OrderProjection? orderProjection = null, MatchProjection? matchProjection = null, string currencyCode = null, string locale = null)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[MARKET_IDS] = marketIds;
            args[PRICE_PROJECTION] = priceProjection;
            args[ORDER_PROJECTION] = orderProjection;
            args[MATCH_PROJECTION] = matchProjection;
            args[LOCALE] = locale;
            args[CURRENCY_CODE] = currencyCode;
            return Invoke<List<MarketBook>>(LIST_MARKET_BOOK_METHOD, args);
        }

        public PlaceExecutionReport placeOrders(string marketId, string customerRef, IList<PlaceInstruction> placeInstructions, string locale = null)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();

            args[MARKET_ID] = marketId;
            args[INSTRUCTIONS] = placeInstructions;
            args[CUSTOMER_REFERENCE] = customerRef;
            args[LOCALE] = locale;

            return Invoke<PlaceExecutionReport>(PLACE_ORDERS_METHOD, args);
        }

        public IList<MarketProfitAndLoss> listMarketProfitAndLoss(IList<string> marketIds, bool includeSettledBets = false, bool includeBspBets = false, bool netOfCommission = false)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[MARKET_IDS] = marketIds;
            args[INCLUDE_SETTLED_BETS] = includeSettledBets;
            args[INCLUDE_BSP_BETS] = includeBspBets;
            args[NET_OF_COMMISSION] = netOfCommission;
            return Invoke<List<MarketProfitAndLoss>>(LIST_MARKET_PROFIT_AND_LOST_METHOD, args);
        }

        public CurrentOrderSummaryReport listCurrentOrders(ISet<string> betIds, ISet<string> marketIds, OrderProjection? orderProjection = null, TimeRange placedDateRange = null, OrderBy? orderBy = null, SortDir? sortDir = null, int? fromRecord = null, int? recordCount = null)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[BET_IDS] = betIds;
            args[MARKET_IDS] = marketIds;
            args[ORDER_PROJECTION] = orderProjection;
            args[PLACED_DATE_RANGE] = placedDateRange;
            args[ORDER_BY] = orderBy;
            args[SORT_DIR] = sortDir;
            args[FROM_RECORD] = fromRecord;
            args[RECORD_COUNT] = recordCount;

            return Invoke<CurrentOrderSummaryReport>(LIST_CURRENT_ORDERS_METHOD, args);
        }

        public ClearedOrderSummaryReport listClearedOrders(BetStatus betStatus, ISet<string> eventTypeIds = null, ISet<string> eventIds = null, ISet<string> marketIds = null, ISet<RunnerId> runnerIds = null, ISet<string> betIds = null, Side? side = null, TimeRange settledDateRange = null, GroupBy? groupBy = null, bool? includeItemDescription = null, String locale = null, int? fromRecord = null, int? recordCount = null)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[BET_STATUS] = betStatus;
            args[EVENT_TYPE_IDS] = eventTypeIds;
            args[EVENT_IDS] = eventIds;
            args[MARKET_IDS] = marketIds;
            args[RUNNER_IDS] = runnerIds;
            args[BET_IDS] = betIds;
            args[SIDE] = side;
            args[SETTLED_DATE_RANGE] = settledDateRange;
            args[GROUP_BY] = groupBy;
            args[INCLUDE_ITEM_DESCRIPTION] = includeItemDescription;
            args[LOCALE] = locale;
            args[FROM_RECORD] = fromRecord;
            args[RECORD_COUNT] = recordCount;

            return Invoke<ClearedOrderSummaryReport>(LIST_CLEARED_ORDERS_METHOD, args);
        }

        public CancelExecutionReport cancelOrders(string marketId, IList<CancelInstruction> instructions, string customerRef)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[MARKET_ID] = marketId;
            args[INSTRUCTIONS] = instructions;
            args[CUSTOMER_REFERENCE] = customerRef;

            return Invoke<CancelExecutionReport>(CANCEL_ORDERS_METHOD, args);
        }

        public ReplaceExecutionReport replaceOrders(string marketId, IList<ReplaceInstruction> instructions, string customerRef)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[MARKET_ID] = marketId;
            args[INSTRUCTIONS] = instructions;
            args[CUSTOMER_REFERENCE] = customerRef;

            return Invoke<ReplaceExecutionReport>(REPLACE_ORDERS_METHOD, args);
        }

        public UpdateExecutionReport updateOrders(string marketId, IList<UpdateInstruction> instructions, string customerRef)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[MARKET_ID] = marketId;
            args[INSTRUCTIONS] = instructions;
            args[CUSTOMER_REFERENCE] = customerRef;

            return Invoke<UpdateExecutionReport>(UPDATE_ORDERS_METHOD, args);
        }

        public AccountFundsResponse getAccountFunds(Wallet wallet)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args[WALLET] = wallet;
            return Invoke<AccountFundsResponse>(GET_ACCOUNT_FUNDS_METHOD, args);
        }

        public T Invoke<T>(string method, IDictionary<string, object> args = null)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            if (method.Length == 0)
                throw new ArgumentException(null, "method");
            if (args.ContainsKey(FILTER))
                ((MarketFilter)args[FILTER]).MarketStartTime = new TimeRange() { To = DateTime.Now.AddDays(1), From = DateTime.Now.AddDays(-1) };

            WebRequest request = CreateWebRequest(new Uri(DEFAULT_API_BASEURL));

            using (Stream stream = request.GetRequestStream())
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                JsonRequest call = new JsonRequest { Method = method, Id = 1, Params = args };
                JsonConvert.Export(call, writer);
            }
            //Console.WriteLine("\nCalling: " + method + " With args: " + JsonConvert.Serialize<IDictionary<string, object>>(args));

            using (WebResponse response = GetWebResponse(request))
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                JsonResponse<T> jsonResponse = JsonConvert.Import<T>(reader);
                // Console.WriteLine("\nGot Response: " + JsonConvert.Serialize<JsonResponse<T>>(jsonResponse));
                if (jsonResponse.HasError)
                {
                    throw ReconstituteException(jsonResponse.Error);
                }
                else
                {
                    return jsonResponse.Result;
                }
            }
        }
        #endregion

        #region API Protected Methods
        protected WebRequest CreateWebRequest(Uri uri)
        {
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json-rpc";
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8");
            request.Headers.Add(CustomHeaders);
            ServicePointManager.Expect100Continue = false;
            return request;
        }
        #endregion

        #region API Private Methods
        private static System.Exception ReconstituteException(Models.BetFair.Exception ex)
        {
            JObject data = ex.Data;

            // API-NG exception -- it must have "data" element to tell us which exception
            string exceptionName = data.Property("exceptionname").Value.ToString();
            string exceptionData = data.Property(exceptionName).Value.ToString();
            return JsonConvert.Deserialize<APINGException>(exceptionData);
        }
        #endregion
    }
}

