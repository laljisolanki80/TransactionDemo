using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.IntegrationEvents
{
    public class BuyerIntegrationEventService : IBuyerIntegrationEventService
    {
        public Task AddAndSaveEventAsync(IntegrationEvent integrationEvent)
        {
            throw new NotImplementedException();
        }

        public Task PublishEventsThroughEventBusAsync()
        {
            throw new NotImplementedException();
        }
    }
}
