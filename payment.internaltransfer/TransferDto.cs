using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payment.internaltransfer
{
    using Wr.API.DbLayer;

    public class TransferDto : Mapper
    {
        public string Id
        {
            get
            {
                return this.GetData(TransferEntity.ID);
            }
            set
            {
                this.SetData(TransferEntity.ID, value);
            }
        }

        public string CreateDate
        {
            get
            {
                return this.GetData(TransferEntity.CREATE_TIME);
            }
            set
            {
                this.SetData(TransferEntity.CREATE_TIME, value);
            }
        }

        public string UpdateDate
        {
            get
            {
                return this.GetData(TransferEntity.UPDATE_TIME);
            }
            set
            {
                this.SetData(TransferEntity.UPDATE_TIME, value);
            }
        }

        public string UserIdFrom
        {
            get
            {
                return this.GetData(TransferEntity.USER_ID_FROM);
            }
            set
            {
                this.SetData(TransferEntity.USER_ID_FROM, value);
            }
        }

        public string UserIdTo
        {
            get
            {
                return this.GetData(TransferEntity.USER_ID_TO);
            }
            set
            {
                this.SetData(TransferEntity.USER_ID_TO, value);
            }
        }

        public double Amount
        {
            get
            {
                return this.GetData(TransferEntity.AMOUNT);
            }
            set
            {
                this.SetData(TransferEntity.AMOUNT, value);
            }
        }

        public string Currency
        {
            get
            {
                return this.GetData(TransferEntity.CURRENCY);
            }
            set
            {
                this.SetData(TransferEntity.CURRENCY, value);
            }
        }

        public string Message
        {
            get
            {
                return this.GetData(TransferEntity.MESSAGE);
            }
            set
            {
                this.SetData(TransferEntity.MESSAGE, value);
            }
        }
    }
}
