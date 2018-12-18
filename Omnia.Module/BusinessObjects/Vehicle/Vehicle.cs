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
    [RuleCriteria("Vehicle.YearOfManufacture", DefaultContexts.Save, "YearOfManufacture is null OR (YearOfManufacture >= 1920 and YearOfManufacture <= GetYear(Now()))", UsedProperties = "YearOfManufacture", CustomMessageTemplate = "Year of manufacture shoul be valid")]
    public class Vehicle : BaseObject
    {
        public Vehicle(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private VehicleModel _Model;
        [Association("VehicleModel-Vehicle")]
        [RuleRequiredField]
        public VehicleModel Model
        {
            get { return _Model; }
            set { SetPropertyValue("Model", ref _Model, value); }
        }

        private int? _YearOfManufacture;
        public int? YearOfManufacture
        {
            get { return _YearOfManufacture; }
            set { SetPropertyValue("YearOfManufacture", ref _YearOfManufacture, value); }
        }

        private string _VIN;
        [RuleUniqueValue]
        [Size(20)]
        public string VIN
        {
            get { return _VIN; }
            set { SetPropertyValue("VIN", ref _VIN, value); }
        }


        private string _NumberPlate;
        [RuleUniqueValue]
        [Size(10)]
        public string NumberPlate
        {
            get { return _NumberPlate; }
            set
            {
                SetPropertyValue("NumberPlate", ref _NumberPlate, value);
                if (value != null)
                    value = value.Replace(" ", "").Replace("-", "").ToUpper();
                SetPropertyValue("NumberPlate2", ref _NumberPlate2, value);
            }
        }

        [VisibleInDetailView(false)]
        private string _NumberPlate2;
        [Size(10)]
        [VisibleInDetailView(false)]
        public string NumberPlate2
        {
            get { return _NumberPlate2; }
        }


        private string _Notes;
        [Size(160)]
        public string Notes
        {
            get { return _Notes; }
            set { SetPropertyValue("Notes", ref _Notes, value); }
        }


    }
}