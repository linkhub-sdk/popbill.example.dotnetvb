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
        Me.GroupBox15 = New System.Windows.Forms.GroupBox
        Me.btnGetAccessURL = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btnGetPartnerURL = New System.Windows.Forms.Button
        Me.btnGetPartnerBalance = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnRefund = New System.Windows.Forms.Button
        Me.btnGetRefundHistory = New System.Windows.Forms.Button
        Me.btnGetUseHistoryURL = New System.Windows.Forms.Button
        Me.btnGetChargeInfo = New System.Windows.Forms.Button
        Me.btnGetPaymentURL = New System.Windows.Forms.Button
        Me.btnUnitCost = New System.Windows.Forms.Button
        Me.btnGetChargeURL = New System.Windows.Forms.Button
        Me.btnPaymentRequest = New System.Windows.Forms.Button
        Me.btnGetRefundableBalance = New System.Windows.Forms.Button
        Me.btnGetRefundInfo = New System.Windows.Forms.Button
        Me.btnGetUseHistory = New System.Windows.Forms.Button
        Me.btnGetSettleResult = New System.Windows.Forms.Button
        Me.btnGetPaymentHistory = New System.Windows.Forms.Button
        Me.btnGetBalance = New System.Windows.Forms.Button
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
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCorpNum = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.btnCheckBizInfo = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtTargetCorpNum = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.btnDeleteContact = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox15)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 46)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1007, 330)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "팝빌 기본 API"
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.btnGetAccessURL)
        Me.GroupBox15.Location = New System.Drawing.Point(763, 20)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(162, 156)
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
        Me.GroupBox5.Location = New System.Drawing.Point(626, 20)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(131, 155)
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
        Me.btnGetPartnerBalance.Location = New System.Drawing.Point(6, 19)
        Me.btnGetPartnerBalance.Name = "btnGetPartnerBalance"
        Me.btnGetPartnerBalance.Size = New System.Drawing.Size(118, 30)
        Me.btnGetPartnerBalance.TabIndex = 3
        Me.btnGetPartnerBalance.Text = "파트너포인트 확인"
        Me.btnGetPartnerBalance.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnRefund)
        Me.GroupBox3.Controls.Add(Me.btnGetRefundHistory)
        Me.GroupBox3.Controls.Add(Me.btnGetUseHistoryURL)
        Me.GroupBox3.Controls.Add(Me.btnGetChargeInfo)
        Me.GroupBox3.Controls.Add(Me.btnGetPaymentURL)
        Me.GroupBox3.Controls.Add(Me.btnUnitCost)
        Me.GroupBox3.Controls.Add(Me.btnGetChargeURL)
        Me.GroupBox3.Controls.Add(Me.btnPaymentRequest)
        Me.GroupBox3.Controls.Add(Me.btnGetRefundableBalance)
        Me.GroupBox3.Controls.Add(Me.btnGetRefundInfo)
        Me.GroupBox3.Controls.Add(Me.btnGetUseHistory)
        Me.GroupBox3.Controls.Add(Me.btnGetSettleResult)
        Me.GroupBox3.Controls.Add(Me.btnGetPaymentHistory)
        Me.GroupBox3.Controls.Add(Me.btnGetBalance)
        Me.GroupBox3.Location = New System.Drawing.Point(297, 20)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(323, 304)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "포인트 관련"
        '
        'btnRefund
        '
        Me.btnRefund.Location = New System.Drawing.Point(6, 248)
        Me.btnRefund.Name = "btnRefund"
        Me.btnRefund.Size = New System.Drawing.Size(160, 32)
        Me.btnRefund.TabIndex = 0
        Me.btnRefund.Text = "포인트 환불신청"
        '
        'btnGetRefundHistory
        '
        Me.btnGetRefundHistory.Location = New System.Drawing.Point(172, 248)
        Me.btnGetRefundHistory.Name = "btnGetRefundHistory"
        Me.btnGetRefundHistory.Size = New System.Drawing.Size(135, 32)
        Me.btnGetRefundHistory.TabIndex = 0
        Me.btnGetRefundHistory.Text = "포인트 환불내역 확인"
        '
        'btnGetUseHistoryURL
        '
        Me.btnGetUseHistoryURL.Location = New System.Drawing.Point(172, 134)
        Me.btnGetUseHistoryURL.Name = "btnGetUseHistoryURL"
        Me.btnGetUseHistoryURL.Size = New System.Drawing.Size(135, 32)
        Me.btnGetUseHistoryURL.TabIndex = 7
        Me.btnGetUseHistoryURL.Text = "포인트 사용내역 URL"
        Me.btnGetUseHistoryURL.UseVisualStyleBackColor = True
        '
        'btnGetChargeInfo
        '
        Me.btnGetChargeInfo.Location = New System.Drawing.Point(6, 20)
        Me.btnGetChargeInfo.Name = "btnGetChargeInfo"
        Me.btnGetChargeInfo.Size = New System.Drawing.Size(160, 32)
        Me.btnGetChargeInfo.TabIndex = 4
        Me.btnGetChargeInfo.Text = "과금정보 확인"
        Me.btnGetChargeInfo.UseVisualStyleBackColor = True
        '
        'btnGetPaymentURL
        '
        Me.btnGetPaymentURL.Location = New System.Drawing.Point(172, 96)
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
        Me.btnUnitCost.Size = New System.Drawing.Size(160, 32)
        Me.btnUnitCost.TabIndex = 3
        Me.btnUnitCost.Text = "요금 단가 확인"
        Me.btnUnitCost.UseVisualStyleBackColor = True
        '
        'btnGetChargeURL
        '
        Me.btnGetChargeURL.Location = New System.Drawing.Point(172, 58)
        Me.btnGetChargeURL.Name = "btnGetChargeURL"
        Me.btnGetChargeURL.Size = New System.Drawing.Size(135, 32)
        Me.btnGetChargeURL.TabIndex = 5
        Me.btnGetChargeURL.Text = "포인트 충전 URL"
        Me.btnGetChargeURL.UseVisualStyleBackColor = True
        '
        'btnPaymentRequest
        '
        Me.btnPaymentRequest.Location = New System.Drawing.Point(6, 96)
        Me.btnPaymentRequest.Name = "btnPaymentRequest"
        Me.btnPaymentRequest.Size = New System.Drawing.Size(160, 32)
        Me.btnPaymentRequest.TabIndex = 8
        Me.btnPaymentRequest.Text = "무통장 입금신청"
        '
        'btnGetRefundableBalance
        '
        Me.btnGetRefundableBalance.Location = New System.Drawing.Point(6, 210)
        Me.btnGetRefundableBalance.Name = "btnGetRefundableBalance"
        Me.btnGetRefundableBalance.Size = New System.Drawing.Size(160, 32)
        Me.btnGetRefundableBalance.TabIndex = 0
        Me.btnGetRefundableBalance.Text = "환불 가능 포인트 조회"
        '
        'btnGetRefundInfo
        '
        Me.btnGetRefundInfo.Location = New System.Drawing.Point(6, 172)
        Me.btnGetRefundInfo.Name = "btnGetRefundInfo"
        Me.btnGetRefundInfo.Size = New System.Drawing.Size(160, 32)
        Me.btnGetRefundInfo.TabIndex = 0
        Me.btnGetRefundInfo.Text = "환불 신청 상태 조회"
        '
        'btnGetUseHistory
        '
        Me.btnGetUseHistory.Location = New System.Drawing.Point(172, 210)
        Me.btnGetUseHistory.Name = "btnGetUseHistory"
        Me.btnGetUseHistory.Size = New System.Drawing.Size(135, 32)
        Me.btnGetUseHistory.TabIndex = 0
        Me.btnGetUseHistory.Text = "포인트 사용내역 확인"
        '
        'btnGetSettleResult
        '
        Me.btnGetSettleResult.Location = New System.Drawing.Point(6, 134)
        Me.btnGetSettleResult.Name = "btnGetSettleResult"
        Me.btnGetSettleResult.Size = New System.Drawing.Size(160, 32)
        Me.btnGetSettleResult.TabIndex = 0
        Me.btnGetSettleResult.Text = "무통장 입금신청 정보확인"
        '
        'btnGetPaymentHistory
        '
        Me.btnGetPaymentHistory.Location = New System.Drawing.Point(172, 172)
        Me.btnGetPaymentHistory.Name = "btnGetPaymentHistory"
        Me.btnGetPaymentHistory.Size = New System.Drawing.Size(135, 32)
        Me.btnGetPaymentHistory.TabIndex = 0
        Me.btnGetPaymentHistory.Text = "포인트 결제내역 확인"
        '
        'btnGetBalance
        '
        Me.btnGetBalance.Location = New System.Drawing.Point(172, 20)
        Me.btnGetBalance.Name = "btnGetBalance"
        Me.btnGetBalance.Size = New System.Drawing.Size(135, 32)
        Me.btnGetBalance.TabIndex = 2
        Me.btnGetBalance.Text = "잔여포인트 확인"
        Me.btnGetBalance.UseVisualStyleBackColor = True
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
        Me.GroupBox6.Size = New System.Drawing.Size(285, 240)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "회원 정보"
        '
        'btnUpdateCorpInfo
        '
        Me.btnUpdateCorpInfo.Location = New System.Drawing.Point(6, 202)
        Me.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo"
        Me.btnUpdateCorpInfo.Size = New System.Drawing.Size(130, 32)
        Me.btnUpdateCorpInfo.TabIndex = 8
        Me.btnUpdateCorpInfo.Text = "회사정보 수정"
        Me.btnUpdateCorpInfo.UseVisualStyleBackColor = True
        '
        'btnGetContactInfo
        '
        Me.btnGetContactInfo.Location = New System.Drawing.Point(142, 58)
        Me.btnGetContactInfo.Name = "btnGetContactInfo"
        Me.btnGetContactInfo.Size = New System.Drawing.Size(130, 32)
        Me.btnGetContactInfo.TabIndex = 8
        Me.btnGetContactInfo.Text = "담당자 정보 확인"
        Me.btnGetContactInfo.UseVisualStyleBackColor = True
        '
        'btnGetCorpInfo
        '
        Me.btnGetCorpInfo.Location = New System.Drawing.Point(6, 166)
        Me.btnGetCorpInfo.Name = "btnGetCorpInfo"
        Me.btnGetCorpInfo.Size = New System.Drawing.Size(130, 32)
        Me.btnGetCorpInfo.TabIndex = 7
        Me.btnGetCorpInfo.Text = "회사정보 조회"
        Me.btnGetCorpInfo.UseVisualStyleBackColor = True
        '
        'btnCheckID
        '
        Me.btnCheckID.Location = New System.Drawing.Point(6, 56)
        Me.btnCheckID.Name = "btnCheckID"
        Me.btnCheckID.Size = New System.Drawing.Size(130, 32)
        Me.btnCheckID.TabIndex = 3
        Me.btnCheckID.Text = "ID 중복 확인"
        Me.btnCheckID.UseVisualStyleBackColor = True
        '
        'btnUpdateContact
        '
        Me.btnUpdateContact.Location = New System.Drawing.Point(142, 128)
        Me.btnUpdateContact.Name = "btnUpdateContact"
        Me.btnUpdateContact.Size = New System.Drawing.Size(130, 32)
        Me.btnUpdateContact.TabIndex = 7
        Me.btnUpdateContact.Text = "담당자 정보 수정"
        Me.btnUpdateContact.UseVisualStyleBackColor = True
        '
        'btnCheckIsMember
        '
        Me.btnCheckIsMember.Location = New System.Drawing.Point(6, 20)
        Me.btnCheckIsMember.Name = "btnCheckIsMember"
        Me.btnCheckIsMember.Size = New System.Drawing.Size(130, 32)
        Me.btnCheckIsMember.TabIndex = 2
        Me.btnCheckIsMember.Text = "가입여부 확인"
        Me.btnCheckIsMember.UseVisualStyleBackColor = True
        '
        'btnListContact
        '
        Me.btnListContact.Location = New System.Drawing.Point(142, 94)
        Me.btnListContact.Name = "btnListContact"
        Me.btnListContact.Size = New System.Drawing.Size(130, 32)
        Me.btnListContact.TabIndex = 6
        Me.btnListContact.Text = "담당자 목록 조회"
        Me.btnListContact.UseVisualStyleBackColor = True
        '
        'btnJoinMember
        '
        Me.btnJoinMember.Location = New System.Drawing.Point(6, 92)
        Me.btnJoinMember.Name = "btnJoinMember"
        Me.btnJoinMember.Size = New System.Drawing.Size(130, 32)
        Me.btnJoinMember.TabIndex = 1
        Me.btnJoinMember.Text = "회원 가입"
        Me.btnJoinMember.UseVisualStyleBackColor = True
        '
        'btnRegistContact
        '
        Me.btnRegistContact.Location = New System.Drawing.Point(142, 20)
        Me.btnRegistContact.Name = "btnRegistContact"
        Me.btnRegistContact.Size = New System.Drawing.Size(130, 32)
        Me.btnRegistContact.TabIndex = 5
        Me.btnRegistContact.Text = "담당자 추가"
        Me.btnRegistContact.UseVisualStyleBackColor = True
        '
        'btnQuitMember
        '
        Me.btnQuitMember.Location = New System.Drawing.Point(6, 128)
        Me.btnQuitMember.Name = "btnQuitMember"
        Me.btnQuitMember.Size = New System.Drawing.Size(130, 32)
        Me.btnQuitMember.TabIndex = 23
        Me.btnQuitMember.Text = "팝빌 회원 탈퇴"
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(412, 15)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(143, 21)
        Me.txtUserId.TabIndex = 33
        Me.txtUserId.Text = "testkorea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(307, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 12)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "팝빌회원 아이디 :"
        '
        'txtCorpNum
        '
        Me.txtCorpNum.Location = New System.Drawing.Point(143, 16)
        Me.txtCorpNum.Name = "txtCorpNum"
        Me.txtCorpNum.Size = New System.Drawing.Size(143, 21)
        Me.txtCorpNum.TabIndex = 31
        Me.txtCorpNum.Text = "1234567890"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 12)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "팝빌회원 사업자번호 : "
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnCheckBizInfo)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.txtTargetCorpNum)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 382)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(547, 65)
        Me.GroupBox4.TabIndex = 34
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "기업정보조회 API"
        '
        'btnCheckBizInfo
        '
        Me.btnCheckBizInfo.Location = New System.Drawing.Point(248, 18)
        Me.btnCheckBizInfo.Name = "btnCheckBizInfo"
        Me.btnCheckBizInfo.Size = New System.Drawing.Size(97, 32)
        Me.btnCheckBizInfo.TabIndex = 2
        Me.btnCheckBizInfo.Text = "단건조회"
        Me.btnCheckBizInfo.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 12)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "조회 사업자번호 :"
        '
        'txtTargetCorpNum
        '
        Me.txtTargetCorpNum.Location = New System.Drawing.Point(117, 23)
        Me.txtTargetCorpNum.Name = "txtTargetCorpNum"
        Me.txtTargetCorpNum.Size = New System.Drawing.Size(122, 21)
        Me.txtTargetCorpNum.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(607, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "응답 URL :"
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(677, 15)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(279, 21)
        Me.txtURL.TabIndex = 4
        '
        'btnDeleteContact
        '
        Me.btnDeleteContact.Location = New System.Drawing.Point(142, 166)
        Me.btnDeleteContact.Name = "btnDeleteContact"
        Me.btnDeleteContact.Size = New System.Drawing.Size(130, 32)
        Me.btnDeleteContact.TabIndex = 25
        Me.btnDeleteContact.Text = "담당자 삭제"
        Me.btnDeleteContact.UseVisualStyleBackColor = True
        '
        'frmExample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 482)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCorpNum)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmExample"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "팝빌 기업정보조회 API VB.Net SDK Example"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
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
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCorpNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTargetCorpNum As System.Windows.Forms.TextBox
    Friend WithEvents btnCheckBizInfo As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents btnGetUseHistoryURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPaymentURL As System.Windows.Forms.Button
    Friend WithEvents btnGetContactInfo As System.Windows.Forms.Button
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
