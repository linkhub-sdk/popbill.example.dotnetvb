'=========================================================================
'
' 팝빌 전자명세서 API VB.NET SDK Example
'
' - VB.NET SDK 연동환경 설정방법 안내 : https://docs.popbill.com/statement/tutorial/dotnet#vb
' - 업데이트 일자 : 2021-08-05
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
'
' <테스트 연동개발 준비사항>
' 1) 22, 25번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
'=========================================================================

Imports Popbill
Imports Popbill.Statement
Imports System.ComponentModel

Public Class frmExample
    '링크아이디
    Private LinkID As String = "TESTER"

    '비밀키
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '전자명세서 서비스 변수 선언
    Private statementService As StatementService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '전자명세서 서비스 객체 초기화
        statementService = New StatementService(LinkID, SecretKey)

        '연동환경 설정값 (True-개발용, False-상업용)
        statementService.IsTest = True

        '인증토큰의 IP제한기능 사용여부, (True-권장)
        statementService.IPRestrictOnOff = True

        '로컬PC 시간 사용 여부 True(사용), False(기본값) - 미사용
        statementService.UseLocalTimeYN = False

    End Sub

    ' 명세서 종류코드 반환
    Private Function selectedItemCode()

        Dim itemCode As Integer = 121

        If cboItemCode.Text = "거래명새서" Then
            itemCode = 121
        ElseIf cboItemCode.Text = "청구서" Then
            itemCode = 122
        ElseIf cboItemCode.Text = "견적서" Then
            itemCode = 123
        ElseIf cboItemCode.Text = "발주서" Then
            itemCode = 124
        ElseIf cboItemCode.Text = "입금표" Then
            itemCode = 125
        ElseIf cboItemCode.Text = "영수증" Then
            itemCode = 126
        End If

        Return itemCode
    End Function

    '=========================================================================
    ' 파트너가 전자명세서 관리 목적으로 할당하는 문서번호의 사용여부를 확인합니다.
    ' - 최대 24자, 영문 대소문자, 숫자, 특수문자('-','_')만 이용 가능
    ' - https://docs.popbill.com/statement/dotnet/api#CheckMgtKeyInUse
    '=========================================================================
    Private Sub btnCheckMgtKeyInUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckMgtKeyInUse.Click

        Try
            Dim InUse As Boolean = statementService.CheckMgtKeyInuse(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            MsgBox(IIf(InUse, "사용중", "미사용중"))

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 작성된 전자명세서 데이터를 팝빌에 저장과 동시에 발행하여, "발행완료" 상태로 처리합니다.
    ' - 팝빌 사이트 [전자명세서] > [환경설정] > [전자명세서 관리] 메뉴의 발행시 자동승인 옵션 설정을 통해 전자명세서를 "발행완료" 상태가 아닌 "승인대기" 상태로 발행 처리 할 수 있습니다.
    ' - https://docs.popbill.com/statement/dotnet/api#RegistIssue
    '=========================================================================
    Private Sub btnRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistIssue.Click
        Dim statement As New Statement

        '[필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20210701"

        '[필수] {영수, 청구} 중 기재
        statement.purposeType = "영수"

        '[필수] 과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text

        '[필수] 전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '[필수] 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text


        '=========================================================================
        '                               발신자 정보
        '=========================================================================

        '발신자 사업자번호, '-' 제외 10자리
        statement.senderCorpNum = txtCorpNum.Text

        '발신자 종사업장 식별번호, 필요시 기재, 형식은 숫자 4자리
        statement.senderTaxRegID = ""

        '발신자 상호
        statement.senderCorpName = "발신자 상호"

        '발신자 대표자 성명
        statement.senderCEOName = "발신자 대표자 성명"

        '발신자 주소
        statement.senderAddr = "발신자 주소"

        '발신자 종목
        statement.senderBizClass = "발신자 종목"

        '발신자 업태
        statement.senderBizType = "발신자 업태,업태2"

        '발신자 담당자성명
        statement.senderContactName = "발신자 담당자명"

        '발신자 이메일
        statement.senderEmail = "test@test.com"

        '발신자 연락처
        statement.senderTEL = "070-7070-0707"

        '발신자 휴대전화 번호
        statement.senderHP = "010-000-2222"


        '=========================================================================
        '                        수신자 정보
        '=========================================================================

        '수신자 사업자번호, '-' 제외 10자리
        statement.receiverCorpNum = "8888888888"

        '수신자 상호
        statement.receiverCorpName = "수신자 상호"

        '수신자 대표자 성명
        statement.receiverCEOName = "수신자 대표자 성명"

        '수신자 주소
        statement.receiverAddr = "수신자 주소"

        '수신자 종목
        statement.receiverBizClass = "수신자 종목 "

        '수신자 업태
        statement.receiverBizType = "수신자 업태"

        '수신자 담당자명
        statement.receiverContactName = "수신자 담당자명"

        '수신자 담당자 휴대폰번호
        statement.receiverHP = "010-1111-2222"

        '수신자 담당자 연락처
        statement.receiverTEL = "070-1234-1234"

        '수신자 메일주소
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        statement.receiverEmail = "test@test.com"

        '=========================================================================
        '                     전자명세서 기재사항
        '=========================================================================

        '[필수] 공급가액 합계
        statement.supplyCostTotal = "100000"

        '[필수] 세액 합계
        statement.taxTotal = "10000"

        '[필수] 합계금액, 공급가액 합계 + 세액 합계
        statement.totalAmount = "110000"

        '기재 상 일련번호 항목
        statement.serialNum = "123"

        '기재 상 비고 항목
        statement.remark1 = "비고1"
        statement.remark2 = "비고2"
        statement.remark3 = "비고3"

        '발행 안내문자 발송여부
        statement.smssendYN = False

        '사업자등록증 이미지 첨부여부
        statement.businessLicenseYN = False

        '통장사본 이미지 첨부여부
        statement.bankBookYN = False


        statement.detailList = New List(Of StatementDetail)

        Dim newDetail As StatementDetail = New StatementDetail

        newDetail.serialNum = 1             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20210701"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격
        newDetail.unit = "단위"             '단위
        newDetail.qty = "1" '               '수량 소수점 2자리까지 문자열로 기재가능
        newDetail.unitCost = "100000"       '단가 소수점 2자리까지 문자열로 기재가능
        newDetail.supplyCost = "100000"     '공급가액 소수점 기재 불가
        newDetail.tax = "10000"             '세액 소수점 기재불가
        newDetail.remark = "비고"           '비고
        newDetail.spare1 = "spare1"         '여분1
        newDetail.spare2 = "spare2"         '여분2
        newDetail.spare3 = "spare3"         '여분3
        newDetail.spare4 = "spare4"         '여분4
        newDetail.spare5 = "spare5"         '여분5

        statement.detailList.Add(newDetail)

        newDetail = New StatementDetail

        newDetail.serialNum = 2             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20210701"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격


        '=========================================================================
        ' 전자명세서 추가속성
        ' - 추가속성에 관한 자세한 사항은 "[전자명세서 API 연동매뉴얼] >
        '   5.2. 기본양식 추가속성 테이블"을 참조하시기 바랍니다.
        '=========================================================================
        statement.propertyBag = New Dictionary(Of String, String)

        statement.propertyBag.Add("CBalance", "10000")
        statement.propertyBag.Add("Deposit", "10000")
        statement.propertyBag.Add("Balance", "10000")

        '메모
        Dim memo As String = "즉시발행 메모"

        '안내메일 제목
        Dim emailSubject As String = ""

        Try
            Dim response As STMIssueResponse = statementService.RegistIssue(txtCorpNum.Text, statement, memo, txtUserId.Text, emailSubject)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message + vbCrLf + "팝빌 승인번호(invoiceNum) : " + response.invoiceNum)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 작성된 전자명세서 데이터를 팝빌에 저장합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#Register
    '=========================================================================
    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim statement As New Statement

        '[필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20210701"

        '[필수] {영수, 청구} 중 기재
        statement.purposeType = "영수"

        '[필수] 과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text

        '[필수] 전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '[필수] 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text


        '=========================================================================
        '                               발신자 정보
        '=========================================================================

        '발신자 사업자번호, '-' 제외 10자리
        statement.senderCorpNum = txtCorpNum.Text

        '발신자 종사업장 식별번호, 필요시 기재, 형식은 숫자 4자리
        statement.senderTaxRegID = ""

        '발신자 상호
        statement.senderCorpName = "발신자 상호"

        '발신자 대표자 성명
        statement.senderCEOName = "발신자 대표자 성명"

        '발신자 주소
        statement.senderAddr = "발신자 주소"

        '발신자 종목
        statement.senderBizClass = "발신자 종목"

        '발신자 업태
        statement.senderBizType = "발신자 업태,업태2"

        '발신자 담당자성명
        statement.senderContactName = "발신자 담당자명"

        '발신자 이메일
        statement.senderEmail = "test@test.com"

        '발신자 연락처
        statement.senderTEL = "070-7070-0707"

        '발신자 휴대전화 번호
        statement.senderHP = "010-000-2222"


        '=========================================================================
        '                        수신자 정보
        '=========================================================================

        '수신자 사업자번호, '-' 제외 10자리
        statement.receiverCorpNum = "8888888888"

        '수신자 상호
        statement.receiverCorpName = "수신자 상호"

        '수신자 대표자 성명
        statement.receiverCEOName = "수신자 대표자 성명"

        '수신자 주소
        statement.receiverAddr = "수신자 주소"

        '수신자 종목
        statement.receiverBizClass = "수신자 종목 "

        '수신자 업태
        statement.receiverBizType = "수신자 업태"

        '수신자 담당자명
        statement.receiverContactName = "수신자 담당자명"

        '수신자 담당자 휴대폰번호
        statement.receiverHP = "010-1111-2222"

        '수신자 담당자 연락처
        statement.receiverTEL = "070-1234-1234"

        '수신자 메일주소
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        statement.receiverEmail = "test@receiver.com"

        '=========================================================================
        '                     전자명세서 기재사항
        '=========================================================================

        '[필수] 공급가액 합계
        statement.supplyCostTotal = "100000"

        '[필수] 세액 합계
        statement.taxTotal = "10000"

        '[필수] 합계금액, 공급가액 합계 + 세액 합계
        statement.totalAmount = "110000"             '필수 합계금액.  공급가액 + 세액

        '기재 상 일련번호 항목
        statement.serialNum = "123"

        '기재 상 비고 항목
        statement.remark1 = "비고1"
        statement.remark2 = "비고2"
        statement.remark3 = "비고3"

        '발행 안내문자 발송여부
        statement.smssendYN = False

        '사업자등록증 이미지 첨부여부
        statement.businessLicenseYN = False

        '통장사본 이미지 첨부여부
        statement.bankBookYN = False


        statement.detailList = New List(Of StatementDetail)

        Dim newDetail As StatementDetail = New StatementDetail

        newDetail.serialNum = 1             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20210701"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격
        newDetail.unit = "단위"             '단위
        newDetail.qty = "1" '               '수량 소수점 2자리까지 문자열로 기재가능
        newDetail.unitCost = "100000"       '단가 소수점 2자리까지 문자열로 기재가능
        newDetail.supplyCost = "100000"     '공급가액 소수점 기재 불가
        newDetail.tax = "10000"             '세액 소수점 기재불가
        newDetail.remark = "비고"           '비고
        newDetail.spare1 = "spare1"         '여분1
        newDetail.spare2 = "spare2"         '여분2
        newDetail.spare3 = "spare3"         '여분3
        newDetail.spare4 = "spare4"         '여분4
        newDetail.spare5 = "spare5"         '여분5

        statement.detailList.Add(newDetail)

        newDetail = New StatementDetail

        newDetail.serialNum = 2             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20210701"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격


        '=========================================================================
        ' 전자명세서 추가속성
        ' - 추가속성에 관한 자세한 사항은 "[전자명세서 API 연동매뉴얼] >
        '   5.2. 기본양식 추가속성 테이블"을 참조하시기 바랍니다.
        '=========================================================================
        statement.propertyBag = New Dictionary(Of String, String)

        statement.propertyBag.Add("CBalance", "10000")
        statement.propertyBag.Add("Deposit", "10000")
        statement.propertyBag.Add("Balance", "10000")

        Try
            Dim response As Response = statementService.Register(txtCorpNum.Text, statement, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 전자명세서를 수정합니다.건의 전자명세서를 [수정]합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#Update
    '=========================================================================
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Dim statement As New Statement

        '[필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20210701"

        '[필수] {영수, 청구} 중 기재
        statement.purposeType = "영수"

        '[필수] 과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text

        '[필수] 전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '[필수] 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text


        '=========================================================================
        '                               발신자 정보
        '=========================================================================

        '발신자 사업자번호, '-' 제외 10자리
        statement.senderCorpNum = txtCorpNum.Text

        '발신자 종사업장 식별번호, 필요시 기재, 형식은 숫자 4자리
        statement.senderTaxRegID = ""

        '발신자 상호
        statement.senderCorpName = "발신자 상호"

        '발신자 대표자 성명
        statement.senderCEOName = "발신자 대표자 성명"

        '발신자 주소
        statement.senderAddr = "발신자 주소"

        '발신자 종목
        statement.senderBizClass = "발신자 종목"

        '발신자 업태
        statement.senderBizType = "발신자 업태,업태2"

        '발신자 담당자성명
        statement.senderContactName = "발신자 담당자명"

        '발신자 이메일
        statement.senderEmail = "test@test.com"

        '발신자 연락처
        statement.senderTEL = "070-7070-0707"

        '발신자 휴대전화 번호
        statement.senderHP = "010-000-2222"


        '=========================================================================
        '                        수신자 정보
        '=========================================================================

        '수신자 사업자번호, '-' 제외 10자리
        statement.receiverCorpNum = "8888888888"

        '수신자 상호
        statement.receiverCorpName = "수신자 상호"

        '수신자 대표자 성명
        statement.receiverCEOName = "수신자 대표자 성명"

        '수신자 주소
        statement.receiverAddr = "수신자 주소"

        '수신자 종목
        statement.receiverBizClass = "수신자 종목 "

        '수신자 업태
        statement.receiverBizType = "수신자 업태"

        '수신자 담당자명
        statement.receiverContactName = "수신자 담당자명"

        '수신자 담당자 휴대폰번호
        statement.receiverHP = "010-1111-2222"

        '수신자 담당자 연락처
        statement.receiverTEL = "070-1234-1234"

        '수신자 메일주소
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        statement.receiverEmail = "test@receiver.com"

        '=========================================================================
        '                     전자명세서 기재사항
        '=========================================================================

        '[필수] 공급가액 합계
        statement.supplyCostTotal = "100000"

        '[필수] 세액 합계
        statement.taxTotal = "10000"

        '[필수] 합계금액, 공급가액 합계 + 세액 합계
        statement.totalAmount = "110000"             '필수 합계금액.  공급가액 + 세액

        '기재 상 일련번호 항목
        statement.serialNum = "123"

        '기재 상 비고 항목
        statement.remark1 = "비고1"
        statement.remark2 = "비고2"
        statement.remark3 = "비고3"

        '발행 안내문자 발송여부
        statement.smssendYN = False

        '사업자등록증 이미지 첨부여부
        statement.businessLicenseYN = False

        '통장사본 이미지 첨부여부
        statement.bankBookYN = False


        statement.detailList = New List(Of StatementDetail)

        Dim newDetail As StatementDetail = New StatementDetail

        newDetail.serialNum = 1             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20210701"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격
        newDetail.unit = "단위"             '단위
        newDetail.qty = "1" '               '수량 소수점 2자리까지 문자열로 기재가능
        newDetail.unitCost = "100000"       '단가 소수점 2자리까지 문자열로 기재가능
        newDetail.supplyCost = "100000"     '공급가액 소수점 기재 불가
        newDetail.tax = "10000"             '세액 소수점 기재불가
        newDetail.remark = "비고"           '비고
        newDetail.spare1 = "spare1"         '여분1
        newDetail.spare2 = "spare2"         '여분2
        newDetail.spare3 = "spare3"         '여분3
        newDetail.spare4 = "spare4"         '여분4
        newDetail.spare5 = "spare5"         '여분5

        statement.detailList.Add(newDetail)

        newDetail = New StatementDetail

        newDetail.serialNum = 2             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20210701"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격


        '=========================================================================
        ' 전자명세서 추가속성
        ' - 추가속성에 관한 자세한 사항은 "[전자명세서 API 연동매뉴얼] >
        '   5.2. 기본양식 추가속성 테이블"을 참조하시기 바랍니다.
        '=========================================================================
        statement.propertyBag = New Dictionary(Of String, String)

        statement.propertyBag.Add("CBalance", "10000")
        statement.propertyBag.Add("Deposit", "10000")
        statement.propertyBag.Add("Balance", "10000")


        Try
            Dim response As Response = statementService.Update(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, statement, txtUserId.Text)
            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 전자명세서를 발행하여, "발행완료" 상태로 처리합니다.
    ' - 팝빌 사이트 [전자명세서] > [환경설정] > [전자명세서 관리] 메뉴의 발행시 자동승인 옵션 설정을 통해 전자명세서를 "발행완료" 상태가 아닌 "승인대기" 상태로 발행 처리 할 수 있습니다.
    ' - 전자명세서 발행 함수 호출시 포인트가 과금되며, 수신자에게 발행 안내 메일이 발송됩니다.
    ' - https://docs.popbill.com/statement/dotnet/api#StmIssue
    '=========================================================================
    Private Sub btnIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIssue.Click

        '메모
        Dim memo As String = "전자명세서 발행 메모"

        Try
            Dim response As Response = statementService.Issue(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 발신자가 발행한 전자명세서를 발행취소합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#CancelIssue
    '=========================================================================
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        '메모
        Dim memo As String = "발행취소 메모"

        Try
            Dim response As Response = statementService.CancelIssue(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 발신자가 발행한 전자명세서를 발행취소합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#CancelIssue
    '=========================================================================
    Private Sub btnCancelIssueSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssueSub.Click

        '메모
        Dim memo As String = "발행취소 메모"

        Try
            Dim response As Response = statementService.CancelIssue(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 전자명세서를 삭제합니다.
    ' - 삭제 가능한 상태: "임시저장", "취소", "승인거부", "발행취소"
    ' - 전자명세서를 삭제하면 사용된 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://docs.popbill.com/statement/dotnet/api#Delete
    '=========================================================================
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim response As Response = statementService.Delete(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 전자명세서를 삭제합니다.
    ' - 삭제 가능한 상태: "임시저장", "취소", "승인거부", "발행취소"
    ' - 전자명세서를 삭제하면 사용된 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://docs.popbill.com/statement/dotnet/api#Delete
    '=========================================================================
    Private Sub btnDeleteSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSub.Click
        Try
            Dim response As Response = statementService.Delete(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 전자명세서의 1건의 상태 및 요약정보 확인합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetInfo
    '=========================================================================
    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click

        Try
            Dim docInfo As StatementInfo = statementService.GetInfo(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp = tmp + "itemKey (팝빌번호) : " + docInfo.itemKey + vbCrLf
            tmp = tmp + "invoiceNum (문서고유번호) : " + docInfo.invoiceNum + vbCrLf
            tmp = tmp + "mgtKey (문서번호) : " + docInfo.mgtKey + vbCrLf
            tmp = tmp + "taxType (세금형태) : " + docInfo.taxType + vbCrLf
            tmp = tmp + "writeDate (작성일자) : " + docInfo.writeDate + vbCrLf
            tmp = tmp + "regDT (임시저장일시) : " + docInfo.regDT + vbCrLf
            tmp = tmp + "senderCorpName (발신자 상호) : " + docInfo.senderCorpName + vbCrLf
            tmp = tmp + "senderCorpNum (발신자 사업자등록번호) : " + docInfo.senderCorpNum + vbCrLf
            tmp = tmp + "senderPrintYN (발신자 인쇄여부) : " + CStr(docInfo.senderPrintYN) + vbCrLf
            tmp = tmp + "receiverCorpName (수신자 상호): " + docInfo.receiverCorpName + vbCrLf
            tmp = tmp + "receiverCorpNum (수신자 사업자등록번호) : " + docInfo.receiverCorpNum + vbCrLf
            tmp = tmp + "receiverPrintYN (수신자 인쇄여부) : " + CStr(docInfo.receiverPrintYN) + vbCrLf
            tmp = tmp + "supplyCostTotal (공급가액 합계) : " + docInfo.supplyCostTotal + vbCrLf
            tmp = tmp + "taxTotal (세액 합계) : " + docInfo.taxTotal + vbCrLf
            tmp = tmp + "purposeType (영수/청구) : " + docInfo.purposeType + vbCrLf
            tmp = tmp + "issueDT (발행일시) : " + docInfo.issueDT + vbCrLf
            tmp = tmp + "stateCode (상태코드) : " + CStr(docInfo.stateCode) + vbCrLf
            tmp = tmp + "stateDT (상태 변경일시) : " + docInfo.stateDT + vbCrLf
            tmp = tmp + "stateMemo (상태메모) : " + docInfo.stateMemo + vbCrLf
            tmp = tmp + "openYN (개봉 여부) : " + CStr(docInfo.openYN) + vbCrLf
            tmp = tmp + "openDT (개봉 일시) : " + docInfo.openDT + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 다수건의 전자명세서 상태 및 요약정보 확인합니다. (1회 호출 시 최대 1,000건 확인 가능)
    ' - https://docs.popbill.com/statement/dotnet/api#GetInfos
    '=========================================================================
    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서번호 배열, 최대 1000건
        MgtKeyList.Add("20210701-01")
        MgtKeyList.Add("20210701-02")

        Try
            Dim statementInfoList As List(Of StatementInfo) = statementService.GetInfos(txtCorpNum.Text, selectedItemCode, MgtKeyList)

            Dim tmp As String = ""

            For Each docInfo As StatementInfo In statementInfoList
                tmp = tmp + "itemKey (팝빌번호) : " + docInfo.itemKey + vbCrLf
                tmp = tmp + "invoiceNum (문서고유번호) : " + docInfo.invoiceNum + vbCrLf
                tmp = tmp + "mgtKey (문서번호) : " + docInfo.mgtKey + vbCrLf
                tmp = tmp + "taxType (세금형태) : " + docInfo.taxType + vbCrLf
                tmp = tmp + "writeDate (작성일자) : " + docInfo.writeDate + vbCrLf
                tmp = tmp + "regDT (임시저장일시) : " + docInfo.regDT + vbCrLf
                tmp = tmp + "senderCorpName (발신자 상호) : " + docInfo.senderCorpName + vbCrLf
                tmp = tmp + "senderCorpNum (발신자 사업자등록번호) : " + docInfo.senderCorpNum + vbCrLf
                tmp = tmp + "senderPrintYN (발신자 인쇄여부) : " + CStr(docInfo.senderPrintYN) + vbCrLf
                tmp = tmp + "receiverCorpName (수신자 상호): " + docInfo.receiverCorpName + vbCrLf
                tmp = tmp + "receiverCorpNum (수신자 사업자등록번호) : " + docInfo.receiverCorpNum + vbCrLf
                tmp = tmp + "receiverPrintYN (수신자 인쇄여부) : " + CStr(docInfo.receiverPrintYN) + vbCrLf
                tmp = tmp + "supplyCostTotal (공급가액 합계) : " + docInfo.supplyCostTotal + vbCrLf
                tmp = tmp + "taxTotal (세액 합계) : " + docInfo.taxTotal + vbCrLf
                tmp = tmp + "purposeType (영수/청구) : " + docInfo.purposeType + vbCrLf
                tmp = tmp + "issueDT (발행일시) : " + docInfo.issueDT + vbCrLf
                tmp = tmp + "stateCode (상태코드) : " + CStr(docInfo.stateCode) + vbCrLf
                tmp = tmp + "stateDT (상태 변경일시) : " + docInfo.stateDT + vbCrLf
                tmp = tmp + "stateMemo (상태메모) : " + docInfo.stateMemo + vbCrLf
                tmp = tmp + "openYN (개봉 여부) : " + CStr(docInfo.openYN) + vbCrLf
                tmp = tmp + "openDT (개봉 일시) : " + docInfo.openDT + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 1건의 상세정보 확인합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetDetailInfo
    '=========================================================================
    Private Sub btnGetDetailInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDetailInfo.Click

        Dim tmp As String = ""

        Try

            Dim docDetailInfo As Statement = statementService.GetDetailInfo(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            tmp = tmp + "itemCode(문서종류코드) : " + CStr(docDetailInfo.itemCode) + vbCrLf
            tmp = tmp + "mgtKey(문서번호) : " + docDetailInfo.mgtKey + vbCrLf
            tmp = tmp + "invoiceNum(문서고유번호) : " + docDetailInfo.invoiceNum + vbCrLf
            tmp = tmp + "formCode(맞춤양식 코드) : " + docDetailInfo.formCode + vbCrLf
            tmp = tmp + "writeDate(작성일자) : " + docDetailInfo.writeDate + vbCrLf
            tmp = tmp + "taxType(세금형태) : " + docDetailInfo.taxType + vbCrLf
            tmp = tmp + "purposeType(영수/청구) : " + docDetailInfo.purposeType + vbCrLf
            tmp = tmp + "serialNum(일련번호) : " + docDetailInfo.serialNum + vbCrLf
            tmp = tmp + "taxTotal(세액 합계) : " + docDetailInfo.taxTotal + vbCrLf
            tmp = tmp + "supplyCostTotal(공급가액 합계) : " + docDetailInfo.supplyCostTotal + vbCrLf
            tmp = tmp + "totalAmount(합계금액) : " + docDetailInfo.totalAmount + vbCrLf
            tmp = tmp + "remark1(비고1) : " + docDetailInfo.remark1 + vbCrLf
            tmp = tmp + "remark2(비고2) : " + docDetailInfo.remark2 + vbCrLf
            tmp = tmp + "remark3(비고3) : " + docDetailInfo.remark3 + vbCrLf

            tmp = tmp + "senderCorpNum(발신자 사업자번호) : " + docDetailInfo.senderCorpNum + vbCrLf
            tmp = tmp + "senderTaxRegID(발신자 종사업장번호) : " + docDetailInfo.senderTaxRegID + vbCrLf
            tmp = tmp + "senderCorpName(발신자 상호) : " + docDetailInfo.senderCorpName + vbCrLf
            tmp = tmp + "senderCEOName(발신자 대표자 성명) : " + docDetailInfo.senderCEOName + vbCrLf
            tmp = tmp + "senderAddr(발신자 주소) : " + docDetailInfo.senderAddr + vbCrLf
            tmp = tmp + "senderBizType(발신자 업태) : " + docDetailInfo.senderBizType + vbCrLf
            tmp = tmp + "senderBizClass(발신자 종목) : " + docDetailInfo.senderBizClass + vbCrLf
            tmp = tmp + "senderContactName(발신자 성명) : " + docDetailInfo.senderContactName + vbCrLf
            tmp = tmp + "senderDeptName(발신자 부서명) : " + docDetailInfo.senderDeptName + vbCrLf
            tmp = tmp + "senderTEL(발신자 연락처) : " + docDetailInfo.senderTEL + vbCrLf
            tmp = tmp + "senderHP(발신자 휴대전화) : " + docDetailInfo.senderHP + vbCrLf
            tmp = tmp + "senderEmail(발신자 이메일주소) : " + docDetailInfo.senderEmail + vbCrLf
            tmp = tmp + "senderFAX(발신자 팩스번호) : " + docDetailInfo.senderFAX + vbCrLf

            tmp = tmp + "receiverCorpNum(수신자 사업자번호) : " + docDetailInfo.receiverCorpNum + vbCrLf
            tmp = tmp + "receiverTaxRegID(수신자 종사업장번호) : " + docDetailInfo.receiverTaxRegID + vbCrLf
            tmp = tmp + "receiverCorpName(수신자 상호) : " + docDetailInfo.receiverCorpName + vbCrLf
            tmp = tmp + "receiverCEOName(수신자 대표자 성명) : " + docDetailInfo.receiverCEOName + vbCrLf
            tmp = tmp + "receiverAddr(수신자 주소) : " + docDetailInfo.receiverAddr + vbCrLf
            tmp = tmp + "receiverBizType(수신자 업태) : " + docDetailInfo.receiverBizType + vbCrLf
            tmp = tmp + "receiverBizClass(수신자 종목) : " + docDetailInfo.receiverBizClass + vbCrLf
            tmp = tmp + "receiverContactName(수신자 성명) : " + docDetailInfo.receiverContactName + vbCrLf
            tmp = tmp + "receiverDeptName(수신자 부서명) : " + docDetailInfo.receiverDeptName + vbCrLf
            tmp = tmp + "receiverTEL(수신자 연락처) : " + docDetailInfo.receiverTEL + vbCrLf
            tmp = tmp + "receiverHP(수신자 휴대전화) : " + docDetailInfo.receiverHP + vbCrLf
            tmp = tmp + "receiverEmail(수신자 이메일주소) : " + docDetailInfo.receiverEmail + vbCrLf
            tmp = tmp + "receiverFAX(수신자 팩스번호) : " + docDetailInfo.receiverFAX + vbCrLf

            If Not docDetailInfo.detailList Is Nothing Then
                For Each detailList As StatementDetail In docDetailInfo.detailList
                    tmp += "[상세항목(품목)]" + vbCrLf
                    tmp += "일련번호 : " + CStr(detailList.serialNum) + vbCrLf
                    tmp += "purchaseDT (거래일자) : " + detailList.purchaseDT + vbCrLf
                    tmp += "itemName (품목명) : " + detailList.itemName + vbCrLf
                    tmp += "spec (규격) :" + detailList.spec + vbCrLf
                    tmp += "qty (수량) :" + detailList.qty + vbCrLf
                    tmp += "unitCost (단가) :" + detailList.unitCost + vbCrLf
                    tmp += "supplyCost (공급가액) : " + detailList.supplyCost + vbCrLf
                    tmp += "tax (세액) :" + detailList.tax + vbCrLf
                    tmp += "remark (비고) :" + detailList.remark + vbCrLf
                Next
                tmp += vbCrLf
            End If

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건에 해당하는 전자명세서를 조회합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(3) As String
        Dim ItemCode(6) As Integer

        '[필수] 일자유형, R-등록일시 W-작성일자 I-발행일시 중 택1
        Dim DType As String = "W"

        '[필수] 시작일자, yyyyMMdd
        Dim SDate As String = "20210701"

        '[필수] 종료일자, yyyyMMdd
        Dim EDate As String = "20210730"

        '전송상태값 배열, 미기재시 전체상태조회, 문서상태값 3자리숫자 작성
        '2,3번째 와일드카드 가능
        State(0) = "2**"
        State(1) = "3**"

        '문서종류코드 배열, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        ItemCode(0) = 121
        ItemCode(1) = 122
        ItemCode(2) = 123
        ItemCode(3) = 124
        ItemCode(4) = 125
        ItemCode(5) = 126

        '거래처 정보조회, 거래처 상호 또는 거래처 사업자등록번호 기재, 미기재시 전체조회
        Dim QString = ""

        '정렬방향, D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

        '페이지 번호
        Dim Page As Integer = 1

        '페이지 목록개수, 최대 1000건
        Dim PerPage As Integer = 10

        Try
            Dim stmtSearchList As DocSearchResult = statementService.Search(txtCorpNum.Text, DType, SDate, EDate, State, ItemCode, QString, Order, Page, PerPage)

            Dim tmp As String

            tmp = "code (응답코드) : " + stmtSearchList.code.ToString + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + stmtSearchList.total.ToString + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + stmtSearchList.perPage.ToString + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + stmtSearchList.pageNum.ToString + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + stmtSearchList.pageCount.ToString + vbCrLf
            tmp = tmp + "message (응답메시지) : " + stmtSearchList.message + vbCrLf + vbCrLf

            Dim docInfo As StatementInfo

            For Each docInfo In stmtSearchList.list
                tmp = tmp + "itemKey (팝빌번호) : " + docInfo.itemKey + vbCrLf
                tmp = tmp + "stateCode (상태코드) : " + docInfo.stateCode.ToString + vbCrLf
                tmp = tmp + "taxType (세금형태) : " + docInfo.taxType + vbCrLf
                tmp = tmp + "purposeType (영수/청구) : " + docInfo.purposeType + vbCrLf
                tmp = tmp + "writeDate (작성일자) : " + docInfo.writeDate + vbCrLf
                tmp = tmp + "senderCorpName (발신자상호) : " + docInfo.senderCorpName + vbCrLf
                tmp = tmp + "senderCorpNum (발신자 사업자번호) : " + docInfo.senderCorpNum + vbCrLf
                tmp = tmp + "senderPrintYN (발신자 인쇄여부) : " + docInfo.senderPrintYN.ToString + vbCrLf
                tmp = tmp + "receiverCorpName (수신자상호) : " + docInfo.receiverCorpName + vbCrLf
                tmp = tmp + "receiverCorpNum (수신자 사업자번호) : " + docInfo.receiverCorpNum + vbCrLf
                tmp = tmp + "receiverPrintYN (수신자 인쇄여부) : " + docInfo.receiverPrintYN.ToString + vbCrLf
                tmp = tmp + "supplyCostTotal (공급가액) : " + docInfo.supplyCostTotal + vbCrLf
                tmp = tmp + "taxTotal (세액) : " + docInfo.taxTotal + vbCrLf
                tmp = tmp + "issueDT (발행일시) : " + docInfo.issueDT + vbCrLf
                tmp = tmp + "stateDT (상태일시) : " + docInfo.stateDT + vbCrLf
                tmp = tmp + "openYN (개봉여부) : " + docInfo.openYN.ToString + vbCrLf
                tmp = tmp + "openDT (개봉일시) : " + docInfo.openDT + vbCrLf
                tmp = tmp + "stateMemo (상태메모) : " + docInfo.stateMemo + vbCrLf
                tmp = tmp + "regDT (임시저장일시) : " + docInfo.regDT + vbCrLf + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서의 상태에 대한 변경이력을 확인합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetLogs
    '=========================================================================
    Private Sub btnGetLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetLogs.Click

        Dim tmp As String = ""

        Try
            Dim logList As List(Of StatementLog) = statementService.GetLogs(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            tmp += "docType(로그타입) | log(이력정보) | procType(처리형태) | procContactName(처리담당자) |"
            tmp += "procMemo(처리메모) | regDT(등록일시) | ip(아이피)" + vbCrLf + vbCrLf

            For Each log As StatementLog In logList
                tmp += log.docLogType.ToString + " | " + log.log + " | " + log.procType + " | " + log.procCorpName + " | " + log.procContactName + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 전자명세서 임시문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetURL
    '=========================================================================
    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TBOX.Click
        Try
            Dim url As String = statementService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX")

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 전자명세서 매출문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetURL
    '=========================================================================
    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_SBOX.Click
        Try
            Dim url As String = statementService.GetURL(txtCorpNum.Text, txtUserId.Text, "SBOX")

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 팝빌 사이트와 동일한 전자명세서 1건의 상세 정보 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetPopUpURL
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        Try
            Dim url As String = statementService.GetPopUpURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 1건을 인쇄하기 위한 페이지의 팝업 URL을 반환하며, 페이지내에서 인쇄 설정값을 "공급자" / "공급받는자" / "공급자+공급받는자"용 중 하나로 지정할 수 있습니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetPrintURL
    '=========================================================================
    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click

        Try
            Dim url As String = statementService.GetPrintURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "공급받는자" 용 전자명세서 1건을 인쇄하기 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetEPrintURL
    '=========================================================================
    Private Sub btnGetEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEPrintURL.Click

        Try
            Dim url As String = statementService.GetEPrintURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 다수건의 전자명세서를 인쇄하기 위한 페이지의 팝업 URL을 반환합니다. (최대 100건)
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetMassPrintURL
    '=========================================================================
    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMassPrintURL.Click

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서번호 배열 (최대 100건)
        MgtKeyList.Add("20210701-01")
        MgtKeyList.Add("20210701-02")

        Try
            Dim url As String = statementService.GetMassPrintURL(txtCorpNum.Text, selectedItemCode, MgtKeyList, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 안내메일과 관련된 전자명세서를 확인 할 수 있는 상세 페이지의 팝업 URL을 반환하며, 해당 URL은 메일 하단의 파란색 버튼의 링크와 같습니다.
    ' - 함수 호출로 반환 받은 URL에는 유효시간이 없습니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetMailURL
    '=========================================================================
    Private Sub btnGetMailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMailURL.Click
        Try
            Dim url As String = statementService.GetMailURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = statementService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 인감 및 첨부문서 등록 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_SEAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_SEAL.Click
        Try
            Dim url As String = statementService.GetSealURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 명세서에 1개의 파일을 첨부합니다. (최대 5개)
    ' - https://docs.popbill.com/statement/dotnet/api#AttachFile
    '=========================================================================
    Private Sub btnAttachFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachFile.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then
            Dim strFileName As String = fileDialog.FileName

            Try
                Dim response As Response = statementService.AttachFile(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, strFileName, txtUserId.Text)

                MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

            End Try

        End If
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 전자명세서에 첨부된 1개의 파일을 삭제합니다.
    ' - 파일을 식별하는 파일아이디는 첨부파일 목록(GetFiles API) 의 응답항목 중 파일아이디(AttachedFile) 값을 통해 확인할 수 있습니다.
    ' - https://docs.popbill.com/statement/dotnet/api#DeleteFile
    '=========================================================================
    Private Sub btnDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFile.Click
        Try
            Dim response As Response = statementService.DeleteFile(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtFileID.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 전자명세서에 첨부된 파일목록을 확인합니다.
    ' - 응답항목 중 파일아이디(AttachedFile) 항목은 파일삭제(DeleteFile API) 호출시 이용할 수 있습니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetFiles
    '=========================================================================
    Private Sub btnGetFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFiles.Click

        Try
            Dim fileList As List(Of AttachedFile) = statementService.GetFiles(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            Dim tmp As String = "serialNum(일련번호) | displayName(첨부파일명) | attachedFile(파일아이디) | regDT(등록일자)" + vbCrLf

            For Each file As AttachedFile In fileList
                tmp += file.serialNum.ToString() + " | " + file.displayName + " | " + file.attachedFile + " | " + file.regDT + vbCrLf

                txtFileID.Text = file.attachedFile
            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' "승인대기", "발행완료" 상태의 전자명세서와 관련된 발행 안내 메일을 재전송 합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#SendEmail
    '=========================================================================
    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click

        '수신메일주소
        Dim receiveMail As String = "test@test.com"

        Try
            Dim response As Response = statementService.SendEmail(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, receiveMail, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서와 관련된 안내 SMS(단문) 문자를 재전송하는 함수로, 팝빌 사이트 [문자·팩스] > [문자] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
    ' - 메시지는 최대 90byte까지 입력 가능하고, 초과한 내용은 자동으로 삭제되어 전송합니다. (한글 최대 45자)
    ' - 함수 호출시 포인트가 과금됩니다.
    ' - https://docs.popbill.com/statement/dotnet/api#SendSMS
    '=========================================================================
    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click

        '발신번호
        Dim sendNum As String = "070-4304-2991"

        '수신번호
        Dim receiveNum As String = "010-111-2222"

        '메시지내용, 최대90Byte(한글45자), 90Byte 초과한 내용은 삭제되어 전송
        Dim contents As String = "문자메시지 내용."

        Try
            Dim response As Response = statementService.SendSMS(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, sendNum, receiveNum, contents, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서를 팩스로 전송하는 함수로, 팝빌 사이트 [문자·팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
    ' - 함수 호출시 포인트가 과금됩니다.
    ' - https://docs.popbill.com/statement/dotnet/api#SendFAX
    '=========================================================================
    Private Sub btnSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFAX.Click

        '팩스 발신번호
        Dim sendNum As String = "070-111-2222"

        '팩스 수신번호
        Dim receiveNum As String = "070-111-2223"

        Try
            Dim response As Response = statementService.SendFAX(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, sendNum, receiveNum, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서를 팩스로 전송하는 함수로, 팝빌에 데이터를 저장하는 과정이 없습니다.
    ' - 팝빌 사이트 [문자·팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
    ' - 함수 호출시 포인트가 과금됩니다.
    ' - 팩스 발행 요청시 작성한 문서번호는 팩스전송 파일명으로 사용됩니다.
    ' - 팩스 전송결과를 확인하기 위해서는 선팩스 전송 요청 시 반환받은 접수번호를 이용하여 팩스 API의 전송결과 확인 (GetFaxDetail) API를 이용하면 됩니다.
    ' - https://docs.popbill.com/statement/dotnet/api#FAXSend
    '=========================================================================
    Private Sub btnFAXSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFAXSend.Click

        '팩스 발신번호
        Dim sendNum As String = "070111222"

        '팩스 수신번호
        Dim receiveNum As String = "070111222"


        Dim statement As New Statement

        '[필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20210701"

        '[필수] {영수, 청구} 중 기재
        statement.purposeType = "영수"

        '[필수] 과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text

        '[필수] 전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '[필수] 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text


        '=========================================================================
        '                               발신자 정보
        '=========================================================================

        '발신자 사업자번호, '-' 제외 10자리
        statement.senderCorpNum = txtCorpNum.Text

        '발신자 종사업장 식별번호, 필요시 기재, 형식은 숫자 4자리
        statement.senderTaxRegID = ""

        '발신자 상호
        statement.senderCorpName = "발신자 상호"

        '발신자 대표자 성명
        statement.senderCEOName = "발신자 대표자 성명"

        '발신자 주소
        statement.senderAddr = "발신자 주소"

        '발신자 종목
        statement.senderBizClass = "발신자 종목"

        '발신자 업태
        statement.senderBizType = "발신자 업태,업태2"

        '발신자 담당자성명
        statement.senderContactName = "발신자 담당자명"

        '발신자 이메일
        statement.senderEmail = "test@test.com"

        '발신자 연락처
        statement.senderTEL = "070-7070-0707"

        '발신자 휴대전화 번호
        statement.senderHP = "010-000-2222"


        '=========================================================================
        '                        수신자 정보
        '=========================================================================

        '수신자 사업자번호, '-' 제외 10자리
        statement.receiverCorpNum = "8888888888"

        '수신자 상호
        statement.receiverCorpName = "수신자 상호"

        '수신자 대표자 성명
        statement.receiverCEOName = "수신자 대표자 성명"

        '수신자 주소
        statement.receiverAddr = "수신자 주소"

        '수신자 종목
        statement.receiverBizClass = "수신자 종목 "

        '수신자 업태
        statement.receiverBizType = "수신자 업태"

        '수신자 담당자명
        statement.receiverContactName = "수신자 담당자명"

        '수신자 담당자 휴대폰번호
        statement.receiverHP = "010-1111-2222"

        '수신자 담당자 연락처
        statement.receiverTEL = "070-1234-1234"

        '수신자 메일주소
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        statement.receiverEmail = "test@receiver.com"

        '=========================================================================
        '                     전자명세서 기재사항
        '=========================================================================

        '[필수] 공급가액 합계
        statement.supplyCostTotal = "100000"

        '[필수] 세액 합계
        statement.taxTotal = "10000"

        '[필수] 합계금액, 공급가액 합계 + 세액 합계
        statement.totalAmount = "110000"             '필수 합계금액.  공급가액 + 세액

        '기재 상 일련번호 항목
        statement.serialNum = "123"

        '기재 상 비고 항목
        statement.remark1 = "비고1"
        statement.remark2 = "비고2"
        statement.remark3 = "비고3"

        '발행 안내문자 발송여부
        statement.smssendYN = False

        '사업자등록증 이미지 첨부여부
        statement.businessLicenseYN = False

        '통장사본 이미지 첨부여부
        statement.bankBookYN = False


        statement.detailList = New List(Of StatementDetail)

        Dim newDetail As StatementDetail = New StatementDetail

        newDetail.serialNum = 1             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20210701"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격
        newDetail.unit = "단위"             '단위
        newDetail.qty = "1" '               '수량 소수점 2자리까지 문자열로 기재가능
        newDetail.unitCost = "100000"       '단가 소수점 2자리까지 문자열로 기재가능
        newDetail.supplyCost = "100000"     '공급가액 소수점 기재 불가
        newDetail.tax = "10000"             '세액 소수점 기재불가
        newDetail.remark = "비고"           '비고
        newDetail.spare1 = "spare1"         '여분1
        newDetail.spare2 = "spare2"         '여분2
        newDetail.spare3 = "spare3"         '여분3
        newDetail.spare4 = "spare4"         '여분4
        newDetail.spare5 = "spare5"         '여분5

        statement.detailList.Add(newDetail)

        newDetail = New StatementDetail

        newDetail.serialNum = 2             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20210701"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격


        '=========================================================================
        ' 전자명세서 추가속성
        ' - 추가속성에 관한 자세한 사항은 "[전자명세서 API 연동매뉴얼] >
        '   5.2. 기본양식 추가속성 테이블"을 참조하시기 바랍니다.
        '=========================================================================
        statement.propertyBag = New Dictionary(Of String, String)

        statement.propertyBag.Add("CBalance", "10000")
        statement.propertyBag.Add("Deposit", "10000")
        statement.propertyBag.Add("Balance", "10000")


        Try
            Dim receiptNum As String = statementService.FAXSend(txtCorpNum.Text, statement, sendNum, receiveNum, txtUserId.Text)

            MsgBox("팩스 접수번호(receiptNum) : " + receiptNum)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 하나의 전자명세서에 다른 전자명세서를 첨부합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#AttachStatement
    '=========================================================================
    Private Sub btnAttachStmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachStmt.Click

        '첨부할 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim subItemCode As Integer = 121

        '첨부할 전자명세서 문서번호
        Dim subMgtKey As String = "20210701-02"

        Try
            Dim response As Response = statementService.AttachStatement(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, subItemCode, subMgtKey)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 하나의 전자명세서에 첨부된 다른 전자명세서를 해제합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#DetachStatement
    '=========================================================================
    Private Sub btnDetachStmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetachStmt.Click

        '첨부해제 대상 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim subItemCode As Integer = 121

        '첨부해제 대상 전자명세서 문서번호
        Dim subMgtKey As String = "20210701-02"

        Try
            Dim response As Response = statementService.DetachStatement(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, subItemCode, subMgtKey)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 관련 메일 항목에 대한 발송설정을 확인합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#ListEmailConfig
    '=========================================================================
    Private Sub btnListEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListEmailConfig.Click
        Try
            Dim emailConfigList As List(Of EmailConfig) = statementService.ListEmailConfig(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "메일전송유형 | 전송여부 " + vbCrLf

            For Each info As EmailConfig In emailConfigList
                If info.emailType = "SMT_ISSUE" Then _
                    tmp += "SMT_ISSUE (수신자에게 전자명세서가 발행 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "SMT_ACCEPT" Then _
                    tmp += "SMT_ACCEPT (발신자에게 전자명세서가 승인 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "SMT_DENY" Then _
                    tmp += "SMT_DENY (발신자게에 전자명세서가 거부 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "SMT_CANCEL" Then _
                    tmp += "SMT_CANCEL (수신자게에 전자명세서가 취소 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "SMT_CANCEL_ISSUE" Then _
                    tmp += "SMT_CANCEL_ISSUE (수신자에게 전자명세서가 발행취소 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 관련 메일 항목에 대한 발송설정을 수정합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#UpdateEmailConfig
    '
    ' 메일전송유형
    ' SMT_ISSUE : 수신자에게 전자명세서가 발행 되었음을 알려주는 메일입니다.
    ' SMT_ACCEPT : 발신자에게 전자명세서가 승인 되었음을 알려주는 메일입니다.
    ' SMT_DENY : 발신자게에 전자명세서가 거부 되었음을 알려주는 메일입니다.
    ' SMT_CANCEL : 수신자게에 전자명세서가 취소 되었음을 알려주는 메일입니다.
    ' SMT_CANCEL_ISSUE : 수신자에게 전자명세서가 발행취소 되었음을 알려주는 메일입니다.
    '=========================================================================
    Private Sub btnUpdateEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUpdateEmailConfig.Click
        Try
            '메일전송유형
            Dim emailType As String = "SMT_ISSUE"

            '전송여부 (True-전송, False-미전송)
            Dim sendYN As Boolean = True

            Dim response As Response = statementService.UpdateEmailConfig(txtCorpNum.Text, emailType, sendYN, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)를 통해 확인하시기 바랍니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = statementService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click

        Try
            Dim url As String = statementService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPaymentURL.Click
        Try
            Dim url As String = statementService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = statementService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = statementService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = statementService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 발행시 과금되는 포인트 단가를 확인합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            Dim unitCost As Single = statementService.GetUnitCost(txtCorpNum.Text, selectedItemCode)

            MsgBox("전자명세서 발행단가(unitCost) : " + unitCost.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 전자명세서 API 서비스 과금정보를 확인합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click

        Try
            Dim ChargeInfo As ChargeInfo = statementService.GetChargeInfo(txtCorpNum.Text, selectedItemCode)

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
    ' - https://docs.popbill.com/statement/dotnet/api#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = statementService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = statementService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#JoinMember
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
        joinInfo.ContactEmail = "test@test.com"

        '담당자 연락처 (최대 20자)
        joinInfo.ContactTEL = "070-4304-2991"

        '담당자 휴대폰번호 (최대 20자)
        joinInfo.ContactHP = "010-111-222"

        '담당자 팩스번호 (최대 20자)
        joinInfo.ContactFAX = "02-6442-9700"

        Try
            Dim response As Response = statementService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetCorpInfo
    '=========================================================================
    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCorpInfo.Click
        Try
            Dim corpInfo As CorpInfo = statementService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://docs.popbill.com/statement/dotnet/api#UpdateCorpInfo
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

            Dim response As Response = statementService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#RegistContact
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
            Dim response As Response = statementService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = statementService.GetContactInfo(txtCorpNum.Text, contactID, txtUserId.Text)

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
    ' - https://docs.popbill.com/statement/dotnet/api#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = statementService.ListContact(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://docs.popbill.com/statement/dotnet/api#UpdateContact
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
            Dim response As Response = statementService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트와 동일한 전자명세서 1건의 상세 정보 페이지(사이트 상단, 좌측 메뉴 및 버튼 제외)의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/statement/dotnet/api#GetViewURL
    '=========================================================================
    Private Sub btnGetViewURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetViewURL.Click

        Try
            Dim url As String = statementService.GetViewURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub
End Class
