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
    [DefaultProperty("DisplayName")]
    [RuleCombinationOfPropertiesIsUnique("Brand;Name")]
    public class VehicleModel : BaseObject
    {
        public VehicleModel(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private VehicleBrand _Brand;
        [Association("VehicleBrand-VehicleModel")]
        [RuleRequiredField]
        public VehicleBrand Brand
        {
            get { return _Brand; }
            set { SetPropertyValue("Brand", ref _Brand, value); }
        }


        private string _Name;
        [XafDisplayName("Name")]
        [RuleRequiredField]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }

        [PersistentAlias("Iif(Brand is null, '', Brand.Name + ' ') + Name")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public string DisplayName
        {
            get
            {
                return (string)EvaluateAlias("DisplayName");
            }
        }

        private VehicleBodyType _BodyType;
        public VehicleBodyType BodyType
        {
            get { return _BodyType; }
            set { SetPropertyValue("BodyType", ref _BodyType, value); }
        }

        [Association("VehicleModel-Vehicle")]
        public XPCollection<Vehicle> Vehicles
        {
            get
            {
                return GetCollection<Vehicle>("Vehicles");
            }
        }


    }
}