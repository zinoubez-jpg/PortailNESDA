using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Ansej.Sidjeme.Business
{
    public class DeclarationHonneurMIBusinessService : GenericBusinessService<DeclarationHonneurMI, int>
    {
        public DeclarationHonneurMIBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        protected override void OnAdding(DeclarationHonneurMI entity)
        {
            if (_repository.Count(x => x.ProjectNumber == entity.ProjectNumber) > 0)
                throw new BusinessException("يوجد بالفعل طلب برقم الملف هذا ");

            if (_repository.Count(x => x.NIN == entity.NIN) > 0)
                throw new BusinessException("يوجد بالفعل طلب برقم التعريف الوطني هذا ");

            if ((entity.DateOfBirth.Date.Year - DateTime.Today.Year) > 18)
                throw new BusinessException("يجب أن يكون عمرك 18 عامًا على الأقل");




            base.OnAdding(entity);
        }

        protected override void OnUpdating(DeclarationHonneurMI entity)
        {
            base.OnUpdating(entity);
        }

        public override Expression<Func<DeclarationHonneurMI, object>>[] GetDefaultLoadProperties()
        {
            Expression<Func<DeclarationHonneurMI, object>> loadWilaya = x => x.Wilaya;
            Expression<Func<DeclarationHonneurMI, object>> loadActivity = x => x.Activity;
            Expression<Func<DeclarationHonneurMI, object>> loadActivitySector = x => x.Activity.secteur;
            Expression<Func<DeclarationHonneurMI, object>> loadGender = x => x.Gender;
            Expression<Func<DeclarationHonneurMI, object>> loadlegalStatus = x => x.LegalStatus;
            Expression<Func<DeclarationHonneurMI, object>> loadTypeOfNeed = x => x.TypeOfNeed;
            Expression<Func<DeclarationHonneurMI, object>> loadStructure = x => x.StructureManager;
            Expression<Func<DeclarationHonneurMI, object>> loadMotif = x => x.MotifTraitement;


            return new Expression<Func<DeclarationHonneurMI, object>>[] { loadWilaya, loadActivity, loadActivitySector, loadGender, loadlegalStatus, loadTypeOfNeed, loadStructure, loadMotif };
        }
    }
}
