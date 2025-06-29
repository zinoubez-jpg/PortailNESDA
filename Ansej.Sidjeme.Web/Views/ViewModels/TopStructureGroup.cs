using Ansej.Sidjeme.Data.Migrations;
using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web.Views.ViewModels
{
    public class TopStructureGroup
    {
        public StructureManager TopStructure { get; set; }
        public List<Domain.Entities.Consultation> Consultations { get; set; }
        public bool IsFromChild { get; set; }
    }
}
