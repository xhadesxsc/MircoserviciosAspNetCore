using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RAEM.ApiSQL.Model;
using RAEM.ApiSQL.Services;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RAEM.ApiSQL.Helper
{
    public interface IReceiverTopicHelper
    {
        Task PreparaFiltrosMensaje();
        Task CierraSuscripcionClient();
    }
    public class ReceiverTopicHelper: IReceiverTopicHelper
    {
        private readonly ParametroConfig config;
        private readonly ILogger<ReceiverTopicHelper> logger;
        private readonly IProcesarData data;
        private readonly SubscriptionClient subscriptionClient;
        public ReceiverTopicHelper(
            IOptions<ParametroConfig> config,
            ILogger<ReceiverTopicHelper> logger,
            IProcesarData data)
        {
            this.config = config.Value;
            this.logger = logger;
            this.data = data;
            this.subscriptionClient = new SubscriptionClient(this.config.BusUrl,
                this.config.BusTopic, this.config.BusTopicSubscriptor);
        }

        public async Task CierraSuscripcionClient()
        {
            await subscriptionClient.CloseAsync();
        }

        public async Task PreparaFiltrosMensaje()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceiveHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            };
            subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

        }

        private Task ExceptionReceiveHandler(ExceptionReceivedEventArgs arg)
        {
            logger.LogError(arg.Exception, "No se pudo recibir el mensaje");
            var context = arg.ExceptionReceivedContext;

            logger.LogDebug($" Endpoint: { context.Endpoint }");
            logger.LogDebug($" Action: { context.Action }");

            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(Message arg1, CancellationToken arg2)
        {
            var myPayload = JsonConvert.DeserializeObject<Compra>
                (Encoding.UTF8.GetString(arg1.Body));
            data.Procesar(myPayload);

            await subscriptionClient.CompleteAsync(arg1.SystemProperties.LockToken);

        }
    }
}
