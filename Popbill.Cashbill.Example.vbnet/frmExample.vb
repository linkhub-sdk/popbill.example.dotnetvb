'=========================================================================
'
' 팝빌 현금영수증 API VB.Net  SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내 : https://docs.popbill.com/cashbill/tutorial/dotnet#vb
' - 업데이트 일자 : 2021-08-05
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
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

        '연동환경 설정값 (True-개발용, False-상업용)
        cashbillService.IsTest = True

        '인증토큰의 IP제한기능 사용여부, (True-권장)
        cashbillService.IPRestrictOnOff = True

        '로컬PC 시간 사용 여부 True(사용), False(기본값) - 미사용
        cashbillService.UseLocalTimeYN = False

    End Sub

    '=========================================================================
    ' 파트너가 현금영수증 관리 목적으로 할당하는 문서번호 사용여부를 확인합니다.
    ' - 이미 사용 중인 문서번호는 중복 사용이 불가하고, 현금영수증이 삭제된 경우에만 문서번호의 재사용이 가능합니다.
    ' - 문서번호는 최대 24자리 영문 대소문자, 숫자, 특수문자('-','_')만 이용 가능
    ' - https://docs.popbill.com/cashbill/dotnet/api#CheckMgtKeyInUse
    '=========================================================================
    Private Sub btnCheckMgtKeyInUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckMgtKeyInUse.Click

        Try
            Dim InUse As Boolean = cashbillService.CheckMgtKeyInUse(txtCorpNum.Text, txtMgtKey.Text)

            MsgBox(IIf(InUse, "사용중", "미사용중"))

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 데이터를 팝빌에 전송하여 발행합니다.
    ' - 현금영수증 국세청 전송 정책 : https://docs.popbill.com/cashbill/ntsSendPolicy?lang=dotnet
    ' - https://docs.popbill.com/cashbill/dotnet/api#RegistIssue
    '=========================================================================
    Private Sub btnRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistIssue.Click
        Dim cashbill As Cashbill = New Cashbill

        '현금영수증 문서번호, 최대 24자리 영문 대소문자, 숫자, 특수문자('-','_')만 이용 가능
        cashbill.mgtKey = txtMgtKey.Text

        '[취소거래시 필수] 원본 현금영수증 국세청승인번호
        '문서정보(GetInfo API)의 응답항목중 국세청승인번호(confirmNum)를 확인하여 기재
        cashbill.orgConfirmNum = ""

        '[취소거래시 필수] 원본 현금영수증 거래일자
        '문서정보(GetInfo API)의 응답항목중 거래일자(tradeDate)를 확인하여 기재
        cashbill.orgTradeDate = ""

        '문서형태, [승인거래, 취소거래] 중 기재
        cashbill.tradeType = "승인거래"

        '거래구분, [소득공제용, 지출증빙용] 중 기재
        cashbill.tradeUsage = "소득공제용"

        '거래유형, [일반, 도서공연, 대중교통] 중 기재
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

        '가맹점 상호명
        cashbill.franchiseCorpName = "발행자 상호"

        '가맹점 대표자 성명
        cashbill.franchiseCEOName = "발행자 대표자"

        '가맹점 주소
        cashbill.franchiseAddr = "발행자 주소d"

        '가맹점 전화번호
        cashbill.franchiseTEL = "070-1234-1234"

        '식별번호, 거래유형에 따라 작성
        '소득공제용 - 주민등록/휴대폰/카드번호 기재가능
        '지출증빙용 - 사업자번호/주민등록/휴대폰/카드번호 기재가능
        cashbill.identityNum = "0101112222"

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

        '주문자 휴대폰번호
        cashbill.hp = "010-111-222"

        '현금영수증 발행 알림문자 전송여부, 미기재시 "false"
        cashbill.smssendYN = False

        '메모
        Dim memo As String = "즉시발행 메모"

        '안내메일 제목, 공백처리시 기본양식으로 전송
        Dim emailSubject As String = ""

        Try
            Dim response As Response = cashbillService.RegistIssue(txtCorpNum.Text, cashbill, memo, txtUserId.Text, emailSubject)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 현금영수증을 [임시저장]합니다.
    ' - [임시저장] 상태의 현금영수증은 발행(Issue API)을 호출해야만 국세청에 전송됩니다.
    ' - 현금영수증 국세청 전송 정책 : https://docs.popbill.com/cashbill/ntsSendPolicy?lang=dotnet
    ' - https://docs.popbill.com/cashbill/dotnet/api#Register
    '=========================================================================
    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cashbill As Cashbill = New Cashbill

        '현금영수증 문서번호, 최대 24자리 영문 대소문자, 숫자, 특수문자('-','_')만 이용 가능
        cashbill.mgtKey = txtMgtKey.Text

        '[취소거래시 필수] 원본 현금영수증 국세청승인번호
        '문서정보(GetInfo API)의 응답항목중 국세청승인번호(confirmNum)를 확인하여 기재
        cashbill.orgConfirmNum = ""

        '[취소거래시 필수] 원본 현금영수증 거래일자
        '문서정보(GetInfo API)의 응답항목중 거래일자(tradeDate)를 확인하여 기재
        cashbill.orgTradeDate = ""

        '문서형태, [승인거래, 취소거래] 중 기재
        cashbill.tradeType = "승인거래"

        '거래구분, [소득공제용, 지출증빙용] 중 기재
        cashbill.tradeUsage = "소득공제용"

        '거래유형, [일반, 도서공연, 대중교통] 중 기재
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

        '가맹점 상호명
        cashbill.franchiseCorpName = "발행자 상호"

        '가맹점 대표자 성명
        cashbill.franchiseCEOName = "발행자 대표자"

        '가맹점 주소
        cashbill.franchiseAddr = "발행자 주소d"

        '가맹점 전화번호
        cashbill.franchiseTEL = "070-1234-1234"

        '식별번호, 거래유형에 따라 작성
        '소득공제용 - 주민등록/휴대폰/카드번호 기재가능
        '지출증빙용 - 사업자번호/주민등록/휴대폰/카드번호 기재가능
        cashbill.identityNum = "0101112222"

        '주문자명
        cashbill.customerName = "주문자명"

        '주문상품명
        cashbill.itemName = "주문상품명"

        '주문번호
        cashbill.orderNumber = "주문번호"

        '주문자 이메일
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        cashbill.email = "test@test.com"

        '주문자 휴대폰번호
        cashbill.hp = "010-111-222"

        '현금영수증 발행 알림문자 전송여부, 미기재시 "false"
        cashbill.smssendYN = False

        Try
            Dim response As Response = cashbillService.Register(txtCorpNum.Text, cashbill, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 1건의 현금영수증을 [수정]합니다.
    ' - [임시저장] 상태의 현금영수증만 수정할 수 있습니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#Update
    '=========================================================================
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim cashbill As Cashbill = New Cashbill

        '현금영수증 문서번호, 최대 24자리 영문 대소문자, 숫자, 특수문자('-','_')만 이용 가능
        cashbill.mgtKey = txtMgtKey.Text

        '[취소거래시 필수] 원본 현금영수증 국세청승인번호
        '문서정보(GetInfo API)의 응답항목중 국세청승인번호(confirmNum)를 확인하여 기재
        cashbill.orgConfirmNum = ""

        '[취소거래시 필수] 원본 현금영수증 거래일자
        '문서정보(GetInfo API)의 응답항목중 거래일자(tradeDate)를 확인하여 기재
        cashbill.orgTradeDate = ""

        '문서형태, [승인거래, 취소거래] 중 기재
        cashbill.tradeType = "승인거래"

        '거래구분, [소득공제용, 지출증빙용] 중 기재
        cashbill.tradeUsage = "소득공제용"

        '거래유형, [일반, 도서공연, 대중교통] 중 기재
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

        '가맹점 상호명
        cashbill.franchiseCorpName = "발행자 상호"

        '가맹점 대표자 성명
        cashbill.franchiseCEOName = "발행자 대표자"

        '가맹점 주소
        cashbill.franchiseAddr = "발행자 주소d"

        '가맹점 전화번호
        cashbill.franchiseTEL = "070-1234-1234"

        '식별번호, 거래유형에 따라 작성
        '소득공제용 - 주민등록/휴대폰/카드번호 기재가능
        '지출증빙용 - 사업자번호/주민등록/휴대폰/카드번호 기재가능
        cashbill.identityNum = "0101112222"

        '주문자명
        cashbill.customerName = "주문자명"

        '주문상품명
        cashbill.itemName = "주문상품명"

        '주문번호
        cashbill.orderNumber = "주문번호"

        '주문자 이메일
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        cashbill.email = "test@test.com"

        '주문자 휴대폰번호
        cashbill.hp = "010-111-222"

        '현금영수증 발행 알림문자 전송여부, 미기재시 "false"
        cashbill.smssendYN = False

        Try
            Dim response As Response = cashbillService.Update(txtCorpNum.Text, txtMgtKey.Text, cashbill, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 [임시저장] 현금영수증을 [발행]합니다.
    ' - 현금영수증 국세청 전송 정책 : https://docs.popbill.com/cashbill/ntsSendPolicy?lang=dotnet
    ' - https://docs.popbill.com/cashbill/dotnet/api#CBIssue
    '=========================================================================
    Private Sub btnIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '발행 메모
        Dim Memo As String = "발행 메모"

        Try

            Dim response As Response = cashbillService.Issue(txtCorpNum.Text, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 국세청 전송 이전 "발행완료" 상태의 현금영수증을 "발행취소"하고 국세청 전송 대상에서 제외됩니다.
    ' - 발행취소는 국세청 전송전에만 가능합니다.
    ' - 발행취소된 현금영수증은 국세청에 전송되지 않습니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#CancelIssue
    '=========================================================================
    Private Sub btnCancelIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '발행취소 메모
        Dim Memo As String = "발행취소 메모"

        Try
            Dim response As Response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 국세청 전송 이전 "발행완료" 상태의 현금영수증을 "발행취소"하고 국세청 전송 대상에서 제외됩니다.
    ' - 발행취소는 국세청 전송전에만 가능합니다.
    ' - 발행취소된 현금영수증은 국세청에 전송되지 않습니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#CancelIssue
    '=========================================================================
    Private Sub btnCancelIssue02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssue02.Click

        '발행취소 메모
        Dim memo As String = "발행취소 메모"

        Try
            Dim response As Response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 국세청 전송 이전 "발행완료" 상태의 현금영수증을 "발행취소"하고 국세청 전송 대상에서 제외됩니다.
    ' - 발행취소는 국세청 전송전에만 가능합니다.
    ' - 발행취소된 현금영수증은 국세청에 전송되지 않습니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#CancelIssue
    '=========================================================================
    Private Sub btnCancelIssueSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssueSub.Click

        '발행취소 메모
        Dim Memo As String = "발행취소 메모"

        Try
            Dim response As Response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 현금영수증을 삭제합니다.
    ' - 삭제 가능한 상태: "임시저장", "발행취소", "전송실패"
    ' - 현금영수증을 삭제하면 사용된 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#Delete
    '=========================================================================
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            Dim response As Response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 현금영수증을 삭제합니다.
    ' - 삭제 가능한 상태: "임시저장", "발행취소", "전송실패"
    ' - 현금영수증을 삭제하면 사용된 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#Delete
    '=========================================================================
    Private Sub btnDeleteSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSub.Click

        Try
            Dim response As Response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 현금영수증을 삭제합니다.
    ' - 삭제 가능한 상태: "임시저장", "발행취소", "전송실패"
    ' - 현금영수증을 삭제하면 사용된 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#Delete
    '=========================================================================
    Private Sub btnDelete02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete02.Click
        Try
            Dim response As Response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 취소 현금영수증을 발행하며 취소 현금영수증의 금액은 원본 금액을 넘을 수 없습니다.
    ' - 현금영수증 국세청 전송 정책 : https://docs.popbill.com/cashbill/ntsSendPolicy?lang=dotnet
    ' - https://docs.popbill.com/cashbill/dotnet/api#RevokeRegistIssue
    '=========================================================================
    Private Sub btnRevokRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevokRegistIssue.Click

        '원본현금영수증 국세청 승인번호
        Dim orgConfirmNum As String = "820116333"

        '원본현금영수증 거래일자
        Dim orgTradeDate As String = "20210701"

        '발행 안내문자 전송여부
        Dim smssendYN As Boolean = False

        '취소현금영수증 메모
        Dim memo As String = "취소현금영수증 발행 메모"

        Try
            Dim response As Response = cashbillService.RevokeRegistIssue(txtCorpNum.Text, txtMgtKey.Text, orgConfirmNum, orgTradeDate, smssendYN, memo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' (부분)취소 현금영수증을 발행하며 취소 현금영수증의 금액은 원본 금액을 넘을 수 없습니다.
    ' - 현금영수증 국세청 전송 정책 : https://docs.popbill.com/cashbill/ntsSendPolicy?lang=dotnet
    ' - https://docs.popbill.com/cashbill/dotnet/api#RevokeRegistIssue
    '=========================================================================
    Private Sub btnRevokeRegistIssue_part_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevokeRegistIssue_part.Click

        '원본현금영수증 국세청 승인번호
        Dim orgConfirmNum As String = "820116333"

        '원본현금영수증 거래일자
        Dim orgTradeDate As String = "20210701"

        '발행안내문자 전송여부
        Dim smssendYN As Boolean = False

        '취소현금영수증 메모
        Dim memo As String = "취소현금영수증 발행 메모"

        '부분취소 여부
        Dim isPartCancel As Boolean = True

        '취소사유, 1-거래취소, 2-오류발급취소, 3- 기타
        Dim cancelType As Integer = 1

        '[취소] 공급가액
        Dim supplyCost As String = "2000"

        '[취소] 세액
        Dim tax As String = "200"

        '[취소] 봉사료
        Dim serviceFee As String = "0"

        '[취소] 합계금액
        Dim totalAmount As String = "2200"

        Try
            Dim response As Response = cashbillService.RevokeRegistIssue(txtCorpNum.Text, txtMgtKey.Text, orgConfirmNum, orgTradeDate, _
                                                                         smssendYN, memo, txtUserId.Text, isPartCancel, cancelType, supplyCost, _
                                                                         tax, serviceFee, totalAmount)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 현금영수증 1건의 상태 및 요약정보를 확인합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetInfo
    '=========================================================================
    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click
        Try
            Dim cbInfo As CashbillInfo = cashbillService.GetInfo(txtCorpNum.Text, txtMgtKey.Text)

            Dim tmp As String = ""
            tmp += "itemKey (팝빌번호) : " + cbInfo.itemKey + vbCrLf
            tmp += "mgtKey (문서번호) : " + cbInfo.mgtKey + vbCrLf
            tmp += "tradeDate (거래일자) : " + cbInfo.tradeDate + vbCrLf
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

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 다수건의 현금영수증 상태 및 요약 정보를 확인합니다. (1회 호출 시 최대 1,000건 확인 가능)
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetInfos
    '=========================================================================
    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서번호 배열, 최대 1000건.
        MgtKeyList.Add("20210701-001")
        MgtKeyList.Add("20210701-002")

        Try
            Dim cashbillInfoList As List(Of CashbillInfo) = cashbillService.GetInfos(txtCorpNum.Text, MgtKeyList)

            Dim tmp As String = ""

            For Each cbInfo As CashbillInfo In cashbillInfoList
                tmp += "itemKey (팝빌번호) : " + cbInfo.itemKey + vbCrLf
                tmp += "mgtKey (문서번호) : " + cbInfo.mgtKey + vbCrLf
                tmp += "tradeDate (거래일자) : " + cbInfo.tradeDate + vbCrLf
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
                tmp += "printYN (인쇄여부) : " + cbInfo.printYN.ToString + vbCrLf + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 1건의 상세정보를 확인합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetDetailInfo
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
            tmp += "tradeType (문서형태) : " + cbDetailInfo.tradeType + vbCrLf
            tmp += "tradeUsage (거래구분) : " + cbDetailInfo.tradeUsage + vbCrLf
            tmp += "tradeOpt (거래유형) : " + cbDetailInfo.tradeOpt + vbCrLf
            tmp += "taxationType (과세형태) : " + cbDetailInfo.taxationType + vbCrLf
            tmp += "totalAmount (거래금액) : " + cbDetailInfo.totalAmount + vbCrLf
            tmp += "supplyCost (공급가액) : " + cbDetailInfo.supplyCost + vbCrLf
            tmp += "tax (부가세) : " + cbDetailInfo.tax + vbCrLf
            tmp += "serviceFee (봉사료) : " + cbDetailInfo.serviceFee + vbCrLf
            tmp += "franchiseCorpNum (가맹점 사업자번호) : " + cbDetailInfo.franchiseCorpNum + vbCrLf
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건에 해당하는 현금영수증을 조회합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim State(3) As String
        Dim tradeType(2) As String
        Dim tradeUsage(2) As String
        Dim taxationType(2) As String
        Dim tradeOpt(3) As String

        '[필수] 일자유형, R-등록일자, T-거래일자 I-발행일자
        Dim DType As String = "T"

        '[필수] 시작일자, 형식(yyyyMMdd)
        Dim SDate As String = "20210701"

        '[필수] 종료일자, 형식(yyyyMMdd)
        Dim EDate As String = "20210730"

        '전송상태코드 배열, 미기재시 전체조회, 2,3번째 자리 와일드카드(*) 가능
        '[참조] 현금영수증 API 연동매뉴열 "5.1. 현금영수증 상태코드"
        State(0) = "2**"
        State(1) = "3**"
        State(2) = "4**"

        '문서형태 배열, N-일반 현금영수증, C-취소 현금영수증
        tradeType(0) = "N"
        tradeType(1) = "C"

        '거래구분 배열, P-소득공제, C-지출증빙
        tradeUsage(0) = "P"
        tradeUsage(1) = "C"

        '거래유형, N-일반, B-도서공연, T-대중교통
        tradeOpt(0) = "N"
        tradeOpt(1) = "B"
        tradeOpt(2) = "T"

        '과세형태 배열, T-과세, N-비과세
        taxationType(0) = "T"
        taxationType(1) = "N"

        '현금영수증 식별번호 조회, 미기재시 전체조회
        Dim QString As String = ""

        '페이지 번호, 기본값 1
        Dim Page As Integer = 1

        '페이지당 목록갯수, 기본값 500
        Dim PerPage As Integer = 30

        '정렬방향 D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

        Try
            Dim cbSearchList As CBSearchResult = cashbillService.Search(txtCorpNum.Text, DType, SDate, EDate, State, _
                                                                tradeType, tradeUsage, tradeOpt, taxationType, QString, Order, Page, PerPage)

            Dim tmp As String

            tmp = "code (응답코드) : " + CStr(cbSearchList.code) + vbCrLf
            tmp = tmp + "message (응답메시지) : " + cbSearchList.message + vbCrLf + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + CStr(cbSearchList.total) + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + CStr(cbSearchList.perPage) + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + CStr(cbSearchList.pageNum) + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + CStr(cbSearchList.pageCount) + vbCrLf

            For Each cbInfo As CashbillInfo In cbSearchList.list
                tmp += "itemKey (팝빌번호) : " + cbInfo.itemKey + vbCrLf
                tmp += "mgtKey (문서번호) : " + cbInfo.mgtKey + vbCrLf
                tmp += "tradeDate (거래일자) : " + cbInfo.tradeDate + vbCrLf
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
                tmp += "printYN (인쇄여부) : " + cbInfo.printYN.ToString + vbCrLf + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증의 상태에 대한 변경이력을 확인합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetLogs
    '=========================================================================
    Private Sub btnGetLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetLogs.Click
        Try
            Dim logList As List(Of CashbillLog) = cashbillService.GetLogs(txtCorpNum.Text, txtMgtKey.Text)

            Dim tmp As String = ""

            For Each log As CashbillLog In logList
                tmp += log.docLogType.ToString + " | " + log.log + " | " + log.procType + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 현금영수증 임시문서함 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetURL
    '=========================================================================
    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TBOX.Click

        'TBOX-임시문서함 / PBOX-발행문서함 / WRITE-현금영수증 신규 작성
        Dim TOGO As String = "TBOX"

        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 팝빌 현금영수증 발행문서함 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetURL
    '=========================================================================
    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_PBOX.Click

        'TBOX-임시문서함 / PBOX-발행문서함 / WRITE-현금영수증 신규 작성
        Dim TOGO As String = "PBOX"

        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 현금영수증 매출문서작성 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetURL
    '=========================================================================
    Private Sub btnGetURL_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_WRITE.Click

        'TBOX-임시문서함 / PBOX-발행문서함 / WRITE-현금영수증 신규 작성
        Dim TOGO As String = "WRITE"

        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트와 동일한 현금영수증 1건의 상세 정보 페이지의 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetPopUpURL
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        Try
            Dim url As String = cashbillService.GetPopUpURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 1건을 인쇄하기 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetPrintURL
    '=========================================================================
    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click
        Try
            Dim url As String = cashbillService.GetPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 다수건의 현금영수증을 인쇄하기 위한 페이지의 팝업 URL을 반환합니다. (최대 100건)
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetMassPrintURL
    '=========================================================================
    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMassPrintURL.Click
        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서 문서번호 배열, 최대 100건.
        MgtKeyList.Add("20210701-001")
        MgtKeyList.Add("20210701-002")

        Try
            Dim url As String = cashbillService.GetMassPrintURL(txtCorpNum.Text, MgtKeyList, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 구매자가 수신하는 현금영수증 안내 메일의 하단에 버튼 URL 주소를 반환합니다.
    ' - 함수 호출로 반환 받은 URL에는 유효시간이 없습니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetMailURL
    '=========================================================================
    Private Sub btnGetEmailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEmailURL.Click
        Try
            Dim url As String = cashbillService.GetMailURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = cashbillService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 현금영수증과 관련된 안내 메일을 재전송 합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#SendEmail
    '=========================================================================
    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
        
        '수자 이메일주소
        Dim receiverMail = "test@test.com"

        Try
            Dim response As Response = cashbillService.SendEmail(txtCorpNum.Text, txtMgtKey.Text, receiverMail, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증과 관련된 안내 SMS(단문) 문자를 재전송하는 함수로, 팝빌 사이트 [문자·팩스] > [문자] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
    ' - 알림문자 전송시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - https://docs.popbill.com/cashbill/dotnet/api#SendSMS
    '=========================================================================
    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click

        '발신번호
        Dim sendNum As String = "070-1234-1234"

        '수신번호
        Dim receiveNum As String = "010-1111-2222"

        '메시지내용, 90byte(한글45자) 초과된 내용은 삭제되어 전송됨
        Dim contents As String = "발신문자 메시지 내용"

        Try
            Dim response As Response = cashbillService.SendSMS(txtCorpNum.Text, txtMgtKey.Text, sendNum, receiveNum, contents, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증을 팩스로 전송하는 함수로, 팝빌 사이트 [문자·팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
    ' - 팩스 전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - https://docs.popbill.com/cashbill/dotnet/api#SendFAX
    '=========================================================================
    Private Sub btnSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFAX.Click

        '발신번호
        Dim sendNum As String = "070-111-2222"

        '수신팩스번호
        Dim receiveNum As String = "070-1111-2222"

        Try
            Dim response As Response = cashbillService.SendFAX(txtCorpNum.Text, txtMgtKey.Text, sendNum, receiveNum, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 관련 메일 항목에 대한 발송설정을 확인합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#ListEmailConfig
    '=========================================================================
    Private Sub btnListEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListEmailConfig.Click
        Try
            Dim emailConfigList As List(Of EmailConfig) = cashbillService.ListEmailConfig(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "메일전송유형 | 전송여부 " + vbCrLf

            For Each info As EmailConfig In emailConfigList
                If info.emailType = "CSH_ISSUE" Then tmp += "CSH_ISSUE (고객에게 현금영수증이 발행 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "CSH_CANCEL" Then tmp += "CSH_CANCEL (고객에게 현금영수증이 발행취소 되었음을 알려주는 메일) |" + info.sendYN.ToString + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 관련 메일 항목에 대한 발송설정을 수정합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#UpdateEmailConfig
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

            Dim response As Response = cashbillService.UpdateEmailConfig(txtCorpNum.Text, emailType, sendYN, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)를 통해 확인하시기 바랍니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = cashbillService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = cashbillService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPaymentURL.Click
        Try
            Dim url As String = cashbillService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = cashbillService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerPoint.Click
        Try
            Dim remainPoint As Double = cashbillService.GetPartnerBalance(txtCorpNum.Text)


            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = cashbillService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 발행시 과금되는 포인트 단가를 확인합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetUnitCost
    '=========================================================================
    Private Sub btnGetUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost.Click
        Try
            Dim unitCost As Single = cashbillService.GetUnitCost(txtCorpNum.Text)

            MsgBox("현금영수증 발행단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 현금영수증 API 서비스 과금정보를 확인합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click
        Try
            Dim ChargeInfo As ChargeInfo = cashbillService.GetChargeInfo(txtCorpNum.Text)

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
    ' - https://docs.popbill.com/cashbill/dotnet/api#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = cashbillService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = cashbillService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#JoinMember
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
            Dim response As Response = cashbillService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#RegistContact
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

        '회사조회 권한여부, True-회사조회, False-개인조회
        joinData.searchAllAllowYN = False

        '관리자 여부, True-관리자, False-사용자
        joinData.mgrYN = False

        Try
            Dim response As Response = cashbillService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = cashbillService.ListContact(txtCorpNum.Text, txtUserId.Text)

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
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#UpdateContact
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
            Dim response As Response = cashbillService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetCorpInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#UpdateCorpInfo
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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 PDF 파일을 다운 받을 수 있는 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#GetPDFURL
    '=========================================================================
    Private Sub btnGetPDFURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPDFURL.Click

        Try
            Dim url As String = cashbillService.GetPDFURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트를 통해 발행하였지만 문서번호가 존재하지 않는 현금영수증에 문서번호를 할당합니다.
    ' - https://docs.popbill.com/cashbill/dotnet/api#AssignMgtKey
    '=========================================================================
    Private Sub btnAssignMgtKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssignMgtKey.Click

        '팝빌번호, 목록조회(Search) API의 반환항목중 ItemKey 참조
        Dim itemKey As String = "020080617004800001"

        '문서번호가 없는 문서에 할당할 문서번호
        '- 최대 24자리 영문 대소문자, 숫자, 특수문자('-','_')만 이용 가능
        Dim mgtKey As String = "20210701-001"

        Try
            Dim response As Response = cashbillService.AssignMgtKey(txtCorpNum.Text, itemKey, mgtKey, txtUserId.Text)
            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub
End Class
