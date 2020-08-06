'=========================================================================
'
' 팝빌 팩스 API VB.Net SDK Example
'
'' - VB.Net SDK 연동환경 설정방법 안내 : https://docs.popbill.com/fax/tutorial/dotnet#vb
' - 업데이트 일자 : 2020-06-01
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

    '비밀키. 유출에 주의하시기 바랍니다.
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '팩스 서비스 객체 생성
    Private faxService As FaxService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '팩스 서비스 객체 초기화
        faxService = New FaxService(LinkID, SecretKey)

        '연동환경 설정값 True(테스트용), False(상업용)
        faxService.IsTest = True

        '인증토큰의 IP제한기능 사용여부, (True-권장)
        faxService.IPRestrictOnOff = True

    End Sub

    Private Function getReserveDT() As DateTime?
        If String.IsNullOrEmpty(txtReserveDT.Text) = False Then

            Return DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
        End If

    End Function

    '=========================================================================
    ' 팩스 발신번호 관리 팝업 URL을 반합니다.
    ' - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetSenderNumberMgtURL
    '=========================================================================
    Private Sub btnGetSenderNumberMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSenderNumberMgtURL.Click
        Try
            Dim url As String = faxService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팩스 발신번호 목록을 반환합니다.
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
    ' 팩스를 전송합니다. (전송할 파일 개수는 최대 20개까지 가능)
    ' - 팩스전송 문서 파일포맷 안내 : https://docs.popbill.com/fax/format?lang=dotnet
    ' - https://docs.popbill.com/fax/dotnet/api#SendFAX
    '=========================================================================
    Private Sub btnSenFax_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenFax_1.Click

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = "070-4304-2991"

            '수신팩스번호
            Dim receiveNum As String = "070-111-2222"

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
    ' 다수의 수신자에게 단건파일 팩스를 전송합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#SendFAX_Same
    '=========================================================================
    Private Sub btnSenFax_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenFax_2.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            '발신번호
            Dim sendNum As String = "070-4304-2991"

            '수신정보배열, 최대 1000건 
            Dim receivers As List(Of FaxReceiver) = New List(Of FaxReceiver)

            For i As Integer = 0 To 99
                Dim receiver As FaxReceiver = New FaxReceiver
                receiver.receiveNum = "070-111-222"
                receiver.receiveName = "수신자명칭_" + CStr(i)
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
    ' 다수 파일 팩스를 전송합니다. (전송할 파일 개수는 최대 20개까지 가능)
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
            Dim sendNum As String = "070-4304-2991"

            '수신번호
            Dim receiveNum As String = "070-111-2222"

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
    ' 다수의 수신자에게 다수 파일 팩스를 전송합니다. (전송할 파일 개수는 최대 20개까지 가능)
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
            Dim sendNum As String = "070-4304-2991"

            '수신정보배열, 최대 1000건 
            Dim receivers As List(Of FaxReceiver) = New List(Of FaxReceiver)

            For i As Integer = 0 To 99
                Dim receiver As FaxReceiver = New FaxReceiver
                receiver.receiveNum = "111-2222-3333"
                receiver.receiveName = "수신자명칭_" + CStr(i)
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
    ' 팩스를 재전송합니다.
    ' - 접수일로부터 60일이 경과된 경우 재전송할 수 없습니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
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
    ' 다수의 수신자에게 팩스를 재전송합니다.
    ' - 접수일로부터 60일이 경과된 경우 재전송할 수 없습니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - https://docs.popbill.com/fax/dotnet/api#ResendFAX
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
            receiver.receiveNum = "070-111-222"

            '수신자명
            receiver.receiveName = "수신자명칭_" + CStr(i)
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
    ' 전송요청번호(requestNum)을 할당한 팩스를 재전송합니다.
    ' - 접수일로부터 60일이 경과된 경우 재전송할 수 없습니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
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
    ' [대량전송] 전송요청번호(requestNum)을 할당한 팩스를 재전송합니다.
    ' - 접수일로부터 60일이 경과된 경우 재전송할 수 없습니다.
    ' - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
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
            receiver.receiveNum = "070-111-222"

            '수신자명
            receiver.receiveName = "수신자명칭_" + CStr(i)
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
    ' 팩스전송요청시 발급받은 접수번호(receiptNum)로 팩스 예약전송건을 취소합니다.
    ' - 예약전송 취소는 예약전송시간 10분전까지 가능하며, 팩스변환 이후 가능합니다.
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
    ' 팩스전송요청시 할당한 전송요청번호(requestNum)로 팩스 예약전송건을 취소합니다.
    ' - 예약전송 취소는 예약전송시간 10분전까지 가능하며, 팩스변환 이후 가능합니다.
    '  - https://docs.popbill.com/fax/dotnet/api#CancelReserveRN
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
    ' 팩스전송요청시 발급받은 접수번호(receiptNum)로 전송결과를 확인합니다
    ' - https://docs.popbill.com/fax/dotnet/api#GetFaxResult
    '=========================================================================
    Private Sub btnGetFaxResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFaxResult.Click
        ListBox1.Items.Clear()
        Try
            Dim ResultList As List(Of FaxResult) = faxService.GetFaxResult(txtCorpNum.Text, txtReceiptNum.Text)

            Dim rowStr As String = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveName(수신번호) | receiveName(수신자명) | "
            rowStr += "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | refundPageCnt(환불 페이지수) | cancelPageCnt(취소 페이지수) | "
            rowStr += "reserveDT(예약시간) | receiptNum(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | fileNames(전송 파일명 리스트) | receiptNum(접수번호) | "
            rowStr += "requestNum(요청번호) | chargePageCnt(과금 페이지수) | tiffFileSize(변환파일용량(단위:Byte))"

            ListBox1.Items.Add(rowStr)

            For Each Result As FaxResult In ResultList
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.sendNum + " | "
                rowStr += Result.senderName + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.title + " | "
                rowStr += Result.sendPageCnt.ToString + " | "
                rowStr += Result.successPageCnt.ToString + " | "
                rowStr += Result.failPageCnt.ToString + " | "
                rowStr += Result.refundPageCnt.ToString + " | "
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
                rowStr += Result.chargePageCnt + " | "
                rowStr += Result.tiffFileSize

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팩스전송요청시 할당한 전송요청번호(requestNum)으로 전송결과를 확인합니다
    ' - https://docs.popbill.com/fax/dotnet/api#GetFaxResultRN
    '=========================================================================
    Private Sub btnGetFaxResultRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFaxResultRN.Click
        ListBox1.Items.Clear()
        Try
            Dim ResultList As List(Of FaxResult) = faxService.GetFaxResultRN(txtCorpNum.Text, txtRequestNum.Text)

            Dim rowStr As String = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveName(수신번호) | receiveName(수신자명) | "
            rowStr += "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | refundPageCnt(환불 페이지수) | cancelPageCnt(취소 페이지수) | "
            rowStr += "reserveDT(예약시간) | receiptNum(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | fileNames(전송 파일명 리스트) | receiptNum(접수번호) | "
            rowStr += "requestNum(요청번호) | chargePageCnt(과금 페이지수) | tiffFileSize(변환파일용량(단위:Byte))"

            ListBox1.Items.Add(rowStr)

            For Each Result As FaxResult In ResultList
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.sendNum + " | "
                rowStr += Result.senderName + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.title + " | "
                rowStr += Result.sendPageCnt.ToString + " | "
                rowStr += Result.successPageCnt.ToString + " | "
                rowStr += Result.failPageCnt.ToString + " | "
                rowStr += Result.refundPageCnt.ToString + " | "
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
                rowStr += Result.chargePageCnt + " | "
                rowStr += Result.tiffFileSize

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건을 사용하여 팩스전송 내역을 조회합니다.
    ' - 최대 검색기간 : 6개월 이내
    ' - https://docs.popbill.com/fax/dotnet/api#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(4) As String

        '최대 검색기간 : 6개월 이내
        '[필수] 시작일자, yyyyMMdd
        Dim SDate As String = "20190901"

        '[필수] 종료일자, yyyyMMdd
        Dim EDate As String = "20191231"

        '전송상태값 배열, 1-대기, 2-성공, 3-실패, 4-취소
        State(0) = "1"
        State(1) = "2"
        State(2) = "3"
        State(3) = "4"

        '예약팩스 검색여부, True(예약팩스만 조회), False(전체조회)
        Dim ReserveYN As Boolean = False

        '개인조회여부, True(개인조회), False(전체조회)
        Dim SenderYN As Boolean = False

        '페이지 번호
        Dim Page As Integer = 1

        '페이지 목록개수, 최대 1000건
        Dim PerPage As Integer = 10

        '정렬방향, D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

        '조회 검색어, 팩스 전송시 기재한 수신자명 또는 발신자명 입력
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

            Dim rowStr As String = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveName(수신번호) | receiveName(수신자명) | "
            rowStr += "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | refundPageCnt(환불 페이지수) | cancelPageCnt(취소 페이지수) | "
            rowStr += "reserveDT(예약시간) | receiptNum(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | fileNames(전송 파일명 리스트) | receiptNum(접수번호) | "
            rowStr += "requestNum(요청번호) | chargePageCnt(과금 페이지수) | tiffFileSize(변환파일용량(단위:Byte))"

            ListBox1.Items.Add(rowStr)

            For Each Result As FaxResult In faxSearchList.list
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.sendNum + " | "
                rowStr += Result.senderName + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.title + " | "
                rowStr += Result.sendPageCnt.ToString + " | "
                rowStr += Result.successPageCnt.ToString + " | "
                rowStr += Result.failPageCnt.ToString + " | "
                rowStr += Result.refundPageCnt.ToString + " | "
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
                rowStr += Result.chargePageCnt + " | "
                rowStr += Result.tiffFileSize

                ListBox1.Items.Add(rowStr)
            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팩스 전송내역 목록 팝업 URL을 반환합니다.
    ' 보안정책으로 인해 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetSentListURL
    '=========================================================================
    Private Sub btnGetSentListURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSentListURL.Click
        Try
            Dim url As String = faxService.GetSentListURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    '접수한 팩스 전송건에 대한 미리보기 팝업 URL을 반환합니다.
    ' - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetPreviewURL
    '=========================================================================
    Private Sub btnGetPreviewURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPreviewURL.Click
        Try
            Dim url As String = faxService.GetPreviewURL(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API) 를 통해 확인하시기 바랍니다.
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
    ' 연동회원 포인트 충전 URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = faxService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
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
    ' 파트너 포인트 충전 팝업 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = faxService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팩스 전송단가를 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            Dim unitCost As Single = faxService.GetUnitCost(txtCorpNum.Text)

            MsgBox("팩스전송 단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 팩스 API 서비스 과금정보를 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click
        Try
            Dim ChargeInfo As ChargeInfo = faxService.GetChargeInfo(txtCorpNum.Text)

            Dim tmp As String = "unitCost (전송단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
    ' - LinkID는 인증정보로 설정되어 있는 링크아이디 값입니다.
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
    ' 팝빌 회원아이디 중복여부를 확인합니다.
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
    ' 팝빌 연동회원 가입을 요청합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#JoinMember
    '=========================================================================
    Private Sub btnJoinMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJoinMember.Click

        Dim joinInfo As JoinForm = New JoinForm

        '아이디, 6자이상 50자 미만
        joinInfo.ID = "userid"

        '비밀번호, 6자이상 20자 미만
        joinInfo.PWD = "pwd_must_be_long_enough"

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
            Dim response As Response = faxService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌(www.popbill.com)에 로그인된 팝빌 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/fax/dotnet/api#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = faxService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자를 신규로 등록합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#RegistContact
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디 (6자이상 50자미만)
        joinData.id = "testkorea1120"

        '비밀번호 (6자이상 20자미만)
        joinData.pwd = "password"

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

        '회사조회 권한여부, True-회사조회, False-개인조회
        joinData.searchAllAllowYN = False

        '관리자 여부, True-관리자, False-사용자
        joinData.mgrYN = False


        Try
            Dim response As Response = faxService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자 목록을 확인합니다.
    ' - https://docs.popbill.com/fax/dotnet/api#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = faxService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자명) | email(메일주소) | hp(휴대폰번호) | fax(팩스) | tel(연락처) |"
            tmp += "regDT(등록일시) | searchAllAllowYN(회사조회 여부) | mgrYN(관리자 여부) | state(상태)" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.email + " | " + info.hp + " | " + info.fax + " | " + info.tel + " | "
                tmp += info.regDT.ToString() + " | " + info.searchAllAllowYN.ToString() + " | " + info.mgrYN.ToString() + " | " + info.state + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자 정보를 수정합니다.
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
        joinData.tel = "070-1111-2222"

        '담당자 휴대폰 (최대 20자)
        joinData.hp = "010-1234-1234"

        '담당자 팩스 (최대 20자)
        joinData.fax = "070-1234-1234"

        '담당자 이메일 (최대 100자)
        joinData.email = "test@test.com"

        '회사조회 권한여부, True-회사조회, False-개인조회
        joinData.searchAllAllowYN = False

        '관리자 여부, True-관리자, False-사용자
        joinData.mgrYN = False

        Try
            Dim response As Response = faxService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

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

End Class
