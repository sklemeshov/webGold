using System.Collections.Generic;
using webGold.Repository.Entity;

namespace webGold.Repository
{
    public interface IPaymentRepository
    {
        IList<PaymentHistory> GetPaymentHistoryBy(string userId);
        double GetAmmountPayPalPorLastDay(string userId);
        double GetAmmountTransferPorLastDay(string userId);
        void DeletePaymentHistoryBy(IList<string> paymentHistoryIdCollection);
        void UpdatePaymenStatus(string userId,string transactionStatus);
        void CreatePaymentHistory(PaymentHistory entity);
        Account GetAccountBy(string userId);
        void CreateAccount(Account entity);
        void UpdateAccount(Account entity);
        void UpdateAccount(double usdAmount, string userId);
        void CreatePayPalTransaction(PayPal entity);
        bool UpdatePayPal(PayPal payPal);
        void UpdatePayPalTransaction(PayPal payPal);
        PayPal GetPayPalBy(string token);
        void DeletPayPalTransactionBy(string id);
        PaymentHistory GetLastTransaction(string userId);
    }
}