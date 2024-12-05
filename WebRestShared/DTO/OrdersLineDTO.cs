using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class OrdersLineDTO
    {
        public string OrdersLineId { get; set; } = null!;

        public string OrdersLineOrdersId { get; set; } = null!;

        public string OrdersLineProductId { get; set; } = null!;

        public string OrdersLineQty { get; set; }

        public string OrdersLinePrice { get; set; }

        public string OrdersLineCrtdId { get; set; } = null!;

        public DateTime OrdersLineCrtdDt { get; set; }

        public string OrdersLineUpdtId { get; set; } = null!;

        public DateTime OrdersLineUpdtDt { get; set; }

    }
}