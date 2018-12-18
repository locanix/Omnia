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
using Omnia.Module.BusinessObjects.Billing;

namespace Omnia.Module.BusinessObjects.Customers
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    public class Customer : BaseObject
    {
        public Customer(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string _Name;
        [RuleRequiredField]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue<string>("Name", ref _Name, value); }
        }

        private string _Website;
        public string Website
        {
            get { return _Website; }
            set { SetPropertyValue<string>("Website", ref _Website, value); }
        }

        private BillingPlan _BillingPlan;
        [Association("Customer-BillingPlan")]
        public BillingPlan BillingPlan
        {
            get { return _BillingPlan; }
            set { SetPropertyValue("BillingPlan", ref _BillingPlan, value); }
        }

        private Server _Server;
        [Association("Customer-Server")]
        public Server Server
        {
            get { return _Server; }
            set { SetPropertyValue("Server", ref _Server, value); }
        }

        private DateTime _FirstContractDate;
        [ToolTip("The date of the first contract starts")]
        public DateTime FirstContractDate
        {
            get { return _FirstContractDate; }
            set { SetPropertyValue("FirstContractDate", ref _FirstContractDate, value); }
        }

        [Association("Customer-CustomerContact"), DevExpress.Xpo.Aggregated]
        public XPCollection<CustomerContact> Contacts
        {
            get { return GetCollection<CustomerContact>("Contacts"); }
        }

        [Association("Customer-Vehicle"), DevExpress.Xpo.Aggregated]
        public XPCollection<CustomerVehicle> Vehicles
        {
            get
            {
                return GetCollection<CustomerVehicle>("Vehicles");
            }
        }

        [PersistentAlias("Vehicles[].Count()")]
        public int NumberOfVehicles
        {
            get
            {
                return (int)EvaluateAlias("NumberOfVehicles");
            }
        }
    }
}
