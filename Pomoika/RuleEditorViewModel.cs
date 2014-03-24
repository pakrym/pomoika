using System;

namespace Pomoika
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Pomoika.Core;


    public class RuleEditorViewModel
    {
        private ObservableCollection<Rule> rules;

        public RuleEditorViewModel(Func<Rule,RuleViewModel> ruleFactory)
        {
            rules = new ObservableCollection<Rule>(new[] { new Rule() });

            this.Rules = new ObservableViewModelCollection<RuleViewModel, Rule>(rules, ruleFactory);
        }

        public ICollection<RuleViewModel> Rules { get; set; } 

        public void Add()
        {
            rules.Add(new Rule());
        }
    }
}