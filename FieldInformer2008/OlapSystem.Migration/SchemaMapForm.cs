using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.AnalysisServices.AdomdClient;


namespace OlapSystem.Migration
{
    public partial class SchemaMapForm : Form
    {
        ReportValidator _validator = new ReportValidator();

        public SchemaMapForm()
        {
            InitializeComponent();
            InitControls();
        }

        protected override void OnClosed(EventArgs e)
        {
            CloseAdomdObjects();
            base.OnClosed(e);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadAdomdObjects();
            LoadMap();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveMap();
        }


        private void LoadAdomdObjects()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _validator.LoadAdomdObjects();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CloseAdomdObjects()
        {
            _validator.CloseAdomdObjects();
        }

        private void InitControls()
        {
            // map file path
            this.txtMapFilePath.Text = _validator.DefaultMapFilePath;
            this.openFileDialog1.FileName = _validator.DefaultMapFilePath;
            this.openFileDialog1.Multiselect = false;
            this.openFileDialog1.Filter = "XML | *.xml";
            this.openFileDialog1.CheckFileExists = false;

            // SourceUniqueName column
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            col1.HeaderText = "SourceUniqueName";
            col1.DataPropertyName = "SourceUniqueName";
            col1.Width = 500;
            this.dataGridView1.Columns.Add(col1);

            // DestUniqueName column
            DataGridViewComboBoxColumn col2 = new DataGridViewComboBoxColumn();
            col2.SortMode = DataGridViewColumnSortMode.NotSortable;
            col2.MaxDropDownItems = 20;
            col2.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;            
            col2.HeaderText = "DestUniqueName";
            col2.DataPropertyName = "DestUniqueName";
            col2.DataSource = _validator.DestSchemaTable.DefaultView;
            col2.ValueMember = "UniqueName";
            col2.Width = 500;            
            this.dataGridView1.Columns.Add(col2);

            // selection            
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
        }

        private void LoadMap()
        {            
            try            
            {
                this.Cursor = Cursors.WaitCursor;
                
                string filePath = this.txtMapFilePath.Text;
                if (!File.Exists(filePath))                
                    MessageBox.Show("Map file does not exist, new map will be created");
                _validator.LoadMap(filePath);

                // map change events, validation
                _validator.MapTable.RowChanged -= new DataRowChangeEventHandler(Map_RowChanged);
                _validator.MapTable.RowChanged += new DataRowChangeEventHandler(Map_RowChanged);
            }
            finally
            {
                if (_validator.MapTable != null)
                    this.dataGridView1.DataSource = _validator.MapTable;                
                this.Cursor = Cursors.Default;
            }
        }


        void Map_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            SourceSchemaMap.MapRow row=(SourceSchemaMap.MapRow)e.Row;
            if (e.Row.Table == null)
                return; // removed row
            if (row.IsDestUniqueNameNull() || row.DestUniqueName == "")
                return; //unmatched row, nothing to validate

            string srcObjectType = row.SourceUniqueName.Substring(0, row.SourceUniqueName.IndexOf(':', 0));
            string destObjectType = row.DestUniqueName.Substring(0, row.DestUniqueName.IndexOf(':', 0));
            if (srcObjectType != destObjectType)
            {
                MessageBox.Show(string.Format("{0} to {1} mapping error: ObjectType mismatch",
                    row.SourceUniqueName, row.DestUniqueName));
                row.DestUniqueName = "";
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.dataGridView1.Invalidate();

            DialogResult res = MessageBox.Show("Save changes?", "", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Cancel)
                e.Cancel = true;
            else if (res == DialogResult.Yes)
                SaveMap();

            base.OnClosing(e);
        }

        private void SaveMap()
        {
            if (_validator.MapTable == null)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // save file
                _validator.SaveMap(this.txtMapFilePath.Text);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = txtMapFilePath.Text;
            openFileDialog1.ShowDialog();
            
            txtMapFilePath.Text = openFileDialog1.FileName;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (_validator.MapTable == null)
                return;

            ReportValidationForm frm = new ReportValidationForm(_validator);
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();
        }

    }
}