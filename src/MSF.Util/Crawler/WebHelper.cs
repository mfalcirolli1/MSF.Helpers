using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using HtmlAgilityPack;

namespace MSF.Util.Crawler
{
    public abstract class WebHelper
    {
        public WebHelper()
        {
            IniciarConfiguracaoSSL();
        }

        protected Dictionary<String, string> _postParameters;
        protected List<KeyValuePair<String, string>> _postParametersDuplicatedKeys;
        protected string _jsonParameter;

        private bool _usePostFormData = false;
        public HttpWebRequest _request;
        protected HttpWebResponse _response;
        private string _boundary = "";
        protected string _responseContent;
        protected HtmlDocument _document;

        #region | Configuração SSL
        public void IniciarConfiguracaoSSL()
        {
            if (ServicePointManager.ServerCertificateValidationCallback == null)
            {
                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(bypassAllCertificateStuff);
            }

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | (SecurityProtocolType)3072 | (SecurityProtocolType)768;
            }
            catch (Exception)
            {
            }
        }

        public static bool bypassAllCertificateStuff(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }
        #endregion

        #region | Post Parameters
        public void RemovePostParameter(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) { throw new ArgumentNullException("id"); }
            if (_postParameters == null) { _postParameters = new Dictionary<string, string>(); }
            if (_postParameters.ContainsKey(id)) { _postParameters.Remove(id); }

            string encodedId = HttpUtility.UrlEncode(id);
            if (_postParameters.ContainsKey(encodedId)) { _postParameters.Remove(encodedId); }

            if (_postParametersDuplicatedKeys == null) { _postParametersDuplicatedKeys = new List<KeyValuePair<string, string>>(); }

