using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentUpdate.modal
{
    public class Invoice
    {
        public string invoiceGuid { get; set; }
        public string invoiceNumber { get; set; }
        public string invoiceStatus { get; set; }
        public string webSlug { get; set; }
        public string customerName { get; set; }
        public string emailAddress { get; set; }
        public object businessRefNumber1 { get; set; }
        public object description { get; set; }
        public string invoiceUrl { get; set; }
        public object redirectSuccess { get; set; }
        public object redirectFailed { get; set; }
        public object redirectCancel { get; set; }
        public double totalAmount { get; set; }
        public List<InvoiceDetailInfo> invoiceDetailInfos { get; set; }
    }
    public class InvoiceDetailInfo
    {
        public string description { get; set; }
        public object businessRefNumber1 { get; set; }
        public int quantity { get; set; }
        public double unitAmount { get; set; }
        public double totalAmount { get; set; }
        public string revenueCode { get; set; }
    }
}
