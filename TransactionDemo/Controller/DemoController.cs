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
using TransactionDemo.ViewModels;

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
            //remote url of ocelot API as middleware communication for TransactionAPI by lalji 31-08-2019 
            _settings = settings;
            _remoteUrl = $"{settings.Value.OcelotUrl}/api/Transaction";

        }

        [HttpPost]
        [Route("SellCancel")]
        public async Task<IActionResult> SellCancel([FromBody] ViewModels.TransactionCancelModel transactionCancelModel)
        {
            //Seller transaction cancel by lalji 31-08-2019
            HttpClient httpClient = new HttpClient();
            string cancelModelJson = JsonConvert.SerializeObject(transactionCancelModel);
            var stringContent = new StringContent(cancelModelJson);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpResponse = await httpClient.PostAsync($"{_remoteUrl}/SellerCancel", stringContent);
            httpResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            
            //convert responseBody to BizResponse by lalji 31-08-2019
            ViewModels.BizResponse bizResponse = JsonConvert.DeserializeObject<BizResponse>(responseBody);
            return Ok(bizResponse);
        }

        [HttpPost]
        [Route("BuyCancel")]
        public async Task<IActionResult> BuyCancel([FromBody] ViewModels.TransactionCancelModel transactionCancelModel)
        {
            //Buyer transaction cancel by lalji 31-08-2019
            HttpClient httpClient = new HttpClient();
            string cancelModelJson = JsonConvert.SerializeObject(transactionCancelModel);
            var stringContent = new StringContent(cancelModelJson);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpResponse = await httpClient.PostAsync($"{_remoteUrl}/BuyerCancel", stringContent);
            httpResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            //convert responseBody to BizResponse by lalji 31-08-2019
            ViewModels.BizResponse bizResponse = JsonConvert.DeserializeObject<BizResponse>(responseBody);
            return Ok(bizResponse);
        }

        [HttpPost]
        [Route("Sell")]
        public async Task<IActionResult> Sell([FromBody] ViewModels.TransactionModel transactionModel)
        {
            //Sell transaction by lalji 31-08-2019
            HttpClient httpClient = new HttpClient();
            string cancelModelJson = JsonConvert.SerializeObject(transactionModel);
            var stringContent = new StringContent(cancelModelJson);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpResponse = await httpClient.PostAsync($"{_remoteUrl}/SellTrade", stringContent);
            httpResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            
            //convert responseBody to BizResponse by lalji 31-08-2019
            ViewModels.BizResponse bizResponse = JsonConvert.DeserializeObject<BizResponse>(responseBody);
            return Ok(bizResponse);
        }
        [HttpPost]
        [Route("Buy")]
        public async Task<IActionResult> Buy([FromBody] ViewModels.TransactionModel transactionModel)
        {
            //Buy transaction  by lalji 31-08-2019
            HttpClient httpClient = new HttpClient();
            string cancelModelJson = JsonConvert.SerializeObject(transactionModel);
            var stringContent = new StringContent(cancelModelJson);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpResponse = await httpClient.PostAsync($"{_remoteUrl}/BuyTrade", stringContent);
            httpResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
           
            //convert responseBody to BizResponse by lalji 31-08-2019
            ViewModels.BizResponse bizResponse = JsonConvert.DeserializeObject<BizResponse>(responseBody);
            return Ok(bizResponse);
        }
    }
}