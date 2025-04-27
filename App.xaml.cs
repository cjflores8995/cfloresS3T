using cfloresS2.Views;

namespace cfloresS2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new vMain());
        }
    }
}