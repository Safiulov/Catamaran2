using System;
using System.Drawing;
using System.Windows.Forms;
namespace Catamaran2
{
    public partial class FormParking : Form
    {
        /// <summary>
        /// Объект от класса-коллекции гаваней
        /// </summary>
        private readonly ParkingCollection _parkingCollection;
        public FormParking()
        {
            InitializeComponent();
            _parkingCollection = new
            ParkingCollection(pictureBox2.Width, pictureBox2.Height);
            
        }
        /// <summary>
        /// Заполнение listBoxLevels
        /// </summary>
        private void ReloadLevels()
        {
            int index = listBox1.SelectedIndex;
            listBox1.Items.Clear();
            for (int i = 0; i < _parkingCollection.Keys.Count; i++)
            {
                listBox1.Items.Add(_parkingCollection.Keys[i]);
            }
            if (listBox1.Items.Count > 0 && (index == -1 || index >=
            listBox1.Items.Count))
            {
                listBox1.SelectedIndex = 0;
            }
            else if (listBox1.Items.Count > 0 && index > -1 && index <
            listBox1.Items.Count)
            {
                listBox1.SelectedIndex = index;
            }
        }
        /// <summary>
        /// Метод отрисовки гавани
        /// </summary>
        private void Draw()
        {
            if (listBox1.SelectedIndex > -1)
            {//если выбран один из пуктов в listBox (при старте программы ни один пункт не будет выбран и может возникнуть ошибка, если мы попытаемся обратиться к элементу listBox)
Bitmap bmp = new Bitmap(pictureBox2.Width,
pictureBox2.Height);
                Graphics gr = Graphics.FromImage(bmp);
                _parkingCollection[listBox1.SelectedItem.ToString()].Draw(gr);
                pictureBox2.Image = bmp;
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Добавить гавань"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddParking_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Введите название гавани", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _parkingCollection.AddParking(textBox1.Text);
            ReloadLevels();
        }

/// <summary>
/// Обработка нажатия кнопки "Удалить гавань"
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void ButtonDelParking_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                if (MessageBox.Show($"Удалить парковку { listBox1.SelectedItem}?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
{                    _parkingCollection.DelParking(listBox1.SelectedItem.ToString());
                    ReloadLevels();
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать лодку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetboat_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    AddToParking(new Лодка(100, 1000, dialog.Color));
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать катамаран"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetcatamaran_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ColorDialog dialogDop = new ColorDialog();
                    if (dialogDop.ShowDialog() == DialogResult.OK)
                    {
                        AddToParking(new ЛодкаКатамаран(100, 1000,
                        dialog.Color, dialogDop.Color, true, true, true));
                    }
                }
            }
        }
/// <summary>
/// Обработка нажатия кнопки "Забрать"

/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void ButtonTakeboat_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1 && maskedTextBox1.Text !=
            "")
            {
                var car =
                _parkingCollection[listBox1.SelectedItem.ToString()] -
                Convert.ToInt32(maskedTextBox1.Text);
                if (car != null)
                {
                    Катамаран form = new Катамаран();
                    form.Setboat(car);
                    form.ShowDialog();
                }
                Draw();
            }
        }
        /// <summary>
        /// Метод обработки выбора элемента на listBoxLevels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxParkings_SelectedIndexChanged(object sender,
 EventArgs e) => Draw();
        /// <summary>
        /// Добавление объекта в класс-хранилище
        /// </summary>
        /// <param name="boat"></param>
        private void AddToParking(Лодка boat)
        {
            if (listBox1.SelectedIndex > -1)
            {
                if
                (_parkingCollection[listBox1.SelectedItem.ToString()] + boat)
                {
                    Draw();
                }
                else
                {
                    MessageBox.Show("Парковка переполнена");
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Добавить автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// Обработка нажатия кнопки "Добавить автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetBoat_Click(object sender, EventArgs e)
        {
            var formCarConfig = new Formboatconfig();
            formCarConfig.AddEvent(Addboat);
            formCarConfig.Show();
        }
        /// <summary>
        /// Метод добавления машины
        /// </summary>
        /// <param name="car"></param>
        private void Addboat(Iboat boat)
        {
            if (boat != null && listBox1.SelectedIndex > -1)
            {
                if
                ((_parkingCollection[listBox1.SelectedItem.ToString()]) + boat)
                {
                    Draw();
                }
                else
                {
                    MessageBox.Show("Лодку не удалось поставить");
                }
            }
        }




        /// <summary>
        /// Обработка нажатия пункта меню "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void СохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (_parkingCollection.SaveData(saveFileDialog1.FileName))
                {
                    MessageBox.Show("Сохранение прошло успешно",
                    "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не сохранилось", "Результат",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Обработка нажатия пункта меню "Загрузить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ЗагрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (_parkingCollection.LoadData(openFileDialog1.FileName))
                {
                    MessageBox.Show("Загрузили", "Результат",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadLevels();
                    Draw();
                }
                else
                {
                    MessageBox.Show("Не загрузили", "Результат",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }















    }
}




 
