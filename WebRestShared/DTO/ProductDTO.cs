using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class ProductDTO
    {
        public string ProductId { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string ProductDesc { get; set; } = null!;

        public string ProductCrtdId { get; set; } = null!;

        public DateTime ProductCrtdDt { get; set; }

        public string ProductUpdtId { get; set; } = null!;

        public DateTime ProductUpdtDt { get; set; }

    }
}