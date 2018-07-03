﻿'=========================================================================
'
' 팝빌 문자 API VB.Net SDK Example
'
' - VB.Net 연동환경 설정방법 안내 : http://blog.linkhub.co.kr/569/
' - 업데이트 일자 : 2018-07-03
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
'
' <테스트 연동개발 준비사항>
' - 18, 21번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
'=========================================================================

Public Class frmExample

    '링크아이디
    Private LinkID As String = "TESTER"

    '비밀키
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '카카오톡 서비스 클래스 선언
    Private kakaoService As KakaoService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '카카오톡 서비스 클래스 초기화
        kakaoService = New KakaoService(LinkID, SecretKey)

        '연동환경 설정값 (True-개발용, False-상업용)
        kakaoService.IsTest = True
    End Sub

    Private Function getReserveDT() As DateTime?
        If String.IsNullOrEmpty(txtReserveDT.Text) = False Then

            Return DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
        End If

    End Function

    '=========================================================================
    ' 해당사업자의 회원가입 여부를 확인합니다.
    ' - 사업자번호는 '-'를 제외한 10자리 숫자 문자열입니다.
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = kakaoService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 회원아이디 중복여부를 확인합니다.
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = kakaoService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 신규가입을 요청합니다.
    '=========================================================================
    Private Sub btnJoinMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJoinMember.Click
        Dim joinInfo As JoinForm = New JoinForm

        '링크아이디
        joinInfo.LinkID = LinkID

        '사업자번호, '-'제외 10자리
        joinInfo.CorpNum = "0000000105"

        '대표자성명
        joinInfo.CEOName = "대표자성명"

        '상호
        joinInfo.CorpName = "상호"

        '주소
        joinInfo.Addr = "주소"

        '업태
        joinInfo.BizType = "업태"

        '종목
        joinInfo.BizClass = "종목"

        '아이디
        joinInfo.ID = "userid1120"

        '비밀번호
        joinInfo.PWD = "pwd_must_be_long_enough"

        '담당자명
        joinInfo.ContactName = "담당자명"

        '담당자 연락처
        joinInfo.ContactTEL = "02-999-9999"

        '담당자 휴대폰번호
        joinInfo.ContactHP = "010-1234-5678"

        '담당자 메일주소
        joinInfo.ContactEmail = "test@test.com"

        Try
            Dim response As Response = kakaoService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 잔여포인트를 확인합니다.
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click

        Try
            Dim remainPoint As Double = kakaoService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트충전 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_CHRG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_CHRG.Click
        Try
            Dim url As String = kakaoService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = kakaoService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트충전 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            Dim url As String = kakaoService.GetPartnerURL(txtCorpNum.Text, "CHRG")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 로그인 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_LOGIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_LOGIN.Click
        Try
            Dim url As String = kakaoService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디
        joinData.id = "testkorea1120"

        '비밀번호
        joinData.pwd = "password"

        '담당자명
        joinData.personName = "담당자명"

        '연락처
        joinData.tel = "070-1111-2222"

        '휴대폰번호
        joinData.hp = "010-1234-1234"

        '이메일
        joinData.email = "test@test.com"

        '회사조회 권한여부, True-회사조회, False-개인조회
        joinData.searchAllAllowYN = False

        Try
            Dim response As Response = kakaoService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = kakaoService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "아이디 | 담당자명 | 메일주소 | 휴대폰번호 | 팩스 | 연락처 | 등록일시 | 회사조회 여부 | 관리자 여부 | 상태" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.email + " | " + info.hp + " | " + info.fax + " | " + info.tel + " | "
                tmp += info.regDT.ToString() + " | " + info.searchAllAllowYN.ToString() + " | " + info.mgrYN.ToString() + " | " + info.state + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContact.Click

        '담당자 정보객체
        Dim joinData As New Contact


        '아이디
        joinData.id = "testkorea1120"

        '담당자명
        joinData.personName = "담당자명"

        '연락처
        joinData.tel = "070-1111-2222"

        '휴대폰번호
        joinData.hp = "010-1234-1234"

        '이메일
        joinData.email = "test@test.com"

        '회사조회 권한여부, True-회사조회, False-개인조회
        joinData.searchAllAllowYN = False

        Try
            Dim response As Response = kakaoService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCorpInfo.Click
        Try
            Dim corpInfo As CorpInfo = kakaoService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "ceoname(대표자성명) : " + corpInfo.ceoname + vbCrLf
            tmp += "corpName(상호) : " + corpInfo.corpName + vbCrLf
            tmp += "addr(주소) : " + corpInfo.addr + vbCrLf
            tmp += "bizType(업태) : " + corpInfo.bizType + vbCrLf
            tmp += "bizClass(종목) : " + corpInfo.bizClass + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnUpdateCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCorpInfo.Click

        Dim corpInfo As New CorpInfo

        '대표자명
        corpInfo.ceoname = "대표자명_수정"

        '상호
        corpInfo.corpName = "상호_수정"

        '주소
        corpInfo.addr = "주소_수정"

        '업태
        corpInfo.bizType = "업태_수정"

        '종목
        corpInfo.bizClass = "종목_수정"

        Try

            Dim response As Response = kakaoService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 알림톡(ATS) 전송단가를 확인합니다
    '=========================================================================
    Private Sub btnUnitCost_SMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost_ATS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.ATS

        Try
            Dim unitCost As Single = kakaoService.GetUnitCost(txtCorpNum.Text, kType)

            MsgBox("알림톡 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 친구톡 텍스트(FTS) 전송단가를 조회합니다.
    '=========================================================================
    Private Sub btnGetUnitCost_FTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost_FTS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.FTS

        Try
            Dim unitCost As Single = kakaoService.GetUnitCost(txtCorpNum.Text, kType)

            MsgBox("친구톡 텍스트 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 친구톡 이미지(FMS) 전송단가를 조회합니다.
    '=========================================================================
    Private Sub btnGetUnitCost_FMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost_FMS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.FMS

        Try
            Dim unitCost As Single = kakaoService.GetUnitCost(txtCorpNum.Text, kType)

            MsgBox("친구톡 이미지 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 알림톡(ATS) 과금정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetChargeInfo_ATS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo_ATS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.ATS

        Try
            Dim ChargeInfo As ChargeInfo = kakaoService.GetChargeInfo(txtCorpNum.Text, kType)

            Dim tmp As String = "unitCost (전송단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 친구톡 텍스트(FTS) 과금정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetChargeInfo_FTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo_FTS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.FTS

        Try
            Dim ChargeInfo As ChargeInfo = kakaoService.GetChargeInfo(txtCorpNum.Text, kType)

            Dim tmp As String = "unitCost (전송단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 친구톡 이미지(FMS) 과금정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetChargeInfo_FMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo_FMS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.FMS

        Try
            Dim ChargeInfo As ChargeInfo = kakaoService.GetChargeInfo(txtCorpNum.Text, kType)

            Dim tmp As String = "unitCost (전송단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendATS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendATS_same.Click

        '알림톡 템플릿 코드, 알림톡 템플릿 목록확인(ListATSTemplate) API 반환항목중 templateCode로 확인
        Dim templateCode As String = "018020000001"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042993"

        '알림톡 템플릿 내용, 최대 1000자
        Dim content As String = "[테스트] 테스트 템플릿입니다."

        '대체문자 메시지 내용
        Dim altContent As String = "대체문자 메시지 내용"

        '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"


        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "010111222" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            receiverList.Add(msg)
        Next
        
        Try
            Dim receiptNum As String = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, content, altContent, _
                                                            altSendType, getReserveDT(), receiverList)
            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    Private Sub btnSendATS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendATS_one.Click

        '알림톡 템플릿 코드, 알림톡 템플릿 목록확인(ListATSTemplate) API 반환항목중 templateCode로 확인
        Dim templateCode As String = "018020000001"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042993"

        '알림톡 템플릿 내용, 최대 1000자
        Dim content As String = "[테스트] 테스트 템플릿입니다."

        '대체문자 메시지 내용
        Dim altContent As String = "대체문자 메시지 내용"

        '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '수신번호
        Dim receiveNum = "010111222"

        '수신자명
        Dim receiveName = "수신자명"

        Try
            Dim receiptNum As String = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, altSendType, getReserveDT(), _
                receiveNum, receiveName, content, altContent)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendATS_multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendATS_multi.Click

        '알림톡 템플릿 코드, 알림톡 템플릿 목록확인(ListATSTemplate) API 반환항목중 templateCode로 확인
        Dim templateCode As String = "018020000001"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042993"

        '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "010111222" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            msg.msg = "[테스트] 테스트 템플릿입니다." '알림톡 템플릿 내용, 최대 1000자
            msg.altmsg = "대체문자 메시지 내용" '대체문자 내용
            receiverList.Add(msg)
        Next

        Try
            Dim receiptNum As String = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, altSendType, _
                                                            getReserveDT(), receiverList)
            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendFTS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFTS_one.Click

        '플러스친구 아이디, 플러스친구 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
        Dim plusFriendID As String = "@팝빌"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042993"

        '수신번호
        Dim receiverNum As String = "010111222"

        '수신자명
        Dim receiverName As String = "수신자명"

        '친구톡 내용, 최대 1000자
        Dim content As String = "친구톡 내용입니다."

        '대체문자 메시지 내용
        Dim altContent As String = "대체문자 메시지 내용입니다."

        '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '광고전송 여부
        Dim adsYN As Boolean = True

        '버튼 배열 최대 5개
        Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

        Dim btnInfo As KakaoButton = New KakaoButton
        btnInfo.n = "버튼명"
        btnInfo.t = "WL"
        btnInfo.u1 = "http://www.linkhub.co.kr"
        btnInfo.u2 = "http://www.popbill.co.kr"
        buttonList.Add(btnInfo)
        
        Try
            Dim receiptNum As String = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content, altContent, altSendType, _
                                                            receiverNum, receiverName, adsYN, getReserveDT(), buttonList)
            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    Private Sub btnSendFTS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFTS_same.Click

        '플러스친구 아이디, 플러스친구 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
        Dim plusFriendID As String = "@팝빌"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042993"

        '친구톡 내용, 최대 1000자
        Dim content As String = "친구톡 내용입니다."

        '대체문자 메시지 내용
        Dim altContent As String = "대체문자 메시지 내용입니다."

        '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '광고전송 여부
        Dim adsYN As Boolean = True


        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "010111222" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            receiverList.Add(msg)
        Next


        '버튼 배열 최대 5개
        Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

        Dim btnInfo As KakaoButton = New KakaoButton
        btnInfo.n = "버튼명"
        btnInfo.t = "WL"
        btnInfo.u1 = "http://www.linkhub.co.kr"
        btnInfo.u2 = "http://www.popbill.co.kr"
        buttonList.Add(btnInfo)

        Try
            Dim receiptNum As String = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content, altContent, altSendType, _
                                                            adsYN, getReserveDT(), receiverList, buttonList)
            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    Private Sub btnSendFTS_multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFTS_multi.Click

        '플러스친구 아이디, 플러스친구 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
        Dim plusFriendID As String = "@팝빌"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042993"

        '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '광고전송 여부
        Dim adsYN As Boolean = True


        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "010111222" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            msg.msg = "친구톡 내용입니다." + CStr(i) '친구톡 내용, 최대 1000자
            msg.altmsg = "대체문자 메시지 내용" + CStr(i) '대체문자 내용
            receiverList.Add(msg)
        Next


        '버튼 배열 최대 5개
        Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

        Dim btnInfo As KakaoButton = New KakaoButton
        btnInfo.n = "버튼명"
        btnInfo.t = "WL"
        btnInfo.u1 = "http://www.linkhub.co.kr"
        btnInfo.u2 = "http://www.popbill.co.kr"
        buttonList.Add(btnInfo)

        Try
            Dim receiptNum As String = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, _
                         altSendType, adsYN, getReserveDT(), receiverList, buttonList)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendFMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFMS_one.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            '플러스친구 아이디, 플러스친구 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
            Dim plusFriendID As String = "@팝빌"

            '팝빌에 사전등록된 발신번호
            Dim senderNum As String = "07043042993"

            '수신번호
            Dim receiverNum As String = "010111222"

            '수신자명
            Dim receiverName As String = "수신자명"

            '친구톡 내용, 최대 400자
            Dim content As String = "친구톡 내용입니다."

            '대체문자 메시지 내용
            Dim altContent As String = "대체문자 메시지 내용입니다."

            '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
            Dim altSendType = "A"

            '광고전송 여부
            Dim adsYN As Boolean = True

            '첨부 이미지 파일경로
            Dim strFileName As String = fileDialog.FileName

            '이미지 링크 URL
            Dim imageURL As String = "www.popbill.com"


            '버튼 배열 최대 5개
            Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

            Dim btnInfo As KakaoButton = New KakaoButton
            btnInfo.n = "버튼명"
            btnInfo.t = "WL"
            btnInfo.u1 = "http://www.linkhub.co.kr"
            btnInfo.u2 = "http://www.popbill.co.kr"
            buttonList.Add(btnInfo)


            Try
                Dim receiptNum As String = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, _
                                                                content, altContent, altSendType, receiverNum, _
                                                                receiverName, adsYN, getReserveDT(), buttonList, strFileName, imageURL)
                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    Private Sub btnSendFMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFMS_same.Click

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            '플러스친구 아이디, 플러스친구 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
            Dim plusFriendID As String = "@팝빌"

            '팝빌에 사전등록된 발신번호
            Dim senderNum As String = "07043042993"

            '친구톡 내용, 최대 400자
            Dim content As String = "친구톡 내용입니다."

            '대체문자 메시지 내용
            Dim altContent As String = "대체문자 메시지 내용입니다."

            '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
            Dim altSendType = "A"

            '광고전송 여부
            Dim adsYN As Boolean = True

            '첨부 이미지 파일경로
            Dim strFileName As String = fileDialog.FileName

            '이미지 링크 URL
            Dim imageURL As String = "www.popbill.com"


            '전송정보 배열, 최대 1000건
            Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

            For i As Integer = 0 To 5
                Dim msg As KakaoReceiver = New KakaoReceiver
                msg.rcv = "010111222" '수신번호
                msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
                receiverList.Add(msg)
            Next


            '버튼 배열 최대 5개
            Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

            Dim btnInfo As KakaoButton = New KakaoButton
            btnInfo.n = "버튼명"
            btnInfo.t = "WL"
            btnInfo.u1 = "http://www.linkhub.co.kr"
            btnInfo.u2 = "http://www.popbill.co.kr"
            buttonList.Add(btnInfo)


            Try
                Dim receiptNum As String = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, content, altContent, altSendType, _
                                                                adsYN, getReserveDT(), receiverList, buttonList, strFileName, imageURL)
                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
            End Try
        End If

    End Sub

    Private Sub btnSendFMS_multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFMS_multi.Click

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            '플러스친구 아이디, 플러스친구 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
            Dim plusFriendID As String = "@팝빌"

            '팝빌에 사전등록된 발신번호
            Dim senderNum As String = "07043042993"

            '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
            Dim altSendType = "A"

            '광고전송 여부
            Dim adsYN As Boolean = True

            '첨부 이미지 파일경로
            Dim strFileName As String = fileDialog.FileName

            '이미지 링크 URL
            Dim imageURL As String = "www.popbill.com"


            '전송정보 배열, 최대 1000건
            Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

            For i As Integer = 0 To 5
                Dim msg As KakaoReceiver = New KakaoReceiver
                msg.rcv = "010111222" '수신번호
                msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
                msg.msg = "친구톡 내용입니다." + CStr(i) '친구톡 내용, 최대 400자
                msg.altmsg = "대체문자 메시지 내용" + CStr(i) '대체문자 내용
                receiverList.Add(msg)
            Next


            '버튼 배열 최대 5개
            Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

            Dim btnInfo As KakaoButton = New KakaoButton
            btnInfo.n = "버튼명"
            btnInfo.t = "WL"
            btnInfo.u1 = "http://www.linkhub.co.kr"
            btnInfo.u2 = "http://www.popbill.co.kr"
            buttonList.Add(btnInfo)

            Try
                Dim receiptNum As String = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, _
                             altSendType, adsYN, getReserveDT(), receiverList, buttonList, strFileName, imageURL)
                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
            End Try

        End If

    End Sub

    '=========================================================================
    ' 플러스친구 계정관리 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetURL_PLUSFRIENDID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_PLUSFRIENDID.Click
        Try
            Dim url As String = kakaoService.GetURL(txtCorpNum.Text, txtUserId.Text, "PLUSFRIEND")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 발신번호 관리 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetURL_SENDER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_SENDER.Click
        Try
            Dim url As String = kakaoService.GetURL(txtCorpNum.Text, txtUserId.Text, "SENDER")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 알림톡 템플릿 관리 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetURL_TEMPLATE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TEMPLATE.Click
        Try
            Dim url As String = kakaoService.GetURL(txtCorpNum.Text, txtUserId.Text, "TEMPLATE")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 카카오톡 전송내역 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetURL_BOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_BOX.Click
        Try
            Dim url As String = kakaoService.GetURL(txtCorpNum.Text, txtUserId.Text, "BOX")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록된 플러스친구 계정 목록을 반환한다.
    '=========================================================================
    Private Sub btnListPlusFriendID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListPlusFriendID.Click
        Try
            Dim plusFriendList As List(Of PlusFriend) = kakaoService.ListPlusFriendID(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "[플러스친구 아이디 | 플러스친구 이름 | 등록일시]" + vbCrLf

            For Each info As PlusFriend In plusFriendList
                tmp += info.plusFriendID + " | " + info.plusFriendName + " | " + info.regDT + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록된 발신번호 목록을 반환한다.
    '=========================================================================
    Private Sub btnGetSenderNumberList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSenderNumberList.Click
        Try
            Dim senderNumberList As List(Of SenderNumber) = kakaoService.GetSenderNumberList(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "[발신번호 | 등록상태 | 대표번호 지정여부]" + vbCrLf

            For Each info As SenderNumber In senderNumberList
                tmp += info.number + " | " + CStr(info.state) + " | " + CStr(info.representYN) + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' (주)카카오로부터 심사후 승인된 알림톡 템플릿 목록을 반환한다.
    '=========================================================================
    Private Sub btnListATSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListATSTemplate.Click
        Try
            Dim templateList As List(Of ATSTemplate) = kakaoService.ListATSTemplate(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = ""

            For Each info As ATSTemplate In templateList
                tmp += "[템플릿 정보]" + vbCrLf
                tmp += "템플릿 코드(templateCode) : " + info.templateCode + vbCrLf
                tmp += "템플릿 제목(templateName) : " + info.templateName + vbCrLf
                tmp += "템플릿 내용(template) : " + info.template + vbCrLf
                tmp += "플러스친구 아이디(plusFriendID) : " + info.plusFriendID + vbCrLf

                If Not info.btns Is Nothing Then
                    For Each btnInfo As KakaoButton In info.btns
                        tmp += "[버튼정보]" + vbCrLf
                        tmp += "버튼명(n) : " + btnInfo.n + vbCrLf
                        tmp += "버튼유형(t) : " + btnInfo.t + vbCrLf
                        tmp += "버튼링크1(u1) : " + btnInfo.u1 + vbCrLf
                        tmp += "버튼링크2(u2) : " + btnInfo.u2 + vbCrLf
                    Next
                    tmp += vbCrLf
                End If
            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 알림톡/친구톡 전송내역 및 전송상태를 확인한다
    '=========================================================================
    Private Sub btnGetMessages_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMessages.Click
        Try
            Dim sentInfo As KakaoSentResult = kakaoService.GetMessages(txtCorpNum.Text, txtReceiptNum.Text)

            Dim tmp As String = "카카오톡 유형(contentType) : " + sentInfo.contentType + vbCrLf
            tmp += "템플릿 코드(templateCode) : " + sentInfo.templateCode + vbCrLf
            tmp += "플러스친구 아이디(plusFriendID) : " + sentInfo.plusFriendID + vbCrLf
            tmp += "발신번호(sendNum) : " + sentInfo.sendNum + vbCrLf
            tmp += "대체문자 내용(altContent) : " + sentInfo.altContent + vbCrLf
            tmp += "대체문자 유형(altSendType) : " + sentInfo.altSendType + vbCrLf
            tmp += "예약일시(reserveDT) : " + sentInfo.altSendType + vbCrLf
            tmp += "광고전송 여부(adsYN) : " + CStr(sentInfo.adsYN) + vbCrLf
            tmp += "친구톡 이미지 URL(imageURL) : " + sentInfo.imageURL + vbCrLf
            tmp += "전송건수(sendCnt) : " + sentInfo.sendCnt + vbCrLf
            tmp += "성공건수(successCnt) : " + sentInfo.successCnt + vbCrLf
            tmp += "실패건수(failCnt) : " + sentInfo.failCnt + vbCrLf
            tmp += "대체문자 건수(altCnt) : " + sentInfo.altCnt + vbCrLf
            tmp += "취소건수(cancelCnt) : " + sentInfo.cancelCnt + vbCrLf

            If Not sentInfo.btns Is Nothing Then
                For Each btnInfo As KakaoButton In sentInfo.btns
                    tmp += "[버튼정보]" + vbCrLf
                    tmp += "버튼명(n) : " + btnInfo.n + vbCrLf
                    tmp += "버튼유형(t) : " + btnInfo.t + vbCrLf
                    tmp += "버튼링크1(u1) : " + btnInfo.u1 + vbCrLf
                    tmp += "버튼링크2(u2) : " + btnInfo.u2 + vbCrLf
                Next
                tmp += vbCrLf
            End If

            MsgBox(tmp)

            '전송결과 정보 리스트
            dataGrid1.DataSource = sentInfo.msgs

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 알림톡/친구톡 예약전송건을 취소한다.
    ' - 예약전송 취소는 예약시간 10분전까지만 가능하다.
    '=========================================================================
    Private Sub btnCancelReserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserve.Click
        Try
            Dim response As Response

            response = kakaoService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 카카오톡 전송내역 목록을 조회한다.
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(6) As String
        Dim item(3) As String

        '[필수] 시작일자, yyyyMMdd
        Dim SDate As String = "20180101"

        '[필수] 종료일자, yyyyMMdd
        Dim EDate As String = "20180430"

        '전송상태값 배열, 0-대기, 1-전송중, 2-성공, 3-대체, 4-실패, 5-취소
        State(0) = "0"
        State(1) = "1"
        State(2) = "2"
        State(3) = "3"
        State(4) = "4"
        State(5) = "5"

        '검색대상 배열, ATS(알림톡), FTS(친구톡 텍스트), FMS(친구톡 이미지)
        item(0) = "ATS"
        item(1) = "FTS"
        item(2) = "FMS"

        '예약문자 검색여부, 공백- 전체조회, 1-예약전송 조회, 0-즉시전송 조회
        Dim ReserveYN As String = ""

        '개인조회여부, True(개인조회), False(전체조회)
        Dim SenderYN As Boolean = False

        '페이지 번호
        Dim Page As Integer = 1

        '페이지 목록개수, 최대 1000건
        Dim PerPage As Integer = 10

        '정렬방향, D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

        Try
            Dim msgSearchList As KakaoSearchResult = kakaoService.Search(txtCorpNum.Text, SDate, EDate, State, _
                                                                       item, ReserveYN, SenderYN, Order, Page, PerPage)

            Dim tmp As String

            tmp = "code (응답코드) : " + CStr(msgSearchList.code) + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + CStr(msgSearchList.total) + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + CStr(msgSearchList.perPage) + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + CStr(msgSearchList.pageNum) + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + CStr(msgSearchList.pageCount) + vbCrLf
            tmp = tmp + "message (응답메시지) : " + msgSearchList.message + vbCrLf + vbCrLf

            dataGrid1.DataSource = msgSearchList.list

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub
End Class