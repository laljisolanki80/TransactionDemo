using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogService.Services;
using IntegrationEventLogService.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Infrastructure.Database;

namespace Transaction.API.Application.IntegrationEvents
{
    public class BuyerIntegrationEventService: IBuyerIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly TransactionDbContext _transactionDbContext;
        private readonly IntegrationEventLogContext _eventLogContext;
        private readonly IIntegrationEventLogService _eventLogService;

        public BuyerIntegrationEventService(IEventBus eventBus,
            TransactionDbContext transactionDbContext,
            IntegrationEventLogContext eventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _transactionDbContext = transactionDbContext ?? throw new ArgumentNullException(nameof(transactionDbContext));
            _eventLogContext = eventLogContext ?? throw new ArgumentNullException(nameof(eventLogContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_transactionDbContext.Database.GetDbConnection());
        }
        public async Task PublishEventsThroughEventBusAsync()
        {
            var pendindLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync();

            foreach (var logEvt in pendindLogEvents)
            {

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    _eventBus.Publish(logEvt.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {

                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            await _eventLogService.SaveEventAsync(evt, _transactionDbContext.GetCurrentTransaction.GetDbTransaction());
        }
    }
}
