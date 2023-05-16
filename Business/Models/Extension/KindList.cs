using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Business.Models
{
    [MetadataType(typeof(KindListMetadata))]
    public partial class KindList
    {        
    }
    public class KindListMetadata
    {
        public int KindID { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string KindName { get; set; }
        public string CreateTime { get; set; }
        public string ModifyTime { get; set; }

    }
}