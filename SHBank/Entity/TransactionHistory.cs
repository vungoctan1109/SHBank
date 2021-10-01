using System;
using Google.Protobuf.WellKnownTypes;

namespace SHBank.Entity
{
    public class TransactionHistory
    {
        public string Id { get; set; }
        public string SenderAccountNumber{ get; set; }
        public string ReceiverAccountNumber{ get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
    }
}