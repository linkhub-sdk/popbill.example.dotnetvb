'=========================================================================
'
' 팝빌 문자 API VB.Net SDK Example
'
' - VB.Net 연동환경 설정방법 안내 : https://docs.popbill.com/message/tutorial/dotnet#vb
' - 업데이트 일자 : 2021-08-05
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
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

        '연동환경 설정값 (True-개발용, False-상업용)
        messageService.IsTest = True

        '인증토큰의 IP제한기능 사용여부, (True-권장)
        messageService.IPRestrictOnOff = True

        '로컬PC 시간 사용 여부 True(사용), False(기본값) - 미사용
        messageService.UseLocalTimeYN = False

    End Sub

    Private Function getReserveDT() As DateTime?
        If String.IsNullOrEmpty(txtReserveDT.Text) = False Then

            Return DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
        End If

    End Function


    '=========================================================================
    ' 발신번호를 등록하고 내역을 확인하는 문자 발신번호 관리 페이지 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetSenderNumberMgtURL
    '=========================================================================
    Private Sub btnGetSenderNumberMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSenderNumberMgtURL.Click
        Try
            Dim url As String = messageService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록한 연동회원의 문자 발신번호 목록을 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetSenderNumberList
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 90byte의 단문(SMS) 메시지 1건 전송을 팝빌에 접수합니다.
    ' - https://docs.popbill.com/message/dotnet/api#SendSMS
    '=========================================================================
    Private Sub btnSendSMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS_one.Click

        '발신번호
        Dim sendNum As String = "07043042991"

        '발신자명
        Dim sendName As String = "발신자명"

        '수신번호
        Dim receiveNum As String = "010111222"

        '수신자명
        Dim receiveName As String = "수신자명칭"

        '메시지 내용
        Dim contents As String = "단문 문자메시지 내용, 각 메시지마다 개별설정 가능."

        ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '광고문자 여부 (기본값 False)
        Dim adsYN As Boolean = False

        Try

            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, sendNum, sendName, receiveNum, receiveName, _
                                                              contents, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 90byte의 단문(SMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 수신자마다 개별 내용을 전송할 수 있습니다(대량전송).
    ' - https://docs.popbill.com/message/dotnet/api#SendSMS_Multi
    '=========================================================================
    Private Sub btn_SendSMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SendSMS_hund.Click

        '전송정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '광고문자 여부 (기본값 False)
        Dim adsYN = True

        For i As Integer = 0 To 99

            Dim msg As Message = New Message

            '발신번호
            msg.sendNum = "07043042991"

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

            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)
            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 90byte의 단문(SMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
    ' - https://docs.popbill.com/message/dotnet/api#SendSMS_Same
    '=========================================================================
    Private Sub btnSendSMS_Same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS_Same.Click

        '발신번호
        Dim sendNum As String = "07043042991"

        '메시지 내용, 최대 90Byte(한글45자) 초과된 내용은 삭제되어 전송됨
        Dim contents As String = "다수의 수신자에게 동일한 문자를 전송하는 예제입니다"

        ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '광고문자 여부 (기본값 False)
        Dim adsYN As Boolean = False

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
            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, sendNum, contents, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MsgBox("접수번호(receiptNum) : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 2,000byte의 장문(LMS) 메시지 1건 전송을 팝빌에 접수합니다.
    ' - https://docs.popbill.com/message/dotnet/api#SendLMS
    '=========================================================================
    Private Sub btnSendLMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_one.Click

        '발신번호
        Dim sendNum As String = "07043042991"

        '발신자명
        Dim sendName As String = "발신자명"

        '수신번호
        Dim receiveNum As String = "010-111-2222"

        '수신자명
        Dim receiveName As String = "수신자명"

        '메시지 제목
        Dim subject As String = "장문 메시지 제목"

        '장문메시지 내용, 최대 2000byte
        Dim contents As String = "장문 메시지 내용. 최대 2000byte"

        ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '광고문자 여부 (기본값 False)
        Dim adsYN As Boolean = False

        Try
            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, sendNum, sendName, receiveNum, _
                                                              receiveName, subject, contents, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 2,000byte의 장문(LMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 수신자마다 개별 내용을 전송할 수 있습니다(대량전송).
    ' - https://docs.popbill.com/message/dotnet/api#SendLMS_Multi
    '=========================================================================
    Private Sub btnSendLMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_hund.Click

        ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '광고문자 여부 (기본값 False)
        Dim adsYN As Boolean = False

        '문자전송정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '발신번호
            msg.sendNum = "07043042991"

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
            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 2,000byte의 장문(LMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
    ' - https://docs.popbill.com/message/dotnet/api#SendLMS_Same
    '=========================================================================
    Private Sub btnSendLMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_same.Click

        '발신번호
        Dim sendNum As String = "07043042991"

        '메시지제목
        Dim subject As String = "메시지 제목"

        '장문메시지 내용, 최대 2000byte
        Dim contents As String = "장문메시지 내용, 최대 2000byte"

        ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '광고문자 여부 (기본값 False)
        Dim adsYN As Boolean = False

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
            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, sendNum, subject, contents, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 메시지 크기(90byte)에 따라 단문/장문(SMS/LMS)을 자동으로 인식하여 1건의 메시지를 전송을 팝빌에 접수합니다.
    ' - https://docs.popbill.com/message/dotnet/api#SendXMS
    '=========================================================================
    Private Sub btnSendXMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_one.Click

        '발신번호
        Dim sendNum As String = "07043042991"

        '발신자명
        Dim sendName As String = "발신자명"

        '수신번호
        Dim receiveNum As String = "010-111-2222"

        '수신자명
        Dim receiveName As String = "수신자명"

        '메시지 제목
        Dim subject As String = "장문 메시지 제목"

        '장문메시지 내용, 최대 2000byte
        Dim contents As String = "장문 메시지 내용. 최대 2000byte"

        ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '광고문자 여부 (기본값 False)
        Dim adsYN As Boolean = False

        Try
            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, sendNum, sendName, _
                                                              receiveNum, receiveName, subject, contents, getReserveDT(), txtUserId.Text, requestNum, adsYN)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 메시지 크기(90byte)에 따라 단문/장문(SMS/LMS)을 자동으로 인식하여 다수건의 메시지 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 수신자마다 개별 내용을 전송할 수 있습니다(대량전송).
    ' - https://docs.popbill.com/message/dotnet/api#SendXMS_Multi
    '=========================================================================
    Private Sub btnSendXMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_hund.Click

        ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '광고문자 여부 (기본값 False)
        Dim adsYN As Boolean = False

        '전송정보 배열, 최대 1000건
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            '발신번호
            msg.sendNum = "07043042991"

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
            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)

            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 메시지 크기(90byte)에 따라 단문/장문(SMS/LMS)을 자동으로 인식하여 다수건의 메시지 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
    ' - https://docs.popbill.com/message/dotnet/api#SendXMS_Same
    '=========================================================================
    Private Sub btnSendXMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_same.Click

        '발신번호
        Dim sendNum As String = "07043042991"

        '제목
        Dim subject As String = "메시지 제목"

        '메시지 내용
        Dim contents As String = "길이 자동인식 전송 메시지 내용"

        ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '광고문자 여부 (기본값 False)
        Dim adsYN As Boolean = False

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

            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, sendNum, subject, contents, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 최대 2,000byte의 메시지와 이미지로 구성된 포토문자(MMS) 1건 전송을 팝빌에 접수합니다.
    ' - 이미지 파일 포맷/규격 : 최대 300Kbyte(JPEG, JPG), 가로/세로 1,000px 이하 권장
    ' - https://docs.popbill.com/message/dotnet/api#SendMMS
    '=========================================================================
    Private Sub btnSendMMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMMS_one.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = "07043042991"

            '발신자명
            Dim sendName As String = "발신자명"

            '수신번호
            Dim receiveNum As String = "010-111-222"

            '수신자명
            Dim receiveName As String = "수신자명"

            '메시지 제목
            Dim subject As String = "포토 메시지 제목"

            '장문메시지 내용, 최대 2000byte
            Dim contents As String = "포토 메시지 내용. 최대 2000byte"

            ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            Dim requestNum = ""

            '광고문자 여부 (기본값 False)
            Dim adsYN As Boolean = False

            Try
                Dim receiptNum As String = messageService.SendMMS(txtCorpNum.Text, sendNum, receiveNum, _
                                                                  receiveName, subject, contents, strFileName, getReserveDT(), txtUserId.Text, requestNum, adsYN)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '===========================================================================
    ' 최대 2,000byte의 메시지와 이미지로 구성된 포토문자(MMS) 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
    ' - 이미지 파일 포맷/규격 : 최대 300Kbyte(JPEG), 가로/세로 1,000px 이하 권장
    ' - https://docs.popbill.com/message/dotnet/api#SendMMS_Same
    '===========================================================================
    Private Sub btnSendMMS_hundered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMMS_hundered.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = "07043042991"

            '메시지 제목
            Dim subject As String = "포토문자 전송 메시지제목"

            '포토 문자 메시지 내용, 최대 2000byte
            Dim contents As String = "포토 문자 메시지 내용, 최대 2000byte"

            ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            Dim requestNum = ""

            '광고문자 여부 (기본값 False)
            Dim adsYN As Boolean = False

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
                                                                  messages, strFileName, getReserveDT(), txtUserId.Text, requestNum, adsYN)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 최대 2,000byte의 메시지와 이미지로 구성된 포토문자(MMS) 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
    ' - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
    ' - 이미지 파일 포맷/규격 : 최대 300Kbyte(JPEG), 가로/세로 1,000px 이하 권장
    ' - https://docs.popbill.com/message/dotnet/api#SendMMS_Same
    '=========================================================================
    Private Sub btnSendMMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMMS_same.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = "07043042991"

            '메시지 제목
            Dim subject As String = "포토문자 전송 메시지제목"

            '포토 문자 메시지 내용, 최대 2000byte
            Dim contents As String = "포토 문자 메시지 내용, 최대 2000byte"

            ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            Dim requestNum = ""

            '광고문자 여부 (기본값 False)
            Dim adsYN As Boolean = False

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
                                                                  messages, strFileName, getReserveDT(), txtUserId.Text, requestNum, adsYN)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 예약접수된 문자 메시지 전송을 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://docs.popbill.com/message/dotnet/api#CancelReserve
    '=========================================================================
    Private Sub btnCancelReserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserve.Click
        Try
            Dim response As Response

            response = messageService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 예약접수된 문자 전송을 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://docs.popbill.com/message/dotnet/api#CancelReserveRN
    '=========================================================================
    Private Sub btnCancelReserveRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserveRN.Click
        Try
            Dim response As Response

            response = messageService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 문자 전송상태 및 결과를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetMessageResult
    '=========================================================================
    Private Sub btnGetMessageResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMessageResult.Click
        ListBox1.Items.Clear()
        Try
            Dim ResultList As List(Of MessageResult) = messageService.GetMessageResult(txtCorpNum.Text, txtReceiptNum.Text)

            Dim rowStr As String = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) | "
            rowStr += "receiptDT(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) | type(메시지 타입) | "
            rowStr += "tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호)"

            ListBox1.Items.Add(rowStr)

            For Each Result As MessageResult In ResultList
                rowStr = ""
                rowStr += Result.subject + " | " + Result.content + " | " + Result.sendNum + " | " + Result.senderName + " | " + Result.receiveNum + " | " + Result.receiveName + " | "
                rowStr += Result.receiptDT + " | " + Result.sendDT + " | " + Result.resultDT + " | " + Result.reserveDT + " | " + Result.state.ToString + " | " + Result.result.ToString + " | "
                rowStr += Result.type + " | " + Result.tranNet + " | " + Result.receiptNum + " | " + Result.requestNum

                ListBox1.Items.Add(rowStr)
            Next

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 문자 전송상태 및 결과를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetMessageResultRN
    '=========================================================================
    Private Sub btnGetMessageResultRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMessageResultRN.Click
        ListBox1.Items.Clear()
        Try
            Dim ResultList As List(Of MessageResult) = messageService.GetMessageResultRN(txtCorpNum.Text, txtRequestNum.Text)

            Dim rowStr As String = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) | "
            rowStr += "receiptDT(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) | type(메시지 타입) | "
            rowStr += "tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호)"

            ListBox1.Items.Add(rowStr)

            For Each Result As MessageResult In ResultList
                rowStr = ""
                rowStr += Result.subject + " | " + Result.content + " | " + Result.sendNum + " | " + Result.senderName + " | " + Result.receiveNum + " | " + Result.receiveName + " | "
                rowStr += Result.receiptDT + " | " + Result.sendDT + " | " + Result.resultDT + " | " + Result.reserveDT + " | " + Result.state.ToString + " | " + Result.result.ToString + " | "
                rowStr += Result.type + " | " + Result.tranNet + " | " + Result.receiptNum + " | " + Result.requestNum

                ListBox1.Items.Add(rowStr)
            Next

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 문자전송에 대한 전송결과 요약정보를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetStates
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건에 해당하는 문자 전송내역을 조회합니다. (조회기간 단위 : 최대 2개월)
    ' - 문자 접수일시로부터 6개월 이내 접수건만 조회할 수 있습니다.
    ' - https://docs.popbill.com/message/dotnet/api#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(4) As String
        Dim item(3) As String

        '최대 검색기간 : 6개월 이내
        '[필수] 시작일자, yyyyMMdd
        Dim SDate As String = "20210801"

        '[필수] 종료일자, yyyyMMdd
        Dim EDate As String = "20210805"

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

        '조회 검색어, 문자 전송시 기재한 수신자명 또는 발신자명 입력
        Dim QString As String = ""

        ListBox1.Items.Clear()
        Try
            Dim msgSearchList As MSGSearchResult = messageService.Search(txtCorpNum.Text, SDate, EDate, State, _
                                                                       item, ReserveYN, SenderYN, Order, Page, PerPage, QString)

            Dim tmp As String

            tmp = "code (응답코드) : " + CStr(msgSearchList.code) + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + CStr(msgSearchList.total) + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + CStr(msgSearchList.perPage) + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + CStr(msgSearchList.pageNum) + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + CStr(msgSearchList.pageCount) + vbCrLf
            tmp = tmp + "message (응답메시지) : " + msgSearchList.message + vbCrLf + vbCrLf

            Dim rowStr As String = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) | "
            rowStr += "receiptDT(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) | type(메시지 타입) | "
            rowStr += "tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호)"

            ListBox1.Items.Add(rowStr)

            For Each Result As MessageResult In msgSearchList.list
                rowStr = ""
                rowStr += Result.subject + " | " + Result.content + " | " + Result.sendNum + " | " + Result.senderName + " | " + Result.receiveNum + " | " + Result.receiveName + " | "
                rowStr += Result.receiptDT + " | " + Result.sendDT + " | " + Result.resultDT + " | " + Result.reserveDT + " | " + Result.state.ToString + " | " + Result.result.ToString + " | "
                rowStr += Result.type + " | " + Result.tranNet + " | " + Result.receiptNum + " | " + Result.requestNum

                ListBox1.Items.Add(rowStr)
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트와 동일한 문자 전송내역 확인 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetSentListURL
    '=========================================================================
    Private Sub btnGetSentListURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSentListURL.Click
        Try
            Dim url As String = messageService.GetSentListURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전용 080 번호에 등록된 수신거부 목록을 반환합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetAutoDenyList
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)를 통해 확인하시기 바랍니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click

        Try
            Dim remainPoint As Double = messageService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = messageService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPaymentURL.Click
        Try
            Dim url As String = messageService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = messageService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = messageService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = messageService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 문자(SMS) 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_SMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost_SMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.SMS

        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, msgType)

            MsgBox("단문 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 문자(LMS) 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetUnitCost
    '=========================================================================
    Private Sub btnGetUnitCost_LMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost_LMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.LMS

        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, msgType)

            MsgBox("장문 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 문자(MMS) 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetUnitCost
    '=========================================================================
    Private Sub btnGetUnitCost_MMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost_MMS.Click

        '문자 유형, SMS-단문, LMS-장문, MMS-포토
        Dim msgType As MessageType = MessageType.MMS

        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, msgType)

            MsgBox("포토문자 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 문자(SMS)API 서비스 과금정보를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetChargeInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 팝빌 문자(LMS) API 서비스 과금정보를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetChargeInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 문자(MMS) API 서비스 과금정보를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetChargeInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = messageService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = messageService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://docs.popbill.com/message/dotnet/api#JoinMember
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
        joinInfo.ContactEmail = "test@test.com"

        '담당자 연락처 (최대 20자)
        joinInfo.ContactTEL = "070-4304-2991"

        '담당자 휴대폰번호 (최대 20자)
        joinInfo.ContactHP = "010-111-222"

        '담당자 팩스번호 (최대 20자)
        joinInfo.ContactFAX = "02-6442-9700"

        Try
            Dim response As Response = messageService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = messageService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://docs.popbill.com/message/dotnet/api#RegistContact
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
        joinData.tel = "070-1111-2222"

        '담당자 휴대폰 (최대 20자)
        joinData.hp = "010-1234-1234"

        '담당자 팩스 (최대 20자)
        joinData.fax = "070-1234-1234"

        '담당자 이메일 (최대 100자)
        joinData.email = "test@test.com"

        '담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = messageService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = messageService.GetContactInfo(txtCorpNum.Text, contactID, txtUserId.Text)

            tmp += "id (담당자 아이디) : " + contactInfo.id + vbCrLf
            tmp += "personName (담당자명) : " + contactInfo.personName + vbCrLf
            tmp += "email (담당자 이메일) : " + contactInfo.email + vbCrLf
            tmp += "hp (휴대폰번호) : " + contactInfo.hp + vbCrLf
            tmp += "searchRole (담당자 권한) : " + contactInfo.searchRole.ToString() + vbCrLf
            tmp += "tel (연락처) : " + contactInfo.tel + vbCrLf
            tmp += "fax (팩스번호) : " + contactInfo.fax + vbCrLf
            tmp += "mgrYN (관리자 여부) : " + contactInfo.mgrYN.ToString() + vbCrLf
            tmp += "regDT (등록일시) : " + contactInfo.regDT + vbCrLf
            tmp += "state (상태) : " + contactInfo.state + vbCrLf

            tmp += vbCrLf

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = messageService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자명) | email(메일주소) | hp(휴대폰번호) | fax(팩스) | tel(연락처) |"
            tmp += "regDT(등록일시) | searchRole(담당자 권한) | mgrYN(관리자 여부) | state(상태)" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.email + " | " + info.hp + " | " + info.fax + " | " + info.tel + " | "
                tmp += info.regDT.ToString() + " | " + info.searchRole.ToString() + " | " + info.mgrYN.ToString() + " | " + info.state + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
    ' - https://docs.popbill.com/message/dotnet/api#UpdateContact
    '=========================================================================
    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디 (6자이상 50자미만)
        joinData.id = "testkorea1120"

        '담당자 성명 (최대 100자)
        joinData.personName = "담당자명"

        '담당자 연락처 (최대 20자)
        joinData.tel = "070-1111-2222"

        '담당자 휴대폰 (최대 20자)
        joinData.hp = "010-1234-1234"

        '담당자 팩스 (최대 20자)
        joinData.fax = "070-1234-1234"

        '담당자 이메일 (최대 100자)
        joinData.email = "test@test.com"

        '담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = messageService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://docs.popbill.com/message/dotnet/api#GetCorpInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다.
    ' - https://docs.popbill.com/message/dotnet/api#UpdateCorpInfo
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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub
End Class
