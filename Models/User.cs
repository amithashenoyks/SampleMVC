using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Tutorial.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(6, MinimumLength = 3)]
        [System.Web.Mvc.Remote("IsUserAvailable", "Validation")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        [Editable(true)]
        public string UserName { get; set; }
    }
}