﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtAccountNumber = New System.Windows.Forms.TextBox
        Me.btnCheckAccountInfo = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBankCode = New System.Windows.Forms.TextBox
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCorpNum = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox15 = New System.Windows.Forms.GroupBox
        Me.btnGetAccessURL = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btnGetPartnerURL = New System.Windows.Forms.Button
        Me.btnGetPartnerBalance = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnGetRefundHistory = New System.Windows.Forms.Button
        Me.btnGetUseHistoryURL = New System.Windows.Forms.Button
        Me.btnRefund = New System.Windows.Forms.Button
        Me.btnGetChargeInfo = New System.Windows.Forms.Button
        Me.btnGetPaymentURL = New System.Windows.Forms.Button
        Me.btnUnitCost = New System.Windows.Forms.Button
        Me.btnGetChargeURL = New System.Windows.Forms.Button
        Me.btnPaymentRequest = New System.Windows.Forms.Button
        Me.btnGetBalance = New System.Windows.Forms.Button
        Me.btnGetPaymentHistory = New System.Windows.Forms.Button
        Me.btnGetSettleResult = New System.Windows.Forms.Button
        Me.btnGetUseHistory = New System.Windows.Forms.Button
        Me.btnGetRefundableBalance = New System.Windows.Forms.Button
        Me.btnGetRefundInfo = New System.Windows.Forms.Button
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.btnUpdateCorpInfo = New System.Windows.Forms.Button
        Me.btnGetContactInfo = New System.Windows.Forms.Button
        Me.btnGetCorpInfo = New System.Windows.Forms.Button
        Me.btnCheckID = New System.Windows.Forms.Button
        Me.btnUpdateContact = New System.Windows.Forms.Button
        Me.btnCheckIsMember = New System.Windows.Forms.Button
        Me.btnListContact = New System.Windows.Forms.Button
        Me.btnJoinMember = New System.Windows.Forms.Button
        Me.btnRegistContact = New System.Windows.Forms.Button
        Me.btnQuitMember = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtIdentityNumTypeDC = New System.Windows.Forms.TextBox
        Me.txtIdentityNumDC = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtAccountNumberDC = New System.Windows.Forms.TextBox
        Me.btnCheckDepositorInfo = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtBankCodeDC = New System.Windows.Forms.TextBox
        Me.btnDeleteContact = New System.Windows.Forms.Button
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.txtAccountNumber)
        Me.GroupBox4.Controls.Add(Me.btnCheckAccountInfo)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.txtBankCode)
        Me.GroupBox4.Location = New System.Drawing.Point(66, 30)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(378, 85)
        Me.GroupBox4.TabIndex = 40
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "계좌성명조회 API"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "계좌번호 : "
        '
        'txtAccountNumber
        '
        Me.txtAccountNumber.Location = New System.Drawing.Point(84, 47)
        Me.txtAccountNumber.Name = "txtAccountNumber"
        Me.txtAccountNumber.Size = New System.Drawing.Size(155, 21)
        Me.txtAccountNumber.TabIndex = 1
        '
        'btnCheckAccountInfo
        '
        Me.btnCheckAccountInfo.Location = New System.Drawing.Point(248, 21)
        Me.btnCheckAccountInfo.Name = "btnCheckAccountInfo"
        Me.btnCheckAccountInfo.Size = New System.Drawing.Size(110, 49)
        Me.btnCheckAccountInfo.TabIndex = 2
        Me.btnCheckAccountInfo.Text = "계좌성명조회"
        Me.btnCheckAccountInfo.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "기관코드 : "
        '
        'txtBankCode
        '
        Me.txtBankCode.Location = New System.Drawing.Point(84, 23)
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(155, 21)
        Me.txtBankCode.TabIndex = 0
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(408, 16)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(143, 21)
        Me.txtUserId.TabIndex = 39
        Me.txtUserId.Text = "testkorea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(303, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 12)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "팝빌회원 아이디 :"
        '
        'txtCorpNum
        '
        Me.txtCorpNum.Location = New System.Drawing.Point(139, 17)
        Me.txtCorpNum.Name = "txtCorpNum"
        Me.txtCorpNum.Size = New System.Drawing.Size(143, 21)
        Me.txtCorpNum.TabIndex = 37
        Me.txtCorpNum.Text = "1234567890"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 12)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "팝빌회원 사업자번호 : "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox15)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 47)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(930, 323)
        Me.GroupBox1.TabIndex = 35
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "팝빌 기본 API"
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.btnGetAccessURL)
        Me.GroupBox15.Location = New System.Drawing.Point(746, 20)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(162, 162)
        Me.GroupBox15.TabIndex = 4
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "팝빌 기본 URL"
        '
        'btnGetAccessURL
        '
        Me.btnGetAccessURL.Location = New System.Drawing.Point(6, 18)
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
        Me.GroupBox5.Location = New System.Drawing.Point(609, 20)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(131, 161)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "파트너과금 포인트"
        '
        'btnGetPartnerURL
        '
        Me.btnGetPartnerURL.Location = New System.Drawing.Point(6, 50)
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
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnGetRefundHistory)
        Me.GroupBox3.Controls.Add(Me.btnGetUseHistoryURL)
        Me.GroupBox3.Controls.Add(Me.btnRefund)
        Me.GroupBox3.Controls.Add(Me.btnGetChargeInfo)
        Me.GroupBox3.Controls.Add(Me.btnGetPaymentURL)
        Me.GroupBox3.Controls.Add(Me.btnUnitCost)
        Me.GroupBox3.Controls.Add(Me.btnGetChargeURL)
        Me.GroupBox3.Controls.Add(Me.btnPaymentRequest)
        Me.GroupBox3.Controls.Add(Me.btnGetBalance)
        Me.GroupBox3.Controls.Add(Me.btnGetPaymentHistory)
        Me.GroupBox3.Controls.Add(Me.btnGetSettleResult)
        Me.GroupBox3.Controls.Add(Me.btnGetUseHistory)
        Me.GroupBox3.Controls.Add(Me.btnGetRefundableBalance)
        Me.GroupBox3.Controls.Add(Me.btnGetRefundInfo)
        Me.GroupBox3.Location = New System.Drawing.Point(280, 17)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(323, 287)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "포인트 관련"
        '
        'btnGetRefundHistory
        '
        Me.btnGetRefundHistory.Location = New System.Drawing.Point(178, 248)
        Me.btnGetRefundHistory.Name = "btnGetRefundHistory"
        Me.btnGetRefundHistory.Size = New System.Drawing.Size(135, 32)
        Me.btnGetRefundHistory.TabIndex = 0
        Me.btnGetRefundHistory.Text = "포인트 환불내역 확인"
        '
        'btnGetUseHistoryURL
        '
        Me.btnGetUseHistoryURL.Location = New System.Drawing.Point(178, 134)
        Me.btnGetUseHistoryURL.Name = "btnGetUseHistoryURL"
        Me.btnGetUseHistoryURL.Size = New System.Drawing.Size(135, 32)
        Me.btnGetUseHistoryURL.TabIndex = 7
        Me.btnGetUseHistoryURL.Text = "포인트 사용내역 URL"
        Me.btnGetUseHistoryURL.UseVisualStyleBackColor = True
        '
        'btnRefund
        '
        Me.btnRefund.Location = New System.Drawing.Point(6, 248)
        Me.btnRefund.Name = "btnRefund"
        Me.btnRefund.Size = New System.Drawing.Size(159, 32)
        Me.btnRefund.TabIndex = 0
        Me.btnRefund.Text = "포인트 환불신청"
        '
        'btnGetChargeInfo
        '
        Me.btnGetChargeInfo.Location = New System.Drawing.Point(6, 20)
        Me.btnGetChargeInfo.Name = "btnGetChargeInfo"
        Me.btnGetChargeInfo.Size = New System.Drawing.Size(159, 32)
        Me.btnGetChargeInfo.TabIndex = 4
        Me.btnGetChargeInfo.Text = "과금정보 확인"
        Me.btnGetChargeInfo.UseVisualStyleBackColor = True
        '
        'btnGetPaymentURL
        '
        Me.btnGetPaymentURL.Location = New System.Drawing.Point(178, 96)
        Me.btnGetPaymentURL.Name = "btnGetPaymentURL"
        Me.btnGetPaymentURL.Size = New System.Drawing.Size(135, 32)
        Me.btnGetPaymentURL.TabIndex = 6
        Me.btnGetPaymentURL.Text = "포인트 결제내역 URL"
        Me.btnGetPaymentURL.UseVisualStyleBackColor = True
        '
        'btnUnitCost
        '
        Me.btnUnitCost.Location = New System.Drawing.Point(6, 58)
        Me.btnUnitCost.Name = "btnUnitCost"
        Me.btnUnitCost.Size = New System.Drawing.Size(159, 32)
        Me.btnUnitCost.TabIndex = 3
        Me.btnUnitCost.Text = "요금 단가 확인"
        Me.btnUnitCost.UseVisualStyleBackColor = True
        '
        'btnGetChargeURL
        '
        Me.btnGetChargeURL.Location = New System.Drawing.Point(178, 58)
        Me.btnGetChargeURL.Name = "btnGetChargeURL"
        Me.btnGetChargeURL.Size = New System.Drawing.Size(135, 32)
        Me.btnGetChargeURL.TabIndex = 5
        Me.btnGetChargeURL.Text = "포인트 충전 URL"
        Me.btnGetChargeURL.UseVisualStyleBackColor = True
        '
        'btnPaymentRequest
        '
        Me.btnPaymentRequest.Location = New System.Drawing.Point(8, 96)
        Me.btnPaymentRequest.Name = "btnPaymentRequest"
        Me.btnPaymentRequest.Size = New System.Drawing.Size(159, 32)
        Me.btnPaymentRequest.TabIndex = 8
        Me.btnPaymentRequest.Text = "무통장 입금신청"
        '
        'btnGetBalance
        '
        Me.btnGetBalance.Location = New System.Drawing.Point(178, 20)
        Me.btnGetBalance.Name = "btnGetBalance"
        Me.btnGetBalance.Size = New System.Drawing.Size(135, 32)
        Me.btnGetBalance.TabIndex = 2
        Me.btnGetBalance.Text = "잔여포인트 확인"
        Me.btnGetBalance.UseVisualStyleBackColor = True
        '
        'btnGetPaymentHistory
        '
        Me.btnGetPaymentHistory.Location = New System.Drawing.Point(178, 172)
        Me.btnGetPaymentHistory.Name = "btnGetPaymentHistory"
        Me.btnGetPaymentHistory.Size = New System.Drawing.Size(135, 32)
        Me.btnGetPaymentHistory.TabIndex = 0
        Me.btnGetPaymentHistory.Text = "포인트 결제내역 확인"
        '
        'btnGetSettleResult
        '
        Me.btnGetSettleResult.Location = New System.Drawing.Point(6, 134)
        Me.btnGetSettleResult.Name = "btnGetSettleResult"
        Me.btnGetSettleResult.Size = New System.Drawing.Size(159, 32)
        Me.btnGetSettleResult.TabIndex = 0
        Me.btnGetSettleResult.Text = "무통장 입금신청 정보확인"
        '
        'btnGetUseHistory
        '
        Me.btnGetUseHistory.Location = New System.Drawing.Point(178, 210)
        Me.btnGetUseHistory.Name = "btnGetUseHistory"
        Me.btnGetUseHistory.Size = New System.Drawing.Size(135, 32)
        Me.btnGetUseHistory.TabIndex = 0
        Me.btnGetUseHistory.Text = "포인트 사용내역 확인"
        '
        'btnGetRefundableBalance
        '
        Me.btnGetRefundableBalance.Location = New System.Drawing.Point(6, 210)
        Me.btnGetRefundableBalance.Name = "btnGetRefundableBalance"
        Me.btnGetRefundableBalance.Size = New System.Drawing.Size(159, 32)
        Me.btnGetRefundableBalance.TabIndex = 0
        Me.btnGetRefundableBalance.Text = "환불 가능 포인트 조회"
        '
        'btnGetRefundInfo
        '
        Me.btnGetRefundInfo.Location = New System.Drawing.Point(6, 172)
        Me.btnGetRefundInfo.Name = "btnGetRefundInfo"
        Me.btnGetRefundInfo.Size = New System.Drawing.Size(159, 32)
        Me.btnGetRefundInfo.TabIndex = 0
        Me.btnGetRefundInfo.Text = "환불 신청 상태 조회"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.btnDeleteContact)
        Me.GroupBox6.Controls.Add(Me.btnUpdateCorpInfo)
        Me.GroupBox6.Controls.Add(Me.btnGetContactInfo)
        Me.GroupBox6.Controls.Add(Me.btnGetCorpInfo)
        Me.GroupBox6.Controls.Add(Me.btnCheckID)
        Me.GroupBox6.Controls.Add(Me.btnUpdateContact)
        Me.GroupBox6.Controls.Add(Me.btnCheckIsMember)
        Me.GroupBox6.Controls.Add(Me.btnListContact)
        Me.GroupBox6.Controls.Add(Me.btnJoinMember)
        Me.GroupBox6.Controls.Add(Me.btnRegistContact)
        Me.GroupBox6.Controls.Add(Me.btnQuitMember)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 17)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(268, 258)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "회원 정보"
        '
        'btnUpdateCorpInfo
        '
        Me.btnUpdateCorpInfo.Location = New System.Drawing.Point(7, 210)
        Me.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo"
        Me.btnUpdateCorpInfo.Size = New System.Drawing.Size(120, 32)
        Me.btnUpdateCorpInfo.TabIndex = 8
        Me.btnUpdateCorpInfo.Text = "회사정보 수정"
        Me.btnUpdateCorpInfo.UseVisualStyleBackColor = True
        '
        'btnGetContactInfo
        '
        Me.btnGetContactInfo.Location = New System.Drawing.Point(132, 58)
        Me.btnGetContactInfo.Name = "btnGetContactInfo"
        Me.btnGetContactInfo.Size = New System.Drawing.Size(120, 32)
        Me.btnGetContactInfo.TabIndex = 8
        Me.btnGetContactInfo.Text = "담당자 정보 확인"
        Me.btnGetContactInfo.UseVisualStyleBackColor = True
        '
        'btnGetCorpInfo
        '
        Me.btnGetCorpInfo.Location = New System.Drawing.Point(6, 172)
        Me.btnGetCorpInfo.Name = "btnGetCorpInfo"
        Me.btnGetCorpInfo.Size = New System.Drawing.Size(120, 32)
        Me.btnGetCorpInfo.TabIndex = 7
        Me.btnGetCorpInfo.Text = "회사정보 조회"
        Me.btnGetCorpInfo.UseVisualStyleBackColor = True
        '
        'btnCheckID
        '
        Me.btnCheckID.Location = New System.Drawing.Point(6, 58)
        Me.btnCheckID.Name = "btnCheckID"
        Me.btnCheckID.Size = New System.Drawing.Size(120, 32)
        Me.btnCheckID.TabIndex = 3
        Me.btnCheckID.Text = "ID 중복 확인"
        Me.btnCheckID.UseVisualStyleBackColor = True
        '
        'btnUpdateContact
        '
        Me.btnUpdateContact.Location = New System.Drawing.Point(130, 134)
        Me.btnUpdateContact.Name = "btnUpdateContact"
        Me.btnUpdateContact.Size = New System.Drawing.Size(120, 32)
        Me.btnUpdateContact.TabIndex = 7
        Me.btnUpdateContact.Text = "담당자 정보 수정"
        Me.btnUpdateContact.UseVisualStyleBackColor = True
        '
        'btnCheckIsMember
        '
        Me.btnCheckIsMember.Location = New System.Drawing.Point(6, 19)
        Me.btnCheckIsMember.Name = "btnCheckIsMember"
        Me.btnCheckIsMember.Size = New System.Drawing.Size(120, 32)
        Me.btnCheckIsMember.TabIndex = 2
        Me.btnCheckIsMember.Text = "가입여부 확인"
        Me.btnCheckIsMember.UseVisualStyleBackColor = True
        '
        'btnListContact
        '
        Me.btnListContact.Location = New System.Drawing.Point(130, 96)
        Me.btnListContact.Name = "btnListContact"
        Me.btnListContact.Size = New System.Drawing.Size(120, 32)
        Me.btnListContact.TabIndex = 6
        Me.btnListContact.Text = "담당자 목록 조회"
        Me.btnListContact.UseVisualStyleBackColor = True
        '
        'btnJoinMember
        '
        Me.btnJoinMember.Location = New System.Drawing.Point(6, 96)
        Me.btnJoinMember.Name = "btnJoinMember"
        Me.btnJoinMember.Size = New System.Drawing.Size(120, 32)
        Me.btnJoinMember.TabIndex = 1
        Me.btnJoinMember.Text = "회원 가입"
        Me.btnJoinMember.UseVisualStyleBackColor = True
        '
        'btnRegistContact
        '
        Me.btnRegistContact.Location = New System.Drawing.Point(132, 20)
        Me.btnRegistContact.Name = "btnRegistContact"
        Me.btnRegistContact.Size = New System.Drawing.Size(120, 32)
        Me.btnRegistContact.TabIndex = 5
        Me.btnRegistContact.Text = "담당자 추가"
        Me.btnRegistContact.UseVisualStyleBackColor = True
        '
        'btnQuitMember
        '
        Me.btnQuitMember.Location = New System.Drawing.Point(6, 134)
        Me.btnQuitMember.Name = "btnQuitMember"
        Me.btnQuitMember.Size = New System.Drawing.Size(120, 32)
        Me.btnQuitMember.TabIndex = 23
        Me.btnQuitMember.Text = "팝빌 회원 탈퇴"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(569, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 12)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "응답 URL :"
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(639, 16)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(271, 21)
        Me.txtURL.TabIndex = 5
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.GroupBox8)
        Me.GroupBox7.Controls.Add(Me.GroupBox4)
        Me.GroupBox7.Location = New System.Drawing.Point(12, 376)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(926, 181)
        Me.GroupBox7.TabIndex = 42
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "예금주 조회 API"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label9)
        Me.GroupBox8.Controls.Add(Me.txtIdentityNumTypeDC)
        Me.GroupBox8.Controls.Add(Me.txtIdentityNumDC)
        Me.GroupBox8.Controls.Add(Me.Label8)
        Me.GroupBox8.Controls.Add(Me.Label6)
        Me.GroupBox8.Controls.Add(Me.txtAccountNumberDC)
        Me.GroupBox8.Controls.Add(Me.btnCheckDepositorInfo)
        Me.GroupBox8.Controls.Add(Me.Label7)
        Me.GroupBox8.Controls.Add(Me.txtBankCodeDC)
        Me.GroupBox8.Location = New System.Drawing.Point(471, 20)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(378, 139)
        Me.GroupBox8.TabIndex = 41
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "계좌실명조회 API"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 106)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 12)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "실명번호  유형 : "
        '
        'txtIdentityNumTypeDC
        '
        Me.txtIdentityNumTypeDC.Location = New System.Drawing.Point(112, 101)
        Me.txtIdentityNumTypeDC.Name = "txtIdentityNumTypeDC"
        Me.txtIdentityNumTypeDC.Size = New System.Drawing.Size(127, 21)
        Me.txtIdentityNumTypeDC.TabIndex = 3
        Me.txtIdentityNumTypeDC.Text = "P"
        '
        'txtIdentityNumDC
        '
        Me.txtIdentityNumDC.Location = New System.Drawing.Point(84, 74)
        Me.txtIdentityNumDC.Name = "txtIdentityNumDC"
        Me.txtIdentityNumDC.Size = New System.Drawing.Size(155, 21)
        Me.txtIdentityNumDC.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 12)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "실명번호 : "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "계좌번호 : "
        '
        'txtAccountNumberDC
        '
        Me.txtAccountNumberDC.Location = New System.Drawing.Point(84, 47)
        Me.txtAccountNumberDC.Name = "txtAccountNumberDC"
        Me.txtAccountNumberDC.Size = New System.Drawing.Size(155, 21)
        Me.txtAccountNumberDC.TabIndex = 1
        '
        'btnCheckDepositorInfo
        '
        Me.btnCheckDepositorInfo.Location = New System.Drawing.Point(248, 21)
        Me.btnCheckDepositorInfo.Name = "btnCheckDepositorInfo"
        Me.btnCheckDepositorInfo.Size = New System.Drawing.Size(110, 49)
        Me.btnCheckDepositorInfo.TabIndex = 4
        Me.btnCheckDepositorInfo.Text = "계좌실명조회"
        Me.btnCheckDepositorInfo.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 12)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "기관코드 : "
        '
        'txtBankCodeDC
        '
        Me.txtBankCodeDC.Location = New System.Drawing.Point(84, 23)
        Me.txtBankCodeDC.Name = "txtBankCodeDC"
        Me.txtBankCodeDC.Size = New System.Drawing.Size(155, 21)
        Me.txtBankCodeDC.TabIndex = 0
        '
        'btnDeleteContact
        '
        Me.btnDeleteContact.Location = New System.Drawing.Point(130, 172)
        Me.btnDeleteContact.Name = "btnDeleteContact"
        Me.btnDeleteContact.Size = New System.Drawing.Size(120, 32)
        Me.btnDeleteContact.TabIndex = 24
        Me.btnDeleteContact.Text = "담당자 삭제"
        Me.btnDeleteContact.UseVisualStyleBackColor = True
        '
        'frmExample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(956, 613)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCorpNum)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmExample"
        Me.Text = "팝빌 예금주조회 API VB.Net SDK Example"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCheckAccountInfo As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBankCode As System.Windows.Forms.TextBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCorpNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateCorpInfo As System.Windows.Forms.Button
    Friend WithEvents btnGetCorpInfo As System.Windows.Forms.Button
    Friend WithEvents btnUpdateContact As System.Windows.Forms.Button
    Friend WithEvents btnListContact As System.Windows.Forms.Button
    Friend WithEvents btnRegistContact As System.Windows.Forms.Button
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetAccessURL As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetPartnerURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPartnerBalance As System.Windows.Forms.Button
    Friend WithEvents btnGetChargeURL As System.Windows.Forms.Button
    Friend WithEvents btnGetBalance As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetChargeInfo As System.Windows.Forms.Button
    Friend WithEvents btnUnitCost As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCheckID As System.Windows.Forms.Button
    Friend WithEvents btnCheckIsMember As System.Windows.Forms.Button
    Friend WithEvents btnJoinMember As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAccountNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents btnGetUseHistoryURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPaymentURL As System.Windows.Forms.Button
    Friend WithEvents btnGetContactInfo As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAccountNumberDC As System.Windows.Forms.TextBox
    Friend WithEvents btnCheckDepositorInfo As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBankCodeDC As System.Windows.Forms.TextBox
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents txtIdentityNumTypeDC As System.Windows.Forms.TextBox
    Private WithEvents txtIdentityNumDC As System.Windows.Forms.TextBox
    Private WithEvents btnPaymentRequest As System.Windows.Forms.Button
    Private WithEvents btnGetSettleResult As System.Windows.Forms.Button
    Private WithEvents btnGetPaymentHistory As System.Windows.Forms.Button
    Private WithEvents btnGetUseHistory As System.Windows.Forms.Button
    Private WithEvents btnRefund As System.Windows.Forms.Button
    Private WithEvents btnGetRefundHistory As System.Windows.Forms.Button
    Private WithEvents btnGetRefundableBalance As System.Windows.Forms.Button
    Private WithEvents btnGetRefundInfo As System.Windows.Forms.Button
    Private WithEvents btnQuitMember As System.Windows.Forms.Button
    Friend WithEvents btnDeleteContact As System.Windows.Forms.Button
End Class
