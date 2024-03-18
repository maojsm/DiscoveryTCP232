using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using USRLibray;
using System;
using System.Windows.Forms;

namespace LocalizadorLocalJSM
{
    public partial class FrmLocalizadorTcp232 : Form
    {
        const int Port = 1901;//45000;

        public FrmLocalizadorTcp232()
        {
            InitializeComponent();

            //// Certifique-se de chamar esse m�todo no construtor do seu form ap�s o InitializeComponent()
            //dataGridView.Columns.Add("deviceNameColumn", "Device Name");
            //dataGridView.Columns.Add("staticIPColumn", "Static IP");
            //dataGridView.Columns.Add("userMACColumn", "MAC Address");
        }


        /// <summary>
        /// Adiciona linha no DataGridView do Device localizado.
        /// </summary>
        /// <param name="modulo"></param>
        private void AddDeviceToDataGridView(Tcp232SearchModel modulo)
        {
            if (modulo.IsValid)
            {
                // Adicionando uma nova linha com os valores fornecidos
                dataGridView.Rows.Add(modulo.DeviceName, modulo.DeviceIp, modulo.DeviceMac);
            }            
        }

        private void btnLimparLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private async void btnDiscoverServers_Click(object sender, EventArgs e)
        {
            txtLog.AppendText("Localizando m�dulos USR-TCP232 na rede...\r\n");

            List<Tcp232SearchModel> tcp232SearchList = await USRTools.DiscoveryModulesUSR();

            foreach (var modulo in tcp232SearchList)
            {
                if (modulo != null && modulo.IsValid)
                {
                    // Imprime informa��es de rede
                    string serverIP = modulo.RemoteEndPoint.Address.ToString();
                    int serverPort = modulo.RemoteEndPoint.Port;
                    txtLog.AppendText($"Server found: {serverIP}:{serverPort}\r\n");
                    // Imprime a resposta do modulo USR-TCP232
                    txtLog.AppendText(modulo.ResponseBuffer);
                    txtLog.AppendText("\r\n");

                    // Adiciona no DataGridView
                    AddDeviceToDataGridView(modulo);

                    // Imprime Log dos dados do modulo
                    txtLog.AppendText(modulo.ToString());
                    
                }
                else
                {
                    txtLog.AppendText("Dados invalidos ou nulos.\r\n");
                }
                txtLog.AppendText("\r\n------------\r\n");

            }


            //using (CancellationTokenSource cts = new CancellationTokenSource())
            //{
            //    cts.CancelAfter(5000); // Set the timeout for 5 seconds
            //    try
            //    {
            //        List<UdpReceiveResult> responses = await USRTools.DiscoverServers(cts.Token);
            //        foreach (var resp in responses)
            //        {
            //            string serverIP = resp.RemoteEndPoint.Address.ToString();
            //            int serverPort = resp.RemoteEndPoint.Port;
            //            txtLog.AppendText($"Server found: {serverIP}:{serverPort}\r\n");


            //            txtLog.AppendText(BitConverter.ToString(resp.Buffer));
            //            txtLog.AppendText("\r\n");

            //            Tcp232SearchModel moduloTcp232 = new Tcp232SearchModel(resp.Buffer);
            //            if (moduloTcp232.IsValid)
            //            {     
            //                // Adiciona no DataGridView
            //                AddDeviceToDataGridView(moduloTcp232);

            //                // Imprime a resposta do modulo USR-TCP232         
            //                txtLog.AppendText(moduloTcp232.ToString());                            
            //            }
            //            txtLog.AppendText("\r\n------------\r\n");

            //        }
            //    }
            //    catch (OperationCanceledException)
            //    {
            //        txtLog.AppendText("Operation was cancelled.\r\n");
            //    }
            //}

            txtLog.AppendText("Pesquisa conclu�da.\r\n");
        }

    }
}





//txtLog.Text = "Localizando modulos USR-TCP232 na rede...\r\n";

