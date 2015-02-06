using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebsite.Resx;

namespace MyWebsite.Utils
{
    public class SelectListLanguage
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Image { get; set; }
        public static List<SelectListLanguage> GetList()
        {
            var itemType = new List<SelectListLanguage>{
                new SelectListLanguage{ Text = RsCommon.English, Value = RsCommon.English_Code, Image = RsCommon.English_Image},
                new SelectListLanguage{ Text = RsCommon.Vietnamese, Value = RsCommon.Vietnamese_Code, Image = RsCommon.Vietnamese_Image} 
                };
            return itemType;
        }
    }
}
