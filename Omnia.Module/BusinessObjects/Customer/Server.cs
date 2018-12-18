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

namespace Omnia.Module.BusinessObjects.Customers
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    public class Server : BaseObject
    {
        public Server(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string _Name;
        [XafDisplayName("Name")]
        [RuleRequiredField]
        [RuleUniqueValue]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }

        private string _Url;
        public string Url
        {
            get { return _Url; }
            set { SetPropertyValue("Url", ref _Url, value); }
        }

        private string _IpAddress;
        [RuleRegularExpression("Server.IpAddress", DefaultContexts.Save, @"(^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?))|((^|:)([0-9a-fA-F]{0,4})){1,8}$", CustomMessageTemplate = "Ip address should be valid IPv4 or IPv6 address")]
        public string IpAddress
        {
            get { return _IpAddress; }
            set { SetPropertyValue("IpAddress", ref _IpAddress, value); }
        }

        private string _Notes;
        [Size(160)]
        public string Notes
        {
            get { return _Notes; }
            set { SetPropertyValue("Notes", ref _Notes, value); }
        }

        [Association("Customer-Server")]
        public XPCollection<Customer> Customers
        {
            get
            {
                return GetCollection<Customer>("Customers");
            }
        }


    }
}