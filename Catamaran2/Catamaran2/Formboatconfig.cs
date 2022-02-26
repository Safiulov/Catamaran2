using System;
using System.Drawing;
using System.Windows.Forms;
namespace Catamaran2
{
    public partial class Formboatconfig : Form
    {
        /// <summary>
        /// Переменная-выбранная лодка
        /// </summary>
        Iboat _boat = null;
        /// <summary>
        /// Событие
        /// </summary>
        private event BoatDelegate EventAddboat;
        public Formboatconfig()
        {
            InitializeComponent();
            panelMaroon.MouseDown += panelColor_MouseDown;
            panelLime.MouseDown += panelColor_MouseDown;
            panelAqua.MouseDown += panelColor_MouseDown;
            panelOrange.MouseDown += panelColor_MouseDown;
            panelRed.MouseDown += panelColor_MouseDown;
            panelWhite.MouseDown += panelColor_MouseDown;
            panelYellow.MouseDown += panelColor_MouseDown;
            panelBlue.MouseDown += panelColor_MouseDown;            button2.Click += (object sender, EventArgs e) => {
                Close();
            };
        }
        /// <summary>
        /// Отрисовать лодку
        /// </summary>
        private void DrawCar()
        {
            if (_boat != null)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Width,
                pictureBox1.Height);
                Graphics gr = Graphics.FromImage(bmp);
                _boat.SetObject(85, 65, pictureBox1.Width,
                pictureBox1.Height);
                _boat.DrawObject(gr);
                pictureBox1.Image = bmp;
            }
        }

/// <summary>
/// Добавление события
/// </summary>
/// <param name="ev"></param>
public void AddEvent(BoatDelegate ev)
        {
            if (EventAddboat == null)
            {
                EventAddboat = new BoatDelegate(ev);
            }
            else
            {
                EventAddboat += ev;
            }
        }
        /// <summary>
        /// Передаем информацию при нажатии на Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelObject_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Label).DoDragDrop((sender as Label).Name,
            DragDropEffects.Move | DragDropEffects.Copy);
        }
        /// <summary>
        /// Проверка получаемой информации (ее типа на соответствие требуемому)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelCar_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /// <summary>
        /// Действия при приеме перетаскиваемой информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelCar_DragDrop(object sender, DragEventArgs e)
        {
            switch (e.Data.GetData(DataFormats.Text).ToString())
            {
                case "labelboat":
                    _boat = new Лодка((int)numericUpDown1.Value,
                    (int)numericUpDown2.Value, Color.Black);
                    break;
                case "labelcatamaran":
                    _boat = new
                    ЛодкаКатамаран((int)numericUpDown1.Value, (int)numericUpDown2.Value, Color.Yellow,
                    Color.Black,
                    
                    checkBox1.Checked,
                    checkBox2.Checked, checkBox3.Checked);
                    break;
            }
            DrawCar();
        }
        /// <summary>
        /// Отправляем цвет с панели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelColor_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Control).DoDragDrop((sender as Control).BackColor,
DragDropEffects.Move | DragDropEffects.Copy);
        }
        /// <summary>
        /// Проверка получаемой информации (ее типа на соответствие требуемому)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelBaseColor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Color)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /// <summary>
        /// Принимаем основной цвет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelBaseColor_DragDrop(object sender, DragEventArgs e)
        {
            if (_boat != null)
            {
                _boat.SetMainColor((Color)e.Data.GetData(typeof(Color)));
                DrawCar();
            }
        }
        /// <summary>
        /// Принимаем дополнительный цвет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelDopColor_DragDrop(object sender, DragEventArgs e)
        {
            if (_boat != null)
            {
                if (_boat is ЛодкаКатамаран)
                {
                    (_boat as
                   ЛодкаКатамаран).SetDopColor((Color)e.Data.GetData(typeof(Color)));
                    DrawCar();
                }
            }
        }
        /// <summary>
        /// Добавление лодки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            EventAddboat?.Invoke(_boat);
            Close();
        }

       
    }
}