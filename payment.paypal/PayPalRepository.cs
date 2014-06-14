using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payment.paypal
{
    using Wr.API.DbLayer;
    using Wr.API.GlobalDb;
    using Wr.Common;

    public class PayPalRepository : RepositoryBase
    {
        public bool CreatePayPal(IMapper mapper)
        {
            var payPal = new PayPalEntity();
            payPal.MergeData(mapper.PropertyDictionary);

            return payPal.Create();
        }

        public bool UpdatePayPal(IMapper mapper)
        {
            var payPal = new PayPalEntity();
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

            return payPal.Create2();
        }

        public IEntity GetPayPalBy(string internalPaymentId)
        {
            var entity = new PayPalEntity();
            var entityList = entity.GetBy(PayPalEntity.INTERNAL_PAYMENT_ID, internalPaymentId);
            if (entityList.Count() > 0)
            {
                return entityList[0];
            }
            return null;
        }

        public IEntity GetPayPalById(string id)
        {
            var entity = new PayPalEntity();
            var entityList = entity.GetBy(PayPalEntity.ID, id);
            if (entityList.Count() > 0)
            {
                return entityList[0];
            }
            return null;
        }

        public IList<IEntity> GetPayPalCollection(string userId)
        {
            var payPal = new PayPalEntity();
            return payPal.GetBy(PayPalEntity.USER_ID, userId);
        }

        protected override void InitializeComponent()
        {
            this.ClassMemberDictionary.TryAdd("UpdatePayPal", new MethodDelegate_ExecuteBy5(UpdatePayPal));
            this.ClassMemberDictionary.TryAdd("CreatePayPal", new MethodDelegate_ExecuteBy5(CreatePayPal));
            this.ClassMemberDictionary.TryAdd("GetPayPalBy", new MethodDelegate_Execute3(GetPayPalBy));
            this.ClassMemberDictionary.TryAdd("GetPayPalById", new MethodDelegate_Execute3(GetPayPalById));
            this.ClassMemberDictionary.TryAdd("GetPayPalCollection", new MethodDelegate_GetListBy(GetPayPalCollection));
        }
    }
}
