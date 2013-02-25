using System.Linq;
using MailSendbox.Models.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSendbox.Tests.Models.Repositories
{
    [TestClass]
    public class MailRepositoryTests
    {
        [TestMethod]
        public void Test_Get()
        {
            var mockedPop3Client = new MockedPop3Client(3);
            var mailRepo = new MailRepository(mockedPop3Client);

            Assert.AreEqual(3, mailRepo.Get().Count());
        }
    }
}