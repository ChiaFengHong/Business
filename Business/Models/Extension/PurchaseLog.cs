using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Business.Models
{
    [MetadataType(typeof(PurchaseLogMetadata))]
    public partial class PurchaseLog
    {
        public List<KindList> KindListCollection { get; set; }
        public List<ItemList> ItemListCollection { get; set; }
        public List<AddItem> PorductListCollection { get; set; }
    }
    public partial class PurchaseLogMetadata
    {
        public int PurchaseID { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string KindName { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string ProductName { get; set; }
        public string CreateTime { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public decimal Qty { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }

}