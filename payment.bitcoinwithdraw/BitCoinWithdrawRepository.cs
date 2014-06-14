using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payment.bitcoinwithdraw
{
    using Wr.API.DbLayer;
    using Wr.API.GlobalDb;
    using Wr.Common;

    public class BitCoinWithdrawRepository : RepositoryBase
    {
        public bool CreateBitCoin(IMapper mapper)
        {
            var bitCoin = new BitCoinWithdrawEntity();
            bitCoin.MergeData(mapper);

            return bitCoin.Create();
        }

        public bool UpdateBitCoin(IMapper mapper)
        {
            var bitCoin = new BitCoinWithdrawEntity();
            bitCoin.MergeData(mapper.PropertyDictionary);

            var payPalEntity = GetBitCoinBy(bitCoin.InternalPaymentId);
            if (payPalEntity != null)
            {
                bitCoin.SetData("id", payPalEntity.PropertyDictionary["id"]);
            }
            else
            {
                return false;
            }

            return bitCoin.Create();
        }

        public IEntity GetBitCoinBy(string internalPaymentId)
        {
            var entity = new BitCoinWithdrawEntity();
            var entityList = entity.GetBy(BitCoinWithdrawEntity.INTERNAL_PAYMENT_ID, internalPaymentId);
            if (entityList.Count() > 0)
            {
                return entityList[0];
            }
            return null;
        }

        protected override void InitializeComponent()
        {
            this.ClassMemberDictionary.TryAdd("CreateBitCoin", new MethodDelegate_ExecuteBy5(CreateBitCoin));
            this.ClassMemberDictionary.TryAdd("UpdateBitCoin", new MethodDelegate_ExecuteBy5(UpdateBitCoin));
            this.ClassMemberDictionary.TryAdd("GetBitCoinBy", new MethodDelegate_Execute3(GetBitCoinBy));
           
        }
    }
}
