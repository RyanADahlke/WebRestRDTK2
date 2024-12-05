using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class AddressTypeDTO
    {
        public string AddressTypeId { get; set; } = null!;

       // public string AddressTypeDesc { get; set; } = null!;

        public string AddressTypeCrtdId { get; set; } = null!;

        public DateTime AddressTypeCrtdDt { get; set; }

        public string AddressTypeUpdtId { get; set; } = null!;

        public DateTime AddressTypeUpdtDt { get; set; }

    }
}
