﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Liquid.Core.Configuration;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Messaging.Configuration;
using Liquid.Messaging.RabbitMq.Attributes;
using Liquid.Messaging.RabbitMq.Configuration;
using Liquid.Messaging.RabbitMq.Tests.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Liquid.Messaging.RabbitMq.Tests.Consumers
{
    /// <summary>
    /// RabbitMqTestEventConsumer Class.
    /// </summary>
    /// <seealso>
    ///     <cref>Liquid.Messaging.RabbitMq.RabbitMqConsumer{Liquid.Messaging.RabbitMq.Tests.Messages.RabbitMqTestMessage}</cref>
    /// </seealso>
    [RabbitMqConsumer("TestRabbitMq", "TestMessageTopic", "TestMessageSubscription")]
    public class RabbitMqTestEventConsumer : RabbitMqConsumer<RabbitMqTestMessage>
    {
        /// <summary>
        /// Gets the rabbit mq settings.
        /// </summary>
        /// <value>
        /// The rabbit mq settings.
        /// </value>
        public override RabbitMqSettings RabbitMqSettings => new RabbitMqSettings
        {
            ExchangeType = "topic",
            AutoAck = false,
            RequestHeartBeatInSeconds = 60,
            Expiration = "60000",
            Global = false,
            Persistent = false,
            PrefetchCount = 10,
            Prefetch = 0,
            AutoDelete = false,
            Durable = false,
            QueueArguments = null,
            ExchangeArguments = null
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqTestEventConsumer"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="telemetryFactory">The telemetry factory.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="messagingSettings">The messaging settings.</param>
        public RabbitMqTestEventConsumer(IServiceProvider serviceProvider, 
                                           IMediator mediator, 
                                           IMapper mapper, 
                                           ILightContextFactory contextFactory, 
                                           ILightTelemetryFactory telemetryFactory, 
                                           ILoggerFactory loggerFactory, 
                                           ILightConfiguration<List<MessagingSettings>> messagingSettings) : base(serviceProvider, mediator, mapper, contextFactory, telemetryFactory, loggerFactory, messagingSettings)
        {
        }

        /// <summary>
        /// Consumes the message from  subscription asynchronous.
        /// </summary>
        /// <param name="message">The message to be consumed.</param>
        /// <param name="headers">The custom headers of message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public override async Task<bool> ConsumeAsync(RabbitMqTestMessage message, IDictionary<string, object> headers, CancellationToken cancellationToken)
        {
            RabbitMqTestMessage.Self = message;
            return await Task.FromResult(true);
        }
    }
}