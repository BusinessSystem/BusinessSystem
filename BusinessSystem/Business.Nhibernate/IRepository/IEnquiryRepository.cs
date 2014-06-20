using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;

namespace Business.Nhibernate.IRepository
{
    public interface IEnquiryRepository : IRepository<Enquiry>
    {

    }

    public interface IEnquiryTransFollowRepository : IRepository<EnquiryTransFollow>
    {

    }
}
