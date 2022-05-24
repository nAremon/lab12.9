using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Pen pen = new Pen(Color.Black, 6);
        private Bitmap bmp;
        private Point Prpoint, point;
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.PNG)|*.bmp;*.jpg;*.gif;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(dialog.FileName);
                int w = image.Width;
                int h = image.Height;
                pictureBox1.Size = image.Size;
                bmp = new Bitmap(image, w, h);
                pictureBox1.Image = bmp;
                g = Graphics.FromImage(pictureBox1.Image);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sdialog = new SaveFileDialog();
            sdialog.Title = "Сохранить картинку как ...";
            sdialog.OverwritePrompt = true;
            sdialog.CheckPathExists = true;
            sdialog.Filter = "Bitmap File(*.bmp|*.bmp| GIF File(*.gif)|*.gif| JPEG File(*.jpg)|*.jpg| PNG File(*.png)|*.png";
            if (sdialog.ShowDialog() == DialogResult.OK)
            {
                string filename = sdialog.FileName;
                string filename2 = filename.Remove(0, filename.Length - 3);
                switch (filename2)
                {
                    case "bmp":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Prpoint.X = e.X;
            Prpoint.Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                point.X = e.X;
                point.Y = e.Y;
                g.DrawLine(pen, Prpoint, point);
                Prpoint.X = point.X;
                Prpoint.Y = point.Y;
                pictureBox1.Invalidate();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            button4.Location = new Point(Width - 163, Height - 84);
            button2.Location = new Point(Width - 300, Height - 84);
            button3.Location = new Point(Width - 493, Height - 84);
        }

        private void button3_Click(object sender, EventArgs e)
         {
            for(int i =1; i<bmp.Width; i+=2)
                for (int j = 0; j < bmp.Height; j ++)
                {
                    int Grey = (bmp.GetPixel(i,j).R + bmp.GetPixel(i, j).G + bmp.GetPixel(i, j).B) / 3;
                    Color color = Color.FromArgb(255, Grey, Grey, Grey);
                    bmp.SetPixel(i, j, color);
                }
            Refresh();
         }
    }
}
