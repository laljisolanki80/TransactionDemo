using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace TransactionDemo.Controller
{
    [Produces("application/json")]
    [Route("api/Demo")]
    public class DemoController : ControllerBase
    {
        private readonly string _remoteUrl;
        private readonly IOptionsSnapshot<appsettings> _settings;
        public DemoController(IOptionsSnapshot<appsettings> settings)
        {
            _remoteUrl = $"{settings.Value.OcelotUrl}/api/Transaction/BuyerCancel";
            _settings = settings;
        }

        [HttpPost]
        [Route("Cancel")]
        public async Task<IActionResult> Cancel([FromBody] string orderid)
        {

            var BaseAddress = _remoteUrl; // the remote url

            return null;
        }

    }
}