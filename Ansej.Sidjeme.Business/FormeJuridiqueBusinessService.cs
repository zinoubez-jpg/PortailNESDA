using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public class FormeJuridiqueBusinessService : GenericBusinessService<FormeJuridique, int>
    {

		public FormeJuridiqueBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

     

    }
}
