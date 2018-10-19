using Microsoft.Extensions.Logging;
using Ocuco.Application.Services.OcucoHub.LuxotticaRXO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Application.Services.OcucoHub.LuxotticaRXO
{
    public class LuxotticaRXOWSService : ILuxotticaRXOWSService
    {
        private readonly ILogger<LuxotticaRXOWSService> logger;

        private HTTPClient _HTTPClient;
        private BaseResponseEntity _WsResponse;
        private readonly IHttpClientFactory _httpClientFactory;

        public LuxotticaRXOWSService(ILogger<LuxotticaRXOWSService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            _httpClientFactory = httpClientFactory;
        } 




        public BaseResponseEntity CallCheckFrame(CheckFrameRequest model)
        {
            int iReturn = 511;

            _WsResponse = new BaseResponseEntity();
            _WsResponse.WebService = "RXOCheckFrame";
            _WsResponse.ErrorCode = iReturn;

            CheckFrameFullRequest _request = new CheckFrameFullRequest();


            _request.Kunnr = model.Kunnr;
            _request.Upc = model.Upc;

            _request.Address = "https://certi-my.luxottica.com:443/Stores-WS/RXOServiceImplService";
            _request.BasicAuth = true;
            _request.BasicAuthUsername = "B2BSalmoiraghi";
            _request.BasicAuthPassword = "Luxottica910";


            _HTTPClient = new HTTPClient(1);

            iReturn = _HTTPClient.InvokeRXOCheckFrame(_request);

            _WsResponse.ErrorCode = iReturn;
            _WsResponse.ErrorDescription = DecodeResponseCodes(iReturn);

            return _WsResponse;
        }


        public async Task<BaseResponseEntity> CallCheckFrameAsync(CheckFrameRequest model)
        {
            //var client = _httpClientFactory.CreateClient();
            //var result = await client.GetStringAsync("http://www.google.com");







            _WsResponse = new BaseResponseEntity();
            _WsResponse.WebService = "RXOCheckFrame";
            _WsResponse.ErrorCode = 1;

            return _WsResponse;
        }




        #region INTERNALS

        private string DecodeResponseCodes(int ValueToDecode)
        {
            string _strDecode = "";

            switch (ValueToDecode)
            {

                case 0:
                {
                    _strDecode = "OK";
                }
                break;
                case 1:
                {
                    _strDecode = "cannot buy";
                }
                break;
                case 2:
                {
                    _strDecode = "cannot provide RX Service";
                }
                break;
                case -1:
                {
                    _strDecode = "internal error";
                }
                break;
                case -2:
                {
                    _strDecode = "UPC Code not found";
                }
                break;
                case -3:
                {
                    _strDecode = "Customer not found";
                }
                break;

                case 408:
                {
                    _strDecode = "RequestTimeout";
                }
                break;

                case 500:
                {
                    _strDecode = "Internal Server Error";
                }
                break;

                case 501:
                {
                    _strDecode = "RXOCheckFrame Generic Error";
                }
                break;

                case 502:
                {
                    _strDecode = "RXOCheckOrder Generic Error";
                }
                break;

                case 503:
                {
                    _strDecode = "RXOOrder Generic Error";
                }
                break;

                default:
                    return _strDecode;
                    break;
            }

            return _strDecode;
        }


        #endregion INTERNALS

    }
}
