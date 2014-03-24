using System.Collections.ObjectModel;
using Pomoika.Core;

namespace Pomoika
{
    public abstract class CompositeFilter : Filter
    {
        protected CompositeFilter()
        {
            Subfilters = new ObservableCollection<Filter>();
        }

        public ObservableCollection<Filter> Subfilters { get; set; }
    }
}