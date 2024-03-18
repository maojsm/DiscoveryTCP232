using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    const int Port = 1901;//45000;


    static async Task Main(string[] args)
    {
        //Console.WriteLine("Procurando equipamento na rede...");

        //List<IPAddress> localIPList = GetLocalIPAddressesOffLine();
        //var broadcastIP = GetBroadcastAddress(localIPList[1], "255.255.255.0"); // Substitua pela máscara de rede correta, se necessário.


        //using (var client = new UdpClient())
        //{
        //    client.EnableBroadcast = true;

        //    // Faz a leitura das configurações basicas do modulo USR-TCP232
        //    //byte[] commandBytes = ReadSettingUSRModule("d8-b0-4c-f6-9d-57", "admin", "jsm20");
        //    // Localiza modulo USR-TCP232 na porta 1901
        //    byte[] commandBytes = SearchUSRModule();

        //    var commandString = BitConverter.ToString(commandBytes);
        //    Console.WriteLine($"Consulta do equipamento: {commandString}");          


        //    await client.SendAsync(commandBytes, commandBytes.Length, new IPEndPoint(broadcastIP, Port));

        //    var timeoutTask = Task.Delay(5000); // Tempo limite de 5 segundos para resposta
        //    var receiveTask = client.ReceiveAsync();

        //    if (await Task.WhenAny(timeoutTask, receiveTask) == receiveTask)
        //    {
        //        var response = receiveTask.Result;
        //        var deviceIP = response.RemoteEndPoint.Address.ToString();

        //        // Exibir a resposta obtida
        //        var responseString = BitConverter.ToString(response.Buffer);
        //        Console.WriteLine($"Resposta do equipamento: {responseString}");
        //        Console.WriteLine($"Equipamento encontrado: {deviceIP}");


        //        RespParse
        //        Console.WriteLine(USR);
                               

        //        // Solicitar ao usuário que digite o nome do equipamento
        //        Console.Write("Pressione qualquer tecla para fechar...");
        //        Console.ReadLine();
        //    }

        //    else
        //    {
        //        Console.WriteLine("Equipamento não encontrado. Pressione qualquer tecla para fechar...");
        //        Console.ReadLine();
        //    }
        //}
    }

    static IPAddress GetLocalIPAddress()
    {
        using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530); // Conecta ao servidor DNS do Google para obter o endereço IP local
            return (socket.LocalEndPoint as IPEndPoint)?.Address;
        }
    }


    ///// <summary>
    ///// Nao usa internet!
    ///// </summary>
    ///// <returns></returns>
    ///// <exception cref="Exception"></exception>
    //static IPAddress GetLocalIPAddressOffLine()
    //{
    //    foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
    //    {
    //        // Verifica se a interface está ativa e suporta IPv4
    //        if (netInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
    //            netInterface.OperationalStatus == OperationalStatus.Up)
    //        {
    //            foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
    //            {
    //                // Seleciona um endereço IPv4 que não seja de loopback
    //                if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
    //                {
    //                    return addrInfo.Address;
    //                }
    //            }
    //        }
    //    }

    //    throw new Exception("Nenhum endereço IP local válido foi encontrado.");
    //}


    /// <summary>
    /// Nao usa internet!
    /// Retorna uma lista de endereços IP locais encontrados.
    /// </summary>
    /// <returns>Uma lista de endereços IP locais.</returns>
    /// <exception cref="Exception">Lança uma exceção se nenhum endereço IP local válido for encontrado.</exception>
    static List<IPAddress> GetLocalIPAddressesOffLine()
    {
        List<IPAddress> localIPs = new List<IPAddress>();

        foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            // Verifica se a interface está ativa e suporta IPv4
            if (netInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                netInterface.OperationalStatus == OperationalStatus.Up)
            {
                foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                {
                    // Seleciona um endereço IPv4 que não seja de loopback
                    if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIPs.Add(addrInfo.Address);
                    }
                }
            }
        }

        if (localIPs.Count == 0)
        {
            throw new Exception("Nenhum endereço IP local válido foi encontrado.");
        }

        return localIPs;
    }


    static IPAddress GetBroadcastAddress(IPAddress address, string subnetMask)
    {
        var ipAddressBytes = address.GetAddressBytes();
        var subnetMaskBytes = IPAddress.Parse(subnetMask).GetAddressBytes();

        if (ipAddressBytes.Length != subnetMaskBytes.Length)
            throw new ArgumentException("Endereço e máscara de sub-rede devem ter o mesmo comprimento.");

        var broadcastAddress = new byte[ipAddressBytes.Length];
        for (int i = 0; i < broadcastAddress.Length; i++)
        {
            broadcastAddress[i] = (byte)(ipAddressBytes[i] | (subnetMaskBytes[i] ^ 255));
        }

        return new IPAddress(broadcastAddress);
    }



    static IPAddress[] GetAllSubnetMasks()
    {
        // Retorna uma lista de todas as máscaras de sub-rede possíveis
        return new IPAddress[]
        {
            IPAddress.Parse("255.0.0.0"),
            IPAddress.Parse("255.128.0.0"),
            IPAddress.Parse("255.192.0.0"),
            IPAddress.Parse("255.224.0.0"),
            IPAddress.Parse("255.240.0.0"),
            IPAddress.Parse("255.248.0.0"),
            IPAddress.Parse("255.252.0.0"),
            IPAddress.Parse("255.254.0.0"),
            IPAddress.Parse("255.255.0.0"),
            IPAddress.Parse("255.255.128.0"),
            IPAddress.Parse("255.255.192.0"),
            IPAddress.Parse("255.255.224.0"),
            IPAddress.Parse("255.255.240.0"),
            IPAddress.Parse("255.255.248.0"),
            IPAddress.Parse("255.255.252.0"),
            IPAddress.Parse("255.255.254.0"),
            IPAddress.Parse("255.255.255.0"),
            IPAddress.Parse("255.255.255.128"),
            IPAddress.Parse("255.255.255.192"),
            IPAddress.Parse("255.255.255.224"),
            IPAddress.Parse("255.255.255.240"),
            IPAddress.Parse("255.255.255.248"),
            IPAddress.Parse("255.255.255.252"),
            IPAddress.Parse("255.255.255.254")
        };
    }


    static IPAddress GetBroadcastAddress(IPAddress address, IPAddress subnetMask)
    {
        var addressBytes = address.GetAddressBytes();
        var maskBytes = subnetMask.GetAddressBytes();
        var broadcastBytes = new byte[addressBytes.Length];

        for (int i = 0; i < addressBytes.Length; i++)
        {
            broadcastBytes[i] = (byte)(addressBytes[i] | (maskBytes[i] ^ 255));
        }

        return new IPAddress(broadcastBytes);
    }

    static async Task SendBroadcast(IPAddress broadcastIP)
    {
        using (var client = new UdpClient())
        {
            client.EnableBroadcast = true;

            byte[] commandBytes = {/* Comando que você deseja enviar */};
            await client.SendAsync(commandBytes, commandBytes.Length, new IPEndPoint(broadcastIP, Port));
        }
    }

    /// <summary>
    /// Converte um trecho de 4 bytes em um endereço IP no formato "xxx.xxx.xxx.xxx"
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string BytesToIPAddress(byte[] bytes, int startIndex)
    {
        // Use o método 'Join' para concatenar os quatro bytes usando '.' como separador
        return string.Join(".", bytes.Skip(startIndex).Take(4).Select(b => b.ToString()));
    }








 


}
