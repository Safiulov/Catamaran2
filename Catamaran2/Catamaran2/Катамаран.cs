using System;
using System.Drawing;
using System.Windows.Forms;
namespace Catamaran2
{
    public partial class Катамаран : Form
    {
        private Лодка _boat;
        /// <summary>
        /// Конструктор
        /// </summary>
        public Катамаран()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод отрисовки лодки
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new(pictureBox1.Width, pictureBox1.Height);
            Graphics gr = Graphics.FromImage(bmp);
            _boat?.Drawboat(gr);
            pictureBox1.Image = bmp;
        }


        /// <summary>
        /// Изменение размеров формы отрисовки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxCars_Resize(object sender, EventArgs e)
        {
            _boat?.ChangeBorders(pictureBox1.Width, pictureBox1.Height);
            Draw();
        }

        /// <summary>
        /// Обработка нажатия кнопки "Создать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            _boat = new Лодка();
            _boat.Init(rnd.Next(100, 300), rnd.Next(1000, 2000),
            Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)));
            _boat.SetPosition(rnd.Next(10, 100), rnd.Next(10, 100),
            pictureBox1.Width, pictureBox1.Height);
            toolStripStatusLabel1.Text = "Скорость:" + _boat.Speed;
            toolStripStatusLabel2.Text = "Вес: " + _boat.Weight;
            toolStripStatusLabel3.Text = "Цвет: " + _boat.BodyColor.Name;
            Draw();
        }





        private void ButtonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = (sender as Button).Name;
            switch (name)
            {
                case "buttonUp":
                    _boat?.Moveboat(Перечисление.Up);
                    break;
                case "buttonDown":
                    _boat?.Moveboat(Перечисление.Down);
                    break;
                case "buttonLeft":
                    _boat?.Moveboat(Перечисление.Left);
                    break;
                case "buttonRight":
                    _boat?.Moveboat(Перечисление.Right);
                    break;
            }
            Draw();
        }
    }
}
