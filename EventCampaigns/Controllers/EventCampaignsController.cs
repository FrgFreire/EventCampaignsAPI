using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EventCampaigns.Model;
using EventCampaigns.Model.ErrorMessages;
using EventCampaigns.Interfaces;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace EventCampaigns.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventCampaignsController : ControllerBase
    {

        private readonly ILogger<EventCampaignsController> _logger;
        private readonly IEventCampaignsRepository _EventCampaignsRepository;

        public EventCampaignsController(IEventCampaignsRepository EventCampaignsRepository, ILogger<EventCampaignsController> logger)
        {
            _EventCampaignsRepository = EventCampaignsRepository;
            _logger = logger;
        }

        [HttpGet("EventCampaigns")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(List<EventCampaign>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorMessage), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorMessage), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EventCampaign>>> GetEventCampaignsAsync()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest);
            }

            try
            {
                IEnumerable<EventCampaign> returnData = await _EventCampaignsRepository.GetEventCampaigns();

                return Ok(returnData.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Exception(ex.Message, ex));
            }
        }

        [HttpGet("EventCampaignById")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(EventCampaign), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorMessage), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorMessage), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EventCampaign>> GetEventCampaignByIdAsync([Required][FromQuery]int eventCampaign)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest);
            }

            try
            {
                EventCampaign returnData = await _EventCampaignsRepository.GetEventCampaign(eventCampaign);

                return Ok(returnData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new Exception(ex.Message, ex));
            }
        }
    }
}
