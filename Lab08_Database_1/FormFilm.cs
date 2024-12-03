using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab08_Database_1
{
    public partial class FormFilm : Form
    {
        private Film _film;
        public Film Film
        {
            get => _film;
        }
        //insert
        public FormFilm(Category[] categories, Film filmToUpdate = null)
        {
            InitializeComponent();

            listBoxCategory.Items.AddRange(categories);
            //update
            if (filmToUpdate != null)
            {
                _film = filmToUpdate;
                listBoxCategory.SelectedItem = _film.Category;
                textBoxTitle.Text = _film.Title;
                numericUpDownDuration.Value = _film.Duration;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (listBoxCategory.SelectedItem is Category &&
               textBoxTitle.Text.Length > 3 &&
               numericUpDownDuration.Value > 0)
            {
                //insert
                if (_film == null)
                {
                    _film = new Film();
                }

                _film.Category = listBoxCategory.SelectedItem as Category;
                _film.Title = textBoxTitle.Text;
                _film.Duration = (int)numericUpDownDuration.Value;

                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Correct data!");
            }
        }
    }
}
