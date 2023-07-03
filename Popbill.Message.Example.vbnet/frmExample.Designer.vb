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
        Me.groupBox7 = New System.Windows.Forms.GroupBox
        Me.btnSendLMS_same = New System.Windows.Forms.Button
        Me.btnSendLMS_hund = New System.Windows.Forms.Button
        Me.btnSendLMS_one = New System.Windows.Forms.Button
        Me.btnSendXMS_same = New System.Windows.Forms.Button
        Me.groupBox8 = New System.Windows.Forms.GroupBox
        Me.btnSendXMS_hund = New System.Windows.Forms.Button
        Me.btnSendXMS_one = New System.Windows.Forms.Button
        Me.btnGetSentListURL = New System.Windows.Forms.Button
        Me.txtReserveDT = New System.Windows.Forms.TextBox
        Me.btnSendSMS_Same = New System.Windows.Forms.Button
        Me.groupBox6 = New System.Windows.Forms.GroupBox
        Me.btn_SendSMS_hund = New System.Windows.Forms.Button
        Me.btnSendSMS_one = New System.Windows.Forms.Button
        Me.label3 = New System.Windows.Forms.Label
        Me.groupBox4 = New System.Windows.Forms.GroupBox
        Me.txtRequestNumbyRCV = New System.Windows.Forms.TextBox
        Me.txtReceiptNumbyRCV = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox18 = New System.Windows.Forms.GroupBox
        Me.txtReceiveNumRNbyRCV = New System.Windows.Forms.TextBox
        Me.btnCancelReserveRNbyRCV = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.GroupBox14 = New System.Windows.Forms.GroupBox
        Me.txtReceiveNumbyRCV = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnCancelReservebyRCV = New System.Windows.Forms.Button
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.btnCancelReserveRN = New System.Windows.Forms.Button
        Me.btnGetMessageResultRN = New System.Windows.Forms.Button
        Me.txtRequestNum = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.btnGetStates = New System.Windows.Forms.Button
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.btnSendMMS_same = New System.Windows.Forms.Button
        Me.btnSendMMS_hundered = New System.Windows.Forms.Button
        Me.btnSendMMS_one = New System.Windows.Forms.Button
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.btnCheckSenderNumber = New System.Windows.Forms.Button
        Me.btnGetSenderNumberMgtURL = New System.Windows.Forms.Button
        Me.btnGetSenderNumberList = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnGetAutoDenyList = New System.Windows.Forms.Button
        Me.btnCancelReserve = New System.Windows.Forms.Button
        Me.btnGetMessageResult = New System.Windows.Forms.Button
        Me.txtReceiptNum = New System.Windows.Forms.TextBox
        Me.label4 = New System.Windows.Forms.Label
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
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
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btnGetPartnerURL = New System.Windows.Forms.Button
        Me.btnGetPartnerBalance = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnGetUseHistoryURL = New System.Windows.Forms.Button
        Me.btnGetPaymentURL = New System.Windows.Forms.Button
        Me.btnGetChargeURL = New System.Windows.Forms.Button
        Me.btnGetBalance = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnGetChargeInfo_MMS = New System.Windows.Forms.Button
        Me.btnGetChargeInfo_LMS = New System.Windows.Forms.Button
        Me.btnGetUnitCost_MMS = New System.Windows.Forms.Button
        Me.btnGetUnitCost_LMS = New System.Windows.Forms.Button
        Me.btnGetChargeInfo_SMS = New System.Windows.Forms.Button
        Me.btnUnitCost_SMS = New System.Windows.Forms.Button
        Me.btnPaymentRequest = New System.Windows.Forms.Button
        Me.btnGetSettleResult = New System.Windows.Forms.Button
        Me.btnGetPaymentHistory = New System.Windows.Forms.Button
        Me.btnGetUseHistory = New System.Windows.Forms.Button
        Me.btnRefund = New System.Windows.Forms.Button
        Me.btnGetRefundHistory = New System.Windows.Forms.Button
        Me.btnGetRefundableBalance = New System.Windows.Forms.Button
        Me.btnGetRefundInfo = New System.Windows.Forms.Button
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.btnCheckID = New System.Windows.Forms.Button
        Me.btnCheckIsMember = New System.Windows.Forms.Button
        Me.btnJoinMember = New System.Windows.Forms.Button
        Me.btnQuitMember = New System.Windows.Forms.Button
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCorpNum = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.fileDialog = New System.Windows.Forms.OpenFileDialog
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.btnCheckAutoDenyNumber = New System.Windows.Forms.Button
        Me.groupBox7.SuspendLayout()
        Me.groupBox8.SuspendLayout()
        Me.groupBox6.SuspendLayout()
        Me.groupBox4.SuspendLayout()
        Me.GroupBox18.SuspendLayout()
        Me.GroupBox14.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBox7
        '
        Me.groupBox7.Controls.Add(Me.btnSendLMS_same)
        Me.groupBox7.Controls.Add(Me.btnSendLMS_hund)
        Me.groupBox7.Controls.Add(Me.btnSendLMS_one)
        Me.groupBox7.Location = New System.Drawing.Point(199, 58)
        Me.groupBox7.Name = "groupBox7"
        Me.groupBox7.Size = New System.Drawing.Size(172, 55)
        Me.groupBox7.TabIndex = 18
        Me.groupBox7.TabStop = False
        Me.groupBox7.Text = "LMS 문자 전송"
        '
        'btnSendLMS_same
        '
        Me.btnSendLMS_same.Location = New System.Drawing.Point(115, 20)
        Me.btnSendLMS_same.Name = "btnSendLMS_same"
        Me.btnSendLMS_same.Size = New System.Drawing.Size(47, 27)
        Me.btnSendLMS_same.TabIndex = 2
        Me.btnSendLMS_same.Text = "동보"
        Me.btnSendLMS_same.UseVisualStyleBackColor = True
        '
        'btnSendLMS_hund
        '
        Me.btnSendLMS_hund.Location = New System.Drawing.Point(62, 20)
        Me.btnSendLMS_hund.Name = "btnSendLMS_hund"
        Me.btnSendLMS_hund.Size = New System.Drawing.Size(47, 27)
        Me.btnSendLMS_hund.TabIndex = 1
        Me.btnSendLMS_hund.Text = "100건"
        Me.btnSendLMS_hund.UseVisualStyleBackColor = True
        '
        'btnSendLMS_one
        '
        Me.btnSendLMS_one.Location = New System.Drawing.Point(9, 20)
        Me.btnSendLMS_one.Name = "btnSendLMS_one"
        Me.btnSendLMS_one.Size = New System.Drawing.Size(47, 27)
        Me.btnSendLMS_one.TabIndex = 0
        Me.btnSendLMS_one.Text = "1건"
        Me.btnSendLMS_one.UseVisualStyleBackColor = True
        '
        'btnSendXMS_same
        '
        Me.btnSendXMS_same.Location = New System.Drawing.Point(115, 20)
        Me.btnSendXMS_same.Name = "btnSendXMS_same"
        Me.btnSendXMS_same.Size = New System.Drawing.Size(47, 27)
        Me.btnSendXMS_same.TabIndex = 2
        Me.btnSendXMS_same.Text = "동보"
        Me.btnSendXMS_same.UseVisualStyleBackColor = True
        '
        'groupBox8
        '
        Me.groupBox8.Controls.Add(Me.btnSendXMS_same)
        Me.groupBox8.Controls.Add(Me.btnSendXMS_hund)
        Me.groupBox8.Controls.Add(Me.btnSendXMS_one)
        Me.groupBox8.Location = New System.Drawing.Point(378, 58)
        Me.groupBox8.Name = "groupBox8"
        Me.groupBox8.Size = New System.Drawing.Size(172, 55)
        Me.groupBox8.TabIndex = 19
        Me.groupBox8.TabStop = False
        Me.groupBox8.Text = "XMS 문자 전송"
        '
        'btnSendXMS_hund
        '
        Me.btnSendXMS_hund.Location = New System.Drawing.Point(62, 20)
        Me.btnSendXMS_hund.Name = "btnSendXMS_hund"
        Me.btnSendXMS_hund.Size = New System.Drawing.Size(47, 27)
        Me.btnSendXMS_hund.TabIndex = 1
        Me.btnSendXMS_hund.Text = "100건"
        Me.btnSendXMS_hund.UseVisualStyleBackColor = True
        '
        'btnSendXMS_one
        '
        Me.btnSendXMS_one.Location = New System.Drawing.Point(9, 20)
        Me.btnSendXMS_one.Name = "btnSendXMS_one"
        Me.btnSendXMS_one.Size = New System.Drawing.Size(47, 27)
        Me.btnSendXMS_one.TabIndex = 0
        Me.btnSendXMS_one.Text = "1건"
        Me.btnSendXMS_one.UseVisualStyleBackColor = True
        '
        'btnGetSentListURL
        '
        Me.btnGetSentListURL.Location = New System.Drawing.Point(676, 18)
        Me.btnGetSentListURL.Name = "btnGetSentListURL"
        Me.btnGetSentListURL.Size = New System.Drawing.Size(132, 33)
        Me.btnGetSentListURL.TabIndex = 20
        Me.btnGetSentListURL.Text = "전송내역조회 팝업"
        Me.btnGetSentListURL.UseVisualStyleBackColor = True
        '
        'txtReserveDT
        '
        Me.txtReserveDT.Location = New System.Drawing.Point(201, 26)
        Me.txtReserveDT.Name = "txtReserveDT"
        Me.txtReserveDT.Size = New System.Drawing.Size(168, 21)
        Me.txtReserveDT.TabIndex = 14
        '
        'btnSendSMS_Same
        '
        Me.btnSendSMS_Same.Location = New System.Drawing.Point(115, 20)
        Me.btnSendSMS_Same.Name = "btnSendSMS_Same"
        Me.btnSendSMS_Same.Size = New System.Drawing.Size(47, 27)
        Me.btnSendSMS_Same.TabIndex = 2
        Me.btnSendSMS_Same.Text = "동보"
        Me.btnSendSMS_Same.UseVisualStyleBackColor = True
        '
        'groupBox6
        '
        Me.groupBox6.Controls.Add(Me.btnSendSMS_Same)
        Me.groupBox6.Controls.Add(Me.btn_SendSMS_hund)
        Me.groupBox6.Controls.Add(Me.btnSendSMS_one)
        Me.groupBox6.Location = New System.Drawing.Point(18, 58)
        Me.groupBox6.Name = "groupBox6"
        Me.groupBox6.Size = New System.Drawing.Size(172, 55)
        Me.groupBox6.TabIndex = 15
        Me.groupBox6.TabStop = False
        Me.groupBox6.Text = "SMS 문자 전송"
        '
        'btn_SendSMS_hund
        '
        Me.btn_SendSMS_hund.Location = New System.Drawing.Point(62, 20)
        Me.btn_SendSMS_hund.Name = "btn_SendSMS_hund"
        Me.btn_SendSMS_hund.Size = New System.Drawing.Size(47, 27)
        Me.btn_SendSMS_hund.TabIndex = 1
        Me.btn_SendSMS_hund.Text = "100건"
        Me.btn_SendSMS_hund.UseVisualStyleBackColor = True
        '
        'btnSendSMS_one
        '
        Me.btnSendSMS_one.Location = New System.Drawing.Point(9, 20)
        Me.btnSendSMS_one.Name = "btnSendSMS_one"
        Me.btnSendSMS_one.Size = New System.Drawing.Size(47, 27)
        Me.btnSendSMS_one.TabIndex = 0
        Me.btnSendSMS_one.Text = "1건"
        Me.btnSendSMS_one.UseVisualStyleBackColor = True
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(15, 32)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(191, 12)
        Me.label3.TabIndex = 13
        Me.label3.Text = "예약시간(yyyyMMddHHmmss) : "
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.txtRequestNumbyRCV)
        Me.groupBox4.Controls.Add(Me.txtReceiptNumbyRCV)
        Me.groupBox4.Controls.Add(Me.Label8)
        Me.groupBox4.Controls.Add(Me.Label7)
        Me.groupBox4.Controls.Add(Me.GroupBox18)
        Me.groupBox4.Controls.Add(Me.GroupBox14)
        Me.groupBox4.Controls.Add(Me.ListBox1)
        Me.groupBox4.Controls.Add(Me.btnCancelReserveRN)
        Me.groupBox4.Controls.Add(Me.btnGetMessageResultRN)
        Me.groupBox4.Controls.Add(Me.txtRequestNum)
        Me.groupBox4.Controls.Add(Me.Label5)
        Me.groupBox4.Controls.Add(Me.GroupBox13)
        Me.groupBox4.Controls.Add(Me.btnGetStates)
        Me.groupBox4.Controls.Add(Me.GroupBox11)
        Me.groupBox4.Controls.Add(Me.GroupBox10)
        Me.groupBox4.Controls.Add(Me.btnSearch)
        Me.groupBox4.Controls.Add(Me.btnGetAutoDenyList)
        Me.groupBox4.Controls.Add(Me.btnCancelReserve)
        Me.groupBox4.Controls.Add(Me.btnGetMessageResult)
        Me.groupBox4.Controls.Add(Me.btnGetSentListURL)
        Me.groupBox4.Controls.Add(Me.groupBox8)
        Me.groupBox4.Controls.Add(Me.groupBox7)
        Me.groupBox4.Controls.Add(Me.txtReceiptNum)
        Me.groupBox4.Controls.Add(Me.label4)
        Me.groupBox4.Controls.Add(Me.groupBox6)
        Me.groupBox4.Controls.Add(Me.txtReserveDT)
        Me.groupBox4.Controls.Add(Me.label3)
        Me.groupBox4.Controls.Add(Me.GroupBox12)
        Me.groupBox4.Location = New System.Drawing.Point(15, 370)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(1198, 450)
        Me.groupBox4.TabIndex = 23
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "메시지 관련 기능"
        '
        'txtRequestNumbyRCV
        '
        Me.txtRequestNumbyRCV.Location = New System.Drawing.Point(908, 139)
        Me.txtRequestNumbyRCV.Name = "txtRequestNumbyRCV"
        Me.txtRequestNumbyRCV.Size = New System.Drawing.Size(165, 21)
        Me.txtRequestNumbyRCV.TabIndex = 42
        '
        'txtReceiptNumbyRCV
        '
        Me.txtReceiptNumbyRCV.Location = New System.Drawing.Point(638, 139)
        Me.txtReceiptNumbyRCV.Name = "txtReceiptNumbyRCV"
        Me.txtReceiptNumbyRCV.Size = New System.Drawing.Size(165, 21)
        Me.txtReceiptNumbyRCV.TabIndex = 37
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(846, 143)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 12)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "요청번호 : "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(576, 143)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 12)
        Me.Label7.TabIndex = 36
        Me.Label7.Text = "접수번호 : "
        '
        'GroupBox18
        '
        Me.GroupBox18.Controls.Add(Me.txtReceiveNumRNbyRCV)
        Me.GroupBox18.Controls.Add(Me.btnCancelReserveRNbyRCV)
        Me.GroupBox18.Controls.Add(Me.Label10)
        Me.GroupBox18.Location = New System.Drawing.Point(831, 119)
        Me.GroupBox18.Name = "GroupBox18"
        Me.GroupBox18.Size = New System.Drawing.Size(264, 115)
        Me.GroupBox18.TabIndex = 45
        Me.GroupBox18.TabStop = False
        Me.GroupBox18.Text = "요청번호 할당 전송건 처리"
        '
        'txtReceiveNumRNbyRCV
        '
        Me.txtReceiveNumRNbyRCV.Location = New System.Drawing.Point(77, 47)
        Me.txtReceiveNumRNbyRCV.Name = "txtReceiveNumRNbyRCV"
        Me.txtReceiveNumRNbyRCV.Size = New System.Drawing.Size(165, 21)
        Me.txtReceiveNumRNbyRCV.TabIndex = 47
        '
        'btnCancelReserveRNbyRCV
        '
        Me.btnCancelReserveRNbyRCV.Location = New System.Drawing.Point(17, 79)
        Me.btnCancelReserveRNbyRCV.Name = "btnCancelReserveRNbyRCV"
        Me.btnCancelReserveRNbyRCV.Size = New System.Drawing.Size(225, 30)
        Me.btnCancelReserveRNbyRCV.TabIndex = 44
        Me.btnCancelReserveRNbyRCV.Text = "예약전송 취소 (요청번호, 수신번호)"
        Me.btnCancelReserveRNbyRCV.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 51)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 12)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "수신번호 : "
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.txtReceiveNumbyRCV)
        Me.GroupBox14.Controls.Add(Me.Label9)
        Me.GroupBox14.Controls.Add(Me.btnCancelReservebyRCV)
        Me.GroupBox14.Location = New System.Drawing.Point(561, 119)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(264, 115)
        Me.GroupBox14.TabIndex = 40
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "접수번호 관련 기능 (요청번호 미할당)"
        '
        'txtReceiveNumbyRCV
        '
        Me.txtReceiveNumbyRCV.Location = New System.Drawing.Point(77, 47)
        Me.txtReceiveNumbyRCV.Name = "txtReceiveNumbyRCV"
        Me.txtReceiveNumbyRCV.Size = New System.Drawing.Size(165, 21)
        Me.txtReceiveNumbyRCV.TabIndex = 47
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 51)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 12)
        Me.Label9.TabIndex = 46
        Me.Label9.Text = "수신번호 : "
        '
        'btnCancelReservebyRCV
        '
        Me.btnCancelReservebyRCV.Location = New System.Drawing.Point(17, 79)
        Me.btnCancelReservebyRCV.Name = "btnCancelReservebyRCV"
        Me.btnCancelReservebyRCV.Size = New System.Drawing.Size(225, 30)
        Me.btnCancelReservebyRCV.TabIndex = 39
        Me.btnCancelReservebyRCV.Text = "예약전송 취소 (접수번호, 수신번호)"
        Me.btnCancelReservebyRCV.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.HorizontalScrollbar = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Location = New System.Drawing.Point(9, 240)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(1164, 196)
        Me.ListBox1.TabIndex = 35
        '
        'btnCancelReserveRN
        '
        Me.btnCancelReserveRN.Location = New System.Drawing.Point(417, 166)
        Me.btnCancelReserveRN.Name = "btnCancelReserveRN"
        Me.btnCancelReserveRN.Size = New System.Drawing.Size(121, 30)
        Me.btnCancelReserveRN.TabIndex = 33
        Me.btnCancelReserveRN.Text = "예약 전송 취소"
        Me.btnCancelReserveRN.UseVisualStyleBackColor = True
        '
        'btnGetMessageResultRN
        '
        Me.btnGetMessageResultRN.Location = New System.Drawing.Point(290, 166)
        Me.btnGetMessageResultRN.Name = "btnGetMessageResultRN"
        Me.btnGetMessageResultRN.Size = New System.Drawing.Size(121, 30)
        Me.btnGetMessageResultRN.TabIndex = 32
        Me.btnGetMessageResultRN.Text = "전송상태확인"
        Me.btnGetMessageResultRN.UseVisualStyleBackColor = True
        '
        'txtRequestNum
        '
        Me.txtRequestNum.Location = New System.Drawing.Point(363, 139)
        Me.txtRequestNum.Name = "txtRequestNum"
        Me.txtRequestNum.Size = New System.Drawing.Size(165, 21)
        Me.txtRequestNum.TabIndex = 31
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(301, 143)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "요청번호 : "
        '
        'GroupBox13
        '
        Me.GroupBox13.Location = New System.Drawing.Point(286, 119)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(264, 88)
        Me.GroupBox13.TabIndex = 34
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "요청번호 할당 전송건 처리"
        '
        'btnGetStates
        '
        Me.btnGetStates.Location = New System.Drawing.Point(537, 18)
        Me.btnGetStates.Name = "btnGetStates"
        Me.btnGetStates.Size = New System.Drawing.Size(132, 33)
        Me.btnGetStates.TabIndex = 28
        Me.btnGetStates.Text = "전송내역 요약정보"
        Me.btnGetStates.UseVisualStyleBackColor = True
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.btnSendMMS_same)
        Me.GroupBox11.Controls.Add(Me.btnSendMMS_hundered)
        Me.GroupBox11.Controls.Add(Me.btnSendMMS_one)
        Me.GroupBox11.Location = New System.Drawing.Point(561, 58)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(172, 55)
        Me.GroupBox11.TabIndex = 27
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "MMS 문자 전송"
        '
        'btnSendMMS_same
        '
        Me.btnSendMMS_same.Location = New System.Drawing.Point(115, 20)
        Me.btnSendMMS_same.Name = "btnSendMMS_same"
        Me.btnSendMMS_same.Size = New System.Drawing.Size(47, 27)
        Me.btnSendMMS_same.TabIndex = 2
        Me.btnSendMMS_same.Text = "동보"
        Me.btnSendMMS_same.UseVisualStyleBackColor = True
        '
        'btnSendMMS_hundered
        '
        Me.btnSendMMS_hundered.Location = New System.Drawing.Point(62, 20)
        Me.btnSendMMS_hundered.Name = "btnSendMMS_hundered"
        Me.btnSendMMS_hundered.Size = New System.Drawing.Size(47, 27)
        Me.btnSendMMS_hundered.TabIndex = 1
        Me.btnSendMMS_hundered.Text = "100건"
        Me.btnSendMMS_hundered.UseVisualStyleBackColor = True
        '
        'btnSendMMS_one
        '
        Me.btnSendMMS_one.Location = New System.Drawing.Point(9, 20)
        Me.btnSendMMS_one.Name = "btnSendMMS_one"
        Me.btnSendMMS_one.Size = New System.Drawing.Size(47, 27)
        Me.btnSendMMS_one.TabIndex = 0
        Me.btnSendMMS_one.Text = "1건"
        Me.btnSendMMS_one.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.btnCheckSenderNumber)
        Me.GroupBox10.Controls.Add(Me.btnGetSenderNumberMgtURL)
        Me.GroupBox10.Controls.Add(Me.btnGetSenderNumberList)
        Me.GroupBox10.Location = New System.Drawing.Point(739, 58)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(453, 55)
        Me.GroupBox10.TabIndex = 26
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "발신번호 관리"
        '
        'btnCheckSenderNumber
        '
        Me.btnCheckSenderNumber.Location = New System.Drawing.Point(6, 18)
        Me.btnCheckSenderNumber.Name = "btnCheckSenderNumber"
        Me.btnCheckSenderNumber.Size = New System.Drawing.Size(147, 33)
        Me.btnCheckSenderNumber.TabIndex = 0
        Me.btnCheckSenderNumber.Text = "발신번호 등록여부 확인"
        Me.btnCheckSenderNumber.UseVisualStyleBackColor = True
        '
        'btnGetSenderNumberMgtURL
        '
        Me.btnGetSenderNumberMgtURL.Location = New System.Drawing.Point(154, 18)
        Me.btnGetSenderNumberMgtURL.Name = "btnGetSenderNumberMgtURL"
        Me.btnGetSenderNumberMgtURL.Size = New System.Drawing.Size(147, 33)
        Me.btnGetSenderNumberMgtURL.TabIndex = 1
        Me.btnGetSenderNumberMgtURL.Text = "발신번호 관리 팝업"
        Me.btnGetSenderNumberMgtURL.UseVisualStyleBackColor = True
        '
        'btnGetSenderNumberList
        '
        Me.btnGetSenderNumberList.Location = New System.Drawing.Point(302, 18)
        Me.btnGetSenderNumberList.Name = "btnGetSenderNumberList"
        Me.btnGetSenderNumberList.Size = New System.Drawing.Size(147, 33)
        Me.btnGetSenderNumberList.TabIndex = 2
        Me.btnGetSenderNumberList.Text = "발신번호 목록 조회"
        Me.btnGetSenderNumberList.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(396, 18)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(132, 33)
        Me.btnSearch.TabIndex = 25
        Me.btnSearch.Text = "전송내역 기간조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnGetAutoDenyList
        '
        Me.btnGetAutoDenyList.Location = New System.Drawing.Point(814, 19)
        Me.btnGetAutoDenyList.Name = "btnGetAutoDenyList"
        Me.btnGetAutoDenyList.Size = New System.Drawing.Size(132, 33)
        Me.btnGetAutoDenyList.TabIndex = 24
        Me.btnGetAutoDenyList.Text = "080 수신거부목록"
        Me.btnGetAutoDenyList.UseVisualStyleBackColor = True
        '
        'btnCancelReserve
        '
        Me.btnCancelReserve.Location = New System.Drawing.Point(144, 166)
        Me.btnCancelReserve.Name = "btnCancelReserve"
        Me.btnCancelReserve.Size = New System.Drawing.Size(121, 30)
        Me.btnCancelReserve.TabIndex = 22
        Me.btnCancelReserve.Text = "예약 전송 취소"
        Me.btnCancelReserve.UseVisualStyleBackColor = True
        '
        'btnGetMessageResult
        '
        Me.btnGetMessageResult.Location = New System.Drawing.Point(17, 166)
        Me.btnGetMessageResult.Name = "btnGetMessageResult"
        Me.btnGetMessageResult.Size = New System.Drawing.Size(121, 30)
        Me.btnGetMessageResult.TabIndex = 21
        Me.btnGetMessageResult.Text = "전송상태확인"
        Me.btnGetMessageResult.UseVisualStyleBackColor = True
        '
        'txtReceiptNum
        '
        Me.txtReceiptNum.Location = New System.Drawing.Point(90, 139)
        Me.txtReceiptNum.Name = "txtReceiptNum"
        Me.txtReceiptNum.Size = New System.Drawing.Size(165, 21)
        Me.txtReceiptNum.TabIndex = 17
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(28, 143)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(65, 12)
        Me.label4.TabIndex = 16
        Me.label4.Text = "접수번호 : "
        '
        'GroupBox12
        '
        Me.GroupBox12.Location = New System.Drawing.Point(13, 119)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(264, 88)
        Me.GroupBox12.TabIndex = 29
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "접수번호 관련 기능 (요청번호 미할당)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox17)
        Me.GroupBox1.Controls.Add(Me.GroupBox16)
        Me.GroupBox1.Controls.Add(Me.GroupBox15)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox9)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 39)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1198, 325)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "팝빌 기본 API"
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.btnUpdateCorpInfo)
        Me.GroupBox17.Controls.Add(Me.btnGetCorpInfo)
        Me.GroupBox17.Location = New System.Drawing.Point(1045, 17)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(134, 161)
        Me.GroupBox17.TabIndex = 6
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "회사정보 관련"
        '
        'btnUpdateCorpInfo
        '
        Me.btnUpdateCorpInfo.Location = New System.Drawing.Point(6, 52)
        Me.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo"
        Me.btnUpdateCorpInfo.Size = New System.Drawing.Size(122, 30)
        Me.btnUpdateCorpInfo.TabIndex = 8
        Me.btnUpdateCorpInfo.Text = "회사정보 수정"
        Me.btnUpdateCorpInfo.UseVisualStyleBackColor = True
        '
        'btnGetCorpInfo
        '
        Me.btnGetCorpInfo.Location = New System.Drawing.Point(6, 19)
        Me.btnGetCorpInfo.Name = "btnGetCorpInfo"
        Me.btnGetCorpInfo.Size = New System.Drawing.Size(122, 30)
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
        Me.GroupBox16.Location = New System.Drawing.Point(900, 17)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(138, 161)
        Me.GroupBox16.TabIndex = 5
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "담당자 관련"
        '
        'btnGetContactInfo
        '
        Me.btnGetContactInfo.Location = New System.Drawing.Point(8, 53)
        Me.btnGetContactInfo.Name = "btnGetContactInfo"
        Me.btnGetContactInfo.Size = New System.Drawing.Size(122, 30)
        Me.btnGetContactInfo.TabIndex = 8
        Me.btnGetContactInfo.Text = "담당자 정보 확인"
        Me.btnGetContactInfo.UseVisualStyleBackColor = True
        '
        'btnUpdateContact
        '
        Me.btnUpdateContact.Location = New System.Drawing.Point(8, 120)
        Me.btnUpdateContact.Name = "btnUpdateContact"
        Me.btnUpdateContact.Size = New System.Drawing.Size(122, 30)
        Me.btnUpdateContact.TabIndex = 7
        Me.btnUpdateContact.Text = "담당자 정보 수정"
        Me.btnUpdateContact.UseVisualStyleBackColor = True
        '
        'btnListContact
        '
        Me.btnListContact.Location = New System.Drawing.Point(8, 86)
        Me.btnListContact.Name = "btnListContact"
        Me.btnListContact.Size = New System.Drawing.Size(122, 30)
        Me.btnListContact.TabIndex = 6
        Me.btnListContact.Text = "담당자 목록 조회"
        Me.btnListContact.UseVisualStyleBackColor = True
        '
        'btnRegistContact
        '
        Me.btnRegistContact.Location = New System.Drawing.Point(8, 19)
        Me.btnRegistContact.Name = "btnRegistContact"
        Me.btnRegistContact.Size = New System.Drawing.Size(122, 30)
        Me.btnRegistContact.TabIndex = 5
        Me.btnRegistContact.Text = "담당자 추가"
        Me.btnRegistContact.UseVisualStyleBackColor = True
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.btnGetAccessURL)
        Me.GroupBox15.Location = New System.Drawing.Point(732, 17)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(162, 161)
        Me.GroupBox15.TabIndex = 4
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "팝빌 기본 URL"
        '
        'btnGetAccessURL
        '
        Me.btnGetAccessURL.Location = New System.Drawing.Point(6, 19)
        Me.btnGetAccessURL.Name = "btnGetAccessURL"
        Me.btnGetAccessURL.Size = New System.Drawing.Size(150, 30)
        Me.btnGetAccessURL.TabIndex = 6
        Me.btnGetAccessURL.Text = "팝빌 로그인 URL"
        Me.btnGetAccessURL.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnGetPartnerURL)
        Me.GroupBox5.Controls.Add(Me.btnGetPartnerBalance)
        Me.GroupBox5.Location = New System.Drawing.Point(595, 17)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(131, 160)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "파트너과금 포인트"
        '
        'btnGetPartnerURL
        '
        Me.btnGetPartnerURL.Location = New System.Drawing.Point(6, 52)
        Me.btnGetPartnerURL.Name = "btnGetPartnerURL"
        Me.btnGetPartnerURL.Size = New System.Drawing.Size(118, 30)
        Me.btnGetPartnerURL.TabIndex = 6
        Me.btnGetPartnerURL.Text = "포인트 충전 URL"
        Me.btnGetPartnerURL.UseVisualStyleBackColor = True
        '
        'btnGetPartnerBalance
        '
        Me.btnGetPartnerBalance.Location = New System.Drawing.Point(6, 19)
        Me.btnGetPartnerBalance.Name = "btnGetPartnerBalance"
        Me.btnGetPartnerBalance.Size = New System.Drawing.Size(118, 30)
        Me.btnGetPartnerBalance.TabIndex = 3
        Me.btnGetPartnerBalance.Text = "파트너포인트 확인"
        Me.btnGetPartnerBalance.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnGetUseHistoryURL)
        Me.GroupBox2.Controls.Add(Me.btnGetPaymentURL)
        Me.GroupBox2.Controls.Add(Me.btnGetChargeURL)
        Me.GroupBox2.Controls.Add(Me.btnGetBalance)
        Me.GroupBox2.Controls.Add(Me.btnGetPaymentHistory)
        Me.GroupBox2.Controls.Add(Me.btnGetRefundHistory)
        Me.GroupBox2.Controls.Add(Me.btnRefund)
        Me.GroupBox2.Controls.Add(Me.btnGetUseHistory)
        Me.GroupBox2.Location = New System.Drawing.Point(439, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(150, 302)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "연동과금 포인트"
        '
        'btnGetUseHistoryURL
        '
        Me.btnGetUseHistoryURL.Location = New System.Drawing.Point(7, 120)
        Me.btnGetUseHistoryURL.Name = "btnGetUseHistoryURL"
        Me.btnGetUseHistoryURL.Size = New System.Drawing.Size(137, 30)
        Me.btnGetUseHistoryURL.TabIndex = 7
        Me.btnGetUseHistoryURL.Text = "포인트 사용내역 URL"
        Me.btnGetUseHistoryURL.UseVisualStyleBackColor = True
        '
        'btnGetPaymentURL
        '
        Me.btnGetPaymentURL.Location = New System.Drawing.Point(7, 86)
        Me.btnGetPaymentURL.Name = "btnGetPaymentURL"
        Me.btnGetPaymentURL.Size = New System.Drawing.Size(137, 30)
        Me.btnGetPaymentURL.TabIndex = 6
        Me.btnGetPaymentURL.Text = "포인트 결제내역 URL"
        Me.btnGetPaymentURL.UseVisualStyleBackColor = True
        '
        'btnGetChargeURL
        '
        Me.btnGetChargeURL.Location = New System.Drawing.Point(7, 52)
        Me.btnGetChargeURL.Name = "btnGetChargeURL"
        Me.btnGetChargeURL.Size = New System.Drawing.Size(137, 30)
        Me.btnGetChargeURL.TabIndex = 5
        Me.btnGetChargeURL.Text = "포인트 충전 URL"
        Me.btnGetChargeURL.UseVisualStyleBackColor = True
        '
        'btnGetBalance
        '
        Me.btnGetBalance.Location = New System.Drawing.Point(7, 19)
        Me.btnGetBalance.Name = "btnGetBalance"
        Me.btnGetBalance.Size = New System.Drawing.Size(137, 30)
        Me.btnGetBalance.TabIndex = 2
        Me.btnGetBalance.Text = "잔여포인트 확인"
        Me.btnGetBalance.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnGetChargeInfo_MMS)
        Me.GroupBox3.Controls.Add(Me.btnGetChargeInfo_LMS)
        Me.GroupBox3.Controls.Add(Me.btnGetUnitCost_MMS)
        Me.GroupBox3.Controls.Add(Me.btnGetUnitCost_LMS)
        Me.GroupBox3.Controls.Add(Me.btnGetChargeInfo_SMS)
        Me.GroupBox3.Controls.Add(Me.btnUnitCost_SMS)
        Me.GroupBox3.Controls.Add(Me.btnPaymentRequest)
        Me.GroupBox3.Controls.Add(Me.btnGetSettleResult)
        Me.GroupBox3.Controls.Add(Me.btnGetRefundableBalance)
        Me.GroupBox3.Controls.Add(Me.btnGetRefundInfo)
        Me.GroupBox3.Location = New System.Drawing.Point(145, 17)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(288, 200)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "포인트 관련"
        '
        'btnGetChargeInfo_MMS
        '
        Me.btnGetChargeInfo_MMS.Location = New System.Drawing.Point(151, 86)
        Me.btnGetChargeInfo_MMS.Name = "btnGetChargeInfo_MMS"
        Me.btnGetChargeInfo_MMS.Size = New System.Drawing.Size(126, 30)
        Me.btnGetChargeInfo_MMS.TabIndex = 8
        Me.btnGetChargeInfo_MMS.Text = "포토 과금정보 확인"
        Me.btnGetChargeInfo_MMS.UseVisualStyleBackColor = True
        '
        'btnGetChargeInfo_LMS
        '
        Me.btnGetChargeInfo_LMS.Location = New System.Drawing.Point(151, 52)
        Me.btnGetChargeInfo_LMS.Name = "btnGetChargeInfo_LMS"
        Me.btnGetChargeInfo_LMS.Size = New System.Drawing.Size(126, 30)
        Me.btnGetChargeInfo_LMS.TabIndex = 7
        Me.btnGetChargeInfo_LMS.Text = "장문 과금정보 확인"
        Me.btnGetChargeInfo_LMS.UseVisualStyleBackColor = True
        '
        'btnGetUnitCost_MMS
        '
        Me.btnGetUnitCost_MMS.Location = New System.Drawing.Point(18, 86)
        Me.btnGetUnitCost_MMS.Name = "btnGetUnitCost_MMS"
        Me.btnGetUnitCost_MMS.Size = New System.Drawing.Size(126, 30)
        Me.btnGetUnitCost_MMS.TabIndex = 6
        Me.btnGetUnitCost_MMS.Text = "포토 요금단가 확인"
        Me.btnGetUnitCost_MMS.UseVisualStyleBackColor = True
        '
        'btnGetUnitCost_LMS
        '
        Me.btnGetUnitCost_LMS.Location = New System.Drawing.Point(18, 52)
        Me.btnGetUnitCost_LMS.Name = "btnGetUnitCost_LMS"
        Me.btnGetUnitCost_LMS.Size = New System.Drawing.Size(126, 30)
        Me.btnGetUnitCost_LMS.TabIndex = 5
        Me.btnGetUnitCost_LMS.Text = "장문 요금단가 확인"
        Me.btnGetUnitCost_LMS.UseVisualStyleBackColor = True
        '
        'btnGetChargeInfo_SMS
        '
        Me.btnGetChargeInfo_SMS.Location = New System.Drawing.Point(151, 19)
        Me.btnGetChargeInfo_SMS.Name = "btnGetChargeInfo_SMS"
        Me.btnGetChargeInfo_SMS.Size = New System.Drawing.Size(126, 30)
        Me.btnGetChargeInfo_SMS.TabIndex = 4
        Me.btnGetChargeInfo_SMS.Text = "단문 과금정보 확인"
        Me.btnGetChargeInfo_SMS.UseVisualStyleBackColor = True
        '
        'btnUnitCost_SMS
        '
        Me.btnUnitCost_SMS.Location = New System.Drawing.Point(18, 19)
        Me.btnUnitCost_SMS.Name = "btnUnitCost_SMS"
        Me.btnUnitCost_SMS.Size = New System.Drawing.Size(126, 30)
        Me.btnUnitCost_SMS.TabIndex = 3
        Me.btnUnitCost_SMS.Text = "단문 요금단가 확인"
        Me.btnUnitCost_SMS.UseVisualStyleBackColor = True
        '
        'btnPaymentRequest
        '
        Me.btnPaymentRequest.Location = New System.Drawing.Point(18, 120)
        Me.btnPaymentRequest.Name = "btnPaymentRequest"
        Me.btnPaymentRequest.Size = New System.Drawing.Size(126, 32)
        Me.btnPaymentRequest.TabIndex = 8
        Me.btnPaymentRequest.Text = "무통장 입금신청"
        '
        'btnGetSettleResult
        '
        Me.btnGetSettleResult.Location = New System.Drawing.Point(18, 156)
        Me.btnGetSettleResult.Name = "btnGetSettleResult"
        Me.btnGetSettleResult.Size = New System.Drawing.Size(126, 32)
        Me.btnGetSettleResult.TabIndex = 0
        Me.btnGetSettleResult.Text = "무통장 입금신청 정보확인"
        '
        'btnGetPaymentHistory
        '
        Me.btnGetPaymentHistory.Location = New System.Drawing.Point(7, 156)
        Me.btnGetPaymentHistory.Name = "btnGetPaymentHistory"
        Me.btnGetPaymentHistory.Size = New System.Drawing.Size(137, 32)
        Me.btnGetPaymentHistory.TabIndex = 0
        Me.btnGetPaymentHistory.Text = "포인트 결제내역 확인"
        '
        'btnGetUseHistory
        '
        Me.btnGetUseHistory.Location = New System.Drawing.Point(7, 192)
        Me.btnGetUseHistory.Name = "btnGetUseHistory"
        Me.btnGetUseHistory.Size = New System.Drawing.Size(137, 32)
        Me.btnGetUseHistory.TabIndex = 0
        Me.btnGetUseHistory.Text = "포인트 사용내역 확인"
        '
        'btnRefund
        '
        Me.btnRefund.Location = New System.Drawing.Point(7, 228)
        Me.btnRefund.Name = "btnRefund"
        Me.btnRefund.Size = New System.Drawing.Size(137, 32)
        Me.btnRefund.TabIndex = 0
        Me.btnRefund.Text = "포인트 환불신청"
        '
        'btnGetRefundHistory
        '
        Me.btnGetRefundHistory.Location = New System.Drawing.Point(7, 264)
        Me.btnGetRefundHistory.Name = "btnGetRefundHistory"
        Me.btnGetRefundHistory.Size = New System.Drawing.Size(137, 32)
        Me.btnGetRefundHistory.TabIndex = 0
        Me.btnGetRefundHistory.Text = "포인트 환불내역 확인"
        '
        'btnGetRefundableBalance
        '
        Me.btnGetRefundableBalance.Location = New System.Drawing.Point(150, 156)
        Me.btnGetRefundableBalance.Name = "btnGetRefundableBalance"
        Me.btnGetRefundableBalance.Size = New System.Drawing.Size(127, 32)
        Me.btnGetRefundableBalance.TabIndex = 0
        Me.btnGetRefundableBalance.Text = "환불 가능 포인트 조회"
        '
        'btnGetRefundInfo
        '
        Me.btnGetRefundInfo.Location = New System.Drawing.Point(151, 120)
        Me.btnGetRefundInfo.Name = "btnGetRefundInfo"
        Me.btnGetRefundInfo.Size = New System.Drawing.Size(126, 32)
        Me.btnGetRefundInfo.TabIndex = 0
        Me.btnGetRefundInfo.Text = "환불 신청 상태 조회"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.btnCheckID)
        Me.GroupBox9.Controls.Add(Me.btnCheckIsMember)
        Me.GroupBox9.Controls.Add(Me.btnJoinMember)
        Me.GroupBox9.Controls.Add(Me.btnQuitMember)
        Me.GroupBox9.Location = New System.Drawing.Point(6, 17)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(131, 160)
        Me.GroupBox9.TabIndex = 0
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "회원 정보"
        '
        'btnCheckID
        '
        Me.btnCheckID.Location = New System.Drawing.Point(6, 51)
        Me.btnCheckID.Name = "btnCheckID"
        Me.btnCheckID.Size = New System.Drawing.Size(118, 30)
        Me.btnCheckID.TabIndex = 3
        Me.btnCheckID.Text = "ID 중복 확인"
        Me.btnCheckID.UseVisualStyleBackColor = True
        '
        'btnCheckIsMember
        '
        Me.btnCheckIsMember.Location = New System.Drawing.Point(6, 19)
        Me.btnCheckIsMember.Name = "btnCheckIsMember"
        Me.btnCheckIsMember.Size = New System.Drawing.Size(118, 30)
        Me.btnCheckIsMember.TabIndex = 2
        Me.btnCheckIsMember.Text = "가입여부 확인"
        Me.btnCheckIsMember.UseVisualStyleBackColor = True
        '
        'btnJoinMember
        '
        Me.btnJoinMember.Location = New System.Drawing.Point(6, 84)
        Me.btnJoinMember.Name = "btnJoinMember"
        Me.btnJoinMember.Size = New System.Drawing.Size(118, 30)
        Me.btnJoinMember.TabIndex = 1
        Me.btnJoinMember.Text = "회원 가입"
        Me.btnJoinMember.UseVisualStyleBackColor = True
        '
        'btnQuitMember
        '
        Me.btnQuitMember.Location = New System.Drawing.Point(6, 120)
        Me.btnQuitMember.Name = "btnQuitMember"
        Me.btnQuitMember.Size = New System.Drawing.Size(118, 32)
        Me.btnQuitMember.TabIndex = 23
        Me.btnQuitMember.Text = "팝빌 회원 탈퇴"
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(417, 9)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(143, 21)
        Me.txtUserId.TabIndex = 27
        Me.txtUserId.Text = "testkorea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(312, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 12)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "팝빌회원 아이디 :"
        '
        'txtCorpNum
        '
        Me.txtCorpNum.Location = New System.Drawing.Point(148, 10)
        Me.txtCorpNum.Name = "txtCorpNum"
        Me.txtCorpNum.Size = New System.Drawing.Size(143, 21)
        Me.txtCorpNum.TabIndex = 25
        Me.txtCorpNum.Text = "1234567890"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 12)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "팝빌회원 사업자번호 : "
        '
        'fileDialog
        '
        Me.fileDialog.FileName = "fileDialog"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(822, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 12)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "응답 URL :"
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(892, 11)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(285, 21)
        Me.txtURL.TabIndex = 36
        '
        'btnCheckAutoDenyNumber
        '
        Me.btnCheckAutoDenyNumber.Location = New System.Drawing.Point(8, 132)
        Me.btnCheckAutoDenyNumber.Name = "btnCheckAutoDenyNumber"
        Me.btnCheckAutoDenyNumber.Size = New System.Drawing.Size(104, 32)
        Me.btnCheckAutoDenyNumber.TabIndex = 23
        Me.btnCheckAutoDenyNumber.Text = "080 번호 확인"
        '
        'frmExample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1225, 828)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCorpNum)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.groupBox4)
        Me.Name = "frmExample"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "팝빌 문자메시지 SDK VB.NET Example"
        Me.groupBox7.ResumeLayout(False)
        Me.groupBox8.ResumeLayout(False)
        Me.groupBox6.ResumeLayout(False)
        Me.groupBox4.ResumeLayout(False)
        Me.groupBox4.PerformLayout()
        Me.GroupBox18.ResumeLayout(False)
        Me.GroupBox18.PerformLayout()
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents groupBox7 As System.Windows.Forms.GroupBox
    Private WithEvents btnSendLMS_same As System.Windows.Forms.Button
    Private WithEvents btnSendLMS_hund As System.Windows.Forms.Button
    Private WithEvents btnSendLMS_one As System.Windows.Forms.Button
    Private WithEvents btnSendXMS_same As System.Windows.Forms.Button
    Private WithEvents groupBox8 As System.Windows.Forms.GroupBox
    Private WithEvents btnSendXMS_hund As System.Windows.Forms.Button
    Private WithEvents btnSendXMS_one As System.Windows.Forms.Button
    Private WithEvents btnGetSentListURL As System.Windows.Forms.Button
    Friend WithEvents txtReserveDT As System.Windows.Forms.TextBox
    Private WithEvents btnSendSMS_Same As System.Windows.Forms.Button
    Private WithEvents groupBox6 As System.Windows.Forms.GroupBox
    Private WithEvents btn_SendSMS_hund As System.Windows.Forms.Button
    Private WithEvents btnSendSMS_one As System.Windows.Forms.Button
    Friend WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents groupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox17 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateCorpInfo As System.Windows.Forms.Button
    Friend WithEvents btnGetCorpInfo As System.Windows.Forms.Button
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateContact As System.Windows.Forms.Button
    Friend WithEvents btnListContact As System.Windows.Forms.Button
    Friend WithEvents btnRegistContact As System.Windows.Forms.Button
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetAccessURL As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetPartnerURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPartnerBalance As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetChargeURL As System.Windows.Forms.Button
    Friend WithEvents btnGetBalance As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetChargeInfo_SMS As System.Windows.Forms.Button
    Friend WithEvents btnUnitCost_SMS As System.Windows.Forms.Button
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCheckID As System.Windows.Forms.Button
    Friend WithEvents btnCheckIsMember As System.Windows.Forms.Button
    Friend WithEvents btnJoinMember As System.Windows.Forms.Button
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCorpNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnGetUnitCost_LMS As System.Windows.Forms.Button
    Friend WithEvents btnGetUnitCost_MMS As System.Windows.Forms.Button
    Friend WithEvents btnGetChargeInfo_LMS As System.Windows.Forms.Button
    Friend WithEvents btnGetChargeInfo_MMS As System.Windows.Forms.Button
    Private WithEvents btnGetAutoDenyList As System.Windows.Forms.Button
    Private WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetSenderNumberList As System.Windows.Forms.Button
    Friend WithEvents btnGetSenderNumberMgtURL As System.Windows.Forms.Button
    Friend WithEvents fileDialog As System.Windows.Forms.OpenFileDialog
    Private WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Private WithEvents btnSendMMS_same As System.Windows.Forms.Button
    Private WithEvents btnSendMMS_hundered As System.Windows.Forms.Button
    Private WithEvents btnSendMMS_one As System.Windows.Forms.Button
    Private WithEvents btnGetStates As System.Windows.Forms.Button
    Private WithEvents btnCancelReserveRN As System.Windows.Forms.Button
    Private WithEvents btnGetMessageResultRN As System.Windows.Forms.Button
    Friend WithEvents txtRequestNum As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents btnGetUseHistoryURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPaymentURL As System.Windows.Forms.Button
    Friend WithEvents btnGetContactInfo As System.Windows.Forms.Button
    Friend WithEvents btnCheckSenderNumber As System.Windows.Forms.Button
    Private WithEvents btnCancelReserveRNbyRCV As System.Windows.Forms.Button
    Private WithEvents btnCancelReservebyRCV As System.Windows.Forms.Button
    Friend WithEvents txtRequestNumbyRCV As System.Windows.Forms.TextBox
    Friend WithEvents txtReceiptNumbyRCV As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents GroupBox18 As System.Windows.Forms.GroupBox
    Private WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Private WithEvents btnCancelReserve As System.Windows.Forms.Button
    Private WithEvents btnGetMessageResult As System.Windows.Forms.Button
    Friend WithEvents txtReceiptNum As System.Windows.Forms.TextBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents txtReceiveNumRNbyRCV As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtReceiveNumbyRCV As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents btnPaymentRequest As System.Windows.Forms.Button
    Private WithEvents btnGetSettleResult As System.Windows.Forms.Button
    Private WithEvents btnGetPaymentHistory As System.Windows.Forms.Button
    Private WithEvents btnGetUseHistory As System.Windows.Forms.Button
    Private WithEvents btnRefund As System.Windows.Forms.Button
    Private WithEvents btnGetRefundHistory As System.Windows.Forms.Button
    Private WithEvents btnGetRefundableBalance As System.Windows.Forms.Button
    Private WithEvents btnGetRefundInfo As System.Windows.Forms.Button
    Private WithEvents btnQuitMember As System.Windows.Forms.Button
    Private WithEvents btnCheckAutoDenyNumber As System.Windows.Forms.Button
End Class
