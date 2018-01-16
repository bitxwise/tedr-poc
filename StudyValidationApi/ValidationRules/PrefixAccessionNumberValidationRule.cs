using System.Collections.Generic;
using StudyValidationApi.Models;

namespace StudyValidationApi.ValidationRules
{
    public class PrefixAccessionNumberValidationRule : IValidationRule
    {
        public string Prefix { get; set; }

        private PrefixOption _prefixOption;
        public PrefixOption Option
        {
            get { return _prefixOption; }
            set { _prefixOption = value; }
        }

        public enum PrefixOption : byte
        {
            StartsWith          = 0,
            DoesNotStartWith    = 1
        }

        public IEnumerable<ValidationException> Validate(Study study)
        {
            if(Option == PrefixOption.StartsWith && !study.AccessionNumber.StartsWith(Prefix))
                yield return new ValidationException(string.Format($"Study({study.Id}) with accession({study.AccessionNumber}) must start with prefix({Prefix})"));
            else if(Option == PrefixOption.DoesNotStartWith && study.AccessionNumber.StartsWith(Prefix))
                yield return new ValidationException(string.Format($"Study({study.Id}) with accession({study.AccessionNumber}) must not start with prefix({Prefix})"));
            else
                yield break;
        }
    }
}