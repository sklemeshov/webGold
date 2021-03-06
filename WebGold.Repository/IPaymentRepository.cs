﻿using System.Collections.Generic;
using webGold.Repository.Entity;

namespace webGold.Repository
{
    public interface IPaymentRepository
    {
        IList<Transaction> GetPaymentHistoryBy(string userId);
        double GetAmmountPayPalPorLastDay(string userId, int paymentMethod);
        double GetAmmountTransferPorLastDay(string userId);
        void DeletePaymentHistoryBy(IList<string> paymentHistoryIdCollection);
        void UpdatePaymenStatus(string userId, int transactionState, int payPalState);
        void CreatePaymentHistory(Transaction entity);
        Account GetAccountBy(string userId);
        void CreateAccount(Account entity);
        void UpdateAccount(Account entity);
        void UpdateAccount(double wrgAmount, string userId);
        void CreatePayPalTransaction(PayPal payPalentity, Transaction transactionEntity);
        bool UpdatePayPal(PayPal payPal);
        void UpdatePayPalTransaction(PayPal payPal);
        PayPal GetPayPalBy(string token);
        void DeletPayPalTransactionBy(string id);
        Transaction GetLastTransaction(string userId);
        void UpdateTransactionAmount(Transaction transaction);
        void CreateTransfer(Transfer entity, Transaction transactionEntity);
        void UpdateTransferStatus(Transfer entity);
        void DeleteTransferBy(string id);
    }
}
