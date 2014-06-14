using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payment.bitcoin
{
    using Wr.API.DbLayer;
    using Wr.API.GlobalDb;
    using Wr.Common;

    public class BitCoinRepository : RepositoryBase
    {
        public bool CreateBitCoin(IMapper mapper)
        {
            var bitCoin = new BitCoinEntity();
            bitCoin.MergeData(mapper);

            return bitCoin.Create();
        }

        public bool UpdateBitCoin(IMapper mapper)
        {
            //var bitCoin = new BitCoinEntity();
            //bitCoin.MergeData(mapper);

            BitCoinEntity bitCoinEntity = GetBitCoinById(mapper.GetData("id")) as BitCoinEntity;

            if (bitCoinEntity != null)
            {
                bitCoinEntity.MergeData(mapper);
            }
            else
            {
                return false;
            }

            return bitCoinEntity.Create();
        }

        public IEntity GetBitCoinBy(string internalPaymentId)
        {
            var entity = new BitCoinEntity();
            var entityList = entity.GetBy(BitCoinEntity.INTERNAL_PAYMENT_ID, internalPaymentId);
            if (entityList.Count() > 0)
            {
                return entityList[0];
            }
            return null;
        }

        public IEntity GetBitCoinById(string id)
        {
            var entity = new BitCoinEntity();
            var entityList = entity.GetBy(BitCoinEntity.ID, id);
            if (entityList.Count() > 0)
            {
                return entityList[0];
            }
            return null;
        }

        public IList<IEntity> GetBitCoinCollection(string userId)
        {
            var bitCoin = new BitCoinEntity();
            return bitCoin.GetBy(BitCoinEntity.USER_ID, userId);
        }

        protected override void InitializeComponent()
        {
            this.ClassMemberDictionary.TryAdd("UpdateBitCoin", new MethodDelegate_ExecuteBy5(UpdateBitCoin));
            this.ClassMemberDictionary.TryAdd("CreateBitCoin", new MethodDelegate_ExecuteBy5(CreateBitCoin));
            this.ClassMemberDictionary.TryAdd("GetBitCoinBy", new MethodDelegate_Execute3(GetBitCoinBy));
            this.ClassMemberDictionary.TryAdd("GetBitCoinById", new MethodDelegate_Execute3(GetBitCoinById));
            this.ClassMemberDictionary.TryAdd("GetBitCoinCollection", new MethodDelegate_GetListBy(GetBitCoinCollection));
        }
    }
}
