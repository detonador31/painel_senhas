using System;
using System.Windows.Forms;
using System.Threading;
using System.Speech.Synthesis;
using System.Media;
using System.Runtime.Serialization.Json;
using PainelDeSenhas.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PainelDeSenhas
{
    public partial class FormChamaSenha : Form
    {
        public SoundPlayer _soundPlayer;
        public SpeechSynthesizer Speaker = new SpeechSynthesizer();
        public Senha SenhaChamada { get; set; }
        public List<Config> config;
        public Config conf;
        public System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer tmr2 = new System.Windows.Forms.Timer();

        /// <summary>
        /// Inicia os primeiros processos do Form
        /// Executa timer para fechar a Tela depois de 10 segundos
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>      
        public FormChamaSenha()
        {
            InitializeComponent();
            conf = Configuracao();
            _soundPlayer = new SoundPlayer(conf.ArquivoSom);

            tmr.Tick += delegate {
                _soundPlayer.Dispose();
                Speaker.Dispose();
                SenhaChamada = null;
                config = null;
                conf = null;
                tmr2.Dispose();
                Dispose();
                this.Close();
                tmr.Dispose();
            };
            tmr.Interval = (int)TimeSpan.FromSeconds(10).TotalMilliseconds;
            tmr.Start();
        }


        /// <summary>
        /// Executa depois do formChamaSenha Carrega os labels da senha
        /// Carrega a string senhaLocal para ser lido pela sintetizadora de voz
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="senhas">Lista de Senhas</param>  
        private void FormChamaSenha_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.FromPoint(MousePosition).Bounds;
            if (SenhaChamada.Local != null) {
                lbSenhaNum.Text = SenhaChamada.SenhaNum.ToString();
                lbLocal.Text    = SenhaChamada.Local;
                lbLocalNum.Text = SenhaChamada.LocalNum.ToString();
                string senhaLocal = "Senha . " + SenhaChamada.SenhaNum + " . " + SenhaChamada.Local + " . " + SenhaChamada.LocalNum;

                tmr2.Tick += delegate {
                    // executa o método vozChamada depois de 1 segundo de exibição
                    VozChamada(senhaLocal);
                };
                tmr2.Interval = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;
                tmr2.Start();
            }
        }


        /// <summary>
        /// Chama a voz sintetizada para ler a string senhaLocal
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="senhaLocal">string senhaLocal</param>
        private void VozChamada(string senhaLocal)
        {
            _soundPlayer.Play();
            
            Thread.Sleep(1800);
            Speaker.SelectVoice(conf.Voz);
            Speaker.Volume = 50;
            Speaker.SpeakAsync(senhaLocal);
            tmr2.Stop();
        }

        /// <summary>
        /// Lê o arquivo de configuração para carregar array
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        static Config Configuracao()
        {
            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\config.json");

            var js = new DataContractJsonSerializer(typeof(List<Config>));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            List<Config> conf = (List<Config>)js.ReadObject(ms);

            return conf[0];
        }

        private void FormChamaSenha_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
