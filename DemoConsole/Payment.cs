using System;
using System.Collections.Generic;
using System.Text;

namespace DemoConsole
{
        [Serializable]
        public class Payment
        {
            public decimal AmountToPay;
            public string CardNumber;
            public string Name;
        }
    
}
