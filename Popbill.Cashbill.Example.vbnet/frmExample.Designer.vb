<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExample
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCorpNum = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.btnGetURL_WRITE = New System.Windows.Forms.Button
        Me.btnGetURL_PBOX = New System.Windows.Forms.Button
        Me.btnGetURL_TBOX = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnRevokeRegistIssue_part = New System.Windows.Forms.Button
        Me.btnCancelIssue02 = New System.Windows.Forms.Button
        Me.btnDelete02 = New System.Windows.Forms.Button
        Me.panel2 = New System.Windows.Forms.Panel
        Me.panel1 = New System.Windows.Forms.Panel
        Me.btnRevokRegistIssue = New System.Windows.Forms.Button
        Me.label11 = New System.Windows.Forms.Label
        Me.groupBox10 = New System.Windows.Forms.GroupBox
        Me.btnCancelIssueSub = New System.Windows.Forms.Button
        Me.btnRegistIssue = New System.Windows.Forms.Button
        Me.btnDeleteSub = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.btnGetPDFURL = New System.Windows.Forms.Button
        Me.btnGetEmailURL = New System.Windows.Forms.Button
        Me.btnGetMassPrintURL = New System.Windows.Forms.Button
        Me.btnEPrintURL = New System.Windows.Forms.Button
        Me.btnGetPrintURL = New System.Windows.Forms.Button
        Me.btnGetPopUpURL = New System.Windows.Forms.Button
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.btnAssignMgtKey = New System.Windows.Forms.Button
        Me.btnUpdateEmailConfig = New System.Windows.Forms.Button
        Me.btnListEmailConfig = New System.Windows.Forms.Button
        Me.btnSendFAX = New System.Windows.Forms.Button
        Me.btnSendSMS = New System.Windows.Forms.Button
        Me.btnSendEmail = New System.Windows.Forms.Button
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnGetInfos = New System.Windows.Forms.Button
        Me.btnGetLogs = New System.Windows.Forms.Button
        Me.btnGetInfo = New System.Windows.Forms.Button
        Me.btnGetDetailInfo = New System.Windows.Forms.Button
        Me.btnCheckMgtKeyInUse = New System.Windows.Forms.Button
        Me.txtMgtKey = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox17 = New System.Windows.Forms.GroupBox
        Me.btnUpdateCorpInfo = New System.Windows.Forms.Button
        Me.btnGetCorpInfo = New System.Windows.Forms.Button
        Me.GroupBox16 = New System.Windows.Forms.GroupBox
        Me.btnGetContactInfo = New System.Windows.Forms.Button
        Me.btnUpdateContact = New System.Windows.Forms.Button
        Me.btnListContact = New System.Windows.Forms.Button
        Me.btnRegistContact = New System.Windows.Forms.Button
        Me.GroupBox15 = New System.Windows.Forms.GroupBox
        Me.btnGetAccessURL = New System.Windows.Forms.Button
        Me.GroupBox14 = New System.Windows.Forms.GroupBox
        Me.btnGetPartnerURL = New System.Windows.Forms.Button
        Me.btnGetPartnerPoint = New System.Windows.Forms.Button
        Me.GroupBox18 = New System.Windows.Forms.GroupBox
        Me.btnGetUseHistoryURL = New System.Windows.Forms.Button
        Me.btnGetPaymentURL = New System.Windows.Forms.Button
        Me.btnGetChargeURL = New System.Windows.Forms.Button
        Me.btnGetBalance = New System.Windows.Forms.Button
        Me.GroupBox19 = New System.Windows.Forms.GroupBox
        Me.btnGetChargeInfo = New System.Windows.Forms.Button
        Me.btnGetUnitCost = New System.Windows.Forms.Button
        Me.GroupBox20 = New System.Windows.Forms.GroupBox
        Me.btnCheckID = New System.Windows.Forms.Button
        Me.btnCheckIsMember = New System.Windows.Forms.Button
        Me.btnJoinMember = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.groupBox10.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox14.SuspendLayout()
        Me.GroupBox18.SuspendLayout()
        Me.GroupBox19.SuspendLayout()
        Me.GroupBox20.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "팝빌회원 사업자번호 : "
        '
        'txtCorpNum
        '
        Me.txtCorpNum.Location = New System.Drawing.Point(148, 13)
        Me.txtCorpNum.Name = "txtCorpNum"
        Me.txtCorpNum.Size = New System.Drawing.Size(143, 21)
        Me.txtCorpNum.TabIndex = 2
        Me.txtCorpNum.Text = "1234567890"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(310, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "팝빌아이디 :"
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(389, 13)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(143, 21)
        Me.txtUserId.TabIndex = 4
        Me.txtUserId.Text = "testkorea"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Label4)
        Me.GroupBox7.Controls.Add(Me.GroupBox13)
        Me.GroupBox7.Controls.Add(Me.GroupBox1)
        Me.GroupBox7.Controls.Add(Me.groupBox10)
        Me.GroupBox7.Controls.Add(Me.GroupBox12)
        Me.GroupBox7.Controls.Add(Me.GroupBox11)
        Me.GroupBox7.Controls.Add(Me.GroupBox9)
        Me.GroupBox7.Controls.Add(Me.btnCheckMgtKeyInUse)
        Me.GroupBox7.Controls.Add(Me.txtMgtKey)
        Me.GroupBox7.Controls.Add(Me.Label3)
        Me.GroupBox7.Location = New System.Drawing.Point(14, 220)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(1028, 452)
        Me.GroupBox7.TabIndex = 6
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "현금영수증 관련 API"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.Location = New System.Drawing.Point(273, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(291, 28)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "국세청에 전송이 완료된 현금영수증을 취소하는 경우에는 '취소현금영수증'을 발행해야 합니다."
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.btnGetURL_WRITE)
        Me.GroupBox13.Controls.Add(Me.btnGetURL_PBOX)
        Me.GroupBox13.Controls.Add(Me.btnGetURL_TBOX)
        Me.GroupBox13.Location = New System.Drawing.Point(776, 243)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(115, 184)
        Me.GroupBox13.TabIndex = 12
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "기타 URL"
        '
        'btnGetURL_WRITE
        '
        Me.btnGetURL_WRITE.Location = New System.Drawing.Point(7, 84)
        Me.btnGetURL_WRITE.Name = "btnGetURL_WRITE"
        Me.btnGetURL_WRITE.Size = New System.Drawing.Size(102, 26)
        Me.btnGetURL_WRITE.TabIndex = 11
        Me.btnGetURL_WRITE.Text = "매출작성"
        Me.btnGetURL_WRITE.UseVisualStyleBackColor = True
        '
        'btnGetURL_PBOX
        '
        Me.btnGetURL_PBOX.Location = New System.Drawing.Point(6, 52)
        Me.btnGetURL_PBOX.Name = "btnGetURL_PBOX"
        Me.btnGetURL_PBOX.Size = New System.Drawing.Size(102, 26)
        Me.btnGetURL_PBOX.TabIndex = 9
        Me.btnGetURL_PBOX.Text = "발행보관함"
        Me.btnGetURL_PBOX.UseVisualStyleBackColor = True
        '
        'btnGetURL_TBOX
        '
        Me.btnGetURL_TBOX.Location = New System.Drawing.Point(6, 20)
        Me.btnGetURL_TBOX.Name = "btnGetURL_TBOX"
        Me.btnGetURL_TBOX.Size = New System.Drawing.Size(102, 26)
        Me.btnGetURL_TBOX.TabIndex = 8
        Me.btnGetURL_TBOX.Text = "임시문서함"
        Me.btnGetURL_TBOX.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnRevokeRegistIssue_part)
        Me.GroupBox1.Controls.Add(Me.btnCancelIssue02)
        Me.GroupBox1.Controls.Add(Me.btnDelete02)
        Me.GroupBox1.Controls.Add(Me.panel2)
        Me.GroupBox1.Controls.Add(Me.panel1)
        Me.GroupBox1.Controls.Add(Me.btnRevokRegistIssue)
        Me.GroupBox1.Controls.Add(Me.label11)
        Me.GroupBox1.Location = New System.Drawing.Point(270, 77)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(277, 140)
        Me.GroupBox1.TabIndex = 22
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "취소현금영수증 즉시발행 프로세스"
        '
        'btnRevokeRegistIssue_part
        '
        Me.btnRevokeRegistIssue_part.BackColor = System.Drawing.Color.LightCoral
        Me.btnRevokeRegistIssue_part.Location = New System.Drawing.Point(164, 24)
        Me.btnRevokeRegistIssue_part.Name = "btnRevokeRegistIssue_part"
        Me.btnRevokeRegistIssue_part.Size = New System.Drawing.Size(69, 28)
        Me.btnRevokeRegistIssue_part.TabIndex = 14
        Me.btnRevokeRegistIssue_part.Text = "부분취소"
        Me.btnRevokeRegistIssue_part.UseVisualStyleBackColor = False
        '
        'btnCancelIssue02
        '
        Me.btnCancelIssue02.BackColor = System.Drawing.Color.LightCoral
        Me.btnCancelIssue02.Location = New System.Drawing.Point(21, 105)
        Me.btnCancelIssue02.Name = "btnCancelIssue02"
        Me.btnCancelIssue02.Size = New System.Drawing.Size(65, 30)
        Me.btnCancelIssue02.TabIndex = 11
        Me.btnCancelIssue02.Text = "발행취소"
        Me.btnCancelIssue02.UseVisualStyleBackColor = False
        '
        'btnDelete02
        '
        Me.btnDelete02.Location = New System.Drawing.Point(130, 106)
        Me.btnDelete02.Name = "btnDelete02"
        Me.btnDelete02.Size = New System.Drawing.Size(56, 29)
        Me.btnDelete02.TabIndex = 10
        Me.btnDelete02.Text = "삭제"
        Me.btnDelete02.UseVisualStyleBackColor = True
        '
        'panel2
        '
        Me.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel2.Location = New System.Drawing.Point(81, 117)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(56, 1)
        Me.panel2.TabIndex = 13
        '
        'panel1
        '
        Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel1.Location = New System.Drawing.Point(52, 56)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(1, 59)
        Me.panel1.TabIndex = 13
        '
        'btnRevokRegistIssue
        '
        Me.btnRevokRegistIssue.BackColor = System.Drawing.Color.LightCoral
        Me.btnRevokRegistIssue.Location = New System.Drawing.Point(80, 24)
        Me.btnRevokRegistIssue.Name = "btnRevokRegistIssue"
        Me.btnRevokRegistIssue.Size = New System.Drawing.Size(69, 28)
        Me.btnRevokRegistIssue.TabIndex = 9
        Me.btnRevokRegistIssue.Text = "전체취소"
        Me.btnRevokRegistIssue.UseVisualStyleBackColor = False
        '
        'label11
        '
        Me.label11.BackColor = System.Drawing.Color.Silver
        Me.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.label11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.label11.Location = New System.Drawing.Point(9, 19)
        Me.label11.Name = "label11"
        Me.label11.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.label11.Size = New System.Drawing.Size(249, 37)
        Me.label11.TabIndex = 6
        Me.label11.Text = "즉시발행"
        Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'groupBox10
        '
        Me.groupBox10.Controls.Add(Me.btnCancelIssueSub)
        Me.groupBox10.Controls.Add(Me.btnRegistIssue)
        Me.groupBox10.Controls.Add(Me.btnDeleteSub)
        Me.groupBox10.Controls.Add(Me.Label8)
        Me.groupBox10.Controls.Add(Me.Label10)
        Me.groupBox10.Controls.Add(Me.Label12)
        Me.groupBox10.Location = New System.Drawing.Point(9, 77)
        Me.groupBox10.Name = "groupBox10"
        Me.groupBox10.Size = New System.Drawing.Size(229, 143)
        Me.groupBox10.TabIndex = 21
        Me.groupBox10.TabStop = False
        Me.groupBox10.Text = "현금영수증 즉시발행 프로세스(권장)"
        '
        'btnCancelIssueSub
        '
        Me.btnCancelIssueSub.BackColor = System.Drawing.Color.LightCoral
        Me.btnCancelIssueSub.Location = New System.Drawing.Point(22, 106)
        Me.btnCancelIssueSub.Name = "btnCancelIssueSub"
        Me.btnCancelIssueSub.Size = New System.Drawing.Size(65, 30)
        Me.btnCancelIssueSub.TabIndex = 8
        Me.btnCancelIssueSub.Text = "발행취소"
        Me.btnCancelIssueSub.UseVisualStyleBackColor = False
        '
        'btnRegistIssue
        '
        Me.btnRegistIssue.BackColor = System.Drawing.Color.LightCoral
        Me.btnRegistIssue.Location = New System.Drawing.Point(92, 22)
        Me.btnRegistIssue.Name = "btnRegistIssue"
        Me.btnRegistIssue.Size = New System.Drawing.Size(69, 28)
        Me.btnRegistIssue.TabIndex = 0
        Me.btnRegistIssue.Text = "즉시발행"
        Me.btnRegistIssue.UseVisualStyleBackColor = False
        '
        'btnDeleteSub
        '
        Me.btnDeleteSub.Location = New System.Drawing.Point(131, 107)
        Me.btnDeleteSub.Name = "btnDeleteSub"
        Me.btnDeleteSub.Size = New System.Drawing.Size(56, 29)
        Me.btnDeleteSub.TabIndex = 1
        Me.btnDeleteSub.Text = "삭제"
        Me.btnDeleteSub.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Silver
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.Location = New System.Drawing.Point(11, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Label8.Size = New System.Drawing.Size(194, 37)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "즉시발행"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.Location = New System.Drawing.Point(43, 122)
        Me.Label10.Name = "Label10"
        Me.Label10.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Label10.Size = New System.Drawing.Size(119, 1)
        Me.Label10.TabIndex = 14
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.Location = New System.Drawing.Point(53, 52)
        Me.Label12.Name = "Label12"
        Me.Label12.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Label12.Size = New System.Drawing.Size(1, 70)
        Me.Label12.TabIndex = 15
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.btnGetPDFURL)
        Me.GroupBox12.Controls.Add(Me.btnGetEmailURL)
        Me.GroupBox12.Controls.Add(Me.btnGetMassPrintURL)
        Me.GroupBox12.Controls.Add(Me.btnEPrintURL)
        Me.GroupBox12.Controls.Add(Me.btnGetPrintURL)
        Me.GroupBox12.Controls.Add(Me.btnGetPopUpURL)
        Me.GroupBox12.Location = New System.Drawing.Point(434, 243)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(328, 184)
        Me.GroupBox12.TabIndex = 11
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "문서관련 URL 기능"
        '
        'btnGetPDFURL
        '
        Me.btnGetPDFURL.Location = New System.Drawing.Point(192, 20)
        Me.btnGetPDFURL.Name = "btnGetPDFURL"
        Me.btnGetPDFURL.Size = New System.Drawing.Size(130, 26)
        Me.btnGetPDFURL.TabIndex = 14
        Me.btnGetPDFURL.Text = "PDF 다운로드 URL"
        Me.btnGetPDFURL.UseVisualStyleBackColor = True
        '
        'btnGetEmailURL
        '
        Me.btnGetEmailURL.Location = New System.Drawing.Point(6, 148)
        Me.btnGetEmailURL.Name = "btnGetEmailURL"
        Me.btnGetEmailURL.Size = New System.Drawing.Size(180, 26)
        Me.btnGetEmailURL.TabIndex = 13
        Me.btnGetEmailURL.Text = "이메일의 보기 버튼 URL"
        Me.btnGetEmailURL.UseVisualStyleBackColor = True
        '
        'btnGetMassPrintURL
        '
        Me.btnGetMassPrintURL.Location = New System.Drawing.Point(6, 116)
        Me.btnGetMassPrintURL.Name = "btnGetMassPrintURL"
        Me.btnGetMassPrintURL.Size = New System.Drawing.Size(180, 26)
        Me.btnGetMassPrintURL.TabIndex = 12
        Me.btnGetMassPrintURL.Text = "대량인쇄 팝업 URL"
        Me.btnGetMassPrintURL.UseVisualStyleBackColor = True
        '
        'btnEPrintURL
        '
        Me.btnEPrintURL.Location = New System.Drawing.Point(6, 84)
        Me.btnEPrintURL.Name = "btnEPrintURL"
        Me.btnEPrintURL.Size = New System.Drawing.Size(180, 26)
        Me.btnEPrintURL.TabIndex = 11
        Me.btnEPrintURL.Text = "공급받는자 인쇄 팝업 URL"
        Me.btnEPrintURL.UseVisualStyleBackColor = True
        '
        'btnGetPrintURL
        '
        Me.btnGetPrintURL.Location = New System.Drawing.Point(6, 52)
        Me.btnGetPrintURL.Name = "btnGetPrintURL"
        Me.btnGetPrintURL.Size = New System.Drawing.Size(180, 26)
        Me.btnGetPrintURL.TabIndex = 10
        Me.btnGetPrintURL.Text = "인쇄 팝업 URL"
        Me.btnGetPrintURL.UseVisualStyleBackColor = True
        '
        'btnGetPopUpURL
        '
        Me.btnGetPopUpURL.Location = New System.Drawing.Point(6, 20)
        Me.btnGetPopUpURL.Name = "btnGetPopUpURL"
        Me.btnGetPopUpURL.Size = New System.Drawing.Size(180, 26)
        Me.btnGetPopUpURL.TabIndex = 9
        Me.btnGetPopUpURL.Text = "문서 내용보기 팝업 URL"
        Me.btnGetPopUpURL.UseVisualStyleBackColor = True
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.btnAssignMgtKey)
        Me.GroupBox11.Controls.Add(Me.btnUpdateEmailConfig)
        Me.GroupBox11.Controls.Add(Me.btnListEmailConfig)
        Me.GroupBox11.Controls.Add(Me.btnSendFAX)
        Me.GroupBox11.Controls.Add(Me.btnSendSMS)
        Me.GroupBox11.Controls.Add(Me.btnSendEmail)
        Me.GroupBox11.Location = New System.Drawing.Point(132, 243)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(296, 184)
        Me.GroupBox11.TabIndex = 10
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "부가 기능"
        '
        'btnAssignMgtKey
        '
        Me.btnAssignMgtKey.Location = New System.Drawing.Point(169, 19)
        Me.btnAssignMgtKey.Name = "btnAssignMgtKey"
        Me.btnAssignMgtKey.Size = New System.Drawing.Size(121, 26)
        Me.btnAssignMgtKey.TabIndex = 11
        Me.btnAssignMgtKey.Text = "문서번호 할당"
        Me.btnAssignMgtKey.UseVisualStyleBackColor = True
        '
        'btnUpdateEmailConfig
        '
        Me.btnUpdateEmailConfig.Location = New System.Drawing.Point(7, 146)
        Me.btnUpdateEmailConfig.Name = "btnUpdateEmailConfig"
        Me.btnUpdateEmailConfig.Size = New System.Drawing.Size(155, 26)
        Me.btnUpdateEmailConfig.TabIndex = 8
        Me.btnUpdateEmailConfig.Text = "알림메일 전송설정 수정"
        Me.btnUpdateEmailConfig.UseVisualStyleBackColor = True
        '
        'btnListEmailConfig
        '
        Me.btnListEmailConfig.Location = New System.Drawing.Point(7, 114)
        Me.btnListEmailConfig.Name = "btnListEmailConfig"
        Me.btnListEmailConfig.Size = New System.Drawing.Size(155, 26)
        Me.btnListEmailConfig.TabIndex = 7
        Me.btnListEmailConfig.Text = "알림메일 전송목록 조회"
        Me.btnListEmailConfig.UseVisualStyleBackColor = True
        '
        'btnSendFAX
        '
        Me.btnSendFAX.Location = New System.Drawing.Point(7, 84)
        Me.btnSendFAX.Name = "btnSendFAX"
        Me.btnSendFAX.Size = New System.Drawing.Size(155, 26)
        Me.btnSendFAX.TabIndex = 10
        Me.btnSendFAX.Text = "팩스 전송"
        Me.btnSendFAX.UseVisualStyleBackColor = True
        '
        'btnSendSMS
        '
        Me.btnSendSMS.Location = New System.Drawing.Point(7, 52)
        Me.btnSendSMS.Name = "btnSendSMS"
        Me.btnSendSMS.Size = New System.Drawing.Size(155, 26)
        Me.btnSendSMS.TabIndex = 9
        Me.btnSendSMS.Text = "문자 전송"
        Me.btnSendSMS.UseVisualStyleBackColor = True
        '
        'btnSendEmail
        '
        Me.btnSendEmail.Location = New System.Drawing.Point(7, 20)
        Me.btnSendEmail.Name = "btnSendEmail"
        Me.btnSendEmail.Size = New System.Drawing.Size(155, 26)
        Me.btnSendEmail.TabIndex = 8
        Me.btnSendEmail.Text = "이메일 전송"
        Me.btnSendEmail.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.btnSearch)
        Me.GroupBox9.Controls.Add(Me.btnGetInfos)
        Me.GroupBox9.Controls.Add(Me.btnGetLogs)
        Me.GroupBox9.Controls.Add(Me.btnGetInfo)
        Me.GroupBox9.Controls.Add(Me.btnGetDetailInfo)
        Me.GroupBox9.Location = New System.Drawing.Point(10, 243)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(116, 184)
        Me.GroupBox9.TabIndex = 8
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "문서 정보"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(6, 147)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(104, 26)
        Me.btnSearch.TabIndex = 10
        Me.btnSearch.Text = "문서목록조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnGetInfos
        '
        Me.btnGetInfos.Location = New System.Drawing.Point(6, 116)
        Me.btnGetInfos.Name = "btnGetInfos"
        Me.btnGetInfos.Size = New System.Drawing.Size(104, 26)
        Me.btnGetInfos.TabIndex = 9
        Me.btnGetInfos.Text = "문서정보(대량)"
        Me.btnGetInfos.UseVisualStyleBackColor = True
        '
        'btnGetLogs
        '
        Me.btnGetLogs.Location = New System.Drawing.Point(6, 84)
        Me.btnGetLogs.Name = "btnGetLogs"
        Me.btnGetLogs.Size = New System.Drawing.Size(104, 26)
        Me.btnGetLogs.TabIndex = 8
        Me.btnGetLogs.Text = "문서이력"
        Me.btnGetLogs.UseVisualStyleBackColor = True
        '
        'btnGetInfo
        '
        Me.btnGetInfo.Location = New System.Drawing.Point(6, 20)
        Me.btnGetInfo.Name = "btnGetInfo"
        Me.btnGetInfo.Size = New System.Drawing.Size(104, 26)
        Me.btnGetInfo.TabIndex = 7
        Me.btnGetInfo.Text = "문서정보"
        Me.btnGetInfo.UseVisualStyleBackColor = True
        '
        'btnGetDetailInfo
        '
        Me.btnGetDetailInfo.Location = New System.Drawing.Point(6, 52)
        Me.btnGetDetailInfo.Name = "btnGetDetailInfo"
        Me.btnGetDetailInfo.Size = New System.Drawing.Size(104, 26)
        Me.btnGetDetailInfo.TabIndex = 6
        Me.btnGetDetailInfo.Text = "문서상세정보"
        Me.btnGetDetailInfo.UseVisualStyleBackColor = True
        '
        'btnCheckMgtKeyInUse
        '
        Me.btnCheckMgtKeyInUse.Location = New System.Drawing.Point(298, 11)
        Me.btnCheckMgtKeyInUse.Name = "btnCheckMgtKeyInUse"
        Me.btnCheckMgtKeyInUse.Size = New System.Drawing.Size(141, 26)
        Me.btnCheckMgtKeyInUse.TabIndex = 5
        Me.btnCheckMgtKeyInUse.Text = "문서번호 사용여부 확인"
        Me.btnCheckMgtKeyInUse.UseVisualStyleBackColor = True
        '
        'txtMgtKey
        '
        Me.txtMgtKey.Location = New System.Drawing.Point(151, 14)
        Me.txtMgtKey.Name = "txtMgtKey"
        Me.txtMgtKey.Size = New System.Drawing.Size(143, 21)
        Me.txtMgtKey.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "문서번호(MgtKey) : "
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox17)
        Me.GroupBox4.Controls.Add(Me.GroupBox16)
        Me.GroupBox4.Controls.Add(Me.GroupBox15)
        Me.GroupBox4.Controls.Add(Me.GroupBox14)
        Me.GroupBox4.Controls.Add(Me.GroupBox18)
        Me.GroupBox4.Controls.Add(Me.GroupBox19)
        Me.GroupBox4.Controls.Add(Me.GroupBox20)
        Me.GroupBox4.Location = New System.Drawing.Point(14, 40)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1028, 174)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "팝빌 기본 API"
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.btnUpdateCorpInfo)
        Me.GroupBox17.Controls.Add(Me.btnGetCorpInfo)
        Me.GroupBox17.Location = New System.Drawing.Point(884, 17)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(134, 153)
        Me.GroupBox17.TabIndex = 6
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "회사정보 관련"
        '
        'btnUpdateCorpInfo
        '
        Me.btnUpdateCorpInfo.Location = New System.Drawing.Point(6, 51)
        Me.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo"
        Me.btnUpdateCorpInfo.Size = New System.Drawing.Size(122, 29)
        Me.btnUpdateCorpInfo.TabIndex = 8
        Me.btnUpdateCorpInfo.Text = "회사정보 수정"
        Me.btnUpdateCorpInfo.UseVisualStyleBackColor = True
        '
        'btnGetCorpInfo
        '
        Me.btnGetCorpInfo.Location = New System.Drawing.Point(6, 20)
        Me.btnGetCorpInfo.Name = "btnGetCorpInfo"
        Me.btnGetCorpInfo.Size = New System.Drawing.Size(122, 29)
        Me.btnGetCorpInfo.TabIndex = 7
        Me.btnGetCorpInfo.Text = "회사정보 조회"
        Me.btnGetCorpInfo.UseVisualStyleBackColor = True
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.btnGetContactInfo)
        Me.GroupBox16.Controls.Add(Me.btnUpdateContact)
        Me.GroupBox16.Controls.Add(Me.btnListContact)
        Me.GroupBox16.Controls.Add(Me.btnRegistContact)
        Me.GroupBox16.Location = New System.Drawing.Point(740, 17)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(138, 153)
        Me.GroupBox16.TabIndex = 5
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "담당자 관련"
        '
        'btnGetContactInfo
        '
        Me.btnGetContactInfo.Location = New System.Drawing.Point(8, 50)
        Me.btnGetContactInfo.Name = "btnGetContactInfo"
        Me.btnGetContactInfo.Size = New System.Drawing.Size(122, 29)
        Me.btnGetContactInfo.TabIndex = 8
        Me.btnGetContactInfo.Text = "담당자 정보 확인"
        Me.btnGetContactInfo.UseVisualStyleBackColor = True
        '
        'btnUpdateContact
        '
        Me.btnUpdateContact.Location = New System.Drawing.Point(8, 112)
        Me.btnUpdateContact.Name = "btnUpdateContact"
        Me.btnUpdateContact.Size = New System.Drawing.Size(122, 29)
        Me.btnUpdateContact.TabIndex = 7
        Me.btnUpdateContact.Text = "담당자 정보 수정"
        Me.btnUpdateContact.UseVisualStyleBackColor = True
        '
        'btnListContact
        '
        Me.btnListContact.Location = New System.Drawing.Point(8, 81)
        Me.btnListContact.Name = "btnListContact"
        Me.btnListContact.Size = New System.Drawing.Size(122, 29)
        Me.btnListContact.TabIndex = 6
        Me.btnListContact.Text = "담당자 목록 조회"
        Me.btnListContact.UseVisualStyleBackColor = True
        '
        'btnRegistContact
        '
        Me.btnRegistContact.Location = New System.Drawing.Point(8, 20)
        Me.btnRegistContact.Name = "btnRegistContact"
        Me.btnRegistContact.Size = New System.Drawing.Size(122, 29)
        Me.btnRegistContact.TabIndex = 5
        Me.btnRegistContact.Text = "담당자 추가"
        Me.btnRegistContact.UseVisualStyleBackColor = True
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.btnGetAccessURL)
        Me.GroupBox15.Location = New System.Drawing.Point(572, 17)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(162, 153)
        Me.GroupBox15.TabIndex = 4
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "팝빌 기본 URL"
        '
        'btnGetAccessURL
        '
        Me.btnGetAccessURL.Location = New System.Drawing.Point(6, 19)
        Me.btnGetAccessURL.Name = "btnGetAccessURL"
        Me.btnGetAccessURL.Size = New System.Drawing.Size(150, 29)
        Me.btnGetAccessURL.TabIndex = 6
        Me.btnGetAccessURL.Text = "팝빌 로그인 URL"
        Me.btnGetAccessURL.UseVisualStyleBackColor = True
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.btnGetPartnerURL)
        Me.GroupBox14.Controls.Add(Me.btnGetPartnerPoint)
        Me.GroupBox14.Location = New System.Drawing.Point(435, 17)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(131, 152)
        Me.GroupBox14.TabIndex = 2
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "파트너과금 포인트"
        '
        'btnGetPartnerURL
        '
        Me.btnGetPartnerURL.Location = New System.Drawing.Point(6, 50)
        Me.btnGetPartnerURL.Name = "btnGetPartnerURL"
        Me.btnGetPartnerURL.Size = New System.Drawing.Size(118, 29)
        Me.btnGetPartnerURL.TabIndex = 6
        Me.btnGetPartnerURL.Text = "포인트 충전 URL"
        Me.btnGetPartnerURL.UseVisualStyleBackColor = True
        '
        'btnGetPartnerPoint
        '
        Me.btnGetPartnerPoint.Location = New System.Drawing.Point(6, 19)
        Me.btnGetPartnerPoint.Name = "btnGetPartnerPoint"
        Me.btnGetPartnerPoint.Size = New System.Drawing.Size(118, 29)
        Me.btnGetPartnerPoint.TabIndex = 3
        Me.btnGetPartnerPoint.Text = "파트너포인트 확인"
        Me.btnGetPartnerPoint.UseVisualStyleBackColor = True
        '
        'GroupBox18
        '
        Me.GroupBox18.Controls.Add(Me.btnGetUseHistoryURL)
        Me.GroupBox18.Controls.Add(Me.btnGetPaymentURL)
        Me.GroupBox18.Controls.Add(Me.btnGetChargeURL)
        Me.GroupBox18.Controls.Add(Me.btnGetBalance)
        Me.GroupBox18.Location = New System.Drawing.Point(284, 17)
        Me.GroupBox18.Name = "GroupBox18"
        Me.GroupBox18.Size = New System.Drawing.Size(144, 152)
        Me.GroupBox18.TabIndex = 1
        Me.GroupBox18.TabStop = False
        Me.GroupBox18.Text = "연동과금 포인트"
        '
        'btnGetUseHistoryURL
        '
        Me.btnGetUseHistoryURL.Location = New System.Drawing.Point(6, 112)
        Me.btnGetUseHistoryURL.Name = "btnGetUseHistoryURL"
        Me.btnGetUseHistoryURL.Size = New System.Drawing.Size(132, 29)
        Me.btnGetUseHistoryURL.TabIndex = 7
        Me.btnGetUseHistoryURL.Text = "포인트 사용내역 URL"
        Me.btnGetUseHistoryURL.UseVisualStyleBackColor = True
        '
        'btnGetPaymentURL
        '
        Me.btnGetPaymentURL.Location = New System.Drawing.Point(6, 81)
        Me.btnGetPaymentURL.Name = "btnGetPaymentURL"
        Me.btnGetPaymentURL.Size = New System.Drawing.Size(132, 29)
        Me.btnGetPaymentURL.TabIndex = 6
        Me.btnGetPaymentURL.Text = "포인트 결제내역 URL"
        Me.btnGetPaymentURL.UseVisualStyleBackColor = True
        '
        'btnGetChargeURL
        '
        Me.btnGetChargeURL.Location = New System.Drawing.Point(6, 50)
        Me.btnGetChargeURL.Name = "btnGetChargeURL"
        Me.btnGetChargeURL.Size = New System.Drawing.Size(132, 29)
        Me.btnGetChargeURL.TabIndex = 5
        Me.btnGetChargeURL.Text = "포인트 충전 URL"
        Me.btnGetChargeURL.UseVisualStyleBackColor = True
        '
        'btnGetBalance
        '
        Me.btnGetBalance.Location = New System.Drawing.Point(6, 19)
        Me.btnGetBalance.Name = "btnGetBalance"
        Me.btnGetBalance.Size = New System.Drawing.Size(132, 29)
        Me.btnGetBalance.TabIndex = 2
        Me.btnGetBalance.Text = "잔여포인트 확인"
        Me.btnGetBalance.UseVisualStyleBackColor = True
        '
        'GroupBox19
        '
        Me.GroupBox19.Controls.Add(Me.btnGetChargeInfo)
        Me.GroupBox19.Controls.Add(Me.btnGetUnitCost)
        Me.GroupBox19.Location = New System.Drawing.Point(145, 17)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Size = New System.Drawing.Size(131, 152)
        Me.GroupBox19.TabIndex = 1
        Me.GroupBox19.TabStop = False
        Me.GroupBox19.Text = "포인트 관련"
        '
        'btnGetChargeInfo
        '
        Me.btnGetChargeInfo.Location = New System.Drawing.Point(6, 19)
        Me.btnGetChargeInfo.Name = "btnGetChargeInfo"
        Me.btnGetChargeInfo.Size = New System.Drawing.Size(118, 29)
        Me.btnGetChargeInfo.TabIndex = 4
        Me.btnGetChargeInfo.Text = "과금정보 확인"
        Me.btnGetChargeInfo.UseVisualStyleBackColor = True
        '
        'btnGetUnitCost
        '
        Me.btnGetUnitCost.Location = New System.Drawing.Point(6, 50)
        Me.btnGetUnitCost.Name = "btnGetUnitCost"
        Me.btnGetUnitCost.Size = New System.Drawing.Size(118, 29)
        Me.btnGetUnitCost.TabIndex = 3
        Me.btnGetUnitCost.Text = "요금 단가 확인"
        Me.btnGetUnitCost.UseVisualStyleBackColor = True
        '
        'GroupBox20
        '
        Me.GroupBox20.Controls.Add(Me.btnCheckID)
        Me.GroupBox20.Controls.Add(Me.btnCheckIsMember)
        Me.GroupBox20.Controls.Add(Me.btnJoinMember)
        Me.GroupBox20.Location = New System.Drawing.Point(6, 17)
        Me.GroupBox20.Name = "GroupBox20"
        Me.GroupBox20.Size = New System.Drawing.Size(131, 152)
        Me.GroupBox20.TabIndex = 0
        Me.GroupBox20.TabStop = False
        Me.GroupBox20.Text = "회원 정보"
        '
        'btnCheckID
        '
        Me.btnCheckID.Location = New System.Drawing.Point(6, 50)
        Me.btnCheckID.Name = "btnCheckID"
        Me.btnCheckID.Size = New System.Drawing.Size(118, 29)
        Me.btnCheckID.TabIndex = 3
        Me.btnCheckID.Text = "ID 중복 확인"
        Me.btnCheckID.UseVisualStyleBackColor = True
        '
        'btnCheckIsMember
        '
        Me.btnCheckIsMember.Location = New System.Drawing.Point(6, 19)
        Me.btnCheckIsMember.Name = "btnCheckIsMember"
        Me.btnCheckIsMember.Size = New System.Drawing.Size(118, 29)
        Me.btnCheckIsMember.TabIndex = 2
        Me.btnCheckIsMember.Text = "가입여부 확인"
        Me.btnCheckIsMember.UseVisualStyleBackColor = True
        '
        'btnJoinMember
        '
        Me.btnJoinMember.Location = New System.Drawing.Point(6, 81)
        Me.btnJoinMember.Name = "btnJoinMember"
        Me.btnJoinMember.Size = New System.Drawing.Size(118, 29)
        Me.btnJoinMember.TabIndex = 1
        Me.btnJoinMember.Text = "회원 가입"
        Me.btnJoinMember.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(672, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "응답 URL :"
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(742, 13)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(278, 21)
        Me.txtURL.TabIndex = 24
        '
        'frmExample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1062, 679)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCorpNum)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmExample"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "팝빌 현금영수증 SDK VB.NET Example"
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.groupBox10.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox18.ResumeLayout(False)
        Me.GroupBox19.ResumeLayout(False)
        Me.GroupBox20.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCorpNum As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMgtKey As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnCheckMgtKeyInUse As System.Windows.Forms.Button
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetDetailInfo As System.Windows.Forms.Button
    Friend WithEvents btnGetInfo As System.Windows.Forms.Button
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetURL_WRITE As System.Windows.Forms.Button
    Friend WithEvents btnGetURL_PBOX As System.Windows.Forms.Button
    Friend WithEvents btnGetURL_TBOX As System.Windows.Forms.Button
    Friend WithEvents btnGetInfos As System.Windows.Forms.Button
    Friend WithEvents btnGetLogs As System.Windows.Forms.Button
    Friend WithEvents btnSendFAX As System.Windows.Forms.Button
    Friend WithEvents btnSendSMS As System.Windows.Forms.Button
    Friend WithEvents btnSendEmail As System.Windows.Forms.Button
    Friend WithEvents btnGetEmailURL As System.Windows.Forms.Button
    Friend WithEvents btnGetMassPrintURL As System.Windows.Forms.Button
    Friend WithEvents btnEPrintURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPrintURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPopUpURL As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox17 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateCorpInfo As System.Windows.Forms.Button
    Friend WithEvents btnGetCorpInfo As System.Windows.Forms.Button
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateContact As System.Windows.Forms.Button
    Friend WithEvents btnListContact As System.Windows.Forms.Button
    Friend WithEvents btnRegistContact As System.Windows.Forms.Button
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetAccessURL As System.Windows.Forms.Button
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetPartnerURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPartnerPoint As System.Windows.Forms.Button
    Friend WithEvents GroupBox18 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetChargeURL As System.Windows.Forms.Button
    Friend WithEvents btnGetBalance As System.Windows.Forms.Button
    Friend WithEvents GroupBox19 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetChargeInfo As System.Windows.Forms.Button
    Friend WithEvents btnGetUnitCost As System.Windows.Forms.Button
    Friend WithEvents GroupBox20 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCheckID As System.Windows.Forms.Button
    Friend WithEvents btnCheckIsMember As System.Windows.Forms.Button
    Friend WithEvents btnJoinMember As System.Windows.Forms.Button
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRevokeRegistIssue_part As System.Windows.Forms.Button
    Friend WithEvents btnCancelIssue02 As System.Windows.Forms.Button
    Friend WithEvents btnDelete02 As System.Windows.Forms.Button
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnRevokRegistIssue As System.Windows.Forms.Button
    Friend WithEvents label11 As System.Windows.Forms.Label
    Friend WithEvents groupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancelIssueSub As System.Windows.Forms.Button
    Friend WithEvents btnRegistIssue As System.Windows.Forms.Button
    Friend WithEvents btnDeleteSub As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnUpdateEmailConfig As System.Windows.Forms.Button
    Friend WithEvents btnListEmailConfig As System.Windows.Forms.Button
    Friend WithEvents btnGetPDFURL As System.Windows.Forms.Button
    Friend WithEvents btnAssignMgtKey As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents btnGetUseHistoryURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPaymentURL As System.Windows.Forms.Button
    Friend WithEvents btnGetContactInfo As System.Windows.Forms.Button

End Class
