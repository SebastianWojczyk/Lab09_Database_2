using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
1. Visual Studio Installer
 - add Linq to SQL Tools
2. Server Explorer
 - Coneect to Database
 - Microsoft SQL Server Database File
 - create file
3. To project add LINQ to SQL Classes
 - Drag and drop tables from Server Explorer to dbml designer
 - cliec YES - copy file to project
 - for file MDF - set property "Copy to Output Directory" to "Copy if newer"

CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL
)
CREATE TABLE [dbo].[Film]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Duration] INT NOT NULL, 
    [CategoryId] INT NOT NULL, 
    CONSTRAINT [FK_Film_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id])
)

*/

namespace Lab08_Database_1
{
    public partial class FormMain : Form
    {
        DBDataContext dataContext = new DBDataContext();
        public FormMain()
        {
            InitializeComponent();

            readFilms();
        }

        private void readFilms()
        {
            List<Film> films = new List<Film>();

            if (radioButtonCategory.Checked)
            {
                films = dataContext.Films.OrderBy(f => f.Category.Name).ToList();
            }
            else if (radioButtonCategoryDESC.Checked)
            {
                films = dataContext.Films.OrderByDescending(f => f.Category.Name).ToList();
            }
            else if (radioButtonTitle.Checked)
            {
                films = dataContext.Films.OrderBy(f => f.Title).ToList();
            }
            else if (radioButtonTitleDESC.Checked)
            {
                films = dataContext.Films.OrderByDescending(f => f.Title).ToList();
            }
            else if (radioButtonDuration.Checked)
            {
                films = dataContext.Films.OrderBy(f => f.Duration).ToList();
            }
            else if (radioButtonDurationDESC.Checked)
            {
                films = dataContext.Films.OrderByDescending(f => f.Duration).ToList();
            }

            listBoxFilms.Items.Clear();
            foreach (Film f in films)
            {
                listBoxFilms.Items.Add(f);
            }
        }

        private void RadioButtonOrder_CheckedChanged(object sender, EventArgs e)
        {
            readFilms();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxFilms.SelectedItem is Film)
            {
                Film filmToDelete = listBoxFilms.SelectedItem as Film;
                dataContext.Films.DeleteOnSubmit(filmToDelete);
                dataContext.SubmitChanges();

                readFilms();
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            FormFilm formFilm = new FormFilm(dataContext.Categories.ToArray());
            if (formFilm.ShowDialog() == DialogResult.OK)
            {
                dataContext.Films.InsertOnSubmit(formFilm.Film);
                dataContext.SubmitChanges();

                readFilms();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (listBoxFilms.SelectedItem is Film)
            {
                Film filmToUpdate = listBoxFilms.SelectedItem as Film;

                FormFilm formFilm = new FormFilm(dataContext.Categories.ToArray(), filmToUpdate);
                if (formFilm.ShowDialog() == DialogResult.OK)
                {
                    dataContext.SubmitChanges();

                    readFilms();
                }
            }
        }
    }
}
