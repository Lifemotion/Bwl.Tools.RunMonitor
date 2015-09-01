<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RunMonitorForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RunMonitorForm))
        Me._btnStartStopPocess = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ФайлToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.НастройкиToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ВыходToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.УстановитьСлужбуToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.trayicon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me._btnEdit = New System.Windows.Forms.Button()
        Me.timerHider = New System.Windows.Forms.Timer(Me.components)
        Me._btnAdd = New System.Windows.Forms.Button()
        Me._btnDelete = New System.Windows.Forms.Button()
        Me.timerUpdateList = New System.Windows.Forms.Timer(Me.components)
        Me._dgvTasks = New System.Windows.Forms.DataGridView()
        Me.cName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTask = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cProcess = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me._btnStartStopTask = New System.Windows.Forms.Button()
        Me.LogWriterList1 = New Bwl.Framework.DatagridLogWriter()
        Me.MenuStrip1.SuspendLayout()
        CType(Me._dgvTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_btnStartStopPocess
        '
        Me._btnStartStopPocess.Location = New System.Drawing.Point(120, 167)
        Me._btnStartStopPocess.Name = "_btnStartStopPocess"
        Me._btnStartStopPocess.Size = New System.Drawing.Size(98, 39)
        Me._btnStartStopPocess.TabIndex = 2
        Me._btnStartStopPocess.Text = "Запустить процесс"
        Me._btnStartStopPocess.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ФайлToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(882, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "menu"
        '
        'ФайлToolStripMenuItem
        '
        Me.ФайлToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.НастройкиToolStripMenuItem, Me.ToolStripMenuItem1, Me.ВыходToolStripMenuItem, Me.УстановитьСлужбуToolStripMenuItem})
        Me.ФайлToolStripMenuItem.Name = "ФайлToolStripMenuItem"
        Me.ФайлToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ФайлToolStripMenuItem.Text = "Файл"
        '
        'НастройкиToolStripMenuItem
        '
        Me.НастройкиToolStripMenuItem.Name = "НастройкиToolStripMenuItem"
        Me.НастройкиToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.НастройкиToolStripMenuItem.Text = "Настройки..."
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(177, 6)
        '
        'ВыходToolStripMenuItem
        '
        Me.ВыходToolStripMenuItem.Name = "ВыходToolStripMenuItem"
        Me.ВыходToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ВыходToolStripMenuItem.Text = "Выход"
        '
        'УстановитьСлужбуToolStripMenuItem
        '
        Me.УстановитьСлужбуToolStripMenuItem.Name = "УстановитьСлужбуToolStripMenuItem"
        Me.УстановитьСлужбуToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.УстановитьСлужбуToolStripMenuItem.Text = "Установить службу"
        Me.УстановитьСлужбуToolStripMenuItem.Visible = False
        '
        'trayicon
        '
        Me.trayicon.Icon = CType(resources.GetObject("trayicon.Icon"), System.Drawing.Icon)
        Me.trayicon.Text = "RunMonitor"
        Me.trayicon.Visible = True
        '
        '_btnEdit
        '
        Me._btnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me._btnEdit.Location = New System.Drawing.Point(3, 257)
        Me._btnEdit.Name = "_btnEdit"
        Me._btnEdit.Size = New System.Drawing.Size(98, 39)
        Me._btnEdit.TabIndex = 4
        Me._btnEdit.Text = "Редактировать задачу"
        Me._btnEdit.UseVisualStyleBackColor = True
        '
        'timerHider
        '
        Me.timerHider.Interval = 500
        '
        '_btnAdd
        '
        Me._btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me._btnAdd.ForeColor = System.Drawing.Color.Black
        Me._btnAdd.Location = New System.Drawing.Point(3, 167)
        Me._btnAdd.Name = "_btnAdd"
        Me._btnAdd.Size = New System.Drawing.Size(96, 39)
        Me._btnAdd.TabIndex = 5
        Me._btnAdd.Text = "Добавить задачу"
        Me._btnAdd.UseVisualStyleBackColor = True
        '
        '_btnDelete
        '
        Me._btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me._btnDelete.ForeColor = System.Drawing.Color.Black
        Me._btnDelete.Location = New System.Drawing.Point(3, 212)
        Me._btnDelete.Name = "_btnDelete"
        Me._btnDelete.Size = New System.Drawing.Size(98, 39)
        Me._btnDelete.TabIndex = 6
        Me._btnDelete.Text = "Удалить задачу"
        Me._btnDelete.UseVisualStyleBackColor = True
        '
        'timerUpdateList
        '
        Me.timerUpdateList.Enabled = True
        Me.timerUpdateList.Interval = 1000
        '
        '_dgvTasks
        '
        Me._dgvTasks.AllowUserToAddRows = False
        Me._dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me._dgvTasks.ColumnHeadersHeight = 20
        Me._dgvTasks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cName, Me.cTask, Me.cProcess})
        Me._dgvTasks.Location = New System.Drawing.Point(3, 24)
        Me._dgvTasks.MultiSelect = False
        Me._dgvTasks.Name = "_dgvTasks"
        Me._dgvTasks.ReadOnly = True
        Me._dgvTasks.RowHeadersVisible = False
        Me._dgvTasks.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader
        Me._dgvTasks.RowTemplate.Height = 21
        Me._dgvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me._dgvTasks.Size = New System.Drawing.Size(215, 137)
        Me._dgvTasks.TabIndex = 9
        '
        'cName
        '
        Me.cName.HeaderText = "Имя"
        Me.cName.Name = "cName"
        Me.cName.ReadOnly = True
        '
        'cTask
        '
        Me.cTask.HeaderText = "Задача"
        Me.cTask.Name = "cTask"
        Me.cTask.ReadOnly = True
        Me.cTask.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cTask.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'cProcess
        '
        Me.cProcess.HeaderText = "Процесс"
        Me.cProcess.Name = "cProcess"
        Me.cProcess.ReadOnly = True
        Me.cProcess.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cProcess.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        '_btnStartStopTask
        '
        Me._btnStartStopTask.Location = New System.Drawing.Point(3, 304)
        Me._btnStartStopTask.Name = "_btnStartStopTask"
        Me._btnStartStopTask.Size = New System.Drawing.Size(98, 39)
        Me._btnStartStopTask.TabIndex = 10
        Me._btnStartStopTask.Text = "Запустить задачу"
        Me._btnStartStopTask.UseVisualStyleBackColor = True
        '
        'LogWriterList1
        '
        Me.LogWriterList1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogWriterList1.FilterText = ""
        Me.LogWriterList1.Location = New System.Drawing.Point(230, 24)
        Me.LogWriterList1.LogEnabled = True
        Me.LogWriterList1.Margin = New System.Windows.Forms.Padding(0)
        Me.LogWriterList1.Name = "LogWriterList1"
        Me.LogWriterList1.ShowDebug = False
        Me.LogWriterList1.ShowErrors = True
        Me.LogWriterList1.ShowInformation = True
        Me.LogWriterList1.ShowMessages = True
        Me.LogWriterList1.ShowWarnings = True
        Me.LogWriterList1.Size = New System.Drawing.Size(652, 332)
        Me.LogWriterList1.TabIndex = 0
        '
        'RunMonitorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(882, 353)
        Me.Controls.Add(Me._btnStartStopTask)
        Me.Controls.Add(Me._btnAdd)
        Me.Controls.Add(Me._btnDelete)
        Me.Controls.Add(Me._btnEdit)
        Me.Controls.Add(Me._btnStartStopPocess)
        Me.Controls.Add(Me._dgvTasks)
        Me.Controls.Add(Me.LogWriterList1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "RunMonitorForm"
        Me.Text = "Bwl Watchdog"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me._dgvTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LogWriterList1 As bwl.Framework.DatagridLogWriter
	Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
	Friend WithEvents ФайлToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ВыходToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents trayicon As System.Windows.Forms.NotifyIcon
	Friend WithEvents НастройкиToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents timerHider As System.Windows.Forms.Timer
	Friend WithEvents УстановитьСлужбуToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents timerUpdateList As System.Windows.Forms.Timer
	Private WithEvents _btnStartStopPocess As System.Windows.Forms.Button
	Private WithEvents _btnEdit As System.Windows.Forms.Button
	Private WithEvents _btnAdd As System.Windows.Forms.Button
	Private WithEvents _btnDelete As System.Windows.Forms.Button
	Private WithEvents _dgvTasks As System.Windows.Forms.DataGridView
	Friend WithEvents _btnStartStopTask As System.Windows.Forms.Button
	Friend WithEvents cName As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents cTask As System.Windows.Forms.DataGridViewCheckBoxColumn
	Friend WithEvents cProcess As System.Windows.Forms.DataGridViewCheckBoxColumn
	Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator

End Class
