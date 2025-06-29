using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public class ConsultationBusinessService : GenericBusinessService<Consultation, int>
    {
        public ConsultationBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        protected override void OnAdding(Consultation entity)
        {
            base.OnAdding(entity);
        }

        protected override void OnUpdating(Consultation entity)
        {
            base.OnUpdating(entity);
        }

        public override Expression<Func<Consultation, object>>[] GetDefaultLoadProperties()
        {
            Expression<Func<Consultation, object>> loadStructure = x => x.StructureManager;

            return new Expression<Func<Consultation, object>>[] { loadStructure };
        }
    }
}
