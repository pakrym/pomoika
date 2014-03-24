using Ninject;

namespace Pomoika
{
    using Caliburn.Micro;

    using Pomoika.Core;


    public class RuleViewModel : PropertyChangedBase
    {
        private readonly Rule rule;
        [Inject]
        public FilterViewModelFactory FilterViewModelFactory { get; set; }

        public RuleViewModel(Rule rule)
        {
            this.rule = rule;
        }


        public FilterViewModel Filter
        {
            get
            {
                return FilterViewModelFactory.CreateEditorViewModel(rule.Filter);
            }
        }

        public string Name
        {
            get
            {
                return this.rule.Name;
            }
            set
            {
                this.rule.Name = value;
                this.NotifyOfPropertyChange(()=>this.Name);
            }
        }

        public bool Enable
        {
            get
            {
                return this.rule.Enable;
            }
            set
            {
                this.rule.Enable = value;
                this.NotifyOfPropertyChange(() => this.Enable);
            }
        }
    }
}