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
        Me.btnGetPopbillURL_LOGIN = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btnGetPartnerURL = New System.Windows.Forms.Button
        Me.btnGetPartnerBalance = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnGetPopbillURL_CHRG = New System.Windows.Forms.Button
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
        Me.groupBox7 = New System.Windows.Forms.GroupBox
        Me.groupBox11 = New System.Windows.Forms.GroupBox
        Me.btnGetFlatRateState = New System.Windows.Forms.Button
        Me.btnGetFlatRatePopUpURL = New System.Windows.Forms.Button
        Me.label4 = New System.Windows.Forms.Label
        Me.listBox1 = New System.Windows.Forms.ListBox
        Me.groupBox9 = New System.Windows.Forms.GroupBox
        Me.btnSummary = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtJobID = New System.Windows.Forms.TextBox
        Me.label3 = New System.Windows.Forms.Label
        Me.groupBox8 = New System.Windows.Forms.GroupBox
        Me.btnRequestJob = New System.Windows.Forms.Button
        Me.btnGetJobState = New System.Windows.Forms.Button
        Me.btnListActiveJob = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.btnRegistDeptUser = New System.Windows.Forms.Button
        Me.btnCheckCertValidation = New System.Windows.Forms.Button
        Me.btnDeleteDeptUser = New System.Windows.Forms.Button
        Me.btnCheckLoginDeptUser = New System.Windows.Forms.Button
        Me.btnCheckDeptUser = New System.Windows.Forms.Button
        Me.btnGetCertificateExpireDate = New System.Windows.Forms.Button
        Me.btnGetCertificatePopUpURL = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.groupBox7.SuspendLayout()
        Me.groupBox11.SuspendLayout()
        Me.groupBox9.SuspendLayout()
        Me.groupBox8.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
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
        Me.GroupBox1.Location = New System.Drawing.Point(17, 44)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1015, 155)
        Me.GroupBox1.TabIndex = 38
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
        Me.txtUserId.Location = New System.Drawing.Point(419, 14)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(143, 21)
        Me.txtUserId.TabIndex = 37
        Me.txtUserId.Text = "testkorea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(314, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 12)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "팝빌회원 아이디 :"
        '
        'txtCorpNum
        '
        Me.txtCorpNum.Location = New System.Drawing.Point(150, 15)
        Me.txtCorpNum.Name = "txtCorpNum"
        Me.txtCorpNum.Size = New System.Drawing.Size(143, 21)
        Me.txtCorpNum.TabIndex = 35
        Me.txtCorpNum.Text = "1234567890"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 12)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "팝빌회원 사업자번호 : "
        '
        'groupBox7
        '
        Me.groupBox7.Controls.Add(Me.GroupBox4)
        Me.groupBox7.Controls.Add(Me.groupBox11)
        Me.groupBox7.Controls.Add(Me.label4)
        Me.groupBox7.Controls.Add(Me.listBox1)
        Me.groupBox7.Controls.Add(Me.groupBox9)
        Me.groupBox7.Controls.Add(Me.txtJobID)
        Me.groupBox7.Controls.Add(Me.label3)
        Me.groupBox7.Controls.Add(Me.groupBox8)
        Me.groupBox7.Location = New System.Drawing.Point(17, 213)
        Me.groupBox7.Name = "groupBox7"
        Me.groupBox7.Size = New System.Drawing.Size(1015, 496)
        Me.groupBox7.TabIndex = 39
        Me.groupBox7.TabStop = False
        Me.groupBox7.Text = "홈택스 현금영수증 연계 관련 API"
        '
        'groupBox11
        '
        Me.groupBox11.Controls.Add(Me.btnGetFlatRateState)
        Me.groupBox11.Controls.Add(Me.btnGetFlatRatePopUpURL)
        Me.groupBox11.Location = New System.Drawing.Point(413, 19)
        Me.groupBox11.Name = "groupBox11"
        Me.groupBox11.Size = New System.Drawing.Size(188, 159)
        Me.groupBox11.TabIndex = 7
        Me.groupBox11.TabStop = False
        Me.groupBox11.Text = "부가기능"
        '
        'btnGetFlatRateState
        '
        Me.btnGetFlatRateState.Location = New System.Drawing.Point(8, 56)
        Me.btnGetFlatRateState.Name = "btnGetFlatRateState"
        Me.btnGetFlatRateState.Size = New System.Drawing.Size(169, 31)
        Me.btnGetFlatRateState.TabIndex = 1
        Me.btnGetFlatRateState.Text = "정액제 서비스 상태 확인"
        Me.btnGetFlatRateState.UseVisualStyleBackColor = True
        '
        'btnGetFlatRatePopUpURL
        '
        Me.btnGetFlatRatePopUpURL.Location = New System.Drawing.Point(8, 23)
        Me.btnGetFlatRatePopUpURL.Name = "btnGetFlatRatePopUpURL"
        Me.btnGetFlatRatePopUpURL.Size = New System.Drawing.Size(169, 31)
        Me.btnGetFlatRatePopUpURL.TabIndex = 0
        Me.btnGetFlatRatePopUpURL.Text = "정액제 서비스 신청 URL"
        Me.btnGetFlatRatePopUpURL.UseVisualStyleBackColor = True
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(278, 194)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(259, 12)
        Me.label4.TabIndex = 5
        Me.label4.Text = "(작업아이디는 '수집 요청' 호출시 생성됩니다.)"
        '
        'listBox1
        '
        Me.listBox1.FormattingEnabled = True
        Me.listBox1.ItemHeight = 12
        Me.listBox1.Location = New System.Drawing.Point(21, 214)
        Me.listBox1.Name = "listBox1"
        Me.listBox1.Size = New System.Drawing.Size(969, 256)
        Me.listBox1.TabIndex = 4
        '
        'groupBox9
        '
        Me.groupBox9.Controls.Add(Me.btnSummary)
        Me.groupBox9.Controls.Add(Me.btnSearch)
        Me.groupBox9.Location = New System.Drawing.Point(210, 21)
        Me.groupBox9.Name = "groupBox9"
        Me.groupBox9.Size = New System.Drawing.Size(188, 159)
        Me.groupBox9.TabIndex = 3
        Me.groupBox9.TabStop = False
        Me.groupBox9.Text = "매출/매입 수집결과 조회"
        '
        'btnSummary
        '
        Me.btnSummary.Location = New System.Drawing.Point(8, 56)
        Me.btnSummary.Name = "btnSummary"
        Me.btnSummary.Size = New System.Drawing.Size(169, 31)
        Me.btnSummary.TabIndex = 1
        Me.btnSummary.Text = "수집 결과 요약정보 조회"
        Me.btnSummary.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(8, 23)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(169, 31)
        Me.btnSearch.TabIndex = 0
        Me.btnSearch.Text = "수집 결과 조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtJobID
        '
        Me.txtJobID.Location = New System.Drawing.Point(140, 189)
        Me.txtJobID.Name = "txtJobID"
        Me.txtJobID.Size = New System.Drawing.Size(134, 21)
        Me.txtJobID.TabIndex = 2
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(19, 194)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(115, 12)
        Me.label3.TabIndex = 1
        Me.label3.Text = "작업아이디 (jobID) :"
        '
        'groupBox8
        '
        Me.groupBox8.Controls.Add(Me.btnRequestJob)
        Me.groupBox8.Controls.Add(Me.btnGetJobState)
        Me.groupBox8.Controls.Add(Me.btnListActiveJob)
        Me.groupBox8.Location = New System.Drawing.Point(11, 21)
        Me.groupBox8.Name = "groupBox8"
        Me.groupBox8.Size = New System.Drawing.Size(188, 159)
        Me.groupBox8.TabIndex = 0
        Me.groupBox8.TabStop = False
        Me.groupBox8.Text = "매출/매입 내역 수집"
        '
        'btnRequestJob
        '
        Me.btnRequestJob.Location = New System.Drawing.Point(8, 23)
        Me.btnRequestJob.Name = "btnRequestJob"
        Me.btnRequestJob.Size = New System.Drawing.Size(169, 31)
        Me.btnRequestJob.TabIndex = 2
        Me.btnRequestJob.Text = "수집 요청"
        Me.btnRequestJob.UseVisualStyleBackColor = True
        '
        'btnGetJobState
        '
        Me.btnGetJobState.Location = New System.Drawing.Point(8, 56)
        Me.btnGetJobState.Name = "btnGetJobState"
        Me.btnGetJobState.Size = New System.Drawing.Size(169, 31)
        Me.btnGetJobState.TabIndex = 1
        Me.btnGetJobState.Text = "수집 상태 확인"
        Me.btnGetJobState.UseVisualStyleBackColor = True
        '
        'btnListActiveJob
        '
        Me.btnListActiveJob.Location = New System.Drawing.Point(8, 89)
        Me.btnListActiveJob.Name = "btnListActiveJob"
        Me.btnListActiveJob.Size = New System.Drawing.Size(169, 31)
        Me.btnListActiveJob.TabIndex = 0
        Me.btnListActiveJob.Text = "수집 상태 목록 확인"
        Me.btnListActiveJob.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnGetCertificateExpireDate)
        Me.GroupBox4.Controls.Add(Me.btnGetCertificatePopUpURL)
        Me.GroupBox4.Controls.Add(Me.btnDeleteDeptUser)
        Me.GroupBox4.Controls.Add(Me.btnCheckLoginDeptUser)
        Me.GroupBox4.Controls.Add(Me.btnCheckDeptUser)
        Me.GroupBox4.Controls.Add(Me.btnRegistDeptUser)
        Me.GroupBox4.Controls.Add(Me.btnCheckCertValidation)
        Me.GroupBox4.Location = New System.Drawing.Point(618, 19)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(383, 159)
        Me.GroupBox4.TabIndex = 8
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "홈택스 인증관련 기능"
        '
        'btnRegistDeptUser
        '
        Me.btnRegistDeptUser.Location = New System.Drawing.Point(8, 122)
        Me.btnRegistDeptUser.Name = "btnRegistDeptUser"
        Me.btnRegistDeptUser.Size = New System.Drawing.Size(169, 31)
        Me.btnRegistDeptUser.TabIndex = 3
        Me.btnRegistDeptUser.Text = "부서사용자 계정등록"
        Me.btnRegistDeptUser.UseVisualStyleBackColor = True
        '
        'btnCheckCertValidation
        '
        Me.btnCheckCertValidation.Location = New System.Drawing.Point(8, 89)
        Me.btnCheckCertValidation.Name = "btnCheckCertValidation"
        Me.btnCheckCertValidation.Size = New System.Drawing.Size(169, 31)
        Me.btnCheckCertValidation.TabIndex = 2
        Me.btnCheckCertValidation.Text = "공인인증서 로그인 테스트"
        Me.btnCheckCertValidation.UseVisualStyleBackColor = True
        '
        'btnDeleteDeptUser
        '
        Me.btnDeleteDeptUser.Location = New System.Drawing.Point(193, 89)
        Me.btnDeleteDeptUser.Name = "btnDeleteDeptUser"
        Me.btnDeleteDeptUser.Size = New System.Drawing.Size(169, 31)
        Me.btnDeleteDeptUser.TabIndex = 6
        Me.btnDeleteDeptUser.Text = "부서사용자 등록정보 삭제"
        Me.btnDeleteDeptUser.UseVisualStyleBackColor = True
        '
        'btnCheckLoginDeptUser
        '
        Me.btnCheckLoginDeptUser.Location = New System.Drawing.Point(193, 56)
        Me.btnCheckLoginDeptUser.Name = "btnCheckLoginDeptUser"
        Me.btnCheckLoginDeptUser.Size = New System.Drawing.Size(169, 31)
        Me.btnCheckLoginDeptUser.TabIndex = 5
        Me.btnCheckLoginDeptUser.Text = "부서사용자 로그인 테스트"
        Me.btnCheckLoginDeptUser.UseVisualStyleBackColor = True
        '
        'btnCheckDeptUser
        '
        Me.btnCheckDeptUser.Location = New System.Drawing.Point(193, 23)
        Me.btnCheckDeptUser.Name = "btnCheckDeptUser"
        Me.btnCheckDeptUser.Size = New System.Drawing.Size(169, 31)
        Me.btnCheckDeptUser.TabIndex = 4
        Me.btnCheckDeptUser.Text = "부서사용자 등록정보 확인"
        Me.btnCheckDeptUser.UseVisualStyleBackColor = True
        '
        'btnGetCertificateExpireDate
        '
        Me.btnGetCertificateExpireDate.Location = New System.Drawing.Point(8, 56)
        Me.btnGetCertificateExpireDate.Name = "btnGetCertificateExpireDate"
        Me.btnGetCertificateExpireDate.Size = New System.Drawing.Size(169, 31)
        Me.btnGetCertificateExpireDate.TabIndex = 8
        Me.btnGetCertificateExpireDate.Text = "공인인증서 만료일자 확인"
        Me.btnGetCertificateExpireDate.UseVisualStyleBackColor = True
        '
        'btnGetCertificatePopUpURL
        '
        Me.btnGetCertificatePopUpURL.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGetCertificatePopUpURL.Location = New System.Drawing.Point(8, 23)
        Me.btnGetCertificatePopUpURL.Name = "btnGetCertificatePopUpURL"
        Me.btnGetCertificatePopUpURL.Size = New System.Drawing.Size(169, 31)
        Me.btnGetCertificatePopUpURL.TabIndex = 7
        Me.btnGetCertificatePopUpURL.Text = "홈택스연동 인증관리 URL"
        Me.btnGetCertificatePopUpURL.UseVisualStyleBackColor = True
        '
        'frmExample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1049, 721)
        Me.Controls.Add(Me.groupBox7)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCorpNum)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmExample"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "홈택스 현금영수증 매입매출 조회 API SDK"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.groupBox7.ResumeLayout(False)
        Me.groupBox7.PerformLayout()
        Me.groupBox11.ResumeLayout(False)
        Me.groupBox9.ResumeLayout(False)
        Me.groupBox8.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
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
    Friend WithEvents btnGetPopbillURL_LOGIN As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetPartnerURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPartnerBalance As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetPopbillURL_CHRG As System.Windows.Forms.Button
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
    Private WithEvents groupBox7 As System.Windows.Forms.GroupBox
    Private WithEvents groupBox11 As System.Windows.Forms.GroupBox
    Private WithEvents btnGetFlatRateState As System.Windows.Forms.Button
    Private WithEvents btnGetFlatRatePopUpURL As System.Windows.Forms.Button
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents listBox1 As System.Windows.Forms.ListBox
    Private WithEvents groupBox9 As System.Windows.Forms.GroupBox
    Private WithEvents btnSummary As System.Windows.Forms.Button
    Private WithEvents btnSearch As System.Windows.Forms.Button
    Private WithEvents txtJobID As System.Windows.Forms.TextBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents groupBox8 As System.Windows.Forms.GroupBox
    Private WithEvents btnRequestJob As System.Windows.Forms.Button
    Private WithEvents btnGetJobState As System.Windows.Forms.Button
    Private WithEvents btnListActiveJob As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Private WithEvents btnGetCertificateExpireDate As System.Windows.Forms.Button
    Private WithEvents btnGetCertificatePopUpURL As System.Windows.Forms.Button
    Friend WithEvents btnDeleteDeptUser As System.Windows.Forms.Button
    Friend WithEvents btnCheckLoginDeptUser As System.Windows.Forms.Button
    Friend WithEvents btnCheckDeptUser As System.Windows.Forms.Button
    Friend WithEvents btnRegistDeptUser As System.Windows.Forms.Button
    Friend WithEvents btnCheckCertValidation As System.Windows.Forms.Button

End Class
