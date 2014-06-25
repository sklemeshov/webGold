using webGold.Repository;

namespace webGold.Repository
{
    public static class RepositoryHelper
    {
        public static IPaymentRepository Initialize()
        {
            return new PaymentRepository();
        }
    }
}
