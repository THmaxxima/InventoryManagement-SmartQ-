Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frm_IVM_Test
        Inherits DevExpress.XtraEditors.XtraForm

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
            Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
            Me.MemoEdit1 = New DevExpress.XtraEditors.MemoEdit()
            Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
            Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
            Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.SimpleButton3 = New DevExpress.XtraEditors.SimpleButton()
            Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
            CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.LayoutControl1.SuspendLayout()
            CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'LayoutControl1
            '
            Me.LayoutControl1.Controls.Add(Me.SimpleButton3)
            Me.LayoutControl1.Controls.Add(Me.SimpleButton2)
            Me.LayoutControl1.Controls.Add(Me.SimpleButton1)
            Me.LayoutControl1.Controls.Add(Me.MemoEdit1)
            Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
            Me.LayoutControl1.Name = "LayoutControl1"
            Me.LayoutControl1.Root = Me.LayoutControlGroup1
            Me.LayoutControl1.Size = New System.Drawing.Size(555, 393)
            Me.LayoutControl1.TabIndex = 0
            Me.LayoutControl1.Text = "LayoutControl1"
            '
            'LayoutControlGroup1
            '
            Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
            Me.LayoutControlGroup1.GroupBordersVisible = False
            Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4})
            Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
            Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
            Me.LayoutControlGroup1.OptionsItemText.TextToControlDistance = 5
            Me.LayoutControlGroup1.Size = New System.Drawing.Size(555, 393)
            Me.LayoutControlGroup1.TextVisible = False
            '
            'MemoEdit1
            '
            Me.MemoEdit1.Location = New System.Drawing.Point(18, 56)
            Me.MemoEdit1.Name = "MemoEdit1"
            Me.MemoEdit1.Size = New System.Drawing.Size(519, 319)
            Me.MemoEdit1.StyleController = Me.LayoutControl1
            Me.MemoEdit1.TabIndex = 4
            '
            'LayoutControlItem1
            '
            Me.LayoutControlItem1.Control = Me.MemoEdit1
            Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 38)
            Me.LayoutControlItem1.Name = "LayoutControlItem1"
            Me.LayoutControlItem1.Size = New System.Drawing.Size(525, 325)
            Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
            Me.LayoutControlItem1.TextVisible = False
            '
            'SimpleButton1
            '
            Me.SimpleButton1.Location = New System.Drawing.Point(18, 18)
            Me.SimpleButton1.Name = "SimpleButton1"
            Me.SimpleButton1.Size = New System.Drawing.Size(138, 32)
            Me.SimpleButton1.StyleController = Me.LayoutControl1
            Me.SimpleButton1.TabIndex = 5
            Me.SimpleButton1.Text = "User Properties"
            '
            'LayoutControlItem2
            '
            Me.LayoutControlItem2.Control = Me.SimpleButton1
            Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
            Me.LayoutControlItem2.Name = "LayoutControlItem2"
            Me.LayoutControlItem2.Size = New System.Drawing.Size(144, 38)
            Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
            Me.LayoutControlItem2.TextVisible = False
            '
            'SimpleButton2
            '
            Me.SimpleButton2.Location = New System.Drawing.Point(162, 18)
            Me.SimpleButton2.Name = "SimpleButton2"
            Me.SimpleButton2.Size = New System.Drawing.Size(154, 32)
            Me.SimpleButton2.StyleController = Me.LayoutControl1
            Me.SimpleButton2.TabIndex = 6
            Me.SimpleButton2.Text = "Test Log"
            '
            'LayoutControlItem3
            '
            Me.LayoutControlItem3.Control = Me.SimpleButton2
            Me.LayoutControlItem3.Location = New System.Drawing.Point(144, 0)
            Me.LayoutControlItem3.Name = "LayoutControlItem3"
            Me.LayoutControlItem3.Size = New System.Drawing.Size(160, 38)
            Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
            Me.LayoutControlItem3.TextVisible = False
            '
            'SimpleButton3
            '
            Me.SimpleButton3.Location = New System.Drawing.Point(322, 18)
            Me.SimpleButton3.Name = "SimpleButton3"
            Me.SimpleButton3.Size = New System.Drawing.Size(215, 32)
            Me.SimpleButton3.StyleController = Me.LayoutControl1
            Me.SimpleButton3.TabIndex = 7
            Me.SimpleButton3.Text = "Test SQL"
            '
            'LayoutControlItem4
            '
            Me.LayoutControlItem4.Control = Me.SimpleButton3
            Me.LayoutControlItem4.Location = New System.Drawing.Point(304, 0)
            Me.LayoutControlItem4.Name = "LayoutControlItem4"
            Me.LayoutControlItem4.Size = New System.Drawing.Size(221, 38)
            Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
            Me.LayoutControlItem4.TextVisible = False
            '
            'frm_IVM_Test
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(555, 393)
            Me.Controls.Add(Me.LayoutControl1)
            Me.Name = "frm_IVM_Test"
            Me.Text = "frm_IVM_Test"
            CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.LayoutControl1.ResumeLayout(False)
            CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
        Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
        Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents MemoEdit1 As DevExpress.XtraEditors.MemoEdit
        Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents SimpleButton3 As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    End Class
End Namespace

