using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class ProductStatusDTO
    {
        public string ProductStatusId { get; set; } = null!;

        public string ProductStatusDesc { get; set; } = null!;

        public string ProductStatusCrtdId { get; set; } = null!;

        public DateTime ProductStatusCrtdDt { get; set; }

        public string ProductStatusUpdtId { get; set; } = null!;

        public DateTime ProductStatusUpdtDt { get; set; }

    }
}