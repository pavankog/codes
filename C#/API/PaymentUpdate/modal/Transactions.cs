using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentUpdate.modal
{
    public class Transactions
    {
        public string transactionIdentifier { get; set; }
        public int userId { get; set; }
        public int accountId { get; set; }
        public int paymentTransactionId { get; set; }
        public int transactionHeaderId { get; set; }
        public string invoiceNumber { get; set; }
    }
}