////List<IPAddress> localIPList = GetLocalIPAddressesOffLine();
//IPAddress localIP = GetLocalIPAddressOffLine();
//var broadcastIP = GetBroadcastAddress(localIP, "255.255.0.0"); // Substitua pela m�scara de rede correta, se necess�rio.


//using (var client = new UdpClient())
//{
//    client.EnableBroadcast = true;

//    // Faz a leitura das configura��es basicas do modulo USR-TCP232
//    //byte[] commandBytes = ReadSettingUSRModule("d8-b0-4c-f6-9d-57", "admin", "jsm20");
//    // Localiza modulo USR-TCP232 na porta 1901
//    byte[] commandBytes = USR_Commands.SearchUSRModule();

//    var commandString = BitConverter.ToString(commandBytes);
//    txtLog.AppendText($"Consulta do equipamento: {commandString}\r\n");


//    await client.SendAsync(commandBytes, commandBytes.Length, new IPEndPoint(broadcastIP, Port));

//    var timeoutTask = Task.Delay(5000); // Tempo limite de 5 segundos para resposta
//    var receiveTask = client.ReceiveAsync();

//    if (await Task.WhenAny(timeoutTask, receiveTask) == receiveTask)
//    {
//        var response = receiveTask.Result;
//        var deviceIP = response.RemoteEndPoint.Address.ToString();

//        // Exibir a resposta obtida
//        var responseString = BitConverter.ToString(response.Buffer);
//        txtLog.AppendText($"Resposta do equipamento: {responseString}\r\n");
//        txtLog.AppendText($"Equipamento encontrado: {deviceIP} \r\n");

//        // Imprime a resposta do modulo USR-TCP232
//        string respUSR = USR_Commands.RespParse(response.Buffer);
//        txtLog.AppendText(respUSR);
//    }

//    else
//    {
//        txtLog.AppendText("Equipamento n�o encontrado!!!\r\n");
//        Console.ReadLine();
//    }
//}



//static IPAddress GetLocalIPAddress()
//{
//    using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
//    {
//        socket.Connect("8.8.8.8", 65530); // Conecta ao servidor DNS do Google para obter o endere�o IP local
//        return (socket.LocalEndPoint as IPEndPoint)?.Address;
//    }
//}

///// <summary>
///// Nao usa internet!
///// </summary>
///// <returns></returns>
///// <exception cref="Exception"></exception>
//static IPAddress GetLocalIPAddressOffLine()
//{
//    foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
//    {
//        // Verifica se a interface est� ativa e suporta IPv4
//        if (netInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
//            netInterface.OperationalStatus == OperationalStatus.Up)
//        {
//            foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
//            {
//                // Seleciona um endere�o IPv4 que n�o seja de loopback
//                if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
//                {
//                    return addrInfo.Address;
//                }
//            }
//        }
//    }

//    throw new Exception("Nenhum endere�o IP local v�lido foi encontrado.");
//}

///// <summary>
///// Nao usa internet!
///// Retorna uma lista de endere�os IP locais encontrados.
///// </summary>
///// <returns>Uma lista de endere�os IP locais.</returns>
///// <exception cref="Exception">Lan�a uma exce��o se nenhum endere�o IP local v�lido for encontrado.</exception>
//static List<IPAddress> GetLocalIPAddressesOffLine()
//{
//    List<IPAddress> localIPs = new List<IPAddress>();

//    foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
//    {
//        // Verifica se a interface est� ativa e suporta IPv4
//        if (netInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
//            netInterface.OperationalStatus == OperationalStatus.Up)
//        {
//            foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
//            {
//                // Seleciona um endere�o IPv4 que n�o seja de loopback
//                if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
//                {
//                    localIPs.Add(addrInfo.Address);
//                }
//            }
//        }
//    }

//    if (localIPs.Count == 0)
//    {
//        throw new Exception("Nenhum endere�o IP local v�lido foi encontrado.");
//    }

