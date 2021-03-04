using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sharepoint.Business.Interfaces;
using Sharepoint.Business.Requests;

namespace Sharepoint.Api.NetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteCollectionController : ControllerBase
    {
        private readonly ILogger<SiteCollectionController> _logger;
        private readonly ISiteCollectionManager _siteCollectionManager;

        public SiteCollectionController(
            ILogger<SiteCollectionController> logger,
            ISiteCollectionManager siteCollectionManager)
        {
            _logger = logger;
            _siteCollectionManager = siteCollectionManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                var alias = Guid.NewGuid().ToString();
                var siteUrl = await _siteCollectionManager.CreateSiteCollectionAsync(new SiteCollectionRequest
                {
                    Alias = alias,
                    Title = alias
                });

                return Ok(siteUrl);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
