using System;
using System.Collections.Generic;

namespace CheckoutSystemEngine
{
    public static class DictionaryExtensions
    {
        public static ExecutionResult<Unit> AddEntry(this Dictionary<Guid, decimal> dictionary, Guid guid, decimal value)
        {
            if (guid == Guid.Empty)
            {
                return ExecutionResult<Unit>.CreateFailure(StringConstants.DictionaryExtensions_KeyIsNull);
            }

            if (!dictionary.ContainsKey(guid))
            {
                dictionary.Add(guid, 0);
            }

            dictionary[guid] += value;

            return ExecutionResult<Unit>.CreateSuccess(Unit.Default);
        }
    }
}
