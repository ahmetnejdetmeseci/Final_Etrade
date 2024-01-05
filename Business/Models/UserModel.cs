using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Business.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string UserName { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }


        #region Extras

        public string RoleOutput { get; set; }

        #endregion

    }
}
