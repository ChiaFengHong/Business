using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Business.Models
{
    [MetadataType(typeof(ItemListMetadata))]
    public partial class ItemList
    {
        public List<KindList> KindListCollection { get; set; }

        public class ItemListMetadata
        {
            public int ItemID { get; set; }
            public int KindID { get; set; }
            [Required(ErrorMessage = "This Field is Required")]
            public string KindName { get; set; }
            [Required(ErrorMessage = "This Field is Required")]
            public string ItemName { get; set; }
            public string CreateTime { get; set; }
            public string ModifyTime { get; set; }
        }
    }
}