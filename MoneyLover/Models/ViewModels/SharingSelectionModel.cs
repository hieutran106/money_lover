using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoneyLover.Models.ViewModels
{
    public class SharingSelectionModel
    {
        public string[] Ids { get; set; }
        public string[] Usernames { get; set; }
        public IEnumerable<AppUser> Stakeholders { get; set; }

        [Required]      
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

    }
 
    
    public class SharingModel
    {
        public IEnumerable<AppUser> Users;
        public decimal Total;
        public decimal Average;
    }
}
