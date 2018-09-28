using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB
{
    class Transaction
    {
        public string id { get; set; }
        public string Operator { get; set; }
        public string Provider { get; set; }
        public string Region { get; set; }
        public string Mobile { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string ExecutionTime { get; set; }
        public DateTime CreatedDate { get; set; }

        string[] Operators = new string[] { "JIO", "Vodafone", "Idea","Airtel" };
        string[] Providers = new string[] { "Euronet", "CyberPlat", "Paytm" };
        string[] Regions = new string[] { "Gujarat", "Maharashtra", "Telengana" };
        string[] Mobiles = new string[] { "9999999999", "8888888888", "7777777777" };
        string[] Amounts = new string[] { "50", "100", "200","500" };
        string[] ExecutionTimes = new string[] { "100 ms", "200 ms", "500 ms" };
        string[] Statuses = new string[] { "Fail", "Success", "Pending"};
        public Transaction()
        {
            
        }

        public Transaction GetDefaultEntity(Transaction txn)
        {
            Random random = new Random();
            txn.id = Convert.ToString(Guid.NewGuid());
            txn.Operator = Operators[random.Next(Operators.Length)];
            txn.Provider = Providers[random.Next(Providers.Length)];
            txn.Region = Regions[random.Next(Regions.Length)];
            txn.Mobile = Mobiles[random.Next(Mobiles.Length)];
            txn.Amount = Amounts[random.Next(Amounts.Length)];
            txn.Status = Statuses[random.Next(Statuses.Length)];
            txn.ExecutionTime = ExecutionTimes[random.Next(ExecutionTimes.Length)];
            txn.CreatedDate = DateTime.Now;

            return txn;
        }

    }
}
