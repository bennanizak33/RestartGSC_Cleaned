using ExecutionWPF;
using McDonalds.Commun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using McDonalds.commun.Constants;
using RestartGSC_WPF.Helpers;
using System.Configuration;
using System.Windows.Threading;

namespace RestartGSC_WPF
{
    /// <summary>
    /// Interaction logic for bloquerWindow.xaml
    /// </summary>
    public partial class bloquerWindow : Window
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        private readonly LogWriter _logWriter;
        private string ServerIpAddress = string.Empty;
        private int RestaurantId = 0;

        private IO.Swagger.Api.ServerEventsApi ServerEventsApi;
        private readonly string cheminBatchRestart = @"" + ConfigurationManager.AppSettings["cheminBatchRestart"];


        public bloquerWindow(LogWriter logWriter, string ipAddress, int restaurantId)
        {
            InitializeComponent();

            _globalKeyboardHook = new GlobalKeyboardHook();
            ServerEventsApi = new IO.Swagger.Api.ServerEventsApi(AppSettings.ReadSetting<string>(AppSettingConstants.RebootGSCApiURL, null));

            _logWriter = logWriter;
            ServerIpAddress = ipAddress;
            RestaurantId = restaurantId;

            // Hooks into all keys.
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
            _logWriter.LogWrite($"Affichage plein écran + bloquer Hotkeys");

#if DEBUG
            //Plein écran pour tout cacher, à personnaliser
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
#endif

            Task.Factory.StartNew(executerProcess);
        }

        private void executerProcess()
        {
            try
            {
                int exitCode = 0;
                // commande restart  .bat
                string output = UptimeHelper.processBatch(cheminBatchRestart, ServerIpAddress, _logWriter, ref exitCode);

                // 10 minutes d'attentes appconfig
                Thread.Sleep(1000 * AppSettings.ReadSetting<int>(AppSettingConstants.ServerResponseTimeout, 10));

                if (PingHelper.CheckAddress(ServerIpAddress).Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    ServerEventsApi.ServerEventsPostServerEvent(new IO.Swagger.Model.ServerEvent()
                    {
                        Date = DateTime.Now,
                        Event = IO.Swagger.Model.ServerEvent.EventEnum.NUMBER_5,
                        RestaurantId = RestaurantId,
                        Detail = "Le serveur a repondu"
                    });

                    this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => this.windowText.Text = "Le serveur distant a bien redemarré"));

                    //Thread.Sleep(1000 * AppSettings.ReadSetting<int>(AppSettingsConstant.XmlRpcResponseTimeout, 5) * 60);

                    //Call XmlRpc

                    //TODO

#if DEBUG
                //Désactiver le vérouillage après 5 secondes pour pouvoir quitter (à être remplacé par un autre logique après)
                Delay(5000).ContinueWith(_ => quitterFenetre());
#endif

                }
                else
                {
                    ServerEventsApi.ServerEventsPostServerEvent(new IO.Swagger.Model.ServerEvent()
                    {
                        Date = DateTime.Now,
                        Event = IO.Swagger.Model.ServerEvent.EventEnum.NUMBER_6,
                        RestaurantId = RestaurantId,
                        Detail = "Le serveur n'a pas repondu"
                    });

                    this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                    {
                        this.windowText.Text += "\n" + "Contactez votre mainteneur!";

                        this.exitButton.Visibility = Visibility.Visible;
                    }));
                }
            }
            catch (Exception ex )
            {
                _logWriter.LogWrite($" ClassName : bloquerWindow ");
                _logWriter.LogWrite($" Exception : {ex.Message}");
                _logWriter.LogWrite($" Exception : {ex.StackTrace}");
                throw;
            }          

            // if OK call xmlRPc attendre 5 minutes is ok update ecran + update api + deblocage ecran


        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            e.Handled = true;
        }

        private void quitterFenetre()
        {
            _globalKeyboardHook.KeyboardPressed -= OnKeyPressed;
            // déblocage de l'interface 
            Application.Current.Dispatcher.Invoke((Action)delegate {
                var _mainWindow = new MainWindow();
                _mainWindow.Show();
                this.Close();
            });
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            quitterFenetre();
        }

        //static void Main()
        //{
        //    Delay(2000).ContinueWith(_ => Console.WriteLine("Done"));
        //    Console.Read();
        //}

        private static Task Delay(int milliseconds)
        {
            var tcs = new TaskCompletionSource<object>();
            new Timer(_ => tcs.SetResult(null)).Change(milliseconds, -1);
            return tcs.Task;
        }
    }
}
