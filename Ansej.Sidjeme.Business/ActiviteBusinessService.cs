using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public class ActiviteBusinessService : GenericBusinessService<Activite, int>
    {

        public ActiviteBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        
    }
}
