using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ansej.Sidjeme.Domain
{
    public partial class Wilaya
    {
        [NotMapped]
        public string DisplayName { get => $"({Code}) {DesignationFr}"; }
        [NotMapped]
        public string DisplayNameAr { get => $"({Code}) {DesignationAr}  "; }
        [NotMapped]
        public string DisplayNameEn { get => $"({Code}) {DesignationEn}  "; }
    }
}
