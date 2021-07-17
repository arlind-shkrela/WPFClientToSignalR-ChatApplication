using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClientApp.Helper;

namespace WpfClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection _connection;
        public MainWindow()
        {
            InitializeComponent();
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44357/chatHub")
                .Build();
            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
        }
        //Connection
        private async void Button_Click_Connect(object sender, RoutedEventArgs e)
        {

            _connection.On<string, byte[]>("ReceiveMessage", (user, message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{user}: {message}";
                    messageList.Items.Add(newMessage);
                });
            });

            try
            {
                await _connection.StartAsync();
                messageList.Items.Add("Connection started");
                connectButton.IsEnabled = false;
                sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                messageList.Items.Add(ex.Message);
            }
        }


        //Send Message
        private async void Button_Click_SendMessage(object sender, RoutedEventArgs e)
        {
            try
            {
                var userMessage = this.userMessage.Text;
                var encryptedMessage = Encrypt.String(userMessage);

                await _connection.InvokeAsync("SendMessage",
                   userName.Text, encryptedMessage);


            }
            catch (Exception ex)
            {
                messageList.Items.Add(ex.Message);
            }

        }



        //SelectFilePath
        private void Button_Click_SelectFilePath(object sender, RoutedEventArgs e)
        {
            var dlg = new Ookii.Dialogs.Wpf.VistaOpenFileDialog();
            var result = dlg.ShowDialog();
            if (result == true) this.filePath.Text = dlg.FileName;
        }

        //Send File
        private async void Button_Click_SendFile(object sender, RoutedEventArgs e)
        {
            try
            {
                var userMessage = this.userName2.Text;
                var filePath = this.filePath.Text;

                if (filePath != null)
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                    File.Encrypt(filePath);
                    var encryptedMessage = Encrypt.String("");

                    //MessageBox.Show(sr.ReadToEnd());
                    await _connection.InvokeAsync("SendMP3",
                       userName.Text, encryptedMessage);

                    file.Close();
                }



            }
            catch (Exception ex)
            {
                messageList.Items.Add(ex.Message);
            }
        }

    }
}
