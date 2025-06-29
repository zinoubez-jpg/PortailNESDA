using System;
using System.Collections.Generic;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public class PromoteurEligibiliteSettings:IPromoteurEligibiliteSettings
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}
