using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace USRLibray
{
    public class USRTools
    {
        /// <summary>
        /// Localiza equipamentos USR-TCP232 na rede local usando UDP em Broadcast.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<List<UdpReceiveResult>> DiscoverServers(CancellationToken cancellationToken)
        {
            int portUdp = 1901;
            List<UdpReceiveResult> foundServers = new List<UdpReceiveResult>();

            using (var client = new UdpClient())
            {
                client.EnableBroadcast = true;
                var commandBytes = new byte[] { 0xFF, 0x01, 0x01, 0x02 };
                var broadcastEndpoint = new IPEndPoint(IPAddress.Broadcast, portUdp);

                await client.SendAsync(commandBytes, commandBytes.Length, broadcastEndpoint);

                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var responseTask = client.ReceiveAsync();

                        // We wait for either the task to complete or the cancellation token to be triggered
                        if (await Task.WhenAny(responseTask, Task.Delay(Timeout.Infinite, cancellationToken)) == responseTask)
                        {
                            foundServers.Add(await responseTask);
                        }
                        else
                        {
                            // The cancellationToken was triggered, break out of the loop
                            break;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // This exception is expected when the cancellation token is triggered
                }
                finally
                {
                    client.Close();
                }
            }

            return foundServers;
        }


        public static string RespParse(byte[] responseBuffer)
        {

            if (responseBuffer[0] != 0xFF)
            {
                return $"Erro STX não Encontrado.";
            }
            if (responseBuffer[1] != responseBuffer.Length)
            {
                return $"Dados incompletos {responseBuffer[1]} != {responseBuffer.Length}.";
            }

            StringBuilder sb = new StringBuilder();

            // Procura modulo USR-TCP232 na rede usando a porta 1901.
            if (responseBuffer[2] == 0x01)
            {
                string deviceName = ByteArrayToAsciiString(responseBuffer, 19, 16);
                string moduleStaticIP = $"{responseBuffer[5]}.{responseBuffer[6]}.{responseBuffer[7]}.{responseBuffer[8]}";
                string userMAC = BitConverter.ToString(responseBuffer, 9, 6);

                sb.AppendLine($"deviceName: {deviceName}");
                sb.AppendLine($"staticIP = {moduleStaticIP}");                
                sb.AppendLine($"userMAC = {userMAC}");
                

            }
            // Configurações basicas do modulo USR-TCP232.
            else if (responseBuffer[2] == 0x03)
            {
                string flagIpType = responseBuffer[3] == 0x80 ? "Static IP"/*0x00*/: "DHCP"/*0x80*/;
                sb.AppendLine($"flagIpType = {flagIpType}");
                string moduleStaticIP = $"{responseBuffer[12]}.{responseBuffer[11]}.{responseBuffer[10]}.{responseBuffer[9]}";
                sb.AppendLine($"staticIP = {moduleStaticIP}");
                string gatewayIP = $"{responseBuffer[16]}.{responseBuffer[15]}.{responseBuffer[14]}.{responseBuffer[13]}";
                sb.AppendLine($"gatewayIP = {gatewayIP}");
                string subNetMask = $"{responseBuffer[20]}.{responseBuffer[19]}.{responseBuffer[18]}.{responseBuffer[17]}";
                sb.AppendLine($"subNetMask = {subNetMask}");
                string daviceName = ByteArrayToAsciiString(responseBuffer, 21, 16);
                sb.AppendLine($"daviceNameY: {daviceName}");
                string userName = ByteArrayToAsciiString(responseBuffer, 37, 6);
                sb.AppendLine($"userName = {userName}");
                string password = ByteArrayToAsciiString(responseBuffer, 43, 6);
                sb.AppendLine($"password = {password}");
                string deviceID = $"{responseBuffer[50]}-{responseBuffer[51]}";
                sb.AppendLine($"deviceID = {deviceID}");
                string userMAC = BitConverter.ToString(responseBuffer, 53, 6);
                sb.AppendLine($"userMAC = {userMAC}");
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine("Comando não reconhecido.");
                sb.AppendLine();
            }

            return sb.ToString();
        }


        /// <summary>
        /// Comando para localizar modulos na rede. O comando é fixo independente das configurações do modulo
        /// </summary>
        /// <returns></returns>
        public static byte[] SearchUSRModule()
        {
            return new byte[] { 0xFF, 0x01, 0x01, 0x02 };
        }

        /// <summary>
        /// COMANDO DE CONSULTA ÀS CONFIGURAÇÕES BASICAS DO MODULO USR-TCP232
        /// Consulta do equipamento: FF-13-03-D8-B0-4C-F6-9D-57-61-64-6D-69-6E-00-6A-73-6D-32-30-00-89
        /// Resposta do equipamento: 
        /// 1E-3C-03-80-20-19-50-00-02-C9-01-01-0A-01-01-01-0A-00-FF-FF-FF-4A-53-4D-5F-30-31-30-31-30-
        /// 31-00-00-00-00-00-00-61-64-6D-69-6E-00-6A-73-6D-32-30-00-02-01-00-00-D8-B0-4C-F6-9D-57-10-
        /// 0E-00-00-0B-00-00-00-00-C2-01-00-08-01-01-01-00-00-00-00-17-00-51-C3-31-38-36-2E-32-30-38-
        /// 2E-31-33-32-2E-37-31-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-47-84-D0-BA-09-01-00-
        /// 00-00-00-00-00-81-01-01-01-01-00-C2-01-00-08-01-01-01-00-00-00-00-C8-AF-3B-F2-31-30-2E-31-
        /// 2E-31-2E-35-30-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-32-01-01-0A-
        /// 0A-02-00-00-00-00-00-00-81-00-00-00-00
        /// </summary>
        public static byte[] ReadSettingUSRModule(
            string macAddressString = "d8-b0-4c-f6-9d-57",
            string userNameString = "admin",
            string passwordString = "jsm20")
        {
            byte[] ucUserMAC = ConvertMacToBytes(macAddressString);//new byte[] { 0xD8, 0xB0, 0x4C, 0xF6, 0x9D, 0x57 };
            byte[] username = ConvertStringToBytes(userNameString, 6);
            byte[] password = ConvertStringToBytes(passwordString, 6);
            byte[] parameters = new byte[] { };

            // Faz a leitura da configuração do modulo USR-TCP232
            // 0xFF [comprimento total] 0x03 [ucUserMAC] [username] [password] [parameters] [parity]
            // parity é a soma de todos os bytes anteriores
            return BuildCommand(ucUserMAC, username, password, parameters);
        }



        /// <summary>
        /// Usado para converter em Bytes uma string de username ou password do modulo USR-TCP232
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fixedSize"></param>
        /// <returns></returns>
        public static byte[] ConvertStringToBytes(string input, int fixedSize = 6)
        {
            // Convert the string to an array of bytes
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);

            // Initialize the array of bytes with the fixed size
            byte[] byteArray = new byte[fixedSize];

            // Copy the input bytes to the array
            // If input is shorter than the fixed size, it will only copy the available bytes
            Array.Copy(inputBytes, byteArray, Math.Min(inputBytes.Length, fixedSize));

            // If the string is shorter than the fixed size, the rest of the array will remain zeros
            return byteArray;
        }


        /// <summary>
        /// Converte um array de bytes em uma string de caracteres ASCII
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string ByteArrayToAsciiString(byte[] byteArray, int startIndex, int length)
        {
            // Assegure-se de que os índices não estão fora dos limites do array
            if (startIndex < 0 || length < 0 || startIndex + length > byteArray.Length)
            {
                // Habilitar para debug
                //throw new ArgumentOutOfRangeException("startIndex or length is out of range.");
                return string.Empty;
            }

            // StringBuilder é uma boa opção para construir strings em loops
            StringBuilder sb = new StringBuilder();

            for (int i = startIndex; i < startIndex + length; i++)
            {
                byte b = byteArray[i];
                if (b >= 32 && b <= 126) // Intervalo de caracteres ASCII imprimíveis
                {
                    sb.Append(Convert.ToChar(b));
                }
                else if (b == 0) // Tratamento para byte nulo (fim de string)
                {
                    break; // Interrompe a adição de caracteres se um byte nulo for encontrado
                }
                else
                {
                    sb.Append('.'); // Representa bytes não imprimíveis com um ponto
                }
            }


            // Retorna a string construída
            return sb.ToString();
        }


        /// <summary>
        /// Usado na conversão de um MAC Address em um array de bytes
        /// Converte string "d8-b0-4c-f6-9d-57" em um array de bytes { 0xD8, 0xB0, 0x4C, 0xF6, 0x9D, 0x57 }
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        private static byte[] ConvertMacToBytes(string mac)
        {
            string cleanMac = mac.Replace("-", string.Empty);
            int numberOfBytes = cleanMac.Length / 2;
            byte[] bytes = new byte[numberOfBytes];

            for (int i = 0; i < numberOfBytes; i++)
            {
                string byteValue = cleanMac.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(byteValue, 16);
            }

            return bytes;
        }

        /// <summary>
        /// Calcula o byte de paridade para um comando do modulo USR-TCP232
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte CalculateParity(byte[] data)
        {
            int sum = 0;
            foreach (var b in data)
            {
                sum += b;
            }
            return (byte)(sum & 0xFF); // Assegura que o resultado é um byte
        }

        /// <summary>
        /// Monta um comando para o modulo USR-TCP232
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static byte[] BuildCommand(byte[] mac, byte[] user, byte[] pass, byte[] parameters)
        {
            byte[] length = new byte[] { (byte)(1 + mac.Length + user.Length + pass.Length + parameters.Length) }; // +1 para o comprimento do comando
            byte[] commandWithoutParity = new byte[] { 0xFF };
            commandWithoutParity = commandWithoutParity.Concat(length).ToArray();
            commandWithoutParity = commandWithoutParity.Concat(new byte[] { 0x03 }).ToArray(); // The command byte
            commandWithoutParity = commandWithoutParity.Concat(mac).ToArray();
            commandWithoutParity = commandWithoutParity.Concat(user).ToArray();
            commandWithoutParity = commandWithoutParity.Concat(pass).ToArray();
            commandWithoutParity = commandWithoutParity.Concat(parameters).ToArray();

            byte parity = CalculateParity(commandWithoutParity.Skip(1).ToArray()); // Skip the first 0xFF
            return commandWithoutParity.Concat(new byte[] { parity }).ToArray();
        }

    }
}
