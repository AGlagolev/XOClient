using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace XOClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isConnected = false;
        private TcpClient _client;
        public MainWindow()
        {
            InitializeComponent();
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if (_client != null)
            {
                _client.Close();
            }
        }


        private void button_SendMessage_Click(object sender, RoutedEventArgs e)
        {
                if (_client != null)
                {
                    _client.Close();
                }

                string IP = textBox_IP.Text;
                string PORT = textBox_PORT.Text;
                string UserName = textBox_User.Text;

                IPAddress address;

                try
                {
                    if (IPAddress.TryParse(IP, out address) && PORT.Length > 0)
                    {
                        IPEndPoint iPEndPoint = new IPEndPoint(address, Convert.ToInt32(PORT));
                        _client = new TcpClient();
                        _client.Connect(iPEndPoint); /// подключение к удаленному серверу по сокету
                        NetworkStream networkStream = _client.GetStream();

                        //string userMessage = textBoxUserMessage.Text;
                        byte[] byteArr = Encoding.Unicode.GetBytes("NewUser:" + UserName);
                        networkStream.Write(byteArr, 0, byteArr.Length);
                        //textBoxUserMessage.Text = "";
                        //label_status.Content = "Message sended";
                        _client.Close();
                    }
                }
                catch (SocketException socketEx)
                {
                    MessageBox.Show("SocketException SERVER:" + socketEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception SERVER:" + ex.Message);
                }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (((TextBlock)sender).Text == "X")
            {
            ((TextBlock)sender).Text = "";

            }
            else
            {
                ((TextBlock)sender).Text = "X";
            }
        }
    }
}
