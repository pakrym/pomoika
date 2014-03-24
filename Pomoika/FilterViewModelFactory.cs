using Ninject;
using Ninject.Parameters;
using Pomoika.Core;

namespace Pomoika
{
    public class FilterViewModelFactory
    {
        private readonly IKernel _kernel;

        public FilterViewModelFactory(IKernel kernel)
        {
            _kernel = kernel;
        }


        public FilterViewModel CreateEditorViewModel(Filter filter)
        {
           var parameter = new ConstructorArgument("filter", filter,true);
           if (filter is CompositeFilter)
           {
                return _kernel.Get<CompositeFilterViewModel>(parameter);
           }
           else
           {
               return _kernel.Get<FilterViewModel>(parameter);
           }
        }
    }
}