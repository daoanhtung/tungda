using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebsite.Resx;

namespace MyWebsite.Domain
{
    public class MenuResult
    {
        [Required(ErrorMessageResourceType = typeof(RsCommon), ErrorMessageResourceName = "RequiredAttribute")]
        public int MenuId { get; set; }
        [Required(ErrorMessageResourceType = typeof(RsCommon), ErrorMessageResourceName = "RequiredAttribute")]
        public string MenuLink { get; set; }
        [Required(ErrorMessageResourceType = typeof(RsCommon), ErrorMessageResourceName = "RequiredAttribute")]
        public string MenuText { get; set; }
        [Required(ErrorMessageResourceType = typeof(RsCommon), ErrorMessageResourceName = "RequiredAttribute")]
        public string MenuText_VI { get; set; }
        [Required(ErrorMessageResourceType = typeof(RsCommon), ErrorMessageResourceName = "RequiredAttribute")]
        public int ParentMenuId { get; set; }
        [Required(ErrorMessageResourceType = typeof(RsCommon), ErrorMessageResourceName = "RequiredAttribute")]
        public int MaxPerColumn { get; set; }
    }
}
