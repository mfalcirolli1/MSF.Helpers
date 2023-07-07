using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using APIShell.Services.Utils;
using MSF.Util.OperationResult;
using System.Net.Http.Headers;

namespace MSF.Util.APIHelper
{
    public class APIHelper
    {
        private readonly HttpClient _httpClient;
        private readonly RetryPolicy<HttpResponseMessage> RetryPolicy;
        private readonly CancellationTokenSource CancellationToken;

        public APIHelper(HttpClient httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
            CancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(100));

            RetryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<AggregateException>()
                .OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.GatewayTimeout || r.StatusCode == HttpStatusCode.RequestTimeout
                || r.StatusCode == HttpStatusCode.InternalServerError || r.StatusCode == HttpStatusCode.BadGateway || r.StatusCode == HttpStatusCode.ServiceUnavailable)
                .WaitAndRetry(2, i => TimeSpan.FromSeconds(10));
        }

        public OperationResult<string> Get(string url, string token, string apiKey = null, string mediaType = "application/json", List<string> listaHeaders = null)
        {
            var retorno = new OperationResult<string>() { Situation = SituationEnum.Success };
            var response = default(HttpResponseMessage);

            try
            {
                if (listaHeaders.IsNotNullOrEmpty())
                {
                    _httpClient.DefaultRequestHeaders.Remove("adicionar-key-do-header-aqui");
                    _httpClient.DefaultRequestHeaders.Add("adicionar-key-do-header-aqui", listaHeaders[0]);
                }
                if (string.IsNullOrWhiteSpace(apiKey) == false)
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    _httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);
                }

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                RetryPolicy.Execute(() =>
                {
                    response = _httpClient.GetAsync(url, CancellationToken.Token).Result;
                    return response;
                });

                return ResponseValidate(response);
            }
            catch (Exception ex)
            {
                retorno.Success = false;
                retorno.Situation = SituationEnum.Error;
                retorno.ErrorMessage = ex.Message;
                retorno.MainException = ex.InnerException;
                return retorno;
            }
        }

        public OperationResult<string> Post(string url, string item, string token, string apiKey = null, Encoding encoding = null, string mediaType = "application/json", List<string> listaHeaders = null)
        {
            var retorno = new OperationResult<string>() { Situation = SituationEnum.Success };
            var response = default(HttpResponseMessage);

            try
            {
                if (listaHeaders.IsNotNullOrEmpty())
                {
                    _httpClient.DefaultRequestHeaders.Remove("adicionar-key-do-header-aqui");
                    _httpClient.DefaultRequestHeaders.Add("adicionar-key-do-header-aqui", listaHeaders[0]);
                }
                if (string.IsNullOrWhiteSpace(apiKey) == false)
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    _httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);
                }
                else
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                RetryPolicy.Execute(() =>
                {
                    response = _httpClient.PostAsync(url, new StringContent(item, encoding ?? Encoding.UTF8, mediaType)).Result;
                    return response;
                });

                return ResponseValidate(response);
            }
            catch (Exception ex)
            {
                retorno.Success = false;
                retorno.Situation = SituationEnum.Error;
                retorno.ErrorMessage = ex.Message;
                retorno.MainException = ex.InnerException;
                return retorno;
            }
        }

        // TODO:
        public OperationResult<string> ResponseValidate(HttpResponseMessage response)
        {
            var status = response?.StatusCode;
            var retorno = new OperationResult<string>() { Situation = SituationEnum.Success };

            return retorno;
        }
    }
}
