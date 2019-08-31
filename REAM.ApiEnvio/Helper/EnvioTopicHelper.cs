using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using REAM.ApiEnvio.Config;
using REAM.ApiEnvio.Model;
using System;
using Microsoft.Azure.ServiceBus;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace REAM.ApiEnvio.Helper
{
    public class EnvioTopicHelper
    {
        private readonly ParametroConfig config;
        private readonly ILogger<EnvioTopicHelper> logger;

        private readonly TopicClient topicClient;
        public EnvioTopicHelper(
            IOptions<ParametroConfig> config,
            ILogger<EnvioTopicHelper> logger
            )
        {
            this.config = config.Value;
            this.logger = logger;
            topicClient = new TopicClient(this.config.BusUrl, this.config.BusTopic);
        }


        public async Task SendMessage(CompraRequest payload)
        {
            //serializarlo a JSON
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));
            try
            {
                await topicClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

        }
    }
}
