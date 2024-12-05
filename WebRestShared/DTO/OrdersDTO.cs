using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class OrdersDTO
    {
        public string OrdersId { get; set; } = null!;

        public string OrdersDate { get; set; }

        public string OrdersCustomerId { get; set; } = null!;

        public string OrdersCrtdId { get; set; } = null!;

        public DateTime OrdersCrtdDt { get; set; }

        public string OrdersUpdtId { get; set; } = null!;

        public DateTime OrdersUpdtDt { get; set; }

    }
}