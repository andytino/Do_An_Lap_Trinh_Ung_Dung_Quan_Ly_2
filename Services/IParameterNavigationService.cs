using PosApp.ViewModel;

namespace PosApp.Services
{
    public interface IParameterNavigationService<TParameter>
    {
        void Navigate(TParameter parameter);
    }
}