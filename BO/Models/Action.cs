using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BO.Models
{
    public class Action : IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ActionId { get; set; }

        [StringLength(100)]
        public string UserId { get; set; }

        [StringLength(200)]
        public string UserDisplayName { get; set; }

        [StringLength(50)]
        public string Message { get; set; }

        public string Platform { get; set; }

        public DateTime ActionDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public Guid UpdateBy { get; set; }
    }
}