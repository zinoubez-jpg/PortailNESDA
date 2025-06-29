using Ansej.Business.Core;
using Ansej.Data.Abstractions;

namespace Ansej.Sidjeme.Identity
{
    public class NiveauService : GenericBusinessService<TypeUser, int>
    {
        public NiveauService(IUnitOfWork<IdentityContext> unitOfWork) : base(unitOfWork)
        {

        }

        protected override void OnAdding(TypeUser entity)
        {
            //Indication: Use the repository.Count(predicate) method
            if (_repository.Count(x => x.Code == entity.Code || x.Designation == entity.Designation || x.DesignationAr == entity.DesignationAr) > 0)
                throw new BusinessException("Un référentiel ayant le même code ou la même designation existe déja!");

            base.OnAdding(entity);
        }

        protected override void OnUpdating(TypeUser entity)
        {
            //Indication: Use the repository.Count(predicate) method
            if (_repository.Count(x => x.Id != entity.Id && (x.Code == entity.Code || x.Designation == entity.Designation || x.DesignationAr == entity.DesignationAr)) > 0)
                throw new BusinessException("Un référentiel ayant le même code ou la même designation existe déja!");

            base.OnUpdating(entity);
        }

    }
}