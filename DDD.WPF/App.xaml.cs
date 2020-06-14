using DDD.Domain.Exceptios;
using DDD.WPF.ViewModels;
using DDD.WPF.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace DDD.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            //処理されない例外の時に発生するイベントのメソッドを追加
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        //例外がキャッチされていなかったら、ここまで落ちてくる
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //_logger.Error(ex.Message, ex);
            MessageBoxImage icon = MessageBoxImage.Error;
            string caption = "エラー";
            var exceptionBase = e.Exception as ExceptionBase;//as 型を変換 変換できなかったらnullになる
            if (exceptionBase != null)
            {
                if (exceptionBase.Kind == ExceptionBase.ExceptionKind.info)
                {
                    icon = MessageBoxImage.Information;
                    caption = "情報";
                }
                else if (exceptionBase.Kind == ExceptionBase.ExceptionKind.Warinng)
                {
                    icon = MessageBoxImage.Warning;
                    caption = "警告";
                }
            }
            MessageBox.Show(e.Exception.Message,
                                          caption,
                                          MessageBoxButton.OK, icon);

            e.Handled = true;//アプリケーションが落ちずにここを通って正しく処理されたと判断される
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<WeatherLatestView>();
            containerRegistry.RegisterForNavigation<WeatherListView>();
            containerRegistry.RegisterDialog<WeatherSaveView,WeatherSaveViewModel>();
            containerRegistry.RegisterSingleton<MainWindowViewModel>();//シングルトン登録
        }
    }
}
