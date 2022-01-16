using System;
using System.Drawing;
using System.Windows.Forms;
namespace Catamaran2
{
    public partial class Катамаран : Form
    {
        private Лодка _car;
        /// <summary>
        /// Конструктор
        /// </summary>
        public Катамаран()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод отрисовки машины
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new(pictureBox1.Width, pictureBox1.Height);
            Graphics gr = Graphics.FromImage(bmp);
            _car?.DrawTransport(gr);
            pictureBox1.Image = bmp;
        }
        /// <summary>
        /// Изменение размеров формы отрисовки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxCars_Resize(object sender, EventArgs e)
        {
            _car?.ChangeBorders(pictureBox1.Width, pictureBox1.Height);
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
            _car = new Лодка();
            _car.Init(rnd.Next(100, 300), rnd.Next(1000, 2000),
            Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)));
            _car.SetPosition(rnd.Next(10, 100), rnd.Next(10, 100),
            pictureBox1.Width, pictureBox1.Height);
            toolStripStatusLabel1.Text = "Скорость:" + _car.Speed;
            toolStripStatusLabel2.Text = "Вес: " + _car.Weight;
            toolStripStatusLabel3.Text = "Цвет: " + _car.BodyColor.Name;
            Draw();
        }





        private void ButtonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = (sender as Button).Name;
            switch (name)
            {
                case "buttonUp":
                    _car?.MoveTransport(Перечисление.Up);
                    break;
                case "buttonDown":
                    _car?.MoveTransport(Перечисление.Down);
                    break;
                case "buttonLeft":
                    _car?.MoveTransport(Перечисление.Left);
                    break;
                case "buttonRight":
                    _car?.MoveTransport(Перечисление.Right);
                    break;
            }
            Draw();
        }
    }
}
