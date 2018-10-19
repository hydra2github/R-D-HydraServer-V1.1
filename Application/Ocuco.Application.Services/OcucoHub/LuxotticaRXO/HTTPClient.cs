using Ocuco.Application.Services.OcucoHub.LuxotticaRXO.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Ocuco.Application.Services.OcucoHub.LuxotticaRXO
{
    public class HTTPClient
    {
        private BaseResponseEntity _AcuitasWsResponse;

        public HTTPClient(int iLoggingLevel)
        {
            //
            // prepare a response structure
            //
            _AcuitasWsResponse = new BaseResponseEntity();
            _AcuitasWsResponse.ErrorCode = -999;
            _AcuitasWsResponse.ErrorDescription = "";

            ////
            //// set logging level
            ////
            //_iLoggingLevel = iLoggingLevel;
        }


        public BaseResponseEntity WsResponse
        {
            get
            {
                return _AcuitasWsResponse;
            }
        }


        public int InvokeRXOCheckFrame(CheckFrameFullRequest _RXOCheckFrameRequest)
        {
            if (_RXOCheckFrameRequest == null)
                return 501;

            HttpWebRequest request = CreateWebRequest(_RXOCheckFrameRequest.Address, _RXOCheckFrameRequest);

            XmlDocument soapEnvelopeXml = new XmlDocument();

            string stringRequest = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:rxo=""http://rxo.webservices.luxottica.it/"">
                                        <soapenv:Header/>
                                        <soapenv:Body>
                                          <rxo:rxoCheckFrame>
                                            <arg0>{0}</arg0>
                                            <arg1>{1}</arg1>
                                          </rxo:rxoCheckFrame>
                                        </soapenv:Body>
                                      </soapenv:Envelope>";


            stringRequest = String.Format(stringRequest, _RXOCheckFrameRequest.Kunnr, _RXOCheckFrameRequest.Upc);

            soapEnvelopeXml.LoadXml(stringRequest);

            //
            // Logging
            //
            //if (_iLoggingLevel > (int)LoggingLevelEnums.None)
            //{
            //    ExceptionLoggingService.Instance.WriteRXOWSLog("HTTPClient.InvokeRXOCheckFrame", "rxoCheckFrame Request", soapEnvelopeXml.InnerXml.ToString());
            //}


            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        string soapResult = rd.ReadToEnd();
                        Console.WriteLine(soapResult);

                        //
                        // Logging
                        //
                        //if (_iLoggingLevel > (int)LoggingLevelEnums.None)
                        //{
                        //    ExceptionLoggingService.Instance.WriteRXOWSLog("HTTPClient.InvokeRXOCheckFrame", "rxoCheckFrame Response", soapResult);
                        //}

                        //
                        // Now parse the response
                        //
                        string strResponseCode = null;
                        XDocument doc = XDocument.Parse(soapResult);
                        XNamespace ns = "http://rxo.webservices.luxottica.it/";
                        IEnumerable<XElement> responses = doc.Descendants(ns + "rxoCheckFrameResponse");
                        foreach (XElement _element in responses)
                        {
                            strResponseCode = (string)_element.Element("return");
                        }

                        if (strResponseCode != null)
                        {
                            int iReturnFromResponse = 500;
                            Int32.TryParse(strResponseCode, out iReturnFromResponse);
                            return iReturnFromResponse;
                        }
                        else
                            return 501;
                    }
                }

            }
            //catch (HttpException httpex)
            //{
            //    return 501;
            //}
            catch (WebException webex)
            {
                if (webex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = webex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Console.WriteLine("HTTP Status Code: " + (int)response.StatusCode);
                        return (int)response.StatusCode;
                    }
                    else
                    {
                        // no http status code available
                    }
                }
                else
                {
                    // no http status code available
                }
            }
            catch (Exception ex)
            {
                return 501;
            }

            return 0;
        }



        #region INTERNALS


        private HttpWebRequest CreateWebRequest(string url, BaseRequestEntity _ReqDetails)
        {
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
            //                                                | System.Net.SecurityProtocolType.Tls
            //                                                | System.Net.SecurityProtocolType.Tls11
            //                                                | System.Net.SecurityProtocolType.Tls12;

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls
                                                            | System.Net.SecurityProtocolType.Tls11
                                                            | System.Net.SecurityProtocolType.Tls12;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            //
            // Soap action
            //

            //webRequest.Headers.Add(@"SOAP:Action");
            //webRequest.Headers.Add("SOAPAction", "http://webservices.luxottica.it/RXOService/rxoCheckOrderRequest");

            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            //
            // Set HttpWebRequest timeout
            //

            if (_ReqDetails.HttpRequest > 0)
            {
                webRequest.Timeout = (_ReqDetails.HttpRequest * 1000);
            }

            //
            // Add Basic Authentication
            //
            if (_ReqDetails.BasicAuth)
            {
                string username = _ReqDetails.BasicAuthUsername;
                string password = _ReqDetails.BasicAuthPassword;

                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

                webRequest.Headers.Add("Authorization", "Basic " + svcCredentials);
            }


            //
            // eventually manage certificates
            //

            //X509Certificate Cert = X509Certificate.CreateFromCertFile("C:\\myluxotticacom.crt");
            //AttachClientCertificate(webRequest, "a", "b");


            return webRequest;
        }


        private HttpWebRequest CreateWebRequestV2(string url, BaseRequestEntity _ReqDetails)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
                                                            | System.Net.SecurityProtocolType.Tls
                                                            | System.Net.SecurityProtocolType.Tls11
                                                            | System.Net.SecurityProtocolType.Tls12;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            //
            // Soap action
            //
            //webRequest.Headers.Add(@"SOAP:Action");
            webRequest.Headers.Add("SOAPAction", "http://webservices.luxottica.it/RXOService/rxoOrderRequest");

            //
            //set compression
            //
            webRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";

            //
            //set connection properties
            //
            webRequest.KeepAlive = true;  //15 sec on server side
            webRequest.Accept = "text/xml";

            //
            //set verb
            //
            webRequest.Method = "POST";

            //
            // Set HttpWebRequest timeout
            //
            if (_ReqDetails.HttpRequest > 0)
            {
                //webRequest.Timeout = Timeout.Infinite;
                webRequest.Timeout = (_ReqDetails.HttpRequest * 1000);
            }

            //
            // Add Basic Authentication
            //
            if (_ReqDetails.BasicAuth)
            {
                string username = _ReqDetails.BasicAuthUsername;
                string password = _ReqDetails.BasicAuthPassword;

                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

                webRequest.Headers.Add("Authorization", "Basic " + svcCredentials);
            }

            return webRequest;
        }



        #endregion INTERNALS   

    }
}
