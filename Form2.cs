using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO;
using PainelDeSenhas.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace PainelDeSenhas
{
    /**
     * Tela de Configuração
     * author Silvio Watakabe <silvio@tcmed.com.br>
     * @since 18-12-2020
     * @version 1.0
     */
    public partial class FormConf : Form
    {
        public SpeechSynthesizer Speaker = new SpeechSynthesizer();
        public List<Config> config;
        public FormConf()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Carrega 
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 18-12-2020
        /// @version 1.0
        /// </summary>
        private void Conf_Load(object sender, EventArgs e)
        {
            CarregarVozes(CbVozes);
            LerJson();
        }

        /// <summary>
        /// Carrega um comboBox com as vozes sintetizadas instaladas no sistema
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 21-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="cmd">ComboBox cmd</param>
        private void CarregarVozes(ComboBox cmd)
        {
            foreach (InstalledVoice voice in Speaker.GetInstalledVoices())
            {
                VoiceInfo info = voice.VoiceInfo;
                String voiceName = info.Name;
                cmd.Items.Add(voiceName);
            }
        }

        /// <summary>
        /// Le arquivo .json e converte para array com parâmetros de configuração
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 21-12-2020
        /// @version 1.0
        /// </summary>
        private void LerJson()
        {
            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\config.json");

            var js = new DataContractJsonSerializer(typeof(List<Config>));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            config = (List<Config>)js.ReadObject(ms);
            TxtSom.Text = config[0].ArquivoSom;
            NumVol.Value = config[0].Volume;
            CbVozes.SelectedItem = config[0].Voz;
            TxtWebSocketUrl.Text = config[0].WebSocketURL;
        }

        /// <summary>
        /// Salva as configurações e reinicia o aplicativo
        /// @author Silvio Watakabe silvio@tcmed.com.br
        /// @since 21-12-2020
        /// @version 1.0
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            Config newConfig = new Config();
            newConfig.ArquivoSom = TxtSom.Text;
            var vol = Convert.ToInt32(NumVol.Value);
            newConfig.Volume = vol;
            newConfig.WebSocketURL = TxtWebSocketUrl.Text;
            newConfig.Voz = CbVozes.Text;

            config[0] = newConfig;

            var json_serializado = JsonConvert.SerializeObject(config);

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\config.json", json_serializado);
            Application.Restart();
        }

    }
}
