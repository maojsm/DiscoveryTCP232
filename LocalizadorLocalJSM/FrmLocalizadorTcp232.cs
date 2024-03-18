using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using USRLibray;
using System;
using System.Windows.Forms;
using LocalizadorLocalJSM.Properties;
using System.Diagnostics;

namespace LocalizadorLocalJSM
{
    public partial class FrmLocalizadorTcp232 : Form
    {
        const int Port = 1901;//45000;

        public FrmLocalizadorTcp232()
        {
            InitializeComponent();
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
            txtLog.AppendText("Pesquisando por módulos USR-TCP232 na rede...\r\n");
            // Obtem a lista de Modulos TCP232 na rede local.
            List<Tcp232SearchModel> tcp232SearchList = await USRTools.DiscoveryModulesUSR();
            // Limpa as linhas do dataGridView
            dataGridView.Rows.Clear();
            // Coloca os modulos encontrados no DataGridView
            foreach (var modulo in tcp232SearchList)
            {
                if (modulo.IsValid)
                {
                    // Imprime informações de rede
                    string serverIP = modulo.RemoteEndPoint != null ? modulo.RemoteEndPoint.Address.ToString() : string.Empty;
                    int serverPort = modulo.RemoteEndPoint != null ? modulo.RemoteEndPoint.Port : 0;
                    txtLog.AppendText($"Servidor encontrado: {serverIP}:{serverPort}\r\n");
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
                txtLog.AppendText("---------------\r\n\r\n");
            }
            txtLog.AppendText("Pesquisa concluída.\r\n");
        }

        /// <summary>
        /// Abre o manual do modulo que deverá estar na pasta Resources, colocado lá durante a instalação do aplicativo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            string pdfPath = Path.Combine(Application.StartupPath, "Resources", "USR-TCP232-E2-Hardware-Manual_V1.0.0.1.pdf");

            // Use ProcessStartInfo para especificar que queremos abrir o arquivo com o programa padrão
            var startInfo = new ProcessStartInfo(pdfPath)
            {
                UseShellExecute = true // Importante para abrir o arquivo com o aplicativo padrão
            };

            System.Diagnostics.Process.Start(startInfo);
        }
    }
}
