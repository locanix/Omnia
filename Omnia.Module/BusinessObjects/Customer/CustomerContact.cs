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

namespace Omnia.Module.BusinessObjects.Customers
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    public class CustomerContact : WorkContact
    {
        public CustomerContact(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private Customer _Customer;
        [Association("Customer-CustomerContact")]
        [RuleRequiredField]
        public Customer Customer
        {
            get { return _Customer; }
            set { SetPropertyValue<Customer>("Customer", ref _Customer, value); }
        }

    }
}
