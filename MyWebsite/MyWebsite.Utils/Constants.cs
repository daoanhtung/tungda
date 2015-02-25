using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebsite.Resx;

namespace MyWebsite.Utils
{
    public sealed class Constants
    {
        public const string EncryptPassword = "MyWebsite";
        public const string LoggerNameSendEmail = "MyWebsiteLogSendEmail";
        public const string LoggerNameWriteFile = "MyWebsiteLogWriteFile";
        public const string UserNameSessionKey = "UserName";
        public const string PasswordSessionKey = "Password";
        public const string WishListCountSessionKey = "WishlistCount";
        public const string TotalProductCountSessionKey = "TotalProduct";
        public const string LanguageQueryString = "lang";
        public const string TopMenuSessionKey = "TopMenu";
        public const string BottomMenuSessionKey = "BottomMenu";
        public const string TotalPriceSessionKey = "TotalPrice";
        public const string Zero = "0";
        public const string SliderSessionKey = "Slider";
        public const string AdminMenuSessionKey = "AdminMenu";
        public const char Tab = '=';
        public const string Space = "&nbsp;";
        public const string UpdateMenuSuccessKeySession = "UpdateMenuSuccess";
        #region css

        public const string FirstClass = " first";
        public const string LastClass = " last";
        public const string ParentClass = " parent";
        public const string ActiveClass = " active";

        #endregion
    }

}
