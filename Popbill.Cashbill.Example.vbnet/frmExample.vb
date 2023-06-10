'=========================================================================
'
' 팝빌 현금영수증 API VB.Net  SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내 : https://developers.popbill.com/guide/cashbill/dotnet/getting-started/tutorial?fwn=vb
' - 업데이트 일자 : 2022-11-08
' - 연동 기술지원 연락처 : 1600-9854
' - 연동 기술지원 이메일 : code@linkhubcorp.com
'
' <테스트 연동개발 준비사항>
' 1) 23, 26번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
'=========================================================================

Imports Popbill
Imports Popbill.Cashbill
Imports System.ComponentModel

Public Class frmExample

    '링크아이디
    Private LinkID As String = "TESTER"

    '비밀키
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '현금영수증 서비스 변수 선언
    Private cashbillService As CashbillService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '현금영수증 서비스 객체 초기화
        cashbillService = New CashbillService(LinkID, SecretKey)

        '연동환경 설정값, True-개발용, False-상업용
        cashbillService.IsTest = True

        '인증토큰 발급 IP 제한 On/Off, True-사용, False-미사용, 기본값(True)
        cashbillService.IPRestrictOnOff = True

        '팝빌 API 서비스 고정 IP 사용여부, True-사용, False-미사용, 기본값(False)
        cashbillService.UseStaticIP = False

        '로컬시스템 시간 사용여부, True-사용, False-미사용, 기본값(False)
        cashbillService.UseLocalTimeYN = False

    End Sub

    '=========================================================================
    ' 파트너가 현금영수증 관리 목적으로 할당하는 문서번호 사용여부를 확인합니다.
    ' - 이미 사용 중인 문서번호는 중복 사용이 불가하고, 현금영수증이 삭제된 경우에만 문서번호의 재사용이 가능합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/info#CheckMgtKeyInUse
    '=========================================================================
    Private Sub btnCheckMgtKeyInUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckMgtKeyInUse.Click

        Try
            Dim InUse As Boolean = cashbillService.CheckMgtKeyInUse(txtCorpNum.Text, txtMgtKey.Text)

            MsgBox(IIf(InUse, "사용중", "미사용중"))

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 작성된 현금영수증 데이터를 팝빌에 저장과 동시에 발행하여 "발행완료" 상태로 처리합니다.
    ' - 현금영수증 국세청 전송 정책 : https://developers.popbill.com/guide/cashbill/dotnet/introduction/policy-of-send-to-nts
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/issue#RegistIssue
    '=========================================================================
    Private Sub btnRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistIssue.Click
        Dim cashbill As Cashbill = New Cashbill

        '현금영수증 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        cashbill.mgtKey = txtMgtKey.Text

        '문서형태, [승인거래, 취소거래] 중 기재
        cashbill.tradeType = "승인거래"

        '거래구분, [소득공제용, 지출증빙용] 중 기재
        cashbill.tradeUsage = "소득공제용"

        '거래유형, [일반, 도서공연, 대중교통] 중 기재
        '- 미입력시 기본값 "일반" 처리
        cashbill.tradeOpt = "일반"

        '과세형태, [과세, 비과세] 중 기재
        cashbill.taxationType = "과세"

        '거래금액, 공급가액 + 봉사료 + 세액
        cashbill.totalAmount = "11000"

        '공급가액
        cashbill.supplyCost = "10000"

        '부가세
        cashbill.tax = "1000"

        '봉사료
        cashbill.serviceFee = "0"

        '가맹점 사업자번호, "-" 제외 10자리
        cashbill.franchiseCorpNum = txtCorpNum.Text

        '가맹점 종사업장 식별번호
        cashbill.franchiseTaxRegID = ""

        '가맹점 상호명
        cashbill.franchiseCorpName = "발행자 상호"

        '가맹점 대표자 성명
        cashbill.franchiseCEOName = "발행자 대표자"

        '가맹점 주소
        cashbill.franchiseAddr = "발행자 주소"

        '가맹점 전화번호
        cashbill.franchiseTEL = ""

        ' 식별번호, 거래구분에 따라 작성
        ' └ 소득공제용 - 주민등록/휴대폰/카드번호(현금영수증 카드)/자진발급용 번호(010-000-1234) 기재가능
        ' └ 지출증빙용 - 사업자번호/주민등록/휴대폰/카드번호(현금영수증 카드) 기재가능
        ' └ 주민등록번호 13자리, 휴대폰번호 10~11자리, 카드번호 13~19자리, 사업자번호 10자리 입력 가능
        cashbill.identityNum = "010-000-1234"

        '주문자명
        cashbill.customerName = "주문자명"

        '주문상품명
        cashbill.itemName = "주문상품명"

        '주문번호
        cashbill.orderNumber = "주문번호"

        '주문자 이메일
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        cashbill.email = ""

        '현금영수증 발행 알림문자 전송여부
        '미입력시 기본값 False 처리
        cashbill.smssendYN = False

        '주문자 휴대폰번호
        ' - {smssendYN} 의 값이 True 인 경우 아래 휴대폰번호로 안내 문자 전송
        cashbill.hp = ""

        '거래일시, 날짜(yyyyMMddHHmmss)
        '당일, 전일만 가능, 미입력시 기본값 발행일시 처리
        cashbill.tradeDT = "20221108000000"

        '메모
        Dim memo As String = "즉시발행 메모"

        '안내메일 제목, 공백처리시 기본양식으로 전송
        Dim emailSubject As String = ""

        Try
            Dim response As CBIssueResponse = cashbillService.RegistIssue(txtCorpNum.Text, cashbill, memo, txtUserId.Text, emailSubject)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message + vbCrLf _
                   + "국세청 승인번호(confirmNum) : " + response.confirmNum + vbCrLf + "거래일자(tradeDate) : " + response.tradeDate)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 100건의 현금영수증 발행을 한번의 요청으로 접수합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/issue#BulkSubmit
    '=========================================================================
    Private Sub btnBulkSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBulkSubmit.Click
        ' 현금영수증 객체정보 목록
        Dim cashbillList As List(Of Cashbill) = New List(Of Cashbill)

        For i = 0 To 99
            Dim cashbill As Cashbill = New Cashbill

            '현금영수증 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            cashbill.mgtKey = txtSubmitID.Text + i.ToString

            '[취소거래시 필수] 원본 현금영수증 국세청승인번호
            'cashbill.orgConfirmNum = ""

            '[취소거래시 필수] 원본 현금영수증 거래일자
            'cashbill.orgTradeDate = ""

            '문서형태, [승인거래, 취소거래] 중 기재
            cashbill.tradeType = "승인거래"

            '거래구분, [소득공제용, 지출증빙용] 중 기재
            cashbill.tradeUsage = "소득공제용"

            '거래유형, [일반, 도서공연, 대중교통] 중 기재
            '- 미입력시 기본값 "일반" 처리
            cashbill.tradeOpt = "일반"

            '과세형태, [과세, 비과세] 중 기재
            cashbill.taxationType = "과세"

            '거래금액, 공급가액 + 봉사료 + 세액
            cashbill.totalAmount = "11000"

            '공급가액
            cashbill.supplyCost = "10000"

            '부가세
            cashbill.tax = "1000"

            '봉사료
            cashbill.serviceFee = "0"

            '가맹점 사업자번호, "-" 제외 10자리
            cashbill.franchiseCorpNum = txtCorpNum.Text

            '가맹점 종사업장 식별번호
            cashbill.franchiseTaxRegID = ""

            '가맹점 상호명
            cashbill.franchiseCorpName = "발행자 상호"

            '가맹점 대표자 성명
            cashbill.franchiseCEOName = "발행자 대표자"

            '가맹점 주소
            cashbill.franchiseAddr = "발행자 주소"

            '가맹점 전화번호
            cashbill.franchiseTEL = ""

            '식별번호, 거래유형에 따라 작성
            '└ 소득공제용 - 주민등록/휴대폰/카드번호(현금영수증 카드)/자진발급용 번호(010-000-1234) 기재가능
            '└ 지출증빙용 - 사업자번호/주민등록/휴대폰/카드번호(현금영수증 카드) 기재가능
            '└ 주민등록번호 13자리, 휴대폰번호 10~11자리, 카드번호 13~19자리, 사업자번호 10자리 입력 가능
            cashbill.identityNum = "010-000-1234"

            '주문자명
            cashbill.customerName = "주문자명"

            '주문상품명
            cashbill.itemName = "주문상품명"

            '주문번호
            cashbill.orderNumber = "주문번호"

            '주문자 이메일
            '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
            '실제 거래처의 메일주소가 기재되지 않도록 주의
            cashbill.email = ""

            '현금영수증 발행 알림문자 전송여부
            '미입력시 기본값 False 처리
            cashbill.smssendYN = False

            '주문자 휴대폰번호
            '- {smssendYN} 의 값이 true 인 경우 아래 휴대폰번호로 안내 문자 전송
            cashbill.hp = ""

            '거래일시, 날짜(yyyyMMddHHmmss)
            '당일, 전일만 가능, 미입력시 기본값 발행일시 처리
            cashbill.tradeDT = "20221108000000"

            cashbillList.Add(cashbill)
        Next

        Try
            Dim response As BulkResponse = cashbillService.BulkSubmit(txtCorpNum.Text, txtSubmitID.Text, cashbillList)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message + vbCrLf + "접수아이디(receiptID) : " + response.receiptID)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 접수시 기재한 SubmitID를 사용하여 현금영수증 접수결과를 확인합니다.
    ' - 개별 현금영수증 처리상태는 접수상태(txState)가 완료(2) 시 반환됩니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/issue#GetBulkResult
    '=========================================================================
    Private Sub btnGetBulkResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBulkResult.Click
        Try
            Dim result As BulkCashbillResult = cashbillService.GetBulkResult(txtCorpNum.Text, txtSubmitID.Text)

            Dim tmp As String = ""

            tmp += "응답 코드(code) : " + result.code.ToString + vbCrLf
            tmp += "응답메시지(message) : " + result.message + vbCrLf
            tmp += "제출아이디(submitID) : " + result.submitID + vbCrLf
            tmp += "현금영수증 접수 건수(submitCount) : " + result.submitCount.ToString + vbCrLf
            tmp += "현금영수증 발행 성공 건수(successCount) : " + result.successCount.ToString + vbCrLf
            tmp += "현금영수증 발행 실패 건수(failCount) : " + result.failCount.ToString + vbCrLf
            tmp += "접수상태코드(txState) : " + result.txState.ToString + vbCrLf
            tmp += "접수 결과코드(txResultCode) : " + result.txResultCode.ToString + vbCrLf
            tmp += "발행처리 시작일시(txStartDT) : " + result.txStartDT + vbCrLf
            tmp += "발행처리 완료일시(txEndDT) : " + result.txEndDT + vbCrLf
            tmp += "접수일시(receiptDT) : " + result.receiptDT + vbCrLf
            tmp += "접수아이디(receiptID) : " + result.receiptID + vbCrLf

            If Not result.issueResult Is Nothing Then
                Dim i As Integer = 1
                For Each issueResult As BulkCashbillIssueResult In result.issueResult
                    tmp += "===========발행결과[" + i.ToString + "/" + result.issueResult.Count.ToString + "]===========" + vbCrLf
                    tmp += "응답코드(code) : " + issueResult.code.ToString + vbCrLf
                    tmp += "응답메시지(message) : " + issueResult.message + vbCrLf
                    tmp += "문서번호(MgtKey) : " + issueResult.mgtKey + vbCrLf
                    tmp += "국세청승인번호(confirmNum) : " + issueResult.confirmNum + vbCrLf
                    tmp += "거래일자(tradeDate) : " + issueResult.tradeDate + vbCrLf
                    tmp += "발행일시(issueDT) : " + issueResult.issueDT + vbCrLf
                    i = i + 1
                Next
            End If

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 현금영수증을 삭제합니다.
    ' - 삭제 가능한 상태: "전송실패"
    ' - 현금영수증을 삭제하면 사용된 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/issue#Delete
    '=========================================================================
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            Dim response As Response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 현금영수증을 삭제합니다.
    ' - 삭제 가능한 상태: "전송실패"
    ' - 현금영수증을 삭제하면 사용된 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/issue#Delete
    '=========================================================================
    Private Sub btnDeleteSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSub.Click

        Try
            Dim response As Response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 현금영수증을 삭제합니다.
    ' - 삭제 가능한 상태: "전송실패"
    ' - 현금영수증을 삭제하면 사용된 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/issue#Delete
    '=========================================================================
    Private Sub btnDelete02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete02.Click
        Try
            Dim response As Response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 취소 현금영수증 데이터를 팝빌에 저장과 동시에 발행하여 "발행완료" 상태로 처리합니다.
    ' - 현금영수증 국세청 전송 정책 : https://developers.popbill.com/guide/cashbill/dotnet/introduction/policy-of-send-to-nts
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/issue#RevokeRegistIssue
    '=========================================================================
    Private Sub btnRevokRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevokRegistIssue.Click

        '원본현금영수증 국세청 승인번호
        Dim orgConfirmNum As String = "TB0000013"

        '원본현금영수증 거래일자
        Dim orgTradeDate As String = "20220501"

        '발행 안내문자 전송여부
        Dim smssendYN As Boolean = False

        '취소현금영수증 메모
        Dim memo As String = "취소현금영수증 발행 메모"

        Try
            Dim response As CBIssueResponse = cashbillService.RevokeRegistIssue(txtCorpNum.Text, txtMgtKey.Text, orgConfirmNum, orgTradeDate, smssendYN, memo)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message + vbCrLf +
                   "국세청 승인번호(confirmNum) : " + response.confirmNum + vbCrLf +
                 "거래일자(tradeDate) : " + response.tradeDate)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 작성된 (부분)취소 현금영수증 데이터를 팝빌에 저장과 동시에 발행하여 "발행완료" 상태로 처리합니다.
    ' - 취소 현금영수증의 금액은 원본 금액을 넘을 수 없습니다.
    ' - 현금영수증 국세청 전송 정책 : https://developers.popbill.com/guide/cashbill/dotnet/introduction/policy-of-send-to-nts
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/issue#RevokeRegistIssue
    '=========================================================================
    Private Sub btnRevokeRegistIssue_part_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevokeRegistIssue_part.Click

        '원본현금영수증 국세청 승인번호
        Dim orgConfirmNum As String = "TB0000013"

        '원본현금영수증 거래일자
        Dim orgTradeDate As String = "20220501"

        '안내 문자 전송여부 , True / False 중 택 1
        '└ True = 전송 , False = 미전송
        '└ 원본 현금영수증의 구매자(고객)의 휴대폰번호 문자 전송
        Dim smssendYN As Boolean = False

        '취소현금영수증 메모
        Dim memo As String = "취소현금영수증 발행 메모"

        '부분취소 여부
        Dim isPartCancel As Boolean = True

        '취소사유 , 1 / 2 / 3 중 택 1
        '└ 1 = 거래취소 , 2 = 오류발급취소 , 3 = 기타
        '└ 미입력시 기본값 1 처리
        Dim cancelType As Integer = 1

        '[취소] 공급가액
        '취소할 공급가액 입력
        Dim supplyCost As String = "2000"

        '[취소] 세액
        '취소할 세액 입력
        Dim tax As String = "200"

        '[취소] 봉사료
        '취소할 봉사료 입력
        Dim serviceFee As String = "0"

        '[취소] 거래금액 (공급가액+세액+봉사료)
        '취소할 거래금액 입력
        Dim totalAmount As String = "2200"

        '안내메일 제목, 공백처리시 기본양식으로 전송
        Dim emailSubject As String = "메일제목 테스트"

        '거래일시, 날짜(yyyyMMddHHmmss)
        '당일, 전일만 가능, 미입력시 기본값 발행일시 처리
        Dim tradeDT As String = "20221108000000"

        Try
            Dim response As CBIssueResponse = cashbillService.RevokeRegistIssue(txtCorpNum.Text, txtMgtKey.Text, orgConfirmNum, orgTradeDate, _
                                                                         smssendYN, memo, txtUserId.Text, isPartCancel, cancelType, supplyCost, _
                                                                         tax, serviceFee, totalAmount, emailSubject, tradeDT)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message + vbCrLf _
                + "국세청 승인번호(confirmNum) : " + response.confirmNum + vbCrLf +
                 "거래일자(tradeDate) : " + response.tradeDate)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 현금영수증 1건의 상태 및 요약정보를 확인합니다.
    ' - 리턴값 'CashbillInfo'의 변수 'stateCode'를 통해 현금영수증의 상태코드를 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/info#GetInfo
    '=========================================================================
    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click
        Try
            Dim cbInfo As CashbillInfo = cashbillService.GetInfo(txtCorpNum.Text, txtMgtKey.Text)

            Dim tmp As String = ""
            tmp += "itemKey (팝빌번호) : " + cbInfo.itemKey + vbCrLf
            tmp += "mgtKey (문서번호) : " + cbInfo.mgtKey + vbCrLf
            tmp += "tradeDate (거래일자) : " + cbInfo.tradeDate + vbCrLf
            tmp += "tradeDT (거래일시) : " + cbInfo.tradeDT + vbCrLf
            tmp += "tradeType (문서형태) : " + cbInfo.tradeType + vbCrLf
            tmp += "tradeUsage (거래구분) : " + cbInfo.tradeUsage + vbCrLf
            tmp += "tradeOpt (거래유형) : " + cbInfo.tradeOpt + vbCrLf
            tmp += "taxationType (과세형태) : " + cbInfo.taxationType + vbCrLf
            tmp += "totalAmount (거래금액) : " + cbInfo.totalAmount + vbCrLf
            tmp += "issueDT (발행일시) : " + cbInfo.issueDT + vbCrLf
            tmp += "regDT (등록일시) : " + cbInfo.regDT + vbCrLf
            tmp += "stateMemo (상태메모) : " + cbInfo.stateMemo + vbCrLf
            tmp += "stateCode (상태코드) : " + cbInfo.stateCode.ToString + vbCrLf
            tmp += "stateDT (상태변경일시) : " + cbInfo.stateDT + vbCrLf
            tmp += "identityNum (식별번호) : " + cbInfo.identityNum + vbCrLf
            tmp += "itemName (주문상품명) : " + cbInfo.itemName + vbCrLf
            tmp += "customerName (주문자명) : " + cbInfo.customerName + vbCrLf
            tmp += "confirmNum (국세청승인번호) : " + cbInfo.confirmNum + vbCrLf
            tmp += "orgConfirmNum (원본 현금영수증 국세청승인번호) : " + cbInfo.orgConfirmNum + vbCrLf
            tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cbInfo.orgTradeDate + vbCrLf
            tmp += "ntssendDT (국세청 전송일시) : " + cbInfo.ntssendDT + vbCrLf
            tmp += "ntsresultDT (국세청 처리결과 수신일시) : " + cbInfo.ntsresultDT + vbCrLf
            tmp += "ntsresultCode (국세청 처리결과 상태코드) : " + cbInfo.ntsresultCode + vbCrLf
            tmp += "ntsresultMessage (국세청 처리결과 메시지) : " + cbInfo.ntsresultMessage + vbCrLf
            tmp += "printYN (인쇄여부) : " + cbInfo.printYN.ToString + vbCrLf
            tmp += "interOPYN (연동문서여부) : " + cbInfo.interOPYN.ToString + vbCrLf + vbCrLf
            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 다수건의 현금영수증 상태 및 요약 정보를 확인합니다. (1회 호출 시 최대 1,000건 확인 가능)
    ' - 리턴값 'CashbillInfo'의 변수 'stateCode'를 통해 현금영수증의 상태코드를 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/info#GetInfos
    '=========================================================================
    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서번호 배열, 최대 1000건.
        MgtKeyList.Add("20220513-001")
        MgtKeyList.Add("20220513-002")

        Try
            Dim cashbillInfoList As List(Of CashbillInfo) = cashbillService.GetInfos(txtCorpNum.Text, MgtKeyList)

            Dim tmp As String = ""

            For Each cbInfo As CashbillInfo In cashbillInfoList
                tmp += "itemKey (팝빌번호) : " + cbInfo.itemKey + vbCrLf
                tmp += "mgtKey (문서번호) : " + cbInfo.mgtKey + vbCrLf
                tmp += "tradeDate (거래일자) : " + cbInfo.tradeDate + vbCrLf
                tmp += "tradeDT (거래일시) : " + cbInfo.tradeDT + vbCrLf
                tmp += "tradeType (문서형태) : " + cbInfo.tradeType + vbCrLf
                tmp += "tradeUsage (거래구분) : " + cbInfo.tradeUsage + vbCrLf
                tmp += "tradeOpt (거래유형) : " + cbInfo.tradeOpt + vbCrLf
                tmp += "taxationType (과세형태) : " + cbInfo.taxationType + vbCrLf
                tmp += "totalAmount (거래금액) : " + cbInfo.totalAmount + vbCrLf
                tmp += "issueDT (발행일시) : " + cbInfo.issueDT + vbCrLf
                tmp += "regDT (등록일시) : " + cbInfo.regDT + vbCrLf
                tmp += "stateMemo (상태메모) : " + cbInfo.stateMemo + vbCrLf
                tmp += "stateCode (상태코드) : " + cbInfo.stateCode.ToString + vbCrLf
                tmp += "stateDT (상태변경일시) : " + cbInfo.stateDT + vbCrLf
                tmp += "identityNum (식별번호) : " + cbInfo.identityNum + vbCrLf
                tmp += "itemName (주문상품명) : " + cbInfo.itemName + vbCrLf
                tmp += "customerName (주문자명) : " + cbInfo.customerName + vbCrLf
                tmp += "confirmNum (국세청승인번호) : " + cbInfo.confirmNum + vbCrLf
                tmp += "orgConfirmNum (원본 현금영수증 국세청승인번호) : " + cbInfo.orgConfirmNum + vbCrLf
                tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cbInfo.orgTradeDate + vbCrLf
                tmp += "ntssendDT (국세청 전송일시) : " + cbInfo.ntssendDT + vbCrLf
                tmp += "ntsresultDT (국세청 처리결과 수신일시) : " + cbInfo.ntsresultDT + vbCrLf
                tmp += "ntsresultCode (국세청 처리결과 상태코드) : " + cbInfo.ntsresultCode + vbCrLf
                tmp += "ntsresultMessage (국세청 처리결과 메시지) : " + cbInfo.ntsresultMessage + vbCrLf
                tmp += "printYN (인쇄여부) : " + cbInfo.printYN.ToString + vbCrLf
                tmp += "interOPYN (연동문서여부) : " + cbInfo.interOPYN.ToString + vbCrLf + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 1건의 상세정보를 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/info#GetDetailInfo
    '=========================================================================
    Private Sub btnGetDetailInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDetailInfo.Click

        Try
            Dim cbDetailInfo As Cashbill = cashbillService.GetDetailInfo(txtCorpNum.Text, txtMgtKey.Text)

            '자세한 문세정보는 작성시 항목을 참조하거나, 연동메뉴얼 참조.

            Dim tmp As String = ""

            tmp += "mgtKey (문서번호) : " + cbDetailInfo.mgtKey + vbCrLf
            tmp += "confirmNum (국세청승인번호) : " + cbDetailInfo.confirmNum + vbCrLf
            tmp += "orgConfirmNum (원본 현금영수증 국세청승인번호) : " + cbDetailInfo.orgConfirmNum + vbCrLf
            tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cbDetailInfo.orgTradeDate + vbCrLf
            tmp += "tradeDate (거래일자) : " + cbDetailInfo.tradeDate + vbCrLf
            tmp += "tradeDT (거래일시) : " + cbDetailInfo.tradeDT + vbCrLf
            tmp += "tradeType (문서형태) : " + cbDetailInfo.tradeType + vbCrLf
            tmp += "tradeUsage (거래구분) : " + cbDetailInfo.tradeUsage + vbCrLf
            tmp += "tradeOpt (거래유형) : " + cbDetailInfo.tradeOpt + vbCrLf
            tmp += "taxationType (과세형태) : " + cbDetailInfo.taxationType + vbCrLf
            tmp += "totalAmount (거래금액) : " + cbDetailInfo.totalAmount + vbCrLf
            tmp += "supplyCost (공급가액) : " + cbDetailInfo.supplyCost + vbCrLf
            tmp += "tax (부가세) : " + cbDetailInfo.tax + vbCrLf
            tmp += "serviceFee (봉사료) : " + cbDetailInfo.serviceFee + vbCrLf
            tmp += "franchiseCorpNum (가맹점 사업자번호) : " + cbDetailInfo.franchiseCorpNum + vbCrLf
            tmp += "franchiseTaxRegID (가맹점 종사업장 식별번호) : " + cbDetailInfo.franchiseTaxRegID + vbCrLf
            tmp += "franchiseCorpName (가맹점 상호) : " + cbDetailInfo.franchiseCorpName + vbCrLf
            tmp += "franchiseCEOName (가맹점 대표자 성명) : " + cbDetailInfo.franchiseCEOName + vbCrLf
            tmp += "franchiseAddr (가맹점 주소) : " + cbDetailInfo.franchiseAddr + vbCrLf
            tmp += "franchiseTEL (가맹점 전화번호) : " + cbDetailInfo.franchiseTEL + vbCrLf
            tmp += "identityNum (식별번호) : " + cbDetailInfo.identityNum + vbCrLf
            tmp += "customerName (주문자명) : " + cbDetailInfo.customerName + vbCrLf
            tmp += "itemName (주문상품명) : " + cbDetailInfo.itemName + vbCrLf
            tmp += "orderNumber (주문번호) : " + cbDetailInfo.orderNumber + vbCrLf
            tmp += "email (주문자 이메일) : " + cbDetailInfo.email + vbCrLf
            tmp += "hp (주문자 휴대폰번호) : " + cbDetailInfo.hp + vbCrLf
            tmp += "smssendYN (알림문자 전송여부) : " + cbDetailInfo.smssendYN.ToString + vbCrLf
            tmp += "cancelType (취소사유) : " + cbDetailInfo.cancelType.ToString + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건에 해당하는 현금영수증을 조회합니다. (조회기간 단위 : 최대 6개월)
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/info#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim State(3) As String
        Dim tradeType(2) As String
        Dim tradeUsage(2) As String
        Dim taxationType(2) As String
        Dim tradeOpt(3) As String

        '일자유형, R-등록일자, T-거래일자 I-발행일자
        Dim DType As String = "T"

        '시작일자, 형식(yyyyMMdd)
        Dim SDate As String = "20221108"

        '종료일자, 형식(yyyyMMdd)
        Dim EDate As String = "20221108"

        '상태코드 배열 (2,3번째 자리에 와일드카드(*) 사용 가능)
        '- 미입력시 전체조회
        State(0) = "3**"

        '문서형태 배열 ("N" , "C" 중 선택, 다중 선택 가능)
        '- N = 일반 현금영수증 , C = 취소 현금영수증
        '- 미입력시 전체조회
        tradeType(0) = "N"
        tradeType(1) = "C"

        '거래구분 배열 ("P" , "C" 중 선택, 다중 선택 가능)
        '- P = 소득공제용 , C = 지출증빙용
        '- 미입력시 전체조회
        tradeUsage(0) = "P"
        tradeUsage(1) = "C"

        '거래유형 배열 ("N" , "B" , "T" 중 선택, 다중 선택 가능)
        '- N = 일반 , B = 도서공연 , T = 대중교통
        '- 미입력시 전체조회
        tradeOpt(0) = "N"
        tradeOpt(1) = "B"
        tradeOpt(2) = "T"

        '과세형태 배열 ("T" , "N" 중 선택, 다중 선택 가능)
        '- T = 과세 , N = 비과세
        '- 미입력시 전체조회
        taxationType(0) = "T"
        taxationType(1) = "N"

        '현금영수증 식별번호 조회, 미기재시 전체조회
        Dim QString As String = ""

        '페이지 번호, 기본값 1
        Dim Page As Integer = 1

        '페이지당 목록갯수, 기본값 500, 최댓값 100
        Dim PerPage As Integer = 30

        '정렬방향 D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

        '가맹점 종사업장 번호
        '└ 다수건 검색시 콤마(",")로 구분. 예) "1234,1000"
        '└ 미입력시 전제조회
        Dim FranchiseTaxRegID = ""

        Try
            Dim cbSearchList As CBSearchResult = cashbillService.Search(txtCorpNum.Text, DType, SDate, EDate, State, _
                                                                tradeType, tradeUsage, tradeOpt, taxationType, QString, Order, Page, PerPage, FranchiseTaxRegID)

            Dim tmp As String = ""

            tmp += "code (응답코드) : " + CStr(cbSearchList.code) + vbCrLf
            tmp += "message (응답메시지) : " + cbSearchList.message + vbCrLf + vbCrLf
            tmp += "total (총 검색결과 건수) : " + CStr(cbSearchList.total) + vbCrLf
            tmp += "perPage (페이지당 검색개수) : " + CStr(cbSearchList.perPage) + vbCrLf
            tmp += "pageNum (페이지 번호) : " + CStr(cbSearchList.pageNum) + vbCrLf
            tmp += "pageCount (페이지 개수) : " + CStr(cbSearchList.pageCount) + vbCrLf

            For Each cbInfo As CashbillInfo In cbSearchList.list
                tmp += "itemKey (팝빌번호) : " + cbInfo.itemKey + vbCrLf
                tmp += "mgtKey (문서번호) : " + cbInfo.mgtKey + vbCrLf
                tmp += "tradeDate (거래일자) : " + cbInfo.tradeDate + vbCrLf
                tmp += "tradeDT (거래일시) : " + cbInfo.tradeDT + vbCrLf
                tmp += "tradeType (문서형태) : " + cbInfo.tradeType + vbCrLf
                tmp += "tradeUsage (거래구분) : " + cbInfo.tradeUsage + vbCrLf
                tmp += "tradeOpt (거래유형) : " + cbInfo.tradeOpt + vbCrLf
                tmp += "taxationType (과세형태) : " + cbInfo.taxationType + vbCrLf
                tmp += "totalAmount (거래금액) : " + cbInfo.totalAmount + vbCrLf
                tmp += "issueDT (발행일시) : " + cbInfo.issueDT + vbCrLf
                tmp += "regDT (등록일시) : " + cbInfo.regDT + vbCrLf
                tmp += "stateMemo (상태메모) : " + cbInfo.stateMemo + vbCrLf
                tmp += "stateCode (상태코드) : " + cbInfo.stateCode.ToString + vbCrLf
                tmp += "stateDT (상태변경일시) : " + cbInfo.stateDT + vbCrLf
                tmp += "identityNum (식별번호) : " + cbInfo.identityNum + vbCrLf
                tmp += "itemName (주문상품명) : " + cbInfo.itemName + vbCrLf
                tmp += "customerName (주문자명) : " + cbInfo.customerName + vbCrLf
                tmp += "confirmNum (국세청승인번호) : " + cbInfo.confirmNum + vbCrLf
                tmp += "orgConfirmNum (원본 현금영수증 국세청승인번호) : " + cbInfo.orgConfirmNum + vbCrLf
                tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cbInfo.orgTradeDate + vbCrLf
                tmp += "ntssendDT (국세청 전송일시) : " + cbInfo.ntssendDT + vbCrLf
                tmp += "ntsresultDT (국세청 처리결과 수신일시) : " + cbInfo.ntsresultDT + vbCrLf
                tmp += "ntsresultCode (국세청 처리결과 상태코드) : " + cbInfo.ntsresultCode + vbCrLf
                tmp += "ntsresultMessage (국세청 처리결과 메시지) : " + cbInfo.ntsresultMessage + vbCrLf
                tmp += "printYN (인쇄여부) : " + cbInfo.printYN.ToString + vbCrLf
                tmp += "interOPYN (연동문서여부) : " + cbInfo.interOPYN.ToString + vbCrLf + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 현금영수증 임시문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TBOX.Click

        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX")

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 현금영수증 발행문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_PBOX.Click

        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX")

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 현금영수증 매출문서작성 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_WRITE.Click

        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE")

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 1건의 상세 정보 페이지의 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/view#GetPopUpURL
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        Try
            Dim url As String = cashbillService.GetPopUpURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 1건의 상세 정보 페이지(사이트 상단, 좌측 메뉴 및 버튼 제외)의 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/view#GetViewURL
    '=========================================================================
    Private Sub btnGetViewURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetViewURL.Click

        Try
            Dim url As String = cashbillService.GetViewURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 1건을 인쇄하기 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/view#GetPrintURL
    '=========================================================================
    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click
        Try
            Dim url As String = cashbillService.GetPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 다수건의 현금영수증을 인쇄하기 위한 페이지의 팝업 URL을 반환합니다. (최대 100건)
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/view#GetMassPrintURL
    '=========================================================================
    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMassPrintURL.Click
        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서 문서번호 배열, 최대 100건.
        MgtKeyList.Add("20220513-001")
        MgtKeyList.Add("20220513-002")

        Try
            Dim url As String = cashbillService.GetMassPrintURL(txtCorpNum.Text, MgtKeyList, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 구매자가 수신하는 현금영수증 안내 메일의 하단에 버튼 URL 주소를 반환합니다.
    ' - 함수 호출로 반환 받은 URL에는 유효시간이 없습니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/view#GetMailURL
    '=========================================================================
    Private Sub btnGetEmailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEmailURL.Click
        Try
            Dim url As String = cashbillService.GetMailURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 현금영수증 PDF 파일을 다운 받을 수 있는 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/view#GetPDFURL
    '=========================================================================
    Private Sub btnGetPDFURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPDFURL.Click

        Try
            Dim url As String = cashbillService.GetPDFURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/etc#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = cashbillService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 현금영수증과 관련된 안내 메일을 재전송 합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/etc#SendEmail
    '=========================================================================
    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click

        '수신이메일주소
        Dim receiverMail = ""

        Try
            Dim response As Response = cashbillService.SendEmail(txtCorpNum.Text, txtMgtKey.Text, receiverMail, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증과 관련된 안내 SMS(단문) 문자를 재전송하는 함수로, 팝빌 사이트 [문자·팩스] > [문자] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
    ' - 메시지는 최대 90byte까지 입력 가능하고, 초과한 내용은 자동으로 삭제되어 전송합니다. (한글 최대 45자)
    ' - 함수 호출 시 포인트가 과금됩니다. (전송실패시 환불처리)
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/etc#SendSMS
    '=========================================================================
    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click

        '발신번호
        Dim sendNum As String = ""

        '수신번호
        Dim receiveNum As String = ""

        '메시지내용, 90byte(한글45자) 초과된 내용은 삭제되어 전송됨
        Dim contents As String = "발신문자 메시지 내용"

        Try
            Dim response As Response = cashbillService.SendSMS(txtCorpNum.Text, txtMgtKey.Text, sendNum, receiveNum, contents, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증을 팩스로 전송하는 함수로, 팝빌 사이트 [문자·팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
    ' - 팩스 전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/etc#SendFAX
    '=========================================================================
    Private Sub btnSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFAX.Click

        '발신번호
        Dim sendNum As String = ""

        '수신팩스번호
        Dim receiveNum As String = ""

        Try
            Dim response As Response = cashbillService.SendFAX(txtCorpNum.Text, txtMgtKey.Text, sendNum, receiveNum, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트를 통해 발행하였지만 문서번호가 존재하지 않는 현금영수증에 문서번호를 할당합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/etc#AssignMgtKey
    '=========================================================================
    Private Sub btnAssignMgtKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssignMgtKey.Click

        '팝빌번호, 목록조회(Search) API의 반환항목중 ItemKey 참조
        Dim itemKey As String = ""

        '문서번호가 없는 문서에 할당할 문서번호
        '- 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        Dim mgtKey As String = "20220504-001"

        Try
            Dim response As Response = cashbillService.AssignMgtKey(txtCorpNum.Text, itemKey, mgtKey)
            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 관련 메일 항목에 대한 발송설정을 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/etc#ListEmailConfig
    '=========================================================================
    Private Sub btnListEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListEmailConfig.Click
        Try
            Dim emailConfigList As List(Of EmailConfig) = cashbillService.ListEmailConfig(txtCorpNum.Text)

            Dim tmp As String = "메일전송유형 | 전송여부 " + vbCrLf

            For Each info As EmailConfig In emailConfigList
                If info.emailType = "CSH_ISSUE" Then tmp += "CSH_ISSUE (고객에게 현금영수증이 발행 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "CSH_CANCEL" Then tmp += "CSH_CANCEL (고객에게 현금영수증이 발행취소 되었음을 알려주는 메일) |" + info.sendYN.ToString + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 관련 메일 항목에 대한 발송설정을 수정합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/etc#UpdateEmailConfig
    '
    ' 메일전송유형
    ' CSH_ISSUE : 고객에게 현금영수증이 발행 되었음을 알려주는 메일 입니다.
    ' CSH_CANCEL : 고객에게 현금영수증 발행취소 되었음을 알려주는 메일 입니다.
    '=========================================================================
    Private Sub btnUpdateEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateEmailConfig.Click
        Try
            '메일전송유형
            Dim emailType As String = "CSH_ISSUE"

            '전송여부 (True-전송, False-미전송)
            Dim sendYN As Boolean = True

            Dim response As Response = cashbillService.UpdateEmailConfig(txtCorpNum.Text, emailType, sendYN)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = cashbillService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = cashbillService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPaymentURL.Click
        Try
            Dim url As String = cashbillService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = cashbillService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerPoint.Click
        Try
            Dim remainPoint As Double = cashbillService.GetPartnerBalance(txtCorpNum.Text)


            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = cashbillService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 발행시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetUnitCost
    '=========================================================================
    Private Sub btnGetUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost.Click
        Try
            Dim unitCost As Single = cashbillService.GetUnitCost(txtCorpNum.Text)

            MsgBox("현금영수증 발행단가(unitCost) : " + unitCost.ToString)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 현금영수증 API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click
        Try
            Dim ChargeInfo As ChargeInfo = cashbillService.GetChargeInfo(txtCorpNum.Text)

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
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/member#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = cashbillService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/member#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = cashbillService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/member#JoinMember
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
            Dim response As Response = cashbillService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/member#GetCorpInfo
    '=========================================================================
    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCorpInfo.Click
        Try
            Dim corpInfo As CorpInfo = cashbillService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/member#UpdateCorpInfo
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

            Dim response As Response = cashbillService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/member#RegistContact
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
            Dim response As Response = cashbillService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/member#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = cashbillService.GetContactInfo(txtCorpNum.Text, contactID)

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
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/member#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = cashbillService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자명) | email(메일주소) | tel(연락처) |"
            tmp += "regDT(등록일시) | searchRole(담당자 권한) | mgrYN(관리자 여부) | state(상태)" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.email + " | " + info.tel + " | " + info.regDT.ToString + " | "
                tmp += info.searchRole.ToString + " | " + info.mgrYN.ToString + " | " + info.state + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/member#UpdateContact
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
            Dim response As Response = cashbillService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 1건의 현금영수증 인쇄 팝업 URL을 반환합니다. (공급받는자용)
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    '=========================================================================
    Private Sub btnEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEPrintURL.Click

        Try
            Dim url As String = cashbillService.GetEPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#PaymentRequest
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
            Dim response As PaymentResponse = cashbillService.PaymentRequest(txtCorpNum.Text, paymentForm, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message+vbCrLf + "정산코드(settleCode) : " + response.settleCode)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetSettleResult
    '=========================================================================
    Private Sub btnGetSettleResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSettleResult.Click

        '정산코드
        Dim SettleCode As String = "202301160000000010"

        Try
            Dim response As PaymentHistory = cashbillService.GetSettleResult (txtCorpNum.Text, SettleCode, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetPaymentHistory
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
            Dim result As PaymentHistoryResult = cashbillService.GetPaymentHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetUseHistory
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
            Dim result As UseHistoryResult = cashbillService.GetUseHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, Order, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#Refund
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
            Dim response As RefundResponse = cashbillService.Refund(txtCorpNum.Text,refundForm, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf +
                        "응답메시지(message) : " + response.Message + vbCrLf +
                   "환불코드(refundCode) : " +response.refundCode )

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 환불신청내역을 확인합니다.
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetRefundHistory
    '=========================================================================
    Private Sub btnGetRefundHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundHistory.Click

        '목폭 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500


        Try
            Dim result As RefundHistoryResult  = cashbillService.GetRefundHistory(txtCorpNum.Text,Page, PerPage, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetRefundInfo
    '=========================================================================
    Private Sub btnGetRefundInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '환불코드
        Dim refundCode As String = "023040000017"

        Try
            Dim history As RefundHistory  = cashbillService.GetRefundInfo(txtCorpNum.Text,refundCode, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/point#GetRefundableBalance
    '=========================================================================
    Private Sub btnGetRefundableBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        Try
            Dim refundableCode As Double  = cashbillService.GetRefundableBalance(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/cashbill/dotnet/api/member#QuitMember
    '=========================================================================
    Private Sub btnQuitMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '탈퇴사유
        Dim quitReason As String = "회원 탈퇴 사유"

        Try
            Dim response As Response  = cashbillService.QuitMember(txtCorpNum.Text, quitReason, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.Message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub
End Class
