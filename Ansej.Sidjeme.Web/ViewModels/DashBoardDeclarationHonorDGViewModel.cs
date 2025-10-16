using System.Collections.Generic;
using Ansej.Sidjeme.Domain.Entities;


namespace Ansej.Sidjeme.Web.ViewModels
{
    public class DashBoardDeclarationHonorDGViewModel
    {
        public int NbrDemandeTotal { get; set; }
        public int NbrDemandeTraite { get; set; }
        public int NbrDemandeNonTraite { get; set; }
        public int NbrDemandeValide { get; set; }
        public int NbrDemandeNonValide { get; set; }
        public int NbrDemandeValideMC { get; set; }

        public List<DemandeParAgenceDto> DemandeParAgence { get; set; }
        public List<DemandeParAgenceDto> ValideParAgence { get; set; }
        public List<DemandeParAgenceDto> NonValideParAgence { get; set; }
        public List<DemandeParSecteurDto> DemandeParSecteur{ get; set; }
    }
}
