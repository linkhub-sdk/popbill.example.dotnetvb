'=========================================================================
' 팝빌 전자명세서 API .NET SDK VB.NET Example
' VB.NET 연동 튜토리얼 안내 : https://developers.popbill.com/guide/statement/dotnet/getting-started/tutorial?fwn=vb
'
' 업데이트 일자 : 2025-07-23
' 연동기술지원 연락처 : 1600-9854
' 연동기술지원 이메일 : code@linkhubcorp.com
'         
' <테스트 연동개발 준비사항>
' 1) API Key 변경 (연동신청 시 메일로 전달된 정보)
'     - LinkID : 링크허브에서 발급한 링크아이디
'     - SecretKey : 링크허브에서 발급한 비밀키
' 2) SDK 환경설정 옵션 설정
'     - IsTest : 연동환경 설정, true-테스트, false-운영(Production), (기본값:true)
'     - IPRestrictOnOff : 인증토큰 IP 검증 설정, true-사용, false-미사용, (기본값:true)
'     - UseStaticIP : 통신 IP 고정, true-사용, false-미사용, (기본값:false)
'     - UseLocalTimeYN : 로컬시스템 시간 사용여부, true-사용, false-미사용, (기본값:true)
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

        '연동환경 설정, true-테스트, false-운영(Production), (기본값:true)
        statementService.IsTest = True

        '인증토큰 IP 검증 설정, true-사용, false-미사용, (기본값:true)
        statementService.IPRestrictOnOff = True

        '통신 IP 고정, true-사용, false-미사용, (기본값:false)
        statementService.UseStaticIP = False

        '로컬시스템 시간 사용여부, true-사용, false-미사용, (기본값:true)
        statementService.UseLocalTimeYN = False

    End Sub

    ' 명세서 종류코드 반환
    Private Function selectedItemCode()

        Dim itemCode As Integer = 121

        If cboItemCode.Text = "거래명세서" Then
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
    ' - https://developers.popbill.com/reference/statement/dotnet/api/info#CheckMgtKeyInUse
    '=========================================================================
    Private Sub btnCheckMgtKeyInUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckMgtKeyInUse.Click

        Try
            Dim InUse As Boolean = statementService.CheckMgtKeyInuse(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            MsgBox(IIf(InUse, "사용중", "미사용중"))

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 작성된 전자명세서 데이터를 팝빌에 저장과 동시에 발행하여, "발행완료" 상태로 처리합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/issue#RegistIssue
    '=========================================================================
    Private Sub btnRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistIssue.Click
        Dim statement As New Statement

        '전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text


        '기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20250731"

        '과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '{영수, 청구, 없음} 중 기재
        statement.purposeType = "영수"

        

        '기재 상 일련번호 항목
        statement.serialNum = "123"

        '세액 합계
        statement.taxTotal = "10000"

        '공급가액 합계
        statement.supplyCostTotal = "100000"

        '합계금액, 공급가액 합계 + 세액 합계
        statement.totalAmount = "110000"

        '기재 상 비고 항목
        statement.remark1 = "비고1"
        statement.remark2 = "비고2"
        statement.remark3 = "비고3"

        
        


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

        '발신자 업태
        statement.senderBizType = "발신자 업태,업태2"

        '발신자 종목
        statement.senderBizClass = "발신자 종목"

        '발신자 담당자성명
        statement.senderContactName = "발신자 담당자명"

        '발신자 이메일
        statement.senderEmail = ""

        '발신자 연락처
        statement.senderTEL = ""

        '발신자 휴대전화 번호
        statement.senderHP = ""


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

        '수신자 업태
        statement.receiverBizType = "수신자 업태"

        '수신자 종목
        statement.receiverBizClass = "수신자 종목 "

        

        '수신자 담당자명
        statement.receiverContactName = "수신자 담당자명"

        '수신자 담당자 휴대폰번호
        statement.receiverHP = ""

        '수신자 담당자 연락처
        statement.receiverTEL = ""

        '수신자 메일주소
        '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        statement.receiverEmail = ""


        ' 문자 자동전송 여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송(기본값)
        statement.smssendYN = False


        ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        statement.businessLicenseYN = False

        ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        statement.bankBookYN = False


        '=========================================================================
        ' 전자명세서 추가속성
        ' - 추가속성에 관한 자세한 사항은 "[전자명세서 API 연동매뉴얼] >
        '   기본양식 추가속성 테이블"을 참조하시기 바랍니다.
        ' [https://developers.popbill.com/guide/statement/dotnet/introduction/statement-form#propertybag-table]
        '=========================================================================
        statement.propertyBag = New Dictionary(Of String, String)

        statement.propertyBag.Add("CBalance", "10000")
        statement.propertyBag.Add("Deposit", "10000")
        statement.propertyBag.Add("Balance", "10000")



        statement.detailList = New List(Of StatementDetail)

        Dim newDetail As StatementDetail = New StatementDetail

        newDetail.serialNum = 1             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20250731"   '거래일자  yyyyMMdd
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
        newDetail.purchaseDT = "20250731"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격

        statement.detailList.Add(newDetail)


        '메모
        Dim memo As String = "즉시발행 메모"

        '안내메일 제목
        Dim emailSubject As String = ""

        Try
            Dim response As STMIssueResponse = statementService.RegistIssue(txtCorpNum.Text, statement, memo, txtUserId.Text, emailSubject)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message + vbCrLf + "invoiceNum(팝빌 승인번호) : " + response.invoiceNum)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 작성된 전자명세서 데이터를 팝빌에 저장합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/issue#Register
    '=========================================================================
    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim statement As New Statement

        '전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text


        '기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20250731"

        '과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '{영수, 청구, 없음} 중 기재
        statement.purposeType = "영수"



        '기재 상 일련번호 항목
        statement.serialNum = "123"

        '세액 합계
        statement.taxTotal = "10000"

        '공급가액 합계
        statement.supplyCostTotal = "100000"

        '합계금액, 공급가액 합계 + 세액 합계
        statement.totalAmount = "110000"

        '기재 상 비고 항목
        statement.remark1 = "비고1"
        statement.remark2 = "비고2"
        statement.remark3 = "비고3"





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

        '발신자 업태
        statement.senderBizType = "발신자 업태,업태2"

        '발신자 종목
        statement.senderBizClass = "발신자 종목"

        '발신자 담당자성명
        statement.senderContactName = "발신자 담당자명"

        '발신자 이메일
        statement.senderEmail = ""

        '발신자 연락처
        statement.senderTEL = ""

        '발신자 휴대전화 번호
        statement.senderHP = ""


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

        '수신자 업태
        statement.receiverBizType = "수신자 업태"

        '수신자 종목
        statement.receiverBizClass = "수신자 종목 "



        '수신자 담당자명
        statement.receiverContactName = "수신자 담당자명"

        '수신자 담당자 휴대폰번호
        statement.receiverHP = ""

        '수신자 담당자 연락처
        statement.receiverTEL = ""

        '수신자 메일주소
        '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        statement.receiverEmail = ""


        ' 문자 자동전송 여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송(기본값)
        statement.smssendYN = False


        ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        statement.businessLicenseYN = False

        ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        statement.bankBookYN = False


        '=========================================================================
        ' 전자명세서 추가속성
        ' - 추가속성에 관한 자세한 사항은 "[전자명세서 API 연동매뉴얼] >
        '   기본양식 추가속성 테이블"을 참조하시기 바랍니다.
        ' [https://developers.popbill.com/guide/statement/dotnet/introduction/statement-form#propertybag-table]
        '=========================================================================
        statement.propertyBag = New Dictionary(Of String, String)

        statement.propertyBag.Add("CBalance", "10000")
        statement.propertyBag.Add("Deposit", "10000")
        statement.propertyBag.Add("Balance", "10000")



        statement.detailList = New List(Of StatementDetail)

        Dim newDetail As StatementDetail = New StatementDetail

        newDetail.serialNum = 1             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20250731"   '거래일자  yyyyMMdd
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
        newDetail.purchaseDT = "20250731"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격

        statement.detailList.Add(newDetail)

        Try
            Dim response As Response = statementService.Register(txtCorpNum.Text, statement, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 전자명세서를 수정합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/issue#Update
    '=========================================================================
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Dim statement As New Statement

        '전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text


        '기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20250731"

        '과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '{영수, 청구, 없음} 중 기재
        statement.purposeType = "영수"



        '기재 상 일련번호 항목
        statement.serialNum = "123"

        '세액 합계
        statement.taxTotal = "10000"

        '공급가액 합계
        statement.supplyCostTotal = "100000"

        '합계금액, 공급가액 합계 + 세액 합계
        statement.totalAmount = "110000"

        '기재 상 비고 항목
        statement.remark1 = "비고1"
        statement.remark2 = "비고2"
        statement.remark3 = "비고3"





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

        '발신자 업태
        statement.senderBizType = "발신자 업태,업태2"

        '발신자 종목
        statement.senderBizClass = "발신자 종목"

        '발신자 담당자성명
        statement.senderContactName = "발신자 담당자명"

        '발신자 이메일
        statement.senderEmail = ""

        '발신자 연락처
        statement.senderTEL = ""

        '발신자 휴대전화 번호
        statement.senderHP = ""


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

        '수신자 업태
        statement.receiverBizType = "수신자 업태"

        '수신자 종목
        statement.receiverBizClass = "수신자 종목 "



        '수신자 담당자명
        statement.receiverContactName = "수신자 담당자명"

        '수신자 담당자 휴대폰번호
        statement.receiverHP = ""

        '수신자 담당자 연락처
        statement.receiverTEL = ""

        '수신자 메일주소
        '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        statement.receiverEmail = ""


        ' 문자 자동전송 여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송(기본값)
        statement.smssendYN = False


        ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        statement.businessLicenseYN = False

        ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        statement.bankBookYN = False


        '=========================================================================
        ' 전자명세서 추가속성
        ' - 추가속성에 관한 자세한 사항은 "[전자명세서 API 연동매뉴얼] >
        '   기본양식 추가속성 테이블"을 참조하시기 바랍니다.
        ' [https://developers.popbill.com/guide/statement/dotnet/introduction/statement-form#propertybag-table]
        '=========================================================================
        statement.propertyBag = New Dictionary(Of String, String)

        statement.propertyBag.Add("CBalance", "10000")
        statement.propertyBag.Add("Deposit", "10000")
        statement.propertyBag.Add("Balance", "10000")



        statement.detailList = New List(Of StatementDetail)

        Dim newDetail As StatementDetail = New StatementDetail

        newDetail.serialNum = 1             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20250731"   '거래일자  yyyyMMdd
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
        newDetail.purchaseDT = "20250731"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격

        statement.detailList.Add(newDetail)


        Try
            Dim response As Response = statementService.Update(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, statement, txtUserId.Text)
            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 전자명세서를 발행하여, "발행완료" 상태로 처리합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/issue#Issue
    '=========================================================================
    Private Sub btnIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIssue.Click

        '메모
        Dim memo As String = "전자명세서 발행 메모"

        '발행 안내메일 제목
        Dim EmailSubject As String = ""

        Try
            Dim response As Response = statementService.Issue(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, memo, EmailSubject, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 발신자가 발행한 전자명세서를 발행취소합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/issue#Cancel
    '=========================================================================
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        '메모
        Dim memo As String = "발행취소 메모"

        Try
            Dim response As Response = statementService.CancelIssue(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 발신자가 발행한 전자명세서를 발행취소합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/issue#Cancel
    '=========================================================================
    Private Sub btnCancelIssueSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssueSub.Click

        '메모
        Dim memo As String = "발행취소 메모"

        Try
            Dim response As Response = statementService.CancelIssue(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 전자명세서를 삭제합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/issue#Delete
    '=========================================================================
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim response As Response = statementService.Delete(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 전자명세서를 삭제합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/issue#Delete
    '=========================================================================
    Private Sub btnDeleteSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSub.Click
        Try
            Dim response As Response = statementService.Delete(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 전자명세서의 1건의 상태 및 요약정보를 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/info#GetInfo
    '=========================================================================
    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click

        Try
            Dim docInfo As StatementInfo = statementService.GetInfo(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp = tmp + "itemCode (전자면세서 문서 유형) : " + docInfo.itemCode.ToString + vbCrLf
            tmp = tmp + "itemKey (팝빌번호) : " + docInfo.itemKey + vbCrLf
            tmp = tmp + "invoiceNum (팝빌 승인번호) : " + docInfo.invoiceNum + vbCrLf
            tmp = tmp + "mgtKey (문서번호) : " + docInfo.mgtKey + vbCrLf

            tmp = tmp + "taxType (과세형태) : " + docInfo.taxType + vbCrLf
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
            tmp = tmp + "stateCode (상태코드) : " + docInfo.stateCode.ToString + vbCrLf
            tmp = tmp + "stateDT (상태 변경일시) : " + docInfo.stateDT + vbCrLf
            tmp = tmp + "stateMemo (상태메모) : " + docInfo.stateMemo + vbCrLf

            tmp = tmp + "openYN (개봉 여부) : " + CStr(docInfo.openYN) + vbCrLf
            tmp = tmp + "openDT (개봉 일시) : " + docInfo.openDT + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 다수건의 전자명세서 상태 및 요약정보를 확인합니다. (1회 호출에 최대 1,000건 확인 가능)
    ' - https://developers.popbill.com/reference/statement/dotnet/api/info#GetInfos
    '=========================================================================
    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서번호 배열, 최대 1000건
        MgtKeyList.Add("20220513-001")
        MgtKeyList.Add("20220513-002")

        Try
            Dim statementInfoList As List(Of StatementInfo) = statementService.GetInfos(txtCorpNum.Text, selectedItemCode, MgtKeyList)

            Dim tmp As String = ""

            For Each docInfo As StatementInfo In statementInfoList
                tmp = tmp + "itemCode (전자면세서 문서 유형) : " + docInfo.itemCode.ToString + vbCrLf
                tmp = tmp + "itemKey (팝빌번호) : " + docInfo.itemKey + vbCrLf
                tmp = tmp + "invoiceNum (팝빌 승인번호) : " + docInfo.invoiceNum + vbCrLf
                tmp = tmp + "mgtKey (문서번호) : " + docInfo.mgtKey + vbCrLf

                tmp = tmp + "taxType (과세형태) : " + docInfo.taxType + vbCrLf
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
                tmp = tmp + "stateCode (상태코드) : " + docInfo.stateCode.ToString + vbCrLf
                tmp = tmp + "stateDT (상태 변경일시) : " + docInfo.stateDT + vbCrLf
                tmp = tmp + "stateMemo (상태메모) : " + docInfo.stateMemo + vbCrLf

                tmp = tmp + "openYN (개봉 여부) : " + CStr(docInfo.openYN) + vbCrLf
                tmp = tmp + "openDT (개봉 일시) : " + docInfo.openDT + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 1건의 상세정보를 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/info#GetDetailInfo
    '=========================================================================
    Private Sub btnGetDetailInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDetailInfo.Click

        Dim tmp As String = ""

        Try

            Dim docDetailInfo As Statement = statementService.GetDetailInfo(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            tmp = tmp + "itemCode(전자명세서 문서 유형) : " + docDetailInfo.itemCode.ToString + vbCrLf
            tmp = tmp + "mgtKey(문서번호) : " + docDetailInfo.mgtKey + vbCrLf
            tmp = tmp + "invoiceNum(팝빌 승인번호) : " + docDetailInfo.invoiceNum + vbCrLf
            tmp = tmp + "formCode(맞춤양식 코드) : " + docDetailInfo.formCode + vbCrLf

            tmp = tmp + "writeDate(작성일자) : " + docDetailInfo.writeDate + vbCrLf
            tmp = tmp + "taxType(과세형태) : " + docDetailInfo.taxType + vbCrLf
            tmp = tmp + "purposeType(영수/청구) : " + docDetailInfo.purposeType + vbCrLf
            tmp = tmp + "serialNum(일련번호) : " + docDetailInfo.serialNum + vbCrLf
            tmp = tmp + "taxTotal(세액 합계) : " + docDetailInfo.taxTotal + vbCrLf
            tmp = tmp + "supplyCostTotal(공급가액 합계) : " + docDetailInfo.supplyCostTotal + vbCrLf
            tmp = tmp + "totalAmount(합계금액) : " + docDetailInfo.totalAmount + vbCrLf
            tmp = tmp + "remark1(비고1) : " + docDetailInfo.remark1 + vbCrLf
            tmp = tmp + "remark2(비고2) : " + docDetailInfo.remark2 + vbCrLf
            tmp = tmp + "remark3(비고3) : " + docDetailInfo.remark3 + vbCrLf

            tmp = tmp + "senderCorpNum(발신자 사업자번호) : " + docDetailInfo.senderCorpNum + vbCrLf
            tmp = tmp + "senderTaxRegID(발신자 종사업장번호 식별번호) : " + docDetailInfo.senderTaxRegID + vbCrLf
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
            tmp = tmp + "receiverTaxRegID(수신자 종사업장번호 식별번호) : " + docDetailInfo.receiverTaxRegID + vbCrLf
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
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건에 해당하는 전자명세서를 조회합니다. (최대 조회기간 : 6개월)
    ' - https://developers.popbill.com/reference/statement/dotnet/api/info#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(3) As String
        Dim ItemCode(6) As Integer

        ' 일자 유형 ("R" , "W" , "I" 중 택 1)
        ' └ R = 등록일자 , W = 작성일자 , I = 발행일자
        Dim DType As String = "W"

        '시작일자, yyyyMMdd
        Dim SDate As String = "20250701"

        '종료일자, yyyyMMdd
        Dim EDate As String = "20250731"

        ' 전자명세서 상태코드 배열 (2,3번째 자리에 와일드카드(*) 사용 가능)
        ' - 미입력시 전체조회
        State(0) = "2**"
        State(1) = "3**"

        '전자명세서 문서 유형 배열, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        ItemCode(0) = 121
        ItemCode(1) = 122
        ItemCode(2) = 123
        ItemCode(3) = 124
        ItemCode(4) = 125
        ItemCode(5) = 126

        ' 통합검색어, 거래처 상호명 또는 거래처 사업자번호로 조회
        ' - 미입력시 전체조회
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
            tmp = tmp + "message (응답메시지) : " + stmtSearchList.message + vbCrLf + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + stmtSearchList.total.ToString + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + stmtSearchList.perPage.ToString + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + stmtSearchList.pageNum.ToString + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + stmtSearchList.pageCount.ToString + vbCrLf

            Dim docInfo As StatementInfo

            For Each docInfo In stmtSearchList.list
                tmp = tmp + "itemCode (전자면세서 문서 유형) : " + docInfo.itemCode.ToString + vbCrLf
                tmp = tmp + "itemKey (팝빌번호) : " + docInfo.itemKey + vbCrLf
                tmp = tmp + "invoiceNum (팝빌 승인번호) : " + docInfo.invoiceNum + vbCrLf
                tmp = tmp + "mgtKey (문서번호) : " + docInfo.mgtKey + vbCrLf

                tmp = tmp + "taxType (과세형태) : " + docInfo.taxType + vbCrLf
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
                tmp = tmp + "stateCode (상태코드) : " + docInfo.stateCode.ToString + vbCrLf
                tmp = tmp + "stateDT (상태 변경일시) : " + docInfo.stateDT + vbCrLf
                tmp = tmp + "stateMemo (상태메모) : " + docInfo.stateMemo + vbCrLf

                tmp = tmp + "openYN (개봉 여부) : " + CStr(docInfo.openYN) + vbCrLf
                tmp = tmp + "openDT (개봉 일시) : " + docInfo.openDT + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서의 상태에 대한 변경이력을 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/info#GetLogs
    '=========================================================================
    Private Sub btnGetLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetLogs.Click

        Dim tmp As String = ""

        Try
            Dim logList As List(Of StatementLog) = statementService.GetLogs(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            tmp += "docType(로그타입) | log(이력정보) | procType(처리형태) |"
            tmp += "procMemo(처리메모) | regDT(등록일시) | ip(아이피)" + vbCrLf + vbCrLf

            For Each log As StatementLog In logList
                tmp += log.docLogType.ToString + " | " + log.log + " | " + log.procType + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 문서함의 팝업 URL을 반환합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TBOX.Click
        Try
            Dim url As String = statementService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX")

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 문서함의 팝업 URL을 반환합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_SBOX.Click
        Try
            Dim url As String = statementService.GetURL(txtCorpNum.Text, txtUserId.Text, "SBOX")

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 전자명세서 1건의 팝업 URL을 반환합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/view#GetPopUpURL
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        Try
            Dim url As String = statementService.GetPopUpURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 1건의 팝업 URL을 반환합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/view#GetViewURL
    '=========================================================================
    Private Sub btnGetViewURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetViewURL.Click

        Try
            Dim url As String = statementService.GetViewURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 1건의 공급자 인쇄 팝업 URL을 반환합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/view#GetPrintURL
    '=========================================================================
    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click

        Try
            Dim url As String = statementService.GetPrintURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 1건의 공급받는자 인쇄 팝업 URL을 반환합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/view#GetEPrintURL
    '=========================================================================
    Private Sub btnGetEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEPrintURL.Click

        Try
            Dim url As String = statementService.GetEPrintURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 다건의 인쇄 팝업 URL을 반환합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/view#GetMassPrintURL
    '=========================================================================
    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMassPrintURL.Click

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서번호 배열 (최대 100건)
        MgtKeyList.Add("20220513-001")
        MgtKeyList.Add("20220513-002")

        Try
            Dim url As String = statementService.GetMassPrintURL(txtCorpNum.Text, selectedItemCode, MgtKeyList, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 발행 안내 메일의 '보기' 버튼 URL을 반환합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/view#GetMailURL
    '=========================================================================
    Private Sub btnGetMailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMailURL.Click
        Try
            Dim url As String = statementService.GetMailURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = statementService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서에 첨부될 인감, 사업자등록증, 통장사본을 등록하는 팝업 URL을 반환합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#GetSealURL
    '=========================================================================
    Private Sub btnGetPopbillURL_SEAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_SEAL.Click
        Try
            Dim url As String = statementService.GetSealURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 명세서에 1개의 파일을 첨부합니다. (최대 5개)
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#AttachFile
    '=========================================================================
    Private Sub btnAttachFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachFile.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then
            Dim strFileName As String = fileDialog.FileName

            Try
                Dim response As Response = statementService.AttachFile(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, strFileName, txtUserId.Text)

                MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
            Catch ex As PopbillException
                MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

            End Try

        End If
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 전자명세서에 첨부된 1개의 파일을 삭제합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#DeleteFile
    '=========================================================================
    Private Sub btnDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFile.Click
        Try
            Dim response As Response = statementService.DeleteFile(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtFileID.Text, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 전자명세서에 첨부된 파일목록을 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#GetFiles
    '=========================================================================
    Private Sub btnGetFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFiles.Click

        Try
            Dim fileList As List(Of AttachedFile) = statementService.GetFiles(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            Dim tmp As String = "serialNum(일련번호) | attachedFile(파일 식별번호) | displayName(파일명) | regDT(등록일자)" + vbCrLf

            For Each file As AttachedFile In fileList
                tmp += file.serialNum.ToString + " | " + file.attachedFile + " | " + file.displayName + " | " + file.regDT + vbCrLf

                txtFileID.Text = file.attachedFile
            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' "승인대기", "발행완료" 상태의 전자명세서와 관련된 발행 안내 메일을 재전송 합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#SendEmail
    '=========================================================================
    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click

        '수신메일주소
        Dim receiveMail As String = ""

        Try
            Dim response As Response = statementService.SendEmail(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, receiveMail, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서와 관련된 안내 SMS(단문) 문자를 재전송하는 함수로, 팝빌 사이트 [ 문자 > 결과 > 전송결과 ] 메뉴에서 전송결과를 확인할 수 있습니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#SendSMS
    '=========================================================================
    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click

        '발신번호
        Dim sendNum As String = ""

        '수신번호
        Dim receiveNum As String = ""

        '메시지내용, 최대90Byte(한글45자), 90Byte 초과한 내용은 삭제되어 전송
        Dim contents As String = "문자메시지 내용."

        Try
            Dim response As Response = statementService.SendSMS(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, sendNum, receiveNum, contents, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서를 팩스로 전송하는 함수로, 팝빌 사이트 [ 팩스 > 결과 > 전송결과 ] 메뉴에서 전송결과를 확인할 수 있습니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#SendFAX
    '=========================================================================
    Private Sub btnSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFAX.Click

        '팩스 발신번호
        Dim sendNum As String = ""

        '팩스 수신번호
        Dim receiveNum As String = ""

        Try
            Dim response As Response = statementService.SendFAX(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, sendNum, receiveNum, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서를 팩스로 전송하는 함수로, 팝빌에 데이터를 저장하는 과정이 없습니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#FAXSend
    '=========================================================================
    Private Sub btnFAXSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFAXSend.Click

        

        Dim statement As New Statement

        '전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text


        '기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20250731"

        '과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '{영수, 청구, 없음} 중 기재
        statement.purposeType = "영수"



        '기재 상 일련번호 항목
        statement.serialNum = "123"

        '세액 합계
        statement.taxTotal = "10000"

        '공급가액 합계
        statement.supplyCostTotal = "100000"

        '합계금액, 공급가액 합계 + 세액 합계
        statement.totalAmount = "110000"

        '기재 상 비고 항목
        statement.remark1 = "비고1"
        statement.remark2 = "비고2"
        statement.remark3 = "비고3"





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

        '발신자 업태
        statement.senderBizType = "발신자 업태,업태2"

        '발신자 종목
        statement.senderBizClass = "발신자 종목"

        '발신자 담당자성명
        statement.senderContactName = "발신자 담당자명"

        '발신자 이메일
        statement.senderEmail = ""

        '발신자 연락처
        statement.senderTEL = ""

        '발신자 휴대전화 번호
        statement.senderHP = ""


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

        '수신자 업태
        statement.receiverBizType = "수신자 업태"

        '수신자 종목
        statement.receiverBizClass = "수신자 종목 "



        '수신자 담당자명
        statement.receiverContactName = "수신자 담당자명"

        '수신자 담당자 휴대폰번호
        statement.receiverHP = ""

        '수신자 담당자 연락처
        statement.receiverTEL = ""

        '수신자 메일주소
        '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        statement.receiverEmail = ""


        ' 문자 자동전송 여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송(기본값)
        statement.smssendYN = False


        ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        statement.businessLicenseYN = False

        ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        statement.bankBookYN = False


        '=========================================================================
        ' 전자명세서 추가속성
        ' - 추가속성에 관한 자세한 사항은 "[전자명세서 API 연동매뉴얼] >
        '   기본양식 추가속성 테이블"을 참조하시기 바랍니다.
        ' [https://developers.popbill.com/guide/statement/dotnet/introduction/statement-form#propertybag-table]
        '=========================================================================
        statement.propertyBag = New Dictionary(Of String, String)

        statement.propertyBag.Add("CBalance", "10000")
        statement.propertyBag.Add("Deposit", "10000")
        statement.propertyBag.Add("Balance", "10000")



        statement.detailList = New List(Of StatementDetail)

        Dim newDetail As StatementDetail = New StatementDetail

        newDetail.serialNum = 1             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20250731"   '거래일자  yyyyMMdd
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
        newDetail.purchaseDT = "20250731"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"         '품목명
        newDetail.spec = "규격"             '규격

        statement.detailList.Add(newDetail)


        '팩스 발신번호
        Dim sendNum As String = ""

        '팩스 수신번호
        Dim receiveNum As String = ""


        Try
            Dim receiptNum As String = statementService.FAXSend(txtCorpNum.Text, statement, sendNum, receiveNum, txtUserId.Text)

            MsgBox("receiptNum(팩스 접수번호) : " + receiptNum)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 하나의 전자명세서에 다른 전자명세서를 첨부합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#AttachStatement
    '=========================================================================
    Private Sub btnAttachStmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachStmt.Click

        '첨부할 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim subItemCode As Integer = 121

        '첨부할 전자명세서 문서번호
        Dim subMgtKey As String = "20220513-001"

        Try
            Dim response As Response = statementService.AttachStatement(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, subItemCode, subMgtKey)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 하나의 전자명세서에 첨부된 다른 전자명세서를 해제합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#DetachStatement
    '=========================================================================
    Private Sub btnDetachStmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetachStmt.Click

        '첨부해제 대상 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim subItemCode As Integer = 121

        '첨부해제 대상 전자명세서 문서번호
        Dim subMgtKey As String = "20220513-001"

        Try
            Dim response As Response = statementService.DetachStatement(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, subItemCode, subMgtKey)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 하나의 전자명세서에 첨부된 다른 전자명세서를 해제합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#ListEmailConfig
    '=========================================================================
    Private Sub btnListEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListEmailConfig.Click
        Try
            Dim emailConfigList As List(Of EmailConfig) = statementService.ListEmailConfig(txtCorpNum.Text)

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
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 관련 메일 항목에 대한 발송설정을 수정합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/api/etc#UpdateEmailConfig
    '=========================================================================
    Private Sub btnUpdateEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUpdateEmailConfig.Click
        Try
            '메일전송유형
            Dim emailType As String = "SMT_ISSUE"

            '전송여부 (True-전송, False-미전송)
            Dim sendYN As Boolean = True

            Dim response As Response = statementService.UpdateEmailConfig(txtCorpNum.Text, emailType, sendYN)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = statementService.GetBalance(txtCorpNum.Text)

            MsgBox("remainPoint(연동회원 잔여포인트) : " + remainPoint.ToString)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click

        Try
            Dim url As String = statementService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPaymentURL.Click
        Try
            Dim url As String = statementService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = statementService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = statementService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("remainPoint(파트너 잔여포인트) : " + remainPoint.ToString)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = statementService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 발행시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            Dim unitCost As Single = statementService.GetUnitCost(txtCorpNum.Text, selectedItemCode)

            MsgBox("unitCost(전자명세서 발행단가) : " + unitCost.ToString)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 전자명세서 API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click

        Try
            Dim ChargeInfo As ChargeInfo = statementService.GetChargeInfo(txtCorpNum.Text, selectedItemCode)

            Dim tmp As String = "unitCost (발행단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = statementService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = statementService.CheckID(txtCorpNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#JoinMember
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
            Dim response As Response = statementService.JoinMember(joinInfo)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#GetCorpInfo
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
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#UpdateCorpInfo
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

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#RegistContact
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistContact.Click

        ' 담당자 정보객체
        Dim joinData As New Contact

        ' 아이디 (6자이상 50자미만)
        joinData.id = "testkorea1120"

        ' 비밀번호, 8자 이상 20자 이하(영문, 숫자, 특수문자 조합)
        joinData.Password = "asdf8536!@#"

        ' 담당자 성명 (최대 100자)
        joinData.personName = "담당자명"

        ' 담당자 휴대폰 (최대 20자)
        joinData.tel = "010-1234-1234"

        ' 담당자 메일 (최대 100자)
        joinData.email = "test@email.com"

        ' 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = statementService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = statementService.GetContactInfo(txtCorpNum.Text, contactID)

            tmp += "id (아이디) : " + contactInfo.id + vbCrLf
            tmp += "personName (담당자 성명) : " + contactInfo.personName + vbCrLf
            tmp += "tel (담당자 휴대폰) : " + contactInfo.tel + vbCrLf
            tmp += "email (담당자 메일) : " + contactInfo.email + vbCrLf

            tmp += "regDT (등록일시) : " + contactInfo.regDT + vbCrLf
            tmp += "searchRole (권한) : " + contactInfo.searchRole.ToString + vbCrLf
            tmp += "mgrYN (역할) : " + contactInfo.mgrYN.ToString + vbCrLf
            tmp += "state (계정상태) : " + contactInfo.state + vbCrLf
            tmp += vbCrLf

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = statementService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자명) | email(담당자 주소) | tel(담당자 휴대폰) |"
            tmp += "regDT(등록일시) | searchRole(권한) | mgrYN(역할) | state(계정상태)" + vbCrLf

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
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#UpdateContact
    '=========================================================================
    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContact.Click

        ' 담당자 정보객체
        Dim joinData As New Contact

        ' 아이디 (6자이상 50자미만)
        joinData.id = "testkorea1120"

        ' 담당자 성명 (최대 100자)
        joinData.personName = "담당자명"

        ' 담당자 휴대폰 (최대 20자)
        joinData.tel = "010-1234-1234"

        ' 담당자 메일 (최대 100자)
        joinData.email = "test@email.com"

        ' 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = statementService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#PaymentRequest
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
            Dim response As PaymentResponse = statementService.PaymentRequest(txtCorpNum.Text, paymentForm, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message + vbCrLf + "settleCode(정산코드) : " + response.settleCode)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetSettleResult
    '=========================================================================
    Private Sub btnGetSettleResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSettleResult.Click

        '정산코드
        Dim SettleCode As String = "202301160000000010"

        Try
            Dim response As PaymentHistory = statementService.GetSettleResult(txtCorpNum.Text, SettleCode, txtUserId.Text)

            Dim tmp As String = ""

            tmp += "productType(결제 내용) : " + response.productType + vbCrLf
            tmp += "productName(결제 상품명) : " + response.productName + vbCrLf
            tmp += "settleType(결제 유형) : " + response.settleType + vbCrLf
            tmp += "settlerName(담당자명) : " + response.settlerName + vbCrLf
            tmp += "settlerEmail(담당자메일) : " + response.settlerEmail + vbCrLf
            tmp += "settleCost(결제 금액) : " + response.settleCost + vbCrLf
            tmp += "settlePoint(충전포인트) : " + response.settlePoint + vbCrLf
            tmp += "settleState(결제 상태) : " + response.settleState.ToString + vbCrLf
            tmp += "regDT(등록일시) : " + response.regDT + vbCrLf
            tmp += "stateDT(상태일시) : " + response.stateDT

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 결제내역을 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetPaymentHistory
    '=========================================================================
    Private Sub btnGetPaymentHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPaymentHistory.Click

        '조회 시작 일자
        Dim SDate As String = "20250701"

        '조회 종료 일자
        Dim EDate As String = "20250731"

        '목록 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500

        Try
            Dim result As PaymentHistoryResult = statementService.GetPaymentHistory(txtCorpNum.Text, SDate, EDate, Page, PerPage, txtUserId.Text)

            Dim tmp As String = ""
            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "결제내역" + vbCrLf

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
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetUseHistory
    '=========================================================================
    Private Sub btnGetUseHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUseHistory.Click

        '조회 시작 일자
        Dim SDate As String = "20250701"

        '조회 종료 일자
        Dim EDate As String = "20250731"

        '목록 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500

        '목록 정렬 방향
        Dim Order As String = "D"

        Try
            Dim result As UseHistoryResult = statementService.GetUseHistory(txtCorpNum.Text, SDate, EDate, Page, PerPage, Order, txtUserId.Text)

            Dim tmp As String = ""
            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "사용 내역" + vbCrLf

            For Each history As UseHistory In result.list

                tmp += "itemCode(서비스 코드) : " + history.itemCode + vbCrLf
                tmp += "txType(포인트 증감 유형) : " + history.txType + vbCrLf
                tmp += "txPoint(증감 포인트) : " + history.txPoint + vbCrLf
                tmp += "balance(잔여포인트) : " + history.balance + vbCrLf
                tmp += "txDT(포인트 증감 일시) : " + history.txDT + vbCrLf
                tmp += "userID(담당자 아이디) : " + history.userID + vbCrLf
                tmp += "userName(담당자명) : " + history.userName + vbCrLf
                tmp += vbCrLf

            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트를 환불 신청합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#Refund
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
            Dim response As RefundResponse = statementService.Refund(txtCorpNum.Text, refundForm, txtUserId.Text)

            Dim tmp As String = ""
            tmp += "code(응답코드) : " + response.code.ToString + vbCrLf
            tmp += "message(응답메시지) : " + response.Message + vbCrLf
            tmp += "refundCode(환불코드) : " + response.refundCode
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 환불신청내역을 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetRefundHistory
    '=========================================================================
    Private Sub btnGetRefundHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundHistory.Click

        '목폭 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500


        Try
            Dim result As RefundHistoryResult = statementService.GetRefundHistory(txtCorpNum.Text, Page, PerPage, txtUserId.Text)

            Dim tmp As String = ""

            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "환불내역" + vbCrLf

            For Each history As RefundHistory In result.list
                tmp += "reqDT (신청일시) :" + history.reqDT + vbCrLf
                tmp += "requestPoint (환불 신청포인트) :" + history.requestPoint + vbCrLf
                tmp += "accountBank (환불계좌 은행명) :" + history.accountBank + vbCrLf
                tmp += "accountNum (환불계좌번호) :" + history.accountNum + vbCrLf
                tmp += "accountName (환불계좌 예금주명) :" + history.accountName + vbCrLf
                tmp += "state (상태) : " + history.state.ToString + vbCrLf
                tmp += "reason (환불사유) : " + history.reason + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 포인트 환불에 대한 상세정보 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetRefundInfo
    '=========================================================================
    Private Sub btnGetRefundInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '환불코드
        Dim refundCode As String = "023040000017"

        Try
            Dim history As RefundHistory = statementService.GetRefundInfo(txtCorpNum.Text, refundCode, txtUserId.Text)
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
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/point#GetRefundableBalance
    '=========================================================================
    Private Sub btnGetRefundableBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundableBalance.Click

        Try
            Dim refundableBalance As Double = statementService.GetRefundableBalance(txtCorpNum.Text, txtUserId.Text)

            MsgBox("refundableBalance(환불 가능 포인트) : " + refundableBalance.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 가입된 연동회원의 탈퇴를 요청합니다.
    ' - 회원탈퇴 신청과 동시에 팝빌의 모든 서비스 이용이 불가하며, 관리자를 포함한 모든 담당자 계정도 일괄탈퇴 됩니다.
    ' - 회원탈퇴로 삭제된 데이터는 복원이 불가능합니다.
    ' - 관리자 계정만 회원탈퇴가 가능합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#QuitMember
    '=========================================================================
    Private Sub btnQuitMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitMember.Click

        '탈퇴사유
        Dim quitReason As String = "회원 탈퇴 사유"

        Try
            Dim response As Response  = statementService.QuitMember(txtCorpNum.Text, quitReason, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.Message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원에 추가된 담당자를 삭제합니다.
    ' - https://developers.popbill.com/reference/statement/dotnet/common-api/member#DeleteContact
    '=========================================================================
    Private Sub btnDeleteContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteContact.Click

        '삭제할 담당자 아이디
        Dim targetUserID As String = "testkorea20250723_01"

        Try
            Dim response As Response = statementService.DeleteContact(txtCorpNum.Text, targetUserID, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub
End Class
