using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CreativeCravings.Models
{
    public class Post
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be betwee 2-100 characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Body must be betwee 2-1000 characters")]
        public string Body { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? DateUpdated { get; set; }
        
    }
}