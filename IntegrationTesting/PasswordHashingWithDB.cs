using NUnit.Framework;
using IIG.PasswordHashingUtils;
using IIG.CoSFE.DatabaseUtils;

namespace KPI.IntegrationTesting
{
    class PasswordHashingWithDB
    {

        private const string server = @"DESKTOP-ET1TDS2\masha";
        private const string db = @"IIG.CoSWE.AuthDB";
        private const bool isTrusted = false;
        private const string login = @"masha";
        private const string password = @"masha";
        private const int conTime = 75;
        private AuthDatabaseUtils connection = new AuthDatabaseUtils(server, db, isTrusted, login, password, conTime);

        [Test]
        public void AddCredentials_WithLatinLogin()
        {
            Assert.IsTrue(connection.AddCredentials("Mariia", PasswordHasher.GetHash("MASHA", "salt", 2)));
        }
        
        [Test]
        public void AddCredentials_WithExistLogin()
        {
            Assert.IsFalse(connection.AddCredentials("Mariia", PasswordHasher.GetHash("mnfdr", "soul", 14)));
        }
        
        [Test]
        public void AddCredentials_WithNotLatinLogin()
        {
            Assert.IsTrue(connection.AddCredentials("Марія", PasswordHasher.GetHash("MASHA", "salt", 12)));
        }

        [Test]
        public void AddCredentials_WithSymbolLogin()
        {
            Assert.IsTrue(connection.AddCredentials("[]{}\\,./14@#!", PasswordHasher.GetHash("fff", "ooo", 2)));
        }

        [Test]
        public void AddCredentials_WithZeroLogin()
        {
            Assert.IsFalse(connection.AddCredentials("", PasswordHasher.GetHash("ttt", "salt", 65)));
        }

        [Test]
        public void AddCredentials_WithZeroPassword()
        {
            Assert.IsFalse(connection.AddCredentials("Vladimir", ""));
        }

        [Test]
        public void AddCredentials_WithSymbolPassword()
        {
            Assert.IsFalse(connection.AddCredentials("Kolya", " !@#$:;'./,][{}*&?^%~fghjkl;;lkjhgffghjkkjhgfghjhgfcdfrewqasdcvf"));
        }

        [Test]
        public void AddCredentials_WithNotLatinPassword()
        {
            Assert.IsFalse(connection.AddCredentials("Vika", "йййййййййййййййййййййййййййййййййййййййййййййййййййййййййййййййй"));
        }

        [Test]
        public void CheckCredentials_WithRightCredentials()
        {
            Assert.IsTrue(connection.CheckCredentials("Mariia", PasswordHasher.GetHash("MASHA", "salt", 2)));
        }

        [Test]
        public void CheckCredentials_WithWrongCredentials()
        {
            Assert.IsFalse(connection.CheckCredentials("Mariia", PasswordHasher.GetHash("MASHA", "salt", 12)));
        }

        [Test]
        public void UpdateCredentials_WithExistingCredentials()
        {
            Assert.IsTrue(connection.UpdateCredentials("Mariia", PasswordHasher.GetHash("MASHA", "salt", 2), "Masha",
                PasswordHasher.GetHash("NewPassword", "soul", 458)));
        }

        [Test]
        public void UpdateCredentials_WithNotExistingCredentials()
        {
            Assert.IsFalse(connection.UpdateCredentials("Mariia Dubyna", "Password", "Mari",
                PasswordHasher.GetHash("Password", "salt", 15)));
        }

        [Test]
        public void UpdateCredentials_WithWrongPassword()
        {
            Assert.IsFalse(connection.UpdateCredentials("Марія", PasswordHasher.GetHash("masha", "soul", 12), "Olexandra",
                PasswordHasher.GetHash("sasha", "salt", 20)));
        }

        [Test]
        public void DeleteCredentials_WithExistingCredentials()
        {
            Assert.IsTrue(connection.DeleteCredentials("Masha", PasswordHasher.GetHash("NewPassword", "soul", 458)));
        }

        [Test]
        public void DeleteCredentials_WithNotExistingCredentials()
        {
            Assert.IsFalse(connection.DeleteCredentials("Mariia Dubyna", "Password"));
        }

        [Test]
        public void DeleteCredentials_WithWrongPassword()
        {
            Assert.IsFalse(connection.DeleteCredentials("Марія", PasswordHasher.GetHash("masha", "soul", 12)));
        }
    }
}
