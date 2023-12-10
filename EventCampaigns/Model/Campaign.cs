namespace EventCampaigns.Model
{
    public class Campaign
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public CampaignType Type { get; set; }
    }

    public enum CampaignType
    {
        Simple = 0
    }
}
