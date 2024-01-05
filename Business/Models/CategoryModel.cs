using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Business.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        #region Extras

        public int ProductCountOutput { get; set; }

        public string ProductsOutput { get; set; }

        #endregion

    }
}
