// Ednei Soares e Emilha de Souza

using System;
using System.Drawing;
using System.Windows.Forms;

namespace MudarCorApp
{
    public partial class Form1 : Form
    {
        private bool redEnabled = true;
        private bool greenEnabled = true;
        private bool blueEnabled = true;
        private int blueValue = 0;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.MouseMove += new MouseEventHandler(this.OnMouseMove);
            this.MouseWheel += new MouseEventHandler(this.OnMouseWheel);
            this.KeyDown += new KeyEventHandler(this.OnKeyDown);
        }

        // Manipula o movimento do mouse (eixo X = vermelho, eixo Y = verde)
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            this.Invalidate();  // Atualiza o formulário para desenhar a nova cor
        }

        // Manipula o scroll do mouse (roda do mouse = azul)
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            blueValue = Math.Max(0, Math.Min(255, blueValue + e.Delta / 120)); // Limita o valor de 0 a 255
            this.Invalidate();
        }

        // Manipula a entrada do teclado (R, G e B)
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R) redEnabled = !redEnabled;
            if (e.KeyCode == Keys.G) greenEnabled = !greenEnabled;
            if (e.KeyCode == Keys.B) blueEnabled = !blueEnabled;

            this.Invalidate();
        }

        // Desenha o fundo com a cor modificada
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Obtém a posição do mouse para definir vermelho e verde
            int mouseX = Math.Max(0, Math.Min(255, this.PointToClient(Cursor.Position).X));
            int mouseY = Math.Max(0, Math.Min(255, this.PointToClient(Cursor.Position).Y));

            // Calcula as cores com base no estado dos canais (on/off)
            int red = redEnabled ? mouseX : 0;
            int green = greenEnabled ? mouseY : 0;
            int blue = blueEnabled ? blueValue : 0;

            // Define a cor do fundo
            Color bgColor = Color.FromArgb(red, green, blue);
            this.BackColor = bgColor;
        }
    }
}