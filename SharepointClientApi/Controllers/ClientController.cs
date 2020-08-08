using SharepointClientApi.Domains;
using SharepointClientApi.Domains.Abstractions;
using SharepointClientApi.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace SharepointClientApi.Controllers
{
    public class ClientController : ApiController
    {
        private readonly ISiteCreation siteCreation;
        private readonly IClientTeamHanlder clientTeamHanlder;

        public ClientController(ISiteCreation siteCreation, IClientTeamHanlder clientTeamHanlder)
        {
            this.siteCreation = siteCreation;
            this.clientTeamHanlder = clientTeamHanlder;
        }

        [Route("api/spo/ClientInvite")]
        [HttpPost]
        public async Task<IHttpActionResult> ClientInviteAsync([FromBody] ClientInviteRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var siteUrl = await siteCreation.CreateCommunicationSiteAsync(new CommunicationSiteRequest 
                { 
                    Alias = request.Alias,
                    Language = request.Language,
                    Title = request.Title
                });

                await clientTeamHanlder.Handle(new ClientTeamRequest
                {
                    SiteUrl = siteUrl,
                    DocumentName = request.DocumentName,
                    GroupName = request.GroupName,
                    Role = request.Role,
                    Users = request.Users
                });

                return Ok(siteUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}