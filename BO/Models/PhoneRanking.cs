using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BO.Models
{
    public class PhoneRanking : IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int Ranking { get; set; }

        [StringLength(100)]
        public string DeviceName { get; set; }

        [StringLength(50)]
        public string OS { get; set; }

        public int Ram { get; set; }

        public int StorageSize { get; set; }

        public int CPU { get; set; }
        public int GPU { get; set; }
        public int MEM { get; set; }

        public int UX { get; set; }

        public int TotalScore { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public Guid UpdateBy { get; set; }
    }
}