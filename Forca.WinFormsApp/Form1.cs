namespace Forca.WinFormsApp
{
    public partial class Form1 : Form
    {
        private int QuantidadeErros { get; set; } = 0;

        // Lista de todos os botoes do alfabeto
        List<Button> ListaBotoesAlfabeto { get; set; } = new List<Button>();

        // Escolhendo palavra sorteada
        List<char> PalavraSorteada { get; set; } = new List<char>();

        // Lista de todas as letras escolhidas pelo jogador
        List<char> ListaLetrasEscolhidas { get; set; } = new List<char>();

        public void configurarListaDeBotoesAlfabeto()
        {
            ListaBotoesAlfabeto.Add(buttonA);
            ListaBotoesAlfabeto.Add(buttonB);
            ListaBotoesAlfabeto.Add(buttonC);
            ListaBotoesAlfabeto.Add(buttonD);
            ListaBotoesAlfabeto.Add(buttonE);
            ListaBotoesAlfabeto.Add(buttonF);
            ListaBotoesAlfabeto.Add(buttonG);
            ListaBotoesAlfabeto.Add(buttonH);
            ListaBotoesAlfabeto.Add(buttonI);
            ListaBotoesAlfabeto.Add(buttonJ);
            ListaBotoesAlfabeto.Add(buttonK);
            ListaBotoesAlfabeto.Add(buttonL);
            ListaBotoesAlfabeto.Add(buttonM);
            ListaBotoesAlfabeto.Add(buttonN);
            ListaBotoesAlfabeto.Add(buttonO);
            ListaBotoesAlfabeto.Add(buttonP);
            ListaBotoesAlfabeto.Add(buttonQ);
            ListaBotoesAlfabeto.Add(buttonR);
            ListaBotoesAlfabeto.Add(buttonS);
            ListaBotoesAlfabeto.Add(buttonT);
            ListaBotoesAlfabeto.Add(buttonU);
            ListaBotoesAlfabeto.Add(buttonV);
            ListaBotoesAlfabeto.Add(buttonW);
            ListaBotoesAlfabeto.Add(buttonX);
            ListaBotoesAlfabeto.Add(buttonY);
            ListaBotoesAlfabeto.Add(buttonZ);
        }

        public static List<char> obterPalavraSorteada()
        {
            // Lista de palavras
            List<string> listaDePalavras = new List<string> {
                    "ABACATE", "CAJU",       "MARACUJA",
                    "ABACAXI", "CARAMBOLA",  "MURICI",
                    "ACEROLA", "CUPUACU",    "PEQUI",
                    "ACAI",    "GRAVIOLA",   "PITANGA",
                    "ARACA",   "GOIABA",     "PITAYA",
                    "ABACATE", "JABUTICABA", "SAPOTI",
                    "BACABA",  "JENIPAPO",   "TANGERINA",
                    "BACURI",  "MACA",       "UMBU",
                    "BANANA",  "MANGABA",    "UVA",
                    "CAJA",    "MANGA",      "UVAIA"
                };

            // Escolhendo palavra sorteada
            Random random = new Random();
            int numeroParaEscolherAPalavraSorteada = random.Next(listaDePalavras.Count());
            List<char> palavraSorteada = new List<char>();
            palavraSorteada.AddRange(listaDePalavras[numeroParaEscolherAPalavraSorteada]);

            return palavraSorteada;
        }


        public Form1()
        {
            InitializeComponent();
            configurarListaDeBotoesAlfabeto();
            ListaBotoesAlfabeto.ForEach(b => b.Enabled = false);
        }
        private void buttonNovaPalavra(object sender, EventArgs e)
        {
            resetDoTabuleiro();

            PalavraSorteada = obterPalavraSorteada();
            PalavraSorteada.ForEach(c => textBoxPalavra.Text += "_");
            //PalavraSorteada.ForEach(c => textBoxPalavra.Text += c); Descomentar para testar
        }


        private void buttonLetraClick(object sender, EventArgs e)
        {
            // disabilita o botao
            Button button = (Button)sender;
            button.Enabled = false;

            ListaLetrasEscolhidas.Add(Convert.ToChar(button.Text));

            // Decreser quantidade de chutes caso a palavra não possua a letra escolhida
            atualizarErros(button);


            // Cria a string estado do jogo verificando quais letras o jogador acertou
            List<char> palavraParaAtualizarODisplay = new List<char>();

            PalavraSorteada.ForEach(c =>
            {
                if (ListaLetrasEscolhidas.Contains(c))
                    palavraParaAtualizarODisplay.Add(c);
                else
                    palavraParaAtualizarODisplay.Add('_');
            });
            textBoxPalavra.Text = String.Concat(palavraParaAtualizarODisplay);

            // Confere se o jogador acertou a palavra
            if (palavraParaAtualizarODisplay.SequenceEqual(PalavraSorteada))
            {
                MessageBox.Show("Parabens voce acertou");
                ListaBotoesAlfabeto.ForEach(b => b.Enabled = false);
            }

            // Confere se o jogador gastou todas as tentativas e não conseguiu adivinhar a palavra
            if (QuantidadeErros == 7)
            {
                MessageBox.Show($"Que pena voce não acertou. A palavra era: {String.Concat(PalavraSorteada)}");
                ListaBotoesAlfabeto.ForEach(b => b.Enabled = false);
            }
        }

        private void resetDoTabuleiro()
        {
            ListaBotoesAlfabeto.ForEach(b => b.Enabled = true);
            pictureBoxForca.Image = Properties.Resources._0;
            listBoxPalavrasPossiveis.Items.Clear();
            textBoxPalavra.Clear();

            PalavraSorteada.Clear();
            ListaLetrasEscolhidas.Clear();
            QuantidadeErros = 0;
        }

        private void atualizarErros(Button button)
        {
            if (!PalavraSorteada.Contains(Convert.ToChar(button.Text)))
            {
                QuantidadeErros++;

                switch (QuantidadeErros)
                {
                    case 1:
                        pictureBoxForca.Image = Properties.Resources._1;
                        break;
                    case 2:
                        pictureBoxForca.Image = Properties.Resources._2;
                        break;
                    case 3:
                        pictureBoxForca.Image = Properties.Resources._3;
                        break;
                    case 4:
                        pictureBoxForca.Image = Properties.Resources._4;
                        break;
                    case 5:
                        pictureBoxForca.Image = Properties.Resources._5;
                        break;
                    case 6:
                        pictureBoxForca.Image = Properties.Resources._6;
                        break;
                    case 7:
                        pictureBoxForca.Image = Properties.Resources._7;
                        break;
                }
            }
        }
    }
}