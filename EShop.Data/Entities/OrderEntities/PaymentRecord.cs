using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.OrderEntities
{
    public class PaymentRecord : BaseEntitiy
    {
        /// <summary>
        /// شناسه تراکنش نوینو
        /// </summary>
        public string trans_id { get; set; }

        /// <summary>
        /// شماره پیگیری بانک
        /// </summary>
        public string ref_id { get; set; }

        /// <summary>
        /// شناسه دیجیتال تراکنش
        /// </summary>
        public string authority { get; set; }

        /// <summary>
        /// شماره كارت پرداخت كننده بصورت Mask شده
        /// </summary>
        public string card_pan { get; set; }

        /// <summary>
        /// مبلغ (ریال)
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// شماره صورتحساب پذیرنده
        /// </summary>
        public string invoice_id { get; set; }

        /// <summary>
        /// ای پی پرداخت کننده
        /// </summary>
        public string buyer_ip { get; set; }

        /// <summary>
        /// زمان پرداخت (timestamp)
        /// </summary>
        public int payment_time { get; set; }
    }
}
