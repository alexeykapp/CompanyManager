using CompanyManager.Base;
using CompanyManager.View;
using CompanyManager.ViewModel;
using System.Windows;

namespace CompanyManager.Services
{
    public class WindowMapper
    {
        private readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();
        public WindowMapper()
        {
            RegisterMapping<StartWindowVM, StartWindow>();
            RegisterMapping<EmployeeWindowVM, EmployeeWindow>();
            RegisterMapping<AddEmployeeVM, AddEmployeeWindow>();
        }
        public void RegisterMapping<TViewModel, TWindow>() where TViewModel : BaseViewModel where TWindow : Window
        {
            _mappings[typeof(TViewModel)] = typeof(TWindow);
        }
        public Type? GetWindowTypeForViewModel(Type viewModelType)
        {
            _mappings.TryGetValue(viewModelType, out var windowType);
            return windowType;
        }
    }
}
