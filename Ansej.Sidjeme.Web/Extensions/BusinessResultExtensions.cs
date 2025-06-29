using Ansej.Business.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web
{
    public static class BusinessResultExtensions
    {
        public static string ToBootstrapAlerts(this BusinessResult businessResult)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var msg in businessResult.Messages)
            {
                sb.AppendFormat("<div class='alert alert-{0} alert-dismissible fade show' role='alert'>{1}  <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">\n    <span aria-hidden=\"true\">&times;</span>\n  </button></div>", msg.MessageType==MessageType.Error?"danger":msg.MessageType.ToString().ToLower(),msg.Message);
            }

            return sb.ToString();
        }
    }
}
