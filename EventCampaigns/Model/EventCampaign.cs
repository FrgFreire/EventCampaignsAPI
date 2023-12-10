using System.Data;

namespace EventCampaigns.Model
{
    public class EventCampaign
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? EffectiveDate { get; set; }

        public Campaign Campaign { get; set; }

        public EventCampaignType Type { get; set; }


        public EventCampaign(DataRow row) 
        {
            Id = Convert.ToInt32(row["Id"]);
            Title = row["Title"].ToString();
            bool dateConverted = DateTime.TryParse(row["EffectiveDate"].ToString(), out DateTime date);
            EffectiveDate = dateConverted ? date : null;
            Campaign = new Campaign()
            {
                Name = row["CampaignName"].ToString(),
                Description = row["CampaignDescription"].ToString(),
                Type = (CampaignType)Convert.ToInt32(row["CampaignType"]),
            };

            Type = (EventCampaignType)Convert.ToInt32(row["Type"].ToString());

        }
        public EventCampaign()
        {
            Id = 1;
            Title = "";
            EffectiveDate = new DateTime();
            Campaign = new Campaign()
            {
                Name = "",
                Description = "",
                Type = CampaignType.Simple
            };
            Type = EventCampaignType.Event;
        }
    }

    public enum EventCampaignType
    {
        Event = 0,
    }

}
