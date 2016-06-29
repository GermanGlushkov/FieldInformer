﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace FI.UI.Web.Controls.Tabs {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class TabsData : DataSet {
        
        private TabsDataTable tableTabs;
        
        private TabImagesDataTable tableTabImages;
        
        public TabsData() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected TabsData(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["Tabs"] != null)) {
                    this.Tables.Add(new TabsDataTable(ds.Tables["Tabs"]));
                }
                if ((ds.Tables["TabImages"] != null)) {
                    this.Tables.Add(new TabImagesDataTable(ds.Tables["TabImages"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.InitClass();
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public TabsDataTable Tabs {
            get {
                return this.tableTabs;
            }
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public TabImagesDataTable TabImages {
            get {
                return this.tableTabImages;
            }
        }
        
        public override DataSet Clone() {
            TabsData cln = ((TabsData)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        protected override void ReadXmlSerializable(XmlReader reader) {
            this.Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if ((ds.Tables["Tabs"] != null)) {
                this.Tables.Add(new TabsDataTable(ds.Tables["Tabs"]));
            }
            if ((ds.Tables["TabImages"] != null)) {
                this.Tables.Add(new TabImagesDataTable(ds.Tables["TabImages"]));
            }
            this.DataSetName = ds.DataSetName;
            this.Prefix = ds.Prefix;
            this.Namespace = ds.Namespace;
            this.Locale = ds.Locale;
            this.CaseSensitive = ds.CaseSensitive;
            this.EnforceConstraints = ds.EnforceConstraints;
            this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
        
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
        }
        
        internal void InitVars() {
            this.tableTabs = ((TabsDataTable)(this.Tables["Tabs"]));
            if ((this.tableTabs != null)) {
                this.tableTabs.InitVars();
            }
            this.tableTabImages = ((TabImagesDataTable)(this.Tables["TabImages"]));
            if ((this.tableTabImages != null)) {
                this.tableTabImages.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "TabsData";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/TabsData.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableTabs = new TabsDataTable();
            this.Tables.Add(this.tableTabs);
            this.tableTabImages = new TabImagesDataTable();
            this.Tables.Add(this.tableTabImages);
        }
        
        private bool ShouldSerializeTabs() {
            return false;
        }
        
        private bool ShouldSerializeTabImages() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void TabsRowChangeEventHandler(object sender, TabsRowChangeEvent e);
        
        public delegate void TabImagesRowChangeEventHandler(object sender, TabImagesRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TabsDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnLevel;
            
            private DataColumn columnId;
            
            private DataColumn columnParentId;
            
            private DataColumn columnCaption;
            
            private DataColumn columnHref;
            
            private DataColumn columnIsActive;
            
            private DataColumn columnIsButton;
            
            internal TabsDataTable() : 
                    base("Tabs") {
                this.InitClass();
            }
            
            internal TabsDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn LevelColumn {
                get {
                    return this.columnLevel;
                }
            }
            
            internal DataColumn IdColumn {
                get {
                    return this.columnId;
                }
            }
            
            internal DataColumn ParentIdColumn {
                get {
                    return this.columnParentId;
                }
            }
            
            internal DataColumn CaptionColumn {
                get {
                    return this.columnCaption;
                }
            }
            
            internal DataColumn HrefColumn {
                get {
                    return this.columnHref;
                }
            }
            
            internal DataColumn IsActiveColumn {
                get {
                    return this.columnIsActive;
                }
            }
            
            internal DataColumn IsButtonColumn {
                get {
                    return this.columnIsButton;
                }
            }
            
            public TabsRow this[int index] {
                get {
                    return ((TabsRow)(this.Rows[index]));
                }
            }
            
            public event TabsRowChangeEventHandler TabsRowChanged;
            
            public event TabsRowChangeEventHandler TabsRowChanging;
            
            public event TabsRowChangeEventHandler TabsRowDeleted;
            
            public event TabsRowChangeEventHandler TabsRowDeleting;
            
            public void AddTabsRow(TabsRow row) {
                this.Rows.Add(row);
            }
            
            public TabsRow AddTabsRow(int Level, int ParentId, string Caption, string Href, bool IsActive, bool IsButton) {
                TabsRow rowTabsRow = ((TabsRow)(this.NewRow()));
                rowTabsRow.ItemArray = new object[] {
                        Level,
                        null,
                        ParentId,
                        Caption,
                        Href,
                        IsActive,
                        IsButton};
                this.Rows.Add(rowTabsRow);
                return rowTabsRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                TabsDataTable cln = ((TabsDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new TabsDataTable();
            }
            
            internal void InitVars() {
                this.columnLevel = this.Columns["Level"];
                this.columnId = this.Columns["Id"];
                this.columnParentId = this.Columns["ParentId"];
                this.columnCaption = this.Columns["Caption"];
                this.columnHref = this.Columns["Href"];
                this.columnIsActive = this.Columns["IsActive"];
                this.columnIsButton = this.Columns["IsButton"];
            }
            
            private void InitClass() {
                this.columnLevel = new DataColumn("Level", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnLevel);
                this.columnId = new DataColumn("Id", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnId);
                this.columnParentId = new DataColumn("ParentId", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnParentId);
                this.columnCaption = new DataColumn("Caption", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCaption);
                this.columnHref = new DataColumn("Href", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHref);
                this.columnIsActive = new DataColumn("IsActive", typeof(bool), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnIsActive);
                this.columnIsButton = new DataColumn("IsButton", typeof(bool), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnIsButton);
                this.columnId.AutoIncrement = true;
                this.columnId.AutoIncrementSeed = 1;
            }
            
            public TabsRow NewTabsRow() {
                return ((TabsRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new TabsRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(TabsRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.TabsRowChanged != null)) {
                    this.TabsRowChanged(this, new TabsRowChangeEvent(((TabsRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.TabsRowChanging != null)) {
                    this.TabsRowChanging(this, new TabsRowChangeEvent(((TabsRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.TabsRowDeleted != null)) {
                    this.TabsRowDeleted(this, new TabsRowChangeEvent(((TabsRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.TabsRowDeleting != null)) {
                    this.TabsRowDeleting(this, new TabsRowChangeEvent(((TabsRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveTabsRow(TabsRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TabsRow : DataRow {
            
            private TabsDataTable tableTabs;
            
            internal TabsRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableTabs = ((TabsDataTable)(this.Table));
            }
            
            public int Level {
                get {
                    try {
                        return ((int)(this[this.tableTabs.LevelColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTabs.LevelColumn] = value;
                }
            }
            
            public int Id {
                get {
                    try {
                        return ((int)(this[this.tableTabs.IdColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTabs.IdColumn] = value;
                }
            }
            
            public int ParentId {
                get {
                    try {
                        return ((int)(this[this.tableTabs.ParentIdColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTabs.ParentIdColumn] = value;
                }
            }
            
            public string Caption {
                get {
                    try {
                        return ((string)(this[this.tableTabs.CaptionColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTabs.CaptionColumn] = value;
                }
            }
            
            public string Href {
                get {
                    try {
                        return ((string)(this[this.tableTabs.HrefColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTabs.HrefColumn] = value;
                }
            }
            
            public bool IsActive {
                get {
                    try {
                        return ((bool)(this[this.tableTabs.IsActiveColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTabs.IsActiveColumn] = value;
                }
            }
            
            public bool IsButton {
                get {
                    try {
                        return ((bool)(this[this.tableTabs.IsButtonColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTabs.IsButtonColumn] = value;
                }
            }
            
            public bool IsLevelNull() {
                return this.IsNull(this.tableTabs.LevelColumn);
            }
            
            public void SetLevelNull() {
                this[this.tableTabs.LevelColumn] = System.Convert.DBNull;
            }
            
            public bool IsIdNull() {
                return this.IsNull(this.tableTabs.IdColumn);
            }
            
            public void SetIdNull() {
                this[this.tableTabs.IdColumn] = System.Convert.DBNull;
            }
            
            public bool IsParentIdNull() {
                return this.IsNull(this.tableTabs.ParentIdColumn);
            }
            
            public void SetParentIdNull() {
                this[this.tableTabs.ParentIdColumn] = System.Convert.DBNull;
            }
            
            public bool IsCaptionNull() {
                return this.IsNull(this.tableTabs.CaptionColumn);
            }
            
            public void SetCaptionNull() {
                this[this.tableTabs.CaptionColumn] = System.Convert.DBNull;
            }
            
            public bool IsHrefNull() {
                return this.IsNull(this.tableTabs.HrefColumn);
            }
            
            public void SetHrefNull() {
                this[this.tableTabs.HrefColumn] = System.Convert.DBNull;
            }
            
            public bool IsIsActiveNull() {
                return this.IsNull(this.tableTabs.IsActiveColumn);
            }
            
            public void SetIsActiveNull() {
                this[this.tableTabs.IsActiveColumn] = System.Convert.DBNull;
            }
            
            public bool IsIsButtonNull() {
                return this.IsNull(this.tableTabs.IsButtonColumn);
            }
            
            public void SetIsButtonNull() {
                this[this.tableTabs.IsButtonColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TabsRowChangeEvent : EventArgs {
            
            private TabsRow eventRow;
            
            private DataRowAction eventAction;
            
            public TabsRowChangeEvent(TabsRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public TabsRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TabImagesDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnTabId;
            
            private DataColumn columnImageUrl;
            
            internal TabImagesDataTable() : 
                    base("TabImages") {
                this.InitClass();
            }
            
            internal TabImagesDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn TabIdColumn {
                get {
                    return this.columnTabId;
                }
            }
            
            internal DataColumn ImageUrlColumn {
                get {
                    return this.columnImageUrl;
                }
            }
            
            public TabImagesRow this[int index] {
                get {
                    return ((TabImagesRow)(this.Rows[index]));
                }
            }
            
            public event TabImagesRowChangeEventHandler TabImagesRowChanged;
            
            public event TabImagesRowChangeEventHandler TabImagesRowChanging;
            
            public event TabImagesRowChangeEventHandler TabImagesRowDeleted;
            
            public event TabImagesRowChangeEventHandler TabImagesRowDeleting;
            
            public void AddTabImagesRow(TabImagesRow row) {
                this.Rows.Add(row);
            }
            
            public TabImagesRow AddTabImagesRow(int TabId, string ImageUrl) {
                TabImagesRow rowTabImagesRow = ((TabImagesRow)(this.NewRow()));
                rowTabImagesRow.ItemArray = new object[] {
                        TabId,
                        ImageUrl};
                this.Rows.Add(rowTabImagesRow);
                return rowTabImagesRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                TabImagesDataTable cln = ((TabImagesDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new TabImagesDataTable();
            }
            
            internal void InitVars() {
                this.columnTabId = this.Columns["TabId"];
                this.columnImageUrl = this.Columns["ImageUrl"];
            }
            
            private void InitClass() {
                this.columnTabId = new DataColumn("TabId", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnTabId);
                this.columnImageUrl = new DataColumn("ImageUrl", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnImageUrl);
            }
            
            public TabImagesRow NewTabImagesRow() {
                return ((TabImagesRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new TabImagesRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(TabImagesRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.TabImagesRowChanged != null)) {
                    this.TabImagesRowChanged(this, new TabImagesRowChangeEvent(((TabImagesRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.TabImagesRowChanging != null)) {
                    this.TabImagesRowChanging(this, new TabImagesRowChangeEvent(((TabImagesRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.TabImagesRowDeleted != null)) {
                    this.TabImagesRowDeleted(this, new TabImagesRowChangeEvent(((TabImagesRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.TabImagesRowDeleting != null)) {
                    this.TabImagesRowDeleting(this, new TabImagesRowChangeEvent(((TabImagesRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveTabImagesRow(TabImagesRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TabImagesRow : DataRow {
            
            private TabImagesDataTable tableTabImages;
            
            internal TabImagesRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableTabImages = ((TabImagesDataTable)(this.Table));
            }
            
            public int TabId {
                get {
                    try {
                        return ((int)(this[this.tableTabImages.TabIdColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTabImages.TabIdColumn] = value;
                }
            }
            
            public string ImageUrl {
                get {
                    try {
                        return ((string)(this[this.tableTabImages.ImageUrlColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTabImages.ImageUrlColumn] = value;
                }
            }
            
            public bool IsTabIdNull() {
                return this.IsNull(this.tableTabImages.TabIdColumn);
            }
            
            public void SetTabIdNull() {
                this[this.tableTabImages.TabIdColumn] = System.Convert.DBNull;
            }
            
            public bool IsImageUrlNull() {
                return this.IsNull(this.tableTabImages.ImageUrlColumn);
            }
            
            public void SetImageUrlNull() {
                this[this.tableTabImages.ImageUrlColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TabImagesRowChangeEvent : EventArgs {
            
            private TabImagesRow eventRow;
            
            private DataRowAction eventAction;
            
            public TabImagesRowChangeEvent(TabImagesRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public TabImagesRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}
