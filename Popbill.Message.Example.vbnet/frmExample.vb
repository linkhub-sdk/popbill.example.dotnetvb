'=========================================================================
'
' 팝빌 문자 API VB.Net SDK Example
'
' - VB.Net 연동환경 설정방법 안내 : http://blog.linkhub.co.kr/569/
' - 업데이트 일자 : 2017-12-04
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
'
' <테스트 연동개발 준비사항>
' 1) 19, 22번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
'=========================================================================

Public Class frmExample

    '링크아이디
    Private LinkID As String = "TESTER"

    '비밀키
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '문자 서비스 변수 선언
    Private messageService As MessageService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '문자서비스 객체 초기화
        messageService = New MessageService(LinkID, SecretKey)

        '연동환경 설정값 (True-개발용, False-상업용)
        messageService.IsTest = True

    End Sub

    Private Function getReserveDT() As DateTime?
        If String.IsNullOrEmpty(txtReserveDT.Text) = False Then

            Return DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
        End If

    End Function

    '=========================================================================
    ' 예약문자전송을 취소합니다.
    ' - 예약취소는 예약전송시간 10분전까지만 가능합니다.
    '=========================================================================
    Private Sub btnCancelReserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserve.Click
        Try
            Dim response As Response

            response = messageService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 문자메시지 전송내역 팝업 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL.Click
        Try
            Dim url As String = messageService.GetURL(txtCorpNum.Text, txtUserId.Text, "BOX")

            MsgBox(url)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 문자전송요청에 대한 전송결과를 확인합니다.
    '=========================================================================
    Private Sub btnGetMessageResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMessageResult.Click
        Try
            Dim ResultList As List(Of MessageResult) = messageService.GetMessageResult(txtCorpNum.Text, txtReceiptNum.Text)

            dataGridView1.DataSource = ResultList


        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btn_SendSMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SendSMS_hund.Click

        '전송정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99

            Dim msg As Message = New Message

            '발신번호
            msg.sendNum = "07075106766"

            '발신자명
            msg.senderName = "발신자명"

            '수신번호
            msg.receiveNum = "11122223333"

            '수신자명
            msg.receiveName = "수신자명칭_" + CStr(i)

            '메시지내용, 90Byte 초과된 내용은 삭제되어 전송
            msg.content = "단문 문자메시지 내용, 각 메시지마다 개별설정 가능." + CStr(i)

            messages.Add(msg)
        Next

        Try

            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendSMS_Same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS_Same.Click

        '발신번호
        Dim sendNum As String = "070-111-2222"

        '메시지 내용, 최대 90Byte(한글45자) 초과된 내용은 삭제되어 전송됨
        Dim contents As String = "다수의 수신자에게 동일한 문자를 전송하는 예제입니다"

        '수신자정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '수신번호
            msg.receiveNum = "010-111-222"

            '수신자명
            msg.receiveName = "수신자명칭_" + CStr(i)
            messages.Add(msg)
        Next

        Try
            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, sendNum, contents, messages, getReserveDT(), txtUserId.Text)

            MsgBox("접수번호(receiptNum) : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendLMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_one.Click

        '발신번호
        Dim sendNum As String = "070-4304-2991"

        '발신자명
        Dim sendName As String = "발신자명"

        '수신번호
        Dim receiveNum As String = "010-111-2222"

        '수신자명
        Dim receiveName As String = "수신자명"

        '메시지 제목
        Dim subject As String = "장문 메시지 제목"

        '장문메시지 내용, 최대 20000byte
        Dim contents As String = "장문 메시지 내용. 최대 2000byte"

        Try
            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, sendNum, sendName, receiveNum, _
                                                              receiveName, subject, contents, getReserveDT(), txtUserId.Text)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendLMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_hund.Click

        '문자전송정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '발신번호
            msg.sendNum = "07075106766"

            '발신자명
            msg.senderName = "발신자명"

            '수신번호
            msg.receiveNum = "11122223333"

            '수신자명
            msg.receiveName = "수신자명칭_" + CStr(i)

            '메시지 제목
            msg.subject = "장문 문자메시지 제목"

            '장문 메시지 내용, 최대 2000byte
            msg.content = "장문 문자메시지 내용, 각 메시지마다 개별설정 가능." + CStr(i)

            messages.Add(msg)
        Next

        Try
            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendLMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_same.Click

        '발신번호
        Dim sendNum As String = "070-111-2222"

        '메시지제목
        Dim subject As String = "메시지 제목"

        '장문메시지 내용, 최대 2000byte
        Dim contents As String = "장문메시지 내용, 최대 2000byte"


        '수신정보배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '수신번호
            msg.receiveNum = "010111222"

            '수신자명
            msg.receiveName = "수신자명칭_" + CStr(i)

            messages.Add(msg)
        Next

        Try
            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, sendNum, subject, contents, messages, getReserveDT(), txtUserId.Text)

            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendXMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_one.Click

        '발신번호
        Dim sendNum As String = "070-4304-2991"

        '발신자명
        Dim sendName As String = "발신자명"

        '수신번호
        Dim receiveNum As String = "010-111-2222"

        '수신자명
        Dim receiveName As String = "수신자명"

        '메시지 제목
        Dim subject As String = "장문 메시지 제목"

        '장문메시지 내용, 최대 20000byte
        Dim contents As String = "장문 메시지 내용. 최대 2000byte"

        Try
            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, sendNum, sendName, _
                                                              receiveNum, receiveName, subject, contents, getReserveDT(), txtUserId.Text)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendXMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_hund.Click

        '전송정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '발신번호
            msg.sendNum = "07075106766"

            '발신자명
            msg.senderName = "발신자명"

            '수신번호
            msg.receiveNum = "010-111-2222"

            '수신자명
            msg.receiveName = "수신자명칭_" + CStr(i)

            '메시지 제목
            msg.subject = "문자메시지 제목"

            '장문메시지 내용, 최대 2000byte
            msg.content = "문자메시지 내용, 각 메시지마다 개별설정 가능." + CStr(i)

            messages.Add(msg)
        Next

        Try
            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text)

            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendXMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_same.Click

        '발신번호
        Dim sendNum As String = "070-111-2222"

        '제목
        Dim subject As String = "메시지 제목"

        '메시지 내용
        Dim contents As String = "길이 자동인식 전송 메시지 내용"


        '전송정보배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '수신번호
            msg.receiveNum = "010-111-222"

            '수신자명
            msg.receiveName = "수신자명칭_" + i.ToString

            messages.Add(msg)
        Next

        Try

            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, sendNum, subject, contents, messages, getReserveDT(), txtUserId.Text)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
    ' - LinkID는 인증정보로 설정되어 있는 링크아이디 값입니다.
    '=========================================================================
    Private Sub btnCheckIsMember_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = messageService.CheckIsMember(txtCorpNum.Text, LinkID)

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
            Dim response As Response = messageService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 연동회원으로 회원가입을 요청합니다.
    '=========================================================================
    Private Sub btnJoinMember_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJoinMember.Click
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
            Dim response As Response = messageService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 문자-단문 API 서비스 과금정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo_SMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.SMS

        Try
            Dim ChargeInfo As ChargeInfo = messageService.GetChargeInfo(txtCorpNum.Text, msgType)

            Dim tmp As String = "unitCost (발행단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 단문(SMS) 전송단가를 확인합니다.
    '=========================================================================
    Private Sub btnUnitCost_SMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost_SMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.SMS

        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, msgType)

            MsgBox("단문 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 장문(LMS) 전송단가를 확인합니다.
    '=========================================================================
    Private Sub btnGetUnitCost_LMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost_LMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.LMS

        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, msgType)

            MsgBox("장문 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 포문(MMS) 전송단가를 확인합니다.
    '=========================================================================
    Private Sub btnGetUnitCost_MMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost_MMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.MMS

        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, msgType)

            MsgBox("포토문자 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 문자-장문 API 서비스 과금정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetChargeInfo_LMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo_LMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.LMS

        Try
            Dim ChargeInfo As ChargeInfo = messageService.GetChargeInfo(txtCorpNum.Text, msgType)

            Dim tmp As String = "unitCost (발행단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 연동회원의 문자-포토 API 서비스 과금정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetChargeInfo_MMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo_MMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.MMS

        Try
            Dim ChargeInfo As ChargeInfo = messageService.GetChargeInfo(txtCorpNum.Text, msgType)

            Dim tmp As String = "unitCost (발행단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)
    '   를 통해 확인하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetBalance_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click

        Try
            Dim remainPoint As Double = messageService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전 URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_CHRG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_CHRG.Click
        Try
            Dim url As String = messageService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를
    '   이용하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = messageService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전 팝업 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            Dim url As String = messageService.GetPartnerURL(txtCorpNum.Text, "CHRG")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌(www.popbill.com)에 로그인된 팝빌 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_LOGIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_LOGIN.Click
        Try
            Dim url As String = messageService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자를 신규로 등록합니다.
    '=========================================================================
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
            Dim response As Response = messageService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자 목록을 확인합니다.
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = messageService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "아이디 | 담당자명 | 연락처 | 휴대폰번호 | 메일주소 | 회사조회 여부" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.tel + " | " + info.hp + " | " + info.email + " | " + info.searchAllAllowYN.ToString() + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자 정보를 수정합니다.
    '=========================================================================
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
            Dim response As Response = messageService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCorpInfo.Click
        Try
            Dim corpInfo As CorpInfo = messageService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

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

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다
    '=========================================================================
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

            Dim response As Response = messageService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendSMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS_one.Click

        '발신번호
        Dim sendNum As String = "07075106766"

        '발신자명
        Dim sendName As String = "발신자명"

        '수신번호
        Dim receiveNum As String = "010111222"

        '수신자명
        Dim receiveName As String = "수신자명칭"

        '메시지 내용
        Dim contents As String = "단문 문자메시지 내용, 각 메시지마다 개별설정 가능."

        Try

            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, sendNum, sendName, _
                                                              receiveNum, receiveName, getReserveDT(), txtUserId.Text)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendMMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMMS_one.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = "070-4304-2993"

            '발신자명
            Dim sendName As String = "발신자명"

            '수신번호
            Dim receiveNum As String = "010-111-222"

            '수신자명
            Dim receiveName As String = "수신자명"

            '메시지 제목
            Dim subject As String = "포토 메시지 제목"

            '장문메시지 내용, 최대 20000byte
            Dim contents As String = "포토 메시지 내용. 최대 2000byte"

            Try
                Dim receiptNum As String = messageService.SendMMS(txtCorpNum.Text, sendNum, receiveNum, _
                                                                  receiveName, subject, contents, strFileName, getReserveDT(), txtUserId.Text)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    Private Sub btnSendMMS_hundered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMMS_hundered.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = "070-4304-2993"

            '메시지 제목
            Dim subject As String = "포토문자 전송 메시지제목"

            '포토 문자 메시지 내용, 최대 2000byte
            Dim contents As String = "포토 문자 메시지 내용, 최대 2000byte"


            '문자전송정보 배열, 최대 1000건
            Dim messages As List(Of Message) = New List(Of Message)

            For i As Integer = 0 To 99
                Dim msg As Message = New Message

                '수신번호
                msg.receiveNum = "010-111-222"

                '수신자명
                msg.receiveName = "수신자명칭_" + CStr(i)

                messages.Add(msg)
            Next

            Try
                Dim receiptNum As String = messageService.SendMMS(txtCorpNum.Text, sendNum, subject, contents, _
                                                                  messages, strFileName, getReserveDT(), txtUserId.Text)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    Private Sub btnSendMMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMMS_same.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = "070-4304-2993"

            '메시지 제목
            Dim subject As String = "포토문자 전송 메시지제목"

            '포토 문자 메시지 내용, 최대 2000byte
            Dim contents As String = "포토 문자 메시지 내용, 최대 2000byte"


            '문자전송정보 배열, 최대 1000건
            Dim messages As List(Of Message) = New List(Of Message)

            For i As Integer = 0 To 99
                Dim msg As Message = New Message

                '수신번호
                msg.receiveNum = "010-111-222"

                '수신자명
                msg.receiveName = "수신자명칭_" + CStr(i)

                messages.Add(msg)
            Next

            Try
                Dim receiptNum As String = messageService.SendMMS(txtCorpNum.Text, sendNum, subject, contents, _
                                                                  messages, strFileName, getReserveDT(), txtUserId.Text)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
            End Try

        End If

    End Sub

    '=========================================================================
    ' 문자 발신번호 목록을 조회합니다.
    '=========================================================================
    Private Sub btnGetSenderNumberList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSenderNumberList.Click
        Try
            Dim senderList As List(Of SenderNumber) = messageService.GetSenderNumberList(txtCorpNum.Text)

            Dim tmp As String = "발신번호 | 대표번호여부 | 인증상태" + vbCrLf
            For Each info As SenderNumber In senderList
                tmp += info.number + " | " + CStr(info.representYN) + " | " + CStr(info.state)
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 발신번호 관리 팝업 URL을 반환합니다.
    ' - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetURL_SENDER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_SENDER.Click
        Try
            Dim url As String = messageService.GetURL(txtCorpNum.Text, txtUserId.Text, "SENDER")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건을 사용하여 문자전송 내역을 조회합니다.
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(4) As String
        Dim item(3) As String

        '[필수] 시작일자, yyyyMMdd
        Dim SDate As String = "20170701"

        '[필수] 종료일자, yyyyMMdd
        Dim EDate As String = "20171231"

        '전송상태값 배열, 1-대기, 2-성공, 3-실패, 4-취소
        State(0) = "1"
        State(1) = "2"
        State(2) = "3"
        State(3) = "4"

        '검색대상 배열, SMS(단문),LMS(장문),MMS(포토)
        item(0) = "SMS"
        item(1) = "LMS"
        item(2) = "MMS"

        '예약문자 검색여부, True(예약문자만 조회), False(전체조회)
        Dim ReserveYN As Boolean = False

        '개인조회여부, True(개인조회), False(전체조회)
        Dim SenderYN As Boolean = False

        '페이지 번호
        Dim Page As Integer = 1

        '페이지 목록개수, 최대 1000건
        Dim PerPage As Integer = 10

        '정렬방향, D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

    

        Try
            Dim msgSearchList As MSGSearchResult = messageService.Search(txtCorpNum.Text, SDate, EDate, State, _
                                                                       item, ReserveYN, SenderYN, Order, Page, PerPage)


            Dim tmp As String

            tmp = "code (응답코드) : " + CStr(msgSearchList.code) + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + CStr(msgSearchList.total) + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + CStr(msgSearchList.perPage) + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + CStr(msgSearchList.pageNum) + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + CStr(msgSearchList.pageCount) + vbCrLf
            tmp = tmp + "message (응답메시지) : " + msgSearchList.message + vbCrLf + vbCrLf

            dataGridView1.DataSource = msgSearchList.list

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 080 서비스 수신거부 목록을 확인합니다.
    '=========================================================================
    Private Sub btnGetAutoDenyList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAutoDenyList.Click
        Try
            Dim numberList As List(Of AutoDeny) = messageService.GetAutoDenyList(txtCorpNum.Text)

            Dim tmp As String = "수신번호 | 등록일시 " + vbCrLf
            For Each info As AutoDeny In numberList
                tmp += info.number + " | " + info.regDT + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub
End Class
