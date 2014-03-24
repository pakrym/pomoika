using Pomoika.Core;

namespace Pomoika
{
    public class FilterViewModel
    {
        internal Filter Filter { get; private set; }

        public FilterViewModel(Filter filter)
        {
            this.Filter = filter;
        }

        public string Name { get { return Filter.Name; } }

    }
}