using System;
using System.Collections.Generic;
using System.Linq;
using StudyApi.Events;
using StudyValidationApi.Models;
using StudyValidationApi.ValidationRules;
using Xunit;

namespace StudyUnitTests
{
    public class PrefixAccessionNumberValidationRuleTests
    {
        [Fact]
        public void ValidStartsWithTest()
        {
            var target = new PrefixAccessionNumberValidationRule();
            target.Option = PrefixAccessionNumberValidationRule.PrefixOption.StartsWith;
            target.Prefix = "abc123";

            string accessionNumber = "abc123xyz456";
            AccessionNumberChangedEvent e = new AccessionNumberChangedEvent(Guid.NewGuid(), accessionNumber);
            
            Study study = new Study();
            study.Apply(e);

            // ensure no validation exceptions
            Assert.False(target.Validate(study).Any());
        }

        [Fact]
        public void InvalidStartsWithTest()
        {
            var target = new PrefixAccessionNumberValidationRule();
            target.Option = PrefixAccessionNumberValidationRule.PrefixOption.StartsWith;
            target.Prefix = "abc123";

            string accessionNumber = "xyz456abc123";
            AccessionNumberChangedEvent e = new AccessionNumberChangedEvent(Guid.NewGuid(), accessionNumber);
            
            Study study = new Study();
            study.Apply(e);

            // ensure a validation exception
            Assert.True(target.Validate(study).Count() == 1);
        }

        [Fact]
        public void ValidDoesNotStartsWithTest()
        {
            var target = new PrefixAccessionNumberValidationRule();
            target.Option = PrefixAccessionNumberValidationRule.PrefixOption.DoesNotStartWith;
            target.Prefix = "abc123";

            string accessionNumber = "xyz456abc123";
            AccessionNumberChangedEvent e = new AccessionNumberChangedEvent(Guid.NewGuid(), accessionNumber);
            
            Study study = new Study();
            study.Apply(e);

            // ensure no validation exceptions
            Assert.False(target.Validate(study).Any());
        }

        [Fact]
        public void InvalidDoesNotStartsWithTest()
        {
            var target = new PrefixAccessionNumberValidationRule();
            target.Option = PrefixAccessionNumberValidationRule.PrefixOption.DoesNotStartWith;
            target.Prefix = "abc123";

            string accessionNumber = "abc123xyz456";
            AccessionNumberChangedEvent e = new AccessionNumberChangedEvent(Guid.NewGuid(), accessionNumber);
            
            Study study = new Study();
            study.Apply(e);

            // ensure a validation exception
            Assert.True(target.Validate(study).Count() == 1);
        }
    }
}
