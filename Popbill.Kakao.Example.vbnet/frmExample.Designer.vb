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
        Me.btnGetChargeInfo_FMS = New System.Windows.Forms.Button
        Me.btnGetChargeInfo_FTS = New System.Windows.Forms.Button
        Me.btnGetUnitCost_FMS = New System.Windows.Forms.Button
        Me.btnGetUnitCost_FTS = New System.Windows.Forms.Button
        Me.btnGetChargeInfo_ATS = New System.Windows.Forms.Button
        Me.btnUnitCost_ATS = New System.Windows.Forms.Button
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.btnCheckID = New System.Windows.Forms.Button
        Me.btnCheckIsMember = New System.Windows.Forms.Button
        Me.btnJoinMember = New System.Windows.Forms.Button
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCorpNum = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnGetURL_BOX = New System.Windows.Forms.Button
        Me.btnGetSenderNumberList = New System.Windows.Forms.Button
        Me.btnGetURL_SENDER = New System.Windows.Forms.Button
        Me.btnListATSTemplate = New System.Windows.Forms.Button
        Me.btnGetURL_TEMPLATE = New System.Windows.Forms.Button
        Me.btnListPlusFriendID = New System.Windows.Forms.Button
        Me.btnGetURL_PLUSFRIENDID = New System.Windows.Forms.Button
        Me.btnCancelReserve = New System.Windows.Forms.Button
        Me.btnGetMessages = New System.Windows.Forms.Button
        Me.txtReceiptNum = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.btnSendFMS_multi = New System.Windows.Forms.Button
        Me.btnSendFMS_same = New System.Windows.Forms.Button
        Me.btnSendFMS_one = New System.Windows.Forms.Button
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.btnSendFTS_multi = New System.Windows.Forms.Button
        Me.btnSendFTS_same = New System.Windows.Forms.Button
        Me.btnSendFTS_one = New System.Windows.Forms.Button
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.btnSendATS_multi = New System.Windows.Forms.Button
        Me.btnSendATS_same = New System.Windows.Forms.Button
        Me.btnSendATS_one = New System.Windows.Forms.Button
        Me.txtReserveDT = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.fileDialog = New System.Windows.Forms.OpenFileDialog
        Me.dataGrid1 = New System.Windows.Forms.DataGridView
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.dataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox1.Controls.Add(Me.GroupBox9)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1091, 159)
        Me.GroupBox1.TabIndex = 33
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "팝빌 기본 API"
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.btnUpdateCorpInfo)
        Me.GroupBox17.Controls.Add(Me.btnGetCorpInfo)
        Me.GroupBox17.Location = New System.Drawing.Point(969, 19)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(110, 125)
        Me.GroupBox17.TabIndex = 6
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "회사정보 관련"
        '
        'btnUpdateCorpInfo
        '
        Me.btnUpdateCorpInfo.Location = New System.Drawing.Point(6, 54)
        Me.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo"
        Me.btnUpdateCorpInfo.Size = New System.Drawing.Size(98, 30)
        Me.btnUpdateCorpInfo.TabIndex = 8
        Me.btnUpdateCorpInfo.Text = "회사정보 수정"
        Me.btnUpdateCorpInfo.UseVisualStyleBackColor = True
        '
        'btnGetCorpInfo
        '
        Me.btnGetCorpInfo.Location = New System.Drawing.Point(6, 20)
        Me.btnGetCorpInfo.Name = "btnGetCorpInfo"
        Me.btnGetCorpInfo.Size = New System.Drawing.Size(98, 30)
        Me.btnGetCorpInfo.TabIndex = 7
        Me.btnGetCorpInfo.Text = "회사정보 조회"
        Me.btnGetCorpInfo.UseVisualStyleBackColor = True
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.btnUpdateContact)
        Me.GroupBox16.Controls.Add(Me.btnListContact)
        Me.GroupBox16.Controls.Add(Me.btnRegistContact)
        Me.GroupBox16.Location = New System.Drawing.Point(842, 20)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(121, 126)
        Me.GroupBox16.TabIndex = 5
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "담당자 관련"
        '
        'btnUpdateContact
        '
        Me.btnUpdateContact.Location = New System.Drawing.Point(8, 84)
        Me.btnUpdateContact.Name = "btnUpdateContact"
        Me.btnUpdateContact.Size = New System.Drawing.Size(111, 30)
        Me.btnUpdateContact.TabIndex = 7
        Me.btnUpdateContact.Text = "담당자 정보 수정"
        Me.btnUpdateContact.UseVisualStyleBackColor = True
        '
        'btnListContact
        '
        Me.btnListContact.Location = New System.Drawing.Point(8, 52)
        Me.btnListContact.Name = "btnListContact"
        Me.btnListContact.Size = New System.Drawing.Size(111, 30)
        Me.btnListContact.TabIndex = 6
        Me.btnListContact.Text = "담당자 목록 조회"
        Me.btnListContact.UseVisualStyleBackColor = True
        '
        'btnRegistContact
        '
        Me.btnRegistContact.Location = New System.Drawing.Point(8, 20)
        Me.btnRegistContact.Name = "btnRegistContact"
        Me.btnRegistContact.Size = New System.Drawing.Size(111, 30)
        Me.btnRegistContact.TabIndex = 5
        Me.btnRegistContact.Text = "담당자 추가"
        Me.btnRegistContact.UseVisualStyleBackColor = True
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.btnGetPopbillURL_LOGIN)
        Me.GroupBox15.Location = New System.Drawing.Point(709, 20)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(127, 126)
        Me.GroupBox15.TabIndex = 4
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "팝빌 기본 URL"
        '
        'btnGetPopbillURL_LOGIN
        '
        Me.btnGetPopbillURL_LOGIN.Location = New System.Drawing.Point(6, 19)
        Me.btnGetPopbillURL_LOGIN.Name = "btnGetPopbillURL_LOGIN"
        Me.btnGetPopbillURL_LOGIN.Size = New System.Drawing.Size(115, 30)
        Me.btnGetPopbillURL_LOGIN.TabIndex = 6
        Me.btnGetPopbillURL_LOGIN.Text = "팝빌 로그인 URL"
        Me.btnGetPopbillURL_LOGIN.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnGetPartnerURL)
        Me.GroupBox5.Controls.Add(Me.btnGetPartnerBalance)
        Me.GroupBox5.Location = New System.Drawing.Point(574, 21)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(129, 125)
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
        Me.GroupBox2.Location = New System.Drawing.Point(453, 21)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(115, 125)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "연동과금 포인트"
        '
        'btnGetPopbillURL_CHRG
        '
        Me.btnGetPopbillURL_CHRG.Location = New System.Drawing.Point(6, 50)
        Me.btnGetPopbillURL_CHRG.Name = "btnGetPopbillURL_CHRG"
        Me.btnGetPopbillURL_CHRG.Size = New System.Drawing.Size(105, 30)
        Me.btnGetPopbillURL_CHRG.TabIndex = 5
        Me.btnGetPopbillURL_CHRG.Text = "포인트 충전 URL"
        Me.btnGetPopbillURL_CHRG.UseVisualStyleBackColor = True
        '
        'btnGetBalance
        '
        Me.btnGetBalance.Location = New System.Drawing.Point(7, 19)
        Me.btnGetBalance.Name = "btnGetBalance"
        Me.btnGetBalance.Size = New System.Drawing.Size(105, 29)
        Me.btnGetBalance.TabIndex = 2
        Me.btnGetBalance.Text = "잔여포인트 확인"
        Me.btnGetBalance.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnGetChargeInfo_FMS)
        Me.GroupBox3.Controls.Add(Me.btnGetChargeInfo_FTS)
        Me.GroupBox3.Controls.Add(Me.btnGetUnitCost_FMS)
        Me.GroupBox3.Controls.Add(Me.btnGetUnitCost_FTS)
        Me.GroupBox3.Controls.Add(Me.btnGetChargeInfo_ATS)
        Me.GroupBox3.Controls.Add(Me.btnUnitCost_ATS)
        Me.GroupBox3.Location = New System.Drawing.Point(133, 21)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(314, 125)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "포인트 관련"
        '
        'btnGetChargeInfo_FMS
        '
        Me.btnGetChargeInfo_FMS.Location = New System.Drawing.Point(156, 86)
        Me.btnGetChargeInfo_FMS.Name = "btnGetChargeInfo_FMS"
        Me.btnGetChargeInfo_FMS.Size = New System.Drawing.Size(144, 30)
        Me.btnGetChargeInfo_FMS.TabIndex = 8
        Me.btnGetChargeInfo_FMS.Text = "친구톡 이미지 과금정보"
        Me.btnGetChargeInfo_FMS.UseVisualStyleBackColor = True
        '
        'btnGetChargeInfo_FTS
        '
        Me.btnGetChargeInfo_FTS.Location = New System.Drawing.Point(156, 52)
        Me.btnGetChargeInfo_FTS.Name = "btnGetChargeInfo_FTS"
        Me.btnGetChargeInfo_FTS.Size = New System.Drawing.Size(144, 30)
        Me.btnGetChargeInfo_FTS.TabIndex = 7
        Me.btnGetChargeInfo_FTS.Text = "친구톡 텍스트 과금정보"
        Me.btnGetChargeInfo_FTS.UseVisualStyleBackColor = True
        '
        'btnGetUnitCost_FMS
        '
        Me.btnGetUnitCost_FMS.Location = New System.Drawing.Point(10, 86)
        Me.btnGetUnitCost_FMS.Name = "btnGetUnitCost_FMS"
        Me.btnGetUnitCost_FMS.Size = New System.Drawing.Size(141, 29)
        Me.btnGetUnitCost_FMS.TabIndex = 6
        Me.btnGetUnitCost_FMS.Text = "친구톡 이미지 전송단가"
        Me.btnGetUnitCost_FMS.UseVisualStyleBackColor = True
        '
        'btnGetUnitCost_FTS
        '
        Me.btnGetUnitCost_FTS.Location = New System.Drawing.Point(10, 52)
        Me.btnGetUnitCost_FTS.Name = "btnGetUnitCost_FTS"
        Me.btnGetUnitCost_FTS.Size = New System.Drawing.Size(141, 29)
        Me.btnGetUnitCost_FTS.TabIndex = 5
        Me.btnGetUnitCost_FTS.Text = "친구톡 텍스트 전송단가"
        Me.btnGetUnitCost_FTS.UseVisualStyleBackColor = True
        '
        'btnGetChargeInfo_ATS
        '
        Me.btnGetChargeInfo_ATS.Location = New System.Drawing.Point(156, 19)
        Me.btnGetChargeInfo_ATS.Name = "btnGetChargeInfo_ATS"
        Me.btnGetChargeInfo_ATS.Size = New System.Drawing.Size(144, 30)
        Me.btnGetChargeInfo_ATS.TabIndex = 4
        Me.btnGetChargeInfo_ATS.Text = "알림톡 과금정보"
        Me.btnGetChargeInfo_ATS.UseVisualStyleBackColor = True
        '
        'btnUnitCost_ATS
        '
        Me.btnUnitCost_ATS.Location = New System.Drawing.Point(10, 19)
        Me.btnUnitCost_ATS.Name = "btnUnitCost_ATS"
        Me.btnUnitCost_ATS.Size = New System.Drawing.Size(141, 29)
        Me.btnUnitCost_ATS.TabIndex = 3
        Me.btnUnitCost_ATS.Text = "알림톡 전송단가"
        Me.btnUnitCost_ATS.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.btnCheckID)
        Me.GroupBox9.Controls.Add(Me.btnCheckIsMember)
        Me.GroupBox9.Controls.Add(Me.btnJoinMember)
        Me.GroupBox9.Location = New System.Drawing.Point(8, 20)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(120, 125)
        Me.GroupBox9.TabIndex = 0
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "회원 정보"
        '
        'btnCheckID
        '
        Me.btnCheckID.Location = New System.Drawing.Point(6, 51)
        Me.btnCheckID.Name = "btnCheckID"
        Me.btnCheckID.Size = New System.Drawing.Size(106, 30)
        Me.btnCheckID.TabIndex = 3
        Me.btnCheckID.Text = "ID 중복 확인"
        Me.btnCheckID.UseVisualStyleBackColor = True
        '
        'btnCheckIsMember
        '
        Me.btnCheckIsMember.Location = New System.Drawing.Point(6, 19)
        Me.btnCheckIsMember.Name = "btnCheckIsMember"
        Me.btnCheckIsMember.Size = New System.Drawing.Size(106, 29)
        Me.btnCheckIsMember.TabIndex = 2
        Me.btnCheckIsMember.Text = "가입여부 확인"
        Me.btnCheckIsMember.UseVisualStyleBackColor = True
        '
        'btnJoinMember
        '
        Me.btnJoinMember.Location = New System.Drawing.Point(6, 84)
        Me.btnJoinMember.Name = "btnJoinMember"
        Me.btnJoinMember.Size = New System.Drawing.Size(106, 31)
        Me.btnJoinMember.TabIndex = 1
        Me.btnJoinMember.Text = "회원 가입"
        Me.btnJoinMember.UseVisualStyleBackColor = True
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(408, 10)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(143, 21)
        Me.txtUserId.TabIndex = 32
        Me.txtUserId.Text = "testkorea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(303, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 12)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "팝빌회원 아이디 :"
        '
        'txtCorpNum
        '
        Me.txtCorpNum.Location = New System.Drawing.Point(139, 11)
        Me.txtCorpNum.Name = "txtCorpNum"
        Me.txtCorpNum.Size = New System.Drawing.Size(143, 21)
        Me.txtCorpNum.TabIndex = 30
        Me.txtCorpNum.Text = "1234567890"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 12)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "팝빌회원 사업자번호 : "
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.dataGrid1)
        Me.GroupBox4.Controls.Add(Me.GroupBox10)
        Me.GroupBox4.Controls.Add(Me.btnCancelReserve)
        Me.GroupBox4.Controls.Add(Me.btnGetMessages)
        Me.GroupBox4.Controls.Add(Me.txtReceiptNum)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.GroupBox8)
        Me.GroupBox4.Controls.Add(Me.GroupBox7)
        Me.GroupBox4.Controls.Add(Me.GroupBox6)
        Me.GroupBox4.Controls.Add(Me.txtReserveDT)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 205)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1091, 413)
        Me.GroupBox4.TabIndex = 34
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "카카오톡 관련 기능"
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.btnSearch)
        Me.GroupBox10.Controls.Add(Me.btnGetURL_BOX)
        Me.GroupBox10.Controls.Add(Me.btnGetSenderNumberList)
        Me.GroupBox10.Controls.Add(Me.btnGetURL_SENDER)
        Me.GroupBox10.Controls.Add(Me.btnListATSTemplate)
        Me.GroupBox10.Controls.Add(Me.btnGetURL_TEMPLATE)
        Me.GroupBox10.Controls.Add(Me.btnListPlusFriendID)
        Me.GroupBox10.Controls.Add(Me.btnGetURL_PLUSFRIENDID)
        Me.GroupBox10.Location = New System.Drawing.Point(689, 20)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(351, 190)
        Me.GroupBox10.TabIndex = 9
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "카카오톡 관리"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(196, 143)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(147, 35)
        Me.btnSearch.TabIndex = 7
        Me.btnSearch.Text = "전송내역 목록조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnGetURL_BOX
        '
        Me.btnGetURL_BOX.Location = New System.Drawing.Point(196, 102)
        Me.btnGetURL_BOX.Name = "btnGetURL_BOX"
        Me.btnGetURL_BOX.Size = New System.Drawing.Size(147, 35)
        Me.btnGetURL_BOX.TabIndex = 6
        Me.btnGetURL_BOX.Text = "전송내역 조회 팝업 URL"
        Me.btnGetURL_BOX.UseVisualStyleBackColor = True
        '
        'btnGetSenderNumberList
        '
        Me.btnGetSenderNumberList.Location = New System.Drawing.Point(196, 61)
        Me.btnGetSenderNumberList.Name = "btnGetSenderNumberList"
        Me.btnGetSenderNumberList.Size = New System.Drawing.Size(147, 35)
        Me.btnGetSenderNumberList.TabIndex = 5
        Me.btnGetSenderNumberList.Text = "발신번호 목록 확인"
        Me.btnGetSenderNumberList.UseVisualStyleBackColor = True
        '
        'btnGetURL_SENDER
        '
        Me.btnGetURL_SENDER.Location = New System.Drawing.Point(196, 20)
        Me.btnGetURL_SENDER.Name = "btnGetURL_SENDER"
        Me.btnGetURL_SENDER.Size = New System.Drawing.Size(147, 35)
        Me.btnGetURL_SENDER.TabIndex = 4
        Me.btnGetURL_SENDER.Text = "발신번호 관리 팝업 URL"
        Me.btnGetURL_SENDER.UseVisualStyleBackColor = True
        '
        'btnListATSTemplate
        '
        Me.btnListATSTemplate.Location = New System.Drawing.Point(10, 143)
        Me.btnListATSTemplate.Name = "btnListATSTemplate"
        Me.btnListATSTemplate.Size = New System.Drawing.Size(180, 35)
        Me.btnListATSTemplate.TabIndex = 3
        Me.btnListATSTemplate.Text = "알림톡 템플릿 목록 확인"
        Me.btnListATSTemplate.UseVisualStyleBackColor = True
        '
        'btnGetURL_TEMPLATE
        '
        Me.btnGetURL_TEMPLATE.Location = New System.Drawing.Point(10, 102)
        Me.btnGetURL_TEMPLATE.Name = "btnGetURL_TEMPLATE"
        Me.btnGetURL_TEMPLATE.Size = New System.Drawing.Size(180, 35)
        Me.btnGetURL_TEMPLATE.TabIndex = 2
        Me.btnGetURL_TEMPLATE.Text = "알림톡 템플릿 관리 팝업 URL"
        Me.btnGetURL_TEMPLATE.UseVisualStyleBackColor = True
        '
        'btnListPlusFriendID
        '
        Me.btnListPlusFriendID.Location = New System.Drawing.Point(10, 61)
        Me.btnListPlusFriendID.Name = "btnListPlusFriendID"
        Me.btnListPlusFriendID.Size = New System.Drawing.Size(180, 35)
        Me.btnListPlusFriendID.TabIndex = 1
        Me.btnListPlusFriendID.Text = "플러스친구 목록 확인"
        Me.btnListPlusFriendID.UseVisualStyleBackColor = True
        '
        'btnGetURL_PLUSFRIENDID
        '
        Me.btnGetURL_PLUSFRIENDID.Location = New System.Drawing.Point(10, 20)
        Me.btnGetURL_PLUSFRIENDID.Name = "btnGetURL_PLUSFRIENDID"
        Me.btnGetURL_PLUSFRIENDID.Size = New System.Drawing.Size(180, 35)
        Me.btnGetURL_PLUSFRIENDID.TabIndex = 0
        Me.btnGetURL_PLUSFRIENDID.Text = "플러스친구 계정관리 팝업 URL"
        Me.btnGetURL_PLUSFRIENDID.UseVisualStyleBackColor = True
        '
        'btnCancelReserve
        '
        Me.btnCancelReserve.Location = New System.Drawing.Point(365, 180)
        Me.btnCancelReserve.Name = "btnCancelReserve"
        Me.btnCancelReserve.Size = New System.Drawing.Size(98, 33)
        Me.btnCancelReserve.TabIndex = 8
        Me.btnCancelReserve.Text = "예약전송 취소"
        Me.btnCancelReserve.UseVisualStyleBackColor = True
        '
        'btnGetMessages
        '
        Me.btnGetMessages.Location = New System.Drawing.Point(263, 180)
        Me.btnGetMessages.Name = "btnGetMessages"
        Me.btnGetMessages.Size = New System.Drawing.Size(98, 33)
        Me.btnGetMessages.TabIndex = 7
        Me.btnGetMessages.Text = "전송상태 확인"
        Me.btnGetMessages.UseVisualStyleBackColor = True
        '
        'txtReceiptNum
        '
        Me.txtReceiptNum.Location = New System.Drawing.Point(78, 186)
        Me.txtReceiptNum.Name = "txtReceiptNum"
        Me.txtReceiptNum.Size = New System.Drawing.Size(179, 21)
        Me.txtReceiptNum.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "접수번호 : "
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.btnSendFMS_multi)
        Me.GroupBox8.Controls.Add(Me.btnSendFMS_same)
        Me.GroupBox8.Controls.Add(Me.btnSendFMS_one)
        Me.GroupBox8.Location = New System.Drawing.Point(13, 111)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(326, 63)
        Me.GroupBox8.TabIndex = 4
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "친구톡(이미지) 전송"
        '
        'btnSendFMS_multi
        '
        Me.btnSendFMS_multi.Location = New System.Drawing.Point(208, 18)
        Me.btnSendFMS_multi.Name = "btnSendFMS_multi"
        Me.btnSendFMS_multi.Size = New System.Drawing.Size(109, 35)
        Me.btnSendFMS_multi.TabIndex = 2
        Me.btnSendFMS_multi.Text = "개별 1000건 전송"
        Me.btnSendFMS_multi.UseVisualStyleBackColor = True
        '
        'btnSendFMS_same
        '
        Me.btnSendFMS_same.Location = New System.Drawing.Point(93, 18)
        Me.btnSendFMS_same.Name = "btnSendFMS_same"
        Me.btnSendFMS_same.Size = New System.Drawing.Size(109, 35)
        Me.btnSendFMS_same.TabIndex = 1
        Me.btnSendFMS_same.Text = "대량 1000건 전송"
        Me.btnSendFMS_same.UseVisualStyleBackColor = True
        '
        'btnSendFMS_one
        '
        Me.btnSendFMS_one.Location = New System.Drawing.Point(10, 18)
        Me.btnSendFMS_one.Name = "btnSendFMS_one"
        Me.btnSendFMS_one.Size = New System.Drawing.Size(77, 35)
        Me.btnSendFMS_one.TabIndex = 0
        Me.btnSendFMS_one.Text = "1건 전송"
        Me.btnSendFMS_one.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.btnSendFTS_multi)
        Me.GroupBox7.Controls.Add(Me.btnSendFTS_same)
        Me.GroupBox7.Controls.Add(Me.btnSendFTS_one)
        Me.GroupBox7.Location = New System.Drawing.Point(345, 44)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(326, 63)
        Me.GroupBox7.TabIndex = 3
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "친구톡(텍스트) 전송"
        '
        'btnSendFTS_multi
        '
        Me.btnSendFTS_multi.Location = New System.Drawing.Point(208, 18)
        Me.btnSendFTS_multi.Name = "btnSendFTS_multi"
        Me.btnSendFTS_multi.Size = New System.Drawing.Size(109, 35)
        Me.btnSendFTS_multi.TabIndex = 2
        Me.btnSendFTS_multi.Text = "개별 1000건 전송"
        Me.btnSendFTS_multi.UseVisualStyleBackColor = True
        '
        'btnSendFTS_same
        '
        Me.btnSendFTS_same.Location = New System.Drawing.Point(93, 18)
        Me.btnSendFTS_same.Name = "btnSendFTS_same"
        Me.btnSendFTS_same.Size = New System.Drawing.Size(109, 35)
        Me.btnSendFTS_same.TabIndex = 1
        Me.btnSendFTS_same.Text = "대량 1000건 전송"
        Me.btnSendFTS_same.UseVisualStyleBackColor = True
        '
        'btnSendFTS_one
        '
        Me.btnSendFTS_one.Location = New System.Drawing.Point(10, 18)
        Me.btnSendFTS_one.Name = "btnSendFTS_one"
        Me.btnSendFTS_one.Size = New System.Drawing.Size(77, 35)
        Me.btnSendFTS_one.TabIndex = 0
        Me.btnSendFTS_one.Text = "1건 전송"
        Me.btnSendFTS_one.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.btnSendATS_multi)
        Me.GroupBox6.Controls.Add(Me.btnSendATS_same)
        Me.GroupBox6.Controls.Add(Me.btnSendATS_one)
        Me.GroupBox6.Location = New System.Drawing.Point(13, 44)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(326, 63)
        Me.GroupBox6.TabIndex = 2
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "알림톡 전송"
        '
        'btnSendATS_multi
        '
        Me.btnSendATS_multi.Location = New System.Drawing.Point(208, 18)
        Me.btnSendATS_multi.Name = "btnSendATS_multi"
        Me.btnSendATS_multi.Size = New System.Drawing.Size(109, 35)
        Me.btnSendATS_multi.TabIndex = 2
        Me.btnSendATS_multi.Text = "개별 1000건 전송"
        Me.btnSendATS_multi.UseVisualStyleBackColor = True
        '
        'btnSendATS_same
        '
        Me.btnSendATS_same.Location = New System.Drawing.Point(93, 18)
        Me.btnSendATS_same.Name = "btnSendATS_same"
        Me.btnSendATS_same.Size = New System.Drawing.Size(109, 35)
        Me.btnSendATS_same.TabIndex = 1
        Me.btnSendATS_same.Text = "대량 1000건 전송"
        Me.btnSendATS_same.UseVisualStyleBackColor = True
        '
        'btnSendATS_one
        '
        Me.btnSendATS_one.Location = New System.Drawing.Point(10, 18)
        Me.btnSendATS_one.Name = "btnSendATS_one"
        Me.btnSendATS_one.Size = New System.Drawing.Size(77, 35)
        Me.btnSendATS_one.TabIndex = 0
        Me.btnSendATS_one.Text = "1건 전송"
        Me.btnSendATS_one.UseVisualStyleBackColor = True
        '
        'txtReserveDT
        '
        Me.txtReserveDT.Location = New System.Drawing.Point(228, 17)
        Me.txtReserveDT.Name = "txtReserveDT"
        Me.txtReserveDT.Size = New System.Drawing.Size(205, 21)
        Me.txtReserveDT.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(215, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "예약전송시간(yyyyMMddHHmmss) : "
        '
        'fileDialog
        '
        Me.fileDialog.FileName = "fileDialob"
        '
        'dataGrid1
        '
        Me.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGrid1.Location = New System.Drawing.Point(11, 219)
        Me.dataGrid1.Name = "dataGrid1"
        Me.dataGrid1.RowTemplate.Height = 21
        Me.dataGrid1.Size = New System.Drawing.Size(1062, 123)
        Me.dataGrid1.TabIndex = 25
        '
        'frmExample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1109, 609)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCorpNum)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmExample"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "팝빌 카카오톡 API SDK VB Example"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.dataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents btnGetChargeInfo_FMS As System.Windows.Forms.Button
    Friend WithEvents btnGetChargeInfo_FTS As System.Windows.Forms.Button
    Friend WithEvents btnGetUnitCost_FMS As System.Windows.Forms.Button
    Friend WithEvents btnGetUnitCost_FTS As System.Windows.Forms.Button
    Friend WithEvents btnGetChargeInfo_ATS As System.Windows.Forms.Button
    Friend WithEvents btnUnitCost_ATS As System.Windows.Forms.Button
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCheckID As System.Windows.Forms.Button
    Friend WithEvents btnCheckIsMember As System.Windows.Forms.Button
    Friend WithEvents btnJoinMember As System.Windows.Forms.Button
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCorpNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtReserveDT As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSendATS_one As System.Windows.Forms.Button
    Friend WithEvents btnSendATS_same As System.Windows.Forms.Button
    Friend WithEvents btnSendATS_multi As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSendFTS_multi As System.Windows.Forms.Button
    Friend WithEvents btnSendFTS_same As System.Windows.Forms.Button
    Friend WithEvents btnSendFTS_one As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSendFMS_multi As System.Windows.Forms.Button
    Friend WithEvents btnSendFMS_same As System.Windows.Forms.Button
    Friend WithEvents btnSendFMS_one As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtReceiptNum As System.Windows.Forms.TextBox
    Friend WithEvents btnGetMessages As System.Windows.Forms.Button
    Friend WithEvents btnCancelReserve As System.Windows.Forms.Button
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetURL_PLUSFRIENDID As System.Windows.Forms.Button
    Friend WithEvents btnListPlusFriendID As System.Windows.Forms.Button
    Friend WithEvents btnListATSTemplate As System.Windows.Forms.Button
    Friend WithEvents btnGetURL_TEMPLATE As System.Windows.Forms.Button
    Friend WithEvents btnGetURL_SENDER As System.Windows.Forms.Button
    Friend WithEvents btnGetSenderNumberList As System.Windows.Forms.Button
    Friend WithEvents btnGetURL_BOX As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents fileDialog As System.Windows.Forms.OpenFileDialog
    Private WithEvents dataGrid1 As System.Windows.Forms.DataGridView

End Class