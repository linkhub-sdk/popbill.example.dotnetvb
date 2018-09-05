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
        Me.btnCancelReserve = New System.Windows.Forms.Button
        Me.btnGetURL = New System.Windows.Forms.Button
        Me.txtReserveDT = New System.Windows.Forms.TextBox
        Me.txtReceiptNum = New System.Windows.Forms.TextBox
        Me.label4 = New System.Windows.Forms.Label
        Me.btnGetFaxResult = New System.Windows.Forms.Button
        Me.label3 = New System.Windows.Forms.Label
        Me.groupBox4 = New System.Windows.Forms.GroupBox
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.btnResendFAXRN_same = New System.Windows.Forms.Button
        Me.btnResendFAX_Multi = New System.Windows.Forms.Button
        Me.btnResendFAXRN = New System.Windows.Forms.Button
        Me.btnResendFAX = New System.Windows.Forms.Button
        Me.btnCancelReserveRN = New System.Windows.Forms.Button
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.getURL_SENDER = New System.Windows.Forms.Button
        Me.btnGetSenderNumberList = New System.Windows.Forms.Button
        Me.btnGetFaxResultRN = New System.Windows.Forms.Button
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtRequestNum = New System.Windows.Forms.TextBox
        Me.btnSenFax_4 = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnSenFax_3 = New System.Windows.Forms.Button
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.btnSenFax_2 = New System.Windows.Forms.Button
        Me.btnSenFax_1 = New System.Windows.Forms.Button
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.fileDialog = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox17 = New System.Windows.Forms.GroupBox
        Me.btnUpdateCorpInfo = New System.Windows.Forms.Button
        Me.btnGetCorpInfo = New System.Windows.Forms.Button
        Me.GroupBox16 = New System.Windows.Forms.GroupBox
        Me.btnUpdateContact = New System.Windows.Forms.Button
        Me.btnListContact = New System.Windows.Forms.Button
        Me.btnRegistContact = New System.Windows.Forms.Button
        Me.GroupBox15 = New System.Windows.Forms.GroupBox
        Me.btnGetPopbillURL_LOGIN = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btnGetPartnerURL = New System.Windows.Forms.Button
        Me.btnGetPartnerBalance = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnGetPopbillURL_CHRG = New System.Windows.Forms.Button
        Me.btnGetBalance = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnGetChargeInfo = New System.Windows.Forms.Button
        Me.btnUnitCost = New System.Windows.Forms.Button
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.btnCheckID = New System.Windows.Forms.Button
        Me.btnCheckIsMember = New System.Windows.Forms.Button
        Me.btnJoinMember = New System.Windows.Forms.Button
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCorpNum = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.groupBox4.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancelReserve
        '
        Me.btnCancelReserve.Location = New System.Drawing.Point(145, 133)
        Me.btnCancelReserve.Name = "btnCancelReserve"
        Me.btnCancelReserve.Size = New System.Drawing.Size(121, 34)
        Me.btnCancelReserve.TabIndex = 22
        Me.btnCancelReserve.Text = "예약 전송 취소"
        Me.btnCancelReserve.UseVisualStyleBackColor = True
        '
        'btnGetURL
        '
        Me.btnGetURL.Location = New System.Drawing.Point(11, 60)
        Me.btnGetURL.Name = "btnGetURL"
        Me.btnGetURL.Size = New System.Drawing.Size(121, 32)
        Me.btnGetURL.TabIndex = 20
        Me.btnGetURL.Text = "전송내역조회 팝업"
        Me.btnGetURL.UseVisualStyleBackColor = True
        '
        'txtReserveDT
        '
        Me.txtReserveDT.Location = New System.Drawing.Point(203, 23)
        Me.txtReserveDT.Name = "txtReserveDT"
        Me.txtReserveDT.Size = New System.Drawing.Size(168, 21)
        Me.txtReserveDT.TabIndex = 14
        '
        'txtReceiptNum
        '
        Me.txtReceiptNum.Location = New System.Drawing.Point(86, 108)
        Me.txtReceiptNum.Name = "txtReceiptNum"
        Me.txtReceiptNum.Size = New System.Drawing.Size(173, 21)
        Me.txtReceiptNum.TabIndex = 17
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(22, 112)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(65, 12)
        Me.label4.TabIndex = 16
        Me.label4.Text = "접수번호 : "
        '
        'btnGetFaxResult
        '
        Me.btnGetFaxResult.Location = New System.Drawing.Point(18, 133)
        Me.btnGetFaxResult.Name = "btnGetFaxResult"
        Me.btnGetFaxResult.Size = New System.Drawing.Size(121, 34)
        Me.btnGetFaxResult.TabIndex = 21
        Me.btnGetFaxResult.Text = "전송상태확인"
        Me.btnGetFaxResult.UseVisualStyleBackColor = True
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(17, 29)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(191, 12)
        Me.label3.TabIndex = 13
        Me.label3.Text = "예약시간(yyyyMMddHHmmss) : "
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.ListBox1)
        Me.groupBox4.Controls.Add(Me.btnResendFAXRN_same)
        Me.groupBox4.Controls.Add(Me.btnResendFAX_Multi)
        Me.groupBox4.Controls.Add(Me.btnResendFAXRN)
        Me.groupBox4.Controls.Add(Me.btnResendFAX)
        Me.groupBox4.Controls.Add(Me.btnCancelReserveRN)
        Me.groupBox4.Controls.Add(Me.GroupBox8)
        Me.groupBox4.Controls.Add(Me.btnGetFaxResultRN)
        Me.groupBox4.Controls.Add(Me.GroupBox7)
        Me.groupBox4.Controls.Add(Me.txtRequestNum)
        Me.groupBox4.Controls.Add(Me.btnSenFax_4)
        Me.groupBox4.Controls.Add(Me.Label5)
        Me.groupBox4.Controls.Add(Me.btnSenFax_3)
        Me.groupBox4.Controls.Add(Me.GroupBox10)
        Me.groupBox4.Controls.Add(Me.btnSenFax_2)
        Me.groupBox4.Controls.Add(Me.btnSenFax_1)
        Me.groupBox4.Controls.Add(Me.btnCancelReserve)
        Me.groupBox4.Controls.Add(Me.btnGetFaxResult)
        Me.groupBox4.Controls.Add(Me.txtReceiptNum)
        Me.groupBox4.Controls.Add(Me.label4)
        Me.groupBox4.Controls.Add(Me.txtReserveDT)
        Me.groupBox4.Controls.Add(Me.label3)
        Me.groupBox4.Controls.Add(Me.GroupBox9)
        Me.groupBox4.Location = New System.Drawing.Point(15, 217)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(1015, 449)
        Me.groupBox4.TabIndex = 23
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "팩스전송 관련 기능"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.HorizontalScrollbar = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Location = New System.Drawing.Point(14, 219)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(987, 220)
        Me.ListBox1.TabIndex = 45
        '
        'btnResendFAXRN_same
        '
        Me.btnResendFAXRN_same.Location = New System.Drawing.Point(415, 172)
        Me.btnResendFAXRN_same.Name = "btnResendFAXRN_same"
        Me.btnResendFAXRN_same.Size = New System.Drawing.Size(121, 32)
        Me.btnResendFAXRN_same.TabIndex = 43
        Me.btnResendFAXRN_same.Text = "동보 재전송"
        Me.btnResendFAXRN_same.UseVisualStyleBackColor = True
        '
        'btnResendFAX_Multi
        '
        Me.btnResendFAX_Multi.Location = New System.Drawing.Point(145, 172)
        Me.btnResendFAX_Multi.Name = "btnResendFAX_Multi"
        Me.btnResendFAX_Multi.Size = New System.Drawing.Size(121, 32)
        Me.btnResendFAX_Multi.TabIndex = 36
        Me.btnResendFAX_Multi.Text = "동보 재전송"
        Me.btnResendFAX_Multi.UseVisualStyleBackColor = True
        '
        'btnResendFAXRN
        '
        Me.btnResendFAXRN.Location = New System.Drawing.Point(288, 171)
        Me.btnResendFAXRN.Name = "btnResendFAXRN"
        Me.btnResendFAXRN.Size = New System.Drawing.Size(121, 32)
        Me.btnResendFAXRN.TabIndex = 42
        Me.btnResendFAXRN.Text = "재전송"
        Me.btnResendFAXRN.UseVisualStyleBackColor = True
        '
        'btnResendFAX
        '
        Me.btnResendFAX.Location = New System.Drawing.Point(18, 171)
        Me.btnResendFAX.Name = "btnResendFAX"
        Me.btnResendFAX.Size = New System.Drawing.Size(121, 32)
        Me.btnResendFAX.TabIndex = 35
        Me.btnResendFAX.Text = "재전송"
        Me.btnResendFAX.UseVisualStyleBackColor = True
        '
        'btnCancelReserveRN
        '
        Me.btnCancelReserveRN.Location = New System.Drawing.Point(415, 133)
        Me.btnCancelReserveRN.Name = "btnCancelReserveRN"
        Me.btnCancelReserveRN.Size = New System.Drawing.Size(121, 34)
        Me.btnCancelReserveRN.TabIndex = 40
        Me.btnCancelReserveRN.Text = "예약 전송 취소"
        Me.btnCancelReserveRN.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.getURL_SENDER)
        Me.GroupBox8.Controls.Add(Me.btnGetSenderNumberList)
        Me.GroupBox8.Location = New System.Drawing.Point(767, 23)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(136, 103)
        Me.GroupBox8.TabIndex = 34
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "발신번호 관리"
        '
        'getURL_SENDER
        '
        Me.getURL_SENDER.Location = New System.Drawing.Point(8, 60)
        Me.getURL_SENDER.Name = "getURL_SENDER"
        Me.getURL_SENDER.Size = New System.Drawing.Size(121, 32)
        Me.getURL_SENDER.TabIndex = 22
        Me.getURL_SENDER.Text = "발신번호 관리 팝업"
        Me.getURL_SENDER.UseVisualStyleBackColor = True
        '
        'btnGetSenderNumberList
        '
        Me.btnGetSenderNumberList.Location = New System.Drawing.Point(8, 22)
        Me.btnGetSenderNumberList.Name = "btnGetSenderNumberList"
        Me.btnGetSenderNumberList.Size = New System.Drawing.Size(121, 32)
        Me.btnGetSenderNumberList.TabIndex = 21
        Me.btnGetSenderNumberList.Text = "발신번호 목록 조회"
        Me.btnGetSenderNumberList.UseVisualStyleBackColor = True
        '
        'btnGetFaxResultRN
        '
        Me.btnGetFaxResultRN.Location = New System.Drawing.Point(288, 133)
        Me.btnGetFaxResultRN.Name = "btnGetFaxResultRN"
        Me.btnGetFaxResultRN.Size = New System.Drawing.Size(121, 34)
        Me.btnGetFaxResultRN.TabIndex = 39
        Me.btnGetFaxResultRN.Text = "전송상태확인"
        Me.btnGetFaxResultRN.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.btnGetURL)
        Me.GroupBox7.Controls.Add(Me.btnSearch)
        Me.GroupBox7.Location = New System.Drawing.Point(620, 23)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(138, 103)
        Me.GroupBox7.TabIndex = 33
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "전송내역 관리"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(11, 22)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(121, 32)
        Me.btnSearch.TabIndex = 32
        Me.btnSearch.Text = "전송내역 기간조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtRequestNum
        '
        Me.txtRequestNum.Location = New System.Drawing.Point(356, 108)
        Me.txtRequestNum.Name = "txtRequestNum"
        Me.txtRequestNum.Size = New System.Drawing.Size(173, 21)
        Me.txtRequestNum.TabIndex = 38
        '
        'btnSenFax_4
        '
        Me.btnSenFax_4.Location = New System.Drawing.Point(297, 50)
        Me.btnSenFax_4.Name = "btnSenFax_4"
        Me.btnSenFax_4.Size = New System.Drawing.Size(113, 32)
        Me.btnSenFax_4.TabIndex = 31
        Me.btnSenFax_4.Text = "다수파일 동보전송"
        Me.btnSenFax_4.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(292, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "요청번호 : "
        '
        'btnSenFax_3
        '
        Me.btnSenFax_3.Location = New System.Drawing.Point(193, 50)
        Me.btnSenFax_3.Name = "btnSenFax_3"
        Me.btnSenFax_3.Size = New System.Drawing.Size(98, 32)
        Me.btnSenFax_3.TabIndex = 30
        Me.btnSenFax_3.Text = "다수 파일 전송"
        Me.btnSenFax_3.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Location = New System.Drawing.Point(284, 88)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(264, 125)
        Me.GroupBox10.TabIndex = 41
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "요청번호 할당 전송건 처리"
        '
        'btnSenFax_2
        '
        Me.btnSenFax_2.Location = New System.Drawing.Point(99, 50)
        Me.btnSenFax_2.Name = "btnSenFax_2"
        Me.btnSenFax_2.Size = New System.Drawing.Size(88, 32)
        Me.btnSenFax_2.TabIndex = 29
        Me.btnSenFax_2.Text = "동보 전송"
        Me.btnSenFax_2.UseVisualStyleBackColor = True
        '
        'btnSenFax_1
        '
        Me.btnSenFax_1.Location = New System.Drawing.Point(14, 49)
        Me.btnSenFax_1.Name = "btnSenFax_1"
        Me.btnSenFax_1.Size = New System.Drawing.Size(80, 32)
        Me.btnSenFax_1.TabIndex = 28
        Me.btnSenFax_1.Text = "전송"
        Me.btnSenFax_1.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Location = New System.Drawing.Point(14, 88)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(264, 125)
        Me.GroupBox9.TabIndex = 34
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "접수번호 관련 기능 (요청번호 미할당)"
        '
        'fileDialog
        '
        Me.fileDialog.FileName = "OpenFileDialog1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox17)
        Me.GroupBox1.Controls.Add(Me.GroupBox16)
        Me.GroupBox1.Controls.Add(Me.GroupBox15)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1015, 155)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "팝빌 기본 API"
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.btnUpdateCorpInfo)
        Me.GroupBox17.Controls.Add(Me.btnGetCorpInfo)
        Me.GroupBox17.Location = New System.Drawing.Point(873, 17)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(134, 125)
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
        Me.btnGetCorpInfo.Location = New System.Drawing.Point(6, 18)
        Me.btnGetCorpInfo.Name = "btnGetCorpInfo"
        Me.btnGetCorpInfo.Size = New System.Drawing.Size(122, 30)
        Me.btnGetCorpInfo.TabIndex = 7
        Me.btnGetCorpInfo.Text = "회사정보 조회"
        Me.btnGetCorpInfo.UseVisualStyleBackColor = True
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.btnUpdateContact)
        Me.GroupBox16.Controls.Add(Me.btnListContact)
        Me.GroupBox16.Controls.Add(Me.btnRegistContact)
        Me.GroupBox16.Location = New System.Drawing.Point(728, 17)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(138, 126)
        Me.GroupBox16.TabIndex = 5
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "담당자 관련"
        '
        'btnUpdateContact
        '
        Me.btnUpdateContact.Location = New System.Drawing.Point(8, 84)
        Me.btnUpdateContact.Name = "btnUpdateContact"
        Me.btnUpdateContact.Size = New System.Drawing.Size(122, 30)
        Me.btnUpdateContact.TabIndex = 7
        Me.btnUpdateContact.Text = "담당자 정보 수정"
        Me.btnUpdateContact.UseVisualStyleBackColor = True
        '
        'btnListContact
        '
        Me.btnListContact.Location = New System.Drawing.Point(8, 52)
        Me.btnListContact.Name = "btnListContact"
        Me.btnListContact.Size = New System.Drawing.Size(122, 30)
        Me.btnListContact.TabIndex = 6
        Me.btnListContact.Text = "담당자 목록 조회"
        Me.btnListContact.UseVisualStyleBackColor = True
        '
        'btnRegistContact
        '
        Me.btnRegistContact.Location = New System.Drawing.Point(8, 20)
        Me.btnRegistContact.Name = "btnRegistContact"
        Me.btnRegistContact.Size = New System.Drawing.Size(122, 30)
        Me.btnRegistContact.TabIndex = 5
        Me.btnRegistContact.Text = "담당자 추가"
        Me.btnRegistContact.UseVisualStyleBackColor = True
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.btnGetPopbillURL_LOGIN)
        Me.GroupBox15.Location = New System.Drawing.Point(560, 17)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(162, 126)
        Me.GroupBox15.TabIndex = 4
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "팝빌 기본 URL"
        '
        'btnGetPopbillURL_LOGIN
        '
        Me.btnGetPopbillURL_LOGIN.Location = New System.Drawing.Point(6, 19)
        Me.btnGetPopbillURL_LOGIN.Name = "btnGetPopbillURL_LOGIN"
        Me.btnGetPopbillURL_LOGIN.Size = New System.Drawing.Size(150, 30)
        Me.btnGetPopbillURL_LOGIN.TabIndex = 6
        Me.btnGetPopbillURL_LOGIN.Text = "팝빌 로그인 URL"
        Me.btnGetPopbillURL_LOGIN.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnGetPartnerURL)
        Me.GroupBox5.Controls.Add(Me.btnGetPartnerBalance)
        Me.GroupBox5.Location = New System.Drawing.Point(423, 17)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(131, 125)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "파트너과금 포인트"
        '
        'btnGetPartnerURL
        '
        Me.btnGetPartnerURL.Location = New System.Drawing.Point(6, 51)
        Me.btnGetPartnerURL.Name = "btnGetPartnerURL"
        Me.btnGetPartnerURL.Size = New System.Drawing.Size(118, 30)
        Me.btnGetPartnerURL.TabIndex = 6
        Me.btnGetPartnerURL.Text = "포인트 충전 URL"
        Me.btnGetPartnerURL.UseVisualStyleBackColor = True
        '
        'btnGetPartnerBalance
        '
        Me.btnGetPartnerBalance.Location = New System.Drawing.Point(6, 18)
        Me.btnGetPartnerBalance.Name = "btnGetPartnerBalance"
        Me.btnGetPartnerBalance.Size = New System.Drawing.Size(118, 31)
        Me.btnGetPartnerBalance.TabIndex = 3
        Me.btnGetPartnerBalance.Text = "파트너포인트 확인"
        Me.btnGetPartnerBalance.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnGetPopbillURL_CHRG)
        Me.GroupBox2.Controls.Add(Me.btnGetBalance)
        Me.GroupBox2.Location = New System.Drawing.Point(284, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(131, 125)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "연동과금 포인트"
        '
        'btnGetPopbillURL_CHRG
        '
        Me.btnGetPopbillURL_CHRG.Location = New System.Drawing.Point(6, 50)
        Me.btnGetPopbillURL_CHRG.Name = "btnGetPopbillURL_CHRG"
        Me.btnGetPopbillURL_CHRG.Size = New System.Drawing.Size(118, 30)
        Me.btnGetPopbillURL_CHRG.TabIndex = 5
        Me.btnGetPopbillURL_CHRG.Text = "포인트 충전 URL"
        Me.btnGetPopbillURL_CHRG.UseVisualStyleBackColor = True
        '
        'btnGetBalance
        '
        Me.btnGetBalance.Location = New System.Drawing.Point(7, 19)
        Me.btnGetBalance.Name = "btnGetBalance"
        Me.btnGetBalance.Size = New System.Drawing.Size(118, 29)
        Me.btnGetBalance.TabIndex = 2
        Me.btnGetBalance.Text = "잔여포인트 확인"
        Me.btnGetBalance.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnGetChargeInfo)
        Me.GroupBox3.Controls.Add(Me.btnUnitCost)
        Me.GroupBox3.Location = New System.Drawing.Point(145, 17)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(131, 125)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "포인트 관련"
        '
        'btnGetChargeInfo
        '
        Me.btnGetChargeInfo.Location = New System.Drawing.Point(6, 19)
        Me.btnGetChargeInfo.Name = "btnGetChargeInfo"
        Me.btnGetChargeInfo.Size = New System.Drawing.Size(118, 30)
        Me.btnGetChargeInfo.TabIndex = 4
        Me.btnGetChargeInfo.Text = "과금정보 확인"
        Me.btnGetChargeInfo.UseVisualStyleBackColor = True
        '
        'btnUnitCost
        '
        Me.btnUnitCost.Location = New System.Drawing.Point(6, 51)
        Me.btnUnitCost.Name = "btnUnitCost"
        Me.btnUnitCost.Size = New System.Drawing.Size(118, 29)
        Me.btnUnitCost.TabIndex = 3
        Me.btnUnitCost.Text = "요금 단가 확인"
        Me.btnUnitCost.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.btnCheckID)
        Me.GroupBox6.Controls.Add(Me.btnCheckIsMember)
        Me.GroupBox6.Controls.Add(Me.btnJoinMember)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 17)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(131, 125)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "회원 정보"
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
        Me.btnCheckIsMember.Size = New System.Drawing.Size(118, 29)
        Me.btnCheckIsMember.TabIndex = 2
        Me.btnCheckIsMember.Text = "가입여부 확인"
        Me.btnCheckIsMember.UseVisualStyleBackColor = True
        '
        'btnJoinMember
        '
        Me.btnJoinMember.Location = New System.Drawing.Point(6, 84)
        Me.btnJoinMember.Name = "btnJoinMember"
        Me.btnJoinMember.Size = New System.Drawing.Size(118, 31)
        Me.btnJoinMember.TabIndex = 1
        Me.btnJoinMember.Text = "회원 가입"
        Me.btnJoinMember.UseVisualStyleBackColor = True
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(417, 11)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(143, 21)
        Me.txtUserId.TabIndex = 27
        Me.txtUserId.Text = "testkorea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(312, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 12)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "팝빌회원 아이디 :"
        '
        'txtCorpNum
        '
        Me.txtCorpNum.Location = New System.Drawing.Point(148, 12)
        Me.txtCorpNum.Name = "txtCorpNum"
        Me.txtCorpNum.Size = New System.Drawing.Size(143, 21)
        Me.txtCorpNum.TabIndex = 25
        Me.txtCorpNum.Text = "1234567890"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 12)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "팝빌회원 사업자번호 : "
        '
        'frmExample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1047, 678)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCorpNum)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.groupBox4)
        Me.Name = "frmExample"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "팝빌 팩스 SDK VB.NET Example"
        Me.groupBox4.ResumeLayout(False)
        Me.groupBox4.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents btnCancelReserve As System.Windows.Forms.Button
    Private WithEvents btnGetURL As System.Windows.Forms.Button
    Friend WithEvents txtReserveDT As System.Windows.Forms.TextBox
    Friend WithEvents txtReceiptNum As System.Windows.Forms.TextBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents btnGetFaxResult As System.Windows.Forms.Button
    Friend WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents groupBox4 As System.Windows.Forms.GroupBox
    Private WithEvents btnSenFax_4 As System.Windows.Forms.Button
    Private WithEvents btnSenFax_3 As System.Windows.Forms.Button
    Private WithEvents btnSenFax_2 As System.Windows.Forms.Button
    Private WithEvents btnSenFax_1 As System.Windows.Forms.Button
    Friend WithEvents fileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox17 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateCorpInfo As System.Windows.Forms.Button
    Friend WithEvents btnGetCorpInfo As System.Windows.Forms.Button
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateContact As System.Windows.Forms.Button
    Friend WithEvents btnListContact As System.Windows.Forms.Button
    Friend WithEvents btnRegistContact As System.Windows.Forms.Button
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetPopbillURL_LOGIN As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetPartnerURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPartnerBalance As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetPopbillURL_CHRG As System.Windows.Forms.Button
    Friend WithEvents btnGetBalance As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetChargeInfo As System.Windows.Forms.Button
    Friend WithEvents btnUnitCost As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCheckID As System.Windows.Forms.Button
    Friend WithEvents btnCheckIsMember As System.Windows.Forms.Button
    Friend WithEvents btnJoinMember As System.Windows.Forms.Button
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCorpNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Private WithEvents getURL_SENDER As System.Windows.Forms.Button
    Private WithEvents btnGetSenderNumberList As System.Windows.Forms.Button
    Private WithEvents btnResendFAX_Multi As System.Windows.Forms.Button
    Private WithEvents btnResendFAX As System.Windows.Forms.Button
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Private WithEvents btnResendFAXRN_same As System.Windows.Forms.Button
    Private WithEvents btnResendFAXRN As System.Windows.Forms.Button
    Private WithEvents btnCancelReserveRN As System.Windows.Forms.Button
    Private WithEvents btnGetFaxResultRN As System.Windows.Forms.Button
    Friend WithEvents txtRequestNum As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox

End Class
