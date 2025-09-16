using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ansej.Business.Core
{
    public interface IRecaptchaService
    {
        Task<bool> ValidateRecaptchaAsync(string recaptchaResponse);
    }
}
