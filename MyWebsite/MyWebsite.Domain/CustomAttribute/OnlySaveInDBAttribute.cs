using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrevisOnline.Domain.CustomAttribute
{
    [AttributeUsage(AttributeTargets.All)]
    public class OnlySaveInDBAttribute : Attribute
    {
    }
}
