using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Transaction
    {
        public int Id { get; set; }

        public DateTime
            Date { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public TransactionType Type { get; set; }

        public DateTime CreatedOn{get; set;}
    }

    public enum TransactionType
    {
        CREDIT, DEBIT
    }
}
