using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class OrderStateDTO
    {
        public string OrderStateId { get; set; } = null!;

        public string OrderStateOrdersId { get; set; } = null!;

         public string OrderStateOrderStatusId { get; set; } = null!;

          public string OrderStateEffDate { get; set; }

        public string OrderStateCrtdId { get; set; } = null!;

        public DateTime OrderStateCrtdDt { get; set; }

        public string OrderStateUpdtId { get; set; } = null!;

        public DateTime OrderStateUpdtDt { get; set; }

    }
}
