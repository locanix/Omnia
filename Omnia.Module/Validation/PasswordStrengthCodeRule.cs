using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Omnia.Module.Validation
{

    [CodeRule]
    public class PasswordStrengthCodeRule : RuleBase<ChangePasswordOnLogonParameters>
    {
        public PasswordStrengthCodeRule()
            : base("", "ChangePassword")
        {
            this.Properties.SkipNullOrEmptyValues = false;
        }

        public PasswordStrengthCodeRule(IRuleBaseProperties properties) : base(properties) { }

        protected override bool IsValidInternal(ChangePasswordOnLogonParameters target, out string errorMessageTemplate)
        {
            if (CalculatePasswordStrength(target.NewPassword) < 3)
            {
                errorMessageTemplate = "password strength is insufficient";
                return false;
            }
            errorMessageTemplate = string.Empty;
            return true;
        }

        private int CalculatePasswordStrength(string pwd)
        {
            int weight = 0;
            if (pwd == null) return weight;
            if (pwd.Length > 1 && pwd.Length < 4)
                ++weight;
            else
            {
                if (pwd.Length > 5)
                    ++weight;
                Regex rxUpperCase = new Regex("[A-Z]");
                Regex rxLowerCase = new Regex("[a-z]");
                Regex rxNumerals = new Regex("[0-9]");
                Match match = rxUpperCase.Match(pwd);
                if (match.Success)
                    ++weight;
                match = rxLowerCase.Match(pwd);
                if (match.Success)
                    ++weight;
                match = rxNumerals.Match(pwd);
                if (match.Success)
                    ++weight;
            }
            if (weight == 3 && pwd.Length < 6)
                --weight;
            if (weight == 4 && pwd.Length > 10)
                ++weight;
            return weight;
        }
    }
}
