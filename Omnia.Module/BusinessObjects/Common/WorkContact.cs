using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using Omnia.Module.BusinessObjects.Common;

namespace Omnia.Module.BusinessObjects.Common
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    public abstract class WorkContact : OmniaUser
    {
        public WorkContact(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private ContactType _ContactType;
        public ContactType ContactType
        {
            get { return _ContactType; }
            set { SetPropertyValue("ContactType", ref _ContactType, value); }
        }
    }
}
