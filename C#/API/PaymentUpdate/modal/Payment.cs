using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentUpdate.modal
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AccountDetail
    {
        public string businessName { get; set; }
        public int prefixTypeID { get; set; }
        public string prefixType { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public int suffixTypeID { get; set; }
        public string suffixType { get; set; }
        public string alternateLastName { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public int stateID { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public int countryID { get; set; }
        public string country { get; set; }
        public int countyID { get; set; }
        public string county { get; set; }
        public int preferredPhoneTypeID { get; set; }
        public string preferredPhoneType { get; set; }
        public string phone1 { get; set; }
        public string extn1 { get; set; }
        public string phone2 { get; set; }
        public string extn2 { get; set; }
        public string fax { get; set; }
        public string mobilePhone { get; set; }
        public string email { get; set; }
        public string webpage { get; set; }
        public bool inactive { get; set; }
        public int createdby { get; set; }
        public DateTime createdDatetime { get; set; }
    }

    public class Payment
    {
        public string transactionType { get; set; }
        public int licenseKey { get; set; }
        public int licenseTypeKey { get; set; }
        public int licenseSubTypeKey { get; set; }
        public string licenseType { get; set; }
        public string licenseSubType { get; set; }
        public string licenseNumber { get; set; }
        public DateTime licensePermitExpirationDate { get; set; }
        public int feeTypeID { get; set; }
        public string feeType { get; set; }
        public double feeAmount { get; set; }
        public int createdby { get; set; }
        public DateTime createdDatetime { get; set; }
        public bool sendForPayment { get; set; }
        public AccountDetail accountDetail { get; set; }
    }

    public class PaymentCollection
    {
        public List<Payment> payment { get; set; }
    }

    public class PaymentDeatils
    {
        public int transactionHeaderId { get; set; }
        public string action { get; set; }
        public int accountKey { get; set; }
        public DateTime paymentDate { get; set; }
        public string paymentConfirmationID { get; set; }
        public string paymentSource { get; set; }
        public string paymentReferenceNumber { get; set; }
        public string orderStatus { get; set; }
        public int createdby { get; set; }
        public DateTime createdDatetime { get; set; }
        public int modifiedBy { get; set; }
        public DateTime modifiedDateTime { get; set; }
        public string transactionIdentifier { get; set; }
        public string hostTransactionId { get; set; }
        public string hostAuthorizationCode { get; set; }
        public string paymentReceiptConfirmation { get; set; }
        public int paymentApprovalStatus { get; set; }
        public double amount { get; set; }
        public double feeAmount { get; set; }
        public double totalRemitted { get; set; }
        public PaymentCollection paymentCollection { get; set; }
    }


}
