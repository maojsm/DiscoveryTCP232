using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace USRLibray
{
    public class Tcp232SearchModel
    {
        // Configurações basicas (rede)
        public string? DeviceName { get; private set; }
        public string? DeviceIp { get; private set; }
        public string? DeviceMac { get; private set; }
        public bool IsValid => !string.IsNullOrEmpty(DeviceName) && !string.IsNullOrEmpty(DeviceIp) && !string.IsNullOrEmpty(DeviceMac);
        // Resposta em Hexadecimal para debug
        public string? ResponseBuffer { get; private set; }
        // Aqui contém o IP e Porta do modulo
        public IPEndPoint? RemoteEndPoint { get; set; }

        public Tcp232SearchModel(UdpReceiveResult response)
        {
            // Estrutura basica de uma respostas. Confere STX e Comprimento da Resposta.
            if (response.Buffer[0] == 0xFF && response.Buffer[1] == response.Buffer.Length)
            {
                // Caso seja a resposta de uma consulta Search (codigo = 0x01). Query = {0xFF, 0x01, 0x01, 0x02}.
                if (response.Buffer[2] == 0x01)
                {
                    DeviceName = USRTools.ByteArrayToAsciiString(response.Buffer, 19, 16);
                    DeviceIp = $"{response.Buffer[5]}.{response.Buffer[6]}.{response.Buffer[7]}.{response.Buffer[8]}";
                    DeviceMac = BitConverter.ToString(response.Buffer, 9, 6);
                    ResponseBuffer = BitConverter.ToString(response.Buffer);
                    RemoteEndPoint = response.RemoteEndPoint;
                }
            }
        }

        /// <summary>
        /// Cria uma string com as informações do modulo
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (IsValid)
            {
                sb.AppendLine($"DeviceName: {DeviceName}");
                sb.AppendLine($"staticIP = {DeviceIp}");
                sb.AppendLine($"userMAC = {DeviceMac}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Comando para localizar modulos na rede. 
        /// O comando é fixo independente das configurações do modulo.
        /// </summary>
        /// <returns></returns>
        public static byte[] QuerySearchUSRModule()
        {
            return new byte[] { 0xFF, 0x01, 0x01, 0x02 };
        }

    }
}
