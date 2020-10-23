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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox17 = New System.Windows.Forms.GroupBox
        Me.btnUpdateCorpInfo = New System.Windows.Forms.Button
        Me.btnGetCorpInfo = New System.Windows.Forms.Button
        Me.GroupBox16 = New System.Windows.Forms.GroupBox
        Me.btnUpdateContact = New System.Windows.Forms.Button
        Me.btnListContact = New System.Windows.Forms.Button
        Me.btnRegistContact = New System.Windows.Forms.Button
        Me.GroupBox15 = New System.Windows.Forms.GroupBox
        Me.btnGetAccessURL = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btnGetPartnerURL = New System.Windows.Forms.Button
        Me.btnGetPartnerBalance = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnGetChargeURL = New System.Windows.Forms.Button
        Me.btnGetBalance = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnGetChargeInfo = New System.Windows.Forms.Button
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.btnCheckID = New System.Windows.Forms.Button
        Me.btnCheckIsMember = New System.Windows.Forms.Button
        Me.btnJoinMember = New System.Windows.Forms.Button
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCorpNum = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.txtTID = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtJobID = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.btnGetFlatRateState = New System.Windows.Forms.Button
        Me.btnFlatRatePopUpURL = New System.Windows.Forms.Button
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.btnRevokeCloseBankAccount = New System.Windows.Forms.Button
        Me.btnCloseBankAccount = New System.Windows.Forms.Button
        Me.btnGetBankAccountInfo = New System.Windows.Forms.Button
        Me.btnUpdateBankAccount = New System.Windows.Forms.Button
        Me.btnRegistBankAccount = New System.Windows.Forms.Button
        Me.btnListBankAccount = New System.Windows.Forms.Button
        Me.btnBankAccountMgtURL = New System.Windows.Forms.Button
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnSaveMemo = New System.Windows.Forms.Button
        Me.btnSummary = New System.Windows.Forms.Button
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.btnListActiveJob = New System.Windows.Forms.Button
        Me.btnGetJobState = New System.Windows.Forms.Button
        Me.btnRequestJob = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
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
        Me.GroupBox1.Location = New System.Drawing.Point(13, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1082, 155)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "팝빌 기본 API"
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.btnUpdateCorpInfo)
        Me.GroupBox17.Controls.Add(Me.btnGetCorpInfo)
        Me.GroupBox17.Location = New System.Drawing.Point(930, 17)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(148, 126)
        Me.GroupBox17.TabIndex = 6
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "회사정보 관련"
        '
        'btnUpdateCorpInfo
        '
        Me.btnUpdateCorpInfo.Location = New System.Drawing.Point(6, 51)
        Me.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo"
        Me.btnUpdateCorpInfo.Size = New System.Drawing.Size(135, 30)
        Me.btnUpdateCorpInfo.TabIndex = 8
        Me.btnUpdateCorpInfo.Text = "회사정보 수정"
        Me.btnUpdateCorpInfo.UseVisualStyleBackColor = True
        '
        'btnGetCorpInfo
        '
        Me.btnGetCorpInfo.Location = New System.Drawing.Point(6, 19)
        Me.btnGetCorpInfo.Name = "btnGetCorpInfo"
        Me.btnGetCorpInfo.Size = New System.Drawing.Size(135, 30)
        Me.btnGetCorpInfo.TabIndex = 7
        Me.btnGetCorpInfo.Text = "회사정보 조회"
        Me.btnGetCorpInfo.UseVisualStyleBackColor = True
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.btnUpdateContact)
        Me.GroupBox16.Controls.Add(Me.btnListContact)
        Me.GroupBox16.Controls.Add(Me.btnRegistContact)
        Me.GroupBox16.Location = New System.Drawing.Point(776, 17)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(148, 126)
        Me.GroupBox16.TabIndex = 5
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "담당자 관련"
        '
        'btnUpdateContact
        '
        Me.btnUpdateContact.Location = New System.Drawing.Point(6, 84)
        Me.btnUpdateContact.Name = "btnUpdateContact"
        Me.btnUpdateContact.Size = New System.Drawing.Size(135, 30)
        Me.btnUpdateContact.TabIndex = 7
        Me.btnUpdateContact.Text = "담당자 정보 수정"
        Me.btnUpdateContact.UseVisualStyleBackColor = True
        '
        'btnListContact
        '
        Me.btnListContact.Location = New System.Drawing.Point(6, 51)
        Me.btnListContact.Name = "btnListContact"
        Me.btnListContact.Size = New System.Drawing.Size(135, 30)
        Me.btnListContact.TabIndex = 6
        Me.btnListContact.Text = "담당자 목록 조회"
        Me.btnListContact.UseVisualStyleBackColor = True
        '
        'btnRegistContact
        '
        Me.btnRegistContact.Location = New System.Drawing.Point(6, 19)
        Me.btnRegistContact.Name = "btnRegistContact"
        Me.btnRegistContact.Size = New System.Drawing.Size(135, 30)
        Me.btnRegistContact.TabIndex = 5
        Me.btnRegistContact.Text = "담당자 추가"
        Me.btnRegistContact.UseVisualStyleBackColor = True
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.btnGetAccessURL)
        Me.GroupBox15.Location = New System.Drawing.Point(622, 17)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(148, 126)
        Me.GroupBox15.TabIndex = 4
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "팝빌 기본 URL"
        '
        'btnGetAccessURL
        '
        Me.btnGetAccessURL.Location = New System.Drawing.Point(6, 19)
        Me.btnGetAccessURL.Name = "btnGetAccessURL"
        Me.btnGetAccessURL.Size = New System.Drawing.Size(135, 30)
        Me.btnGetAccessURL.TabIndex = 6
        Me.btnGetAccessURL.Text = "팝빌 로그인 URL"
        Me.btnGetAccessURL.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnGetPartnerURL)
        Me.GroupBox5.Controls.Add(Me.btnGetPartnerBalance)
        Me.GroupBox5.Location = New System.Drawing.Point(468, 17)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(148, 126)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "파트너과금 포인트"
        '
        'btnGetPartnerURL
        '
        Me.btnGetPartnerURL.Location = New System.Drawing.Point(6, 51)
        Me.btnGetPartnerURL.Name = "btnGetPartnerURL"
        Me.btnGetPartnerURL.Size = New System.Drawing.Size(135, 30)
        Me.btnGetPartnerURL.TabIndex = 6
        Me.btnGetPartnerURL.Text = "포인트 충전 URL"
        Me.btnGetPartnerURL.UseVisualStyleBackColor = True
        '
        'btnGetPartnerBalance
        '
        Me.btnGetPartnerBalance.Location = New System.Drawing.Point(6, 19)
        Me.btnGetPartnerBalance.Name = "btnGetPartnerBalance"
        Me.btnGetPartnerBalance.Size = New System.Drawing.Size(135, 30)
        Me.btnGetPartnerBalance.TabIndex = 3
        Me.btnGetPartnerBalance.Text = "파트너포인트 확인"
        Me.btnGetPartnerBalance.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnGetChargeURL)
        Me.GroupBox2.Controls.Add(Me.btnGetBalance)
        Me.GroupBox2.Location = New System.Drawing.Point(314, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(148, 126)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "연동과금 포인트"
        '
        'btnGetChargeURL
        '
        Me.btnGetChargeURL.Location = New System.Drawing.Point(6, 51)
        Me.btnGetChargeURL.Name = "btnGetChargeURL"
        Me.btnGetChargeURL.Size = New System.Drawing.Size(135, 30)
        Me.btnGetChargeURL.TabIndex = 5
        Me.btnGetChargeURL.Text = "포인트 충전 URL"
        Me.btnGetChargeURL.UseVisualStyleBackColor = True
        '
        'btnGetBalance
        '
        Me.btnGetBalance.Location = New System.Drawing.Point(6, 19)
        Me.btnGetBalance.Name = "btnGetBalance"
        Me.btnGetBalance.Size = New System.Drawing.Size(135, 30)
        Me.btnGetBalance.TabIndex = 2
        Me.btnGetBalance.Text = "잔여포인트 확인"
        Me.btnGetBalance.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnGetChargeInfo)
        Me.GroupBox3.Location = New System.Drawing.Point(160, 17)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(148, 126)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "포인트 관련"
        '
        'btnGetChargeInfo
        '
        Me.btnGetChargeInfo.Location = New System.Drawing.Point(6, 19)
        Me.btnGetChargeInfo.Name = "btnGetChargeInfo"
        Me.btnGetChargeInfo.Size = New System.Drawing.Size(135, 30)
        Me.btnGetChargeInfo.TabIndex = 4
        Me.btnGetChargeInfo.Text = "과금정보 확인"
        Me.btnGetChargeInfo.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.btnCheckID)
        Me.GroupBox6.Controls.Add(Me.btnCheckIsMember)
        Me.GroupBox6.Controls.Add(Me.btnJoinMember)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 17)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(148, 126)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "회원 정보"
        '
        'btnCheckID
        '
        Me.btnCheckID.Location = New System.Drawing.Point(6, 51)
        Me.btnCheckID.Name = "btnCheckID"
        Me.btnCheckID.Size = New System.Drawing.Size(135, 30)
        Me.btnCheckID.TabIndex = 3
        Me.btnCheckID.Text = "ID 중복 확인"
        Me.btnCheckID.UseVisualStyleBackColor = True
        '
        'btnCheckIsMember
        '
        Me.btnCheckIsMember.Location = New System.Drawing.Point(6, 19)
        Me.btnCheckIsMember.Name = "btnCheckIsMember"
        Me.btnCheckIsMember.Size = New System.Drawing.Size(135, 30)
        Me.btnCheckIsMember.TabIndex = 2
        Me.btnCheckIsMember.Text = "가입여부 확인"
        Me.btnCheckIsMember.UseVisualStyleBackColor = True
        '
        'btnJoinMember
        '
        Me.btnJoinMember.Location = New System.Drawing.Point(6, 83)
        Me.btnJoinMember.Name = "btnJoinMember"
        Me.btnJoinMember.Size = New System.Drawing.Size(135, 30)
        Me.btnJoinMember.TabIndex = 1
        Me.btnJoinMember.Text = "회원 가입"
        Me.btnJoinMember.UseVisualStyleBackColor = True
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(415, 12)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(143, 21)
        Me.txtUserId.TabIndex = 37
        Me.txtUserId.Text = "testkorea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(310, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 12)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "팝빌회원 아이디 :"
        '
        'txtCorpNum
        '
        Me.txtCorpNum.Location = New System.Drawing.Point(146, 12)
        Me.txtCorpNum.Name = "txtCorpNum"
        Me.txtCorpNum.Size = New System.Drawing.Size(143, 21)
        Me.txtCorpNum.TabIndex = 35
        Me.txtCorpNum.Text = "1234567890"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 12)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "팝빌회원 사업자번호 : "
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.ListBox1)
        Me.GroupBox4.Controls.Add(Me.txtTID)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.txtJobID)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.GroupBox10)
        Me.GroupBox4.Controls.Add(Me.GroupBox9)
        Me.GroupBox4.Controls.Add(Me.GroupBox8)
        Me.GroupBox4.Controls.Add(Me.GroupBox7)
        Me.GroupBox4.Location = New System.Drawing.Point(13, 202)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1082, 433)
        Me.GroupBox4.TabIndex = 39
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "계좌조회 API"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Location = New System.Drawing.Point(17, 202)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(843, 208)
        Me.ListBox1.TabIndex = 8
        '
        'txtTID
        '
        Me.txtTID.Location = New System.Drawing.Point(474, 168)
        Me.txtTID.Name = "txtTID"
        Me.txtTID.Size = New System.Drawing.Size(175, 21)
        Me.txtTID.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(340, 173)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(134, 12)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "거래내역 아이디(TID) : "
        '
        'txtJobID
        '
        Me.txtJobID.Location = New System.Drawing.Point(153, 168)
        Me.txtJobID.Name = "txtJobID"
        Me.txtJobID.Size = New System.Drawing.Size(165, 21)
        Me.txtJobID.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 173)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(143, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "수집 작업아이디(jobID) : "
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.btnGetFlatRateState)
        Me.GroupBox10.Controls.Add(Me.btnFlatRatePopUpURL)
        Me.GroupBox10.Location = New System.Drawing.Point(748, 20)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(169, 135)
        Me.GroupBox10.TabIndex = 3
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "정액제 관리"
        '
        'btnGetFlatRateState
        '
        Me.btnGetFlatRateState.Location = New System.Drawing.Point(8, 57)
        Me.btnGetFlatRateState.Name = "btnGetFlatRateState"
        Me.btnGetFlatRateState.Size = New System.Drawing.Size(152, 31)
        Me.btnGetFlatRateState.TabIndex = 6
        Me.btnGetFlatRateState.Text = "정액제 서비스 상태 확인"
        Me.btnGetFlatRateState.UseVisualStyleBackColor = True
        '
        'btnFlatRatePopUpURL
        '
        Me.btnFlatRatePopUpURL.Location = New System.Drawing.Point(8, 20)
        Me.btnFlatRatePopUpURL.Name = "btnFlatRatePopUpURL"
        Me.btnFlatRatePopUpURL.Size = New System.Drawing.Size(152, 31)
        Me.btnFlatRatePopUpURL.TabIndex = 5
        Me.btnFlatRatePopUpURL.Text = "정액제 서비스 신청 URL"
        Me.btnFlatRatePopUpURL.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.btnRevokeCloseBankAccount)
        Me.GroupBox9.Controls.Add(Me.btnCloseBankAccount)
        Me.GroupBox9.Controls.Add(Me.btnGetBankAccountInfo)
        Me.GroupBox9.Controls.Add(Me.btnUpdateBankAccount)
        Me.GroupBox9.Controls.Add(Me.btnRegistBankAccount)
        Me.GroupBox9.Controls.Add(Me.btnListBankAccount)
        Me.GroupBox9.Controls.Add(Me.btnBankAccountMgtURL)
        Me.GroupBox9.Location = New System.Drawing.Point(324, 20)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(418, 135)
        Me.GroupBox9.TabIndex = 2
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "계좌 관리"
        '
        'btnRevokeCloseBankAccount
        '
        Me.btnRevokeCloseBankAccount.Location = New System.Drawing.Point(281, 58)
        Me.btnRevokeCloseBankAccount.Name = "btnRevokeCloseBankAccount"
        Me.btnRevokeCloseBankAccount.Size = New System.Drawing.Size(131, 30)
        Me.btnRevokeCloseBankAccount.TabIndex = 10
        Me.btnRevokeCloseBankAccount.Text = "정액제 해지신청 취소"
        Me.btnRevokeCloseBankAccount.UseVisualStyleBackColor = True
        '
        'btnCloseBankAccount
        '
        Me.btnCloseBankAccount.Location = New System.Drawing.Point(281, 20)
        Me.btnCloseBankAccount.Name = "btnCloseBankAccount"
        Me.btnCloseBankAccount.Size = New System.Drawing.Size(131, 30)
        Me.btnCloseBankAccount.TabIndex = 9
        Me.btnCloseBankAccount.Text = "계좌 정액제 해지신청"
        Me.btnCloseBankAccount.UseVisualStyleBackColor = True
        '
        'btnGetBankAccountInfo
        '
        Me.btnGetBankAccountInfo.Location = New System.Drawing.Point(143, 20)
        Me.btnGetBankAccountInfo.Name = "btnGetBankAccountInfo"
        Me.btnGetBankAccountInfo.Size = New System.Drawing.Size(131, 30)
        Me.btnGetBankAccountInfo.TabIndex = 8
        Me.btnGetBankAccountInfo.Text = "계좌 정보 확인"
        Me.btnGetBankAccountInfo.UseVisualStyleBackColor = True
        '
        'btnUpdateBankAccount
        '
        Me.btnUpdateBankAccount.Location = New System.Drawing.Point(6, 56)
        Me.btnUpdateBankAccount.Name = "btnUpdateBankAccount"
        Me.btnUpdateBankAccount.Size = New System.Drawing.Size(131, 32)
        Me.btnUpdateBankAccount.TabIndex = 7
        Me.btnUpdateBankAccount.Text = "계좌 정보 수정"
        Me.btnUpdateBankAccount.UseVisualStyleBackColor = True
        '
        'btnRegistBankAccount
        '
        Me.btnRegistBankAccount.Location = New System.Drawing.Point(6, 20)
        Me.btnRegistBankAccount.Name = "btnRegistBankAccount"
        Me.btnRegistBankAccount.Size = New System.Drawing.Size(131, 32)
        Me.btnRegistBankAccount.TabIndex = 6
        Me.btnRegistBankAccount.Text = "계좌 등록"
        Me.btnRegistBankAccount.UseVisualStyleBackColor = True
        '
        'btnListBankAccount
        '
        Me.btnListBankAccount.Location = New System.Drawing.Point(143, 57)
        Me.btnListBankAccount.Name = "btnListBankAccount"
        Me.btnListBankAccount.Size = New System.Drawing.Size(131, 31)
        Me.btnListBankAccount.TabIndex = 5
        Me.btnListBankAccount.Text = "계좌 목록 확인"
        Me.btnListBankAccount.UseVisualStyleBackColor = True
        '
        'btnBankAccountMgtURL
        '
        Me.btnBankAccountMgtURL.Location = New System.Drawing.Point(6, 94)
        Me.btnBankAccountMgtURL.Name = "btnBankAccountMgtURL"
        Me.btnBankAccountMgtURL.Size = New System.Drawing.Size(131, 31)
        Me.btnBankAccountMgtURL.TabIndex = 4
        Me.btnBankAccountMgtURL.Text = "계좌관리 팝업 URL"
        Me.btnBankAccountMgtURL.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.btnSearch)
        Me.GroupBox8.Controls.Add(Me.btnSaveMemo)
        Me.GroupBox8.Controls.Add(Me.btnSummary)
        Me.GroupBox8.Location = New System.Drawing.Point(168, 20)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(150, 135)
        Me.GroupBox8.TabIndex = 1
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "거래내역 관리"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(10, 20)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(131, 31)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "거래내역 조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnSaveMemo
        '
        Me.btnSaveMemo.Location = New System.Drawing.Point(10, 94)
        Me.btnSaveMemo.Name = "btnSaveMemo"
        Me.btnSaveMemo.Size = New System.Drawing.Size(131, 31)
        Me.btnSaveMemo.TabIndex = 2
        Me.btnSaveMemo.Text = "거래내역 메모저장"
        Me.btnSaveMemo.UseVisualStyleBackColor = True
        '
        'btnSummary
        '
        Me.btnSummary.Location = New System.Drawing.Point(10, 57)
        Me.btnSummary.Name = "btnSummary"
        Me.btnSummary.Size = New System.Drawing.Size(131, 31)
        Me.btnSummary.TabIndex = 1
        Me.btnSummary.Text = "거래내역 요약정보"
        Me.btnSummary.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.btnListActiveJob)
        Me.GroupBox7.Controls.Add(Me.btnGetJobState)
        Me.GroupBox7.Controls.Add(Me.btnRequestJob)
        Me.GroupBox7.Location = New System.Drawing.Point(12, 20)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(150, 135)
        Me.GroupBox7.TabIndex = 0
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "거래내역 수집"
        '
        'btnListActiveJob
        '
        Me.btnListActiveJob.Location = New System.Drawing.Point(10, 94)
        Me.btnListActiveJob.Name = "btnListActiveJob"
        Me.btnListActiveJob.Size = New System.Drawing.Size(131, 31)
        Me.btnListActiveJob.TabIndex = 2
        Me.btnListActiveJob.Text = "수집상태 목록 확인"
        Me.btnListActiveJob.UseVisualStyleBackColor = True
        '
        'btnGetJobState
        '
        Me.btnGetJobState.Location = New System.Drawing.Point(10, 57)
        Me.btnGetJobState.Name = "btnGetJobState"
        Me.btnGetJobState.Size = New System.Drawing.Size(131, 31)
        Me.btnGetJobState.TabIndex = 1
        Me.btnGetJobState.Text = "수집 상태 확인"
        Me.btnGetJobState.UseVisualStyleBackColor = True
        '
        'btnRequestJob
        '
        Me.btnRequestJob.Location = New System.Drawing.Point(10, 20)
        Me.btnRequestJob.Name = "btnRequestJob"
        Me.btnRequestJob.Size = New System.Drawing.Size(131, 31)
        Me.btnRequestJob.TabIndex = 0
        Me.btnRequestJob.Text = "수집 요청"
        Me.btnRequestJob.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(719, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 12)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "응답 URL :"
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(789, 14)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(302, 21)
        Me.txtURL.TabIndex = 9
        '
        'frmExample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1110, 665)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCorpNum)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmExample"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "팝빌 계좌조회 API SDK VB.NET Example "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
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
    Friend WithEvents btnGetChargeInfo As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCheckID As System.Windows.Forms.Button
    Friend WithEvents btnCheckIsMember As System.Windows.Forms.Button
    Friend WithEvents btnJoinMember As System.Windows.Forms.Button
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCorpNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRequestJob As System.Windows.Forms.Button
    Friend WithEvents btnGetJobState As System.Windows.Forms.Button
    Friend WithEvents btnListActiveJob As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnSaveMemo As System.Windows.Forms.Button
    Friend WithEvents btnSummary As System.Windows.Forms.Button
    Friend WithEvents btnListBankAccount As System.Windows.Forms.Button
    Friend WithEvents btnBankAccountMgtURL As System.Windows.Forms.Button
    Friend WithEvents btnGetFlatRateState As System.Windows.Forms.Button
    Friend WithEvents btnFlatRatePopUpURL As System.Windows.Forms.Button
    Friend WithEvents txtJobID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTID As System.Windows.Forms.TextBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents btnRegistBankAccount As System.Windows.Forms.Button
    Friend WithEvents btnGetBankAccountInfo As System.Windows.Forms.Button
    Friend WithEvents btnUpdateBankAccount As System.Windows.Forms.Button
    Friend WithEvents btnRevokeCloseBankAccount As System.Windows.Forms.Button
    Friend WithEvents btnCloseBankAccount As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtURL As System.Windows.Forms.TextBox

End Class
