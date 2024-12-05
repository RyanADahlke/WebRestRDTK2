using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class ProductPriceDTO
    {
        public string ProductPriceId { get; set; } = null!;

        public string ProductPriceProductId { get; set; } = null!;

        public string ProductPriceEffDate { get; set; }

        public string ProductPricePrice { get; set; }

        public string ProductPriceCrtdId { get; set; } = null!;

        public DateTime ProductPriceCrtdDt { get; set; }

        public string ProductPriceUpdtId { get; set; } = null!;

        public DateTime ProductPriceUpdtDt { get; set; }

    }
}