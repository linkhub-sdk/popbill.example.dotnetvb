'=========================================================================
'
' 팝빌 팩스 API VB.Net SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내 : https://docs.popbill.com/fax/tutorial/dotnet_vb
' - 업데이트 일자 : 2022-05-13
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
    ' - https://docs.popbill.com/fax/dotnet/api#CheckSenderNumber
    '=========================================================================
    Private Sub btnCheckSenderNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckSenderNumber.Click
        Try
            Dim response As Response
            Dim senderNumber As String = ""

            response = faxService.CheckSenderNumber(txtCorpNum.Text, senderNumber, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 발신번호를 등록하고 내역을 확인하는 팩스 발신번호 관리 페이지 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetSenderNumberMgtURL
    '=========================================================================
    Private Sub btnGetSenderNumberMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSenderNumberMgtURL.Click
        Try
            Dim url As String = faxService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록한 연동회원의 팩스 발신번호 목록을 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetSenderNumberList
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팩스 1건을 전송합니다. (최대 전송파일 개수: 20개)
    ' - 팩스전송 문서 파일포맷 안내 : https://docs.popbill.com/fax/format?lang=dotnet
    ' - https://docs.popbill.com/fax/dotnet/api#SendFAX
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
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try
        End If
    End Sub

    '=========================================================================
    ' 동일한 팩스파일을 다수의 수신자에게 전송하기 위해 팝빌에 접수합니다. (최대 1,000건)
    ' - 팩스전송 문서 파일포맷 안내 : https://docs.popbill.com/fax/format?lang=dotnet
    ' - https://docs.popbill.com/fax/dotnet/api#SendFAX_Same
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
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 팩스 1건을 전송합니다.(다중파일 전송) (최대 전송파일 개수: 20개)
    ' - 팩스전송 문서 파일포맷 안내 : https://docs.popbill.com/fax/format?lang=dotnet
    ' - https://docs.popbill.com/fax/dotnet/api#SendFAX_Multi
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
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 동일한 팩스파일을 다수의 수신자에게 전송하기 위해 팝빌에 접수합니다.(다중파일 동보전송) (최대 전송파일 개수 : 20개) (최대 1,000건)
    ' - 팩스전송 문서 파일포맷 안내 : https://docs.popbill.com/fax/format?lang=dotnet
    ' - https://docs.popbill.com/fax/dotnet/api#SendFAX_Multi_Same
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
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 팩스 1건을 재전송합니다.
    ' - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#ResendFAX
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 다수건의 팩스를 재전송합니다. (최대 전송파일 개수: 20개) (최대 1,000건)
    ' - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#ResendFAX_Same
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 팩스 1건을 재전송합니다.
    ' - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#ResendFAXRN
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
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 다수건의 팩스를 재전송합니다. (최대 전송파일 개수: 20개) (최대 1,000건)
    ' - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#ResendFAXRN_Same
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 예약접수된 팩스 전송을 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://docs.popbill.com/fax/dotnet/api#CancelReserve
    '=========================================================================
    Private Sub btnCancelReserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserve.Click
        Try
            Dim response As Response

            response = faxService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 예약접수된 팩스 전송을 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://docs.popbill.com/fax/dotnet/api#CancelReserveRN
    '=========================================================================
    Private Sub btnCancelReserveRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserveRN.Click
        Try
            Dim response As Response

            response = faxService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에서 반환 받은 접수번호를 통해 팩스 전송상태 및 결과를 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetFaxResult
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
                rowStr += Result.chargePageCnt.ToString + " | "
                rowStr += Result.refundPageCnt.ToString + " | "
                rowStr += Result.tiffFileSize

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 팩스 전송상태 및 결과를 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetFaxResultRN
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
                rowStr += Result.chargePageCnt.ToString + " | "
                rowStr += Result.refundPageCnt.ToString + " | "
                rowStr += Result.tiffFileSize

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건에 해당하는 팩스 전송내역 목록을 조회합니다. (조회기간 단위 : 최대 2개월)
    ' - 팩스 접수일시로부터 2개월 이내 접수건만 조회할 수 있습니다.
    ' - https://docs.popbill.com/fax/dotnet/api#Search
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
                rowStr += Result.chargePageCnt.ToString + " | "
                rowStr += Result.refundPageCnt.ToString + " | "
                rowStr += Result.tiffFileSize

                ListBox1.Items.Add(rowStr)
            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트와 동일한 팩스 전송내역 확인 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetSentListURL
    '=========================================================================
    Private Sub btnGetSentListURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSentListURL.Click
        Try
            Dim url As String = faxService.GetSentListURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    '팩스 미리보기 팝업 URL을 반환하며, 팩스전송을 위한 TIF 포맷 변환 완료 후 호출 할 수 있습니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetPreviewURL
    '=========================================================================
    Private Sub btnGetPreviewURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPreviewURL.Click
        Try
            Dim url As String = faxService.GetPreviewURL(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = faxService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = faxService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPaymentURL.Click
        Try
            Dim url As String = faxService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = faxService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = faxService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = faxService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팩스 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            '수신번호 유형, 일반 / 지능 중 택 1
            Dim receiveNumType As String = "지능"

            Dim unitCost As Single = faxService.GetUnitCost(txtCorpNum.Text, receiveNumType)

            MsgBox("팩스전송 단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 팩스 API 서비스 과금정보를 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetChargeInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = faxService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = faxService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#JoinMember
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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = faxService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetCorpInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다
    ' - https://docs.popbill.com/fax/dotnet/api#UpdateCorpInfo
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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#RegistContact
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
            Dim response As Response = faxService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = faxService.GetContactInfo(txtCorpNum.Text, contactID, txtUserId.Text)

            tmp += "id (담당자 아이디) : " + contactInfo.id + vbCrLf
            tmp += "personName (담당자명) : " + contactInfo.personName + vbCrLf
            tmp += "email (담당자 이메일) : " + contactInfo.email + vbCrLf
            tmp += "searchRole (담당자 권한) : " + contactInfo.searchRole.ToString() + vbCrLf
            tmp += "tel (연락처) : " + contactInfo.tel + vbCrLf
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
    ' - https://docs.popbill.com/fax/dotnet/api#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = faxService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자명) | email(메일주소) | tel(연락처) |"
            tmp += "regDT(등록일시) | searchRole(담당자 권한) | mgrYN(관리자 여부) | state(상태)" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.email + " | " + info.tel + " | "
                tmp += info.regDT.ToString() + " | " + info.searchRole.ToString() + " | " + info.mgrYN.ToString() + " | " + info.state + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#UpdateContact
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
            Dim response As Response = faxService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub
End Class
