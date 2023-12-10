using EventCampaigns.Model;

namespace EventCampaigns.Interfaces
{
    public interface IEventCampaignsRepository
    {
        Task<IEnumerable<EventCampaign>> GetEventCampaigns();
        Task<EventCampaign> GetEventCampaign(int eventCampaign);
    }
}
