using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using PainelDeSenhas.Serialization;
using Websocket.Client;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PainelDeSenhas
{
    /**
     * Tela Principal para exibir imagem da TV e útimas senhas
     * author Silvio Watakabe <silvio@tcmed.com.br>
     * @since 18-12-2020
     * @version 1.0
     */
    public partial class TelaInicial : Form
    {
        private VideoCaptureDevice videoSource;
        public  WebsocketClient client;
        public  string messageSended;
        public  Senha senhaThreadExterna = new Senha();
        public  List<Senha> arraySenhas = new List<Senha>();
        public  List<Senha> JsonSenhas  = new List<Senha>();
        private delegate void DelegateChamadaSegura(List<Senha> senhas);
        public  DateTime today;
        public  DateTime configDate = new DateTime();
        public Config config;

        /// <summary>
        /// Inicia os primeiros métodos para carregar o form
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        public TelaInicial()
        {
            InitializeComponent();

            var videoSources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoSources != null && videoSources.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoSources[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
            }
            TurnOnOffCamera();
        }

        /// <summary>
        /// Carrega outras dependencias do Form
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.FromPoint(MousePosition).Bounds;
            Conectar();
            CarregarSenhas();

            // Se existir chama a primeira senha gravada na variavel senhaThreadExterna
            if (senhaThreadExterna.Local != null) {
                Thread threadShowLastSenha = new Thread(new ThreadStart(ChamaUltimaSenha));
                threadShowLastSenha.Start();
            }
        }

        /// <summary>
        /// Chama última senha por meio de outra thread
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        private void ChamaUltimaSenha()
        {
            FormChamaSenha formSenha = new FormChamaSenha();
            formSenha.SenhaChamada = senhaThreadExterna;
            formSenha.Show();
            Application.Run();
        }


        /// <summary>
        /// Lê o .Json de senhas de senhas
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="senhas">Lista de Senhas</param> 
        private void CarregarSenhas() {
            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\senhas.json");
            JsonSenhas = ConvertTextSenhasToJson(json);
            // Verifica se a data é diferente da atual, se verdadeiro zera o arquivo senhas.json
            Boolean checkdate = CheckDateSenhas();
            if (checkdate)
            {
                JsonSenhas = new List<Senha>();
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\senhas.json", "");
            }
            CarregaSenhasPainel(JsonSenhas);
        }

        /// <summary>
        /// Lê a última data do Json de config
        /// Caso seja diferente da data atual grava um Json em Branco das Senhas
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="senhas">Lista de Senhas</param> 
        Boolean CheckDateSenhas()
        {
            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\config.json");
            config = ConvertTextConfigToJson(json);

            if (config.DateSenhas != string.Empty)
            {
                configDate = DateTime.ParseExact(config.DateSenhas, "dd/MM/yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
            }

            DateTime hoje = DateTime.Now;

            if (configDate.ToShortDateString() != hoje.ToShortDateString())
            {
                config.DateSenhas = DateTime.Now.ToShortDateString();
                SaveDateOfListSenhas(config);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Carrega os Labels de senhas conforme a quantidade de senhas disponiveis
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="senhas">Lista de Senhas</param> 
        private void CarregaSenhasPainel(List<Senha> senhas)
        {
            int qtdSenhas = senhas.Count;
            int i2;
            int indice = SetIndiceSenhas(qtdSenhas);
            int inicio = qtdSenhas - indice;
            if (qtdSenhas > 0)
            {
                for (int i = inicio; i < qtdSenhas; i++)
                {
                    if (i + 1 == qtdSenhas)
                    {
                        lbSenhaP.Text = "SENHA";
                        lbSenhaNumP.Text = senhas[i].SenhaNum.ToString();
                        lbLocalP.Text = senhas[i].Local;
                        lbLocalNumP.Text = senhas[i].LocalNum.ToString();
                        int qtdApaga = 3;

                        // Chama a Última senha caso exista
                        senhaThreadExterna = senhas[i];                        

                        if (qtdSenhas > 1)
                        {
                            i2 = i - 1;
                            lbSenha1.Text = "SENHA";
                            lbSenhaNum1.Text = senhas[i2].SenhaNum.ToString();
                            lbLocal1.Text = senhas[i2].Local;
                            lbLocalNum1.Text = senhas[i2].LocalNum.ToString();
                            qtdApaga = 2;
                        }

                        if (qtdSenhas > 2)
                        {
                            i2 = i - 2;
                            lbSenha2.Text = "SENHA";
                            lbSenhaNum2.Text = senhas[i2].SenhaNum.ToString();
                            lbLocal2.Text = senhas[i2].Local;
                            lbLocalNum2.Text = senhas[i2].LocalNum.ToString();
                            qtdApaga = 1;
                        }

                        if (qtdSenhas > 3)
                        {
                            i2 = i - 3;
                            lbSenha3.Text = "SENHA";
                            lbSenhaNum3.Text = senhas[i2].SenhaNum.ToString();
                            lbLocal3.Text = senhas[i2].Local;
                            lbLocalNum3.Text = senhas[i2].LocalNum.ToString();
                            qtdApaga = 0;
                        }

                        if (qtdApaga > 0)
                        {
                            ApagaLabels(qtdApaga);
                        }
                    }
                }
            }
            else {
                ApagaLabels(4);
            }
        }

        /// <summary>
        /// Apaga os Labels de outra Thread dependendo da quantidade de senhas carregadas no senhas.json
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="qtd">qtd int</param>
        public void ApagaLabelsThread(int qtd)
        {
            switch (qtd)
            {
                case 1:
                    SetText(lbSenha3, "");
                    SetText(lbSenhaNum3, "");
                    SetText(lbLocal3, "");
                    SetText(lbLocalNum3, "");
                    break;
                case 2:
                    SetText(lbSenha3, "");
                    SetText(lbSenhaNum3, "");
                    SetText(lbLocal3, "");
                    SetText(lbLocalNum3, "");
                    SetText(lbSenha2, "");
                    SetText(lbSenhaNum2, "");
                    SetText(lbLocal2, "");
                    SetText(lbLocalNum2, "");
                    break;
                case 3:
                    SetText(lbSenha3, "");
                    SetText(lbSenhaNum3, "");
                    SetText(lbLocal3, "");
                    SetText(lbLocalNum3, "");
                    SetText(lbSenha2, "");
                    SetText(lbSenhaNum2, "");
                    SetText(lbLocal2, "");
                    SetText(lbLocalNum2, "");
                    SetText(lbSenha1, "");
                    SetText(lbSenhaNum1, "");
                    SetText(lbLocal1, "");
                    SetText(lbLocalNum1, "");
                    break;
                case 4:
                    SetText(lbSenha3, "");
                    SetText(lbSenhaNum3, "");
                    SetText(lbLocal3, "");
                    SetText(lbLocalNum3, "");
                    SetText(lbSenha2, "");
                    SetText(lbSenhaNum2, "");
                    SetText(lbLocal2, "");
                    SetText(lbLocalNum2, "");
                    SetText(lbSenha1, "");
                    SetText(lbSenhaNum1, "");
                    SetText(lbLocal1, "");
                    SetText(lbLocalNum1, "");
                    SetText(lbSenhaP, "");
                    SetText(lbSenhaNumP, "");
                    SetText(lbLocalP, "");
                    SetText(lbLocalNumP, "");
                    break;
            }
        }

        /// <summary>
        /// Apaga os Labels do Form dependendo da quantidade de senhas carregadas no senhas.json
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="qtd">qtd int</param>
        public void ApagaLabels(int qtd) {
            switch (qtd) {
                case 1:
                    lbSenha3.Text = "";
                    lbSenhaNum3.Text = "";
                    lbLocal3.Text = "";
                    lbLocalNum3.Text = "";
                    break;
                case 2:
                    lbSenha3.Text = "";
                    lbSenhaNum3.Text = "";
                    lbLocal3.Text = "";
                    lbLocalNum3.Text = "";
                    lbSenha2.Text = "";
                    lbSenhaNum2.Text = "";
                    lbLocal2.Text = "";
                    lbLocalNum2.Text = "";
                    break;
                case 3:
                    lbSenha3.Text = "";
                    lbSenhaNum3.Text = "";
                    lbLocal3.Text = "";
                    lbLocalNum3.Text = "";
                    lbSenha2.Text = "";
                    lbSenhaNum2.Text = "";
                    lbLocal2.Text = "";
                    lbLocalNum2.Text = "";
                    lbSenha1.Text = "";
                    lbSenhaNum1.Text = "";
                    lbLocal1.Text = "";
                    lbLocalNum1.Text = "";
                    break;
                case 4:
                    lbSenha3.Text = "";
                    lbSenhaNum3.Text = "";
                    lbLocal3.Text = "";
                    lbLocalNum3.Text = "";
                    lbSenha2.Text = "";
                    lbSenhaNum2.Text = "";
                    lbLocal2.Text = "";
                    lbLocalNum2.Text = "";
                    lbSenha1.Text = "";
                    lbSenhaNum1.Text = "";
                    lbLocal1.Text = "";
                    lbLocalNum1.Text = "";
                    lbSenhaP.Text = "";
                    lbSenhaNumP.Text = "";
                    lbLocalP.Text = "";
                    lbLocalNumP.Text = "";
                    break;
            }
        }

        /// <summary>
        /// Converte arquivo de texto Json em Array de List<Senha>
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="json">string json</param>
        List<Senha> ConvertTextSenhasToJson(string json)
        {
            var senhas = new List<Senha>();
            if (json != "" && json != null)
            {
                var js = new DataContractJsonSerializer(typeof(List<Senha>));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                senhas = (List<Senha>)js.ReadObject(ms);
            }
            return senhas;
        }

        /// <summary>
        /// Converte arquivo de texto Json em Array de List<Config>
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="json">string json</param>
        Config ConvertTextConfigToJson(string json)
        {
            List<Config> configs = new List<Config>();
            configs.Add(new Config());
            if (json != "" && json != null)
            {
                var js  = new DataContractJsonSerializer(typeof(List<Config>));
                var ms  = new MemoryStream(Encoding.UTF8.GetBytes(json));
                configs = (List<Config>)js.ReadObject(ms);
            }
            return configs[0];
        }

        /// <summary>
        /// Salva arquivo config.json com a última data do arquivo de senhas
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="json">string json</param>
        private void SaveDateOfListSenhas(Config config)
        {
            if (config.Volume > 0) {
                List<Config> configs = new List<Config>();
                configs.Add(config);

                var json_serializado = JsonConvert.SerializeObject(configs);

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\config.json", json_serializado);
            }
        }

        /// <summary>
        /// Gera as imagens em vídeo Frame por Frame
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="eventArgs">AForge.Video.NewFrameEventArgs eventArgs</param>
        private void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            if (pcbTv.Image != null)
            {
                pcbTv.Image.Dispose();
            }
            pcbTv.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        /// <summary>
        /// Liga ou desliga o dispositivo de captura de vídeo
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>   
        private void TurnOnOffCamera()
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                pcbTv.Image = null;
            }
            else
            {
                videoSource.Start();
            }
        }

        /// <summary>
        /// Desativa o dispositivo de captura antes de fechar o aplicativo
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">FormClosingEventArgs e</param>
        private void FormInicial_FormClosing(object sender, FormClosingEventArgs e)
        {
            TurnOnOffCamera();
        }

        private void pcbTv_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Monitora teclas pressionadas, se F2 pressionada abre as configurações do aplicativo
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">KeyEventArgs e</param>
        private void TelaInicial_KeyDown(object sender, KeyEventArgs e)
        {
            string key = Convert.ToString(e.KeyValue);

            if (key == "113") {
                FormConf formConf = new FormConf();
                formConf.Show();
            }
        }

        /// <summary>
        /// Conecta com um servidor WebSock, recebe mensagem do servidor para executar uma ação
        /// Faz reconexão automática em caso de queda na conexão
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        async private Task Conectar()
        {
            var url = new Uri("wss://echo.websocket.org");
            client = new WebsocketClient(url);
            client.ReconnectTimeout = TimeSpan.FromSeconds(30);

            // Thread Faz a conexão ou reconexão automática
            client.ReconnectionHappened.Subscribe(info =>
                Debug.WriteLine($"Reconexao!!, type: {info.Type}"));
            client.Start();

            // Thread recebe as mensagens do Servidor WebSocktet
            client.MessageReceived.Subscribe(msg => {
                Debug.WriteLine($"Mensagem recebido do Servidor: {msg}");
                string json = msg.ToString();
                string json2 = json.Replace('%', '"');
                var senhas = ConvertTextSenhasToJson(json2);

                senhaThreadExterna = senhas[0];
                JsonSenhas.Add(senhaThreadExterna);

                var json_serializado = JsonConvert.SerializeObject(JsonSenhas);
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\senhas.json", json_serializado);

                // ------------------------------------------------

                DateTime hoje = DateTime.Now;
                if (configDate.ToShortDateString() != hoje.ToShortDateString())
                {
                    config.DateSenhas = DateTime.Now.ToShortDateString();
                    SaveDateOfListSenhas(config);

                    JsonSenhas = new List<Senha>();
                    JsonSenhas.Add(senhas[0]);
                    json_serializado = JsonConvert.SerializeObject(JsonSenhas);
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\senhas.json", json_serializado);
                    Application.Restart();
                }

                Thread thread2 = new Thread(new ThreadStart(DefineRetornoAtualiza));
                thread2.Start();

                Thread thread3 = new Thread(AbreChamaSenha);
                thread3.Start();
            });
            // Valida se cliente está conectado ao WebSocktet
            if (client.IsStarted)
            {
                Console.WriteLine( "Conectado!");
            }

        }

        /// <summary>
        /// Executa de forma indireta por meio de outra Thread o método AtualizaSenhasPainel
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        private void DefineRetornoAtualiza()
        {
            AtualizaSenhasPainel(JsonSenhas);
        }

        /// <summary>
        /// Calcula indice de inicio do arquivo de senhas baseado nas últimas 4 senhas
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="qtdSenhas">int qtdSenhas</param>
        int SetIndiceSenhas(int qtdSenhas)
        {
            int indice = 0;

            if (qtdSenhas > 4) {
                indice = 4;
                return indice;
            }

            switch (qtdSenhas) {
                case 4:
                    indice = 4;
                    break;
                case 3:
                    indice = 3;
                    break;
                case 2:
                    indice = 2;
                    break;
                case 1:
                    indice = 1;
                    break;
            }
            return indice;
        }

        /// <summary>
        /// Invoca o thread do elemento Label para aplicar a string
        /// Usando quando precisa alterar a .text do Label de outra Thread
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="lb">Label lb</param>
        /// <param name="texto">string texto</param>
        public void SetText(Label lb, string texto) {
            if (lb.InvokeRequired)
            {
                lb.Invoke(new MethodInvoker(() => lb.Text = texto));
            }
            else {
                lb.Text = texto;
            }
        }

        /// <summary>
        /// Atualiza as senhas do painel conforme as ultimas 4 senhas armazenadas
        /// Adaptada para ser chamada de outra thread
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="List">senhas List</param>
        private void AtualizaSenhasPainel(List<Senha> senhas)
        {
            int qtdSenhas = senhas.Count;
            int i2;
            int indice = SetIndiceSenhas(qtdSenhas);
            int inicio = qtdSenhas - indice;

            if (qtdSenhas > 0)
            {
                for (int i = inicio; i < qtdSenhas; i++)
                {
                    if (i + 1 == qtdSenhas)
                    {
                        SetText(lbSenhaP, "SENHA");
                        SetText(lbSenhaNumP, senhas[i].SenhaNum.ToString());
                        SetText(lbLocalP, senhas[i].Local);
                        SetText(lbLocalNumP, senhas[i].LocalNum.ToString());
                        int qtdApaga = 3;

                        if (qtdSenhas > 1)
                        {
                            i2 = i - 1;
                            SetText(lbSenha1, "SENHA");
                            SetText(lbSenhaNum1, senhas[i2].SenhaNum.ToString());
                            SetText(lbLocal1, senhas[i2].Local);
                            SetText(lbLocalNum1, senhas[i2].LocalNum.ToString());
                            qtdApaga = 2;
                        }

                        if (qtdSenhas > 2)
                        {
                            i2 = i - 2;
                            SetText(lbSenha2, "SENHA");
                            SetText(lbSenhaNum2, senhas[i2].SenhaNum.ToString());
                            SetText(lbLocal2, senhas[i2].Local);
                            SetText(lbLocalNum2, senhas[i2].LocalNum.ToString());
                            qtdApaga = 1;
                        }

                        if (qtdSenhas > 3)
                        {
                            i2 = i - 3;
                            SetText(lbSenha3, "SENHA");
                            SetText(lbSenhaNum3, senhas[i2].SenhaNum.ToString());
                            SetText(lbLocal3, senhas[i2].Local);
                            SetText(lbLocalNum3, senhas[i2].LocalNum.ToString());
                            qtdApaga = 0;
                        }
                        if (qtdApaga > 0) {
                            ApagaLabelsThread(qtdApaga);
                        }
                    }
                }
            }
            else {
                ApagaLabelsThread(4);
            }
        }

        /// <summary>
        /// Abre a Tela para Chamar a senha depois de receber requisição do servidor em forma de mensagem
        /// A Mensagem deve vir como uma string representando um json de senha
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        private void AbreChamaSenha() {
            FormChamaSenha formSenha = new FormChamaSenha();
            formSenha.SenhaChamada = senhaThreadExterna;
            formSenha.Show();
            Application.Run();
        }

        /// <summary>
        /// "Para Teste" - envia mensagem para Servidor WebSock, recebe a mesma mensagem enviada
        /// Monta um array de senha dos campos de teste e converte para string antes de enviar
        /// Adaptada para ser chamada de outra thread
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            Senha SenhaLocal = new Senha();
            int ChamadaSenha;
            int.TryParse(TxtSenha.Text, out ChamadaSenha);
            SenhaLocal.SenhaNum = ChamadaSenha;
            SenhaLocal.Local    = TxtLocal.Text;
            int ChamadaLocalNum;
            int.TryParse(TxtLocalNum.Text, out ChamadaLocalNum);
            SenhaLocal.SenhaNum = ChamadaSenha;
            SenhaLocal.LocalNum = ChamadaLocalNum;
            // Array Usado somente para enviar a senha para o WebSocket Server
            arraySenhas.Add(SenhaLocal);

            var senha_enviar = JsonConvert.SerializeObject(arraySenhas);
            if (SenhaLocal.SenhaNum > 0) {
                SendMessage(senha_enviar);

                TxtSenha.Text = null;
                TxtLocal.Text = null;
                TxtLocalNum.Text = null;
                arraySenhas = new List<Senha>();
            }
        }

        /// <summary>
        /// "Para Teste" - Valida se há conexão com o Servidor WebSocket antes de enviar mensagem
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="message">string message</param>
        private async Task SendMessage(string message)
        {
            if (client != null)
            {
                if (client.IsStarted)
                {
                    string strAlterada = message.Replace('"', '%');
                    Task.Run(() => client.Send(strAlterada));
                    Console.WriteLine("Client : " + message + "\r\n");
                    message = null;

                }
                else
                {
                    MessageBox.Show($"Conecte ao Servidor!");
                    Debug.WriteLine($"Conecte ao Servidor!");
                }
            }
            else
            {
                MessageBox.Show($"Conecte ao Servidor!");
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void TelaInicial_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
