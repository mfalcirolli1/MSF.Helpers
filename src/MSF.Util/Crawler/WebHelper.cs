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
        private string _boundary = "";

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

        #endregion

        #region | Response

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
    }
}
