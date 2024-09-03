using CompanyManager.Interfaces;

namespace CompanyManager.Services
{
    public class ItemsService : IItemsService
    {
        private readonly Dictionary<Type, object> _data = new();

        public void ClearData<TViewModel>()
        {
            _data.Remove(typeof(TViewModel));
        }

        public T GetData<TViewModel, T>()
        {
            if (_data.TryGetValue(typeof(TViewModel), out var data))
            {
                return (T)data;
            }
            return default;
        }

        public void SetData<TViewModel>(object data)
        {
            _data[typeof(TViewModel)] = data;
        }
    }
}
