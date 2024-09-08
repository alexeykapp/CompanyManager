namespace CompanyManager.Interfaces
{
    public interface IViewModelDataService
    {
        void SetData<TViewModel>(object data);
        T GetData<TViewModel, T>();
        void ClearData<TViewModel>();
    }
}
