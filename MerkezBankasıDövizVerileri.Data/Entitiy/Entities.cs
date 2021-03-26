using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MerkezBankasıDövizVerileri.Data.Entitiy
{
    public class Entities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string USD { get; set; }
        public string AUD { get; set; }
        public string EUR { get; set; }
        public string GBP { get; set; }
        public string CreateDate { get; set; }

    }
}
