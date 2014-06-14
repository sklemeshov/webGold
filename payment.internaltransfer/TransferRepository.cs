using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payment.internaltransfer
{
    using Wr.API.DbLayer;
    using Wr.API.GlobalDb;
    using Wr.Common;

    public class TransferRepository : RepositoryBase
    {
        public bool CreateTransfer(IMapper mapper)
        {
            var transferEntity = new TransferEntity();
            transferEntity.MergeData(mapper.PropertyDictionary);

            return transferEntity.Create();
        }

        public IList<IEntity> GetTransferCollectionTo(string userId)
        {
            var transfer = new TransferEntity();
            return transfer.GetBy(TransferEntity.USER_ID_TO, userId);
        }

        public IList<IEntity> GetTransferCollectionFrom(string userId)
        {
            var transfer = new TransferEntity();
            return transfer.GetBy(TransferEntity.USER_ID_FROM, userId);
        }

        public IList<IEntity> GetTransferCollection()
        {
            return new TransferEntity().Get();
        } 

        protected override void InitializeComponent()
        {
            this.ClassMemberDictionary.TryAdd("CreateTransfer", new MethodDelegate_ExecuteBy5(CreateTransfer));
            this.ClassMemberDictionary.TryAdd("GetTransferCollectionTo", new MethodDelegate_GetListBy(GetTransferCollectionTo));
            this.ClassMemberDictionary.TryAdd("GetTransferCollectionFrom", new MethodDelegate_GetListBy(GetTransferCollectionFrom));
            this.ClassMemberDictionary.TryAdd("GetTransferCollection", new MethodDelegate_GetItemCollection(GetTransferCollection));
        }
    }
}