            foreach (var item in _postParametersDuplicatedKeys)
            {
                if (item.Key == id)
                {
                    _postParametersDuplicatedKeys.Remove(item);
                }
                if (item.Key == encodedId)
                {
                    _postParametersDuplicatedKeys.Remove(item);
                }
            }
        }

        public void AddAllPostParameters(Dictionary<string, string> postData)
        {
            foreach (var input in postData)
            {
                AddPostParameter(input.Key, input.Value);
            }
        }

        public void AddPostParameter(string id, string value)
        {
            if (id == null && value == null)
                return;

            AddPostParameter(id, value, this._usePostFormData);
        }

        public void AddPostParameter(string id, string value, bool isFormData)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            if (_postParameters == null)
            {
                _postParameters = new Dictionary<string, string>();
            }

            if (!isFormData)
            {
                id = HttpUtility.UrlEncode(Encoding.GetEncoding("ISO-8859-1").GetBytes(id));
                value = value == null ? "" : HttpUtility.UrlEncode(Encoding.GetEncoding("ISO-8859-1").GetBytes(value));
            }

            if (_postParameters.ContainsKey(id))
            {
                _postParameters[id] = value;
            }
            else
            {
                _postParameters.Add(id, value);
            }
        }

        public void AddAllPostParametersDuplicatedKeys(List<KeyValuePair<string, string>> postData)
        {
            foreach (var input in postData)
            {
                AddPostParametersDuplicatedKeys(input.Key, input.Value);
            }
        }

        public void AddPostParametersDuplicatedKeys(string id, string value)
        {
            AddPostParametersDuplicatedKeys(id, value, this._usePostFormData);
        }

        public void AddPostParametersDuplicatedKeys(string id, string value, bool isFormData)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            if (_postParametersDuplicatedKeys == null)
            {
                _postParametersDuplicatedKeys = new List<KeyValuePair<string, string>>();
            }

            if (!isFormData)
            {
                id = HttpUtility.UrlEncode(Encoding.GetEncoding("ISO-8859-1").GetBytes(id));
                value = value == null ? "" : HttpUtility.UrlEncode(Encoding.GetEncoding("ISO-8859-1").GetBytes(value));
            }

            _postParametersDuplicatedKeys.Add(new KeyValuePair<string, string>(id, value));
        }

        public void AddJsonParameter(string jsonParameter)
        {
            _jsonParameter = jsonParameter;
        }

        public void AddPostStringBody(string body)
        {
            var paramBytes = Encoding.UTF8.GetBytes(body);
            _request.ContentLength = paramBytes.Length;

            var requestStream = _request.GetRequestStream();
            requestStream.Write(paramBytes, 0, paramBytes.Length);
            requestStream.Close();
        }

        private void ClearPostParameters()
        {
            _postParameters = null;
        }

        private void ClearPostParametersDuplicatedKeys()
        {
            _postParametersDuplicatedKeys = null;
        }

        private void ClearJsonParameters()
        {
            _jsonParameter = string.Empty;
        }

        private string GetPostParametersString()
        {
            if (_postParameters == null || _postParameters.Count == 0)
            {
                return String.Empty;
            }
            return String.Join("&", _postParameters.Select(x => string.Format("{0}={1}", x.Key, x.Value))); ;
        }

        private string GetPostParametersDuplicatedKeysString()
        {
            if (_postParametersDuplicatedKeys == null || _postParametersDuplicatedKeys.Count == 0)
            {
                return String.Empty;
            }
            return String.Join("&", _postParametersDuplicatedKeys.Select(x => string.Format("{0}={1}", x.Key, x.Value))); ;
        }

        private string GetJsonParameterString()
        {
            if (string.IsNullOrEmpty(_jsonParameter))
            {
                return string.Empty;
            }
            return _jsonParameter;
        }

        private void AddRequestPostParameters(HttpWebRequest request)
        {
            Stream requestStream = null;

            if (this._usePostFormData)
            {
                byte[] boundaryBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes("\r\n--" + boundary + "\r\n");

                requestStream = request.GetRequestStream();

                string formDataTemplate = "";
                string formItem = "";
                byte[] formItemBytes = null;

                List<KeyValuePair<String, string>> lstParam = new List<KeyValuePair<string, string>>();

                if (_postParameters != null)
                {
                    foreach (var par in _postParameters)
                    {
                        lstParam.Add(new KeyValuePair<string, string>(par.Key, par.Value));
                    }
                }

                if (_postParametersDuplicatedKeys != null)
                {
                    foreach (var parD in _postParametersDuplicatedKeys)
                    {
                        lstParam.Add(new KeyValuePair<string, string>(parD.Key, parD.Value));
                    }
                }

                foreach (var p in lstParam)
                {
                    var arrayKey = p.Key.Split('|');
                    if (arrayKey.Length > 1)
                    {
                        string extraContent = string.Empty;
                        for (int i = 0; i < arrayKey.Length; i++)
                        {
                            extraContent += arrayKey[i];
                        }

                        formDataTemplate = "Content-Disposition: form-data; name=\"{0}\"{1}\r\n\r\n{2}";
                        formItem = string.Format(formDataTemplate, arrayKey[0], extraContent, p.Value);
                    }
                    else
                    {
                        formDataTemplate = "Content-Disposition: form-data; name=\"{0}\"{1}\r\n\r\n{1}";
                        formItem = string.Format(formDataTemplate, p.Key, p.Value);
                    }

                    formItemBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(formItem);
                    requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    requestStream.Write(formItemBytes, 0, formItemBytes.Length);
                }

                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

                byte[] trailer = Encoding.GetEncoding("ISO-8859-1").GetBytes("\r\n--" + boundary + "--\r\n");
                requestStream.Write(trailer, 0, trailer.Length);
                requestStream.Close();
            }
            else
            {
                byte[] paramBytes = null;

                string strParametersJson = GetJsonParameterString();

                if (!string.IsNullOrEmpty(strParametersJson))
                {
                    paramBytes = Encoding.UTF8.GetBytes(strParametersJson);
                }
                else
                {
                    string strParameters = GetPostParametersString();
                    string strParametersDK = GetPostParametersDuplicatedKeysString();

                    if (!String.IsNullOrWhiteSpace(strParameters) && !String.IsNullOrWhiteSpace(strParametersDK))
                    {
                        strParameters = strParameters + "&" + strParametersDK;
                    }
                    else if (String.IsNullOrWhiteSpace(strParameters) && !String.IsNullOrWhiteSpace(strParametersDK))
                    {
                        strParameters = strParametersDK;
                    }

                    if (!String.IsNullOrWhiteSpace(strParameters))
                    {
                        paramBytes = Encoding.UTF8.GetBytes(strParameters);
                    }
                    else
                    {
                        return;
                    }
                }

                request.ContentLength = paramBytes.Length;
                requestStream = request.GetRequestStream();
                requestStream.Write(paramBytes, 0, paramBytes.Length);
                requestStream.Close();
            }
        }

        protected string boundary
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_boundary))
                {
                    _boundary = "-----" + DateTime.Now.Ticks.ToString("x");
                }
                return _boundary;
            }
        }
        #endregion

        #region | Request

        public void BeginStep(string url, string method)
        {
            BeginStep(url, method, false);
        }

        public void BeginStep(string url, string method, bool usePostFormData)
        {
            BeginStep(url, method, String.Empty, usePostFormData);
        }

        public void BeginStep(string url, string method, string referer)
        {
            BeginStep(url, method, referer, false);
        }

        public void BeginStep(string url, string method, string referer, bool usePostFormData)
        {
            _usePostFormData = usePostFormData;
            _boundary = string.Empty;
            _request = null;
            _response = null;
            _responseContent = string.Empty;
            _document = null;

            ClearPostParameters();
            ClearPostParametersDuplicatedKeys();
            ClearJsonParameters();

            _cookieContainer = new CookieContainer();
            _cookieContainer.Add(_cookies);
            CreateRequest(url, method, referer);
        }

        private void CreateRequest(string url, string method, string referer)
        {
            CreateRequest(url, method, referer, false);
        }

        private void CreateRequest(string url, string method, string referer, bool useProxy)
        {
            _request = GetRequest(url, method, referer);
        }

        private HttpWebRequest GetRequest(string url, string method, string referer)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = method;
            request.KeepAlive = true;
            request.Referer = referer;
            request.CachePolicy = new System.Net.Cache.HttpRequestCachePolicy(System.Net.Cache.HttpRequestCacheLevel.NoCacheNoStore);
            request.ReadWriteTimeout = 600000;
            request.Timeout = 600000;

            if (this._usePostFormData)
            {
                request.ContentType = String.Format("multipart/form-data; boundary{0}", boundary);
                request.AllowWriteStreamBuffering = true;
            }
            else
            {
                request.ContentType = String.Format("application/x-www-form-urlencoded");
            }

            request.Accept = "image/gif, image/jpeg, image/pjpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/vnd.msword, application/vnd.x-ms-application, application/vnd.x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, */*";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.2; .NET4.0E; itx)";
            request.CookieContainer = _cookieContainer;
            request.PreAuthenticate = true;
            request.AllowAutoRedirect = true;
            request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");

            return request;
        }

        #endregion

        #region | Response

        public HttpWebResponse GetResponseInternal()
        {
            if (_response == null)
            {
                AddRequestPostParameters(_request);
                _response = _GetResponseInternal(_request);
            }

            return _response;
        }

        private HttpWebResponse _GetResponseInternal(HttpWebRequest request)
        {
            var httpResponse = request.GetResponseSafe() as HttpWebResponse;
            AddCookies(httpResponse.Cookies);
            return httpResponse;
        }

        public void EndStep()
        {
            GetResponseInternal().Close();
        }

        #endregion

        #region | Cookies

        public CookieContainer _cookieContainer;
        public CookieCollection _cookies = new CookieCollection();

        public void AddCookies(CookieCollection cookies)
        {
            var newCookies = (from c in cookies.Cast<Cookie>() 
                              where !_cookies.Cast<Cookie>().Any(c1 => c1.Name == c.Name)
                              select c).ToList();

            var updateCookies = (from c in cookies.Cast<Cookie>()
                                 let newCookie = cookies.Cast<Cookie>().FirstOrDefault(c1 => c1.Name == c.Name && c1.Value != c.Value)
                                 where newCookie != null
                                 select new 
                                 { 
                                     cookie = c,
                                     newValue = newCookie.Value
                                 }).ToList();

            foreach (var c in newCookies)
            {
                _cookies.Add(c);
            }

            foreach (var c in updateCookies)
            {
                c.cookie.Value = c.newValue;
            }
        }

        #endregion

        #region | Read Response

        public string GetResponseContent()
        {
            return _GetResponseContent(GetResponseInternal());
        }

        private string _GetResponseContent(HttpWebResponse response)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(_responseContent))
                {
                    var reponseStream = response.GetResponseStream();
                    Stream encondedStream;

                }
            }
            catch { }

            return _responseContent;
        }

        #endregion
    }
}