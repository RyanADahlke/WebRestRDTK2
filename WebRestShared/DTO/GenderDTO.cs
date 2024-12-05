using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestShared.DTO
{
    public partial class GenderDTO
    {
        public string GenderId { get; set; } = null!;

        public string GenderName { get; set; } = null!;

        public string GenderCrtdId { get; set; } = null!;

        public DateTime GenderCrtdDt { get; set; }

        public string GenderUpdtId { get; set; } = null!;

        public DateTime GenderUpdtDt { get; set; }

    }
}
