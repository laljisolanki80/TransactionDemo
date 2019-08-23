using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.API.Application.Events;

namespace Transaction.API.Application.EventHandler
{
    public class TransactionSettleEventHandler
    {
        public event EventHandler<TransactionSettleEvent> RaiseSettleEvent;
        public void SettleMessage()
        {
            OnRaiseSettleEvent(new TransactionSettleEvent("Settlement is successfull"));
        }

        private void OnRaiseSettleEvent(TransactionSettleEvent transactionSettleEvent)
        {
            EventHandler<TransactionSettleEvent> handler = RaiseSettleEvent;
            if(handler!=null)
            {
                transactionSettleEvent.Message += $" at {DateTime.Now}";
                handler(this, transactionSettleEvent);
            }
        }
    }
}
