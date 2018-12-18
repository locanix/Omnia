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
using Omnia.Module.BusinessObjects.Vehicles;

namespace Omnia.Module.BusinessObjects.Customers
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    public class CustomerVehicle : Vehicle
    { 
        public CustomerVehicle(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private Customer _Customer;
        [Association("Customer-Vehicle")]
        [RuleRequiredField]
        [DetailViewLayout("Vehicle")]
        public Customer Customer
        {
            get { return _Customer; }
            set { SetPropertyValue("Customer", ref _Customer, value); }
        }

        private bool _IsBillable;
        [DetailViewLayout("Vehicle")]
        public bool IsBillable
        {
            get { return _IsBillable; }
            set { SetPropertyValue("IsBillable", ref _IsBillable, value); }
        }
    }
}