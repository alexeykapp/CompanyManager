namespace CompanyManager.Interfaces
{
    public interface IItemsService
    {
        void SetData<TViewModel>(object data);
        T GetData<TViewModel, T>();
        void ClearData<TViewModel>();
    }
}
