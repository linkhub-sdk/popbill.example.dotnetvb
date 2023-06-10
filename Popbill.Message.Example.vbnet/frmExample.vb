'=========================================================================
'
' 팝빌 문자 API VB.Net SDK Example
'
' - VB.Net 연동환경 설정방법 안내 : https://developers.popbill.com/guide/sms/dotnet/getting-started/tutorial?fwn=vb
' - 업데이트 일자 : 2022-10-26
' - 연동 기술지원 연락처 : 1600-9854
' - 연동 기술지원 이메일 : code@linkhubcorp.com
'
' <테스트 연동개발 준비사항>
' 1) 22, 25번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
' 3) 발신번호 사전등록을 합니다. (등록방법은 사이트/API 두가지 방식이 있습니다.)
'    - 1. 팝빌 사이트 로그인 > [문자/팩스] > [문자] > [발신번호 사전등록] 메뉴에서 등록
'    - 2. getSenderNumberMgtURL API를 통해 반환된 URL을 이용하여 발신번호 등록
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

        '연동환경 설정값, True-개발용, False-상업용
        messageService.IsTest = True

        '인증토큰 발급 IP 제한 On/Off, True-사용, False-미사용, 기본값(True)
        messageService.IPRestrictOnOff = True

        '팝빌 API 서비스 고정 IP 사용여부, True-사용, False-미사용, 기본값(False)
        messageService.UseStaticIP = False

        '로컬시스템 시간 사용여부, True-사용, False-미사용, 기본값(False)
        messageService.UseLocalTimeYN = False

    End Sub

    Private Function getReserveDT() As DateTime?
        If String.IsNullOrEmpty(txtReserveDT.Text) = False Then

            Return DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
        End If

    End Function

    '=========================================================================
    ' 문자 발신번호 등록여부를 확인합니다.
    ' - 발신번호 상태가 '승인'인 경우에만 리턴값 'Response'의 변수 'code'가 1로 반환됩니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/sendnum#CheckSenderNumber
    '=========================================================================
    Private Sub btnCheckSenderNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckSenderNumber.Click
        Try
            Dim response As Response
            Dim senderNumber As String = ""

            response = messageService.CheckSenderNumber(txtCorpNum.Text, senderNumber)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 발신번호를 등록하고 내역을 확인하는 문자 발신번호 관리 페이지 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/sendnum#GetSenderNumberMgtURL
    '=========================================================================
    Private Sub btnGetSenderNumberMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSenderNumberMgtURL.Click
        Try
            Dim url As String = messageService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록한 연동회원의 문자 발신번호 목록을 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/sendnum#GetSenderNumberList
    '=========================================================================
    Private Sub btnGetSenderNumberList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSenderNumberList.Click
        Try
            Dim senderList As List(Of SenderNumber) = messageService.GetSenderNumberList(txtCorpNum.Text)

            Dim tmp As String = "number(발신번호) | representYN(대표번호여부) | state(인증상태) | memo(메모)" + vbCrLf
            For Each info As SenderNumber In senderList
                tmp += info.number + " | " + CStr(info.representYN) + " | " + CStr(info.state) + " | " + info.memo + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 90byte의 단문(SMS) 메시지 1건 전송을 팝빌에 접수합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendSMSOne
    '=========================================================================
    Private Sub btnSendSMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS_one.Click

        '발신번호
        Dim sendNum As String = ""

        '발신자명
        Dim sendName As String = "발신자명"

        '수신번호
        Dim receiveNum As String = ""

        '수신자명
        Dim receiveName As String = "수신자명칭"

        '메시지 내용
        Dim contents As String = "단문 문자메시지 내용, 각 메시지마다 개별설정 가능."

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = False

        Try

            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, sendNum, sendName, receiveNum, receiveName, _
                                                              contents, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 90byte의 단문(SMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 수신자마다 개별 내용을 전송할 수 있습니다(대량전송).
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendSMSMulti
    '=========================================================================
    Private Sub btn_SendSMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SendSMS_hund.Click

        '전송정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN = True

        For i As Integer = 0 To 2

            Dim msg As Message = New Message

            '발신번호
            msg.sendNum = ""

            '발신자명
            msg.senderName = "발신자명"

            '수신번호
            msg.receiveNum = ""

            '수신자명
            msg.receiveName = "수신자명칭_" + CStr(i)

            '메시지내용, 90Byte 초과된 내용은 삭제되어 전송
            msg.content = "단문 문자메시지 내용, 각 메시지마다 개별설정 가능." + CStr(i)

            '파트너 지정키, 대량전송 시 수신자 구별용 메모
            msg.interOPRefKey = "20220513-" + CStr(i)

            messages.Add(msg)
        Next

        Try

            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)
            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 90byte의 단문(SMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendSMSSame
    '=========================================================================
    Private Sub btnSendSMS_Same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS_Same.Click

        '발신번호
        Dim sendNum As String = ""

        '메시지 내용, 최대 90Byte(한글45자) 초과된 내용은 삭제되어 전송됨
        Dim contents As String = "다수의 수신자에게 동일한 문자를 전송하는 예제입니다"

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = False

        '수신자정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '수신번호
            msg.receiveNum = ""

            '수신자명
            msg.receiveName = "수신자명칭_" + CStr(i)

            '파트너 지정키, 대량전송 시 수신자 구별용 메모
            msg.interOPRefKey = "20220513-" + CStr(i)

            messages.Add(msg)
        Next

        Try
            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, sendNum, contents, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MsgBox("접수번호(receiptNum) : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 2,000byte의 장문(LMS) 메시지 1건 전송을 팝빌에 접수합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendLMSOne
    '=========================================================================
    Private Sub btnSendLMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_one.Click

        '발신번호
        Dim sendNum As String = ""

        '발신자명
        Dim sendName As String = "발신자명"

        '수신번호
        Dim receiveNum As String = ""

        '수신자명
        Dim receiveName As String = "수신자명"

        '메시지 제목
        Dim subject As String = "장문 메시지 제목"

        '장문메시지 내용, 최대 2000byte
        Dim contents As String = "장문 메시지 내용. 최대 2000byte"

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = False

        Try
            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, sendNum, sendName, receiveNum, _
                                                              receiveName, subject, contents, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 2,000byte의 장문(LMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 수신자마다 개별 내용을 전송할 수 있습니다(대량전송).
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendLMSMulti
    '=========================================================================
    Private Sub btnSendLMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_hund.Click

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = False

        '문자전송정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '발신번호
            msg.sendNum = ""

            '발신자명
            msg.senderName = "발신자명"

            '수신번호
            msg.receiveNum = ""

            '수신자명
            msg.receiveName = "수신자명칭_" + CStr(i)

            '메시지 제목
            msg.subject = "장문 문자메시지 제목"

            '장문 메시지 내용, 최대 2000byte
            msg.content = "장문 문자메시지 내용, 각 메시지마다 개별설정 가능." + CStr(i)

            '파트너 지정키, 대량전송 시 수신자 구별용 메모
            msg.interOPRefKey = "20220513-" + CStr(i)

            messages.Add(msg)
        Next

        Try
            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 2,000byte의 장문(LMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendLMSSame
    '=========================================================================
    Private Sub btnSendLMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_same.Click

        '발신번호
        Dim sendNum As String = ""

        '메시지제목
        Dim subject As String = "메시지 제목"

        '장문메시지 내용, 최대 2000byte
        Dim contents As String = "장문메시지 내용, 최대 2000byte"

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = False

        '수신정보배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '수신번호
            msg.receiveNum = ""

            '수신자명
            msg.receiveName = "수신자명칭_" + CStr(i)

            '파트너 지정키, 대량전송 시 수신자 구별용 메모
            msg.interOPRefKey = "20220513-" + CStr(i)

            messages.Add(msg)
        Next

        Try
            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, sendNum, subject, contents, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 메시지 크기(90byte)에 따라 단문/장문(SMS/LMS)을 자동으로 인식하여 1건의 메시지를 전송을 팝빌에 접수합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendXMSOne
    '=========================================================================
    Private Sub btnSendXMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_one.Click

        '발신번호
        Dim sendNum As String = ""

        '발신자명
        Dim sendName As String = "발신자명"

        '수신번호
        Dim receiveNum As String = ""

        '수신자명
        Dim receiveName As String = "수신자명"

        '메시지 제목
        Dim subject As String = "장문 메시지 제목"

        '장문메시지 내용, 최대 2000byte
        Dim contents As String = "장문 메시지 내용. 최대 2000byte"

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = False

        Try
            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, sendNum, sendName, _
                                                              receiveNum, receiveName, subject, contents, getReserveDT(), txtUserId.Text, requestNum, adsYN)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 메시지 크기(90byte)에 따라 단문/장문(SMS/LMS)을 자동으로 인식하여 다수건의 메시지 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 수신자마다 개별 내용을 전송할 수 있습니다(대량전송).
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendXMSMulti
    '=========================================================================
    Private Sub btnSendXMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_hund.Click

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = False

        '전송정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '발신번호
            msg.sendNum = ""

            '발신자명
            msg.senderName = "발신자명"

            '수신번호
            msg.receiveNum = ""

            '수신자명
            msg.receiveName = "수신자명칭_" + CStr(i)

            '메시지 제목
            msg.subject = "문자메시지 제목"

            '장문메시지 내용, 최대 2000byte
            msg.content = "문자메시지 내용, 각 메시지마다 개별설정 가능." + CStr(i)

            '파트너 지정키, 대량전송 시 수신자 구별용 메모
            msg.interOPRefKey = "20220513-" + CStr(i)

            messages.Add(msg)
        Next

        Try
            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 메시지 크기(90byte)에 따라 단문/장문(SMS/LMS)을 자동으로 인식하여 다수건의 메시지 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendXMSSame
    '=========================================================================
    Private Sub btnSendXMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_same.Click

        '발신번호
        Dim sendNum As String = ""

        '제목
        Dim subject As String = "메시지 제목"

        '메시지 내용
        Dim contents As String = "길이 자동인식 전송 메시지 내용"

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = False

        '전송정보배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '수신번호
            msg.receiveNum = ""

            '수신자명
            msg.receiveName = "수신자명칭_" + i.ToString

            '파트너 지정키, 대량전송 시 수신자 구별용 메모
            msg.interOPRefKey = "20220513-" + CStr(i)

            messages.Add(msg)
        Next

        Try

            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, sendNum, subject, contents, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 최대 2,000byte의 메시지와 이미지로 구성된 포토문자(MMS) 1건 전송을 팝빌에 접수합니다.
    ' - 이미지 파일 포맷/규격 : 최대 300Kbyte(JPEG, JPG), 가로/세로 1,000px 이하 권장
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendMMSOne
    '=========================================================================
    Private Sub btnSendMMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMMS_one.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = ""

            '발신자명
            Dim sendName As String = "발신자명"

            '수신번호
            Dim receiveNum As String = ""

            '수신자명
            Dim receiveName As String = "수신자명"

            '메시지 제목
            Dim subject As String = "포토 메시지 제목"

            '장문메시지 내용, 최대 2000byte
            Dim contents As String = "포토 메시지 내용. 최대 2000byte"

            ' 전송요청번호
            ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
            ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            Dim requestNum = ""

            '광고문자 여부 (기본값 False)
            Dim adsYN As Boolean = False

            Try
                Dim receiptNum As String = messageService.SendMMS(txtCorpNum.Text, sendNum, receiveNum, _
                                                                  receiveName, subject, contents, strFileName, getReserveDT(), txtUserId.Text, requestNum, adsYN)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '===========================================================================
    ' 최대 2,000byte의 메시지와 이미지로 구성된 포토문자(MMS) 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
    ' - 이미지 파일 포맷/규격 : 최대 300Kbyte(JPEG), 가로/세로 1,000px 이하 권장
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendMMSSame
    '===========================================================================
    Private Sub btnSendMMS_hundered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMMS_hundered.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = ""

            '메시지 제목
            Dim subject As String = "포토문자 전송 메시지제목"

            '포토 문자 메시지 내용, 최대 2000byte
            Dim contents As String = "포토 문자 메시지 내용, 최대 2000byte"

            ' 전송요청번호
            ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
            ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            Dim requestNum = ""

            ' 광고성 메시지 여부 ( true , false 중 택 1)
            ' └ true = 광고 , false = 일반
            ' - 미입력 시 기본값 false 처리
            Dim adsYN As Boolean = False

            '문자전송정보 배열, 최대 1000건
            Dim messages As List(Of Message) = New List(Of Message)

            For i As Integer = 0 To 99
                Dim msg As Message = New Message

                '수신번호
                msg.receiveNum = ""

                '수신자명
                msg.receiveName = "수신자명칭_" + CStr(i)

                '파트너 지정키, 대량전송 시 수신자 구별용 메모
                msg.interOPRefKey = "20220513-" + CStr(i)

                messages.Add(msg)
            Next

            Try
                Dim receiptNum As String = messageService.SendMMS(txtCorpNum.Text, sendNum, subject, contents, _
                                                                  messages, strFileName, getReserveDT(), txtUserId.Text, requestNum, adsYN)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 최대 2,000byte의 메시지와 이미지로 구성된 포토문자(MMS) 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
    ' - 이미지 파일 포맷/규격 : 최대 300Kbyte(JPEG), 가로/세로 1,000px 이하 권장
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#SendMMSSame
    '=========================================================================
    Private Sub btnSendMMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMMS_same.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = ""

            '메시지 제목
            Dim subject As String = "포토문자 전송 메시지제목"

            '포토 문자 메시지 내용, 최대 2000byte
            Dim contents As String = "포토 문자 메시지 내용, 최대 2000byte"

            ' 전송요청번호
            ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
            ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            Dim requestNum = ""

            ' 광고성 메시지 여부 ( true , false 중 택 1)
            ' └ true = 광고 , false = 일반
            ' - 미입력 시 기본값 false 처리
            Dim adsYN As Boolean = False

            '문자전송정보 배열, 최대 1000건
            Dim messages As List(Of Message) = New List(Of Message)

            For i As Integer = 0 To 99
                Dim msg As Message = New Message

                '수신번호
                msg.receiveNum = ""

                '수신자명
                msg.receiveName = "수신자명칭_" + CStr(i)

                '파트너 지정키, 대량전송 시 수신자 구별용 메모
                msg.interOPRefKey = "20220513-" + CStr(i)

                messages.Add(msg)
            Next

            Try
                Dim receiptNum As String = messageService.SendMMS(txtCorpNum.Text, sendNum, subject, contents, _
                                                                  messages, strFileName, getReserveDT(), txtUserId.Text, requestNum, adsYN)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 예약접수된 문자 메시지 전송을 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#CancelReserve
    '=========================================================================
    Private Sub btnCancelReserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserve.Click
        Try
            Dim response As Response

            response = messageService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 예약접수된 문자 전송을 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#CancelReserveRN
    '=========================================================================
    Private Sub btnCancelReserveRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserveRN.Click
        Try
            Dim response As Response

            response = messageService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호와 수신번호를 통해 예약접수된 문자 메시지 전송을 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#CancelReservebyRCV
    '=========================================================================
    Private Sub btnCancelReservebyRCV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReservebyRCV.Click
        Try
            Dim response As Response

            response = messageService.CancelReservebyRCV(txtCorpNum.Text, txtReceiptNumbyRCV.Text, txtReceiveNumbyRCV.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호와 수신번호를 통해 예약접수된 문자 전송을 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://developers.popbill.com/reference/sms/dotnet/api/send#CancelReserveRNbyRCV
    '=========================================================================
    Private Sub btnCancelReserveRNbyRCV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserveRNbyRCV.Click
        Try
            Dim response As Response

            response = messageService.CancelReserveRNbyRCV(txtCorpNum.Text, txtRequestNumbyRCV.Text, txtReceiveNumRNbyRCV.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 문자 전송상태 및 결과를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/info#GetMessages
    '=========================================================================
    Private Sub btnGetMessageResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMessageResult.Click
        ListBox1.Items.Clear()
        Try
            Dim ResultList As List(Of MessageResult) = messageService.GetMessageResult(txtCorpNum.Text, txtReceiptNum.Text)

            Dim rowStr As String = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) | "
            rowStr += "receiptDT(접수일시) | sendDT(전송일시) | resultDT(전송결과 수신일시) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) | type(메시지 타입) | "
            rowStr += "tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호) | interOPRefKey(파트너 지정키)"

            ListBox1.Items.Add(rowStr)

            For Each Result As MessageResult In ResultList
                rowStr = ""
                rowStr += Result.subject + " | " + Result.content + " | " + Result.sendNum + " | " + Result.senderName + " | " + Result.receiveNum + " | " + Result.receiveName + " | "
                rowStr += Result.receiptDT + " | " + Result.sendDT + " | " + Result.resultDT + " | " + Result.reserveDT + " | " + Result.state.ToString + " | " + Result.result.ToString + " | "
                rowStr += Result.type + " | " + Result.tranNet + " | " + Result.receiptNum + " | " + Result.requestNum + " | " + Result.interOPRefKey

                ListBox1.Items.Add(rowStr)
            Next

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 문자 전송상태 및 결과를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/info#GetMessagesRN
    '=========================================================================
    Private Sub btnGetMessageResultRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMessageResultRN.Click
        ListBox1.Items.Clear()
        Try
            Dim ResultList As List(Of MessageResult) = messageService.GetMessageResultRN(txtCorpNum.Text, txtRequestNum.Text)

            Dim rowStr As String = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) | "
            rowStr += "receiptDT(접수일시) | sendDT(전송일시) | resultDT(전송결과 수신일시) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) | type(메시지 타입) | "
            rowStr += "tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호) | interOPRefKey(파트너 지정키)"

            ListBox1.Items.Add(rowStr)

            For Each Result As MessageResult In ResultList
                rowStr = ""
                rowStr += Result.subject + " | " + Result.content + " | " + Result.sendNum + " | " + Result.senderName + " | " + Result.receiveNum + " | " + Result.receiveName + " | "
                rowStr += Result.receiptDT + " | " + Result.sendDT + " | " + Result.resultDT + " | " + Result.reserveDT + " | " + Result.state.ToString + " | " + Result.result.ToString + " | "
                rowStr += Result.type + " | " + Result.tranNet + " | " + Result.receiptNum + " | " + Result.requestNum + " | " + Result.interOPRefKey

                ListBox1.Items.Add(rowStr)
            Next

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건에 해당하는 문자 전송내역을 조회합니다. (조회기간 단위 : 최대 2개월)
    ' - 문자 접수일시로부터 6개월 이내 접수건만 조회할 수 있습니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/info#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(4) As String
        Dim item(3) As String

        '최대 검색기간 : 6개월 이내
        '시작일자, yyyyMMdd
        Dim SDate As String = "20220501"

        '종료일자, yyyyMMdd
        Dim EDate As String = "20220531"

        ' 전송상태 배열 ("1" , "2" , "3" , "4" 중 선택, 다중 선택 가능)
        ' └ 1 = 대기 , 2 = 성공 , 3 = 실패 , 4 = 취소
        ' - 미입력 시 전체조회
        State(0) = "1"
        State(1) = "2"
        State(2) = "3"
        State(3) = "4"

        ' 검색대상 배열 ("SMS" , "LMS" , "MMS" 중 선택, 다중 선택 가능)
        ' └ SMS = 단문 , LMS = 장문 , MMS = 포토문자
        ' - 미입력 시 전체조회
        item(0) = "SMS"
        item(1) = "LMS"
        item(2) = "MMS"

        ' 예약여부 (false , true 중 택 1)
        ' └ false = 전체조회, true = 예약전송건 조회
        ' - 미입력시 기본값 false 처리
        Dim ReserveYN As Boolean = False

        ' 개인조회 여부 (false , true 중 택 1)
        ' └ false = 접수한 문자 전체 조회 (관리자권한)
        ' └ true = 해당 담당자 계정으로 접수한 문자만 조회 (개인권한)
        ' - 미입력시 기본값 false 처리
        Dim SenderYN As Boolean = False

        '페이지 번호
        Dim Page As Integer = 1

        '페이지 목록개수, 최대 1000건
        Dim PerPage As Integer = 10

        '정렬방향, D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

        ' 조회하고자 하는 발신자명 또는 수신자명
        ' - 미입력시 전체조회
        Dim QString As String = ""

        ListBox1.Items.Clear()
        Try
            Dim msgSearchList As MSGSearchResult = messageService.Search(txtCorpNum.Text, SDate, EDate, State, _
                                                                       item, ReserveYN, SenderYN, Order, Page, PerPage, QString)

            Dim tmp As String = ""

            tmp += "code (응답코드) : " + CStr(msgSearchList.code) + vbCrLf
            tmp += "message (응답메시지) : " + msgSearchList.message + vbCrLf + vbCrLf
            tmp += "total (총 검색결과 건수) : " + CStr(msgSearchList.total) + vbCrLf
            tmp += "perPage (페이지당 검색개수) : " + CStr(msgSearchList.perPage) + vbCrLf
            tmp += "pageNum (페이지 번호) : " + CStr(msgSearchList.pageNum) + vbCrLf
            tmp += "pageCount (페이지 개수) : " + CStr(msgSearchList.pageCount) + vbCrLf

            Dim rowStr As String = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) | "
            rowStr += "receiptDT(접수일시) | sendDT(전송일시) | resultDT(전송결과 수신일시) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) | type(메시지 타입) | "
            rowStr += "tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호) | interOPRefKey(파트너 지정키)"

            ListBox1.Items.Add(rowStr)

            For Each Result As MessageResult In msgSearchList.list
                rowStr = ""
                rowStr += Result.subject + " | " + Result.content + " | " + Result.sendNum + " | " + Result.senderName + " | " + Result.receiveNum + " | " + Result.receiveName + " | "
                rowStr += Result.receiptDT + " | " + Result.sendDT + " | " + Result.resultDT + " | " + Result.reserveDT + " | " + Result.state.ToString + " | " + Result.result.ToString + " | "
                rowStr += Result.type + " | " + Result.tranNet + " | " + Result.receiptNum + " | " + Result.requestNum + " | " + Result.interOPRefKey

                ListBox1.Items.Add(rowStr)
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트와 동일한 문자 전송내역 확인 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/info#GetSentListURL
    '=========================================================================
    Private Sub btnGetSentListURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSentListURL.Click
        Try
            Dim url As String = messageService.GetSentListURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전용 080 번호에 등록된 수신거부 목록을 반환합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/info#GetAutoDenyList
    '=========================================================================
    Private Sub btnGetAutoDenyList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAutoDenyList.Click
        Try
            Dim numberList As List(Of AutoDeny) = messageService.GetAutoDenyList(txtCorpNum.Text)

            Dim tmp As String = "number(수신거부번호) | regDT(등록일시) " + vbCrLf
            For Each info As AutoDeny In numberList
                tmp += info.number + " | " + info.regDT + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click

        Try
            Dim remainPoint As Double = messageService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = messageService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPaymentURL.Click
        Try
            Dim url As String = messageService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = messageService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = messageService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = messageService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 문자(SMS) 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_SMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost_SMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.SMS

        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, msgType)

            MsgBox("단문 전송단가(unitCost) : " + unitCost.ToString)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 문자(LMS) 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetUnitCost
    '=========================================================================
    Private Sub btnGetUnitCost_LMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost_LMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.LMS

        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, msgType)

            MsgBox("장문 전송단가(unitCost) : " + unitCost.ToString)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 문자(MMS) 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetUnitCost
    '=========================================================================
    Private Sub btnGetUnitCost_MMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost_MMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.MMS

        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, msgType)

            MsgBox("포토문자 전송단가(unitCost) : " + unitCost.ToString)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 문자(SMS)API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetChargeInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 팝빌 문자(LMS) API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetChargeInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 문자(MMS) API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetChargeInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = messageService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = messageService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#JoinMember
    '=========================================================================
    Private Sub btnJoinMember_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJoinMember.Click
        Dim joinInfo As JoinForm = New JoinForm


        '아이디, 6자이상 50자 미만
        joinInfo.ID = "userid"

        '비밀번호, 8자 이상 20자 이하(영문, 숫자, 특수문자 조합)
        joinInfo.Password = "asdf8536!@#"

        '링크아이디
        joinInfo.LinkID = LinkID

        '사업자번호 "-" 제외
        joinInfo.CorpNum = "1231212312"

        '대표자명 (최대 100자)
        joinInfo.CEOName = "대표자성명"

        '상호 (최대 200자)
        joinInfo.CorpName = "상호"

        '사업장 주소 (최대 300자)
        joinInfo.Addr = "주소"

        '업태 (최대 100자)
        joinInfo.BizType = "업태"

        '종목 (최대 100자)
        joinInfo.BizClass = "종목"

        '담당자 성명 (최대 100자)
        joinInfo.ContactName = "담당자명"

        '담당자 이메일 (최대 20자)
        joinInfo.ContactEmail = ""

        '담당자 연락처 (최대 20자)
        joinInfo.ContactTEL = ""

        Try
            Dim response As Response = messageService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#GetCorpInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#UpdateCorpInfo
    '=========================================================================
    Private Sub btnUpdateCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCorpInfo.Click
        Dim corpInfo As New CorpInfo

        '대표자명(최대 100자)
        corpInfo.ceoname = "대표자명_수정"

        '상호(최대 200자)
        corpInfo.corpName = "상호_수정"

        '주소(최대 300자)
        corpInfo.addr = "주소_수정"

        '업태(최대 100자)
        corpInfo.bizType = "업태_수정"

        '종목(최대 100자)
        corpInfo.bizClass = "종목_수정"

        Try

            Dim response As Response = messageService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = messageService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#RegistContact
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디 (6자이상 50자미만)
        joinData.id = "testkorea1120"

        '비밀번호, 8자 이상 20자 이하(영문, 숫자, 특수문자 조합)
        joinData.Password = "asdf8536!@#"

        '담당자 성명 (최대 100자)
        joinData.personName = "담당자명"

        '담당자 연락처 (최대 20자)
        joinData.tel = ""

        '담당자 이메일 (최대 100자)
        joinData.email = ""

        '담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = messageService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = messageService.GetContactInfo(txtCorpNum.Text, contactID)

            tmp += "id (담당자 아이디) : " + contactInfo.id + vbCrLf
            tmp += "personName (담당자명) : " + contactInfo.personName + vbCrLf
            tmp += "email (담당자 이메일) : " + contactInfo.email + vbCrLf
            tmp += "searchRole (담당자 권한) : " + contactInfo.searchRole.ToString + vbCrLf
            tmp += "tel (연락처) : " + contactInfo.tel + vbCrLf
            tmp += "mgrYN (관리자 여부) : " + contactInfo.mgrYN.ToString + vbCrLf
            tmp += "regDT (등록일시) : " + contactInfo.regDT + vbCrLf
            tmp += "state (상태) : " + contactInfo.state + vbCrLf

            tmp += vbCrLf

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = messageService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자명) | email(메일주소) | tel(연락처) |"
            tmp += "regDT(등록일시) | searchRole(담당자 권한) | mgrYN(관리자 여부) | state(상태)" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.email + " | " + info.tel + " | "
                tmp += info.regDT.ToString + " | " + info.searchRole.ToString + " | " + info.mgrYN.ToString + " | " + info.state + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#UpdateContact
    '=========================================================================
    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디 (6자이상 50자미만)
        joinData.id = "testkorea1120"

        '담당자 성명 (최대 100자)
        joinData.personName = "담당자명"

        '담당자 연락처 (최대 20자)
        joinData.tel = ""

        '담당자 이메일 (최대 100자)
        joinData.email = ""

        '담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = messageService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 문자전송에 대한 전송결과 요약정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetStates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetStates.Click
        Dim ReciptNumList As List(Of String) = New List(Of String)

        '문자전송 접수번호
        ReciptNumList.Add("018090410000000395")
        ReciptNumList.Add("018090410000000416")

        ListBox1.Items.Clear()
        Try
            Dim resultList As List(Of MessageState) = messageService.GetStates(txtCorpNum.Text, ReciptNumList, txtUserId.Text)


            Dim rowStr As String = "rNum(접수번호) | sn(일련번호) | stat(전송 상태코드) | rlt(전송 결과코드) | sDT(전송일시) | rDT(결과코드 수신일시) | net(전송 이동통신사명) | srt(구 전송결과 코드)"

            ListBox1.Items.Add(rowStr)

            For Each Result As MessageState In resultList
                rowStr = ""
                rowStr += Result.rNum + " | " + Result.sn + " | " + Result.stat + " | " + Result.rlt + " | " + Result.sDT + " | " + Result.rDT + " | " + Result.net + " | " + Result.srt

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#PaymentRequest
    '=========================================================================
    Private Sub btnPaymentRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentRequest.Click

        '입금신청 객체정보
        Dim paymentForm As New PaymentForm

        '담당자명
        paymentForm.settlerName	="담당자명"
        '담당자 이메일
        paymentForm.settlerEmail	="담당자 이메일"
        '담당자 휴대폰
        paymentForm.notifyHP	="담당자 휴대폰"
        '입금자명
        paymentForm.paymentName	="입금자명"
        '결제금액
        paymentForm.settleCost	="결제금액"

        Try
            Dim response As PaymentResponse = messageService.PaymentRequest(txtCorpNum.Text, paymentForm, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message+vbCrLf + "정산코드(settleCode) : " + response.settleCode)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetSettleResult
    '=========================================================================
    Private Sub btnGetSettleResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSettleResult.Click

        '정산코드
        Dim SettleCode As String = "202301160000000010"

        Try
            Dim response As PaymentHistory = messageService.GetSettleResult (txtCorpNum.Text, SettleCode, txtUserId.Text)

            MsgBox(
                "결제 내용(productType) : " + response.productType + vbCrLf +
                "정액제 상품명(productName) : " + response.productName + vbCrLf +
                "결제 유형(settleType) : " + response.settleType + vbCrLf +
                "담당자명(settlerName) : " + response.settlerName + vbCrLf +
                "담당자메일(settlerEmail) : " + response.settlerEmail + vbCrLf +
                "결제 금액(settleCost) : " + response.settleCost + vbCrLf +
                "충전포인트(settlePoint) : " + response.settlePoint + vbCrLf +
                "결제 상태(settleState) : " + response.settleState.ToString + vbCrLf +
                "등록일시(regDT) : " + response.regDT + vbCrLf +
                "상태일시(stateDT) : " + response.stateDT
                )

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 결제내역을 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetPaymentHistory
    '=========================================================================
    Private Sub btnGetPaymentHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPaymentHistory.Click

        '조회 시작 일자
        Dim SDate As String = "20230501"

        '조회 종료 일자
        Dim EDate As String = "20230530"

        '목록 페이지 번호
        Dim Page  As Integer = 1

        '페이지당 목록 개수
        Dim PerPage  As Integer = 500

        Try
            Dim result As PaymentHistoryResult = messageService.GetPaymentHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, txtUserId.Text)

            Dim tmp As String = ""
            For Each history As PaymentHistory In result.list

            tmp += "결제 내용(productType) : " + history.productType + vbCrLf
            tmp += "정액제 상품명(productName) : " + history.productName + vbCrLf
            tmp += "결제 유형(settleType) : " + history.settleType + vbCrLf
            tmp += "담당자명(settlerName) : " + history.settlerName + vbCrLf
            tmp += "담당자메일(settlerEmail) : " + history.settlerEmail + vbCrLf
            tmp += "결제 금액(settleCost) : " + history.settleCost + vbCrLf
            tmp += "충전포인트(settlePoint) : " + history.settlePoint + vbCrLf
            tmp += "결제 상태(settleState) : " + history.settleState.ToString + vbCrLf
            tmp += "등록일시(regDT) : " + history.regDT + vbCrLf
            tmp += "상태일시(stateDT) : " + history.stateDT + vbCrLf
            tmp += vbCrLf

            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 사용내역을 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetUseHistory
    '=========================================================================
    Private Sub btnGetUseHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUseHistory.Click

        '조회 시작 일자
        Dim SDate As String = "20230501"

        '조회 종료 일자
        Dim EDate As String = "20230530"

        '목록 페이지 번호
        Dim Page  As Integer = 1

        '페이지당 목록 개수
        Dim PerPage  As Integer = 500

        '목록 정렬 방향
        Dim Order As String = "D"

        Try
            Dim result As UseHistoryResult = messageService.GetUseHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, Order, txtUserId.Text)

            Dim tmp As String = ""
            For Each history As UseHistory In result.list

                tmp += "서비스 코드(itemCode) : " + history.itemCode + vbCrLf
                tmp += "포인트 증감 유형(txType) : " + history.txType + vbCrLf
                tmp += "결제 유형(txPoint) : " + history.txPoint + vbCrLf
                tmp += "담당자명(balance) : " + history.balance + vbCrLf
                tmp += "담당자메일(txDT) : " + history.txDT + vbCrLf
                tmp += "결제 금액(userID) : " + history.userID + vbCrLf
                tmp += "충전포인트(userName) : " + history.userName + vbCrLf
                tmp += vbCrLf

            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트를 환불 신청합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#Refund
    '=========================================================================
    Private Sub btnRefund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefund.Click

        '환불신청 객체정보
        Dim refundForm As RefundForm = New RefundForm

        '담당자명
        refundForm.ContactName = "담당자명"

        '담당자 연락처
        refundForm.TEL = "010-1234-1234"

        '환불 신청 포인트
        refundForm.RequestPoint = "100"

        '은행명
        refundForm.AccountBank = "국민"

        '계좌 번호
        refundForm.AccountNum = "123-12-10981204"

        '예금주명
        refundForm.AccountName = "예금주"

        '환불 사유
        refundForm.Reason = "환불 사유"

        Try
            Dim response As RefundResponse = messageService.Refund(txtCorpNum.Text,refundForm, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf +
                        "응답메시지(message) : " + response.Message + vbCrLf +
                   "환불코드(refundCode) : " +response.refundCode )

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 환불신청내역을 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetRefundHistory
    '=========================================================================
    Private Sub btnGetRefundHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundHistory.Click

        '목폭 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500


        Try
            Dim result As RefundHistoryResult  = messageService.GetRefundHistory(txtCorpNum.Text,Page, PerPage, txtUserId.Text)

            Dim tmp As String = ""

            For Each history As RefundHistory In result.list
                tmp += "reqDT (신청일시) :" + history.reqDT + vbCrLf
                tmp += "requestPoint (환불 신청포인트) :" + history.requestPoint + vbCrLf
                tmp += "accountBank (환불계좌 은행명) :" + history.accountBank + vbCrLf
                tmp += "accountNum (환불계좌번호) :" + history.accountNum + vbCrLf
                tmp += "accountName (환불계좌 예금주명) :" + history.accountName + vbCrLf
                tmp += "state (상태) : " + history.state.ToString + vbCrLf
                tmp += "reason (환불사유) : " + history.reason + vbCrLf
            Next

            MsgBox("응답코드(code) : " + result.code.ToString + vbCrLf+
                   "총 검색결과 건수(total) : " + result.total.ToString + vbCrLf+
                   "페이지당 검색개수(perPage) : " + result.perPage.ToString +vbCrLf+
                   "페이지 번호(pageNum) : " + result.pageNum.ToString +vbCrLf+
                   "페이지 개수(pageCount) : " + result.pageCount.ToString +vbCrLf +
                   "사용내역"+vbCrLf+
                   tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 포인트 환불에 대한 상세정보 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetRefundInfo
    '=========================================================================
    Private Sub btnGetRefundInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '환불코드
        Dim refundCode As String = "023040000017"

        Try
            Dim history As RefundHistory  = messageService.GetRefundInfo(txtCorpNum.Text,refundCode, txtUserId.Text)

            MsgBox("reqDT (신청일시) :" + history.reqDT + vbCrLf+
                   "requestPoint (환불 신청포인트) :" + history.requestPoint + vbCrLf+
                   "accountBank (환불계좌 은행명) :" + history.accountBank + vbCrLf+
                   "accountNum (환불계좌번호) :" + history.accountNum + vbCrLf+
                   "accountName (환불계좌 예금주명) :" + history.accountName + vbCrLf+
                   "state (상태) : " + history.state.ToString + vbCrLf+
                   "reason (환불사유) : " + history.reason + vbCrLf
                   )

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 환불 가능한 포인트를 확인합니다. (보너스 포인트는 환불가능포인트에서 제외됩니다.)
    ' - https://developers.popbill.com/reference/sms/dotnet/api/point#GetRefundableBalance
    '=========================================================================
    Private Sub btnGetRefundableBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        Try
            Dim refundableCode As Double  = messageService.GetRefundableBalance(txtCorpNum.Text, txtUserId.Text)

            MsgBox("환불 가능 포인트(refundableCode) : " + refundableCode)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 가입된 연동회원의 탈퇴를 요청합니다.
    ' - 회원탈퇴 신청과 동시에 팝빌의 모든 서비스 이용이 불가하며, 관리자를 포함한 모든 담당자 계정도 일괄탈퇴 됩니다.
    ' - 회원탈퇴로 삭제된 데이터는 복원이 불가능합니다.
    ' - 관리자 계정만 회원탈퇴가 가능합니다.
    ' - https://developers.popbill.com/reference/sms/dotnet/api/member#QuitMember
    '=========================================================================
    Private Sub btnQuitMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '탈퇴사유
        Dim quitReason As String = "회원 탈퇴 사유"

        Try
            Dim response As Response  = messageService.QuitMember(txtCorpNum.Text, quitReason, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.Message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub
End Class
