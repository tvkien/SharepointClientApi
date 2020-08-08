using SharepointClientApi.Domains.Abstractions;
using SharepointClientApi.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace SharepointClientApi.Controllers
{
    public class SiteCollectionController : ApiController
    {
        private readonly ISiteCreation siteCreation;

        public SiteCollectionController(ISiteCreation siteCreation)
        {
            this.siteCreation = siteCreation;
        }

        [Route("api/spo/CreateComunicateSite")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateComunicateSiteAsync([FromBody] CreateComunicateSiteRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var siteUrl = await siteCreation.CreateCommunicationSiteAsync(request.CommunicationSiteRequest());

                return Ok(siteUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}