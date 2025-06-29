using System;
using System.Collections.Generic;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public interface IPromoteurEligibiliteSettings
    {
        int MinAge { get; set; }
        int MaxAge { get; set; }
    }
}
