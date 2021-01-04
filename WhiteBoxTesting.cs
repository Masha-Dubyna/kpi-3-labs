using NUnit.Framework;
using System;

namespace KPI
{
    class WhiteBoxTesting
    {
        [Test]
        public void Init_SaltIsNull_ModIsZero()
        {
            string expected = PasswordHasher.GetHash("testString");
            PasswordHasher.Init(null, 0);
            Assert.AreEqual(expected, PasswordHasher.GetHash("testString"));
        }

        [Test]
        public void Init_SaltIsNotNull_ModIsZero()
        {
            string expected = PasswordHasher.GetHash("testString");
            PasswordHasher.Init("this is my new salt", 0);
            Assert.AreNotEqual(expected, PasswordHasher.GetHash("testString"));
        }

        [Test]
        public void Init_SaltIsNull_ModIsNotZero()
        {
            string expected = PasswordHasher.GetHash("testString");
            PasswordHasher.Init(null, 100);
            Assert.AreNotEqual(expected, PasswordHasher.GetHash("testString"));
        }

        [Test]
        public void Init_SaltIsNotNull_ModIsNotZero()
        {
            string expected = PasswordHasher.GetHash("testString");
            PasswordHasher.Init("this is my new salt", 100);
            Assert.AreNotEqual(expected, PasswordHasher.GetHash("testString"));
        }

        [Test]
        public void GetHash_WithNullablePassword()
        {
            Assert.Throws<ArgumentNullException>(() => PasswordHasher.GetHash(null));
        }

        [Test]
        public void GetHash_WithNotLatinPassword()
        {
            Assert.DoesNotThrow(() => PasswordHasher.GetHash("тестік"));
        }

        [Test]
        public void GetHash_WithEmptyPassword()
        {
            Assert.IsNotEmpty(PasswordHasher.GetHash(""));
        }

        [Test]
        public void GetHash_WithSamePasswords_OneParameter()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testString"), PasswordHasher.GetHash("testString"));
        }

        [Test]
        public void GetHash_WithDifferentPasswords_OneParameter()
        {
            Assert.AreNotEqual(PasswordHasher.GetHash("teststring"), PasswordHasher.GetHash("testString"));
        }

        [Test]
        public void GetHash_WithSameSalt_TwoParameters()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testString", "salt"), PasswordHasher.GetHash("testString", "salt"));
        }

        [Test]
        public void GetHash_WithDifferentSalt_TwoParameters()
        {
            Assert.AreNotEqual(PasswordHasher.GetHash("testString", "Salt"), PasswordHasher.GetHash("testString", "salt"));
        }

        [Test]
        public void GetHash_WithDefaultSalt()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testString"), PasswordHasher.GetHash("testString", "put your soul(or salt) here"));
        }

        [Test]
        public void GetHash_WithSameMod_ThreeParameters()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testString", null, 15), PasswordHasher.GetHash("testString", null, 15));
        }

        [Test]
        public void GetHash_WithDifferentMod_ThreeParameters()
        {
            Assert.AreNotEqual(PasswordHasher.GetHash("testString", null, 10), PasswordHasher.GetHash("testString", null, 15));
        }

        [Test]
        public void GetHash_WithDefaultSaltAndMod()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testString"), PasswordHasher.GetHash("testString", "put your soul(or salt) here", 65521));
        }

        [Test]
        public void GetHash_WithDefaultMod()
        {
            Assert.AreEqual(PasswordHasher.GetHash("testString", "my salt"), PasswordHasher.GetHash("testString", "my salt", 65521));
        }

    }

}
