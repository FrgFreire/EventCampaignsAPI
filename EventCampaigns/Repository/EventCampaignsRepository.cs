using EventCampaigns.Interfaces;
using EventCampaigns.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace EventCampaigns.Repository
{
    public class EventCampaignsRepository : IEventCampaignsRepository
    {
        private readonly string _connectionString;
        public async Task<EventCampaign> GetEventCampaign(int eventCampaignId)
        {
#if DEBUG
            return new EventCampaign();
#else
            

            List<EventCampaign> eventCampaigns = new List<EventCampaign>();

            var dataTable = new DataTable
            {
                Locale = System.Globalization.CultureInfo.InvariantCulture
            };

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("GetEventCampaignById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.Add(new SqlParameter() { ParameterName = "@EventCampaignId", Value = eventCampaignId });
                            
                        connection.Open();
                        var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                            foreach (DataRow row in dataTable.Rows)
                            {
                                eventCampaigns.Add(new EventCampaign(row));
                            };

                        }

                        //close data reader
                        reader.Close();

                        //close connection
                        connection.Close();

                        return eventCampaigns.FirstOrDefault();
                    }
                }
            }
            catch (DBConcurrencyException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
#endif
        }

        public async Task<IEnumerable<EventCampaign>> GetEventCampaigns()
        {
#if DEBUG
            List<EventCampaign> eventCampaigns = new List<EventCampaign>();
            eventCampaigns.Add(new EventCampaign());
            eventCampaigns.Add(new EventCampaign());
            eventCampaigns.Add(new EventCampaign());
            return eventCampaigns;
#else

            List<EventCampaign> eventCampaigns = new List<EventCampaign>();

            var dataTable = new DataTable
            {
                Locale = System.Globalization.CultureInfo.InvariantCulture
            };

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("GetAllEventCampaigns", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                            foreach (DataRow row in dataTable.Rows)
                            {
                                eventCampaigns.Add(new EventCampaign(row));
                            };

                        }

                        //close data reader
                        reader.Close();

                        //close connection
                        connection.Close();

                        return eventCampaigns;
                    }
                }
            }
            catch (DBConcurrencyException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
#endif

        }
    }
}
