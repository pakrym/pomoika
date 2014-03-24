using Ninject;

namespace Pomoika {
    using Pomoika.Core;

    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell
    {
        public ShellViewModel()
        {
        }
        [Inject]
        public RuleEditorViewModel RuleEditor { get; set; }
    }

    public interface IShell
    {
    }
}