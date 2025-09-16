using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public class SecteurActiviteBusinessService : GenericBusinessService<SecteurActivite, int>
    {

        public SecteurActiviteBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        

        public override Expression<Func<SecteurActivite, object>>[] GetDefaultLoadProperties()
        {
            Expression<Func<SecteurActivite, object>> loadActivities = x => x.Activites;

            return new Expression<Func<SecteurActivite, object>>[] { loadActivities };
        }
    }
}
