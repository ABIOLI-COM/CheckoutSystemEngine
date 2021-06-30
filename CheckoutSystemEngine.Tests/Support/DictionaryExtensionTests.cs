using System;
using System.Collections.Generic;

using Xunit;

namespace CheckoutSystemEngine.Tests
{
    public class DictionaryExtensionTests
    {
        private static readonly Guid guidA = Guid.NewGuid();
        private static readonly Guid guidB = Guid.NewGuid();
        private static readonly Guid guidC = Guid.NewGuid();
        private static readonly Guid guidD = Guid.NewGuid();

        private static Dictionary<Guid, decimal> CreateTestDictionary()
        {
            Dictionary<Guid, decimal> dictionary = new();

            dictionary.Add(guidA, 0);
            dictionary.Add(guidB, 1);
            dictionary.Add(guidC, 2);

            return dictionary;
        }

        [Fact]
        public void AddEntry_Works_WithNewEntry()
        {
            Dictionary<Guid, decimal> dictionary = CreateTestDictionary();

            dictionary.AddEntry(guidD, 32);

            Assert.Equal(32, dictionary[guidD]);
            Assert.Equal(4, dictionary.Count);
        }

        [Fact]
        public void AddEntry_Works_WithExistingEntryAtZero()
        {
            Dictionary<Guid, decimal> dictionary = CreateTestDictionary();

            dictionary.AddEntry(guidA, 32);

            Assert.Equal(32, dictionary[guidA]);
            Assert.Equal(3, dictionary.Count);
        }

        [Fact]
        public void AddEntry_Works_WithExistingEntryAtNonZero()
        {
            Dictionary<Guid, decimal> dictionary = CreateTestDictionary();

            dictionary.AddEntry(guidC, 32);

            Assert.Equal(34, dictionary[guidC]);
            Assert.Equal(3, dictionary.Count);
        }

        [Fact]
        public void AddEntry_Fails_IfKeyNull()
        {
            Dictionary<Guid, decimal> dictionary = CreateTestDictionary();

            var result = dictionary.AddEntry(Guid.Empty, 32);

            Assert.False(result.IsSuccess);
            Assert.Equal(StringConstants.DictionaryExtensions_KeyIsNull, result.ErrorMessage);
        }
    }
}
