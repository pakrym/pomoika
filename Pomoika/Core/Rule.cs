using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomoika.Core
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Action = System.Action;

    public class Rule
    {
        public Rule()
        {
            Inputs = new ObservableCollection<Input>();
            Actions = new ObservableCollection<Action>();
            Filter = new AndFilter();
            Schedule = new AutomaticSchedule();
        }

        public Schedule Schedule { get; set; }
        public ICollection<Input> Inputs { get; set; }
        public Filter Filter { get; set; }
        public ICollection<Action> Actions { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }
    }

    public class AutomaticSchedule : Schedule
    {
    }

    public class Schedule
    {
    }

    public class Input
    {
    }

    [FilterAttribute("And")]
    public class AndFilter : CompositeFilter
    {
        public override bool Match(FileInfo fileInfo)
        {
            return Subfilters.All(s=>s.Match(fileInfo));
        }

    }

    public class FilterAttribute : Attribute
    {
        public string Name { get; set; }

        public FilterAttribute(string name)
        {
            Name = name;
        }
    }

    public abstract class Filter
    {
        protected Filter()
        {
            this.Name = GetFilterName(this.GetType());
        }

        internal static string GetFilterName(Type getType)
        {
            var attribute =
                getType
                    .GetCustomAttributes(typeof(FilterAttribute), false)
                    .OfType<FilterAttribute>()
                    .FirstOrDefault();
            if (attribute != null)
                return  attribute.Name;
            return String.Empty;
        }

        public abstract bool Match(FileInfo fileInfo);
        public string Name { get; private set; }
    }
}
