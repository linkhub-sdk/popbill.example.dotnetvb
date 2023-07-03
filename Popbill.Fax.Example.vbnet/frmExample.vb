'=========================================================================
'
' 팝빌 팩스 API VB.Net SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내 : https://developers.popbill.com/guide/fax/dotnet/getting-started/tutorial?fwn=vb
' - 업데이트 일자 : 2023-07-03
' - 연동 기술지원 연락처 : 1600-9854
' - 연동 기술지원 이메일 : code@linkhubcorp.com
'
' <테스트 연동개발 준비사항>
' 1) 19, 22번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
'=========================================================================

Public Class frmExample

    '링크아이디
    Private LinkID As String = "TESTER"

    '비밀키. 유출에 주의하시기 바랍니다.
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '팩스 서비스 객체 생성
    Private faxService As FaxService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '팩스 서비스 객체 초기화
        faxService = New FaxService(LinkID, SecretKey)

        '연동환경 설정값, True-개발용, False-상업용
        faxService.IsTest = True

        '인증토큰 발급 IP 제한 On/Off, True-사용, False-미사용, 기본값(True)
        faxService.IPRestrictOnOff = True

        '팝빌 API 서비스 고정 IP 사용여부, True-사용, False-미사용, 기본값(False)
        faxService.UseStaticIP = False

        '로컬시스템 시간 사용여부, True-사용, False-미사용, 기본값(False)
        faxService.UseLocalTimeYN = False

    End Sub

    Private Function getReserveDT() As DateTime?
        If String.IsNullOrEmpty(txtReserveDT.Text) = False Then

            Return DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
        End If

    End Function

    '=========================================================================
    ' 팩스 발신번호 등록여부를 확인합니다.
    ' - 발신번호 상태가 '승인'인 경우에만 리턴값 'Response'의 변수 'code'가 1로 반환됩니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/sendnum#CheckSenderNumber
    '=========================================================================
    Private Sub btnCheckSenderNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckSenderNumber.Click
        Try
            Dim response As Response
            Dim senderNumber As String = ""

            response = faxService.CheckSenderNumber(txtCorpNum.Text, senderNumber)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 발신번호를 등록하고 내역을 확인하는 팩스 발신번호 관리 페이지 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/sendnum#GetSenderNumberMgtURL
    '=========================================================================
    Private Sub btnGetSenderNumberMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSenderNumberMgtURL.Click
        Try
            Dim url As String = faxService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록한 연동회원의 팩스 발신번호 목록을 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/sendnum#GetSenderNumberList
    '=========================================================================
    Private Sub btnGetSenderNumberList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSenderNumberList.Click
        Try
            Dim senderList As List(Of SenderNumber) = faxService.GetSenderNumberList(txtCorpNum.Text)

            Dim tmp As String = "number(발신번호) | representYN(대표번호여부) | state(인증상태) | memo(메모)" + vbCrLf
            For Each info As SenderNumber In senderList
                tmp += info.number + " | " + CStr(info.representYN) + " | " + CStr(info.state) + " | " + info.memo + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팩스 1건을 전송합니다. (최대 전송파일 개수: 20개)
    ' - https://developers.popbill.com/reference/fax/dotnet/api/send#SendFAX
    '=========================================================================
    Private Sub btnSenFax_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenFax_1.Click

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = ""

            '수신팩스번호
            Dim receiveNum As String = ""

            '수신자명
            Dim receiveName As String = "수신자명"

            '광고팩스 전송여부
            Dim adsYN As Boolean = False

            '팩스제목, 팩스내용에는 미기재, 전송내역 목록확인용
            Dim title As String = "팩스전송제목"

            ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            Dim requestNum = ""

            Try
                Dim receiptNum As String = faxService.SendFAX(txtCorpNum.Text, sendNum, receiveNum, receiveName, _
                                                              strFileName, getReserveDT(), txtUserId.Text, adsYN, title, requestNum)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
            End Try
        End If
    End Sub

    '=========================================================================
    ' 동일한 팩스파일을 다수의 수신자에게 전송하기 위해 팝빌에 접수합니다. (최대 1,000건)
    ' - https://developers.popbill.com/reference/fax/dotnet/api/send#SendFAXSame
    '=========================================================================
    Private Sub btnSenFax_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenFax_2.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = ""

            '수신정보배열, 최대 1000건
            Dim receivers As List(Of FaxReceiver) = New List(Of FaxReceiver)

            For i As Integer = 0 To 99
                Dim receiver As FaxReceiver = New FaxReceiver
                receiver.receiveNum = ""
                receiver.receiveName = "수신자명칭_" + CStr(i)
                receiver.interOPRefKey = "20220513-" + CStr(i)
                receivers.Add(receiver)
            Next i

            '광고팩스 전송여부
            Dim adsYN As Boolean = False

            '팩스제목, 팩스내용에는 미기재, 전송내역목록 확인용
            Dim title As String = "팩스전송 제목"

            ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            Dim requestNum = ""

            Try
                Dim receiptNum As String = faxService.SendFAX(txtCorpNum.Text, sendNum, receivers, strFileName, _
                                                              getReserveDT(), txtUserId.Text, adsYN, title, requestNum)
                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 팩스 1건을 전송합니다.(다중파일 전송) (최대 전송파일 개수: 20개)
    ' - https://developers.popbill.com/reference/fax/dotnet/api/send#SendFAXMulti
    '=========================================================================
    Private Sub btnSenFax_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenFax_3.Click
        Dim filepaths As List(Of String) = New List(Of String)

        '팩스전송파일, 최대 20개
        Do While fileDialog.ShowDialog(Me) = DialogResult.OK
            filepaths.Add(fileDialog.FileName)
        Loop

        If filepaths.Count > 0 Then

            '발신번호
            Dim sendNum As String = ""

            '수신번호
            Dim receiveNum As String = ""

            '수신자명
            Dim receiveName As String = "수신자명칭"

            '광고팩스 전송여부
            Dim adsYN As Boolean = False

            '팩스 제목, 팩스내용에는 기재되지 않음, 팩스전송내역 확인용
            Dim title As String = ""

            ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            Dim requestNum = ""

            Try
                Dim receiptNum As String = faxService.SendFAX(txtCorpNum.Text, sendNum, receiveNum, receiveName, _
                                                              filepaths, getReserveDT(), txtUserId.Text, adsYN, title, requestNum)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 동일한 팩스파일을 다수의 수신자에게 전송하기 위해 팝빌에 접수합니다.(다중파일 동보전송) (최대 전송파일 개수 : 20개) (최대 1,000건)
    ' - https://developers.popbill.com/reference/fax/dotnet/api/send#SendFAXMultiSame
    '=========================================================================
    Private Sub btnSenFax_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenFax_4.Click
        Dim filepaths As List(Of String) = New List(Of String)

        '팩스전송파일, 최대 20개
        Do While fileDialog.ShowDialog(Me) = DialogResult.OK
            filepaths.Add(fileDialog.FileName)
        Loop

        If filepaths.Count > 0 Then

            '발신번호
            Dim sendNum As String = ""

            '수신정보배열, 최대 1000건
            Dim receivers As List(Of FaxReceiver) = New List(Of FaxReceiver)

            For i As Integer = 0 To 99
                Dim receiver As FaxReceiver = New FaxReceiver
                receiver.receiveNum = ""
                receiver.receiveName = "수신자명칭_" + CStr(i)
                receiver.interOPRefKey = "20220513-" + CStr(i)
                receivers.Add(receiver)
            Next i

            '광고팩스 전송여부
            Dim adsYN As Boolean = False

            '팩스제목, 팩스내용에는 미기재, 전송내역목록 확인용
            Dim title As String = "팩스전송 제목"

            ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            Dim requestNum = ""

            Try
                Dim receiptNum As String = faxService.SendFAX(txtCorpNum.Text, sendNum, receivers, filepaths, _
                                                              getReserveDT(), txtUserId.Text, adsYN, title, requestNum)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 팩스 1건을 재전송합니다.
    ' - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/send#ResendFAX
    '=========================================================================
    Private Sub btnResendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResendFAX.Click

        '원본팩스 접수번호(receiptNum)
        Dim preReceiptNum As String = txtReceiptNum.Text

        '재전송 발신번호 (미기재시 기존 발신번호으로 전송 )
        Dim sendNum As String = ""

        '재전송 발신자명 (미기재시 기존 발신자명으로 전송)
        Dim sendName As String = ""

        '재전송 수신번호 (미기재시 기존 수신번호로 전송)
        Dim receiveNum As String = ""

        '재전송 수신자명 (미기재시 기존 수신자명으로 전송)
        Dim receiveName As String = ""

        '팩스제목, 팩스내용에는 미기재, 전송내역목록 확인용
        Dim title As String = "팩스전송 제목"

        Try
            Dim receiptNum As String = faxService.ResendFAX(txtCorpNum.Text, preReceiptNum, _
                    sendNum, sendName, receiveNum, receiveName, getReserveDT, txtUserId.Text, title)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 다수건의 팩스를 재전송합니다. (최대 전송파일 개수: 20개) (최대 1,000건)
    ' - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/send#ResendFAXSame
    '=========================================================================
    Private Sub btnResendFAX_Multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResendFAX_Multi.Click

        '원본팩스 접수번호(receiptNum)
        Dim preReceiptNum As String = txtReceiptNum.Text

        '재전송 발신번호 (미기재시 기존 발신번호으로 전송 )
        Dim sendNum As String = ""

        '재전송 발신자명 (미기재시 기존 발신자명으로 전송)
        Dim sendName As String = ""

        '수신정보배열, 최대 1000건
        Dim receivers As List(Of FaxReceiver) = New List(Of FaxReceiver)

        For i As Integer = 0 To 99
            Dim receiver As FaxReceiver = New FaxReceiver

            '수신팩스번호
            receiver.receiveNum = ""

            '수신자명
            receiver.receiveName = "수신자명칭_" + CStr(i)

            receiver.interOPRefKey = "20220513-" + CStr(i)
            receivers.Add(receiver)
        Next i

        '팩스전송 제목, 팩스내용에는 미기재 전송내역 목록확인용
        Dim title As String = "팩스전송 제목"

        Try
            Dim receiptNum As String = faxService.ResendFAX(txtCorpNum.Text, preReceiptNum, _
                    sendNum, sendName, receivers, getReserveDT, txtUserId.Text, title)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 팩스 1건을 재전송합니다.
    ' - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/send#ResendFAXRN
    '=========================================================================
    Private Sub btnResendFAXRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResendFAXRN.Click
        Try
            ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            Dim requestNum = ""

            '재전송 발신번호 (미기재시 기존 발신번호으로 전송 )
            Dim sendNum As String = ""

            '재전송 발신자명 (미기재시 기존 발신자명으로 전송)
            Dim sendName As String = ""

            '재전송 수신번호 (미기재시 기존 수신번호로 전송)
            Dim receiveNum As String = ""

            '재전송 수신자명 (미기재시 기존 수신자명으로 전송)
            Dim receiveName As String = ""

            '팩스제목, 팩스내용에는 미기재, 전송내역목록 확인용
            Dim title As String = "팩스전송 제목"

            Try
                Dim receiptNum As String = faxService.ResendFAXRN(txtCorpNum.Text, txtRequestNum.Text, requestNum, sendNum, sendName, _
                                                                  receiveNum, receiveName, getReserveDT, txtUserId.Text, title)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
            End Try
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 다수건의 팩스를 재전송합니다. (최대 전송파일 개수: 20개) (최대 1,000건)
    ' - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/send#ResendFAXRNSame
    '=========================================================================
    Private Sub btnResendFAXRN_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResendFAXRN_same.Click
        ' 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        ' 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '재전송 발신번호 (미기재시 기존 발신번호으로 전송 )
        Dim sendNum As String = ""

        '재전송 발신자명 (미기재시 기존 발신자명으로 전송)
        Dim sendName As String = ""

        '수신정보배열, 최대 1000건
        Dim receivers As List(Of FaxReceiver) = New List(Of FaxReceiver)

        For i As Integer = 0 To 99
            Dim receiver As FaxReceiver = New FaxReceiver

            '수신팩스번호
            receiver.receiveNum = ""

            '수신자명
            receiver.receiveName = "수신자명칭_" + CStr(i)

            receiver.interOPRefKey = "20220513-" + CStr(i)

            receivers.Add(receiver)
        Next i

        '팩스전송 제목, 팩스내용에는 미기재 전송내역 목록확인용
        Dim title As String = "팩스전송 제목"

        Try
            Dim receiptNum As String = faxService.ResendFAXRN(txtCorpNum.Text, txtRequestNum.Text, requestNum, sendNum, _
                                                              sendName, receivers, getReserveDT, txtUserId.Text, title)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 예약접수된 팩스 전송을 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://developers.popbill.com/reference/fax/dotnet/api/send#CancelReserve
    '=========================================================================
    Private Sub btnCancelReserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserve.Click
        Try
            Dim response As Response

            response = faxService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 예약접수된 팩스 전송을 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://developers.popbill.com/reference/fax/dotnet/api/send#CancelReserveRN
    '=========================================================================
    Private Sub btnCancelReserveRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserveRN.Click
        Try
            Dim response As Response

            response = faxService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에서 반환 받은 접수번호를 통해 팩스 전송상태 및 결과를 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/info#GetFaxResult
    '=========================================================================
    Private Sub btnGetFaxResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFaxResult.Click
        ListBox1.Items.Clear()
        Try
            Dim ResultList As List(Of FaxResult) = faxService.GetFaxResult(txtCorpNum.Text, txtReceiptNum.Text)

            Dim rowStr As String = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveNumType(수신번호 유형) | receiveName(수신번호) | "
            rowStr += "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | cancelPageCnt(취소 페이지수) | "
            rowStr += "reserveDT(예약시간) | receiptDT(접수일시) | sendDT(전송일시) | resultDT(전송결과 수신일시) | fileNames(전송 파일명 리스트) | receiptNum(접수번호) | "
            rowStr += "requestNum(요청번호) | interOPRefKey(파트너 지정키) | chargePageCnt(과금 페이지수) | refundPageCnt(환불 페이지수) | tiffFileSize(변환파일용량(단위:Byte))"

            ListBox1.Items.Add(rowStr)

            For Each Result As FaxResult In ResultList
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.sendNum + " | "
                rowStr += Result.senderName + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveNumType + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.title + " | "
                rowStr += Result.sendPageCnt.ToString + " | "
                rowStr += Result.successPageCnt.ToString + " | "
                rowStr += Result.failPageCnt.ToString + " | "
                rowStr += Result.cancelPageCnt.ToString + " | "
                rowStr += Result.reserveDT + " | "
                rowStr += Result.receiptDT + " | "
                rowStr += Result.sendDT + " | "
                rowStr += Result.resultDT + " | "

                For i As Integer = 0 To Result.fileNames.Count - 1
                    If i = Result.fileNames.Count - 1 Then
                        rowStr += Result.fileNames(i) + " | "
                    Else
                        rowStr += Result.fileNames(i) + ","
                    End If
                Next

                rowStr += Result.receiptNum + " | "
                rowStr += Result.requestNum + " | "
                rowStr += Result.interOPRefKey + " | "
                rowStr += Result.chargePageCnt + " | "
                rowStr += Result.refundPageCnt.ToString + " | "
                rowStr += Result.tiffFileSize

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 팩스 전송상태 및 결과를 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/info#GetFaxResultRN
    '=========================================================================
    Private Sub btnGetFaxResultRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFaxResultRN.Click
        ListBox1.Items.Clear()
        Try
            Dim ResultList As List(Of FaxResult) = faxService.GetFaxResultRN(txtCorpNum.Text, txtRequestNum.Text)

            Dim rowStr As String = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveNumType(수신번호 유형) | receiveName(수신번호) | "
            rowStr += "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | cancelPageCnt(취소 페이지수) | "
            rowStr += "reserveDT(예약시간) | receiptDT(접수일시) | sendDT(전송일시) | resultDT(전송결과 수신일시) | fileNames(전송 파일명 리스트) | receiptNum(접수번호) | "
            rowStr += "requestNum(요청번호) | interOPRefKey(파트너 지정키) | chargePageCnt(과금 페이지수) | refundPageCnt(환불 페이지수) | tiffFileSize(변환파일용량(단위:Byte))"

            ListBox1.Items.Add(rowStr)

            For Each Result As FaxResult In ResultList
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.sendNum + " | "
                rowStr += Result.senderName + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveNumType + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.title + " | "
                rowStr += Result.sendPageCnt.ToString + " | "
                rowStr += Result.successPageCnt.ToString + " | "
                rowStr += Result.failPageCnt.ToString + " | "
                rowStr += Result.cancelPageCnt.ToString + " | "
                rowStr += Result.reserveDT + " | "
                rowStr += Result.receiptDT + " | "
                rowStr += Result.sendDT + " | "
                rowStr += Result.resultDT + " | "

                For i As Integer = 0 To Result.fileNames.Count - 1
                    If i = Result.fileNames.Count - 1 Then
                        rowStr += Result.fileNames(i) + " | "
                    Else
                        rowStr += Result.fileNames(i) + ","
                    End If
                Next

                rowStr += Result.receiptNum + " | "
                rowStr += Result.requestNum + " | "
                rowStr += Result.interOPRefKey + " | "
                rowStr += Result.chargePageCnt + " | "
                rowStr += Result.refundPageCnt.ToString + " | "
                rowStr += Result.tiffFileSize

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건에 해당하는 팩스 전송내역 목록을 조회합니다. (조회기간 단위 : 최대 2개월)
    ' - 팩스 접수일시로부터 2개월 이내 접수건만 조회할 수 있습니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/info#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(4) As String

        '최대 검색기간 : 2개월 이내
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

        ' 예약여부 (false , true 중 택 1)
        ' └ false = 전체조회, true = 예약전송건 조회
        ' - 미입력시 기본값 false 처리
        Dim ReserveYN As Boolean = False

        ' 개인조회 여부 (false , true 중 택 1)
        ' false = 접수한 팩스 전체 조회 (관리자권한)
        ' true = 해당 담당자 계정으로 접수한 팩스만 조회 (개인권한)
        ' 미입력시 기본값 false 처리
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
            Dim faxSearchList As FAXSearchResult = faxService.Search(txtCorpNum.Text, SDate, EDate, State, _
                                                                       ReserveYN, SenderYN, Order, Page, PerPage, QString)
            Dim tmp As String

            tmp = "code (응답코드) : " + CStr(faxSearchList.code) + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + CStr(faxSearchList.total) + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + CStr(faxSearchList.perPage) + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + CStr(faxSearchList.pageNum) + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + CStr(faxSearchList.pageCount) + vbCrLf
            tmp = tmp + "message (응답메시지) : " + faxSearchList.message + vbCrLf + vbCrLf

            Dim rowStr As String = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveNumType(수신번호 유형) | receiveName(수신번호) | "
            rowStr += "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | cancelPageCnt(취소 페이지수) | "
            rowStr += "reserveDT(예약시간) | receiptDT(접수일시) | sendDT(전송일시) | resultDT(전송결과 수신일시) | fileNames(전송 파일명 리스트) | receiptNum(접수번호) | "
            rowStr += "requestNum(요청번호) | interOPRefKey(파트너 지정키) | chargePageCnt(과금 페이지수) | refundPageCnt(환불 페이지수) | tiffFileSize(변환파일용량(단위:Byte))"

            ListBox1.Items.Add(rowStr)

            For Each Result As FaxResult In faxSearchList.list

                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.sendNum + " | "
                rowStr += Result.senderName + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveNumType + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.title + " | "
                rowStr += Result.sendPageCnt.ToString + " | "
                rowStr += Result.successPageCnt.ToString + " | "
                rowStr += Result.failPageCnt.ToString + " | "
                rowStr += Result.cancelPageCnt.ToString + " | "
                rowStr += Result.reserveDT + " | "
                rowStr += Result.receiptDT + " | "
                rowStr += Result.sendDT + " | "
                rowStr += Result.resultDT + " | "

                For i As Integer = 0 To Result.fileNames.Count - 1
                    If i = Result.fileNames.Count - 1 Then
                        rowStr += Result.fileNames(i) + " | "
                    Else
                        rowStr += Result.fileNames(i) + ","
                    End If
                Next

                rowStr += Result.receiptNum + " | "
                rowStr += Result.requestNum + " | "
                rowStr += Result.interOPRefKey + " | "
                rowStr += Result.chargePageCnt + " | "
                rowStr += Result.refundPageCnt.ToString + " | "
                rowStr += Result.tiffFileSize

                ListBox1.Items.Add(rowStr)
            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트와 동일한 팩스 전송내역 확인 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/info#GetSentListURL
    '=========================================================================
    Private Sub btnGetSentListURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSentListURL.Click
        Try
            Dim url As String = faxService.GetSentListURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    '팩스 미리보기 팝업 URL을 반환하며, 팩스전송을 위한 TIF 포맷 변환 완료 후 호출 할 수 있습니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/info#GetPreviewURL
    '=========================================================================
    Private Sub btnGetPreviewURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPreviewURL.Click
        Try
            Dim url As String = faxService.GetPreviewURL(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = faxService.GetBalance(txtCorpNum.Text)

            MsgBox("remainPoint(연동회원 잔여포인트) : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = faxService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPaymentURL.Click
        Try
            Dim url As String = faxService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = faxService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = faxService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("remainPoint(파트너 잔여포인트) : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = faxService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팩스 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            '수신번호 유형, 일반 / 지능 중 택 1
            Dim receiveNumType As String = "지능"

            Dim unitCost As Single = faxService.GetUnitCost(txtCorpNum.Text, receiveNumType)

            MsgBox("unitCost(팩스전송 단가) : " + unitCost.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 팩스 API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click
        Try
            '수신번호 유형, 일반 / 지능 중 택 1
            Dim receiveNumType As String = "지능"

            Dim ChargeInfo As ChargeInfo = faxService.GetChargeInfo(txtCorpNum.Text, receiveNumType, txtUserId.Text)

            Dim tmp As String = "unitCost (전송단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = faxService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = faxService.CheckID(txtCorpNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#JoinMember
    '=========================================================================
    Private Sub btnJoinMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJoinMember.Click

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
            Dim response As Response = faxService.JoinMember(joinInfo)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = faxService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#GetCorpInfo
    '=========================================================================
    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCorpInfo.Click

        Try
            Dim corpInfo As CorpInfo = faxService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "ceoname(대표자성명) : " + corpInfo.ceoname + vbCrLf
            tmp += "corpName(상호) : " + corpInfo.corpName + vbCrLf
            tmp += "addr(주소) : " + corpInfo.addr + vbCrLf
            tmp += "bizType(업태) : " + corpInfo.bizType + vbCrLf
            tmp += "bizClass(종목) : " + corpInfo.bizClass + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#UpdateCorpInfo
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

            Dim response As Response = faxService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#RegistContact
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
        joinData.tel = "010-1234-1234"

        '담당자 이메일 (최대 100자)
        joinData.email = "test@email.com"

        '담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = faxService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = faxService.GetContactInfo(txtCorpNum.Text, contactID)

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
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = faxService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자명) | email(메일주소) | tel(연락처) |"
            tmp += "regDT(등록일시) | searchRole(담당자 권한) | mgrYN(관리자 여부) | state(상태)" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.email + " | " + info.tel + " | "
                tmp += info.regDT.ToString + " | " + info.searchRole.ToString + " | " + info.mgrYN.ToString + " | " + info.state + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException

            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#UpdateContact
    '=========================================================================
    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디 (6자이상 50자미만)
        joinData.id = "testkorea1120"

        '담당자 성명 (최대 100자)
        joinData.personName = "담당자명"

        '담당자 연락처 (최대 20자)
        joinData.tel = "010-1234-1234"

        '담당자 이메일 (최대 100자)
        joinData.email = "test@email.com"

        '담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = faxService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

        '=========================================================================
    ' 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#PaymentRequest
    '=========================================================================
    Private Sub btnPaymentRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentRequest.Click

        '입금신청 객체정보
        Dim paymentForm As New PaymentForm

        '담당자명
        paymentForm.settlerName = "담당자명"
        '담당자 이메일
        paymentForm.settlerEmail = "test@email.com"
        '담당자 휴대폰
        paymentForm.notifyHP = "010-1234-1234"
        '입금자명
        paymentForm.paymentName = "입금자명"
        '결제금액
        paymentForm.settleCost = "1000"

        Try
            Dim response As PaymentResponse = faxService.PaymentRequest(txtCorpNum.Text, paymentForm, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message+ vbCrLf + "settleCode(정산코드) : " + response.settleCode)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetSettleResult
    '=========================================================================
    Private Sub btnGetSettleResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSettleResult.Click

        '정산코드
        Dim SettleCode As String = "202301160000000010"

        Try
            Dim response As PaymentHistory = faxService.GetSettleResult (txtCorpNum.Text, SettleCode, txtUserId.Text)

            Dim tmp As String = ""

            tmp+ ="productType(결제 내용) : " + response.productType + vbCrLf
            tmp+ ="productName(결제 상품명) : " + response.productName + vbCrLf
            tmp+ ="settleType(결제 유형) : " + response.settleType + vbCrLf
            tmp+ ="settlerName(담당자명) : " + response.settlerName + vbCrLf
            tmp+ ="settlerEmail(담당자메일) : " + response.settlerEmail + vbCrLf
            tmp+ ="settleCost(결제 금액) : " + response.settleCost + vbCrLf
            tmp+ ="settlePoint(충전포인트) : " + response.settlePoint + vbCrLf
            tmp+ ="settleState(결제 상태) : " + response.settleState.ToString + vbCrLf
            tmp+ ="regDT(등록일시) : " + response.regDT + vbCrLf
            tmp+ ="stateDT(상태일시) : " + response.stateDT

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 결제내역을 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetPaymentHistory
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
            Dim result As PaymentHistoryResult = faxService.GetPaymentHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, txtUserId.Text)

            Dim tmp As String = ""
            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "결제내역"+ vbCrLf

            For Each history As PaymentHistory In result.list

            tmp += "productType(결제 내용) : " + history.productType + vbCrLf
            tmp += "productName(결제 상품명) : " + history.productName + vbCrLf
            tmp += "settleType(결제 유형) : " + history.settleType + vbCrLf
            tmp += "settlerName(담당자명) : " + history.settlerName + vbCrLf
            tmp += "settlerEmail(담당자메일) : " + history.settlerEmail + vbCrLf
            tmp += "settleCost(결제 금액) : " + history.settleCost + vbCrLf
            tmp += "settlePoint(충전포인트) : " + history.settlePoint + vbCrLf
            tmp += "settleState(결제 상태) : " + history.settleState.ToString + vbCrLf
            tmp += "regDT(등록일시) : " + history.regDT + vbCrLf
            tmp += "stateDT(상태일시) : " + history.stateDT + vbCrLf
            tmp += vbCrLf

            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 사용내역을 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetUseHistory
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
            Dim result As UseHistoryResult = faxService.GetUseHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, Order, txtUserId.Text)

            Dim tmp As String = ""
            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "사용내역"+ vbCrLf

            For Each history As UseHistory In result.list

                tmp += "itemCode(서비스 코드) : " + history.itemCode + vbCrLf
                tmp += "txType(포인트 증감 유형) : " + history.txType + vbCrLf
                tmp += "txPoint(결제 유형) : " + history.txPoint + vbCrLf
                tmp += "balance(담당자명) : " + history.balance + vbCrLf
                tmp += "txDT(담당자메일) : " + history.txDT + vbCrLf
                tmp += "userID(결제 금액) : " + history.userID + vbCrLf
                tmp += "userName(충전포인트) : " + history.userName + vbCrLf
                tmp += vbCrLf

            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트를 환불 신청합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#Refund
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
            Dim response As RefundResponse = faxService.Refund(txtCorpNum.Text,refundForm, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.Message + vbCrLf + "refundCode(환불코드) : " +response.refundCode )

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 환불신청내역을 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetRefundHistory
    '=========================================================================
    Private Sub btnGetRefundHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundHistory.Click

        '목폭 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500


        Try
            Dim result As RefundHistoryResult  = faxService.GetRefundHistory(txtCorpNum.Text,Page, PerPage, txtUserId.Text)

            Dim tmp As String = ""

            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "환불내역"+ vbCrLf

            For Each history As RefundHistory In result.list
                tmp += "reqDT(신청일시) :" + history.reqDT + vbCrLf
                tmp += "requestPoint(환불 신청포인트) :" + history.requestPoint + vbCrLf
                tmp += "accountBank(환불계좌 은행명) :" + history.accountBank + vbCrLf
                tmp += "accountNum(환불계좌번호) :" + history.accountNum + vbCrLf
                tmp += "accountName(환불계좌 예금주명) :" + history.accountName + vbCrLf
                tmp += "state(상태) : " + history.state.ToString + vbCrLf
                tmp += "reason(환불사유) : " + history.reason + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 포인트 환불에 대한 상세정보 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetRefundInfo
    '=========================================================================
    Private Sub btnGetRefundInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '환불코드
        Dim refundCode As String = "023040000017"

        Try
            Dim history As RefundHistory  = faxService.GetRefundInfo(txtCorpNum.Text,refundCode, txtUserId.Text)

            Dim tmp As String = ""

            tmp += "reqDT (신청일시) :" + history.reqDT + vbCrLf
            tmp += "requestPoint (환불 신청포인트) :" + history.requestPoint + vbCrLf
            tmp += "accountBank (환불계좌 은행명) :" + history.accountBank + vbCrLf
            tmp += "accountNum (환불계좌번호) :" + history.accountNum + vbCrLf
            tmp += "accountName (환불계좌 예금주명) :" + history.accountName + vbCrLf
            tmp += "state (상태) : " + history.state.ToString + vbCrLf
            tmp += "reason (환불사유) : " + history.reason + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 환불 가능한 포인트를 확인합니다. (보너스 포인트는 환불가능포인트에서 제외됩니다.)
    ' - https://developers.popbill.com/reference/fax/dotnet/api/point#GetRefundableBalance
    '=========================================================================
    Private Sub btnGetRefundableBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundableBalance.Click

        Try
            Dim refundableCode As Double  = faxService.GetRefundableBalance(txtCorpNum.Text, txtUserId.Text)

            MsgBox("refundableCode(환불 가능 포인트) : " + refundableCode.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 가입된 연동회원의 탈퇴를 요청합니다.
    ' - 회원탈퇴 신청과 동시에 팝빌의 모든 서비스 이용이 불가하며, 관리자를 포함한 모든 담당자 계정도 일괄탈퇴 됩니다.
    ' - 회원탈퇴로 삭제된 데이터는 복원이 불가능합니다.
    ' - 관리자 계정만 회원탈퇴가 가능합니다.
    ' - https://developers.popbill.com/reference/fax/dotnet/api/member#QuitMember
    '=========================================================================
    Private Sub btnQuitMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitMember.Click

        '탈퇴사유
        Dim quitReason As String = "회원 탈퇴 사유"

        Try
            Dim response As Response  = faxService.QuitMember(txtCorpNum.Text, quitReason, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.Message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub
End Class
