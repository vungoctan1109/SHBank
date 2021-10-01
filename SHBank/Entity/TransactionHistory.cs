using System;
using Google.Protobuf.WellKnownTypes;

namespace SHBank.Entity
{
    public class TransactionHistory
    {
        public string Id { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }

        public TransactionHistory()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            Status = 1;
        }

        public override string ToString()
        {
            return String.Format("|{0,37}|{1,37}|{2,37}|{3,4}|{4,10}|{5,15}|{6,22}|{7,6}|", Id,
                SenderAccountNumber, ReceiverAccountNumber, Type, Amount, Message, CreatedAt,
                Status);
        }
    }
}