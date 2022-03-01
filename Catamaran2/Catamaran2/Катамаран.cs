using System;
using System.Drawing;
using System.Windows.Forms;
namespace Catamaran2
{
    public partial class Катамаран : Form
    {
        private Iboat _boat;
        /// <summary>
        /// Конструктор
        /// </summary>
        public Катамаран()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Передача машины на форму
        /// </summary>
        /// <param name="car"></param>
        public void Setboat(Iboat boat)
        {
            _boat = boat;
            Draw();
        }
        /// <summary>
        /// Метод отрисовки машины
        /// </summary>
        private void Draw()
        {
            
        Bitmap bmp = new Bitmap(pictureBox1.Width,
        pictureBox1.Height);
            Graphics gr = Graphics.FromImage(bmp);
            _boat?.DrawObject(gr);
            pictureBox1.Image = bmp;
        }
        /// <summary>
        /// Обработка нажатия кнопок управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = (sender as Button).Name;
            switch (name)
            {
                case "buttonUp":
                    _boat?.MoveObject(Перечисление.Up);
                    break;
                case "buttonDown":
                    _boat?.MoveObject(Перечисление.Down);
                    break;
                case "buttonLeft":
                    _boat?.MoveObject(Перечисление.Left);
                    break;
                case "buttonRight":
                    _boat?.MoveObject(Перечисление.Right);
                    break;
            }
            Draw();
        }
    }
}