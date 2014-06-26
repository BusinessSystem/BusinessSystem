using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public class EnquiryTransFollowRepository:Repository<EnquiryTransFollow>,IEnquiryTransFollowRepository
    {

        public IList<EnquiryTransFollow> GetEnquiryTransFollowsByEnquiryId(long enquiryId)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<EnquiryTransFollow>().Where(m => m.EnquiryId == enquiryId).List();
            }
        }
    }
}
