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
    public partial class UnmatchedObjectsForm : Form
    {
        StringCollection _data = null;

        public UnmatchedObjectsForm(StringCollection data)
        {
            if (data == null)
                throw new ArgumentNullException();
            _data = data;

            InitializeComponent();
            InitControls();
        }


        private void InitControls()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("UniqueName", typeof(string));
            foreach(string s in _data)
                tbl.Rows.Add(new object[]{s});

            this.dataGridView1.DataSource = tbl;
            this.dataGridView1.Columns[0].Width = 500;
        }



    }
}