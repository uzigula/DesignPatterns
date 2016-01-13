using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using Patterns.Api.Contracts;
using Patterns.Api.Models;

namespace Patterns.Api.Controllers
{
    [RoutePrefix("rules")]
    public class ClaimController : ApiController
    {
        private readonly IEnumerable<Remark> remarks;

        public ClaimController(IEnumerable<Remark> remarks )
        {
            this.remarks = remarks;
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Claim claim)
        {
            var result = ApplyRemarks(claim);
            return Ok(result);
        }

        private string ApplyRemarks(Claim claim)
        {
            var message = new StringBuilder();
            foreach (var remark in remarks)
            {
                var result = remark.Apply(claim);
                if (!string.IsNullOrEmpty(result))
                    message.AppendLine(result);
            }

            return message.ToString().IsNullOrEmpty() ? "No rules Applied" : message.ToString();


        }
    }
}