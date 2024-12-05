using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class CustomerDTO
    {
        public string CustomerId { get; set; } = null!;

        public string CustomerFirstName { get; set; } = null!;

        public string CustomerMiddleName { get; set; }

        public string CustomerLastName { get; set; } = null!;

        public string CustomerDateOfBirth { get; set; }

        public string CustomerCrtdId { get; set; } = null!;

        public DateTime CustomerCrtdDt { get; set; }

        public string CustomerUpdtId { get; set; } = null!;

        public DateTime CustomerUpdtDt { get; set; }

    }
}
