using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bombagame
{
    public partial class Form1 : Form
    {

        int addim = 0;
        int bombaSureti = 5;
        int adamSureti = 20;

        int adamYeri;
        int adamYuxarisi;

        Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            this.Size = new Size(450, 720);
            this.KeyPreview = true;

            timer1.Interval = 30;

            this.Load -= Form1_Load;
            this.Load += Form1_Load;

            timer1.Tick -= timer1_Tick;
            timer1.Tick += timer1_Tick;

            this.KeyDown -= Form1_KeyDown;
            this.KeyDown += Form1_KeyDown;

            this.KeyUp -= Form1_KeyUp;
            this.KeyUp += Form1_KeyUp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adamYeri = (this.ClientSize.Width - pictureBox3.Width) / 2;
            adamYuxarisi = this.ClientSize.Height - pictureBox3.Height;

            pictureBox3.Left = adamYeri;
            pictureBox4.Left = adamYeri;

            pictureBox3.Top = adamYuxarisi;
            pictureBox4.Top = adamYuxarisi;

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            BombalariYerlestir();

            timer1.Start();
        }

        private void BombalariYerlestir()
        {
            pictureBox1.Left = random.Next(
                0,
                this.ClientSize.Width - pictureBox1.Width
            );

            pictureBox2.Left = random.Next(
                0,
                this.ClientSize.Width - pictureBox2.Width
            );

            pictureBox1.Top = -pictureBox1.Height;

            pictureBox2.Top = -pictureBox2.Height - random.Next(150, 350);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Top += bombaSureti;
            pictureBox2.Top += bombaSureti;

            Rectangle adam = new Rectangle(
                adamYeri,
                adamYuxarisi,
                pictureBox3.Width,
                pictureBox3.Height
            );

            if (pictureBox1.Bounds.IntersectsWith(adam) ||
                pictureBox2.Bounds.IntersectsWith(adam))
            {
                OyunBitdi();
                return;
            }

            if (pictureBox1.Top >= this.ClientSize.Height)
            {
                pictureBox1.Top = -pictureBox1.Height;

                pictureBox1.Left = random.Next(
                    0,
                    this.ClientSize.Width - pictureBox1.Width
                );

                addim++;
            }

            if (pictureBox2.Top >= this.ClientSize.Height)
            {
                pictureBox2.Top = -pictureBox2.Height;

                pictureBox2.Left = random.Next(
                    0,
                    this.ClientSize.Width - pictureBox2.Width
                );

                addim++;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                pictureBox3.Visible = true;
                pictureBox4.Visible = false;

                adamYeri -= adamSureti;

                if (adamYeri < 0)
                {
                    adamYeri = 0;
                }

                pictureBox3.Left = adamYeri;
                pictureBox4.Left = adamYeri;
            }
            else if (e.KeyCode == Keys.Right)
            {
                pictureBox3.Visible = false;
                pictureBox4.Visible = true;

                adamYeri += adamSureti;

                if (adamYeri + pictureBox4.Width > this.ClientSize.Width)
                {
                    adamYeri = this.ClientSize.Width - pictureBox4.Width;
                }

                pictureBox3.Left = adamYeri;
                pictureBox4.Left = adamYeri;
            }
            else
            {
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
        }

        private void OyunBitdi()
        {
            timer1.Stop();

            MessageBox.Show(
                "Oyun bitdi! Sizin nəticə: " + addim,
                "Məlumat",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            DialogResult cavab = MessageBox.Show(
                "Yeni oyun başlasınmı?",
                "Məlumat",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (cavab == DialogResult.Yes)
            {
                YeniOyun();
            }
            else
            {
                this.Close();
            }
        }

        private void YeniOyun()
        {
            addim = 0;

            adamYeri = (this.ClientSize.Width - pictureBox3.Width) / 2;

            pictureBox3.Left = adamYeri;
            pictureBox4.Left = adamYeri;

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            BombalariYerlestir();

            timer1.Start();
        }
    }
}