using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using Microsoft.AnalysisServices.AdomdClient;


namespace OlapSystem.Migration
{
    public partial class ReportValidationForm : Form
    {
        ReportValidator _validator;

        public ReportValidationForm(ReportValidator validator)
        {
            if (validator == null)
                throw new ArgumentNullException();
            _validator = validator;

            InitializeComponent();
            InitControls();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _validator.LoadReports();
                this.dataGridView1.DataSource = _validator.ReportsTable;
            }
            catch (Exception exc)
            {
                this.dataGridView1.DataSource = null;
                MessageBox.Show(exc.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Sure?", "", MessageBoxButtons.YesNo);
            if (res == DialogResult.No)
                return;

            Convert();
        }


        private void InitControls()
        {
            this.dataGridView1.AutoGenerateColumns = false;

            // datagrid columns
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            col.HeaderText = "Id";
            col.DataPropertyName = "Id";
            col.Width = 80;
            this.dataGridView1.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            col.HeaderText = "User";
            col.DataPropertyName = "User";
            col.Width = 150;
            this.dataGridView1.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            col.HeaderText = "Name";
            col.DataPropertyName = "Name";
            col.Width = 150;
            this.dataGridView1.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            col.HeaderText = "Description";
            col.DataPropertyName = "Description";
            col.Width = 250;
            this.dataGridView1.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            col.HeaderText = "ObjectsTotal";
            col.DataPropertyName = "ObjectsTotal";
            col.Width = 100;
            this.dataGridView1.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            col.HeaderText = "ObjectsInvalid";
            col.DataPropertyName = "ObjectsInvalid";
            col.Width = 100;
            this.dataGridView1.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            col.HeaderText = "ObjectsInvalidNew";
            col.DataPropertyName = "ObjectsInvalidNew";
            col.Width = 100;
            this.dataGridView1.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.SortMode = DataGridViewColumnSortMode.Automatic;
            col.HeaderText = "InvalidDiff";
            col.DataPropertyName = "InvalidDiff";
            col.Width = 100;
            this.dataGridView1.Columns.Add(col);

            //col = new DataGridViewTextBoxColumn();
            //col.SortMode = DataGridViewColumnSortMode.NotSortable;
            //col.HeaderText = "ReportXml";
            //col.DataPropertyName = "ReportXml";
            //col.Visible = false;
            //this.dataGridView1.Columns.Add(col);

        }


        private void ShowException(Exception exc)
        {
            MessageBox.Show(exc.Message + "\r\n\r\n" + exc.StackTrace);
        }


        private void Convert()
        {
            if (_validator.ReportsTable == null)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                _validator.SaveReports();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (_validator.ReportsTable == null)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                _validator.ValidateReports();
            }
            catch(Exception exc)
            {
                ShowException(exc);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnShowUnmatched_Click(object sender, EventArgs e)
        {
            if (_validator.UnmatchedObjects == null)
                return;

            UnmatchedObjectsForm frm = new UnmatchedObjectsForm(_validator.UnmatchedObjects);
            frm.ShowDialog();
        }


    }
}