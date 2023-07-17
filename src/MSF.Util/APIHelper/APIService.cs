using APIShell.Services.Utils;
using MSF.Util.OperationResult;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.APIHelper
{
    public class APIService
    {
        public APIHelper APIHelper { get; set; }
        private readonly string _urlBase = "";
        private readonly string _urlEndpoint = "";

        public APIService()
        {
            APIHelper = new APIHelper();
        }

        public OperationResult<string> GetExemplo(string requestExemplo)
        {
            var retorno = new OperationResult<string>() { Situation = SituationEnum.Success };

            try
            {
                var urlOrganizada = _urlBase + _urlEndpoint + $"?parametro1={requestExemplo}&parametro2={requestExemplo}";

                retorno = APIHelper.Get(url: urlOrganizada, token: "");

                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Success = false;
                retorno.Situation = SituationEnum.Error;
                retorno.MainException = ex.InnerException;
                retorno.ErrorMessage = $"{nameof(GetExemplo)} exception - { ex.Message}";
                return retorno;
            }
        }

        public OperationResult<string> PostExemplo(string requestExemplo, string token)
        {
            var retorno = new OperationResult<string>() { Situation = SituationEnum.Success };

            try
            {
                var urlOrganizada = _urlBase + _urlEndpoint;

                var parametrosModel = new APIModel() { Parametro = requestExemplo };
                var parametros = JsonConvert.SerializeObject(parametrosModel);

                retorno = APIHelper.Post(url: urlOrganizada, item: parametros, token: token);

                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Success = false;
                retorno.Situation = SituationEnum.Error;
                retorno.MainException = ex.InnerException;
                retorno.ErrorMessage = $"{nameof(PostExemplo)} exception - {ex.Message}";
                return retorno;
            }
        }
    }
}
