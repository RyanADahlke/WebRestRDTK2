using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class OrderStatusDTO
    {
        public string OrderStatusId { get; set; } = null!;

        public string OrderStatusDesc { get; set; } = null!;

        public string OrderStatusNextOrderStatusId { get; set; } = null!;

        public string OrderStatusCrtdId { get; set; } = null!;

        public DateTime OrderStatusCrtdDt { get; set; }

        public string OrderStatusUpdtId { get; set; } = null!;

        public DateTime OrderStatusUpdtDt { get; set; }

    }
}
