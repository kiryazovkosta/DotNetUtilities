using CounterWebApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Data;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace CounterWebApi.Generators
{
    public class SampleCodeGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;

        protected override object? NextValue(EntityEntry entry)
        {
            if (entry is null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            var customer = entry.CurrentValues["Customer"];
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            string sequenceObjectName = GetSequenceObjectName(customer);

            var code = GetNextValue(entry, sequenceObjectName);

            return code;
        }

        private string GetSequenceObjectName(object customer)
        {
            var coustomerId = Convert.ToInt32(customer);

            if (coustomerId == 1)
            {
                return "OrdersNumber";
            }
            if (coustomerId == 2)
            {
                return "dbo.BillOfLandingsNumber";
            }

            return "dbo.OrdersNumber";

        }

        private int GetNextValue(EntityEntry entry, string sequenceObjectName)
        {
            var queryText = $"SELECT NEXT VALUE FOR {sequenceObjectName}";
            var sequenceObject = entry.Context.Database
                .SqlQueryRaw<int>(queryText)
                .ToList();
            if (!sequenceObject.Any())
            {
                throw new IndexOutOfRangeException(nameof(sequenceObject));
            }

            return sequenceObject.FirstOrDefault();
        }
    }
}
