using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Business.Models
{
    [MetadataType(typeof(AdditemMetadata))]
    public partial class AddItem
    {
        public List<KindList> KindListCollection { get; set; }
        public List<ItemList> ItemListCollection { get; set; }
    }
    public partial class AdditemMetadata
    {
        [Required(ErrorMessage = "This Field is Required")]
        public string KindName { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string ProductName { get; set; }
        public string CreateTime { get; set; }
        public int ProductID { get; set; }
        public string ModifyTime { get; set; }
        public int KindID { get; set; }
        public int ItemID { get; set; }         
    }

}