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

namespace Omnia.Module.BusinessObjects.Vehicles
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    public class VehicleBodyType : BaseObject
    {
        public VehicleBodyType(Session session)
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
    }
}