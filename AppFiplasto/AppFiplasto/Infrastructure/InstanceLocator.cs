namespace AppFiplasto.Infrastructure
{
    using AppFiplasto.ViewModels;

    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }


        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
