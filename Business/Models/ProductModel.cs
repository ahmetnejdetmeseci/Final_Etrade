using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Business.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [DisplayName("Discontinued")]
        public bool IsDiscontinued { get; set; }

        public int CategoryId { get; set; }


        #region Extras
        [DisplayName("Unit Price")]
        public string UnitPriceOutput { get; set; }

        [DisplayName("Expiration Date")]
        public string ExpirationDateOutput { get; set; }

        [DisplayName("Discontinued")]
        public string IsContinuedOutput { get; set; }

        [DisplayName("Category")]
        public string CategoryOutput { get; set; }

        [DisplayName("Stores")]
        public string StoresOutput { get; set; }

        [DisplayName("Stores")]
        public List<int> StoreIdsInput { get; set; }

        #endregion
    }
}
