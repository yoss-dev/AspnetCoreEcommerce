﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreEcommerce.Core.Domain.Catalog
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DateModified { get; set; }

    }
}
