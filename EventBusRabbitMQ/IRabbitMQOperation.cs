﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
    interface IRabbitMQOperation
    {
        string SendMessage();
    }
}