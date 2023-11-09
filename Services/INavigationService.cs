using PosApp.ViewModel;

namespace PosApp.Services
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }
}