using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject;
using Pomoika.Core;

namespace Pomoika
{
    public class CompositeFilterViewModel: FilterViewModel
    {
        public FilterViewModelFactory FilterViewModelFactory { get; private set; }
      
        public FilterProvider FilterProvider { get; private set; }
        
        public ICollection<FilterViewModel> Subfilters { get; private set; }

        public IEnumerable<FilterInfo> AllFilters { get { return FilterProvider.GetFilterList(); } }
 
        public CompositeFilterViewModel(
            CompositeFilter filter, 
            FilterViewModelFactory filterViewModelFactory, 
            FilterProvider filterProvider) : base(filter)
        {
            FilterViewModelFactory = filterViewModelFactory;
            FilterProvider = filterProvider;
            Subfilters = new ObservableViewModelCollection<FilterViewModel, Filter>(filter.Subfilters, FilterViewModelFactory.CreateEditorViewModel);
        }

        public void Delete(FilterViewModel filter)
        {
            ((CompositeFilter) Filter).Subfilters.Remove(filter.Filter);
        }

        public void Add(FilterInfo filterInfo)
        {
            if (filterInfo != null)
            ((CompositeFilter)Filter).Subfilters.Add(FilterProvider.CreateFilter(filterInfo.Type));
        }
    }

    public class FilterProvider
    {
        private readonly IKernel _kernel;
        private IEnumerable<FilterInfo> _filterInfos;

        public FilterProvider(IKernel kernel)
        {
            _kernel = kernel;
            _filterInfos = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t=>typeof(Filter).IsAssignableFrom(t))
                .Where(t=>!t.IsAbstract)
                .Select(f=> new FilterInfo(Filter.GetFilterName(f),f)).ToArray();
        }

        public IEnumerable<FilterInfo> GetFilterList()
        {
            return _filterInfos;
        }

        public Filter CreateFilter(Type type)
        {
            return (Filter) _kernel.Get(type);
        }
    }

    public class FilterInfo
    {
        public FilterInfo(string name, Type type)
        {
            Name = name;
            Type = type;
        }        

        public string Name { get; private set; }
        public Type Type { get; private set; }
    }
}