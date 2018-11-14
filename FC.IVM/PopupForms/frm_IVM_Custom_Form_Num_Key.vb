Imports System.Windows.Forms
Imports FC.IVM.Bus.Modules
Imports FC.MainApp.Modules
Imports FC.MainWinApp
Imports FC.M.BLL_Util
Imports DevExpress.XtraLayout
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.Utils
Imports System.Drawing

Namespace PopupForms
    ''' <summary>
    '''   <para>Custom popup form สำหรับอำนวยความสะดวกในการป้อนข้อมูลประเภท ตัวเลข สำหรับใช้งานบน แทปเลต.</para>
    '''   <para></para>
    ''' </summary>
    ''' <example>
    ''' ใช้งานในโปรเซส ย้าย, ตัดจ่าย วัตถุดิบ
    ''' </example>
    Public Class frm_IVM_Custom_Form_Num_Key
        Inherits EditFormUserControl

        Dim textEdit As New TextEdit
        Dim textEdit2 As New TextEdit
        Dim lookUpEdit1 As New LookUpEdit
        Dim tmpQty As Double = 0

        Private list As List(Of IVMClass.Item)
        Private strings As String()
        Dim itemPackageSize As LayoutControlItem = Nothing

        ''' <param name="inColumns">ชื่อ คอลัมน์ สำหรับการ Insert data (Bind column)</param>
        ''' <param name="inDefaultFont">Font for use in this form.</param>
        Public Sub New(inColumns As List(Of IVMClass.Item), inNoShow() As String _
                               , inDefaultFont As Font, ByVal dataTable As DataTable _
                               , Optional inEditorHeight As Integer = 90)
            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            InitializeComponent()
            Me.BorderStyle = BorderStyle.None

            inDefaultFont = FontsCollection("mainArea")
            'lookUpEdit1.Properties.AppearanceDropDown.Font = New Font(lookUpEdit1.Properties.AppearanceDropDown.Font.FontFamily, 16)
            lookUpEdit1.Properties.DataSource = dataTable
            lookUpEdit1.Properties.ValueMember = "PackageName"
            lookUpEdit1.Properties.DisplayMember = "PackageFullName"
            lookUpEdit1.Properties.AppearanceDropDown.Font = inDefaultFont
            lookUpEdit1.Properties.PopupWidth = 300

            AddHandler lookUpEdit1.EditValueChanged, AddressOf lookUpEdit1_EditValueChanged
            '------------------------------------------------------------------
            Dim lc As New LayoutControl() With {.Dock = DockStyle.Fill}
            Me.Controls.Add(lc)
            Dim item1 As LayoutControlItem = Nothing
            Dim item2 As LayoutControlItem = Nothing
            Dim itemPackageSize As LayoutControlItem = Nothing
            Dim lineItem As LineLocation = LineLocation.Center
            ' convert values to lower case
            Dim LowerCaseNoShow As New List(Of String)
            For Each NoShowEntry As String In inNoShow
                LowerCaseNoShow.Add(NoShowEntry.ToLower)
            Next
            ' Lock the layout control to prevent excessive updates.
            lc.BeginUpdate()

            ' Bind the text editors to the MyRecord.FirstName and MyRecord.LastName properties.
            'textEdit.DataBindings.Add(New Binding("EditValue", Me.textEdit,
            '"Quantity", True))

            Try
                ' Hide the root group's border and caption.
                lc.Root.GroupBordersVisible = False

                Dim group1 As New LayoutControlGroup() With {.GroupBordersVisible = False}

                For Each AColumn As IVMClass.Item In inColumns
                    If Not LowerCaseNoShow.Contains(AColumn.ColumnName.ToLower) Then
                        If Not AColumn.AutoNumber Then
                            itemPackageSize = group1.AddItem()
                            group1.StartNewLine = True
                            item1 = group1.AddItem()

                            textEdit2.Font = inDefaultFont
                            textEdit2.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far

                            With item1
                                .Control = textEdit
                                .Text = String.Format("{0}:", AColumn.ColumnName)
                                .AppearanceItemCaption.Font = inDefaultFont
                            End With

                            Me.textEdit.Properties.Mask.UseMaskAsDisplayFormat = True
                            Me.textEdit.Font = inDefaultFont
                            Me.textEdit.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far
                            'Set Binding datasouce
                            Me.SetBoundFieldName(textEdit, AColumn.ColumnName)
                            'Set display format to 1 decimal
                            Me.textEdit.Properties.Mask.EditMask = "N"
                            Me.textEdit.Properties.DisplayFormat.FormatType = FormatType.Numeric
                            Me.textEdit.Properties.DisplayFormat.FormatString = "{0:n1}"

                            With itemPackageSize
                                .Control = lookUpEdit1
                                .Text = String.Format("{0}:", AColumn.PackageSize)
                                .AppearanceItemCaption.Font = inDefaultFont
                            End With
                            lookUpEdit1.Font = inDefaultFont
                            'Me.SetBoundFieldName(lookUpEdit1, AColumn.PackageSize)
                            '+++++++++++++++++++++++++++++++++++++++++++++++++++++
                            Me.lblNetamount.Text = AColumn.ColumnName2
                        End If
                    End If

                    If (AColumn.Memo = "UnloadImport" Or AColumn.Memo = "Unload") Then
                        lookUpEdit1.Visible = True
                        lookUpEdit1.EditValue = "A"
                    Else
                        lookUpEdit1.ReadOnly = True
                    End If
                Next

                lc.Root.Add(group1)

            Finally
                ' Unlock and update the layout control.
                lc.EndUpdate()
            End Try
        End Sub
        ''' <summary>Display button value.</summary>
        Private Sub press_num(sender As Object, e As EventArgs) Handles btn9.Click, btn8.Click, btn7.Click, btn6.Click, btn5.Click, btn4.Click, btn3.Click, btn2.Click, btn1.Click, btn0.Click, btnClear.Click, btnP.Click
            Try
                Dim btn As New DevExpress.XtraEditors.SimpleButton
                btn = CType(sender, DevExpress.XtraEditors.SimpleButton)

                If (TextEdit.Text = "0") Then
                    TextEdit.Text = String.Empty
                End If
                If (btn.Text = ".") Then
                    If (TextEdit.Text.Contains(".") = False) Then
                        TextEdit.Text &= btn.Text
                    End If
                ElseIf (btn.Text = "C") Then
                    TextEdit.Text = CDbl(0).ToString
                ElseIf (isEdit = True) Then
                    TextEdit.Text &= btn.Text
                    isEdit = True
                Else
                    TextEdit.Text = btn.Text
                    isEdit = True
                End If

            Catch ex As Exception
                MsgBox("press_num" & ex.Message)
            Finally
                'isEdit = False
            End Try

        End Sub
        'lookUpEdit1_EditValueChanged
        ''' <summary>Set material package size.</summary>
        Private Sub lookUpEdit1_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            mod_IVM_Utility.tmpUnloadPackSize = lookUpEdit1.EditValue.ToString

            Me.TextEdit.Focus()

        End Sub
        Private Sub textEdit_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            lookUpEdit1.Enabled = True
        End Sub

        Private Sub InitializeComponent()
            Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
            Me.lblNetamount = New DevExpress.XtraEditors.LabelControl()
            Me.btnP = New DevExpress.XtraEditors.SimpleButton()
            Me.btn9 = New DevExpress.XtraEditors.SimpleButton()
            Me.btn8 = New DevExpress.XtraEditors.SimpleButton()
            Me.btn7 = New DevExpress.XtraEditors.SimpleButton()
            Me.btn6 = New DevExpress.XtraEditors.SimpleButton()
            Me.btn5 = New DevExpress.XtraEditors.SimpleButton()
            Me.btn4 = New DevExpress.XtraEditors.SimpleButton()
            Me.btn3 = New DevExpress.XtraEditors.SimpleButton()
            Me.btn2 = New DevExpress.XtraEditors.SimpleButton()
            Me.btn1 = New DevExpress.XtraEditors.SimpleButton()
            Me.btn0 = New DevExpress.XtraEditors.SimpleButton()
            Me.SuspendLayout()
            '
            'btnClear
            '
            Me.btnClear.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnClear.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btnClear, "")
            Me.btnClear.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btnClear.Location = New System.Drawing.Point(280, 370)
            Me.btnClear.Name = "btnClear"
            Me.btnClear.Size = New System.Drawing.Size(120, 77)
            Me.btnClear.TabIndex = 39
            Me.btnClear.Text = "C"
            '
            'lblNetamount
            '
            Me.lblNetamount.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblNetamount.Appearance.Options.UseFont = True
            Me.lblNetamount.Appearance.Options.UseTextOptions = True
            Me.lblNetamount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Me.lblNetamount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
            Me.lblNetamount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.SetBoundPropertyName(Me.lblNetamount, "")
            Me.lblNetamount.Location = New System.Drawing.Point(25, 88)
            Me.lblNetamount.Name = "lblNetamount"
            Me.lblNetamount.Size = New System.Drawing.Size(83, 32)
            Me.lblNetamount.TabIndex = 38
            Me.lblNetamount.Text = "lblnetamount"
            Me.lblNetamount.Visible = False
            '
            'btnP
            '
            Me.btnP.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnP.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btnP, "")
            Me.btnP.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btnP.Location = New System.Drawing.Point(145, 370)
            Me.btnP.Name = "btnP"
            Me.btnP.Size = New System.Drawing.Size(130, 77)
            Me.btnP.TabIndex = 37
            Me.btnP.Text = "."
            '
            'btn9
            '
            Me.btn9.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btn9.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btn9, "")
            Me.btn9.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btn9.Location = New System.Drawing.Point(280, 121)
            Me.btn9.Name = "btn9"
            Me.btn9.Size = New System.Drawing.Size(120, 77)
            Me.btn9.TabIndex = 36
            Me.btn9.Text = "9"
            '
            'btn8
            '
            Me.btn8.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btn8.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btn8, "")
            Me.btn8.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btn8.Location = New System.Drawing.Point(145, 121)
            Me.btn8.Name = "btn8"
            Me.btn8.Size = New System.Drawing.Size(130, 77)
            Me.btn8.TabIndex = 35
            Me.btn8.Text = "8"
            '
            'btn7
            '
            Me.btn7.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btn7.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btn7, "")
            Me.btn7.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btn7.Location = New System.Drawing.Point(9, 121)
            Me.btn7.Name = "btn7"
            Me.btn7.Size = New System.Drawing.Size(130, 77)
            Me.btn7.TabIndex = 34
            Me.btn7.Text = "7"
            '
            'btn6
            '
            Me.btn6.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btn6.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btn6, "")
            Me.btn6.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btn6.Location = New System.Drawing.Point(280, 204)
            Me.btn6.Name = "btn6"
            Me.btn6.Size = New System.Drawing.Size(120, 77)
            Me.btn6.TabIndex = 33
            Me.btn6.Text = "6"
            '
            'btn5
            '
            Me.btn5.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btn5.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btn5, "")
            Me.btn5.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btn5.Location = New System.Drawing.Point(145, 204)
            Me.btn5.Name = "btn5"
            Me.btn5.Size = New System.Drawing.Size(130, 77)
            Me.btn5.TabIndex = 32
            Me.btn5.Text = "5"
            '
            'btn4
            '
            Me.btn4.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btn4.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btn4, "")
            Me.btn4.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btn4.Location = New System.Drawing.Point(9, 204)
            Me.btn4.Name = "btn4"
            Me.btn4.Size = New System.Drawing.Size(130, 77)
            Me.btn4.TabIndex = 31
            Me.btn4.Text = "4"
            '
            'btn3
            '
            Me.btn3.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btn3.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btn3, "")
            Me.btn3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btn3.Location = New System.Drawing.Point(280, 287)
            Me.btn3.Name = "btn3"
            Me.btn3.Size = New System.Drawing.Size(120, 77)
            Me.btn3.TabIndex = 30
            Me.btn3.Text = "3"
            '
            'btn2
            '
            Me.btn2.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btn2.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btn2, "")
            Me.btn2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btn2.Location = New System.Drawing.Point(145, 287)
            Me.btn2.Name = "btn2"
            Me.btn2.Size = New System.Drawing.Size(130, 77)
            Me.btn2.TabIndex = 29
            Me.btn2.Text = "2"
            '
            'btn1
            '
            Me.btn1.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btn1.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btn1, "")
            Me.btn1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btn1.Location = New System.Drawing.Point(9, 287)
            Me.btn1.Name = "btn1"
            Me.btn1.Size = New System.Drawing.Size(130, 77)
            Me.btn1.TabIndex = 28
            Me.btn1.Text = "1"
            '
            'btn0
            '
            Me.btn0.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btn0.Appearance.Options.UseFont = True
            Me.SetBoundPropertyName(Me.btn0, "")
            Me.btn0.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
            Me.btn0.Location = New System.Drawing.Point(9, 370)
            Me.btn0.Name = "btn0"
            Me.btn0.Size = New System.Drawing.Size(130, 77)
            Me.btn0.TabIndex = 27
            Me.btn0.Text = "0"
            '
            'frm_IVM_Custom_Form_Num_Key
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
            Me.Controls.Add(Me.btnClear)
            Me.Controls.Add(Me.lblNetamount)
            Me.Controls.Add(Me.btnP)
            Me.Controls.Add(Me.btn9)
            Me.Controls.Add(Me.btn8)
            Me.Controls.Add(Me.btn7)
            Me.Controls.Add(Me.btn6)
            Me.Controls.Add(Me.btn5)
            Me.Controls.Add(Me.btn4)
            Me.Controls.Add(Me.btn3)
            Me.Controls.Add(Me.btn2)
            Me.Controls.Add(Me.btn1)
            Me.Controls.Add(Me.btn0)
            Me.Name = "frm_IVM_Custom_Form_Num_Key"
            Me.Size = New System.Drawing.Size(453, 473)
            Me.ResumeLayout(False)

        End Sub


        Friend WithEvents btnClear As SimpleButton
        Friend WithEvents lblNetamount As LabelControl
        Friend WithEvents btnP As SimpleButton
        Friend WithEvents btn9 As SimpleButton
        Friend WithEvents btn8 As SimpleButton
        Friend WithEvents btn7 As SimpleButton
        Friend WithEvents btn6 As SimpleButton
        Friend WithEvents btn5 As SimpleButton
        Friend WithEvents btn4 As SimpleButton
        Friend WithEvents btn3 As SimpleButton
        Friend WithEvents btn2 As SimpleButton
        Friend WithEvents btn1 As SimpleButton
        Friend WithEvents btn0 As SimpleButton
    End Class
End Namespace
