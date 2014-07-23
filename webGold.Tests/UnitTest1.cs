using Microsoft.VisualStudio.TestTools.UnitTesting;
using webGold.Repository;


namespace webGold.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string connection = "Data Source=54.235.73.25;Port=3306;Database=dev_wrio;uid=dev;pwd=164103148;";

            var entityCollection = new PaymentRepository().GetPaymentHistoryBy("b5947c58-ac7c-4132-9fa5-a386d6e3da4b");
        }
    }
}
