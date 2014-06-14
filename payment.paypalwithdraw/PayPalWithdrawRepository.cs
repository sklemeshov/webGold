using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payment.paypalwithdraw
{
    using Wr.API.DbLayer;
    using Wr.API.GlobalDb;
    using Wr.Common;

    public class PayPalWithdrawRepository : RepositoryBase
    {
        public bool CreatePayPal(IMapper mapper)
        {
            var payPal = new PayPalWithdrawEntity();
            payPal.MergeData(mapper.PropertyDictionary);

            return payPal.Create();
        }

        public bool UpdatePayPal(IMapper mapper)
        {
            var payPal = new PayPalWithdrawEntity();
            payPal.MergeData(mapper.PropertyDictionary);

            var payPalEntity = GetPayPalBy(payPal.InternalPaymentId);
            if (payPalEntity != null)
            {
                payPal.SetData("id", payPalEntity.PropertyDictionary["id"]);
            }
            else
            {
                return false;
            }

            return payPal.Create();
        }

        public IEntity GetPayPalBy(string internalPaymentId)
        {
            var entity = new PayPalWithdrawEntity();
            var entityList = entity.GetBy(PayPalWithdrawEntity.INTERNAL_PAYMENT_ID, internalPaymentId);
            if (entityList.Count() > 0)
            {
                return entityList[0];
            }
            return null;
        }

        public IEntity GetPayPalById(string id)
        {
            var entity = new PayPalWithdrawEntity();
            var entityList = entity.GetBy(PayPalWithdrawEntity.ID, id);
            if (entityList.Count() > 0)
            {
                return entityList[0];
            }
            return null;
        }

        public IList<IEntity> GetPayPalCollection(string userId)
        {
            var payPal = new PayPalWithdrawEntity();
            return payPal.GetBy(PayPalWithdrawEntity.USER_ID, userId);
        }

        public IList<IEntity> GetPayPalWithdrawCollection(string state)
        {
            var payPal = new PayPalWithdrawEntity();
            return payPal.Get();
            //return payPal.GetBy(PayPalWithdrawEntity.STATE, state);
        }

        public IList<IEntity> GetPayPalProcessedWithdrawCollection(string state)
        {
            var payPal = new PayPalWithdrawEntity();
            //ToDo:
            return payPal.GetBy(PayPalWithdrawEntity.STATE, "failure");
        }


        protected override void InitializeComponent()
        {
            this.ClassMemberDictionary.TryAdd("UpdatePayPal", new MethodDelegate_ExecuteBy5(UpdatePayPal));
            this.ClassMemberDictionary.TryAdd("CreatePayPal", new MethodDelegate_ExecuteBy5(CreatePayPal));
            this.ClassMemberDictionary.TryAdd("GetPayPalBy", new MethodDelegate_Execute3(GetPayPalBy));
            this.ClassMemberDictionary.TryAdd("GetPayPalById", new MethodDelegate_Execute3(GetPayPalById));
            this.ClassMemberDictionary.TryAdd("GetPayPalCollection", new MethodDelegate_GetListBy(GetPayPalCollection));
            this.ClassMemberDictionary.TryAdd("GetPayPalWithdrawCollection", new MethodDelegate_GetListBy(GetPayPalWithdrawCollection));
            this.ClassMemberDictionary.TryAdd("GetPayPalProcessedWithdrawCollection", new MethodDelegate_GetListBy(GetPayPalProcessedWithdrawCollection));
            
        }
    }
}
