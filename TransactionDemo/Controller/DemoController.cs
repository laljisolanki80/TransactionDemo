using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace TransactionDemo.Controller
{
    [Produces("application/json")]
    [Route("api/Demo")]
    public class DemoController : ControllerBase
    {
        private readonly string _remoteUrl;
        private readonly IOptionsSnapshot<AppSettings> _settings;
        public DemoController(IOptionsSnapshot<AppSettings> settings)
        {
            _settings = settings;
            _remoteUrl = $"{settings.Value.OcelotUrl}/api/Transaction/SellerCancel";
            
        }

        [HttpPost]
        [Route("Cancel")]
        public async Task<IActionResult> Cancel([FromBody] TransactionCancelModel transactionCancelModel)
        {
            HttpClient httpClient = new HttpClient();
            string cancelModelJson = JsonConvert.SerializeObject(transactionCancelModel);
            var stringContent = new StringContent(cancelModelJson);
            stringContent.Headers.ContentType= new MediaTypeHeaderValue("application/json");

            var httpResponse = await httpClient.PostAsync(_remoteUrl, stringContent);
            httpResponse.Content.Headers.ContentType=new MediaTypeHeaderValue("application/json");
            httpResponse.EnsureSuccessStatusCode();
            string responseBody=await httpResponse.Content.ReadAsStringAsync();

            BizResponse bizResponse = JsonConvert.DeserializeObject<BizResponse>(responseBody);
            return Ok(bizResponse);
        }

    }

    public class TransactionCancelModel
    {
        public string Id { get; set; }
    }
    public class BizResponse
    {
        public enErrorCode ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

    }
    public enum enErrorCode
    {
        Success = 10001,
        InternalError = 10009,
        TransactionNotFoundError = 10002

    }
}