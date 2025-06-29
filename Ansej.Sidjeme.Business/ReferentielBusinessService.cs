
using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Domain.Core;
using Ansej.Sidjeme.Domain;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Localization;

namespace Ansej.Sidjeme.Business
{
    public class ReferentielBusinessService<T, TKey> : GenericBusinessService<T, TKey> where T : Referentiel, IEntity<TKey>
    {
        private readonly IStringLocalizer<BusinessMessage> _localizer;

        public ReferentielBusinessService(IUnitOfWork unitOfWork, IStringLocalizer<BusinessMessage> localizer) : base(unitOfWork)
        {
            _localizer = localizer;

        }

        protected override void OnAdding(T entity)
        {
            //Indication: Use the repository.Count(predicate) method
            if (_repository.Count(x => x.Code == entity.Code || x.DesignationFr == entity.DesignationFr || x.DesignationAr == entity.DesignationAr) > 0)
                throw new BusinessException(_localizer["AlreadyExists"]);

            base.OnAdding(entity);
        }

        protected override void OnUpdating(T entity)
        {            
            //Indication: Use the repository.Count(predicate) method
            if (_repository.Count(x => x.Id != entity.Id && (x.Code == entity.Code || x.DesignationFr == entity.DesignationFr || x.DesignationAr == entity.DesignationAr)) > 0)
                throw new BusinessException(_localizer["AlreadyExists"]);

            base.OnUpdating(entity);
        }

    }
}
