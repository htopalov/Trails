using NUnit.Framework;
using Trails.Security;

namespace Trails.Test.SecurityProviderTests
{
    public class SecurityProviderTest
    {
        private readonly string ActualKey = "123456";
        private readonly string ExpectedHashedResult =
            "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92";

        private readonly int ExpectedCountOfCharsForKey = 64;

        [Test]
        public void KeyHasherShouldReturnCorrectResult()
        {
            var result = SecurityProvider.KeyHasher(ActualKey);
            Assert.AreEqual(ExpectedHashedResult,result);
        }

        [Test]
        public void KeyGeneratorShouldReturnCorrectCountOfCharacters()
        {
            var result = SecurityProvider.RandomBeaconKeyGenerator();
            Assert.AreEqual(ExpectedCountOfCharsForKey, result.Length);
        }
    }
}
