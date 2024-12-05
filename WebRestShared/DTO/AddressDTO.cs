using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRestShared.DTO
{
    public  class AddressDTO
    {
        public string AddressId { get; set; } = null!;

        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }

        public string? AddressLine3 { get; set; }

        public string AddressCity { get; set; } = null!;
        public string AddressState { get; set; } = null!;
        public string AddressZip { get; set; } = null!;
        public string AddressCrtdId { get; set; } = null!;

        public DateTime AddressCrtdDt { get; set; }

        public string AddressUpdtId { get; set; } = null!;

        public DateTime AddressUpdtDt { get; set; }
    }
}
