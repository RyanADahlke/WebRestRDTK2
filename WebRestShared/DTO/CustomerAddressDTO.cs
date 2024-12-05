using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class CustomerAddressDTO
    {
        public string CustomerAddressId { get; set; } = null!;

        public string CustomerAddressCustomerId { get; set; } = null!;

        public string CustomerAddressAddressId { get; set; } = null!;

        public string CustomerAddressAddressTypeId { get; set; } = null!;

        public string CustomerAddressActvInd { get; set; }

        public string CustomerAddressDefaultInd { get; set; }

        public string CustomerAddressCrtdId { get; set; } = null!;

        public DateTime CustomerAddressCrtdDt { get; set; }

        public string CustomerAddressUpdtId { get; set; } = null!;

        public DateTime CustomerAddressUpdtDt { get; set; }

    }
}