//    return localIPs;
//}


//static IPAddress GetBroadcastAddress(IPAddress address, string subnetMask)
//{
//    var ipAddressBytes = address.GetAddressBytes();
//    var subnetMaskBytes = IPAddress.Parse(subnetMask).GetAddressBytes();

//    if (ipAddressBytes.Length != subnetMaskBytes.Length)
//        throw new ArgumentException("Endere�o e m�scara de sub-rede devem ter o mesmo comprimento.");

//    var broadcastAddress = new byte[ipAddressBytes.Length];
//    for (int i = 0; i < broadcastAddress.Length; i++)
//    {
//        broadcastAddress[i] = (byte)(ipAddressBytes[i] | (subnetMaskBytes[i] ^ 255));
//    }

//    return new IPAddress(broadcastAddress);
//}



//static IPAddress[] GetAllSubnetMasks()
//{
//    // Retorna uma lista de todas as m�scaras de sub-rede poss�veis
//    return new IPAddress[]
//    {
//    IPAddress.Parse("255.0.0.0"),
//    IPAddress.Parse("255.128.0.0"),
//    IPAddress.Parse("255.192.0.0"),
//    IPAddress.Parse("255.224.0.0"),
//    IPAddress.Parse("255.240.0.0"),
//    IPAddress.Parse("255.248.0.0"),
//    IPAddress.Parse("255.252.0.0"),
//    IPAddress.Parse("255.254.0.0"),
//    IPAddress.Parse("255.255.0.0"),
//    IPAddress.Parse("255.255.128.0"),
//    IPAddress.Parse("255.255.192.0"),
//    IPAddress.Parse("255.255.224.0"),
//    IPAddress.Parse("255.255.240.0"),
//    IPAddress.Parse("255.255.248.0"),
//    IPAddress.Parse("255.255.252.0"),
//    IPAddress.Parse("255.255.254.0"),
//    IPAddress.Parse("255.255.255.0"),
//    IPAddress.Parse("255.255.255.128"),
//    IPAddress.Parse("255.255.255.192"),
//    IPAddress.Parse("255.255.255.224"),
//    IPAddress.Parse("255.255.255.240"),
//    IPAddress.Parse("255.255.255.248"),
//    IPAddress.Parse("255.255.255.252"),
//    IPAddress.Parse("255.255.255.254")
//    };
//}


//static IPAddress GetBroadcastAddress(IPAddress address, IPAddress subnetMask)
//{
//    var addressBytes = address.GetAddressBytes();
//    var maskBytes = subnetMask.GetAddressBytes();
//    var broadcastBytes = new byte[addressBytes.Length];

//    for (int i = 0; i < addressBytes.Length; i++)
//    {
//        broadcastBytes[i] = (byte)(addressBytes[i] | (maskBytes[i] ^ 255));
//    }

//    return new IPAddress(broadcastBytes);
//}

//static async Task SendBroadcast(IPAddress broadcastIP)
//{
//    using (var client = new UdpClient())
//    {
//        client.EnableBroadcast = true;

//        byte[] commandBytes = {/* Comando que voc� deseja enviar */};
//        await client.SendAsync(commandBytes, commandBytes.Length, new IPEndPoint(broadcastIP, Port));
//    }
//}

///// <summary>
///// Converte um trecho de 4 bytes em um endere�o IP no formato "xxx.xxx.xxx.xxx"
///// </summary>
///// <param name="bytes"></param>
///// <param name="startIndex"></param>
///// <param name="length"></param>
///// <returns></returns>
///// <exception cref="ArgumentException"></exception>
///// <exception cref="ArgumentOutOfRangeException"></exception>
//public static string BytesToIPAddress(byte[] bytes, int startIndex)
//{
//    // Use o m�todo 'Join' para concatenar os quatro bytes usando '.' como separador
//    return string.Join(".", bytes.Skip(startIndex).Take(4).Select(b => b.ToString()));
//}
