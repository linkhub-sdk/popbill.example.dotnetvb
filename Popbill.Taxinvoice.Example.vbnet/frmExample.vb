'=========================================================================
' 팝빌 전자세금계산서 API .NET SDK VB.NET Example
' VB.NET 연동 튜토리얼 안내 : https://developers.popbill.com/guide/taxinvoice/dotnet/getting-started/tutorial?fwn=vb
'
' 업데이트 일자 : 2024-11-12
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
' 3) 전자세금계산서 발행을 위해 공동인증서를 등록합니다.
'    - 팝빌사이트 로그인 > [전자세금계산서] > [환경설정] > [공동인증서 관리]
'    - 공동인증서 등록 팝업 URL (GetTaxCertURL API)을 이용하여 등록
'=========================================================================

Imports Popbill
Imports Popbill.Taxinvoice
Imports System.ComponentModel

Public Class frmExample
    '링크아이디
    Private Const LinkID As String = "TESTER"

    '비밀키
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '세금계산서 서비스 변수 선언
    Private taxinvoiceService As TaxinvoiceService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '세금계산서 서비스 객체 초기화
        taxinvoiceService = New TaxinvoiceService(LinkID, SecretKey)

        '연동환경 설정, true-테스트, false-운영(Production), (기본값:true)
        taxinvoiceService.IsTest = True

        '인증토큰 IP 검증 설정, true-사용, false-미사용, (기본값:true)
        taxinvoiceService.IPRestrictOnOff = True

        '통신 IP 고정, true-사용, false-미사용, (기본값:false)
        taxinvoiceService.UseStaticIP = False

        '로컬시스템 시간 사용여부, true-사용, false-미사용, (기본값:true)
        taxinvoiceService.UseLocalTimeYN = False

    End Sub

    '=========================================================================
    ' 파트너가 세금계산서 관리 목적으로 할당하는 문서번호의 사용여부를 확인합니다.
    ' - 이미 사용 중인 문서번호는 중복 사용이 불가하고, 세금계산서가 삭제된 경우에만 문서번호의 재사용이 가능합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#CheckMgtKeyInUse
    '=========================================================================
    Private Sub btnCheckMgtKeyInUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckMgtKeyInUse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim InUse As Boolean = taxinvoiceService.CheckMgtKeyInUse(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox(IIf(InUse, "사용중", "미사용중"))
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 작성된 세금계산서 데이터를 팝빌에 저장과 동시에 발행(전자서명)하여 "발행완료" 상태로 처리합니다.
    ' - 세금계산서 국세청 전송 정책 [https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/policy-of-send-to-nts]
    ' - "발행완료"된 전자세금계산서는 국세청 전송 이전에 발행취소(CancelIssue API) 함수로 국세청 신고 대상에서 제외할 수 있습니다.
    ' - 임시저장(Register API) 함수와 발행(Issue API) 함수를 한 번의 프로세스로 처리합니다.
    ' - 세금계산서 발행을 위해서 공급자의 인증서가 팝빌 인증서버에 사전등록 되어야 합니다.
    '   └ 위수탁발행의 경우, 수탁자의 인증서 등록이 필요합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#RegistIssue
    '=========================================================================
    Private Sub btnRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRegistIssue.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '작성일자,yyyyMMdd( 표시형식 )
        taxinvoice.writeDate = "20220513"

        '발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "정발행"

        ' 과금방향, {정과금, 역과금} 중 기재
        ' └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
        ' -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
        taxinvoice.chargeDirection = "정과금"

        '영수/청구, [영수, 청구, 없음] 중 기재
        taxinvoice.purposeType = "영수"

        '과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"

        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = txtCorpNum.Text

        '공급자 종사업장 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = txtMgtKey.Text

        '공급자 대표자 성명
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"

        '공급자 주소
        taxinvoice.invoicerAddr = "공급자 주소"

        '공급자 종목
        taxinvoice.invoicerBizClass = "공급자 업종"

        '공급자 업태
        taxinvoice.invoicerBizType = "공급자 업태,업태2"

        '공급자 담당자명
        taxinvoice.invoicerContactName = "공급자 담당자명"

        '공급자 담당자 메일주소
        taxinvoice.invoicerEmail = ""

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = ""

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = ""

        ' 발행 안내 문자 전송여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송
        ' └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
        ' - 전송 시 포인트 차감되며, 전송실패시 환불처리
        taxinvoice.invoicerSMSSendYN = False

        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        ' 공급받는자 사업자번호
        ' - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
        ' - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
        ' - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
        taxinvoice.invoiceeCorpNum = "8888888888"

        '공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '공급받는자 대표자 성명
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"

        '공급받는자 주소
        taxinvoice.invoiceeAddr = "공급받는자 주소"

        '공급받는자 종목
        taxinvoice.invoiceeBizClass = "공급받는자 종목"

        '공급받는자 업태
        taxinvoice.invoiceeBizType = "공급받는자 업태"

        '공급받는자 담당자명
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"

        '공급받는자 담당자 메일주소
        '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = ""


        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = ""

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = ""


        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '세액 합계
        taxinvoice.taxTotal = "10000"

        '합계금액, 공급가액 합계 + 세액합계
        taxinvoice.totalAmount = "110000"

        '기재 상 '일련번호' 항목
        taxinvoice.serialNum = "123"

        '기재상 권 항목, 최대값 32767
        '미기재시 taxinvoice.kwon = Nothing
        taxinvoice.kwon = Nothing

        '기재상 호 항목, 최대값 32767
        '미기재시 taxinvoice.ho = Nothing
        taxinvoice.ho = Nothing

        '기재 상 '현금' 항목
        taxinvoice.cash = ""

        '기재 상 '수표' 항목
        taxinvoice.chkBill = ""

        '기재 상 '어음' 항목
        taxinvoice.note = ""

        '기재 상 '외상미수금' 항목
        taxinvoice.credit = ""

        ' 비고
        ' {invoiceeType}이 "외국인" 이면 remark1 필수
        ' - 외국인 등록번호 또는 여권번호 입력
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.businessLicenseYN = False

        ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.bankBookYN = False


        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
        '========================================================================='

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 국세청승인번호
        taxinvoice.orgNTSConfirmNum = ""


        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "20220513"                 '거래일자, yyyyMMdd
        detail.itemName = "품목명"                      '품목명
        detail.spec = "규격"                            '규격
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"                      '비고

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        '=========================================================================
        '                              추가담당자 정보
        ' - 세금계산서 발행안내 메일을 수신받을 공급받는자 담당자가 다수인 경우
        ' 담당자 정보를 추가하여 발행안내메일을 다수에게 전송할 수 있습니다.
        '=========================================================================

        taxinvoice.addContactList = New List(Of TaxinvoiceAddContact)

        Dim addContact As TaxinvoiceAddContact = New TaxinvoiceAddContact

        addContact.serialNum = 1                        '일련번호, 1부터 순차기재
        addContact.contactName = "추가담당자명"         '담당자 성명
        addContact.email = ""         '담당자 메일주소

        taxinvoice.addContactList.Add(addContact)


        ' 지연발행 강제여부  (true / false 중 택 1)
        ' └ true = 가능 , false = 불가능
        ' - 미입력 시 기본값 false 처리
        ' - 발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
        ' - 가산세가 부과되더라도 발행을 해야하는 경우에는 forceIssue의 값을
        '   true로 선언하여 발행(Issue API)를 호출하시면 됩니다.
        Dim forceIssue As Boolean = False

        '발행시 메모
        Dim memo As String = "즉시발행 메모"

        ' 거래명세서 동시작성여부 (true / false 중 택 1)
        ' └ true = 사용 , false = 미사용
        ' - 미입력 시 기본값 false 처리
        Dim writeSpecification As Boolean = False

        ' {writeSpecification} = true인 경우, 거래명세서 문서번호 할당
        ' - 미입력시 기본값 세금계산서 문서번호와 동일하게 할당
        Dim dealInvoiceMgtKey As String = ""

        ' 발행안내메일 제목, 미기재시 기본양식으로 전송
        Dim emailSubject As String = ""

        Try
            Dim response As IssueResponse = taxinvoiceService.RegistIssue(txtCorpNum.Text, taxinvoice, forceIssue, memo, writeSpecification, dealInvoiceMgtKey, emailSubject)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message + vbCrLf + "ntsConfirmNum(국세청승인번호) : " + response.ntsConfirmNum)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 최대 100건의 세금계산서 발행을 한번의 요청으로 접수합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#BulkSubmit
    '=========================================================================
    Private Sub btnBulkSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnBulkSubmit.Click
        ' 세금계산서 객체정보 목록
        Dim taxinvoiceList As List(Of Taxinvoice) = New List(Of Taxinvoice)

        ' 지연발행 강제여부  (true / false 중 택 1)
        ' └ true = 가능 , false = 불가능
        ' - 미입력 시 기본값 false 처리
        ' - 발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
        ' - 가산세가 부과되더라도 발행을 해야하는 경우에는 forceIssue의 값을
        '   true로 선언하여 발행(Issue API)를 호출하시면 됩니다.
        Dim forceIssue As Boolean = False

        For i = 0 To 99
            Dim taxinvoice As Taxinvoice = New Taxinvoice

            '작성일자, 표시형식 (yyyyMMdd)
            taxinvoice.writeDate = "20220513"

            '발행형태, [정발행, 역발행, 위수탁] 중 기재
            taxinvoice.issueType = "정발행"

            ' 과금방향, {정과금, 역과금} 중 기재
            ' └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
            ' -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
            taxinvoice.chargeDirection = "정과금"

            '영수/청구, [영수, 청구, 없음] 중 기재
            taxinvoice.purposeType = "영수"

            '과세형태, [과세, 영세, 면세] 중 기재
            taxinvoice.taxType = "과세"

            '=========================================================================
            '                              공급자 정보
            '=========================================================================

            '공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = txtCorpNum.Text

            '공급자 종사업장 식별번호. 필요시 숫자 4자리 기재
            taxinvoice.invoicerTaxRegID = ""

            '공급자 상호
            taxinvoice.invoicerCorpName = "공급자 "

            '공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            taxinvoice.invoicerMgtKey = txtSubmitID.Text + i.ToString

            '공급자 대표자 성명
            taxinvoice.invoicerCEOName = "공급자 대표자 성명"

            '공급자 주소
            taxinvoice.invoicerAddr = "공급자 주소"

            '공급자 종목
            taxinvoice.invoicerBizClass = "공급자 업종"

            '공급자 업태
            taxinvoice.invoicerBizType = "공급자 업태,업태2"

            '공급자 담당자명
            taxinvoice.invoicerContactName = "공급자 담당자명"

            '공급자 담당자 메일주소
            taxinvoice.invoicerEmail = ""

            '공급자 담당자 연락처
            taxinvoice.invoicerTEL = ""

            '공급자 담당자 휴대폰번호
            taxinvoice.invoicerHP = ""

            ' 발행 안내 문자 전송여부 (true / false 중 택 1)
            ' └ true = 전송 , false = 미전송
            ' └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
            ' - 전송 시 포인트 차감되며, 전송실패시 환불처리
            taxinvoice.invoicerSMSSendYN = False

            '=========================================================================
            '                            공급받는자 정보
            '=========================================================================

            '공급받는자 구분, [사업자, 개인, 외국인] 중 기재
            taxinvoice.invoiceeType = "사업자"

            ' 공급받는자 사업자번호
            ' - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
            ' - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
            ' - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
            taxinvoice.invoiceeCorpNum = "8888888888"

            '공급자받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호"

            '공급받는자 대표자 성명
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"

            '공급받는자 주소
            taxinvoice.invoiceeAddr = "공급받는자 주소"

            '공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목"

            '공급받는자 업태
            taxinvoice.invoiceeBizType = "공급받는자 업태"

            '공급받는자 담당자명
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"

            '공급받는자 담당자 메일주소
            '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
            '실제 거래처의 메일주소가 기재되지 않도록 주의
            taxinvoice.invoiceeEmail1 = ""

            '공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = ""

            '공급받는자 담당자 휴대폰번호
            taxinvoice.invoiceeHP1 = ""


            '=========================================================================
            '                            세금계산서 정보
            '=========================================================================

            '공급가액 합계
            taxinvoice.supplyCostTotal = "100000"

            '세액 합계
            taxinvoice.taxTotal = "10000"

            '합계금액, 공급가액 합계 + 세액합계
            taxinvoice.totalAmount = "110000"

            '기재 상 '일련번호' 항목
            taxinvoice.serialNum = "123"

            '기재상 권 항목, 최대값 32767
            '미기재시 taxinvoice.kwon = Nothing
            taxinvoice.kwon = Nothing

            '기재상 호 항목, 최대값 32767
            '미기재시 taxinvoice.ho = Nothing
            taxinvoice.ho = Nothing

            '기재 상 '현금' 항목
            taxinvoice.cash = ""

            '기재 상 '수표' 항목
            taxinvoice.chkBill = ""

            '기재 상 '어음' 항목
            taxinvoice.note = ""

            '기재 상 '외상미수금' 항목
            taxinvoice.credit = ""

            ' 비고
            ' {invoiceeType}이 "외국인" 이면 remark1 필수
            ' - 외국인 등록번호 또는 여권번호 입력
            taxinvoice.remark1 = "비고1"
            taxinvoice.remark2 = "비고2"
            taxinvoice.remark3 = "비고3"

            ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
            ' └ true = 첨부 , false = 미첨부(기본값)
            ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.businessLicenseYN = False

            ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
            ' └ true = 첨부 , false = 미첨부(기본값)
            ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.bankBookYN = False


            '=========================================================================
            '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
            ' - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
            '========================================================================='

            ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
            taxinvoice.modifyCode = Nothing

            ' 원본세금계산서의 국세청승인번호
            taxinvoice.orgNTSConfirmNum = ""


            '=========================================================================
            '                            상세항목(품목) 정보
            '=========================================================================

            taxinvoice.detailList = New List(Of TaxinvoiceDetail)

            Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

            detail.serialNum = 1                            '일련번호, 1부터 순차기재
            detail.purchaseDT = "20220513"                 '거래일자, yyyyMMdd
            detail.itemName = "품목명"                      '품목명
            detail.spec = "규격"                            '규격
            detail.qty = "1"                                '수량
            detail.unitCost = "100000"                      '단가
            detail.supplyCost = "100000"                    '공급가액
            detail.tax = "10000"                            '세액
            detail.remark = "품목비고"                      '비고

            taxinvoice.detailList.Add(detail)

            detail = New TaxinvoiceDetail

            detail.serialNum = 2
            detail.itemName = "품목명"

            taxinvoice.detailList.Add(detail)

            '=========================================================================
            '                              추가담당자 정보
            ' - 세금계산서 발행안내 메일을 수신받을 공급받는자 담당자가 다수인 경우
            ' 담당자 정보를 추가하여 발행안내메일을 다수에게 전송할 수 있습니다.
            '=========================================================================

            taxinvoice.addContactList = New List(Of TaxinvoiceAddContact)

            Dim addContact As TaxinvoiceAddContact = New TaxinvoiceAddContact

            addContact.serialNum = 1                        '일련번호, 1부터 순차기재
            addContact.contactName = "추가담당자명"         '담당자 성명
            addContact.email = ""         '담당자 메일주소

            taxinvoice.addContactList.Add(addContact)

            taxinvoiceList.Add(taxinvoice)
        Next

        Try
            Dim response As BulkResponse = taxinvoiceService.BulkSubmit(txtCorpNum.Text, txtSubmitID.Text, taxinvoiceList, forceIssue)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message + vbCrLf + "receiptID(접수아이디) : " + response.receiptID)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 접수시 기재한 SubmitID를 사용하여 세금계산서 접수결과를 확인합니다.
    ' - 개별 세금계산서 처리상태는 접수상태(txState)가 완료(2) 시 반환됩니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#GetBulkResult
    '=========================================================================
    Private Sub btnGetBulkResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetBulkResult.Click
        Try
            Dim result As BulkTaxinvoiceResult = taxinvoiceService.GetBulkResult(txtCorpNum.Text, txtSubmitID.Text)

            Dim tmp As String = ""

            tmp += "code(응답 코드) : " + result.code.ToString + vbCrLf
            tmp += "message(응답메시지) : " + result.message + vbCrLf
            tmp += "submitID(제출아이디) : " + result.submitID + vbCrLf
            tmp += "submitCount(세금계산서 접수 건수) : " + result.submitCount.ToString + vbCrLf
            tmp += "successCount(세금계산서 발행 성공 건수) : " + result.successCount.ToString + vbCrLf
            tmp += "failCount(세금계산서 발행 실패 건수) : " + result.failCount.ToString + vbCrLf
            tmp += "txState(접수상태코드) : " + result.txState.ToString + vbCrLf
            tmp += "txResultCode(접수 결과코드) : " + result.txResultCode.ToString + vbCrLf
            tmp += "txStartDT(발행처리 시작일시) : " + result.txStartDT + vbCrLf
            tmp += "txEndDT(발행처리 완료일시) : " + result.txEndDT + vbCrLf
            tmp += "receiptDT(접수일시) : " + result.receiptDT + vbCrLf
            tmp += "receiptID(접수아이디) : " + result.receiptID + vbCrLf

            If Not result.issueResult Is Nothing Then
                Dim i As Integer = 1
                For Each issueResult As BulkTaxinvoiceIssueResult In result.issueResult
                    tmp += "===========발행결과[" + i.ToString + "/" + result.issueResult.Count.ToString + "]===========" + vbCrLf
                    tmp += "invoicerMgtKey(공급자 문서번호) : " + issueResult.invoicerMgtKey + vbCrLf
                    tmp += "code(응답코드) : " + issueResult.code.ToString + vbCrLf
                    tmp += "message(응답메시지) : " + issueResult.message + vbCrLf
                    tmp += "ntsconfirmNum(국세청승인번호) : " + issueResult.ntsconfirmNum + vbCrLf
                    tmp += "issueDT(발행일시) : " + issueResult.issueDT + vbCrLf
                    i = i + 1
                Next
            End If

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 작성된 정발행 세금계산서 데이터를 팝빌에 저장합니다.
    ' - "임시저장" Issue(상태의 세금계산서는 발행) 함수를 호출하여 "발행완료" 처리한 경우에만 국세청으로 전송됩니다.
    ' - 정발행 시 임시저장(Register)과 발행(Issue)을 한번의 호출로 처리하는 즉시발행(RegistIssue API) 프로세스 연동을 권장합니다.
    ' - 세금계산서 파일첨부 기능을 구현하는 경우, 임시저장(Register API) -> 파일첨부(AttachFile API) -> 발행(Issue API) 함수를 차례로 호출합니다.
    ' - 임시저장된 세금계산서는 팝빌 사이트 '임시문서함'에서 확인 가능합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Register
    '=========================================================================
    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '작성일자, 표시형식 (yyyyMMdd)
        taxinvoice.writeDate = "20220513"

        '발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "정발행"

        ' 과금방향, {정과금, 역과금} 중 기재
        ' └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
        ' -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
        taxinvoice.chargeDirection = "정과금"

        '영수/청구, [영수, 청구, 없음] 중 기재
        taxinvoice.purposeType = "영수"

        '과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"


        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = txtCorpNum.Text

        '공급자 종사업장 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = txtMgtKey.Text

        '공급자 대표자 성명
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"

        '공급자 주소
        taxinvoice.invoicerAddr = "공급자 주소"

        '공급자 종목
        taxinvoice.invoicerBizClass = "공급자 업종"

        '공급자 업태
        taxinvoice.invoicerBizType = "공급자 업태,업태2"

        '공급자 담당자명
        taxinvoice.invoicerContactName = "공급자 담당자명"

        '공급자 담당자 메일주소
        taxinvoice.invoicerEmail = ""

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = ""

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = ""

        ' 발행 안내 문자 전송여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송
        ' └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
        ' - 전송 시 포인트 차감되며, 전송실패시 환불처리
        taxinvoice.invoicerSMSSendYN = False


        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        ' 공급받는자 사업자번호
        ' - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
        ' - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
        ' - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
        taxinvoice.invoiceeCorpNum = "8888888888"

        '공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '공급받는자 대표자 성명
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"

        '공급받는자 주소
        taxinvoice.invoiceeAddr = "공급받는자 주소"

        '공급받는자 종목
        taxinvoice.invoiceeBizClass = "공급받는자 종목"

        '공급받는자 업태
        taxinvoice.invoiceeBizType = "공급받는자 업태"

        '공급받는자 담당자명
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"

        '공급받는자 담당자 메일주소
        '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = ""

        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = ""

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = ""


        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '세액 합계
        taxinvoice.taxTotal = "10000"

        '합계금액, 공급가액 합계 + 세액합계
        taxinvoice.totalAmount = "110000"

        '기재 상 '일련번호' 항목
        taxinvoice.serialNum = "123"

        '기재상 권 항목, 최대값 32767
        '미기재시 taxinvoice.kwon = Nothing
        taxinvoice.kwon = Nothing

        '기재상 호 항목, 최대값 32767
        '미기재시 taxinvoice.ho = Nothing
        taxinvoice.ho = Nothing

        '기재 상 '현금' 항목
        taxinvoice.cash = ""

        '기재 상 '수표' 항목
        taxinvoice.chkBill = ""

        '기재 상 '어음' 항목
        taxinvoice.note = ""

        '기재 상 '외상미수금' 항목
        taxinvoice.credit = ""
        ' 비고
        ' {invoiceeType}이 "외국인" 이면 remark1 필수
        ' - 외국인 등록번호 또는 여권번호 입력
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.businessLicenseYN = False

        ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.bankBookYN = False


        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
        '========================================================================='

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 국세청승인번호
        taxinvoice.orgNTSConfirmNum = ""

        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "20220513"                  '거래일자, yyyyMMdd
        detail.itemName = "품목명"                      '품목명
        detail.spec = "규격"                            '규격
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"                      '비고

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        '=========================================================================
        '                              추가담당자 정보
        ' - 세금계산서 발행안내 메일을 수신받을 공급받는자 담당자가 다수인 경우
        ' 담당자 정보를 추가하여 발행안내메일을 다수에게 전송할 수 있습니다.
        '=========================================================================

        taxinvoice.addContactList = New List(Of TaxinvoiceAddContact)

        Dim addContact As TaxinvoiceAddContact = New TaxinvoiceAddContact

        addContact.serialNum = 1                        '일련번호, 1부터 순차기재
        addContact.contactName = "추가담당자명"         '담당자 성명
        addContact.email = ""         '담당자 메일주소

        taxinvoice.addContactList.Add(addContact)

        '전자거래명세서 동시작성 여부
        Dim writeSpecification As Boolean = False

        Try
            Dim response As Response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text, writeSpecification)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 작성된 역발행 세금계산서 데이터를 팝빌에 저장합니다.
    ' - "임시저장"request( 상태의 세금계산서는 역발행 요청) 함수를 호출하여 "(역)발행대기" 상태가 되며
    'issue(   공급자가 팝빌 사이트 또는 발행) 함수를 호출하여 발행한 경우에만 국세청으로 전송됩니다.
    ' -register( 역발행 시 임시저장)request(과 역발행요청)을 한번의 호출로 처리하는 즉시요청(RegistRequest API) 프로세스 연동을 권장합니다.
    ' - 세금계산서 파일첨부 기능을 구현하는 경우, 임시저장(Register API) -> 파일첨부(AttachFile API) -> 역발행 요청(Request API) 함수를 차례로 호출합니다.
    ' - 역발행 세금계산서를 저장하는 경우, 객체 'Taxinvoice'의 변수 'chargeDirection' 값을 통해 과금 주체를 지정할 수 있습니다.
    '   └ 정과금 : 공급자 과금 , 역과금 : 공급받는자 과금
    ' - 임시저장된 세금계산서는 팝빌 사이트 '임시문서함'에서 확인 가능합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Register
    '=========================================================================
    Private Sub btnRegister_Reverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRegister_Reverse.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '작성일자,yyyyMMdd( 표시형식 )
        taxinvoice.writeDate = "20220513"

        '발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "역발행"

        ' 과금방향, {정과금, 역과금} 중 기재
        ' └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
        ' -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
        taxinvoice.chargeDirection = "정과금"

        '영수/청구, [영수, 청구, 없음] 중 기재
        taxinvoice.purposeType = "영수"

        '과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"


        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = "8888888888"

        '공급자 종사업장 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = ""

        '공급자 대표자 성명
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"

        '공급자 주소
        taxinvoice.invoicerAddr = "공급자 주소"

        '공급자 종목
        taxinvoice.invoicerBizClass = "공급자 업종"

        '공급자 업태
        taxinvoice.invoicerBizType = "공급자 업태,업태2"

        '공급자 담당자명
        taxinvoice.invoicerContactName = "공급자 담당자명"

        '공급자 담당자 메일주소
        taxinvoice.invoicerEmail = ""

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = ""

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = ""

        ' 발행 안내 문자 전송여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송
        ' └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
        ' - 전송 시 포인트 차감되며, 전송실패시 환불처리
        taxinvoice.invoicerSMSSendYN = False


        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        '공급받는자 사업자번호, '-' 제외 10자리
        taxinvoice.invoiceeCorpNum = txtCorpNum.Text

        '공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '[역발행시 필수] 공급받는자 문서번호(역발행시 필수), 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        taxinvoice.invoiceeMgtKey = txtMgtKey.Text

        '공급받는자 대표자 성명
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"

        '공급받는자 주소
        taxinvoice.invoiceeAddr = "공급받는자 주소"

        '공급받는자 종목
        taxinvoice.invoiceeBizClass = "공급받는자 종목"

        '공급받는자 업태
        taxinvoice.invoiceeBizType = "공급받는자 업태"

        '공급받는자 담당자명
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"

        '공급받는자 담당자 메일주소
        '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = ""

        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = ""

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = ""

        ' 역발행 안내 문자 전송여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송
        ' └ 공급자 담당자 휴대폰번호 {invoicerHP} 값으로 문자 전송
        ' - 전송 시 포인트 차감되며, 전송실패시 환불처리
        taxinvoice.invoiceeSMSSendYN = False

        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '세액 합계
        taxinvoice.taxTotal = "10000"

        '합계금액, 공급가액 합계 + 세액합계
        taxinvoice.totalAmount = "110000"

        '기재 상 '일련번호' 항목
        taxinvoice.serialNum = "123"

        '기재상 권 항목, 최대값 32767
        '미기재시 taxinvoice.kwon = Nothing
        taxinvoice.kwon = Nothing

        '기재상 호 항목, 최대값 32767
        '미기재시 taxinvoice.ho = Nothing
        taxinvoice.ho = Nothing

        '기재 상 '현금' 항목
        taxinvoice.cash = ""

        '기재 상 '수표' 항목
        taxinvoice.chkBill = ""

        '기재 상 '어음' 항목
        taxinvoice.note = ""

        '기재 상 '외상미수금' 항목
        taxinvoice.credit = ""

        ' 비고
        ' {invoiceeType}이 "외국인" 이면 remark1 필수
        ' - 외국인 등록번호 또는 여권번호 입력
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.businessLicenseYN = False

        ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.bankBookYN = False


        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
        '=========================================================================

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 국세청승인번호
        taxinvoice.orgNTSConfirmNum = ""


        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "20220513"                 '거래일자, yyyyMMdd
        detail.itemName = "품목명"                      '품목명
        detail.spec = "규격"                            '규격
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"                      '비고

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        Try
            Dim response As Response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 정발행 세금계산서를 수정합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Update
    '=========================================================================
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '작성일자,yyyyMMdd( 표시형식 )
        taxinvoice.writeDate = "20220513"

        '발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "정발행"

        ' 과금방향, {정과금, 역과금} 중 기재
        ' └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
        ' -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
        taxinvoice.chargeDirection = "정과금"

        '영수/청구, [영수, 청구] 중 기재
        taxinvoice.purposeType = "영수"

        '과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"

        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = txtCorpNum.Text

        '공급자 종사업장 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = txtMgtKey.Text

        '공급자 대표자 성명
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"

        '공급자 주소
        taxinvoice.invoicerAddr = "공급자 주소"

        '공급자 종목
        taxinvoice.invoicerBizClass = "공급자 업종"

        '공급자 업태
        taxinvoice.invoicerBizType = "공급자 업태,업태2"

        '공급자 담당자명
        taxinvoice.invoicerContactName = "공급자 담당자명"

        '공급자 담당자 메일주소
        taxinvoice.invoicerEmail = ""

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = ""

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = ""

        ' 발행 안내 문자 전송여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송
        ' └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
        ' - 전송 시 포인트 차감되며, 전송실패시 환불처리
        taxinvoice.invoicerSMSSendYN = False

        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        ' 공급받는자 사업자번호
        ' - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
        ' - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
        ' - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
        taxinvoice.invoiceeCorpNum = "8888888888"

        '공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '공급받는자 대표자 성명
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"

        '공급받는자 주소
        taxinvoice.invoiceeAddr = "공급받는자 주소"

        '공급받는자 종목
        taxinvoice.invoiceeBizClass = "공급받는자 종목"

        '공급받는자 업태
        taxinvoice.invoiceeBizType = "공급받는자 업태"

        '공급받는자 담당자명
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"

        '공급받는자 담당자 메일주소
        '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = ""

        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = ""

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = ""

        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '세액 합계
        taxinvoice.taxTotal = "10000"

        '합계금액, 공급가액 합계 + 세액합계
        taxinvoice.totalAmount = "110000"

        '기재 상 '일련번호' 항목
        taxinvoice.serialNum = "123"

        '기재상 권 항목, 최대값 32767
        '미기재시 taxinvoice.kwon = Nothing
        taxinvoice.kwon = Nothing

        '기재상 호 항목, 최대값 32767
        '미기재시 taxinvoice.ho = Nothing
        taxinvoice.ho = Nothing

        '기재 상 '현금' 항목
        taxinvoice.cash = ""

        '기재 상 '수표' 항목
        taxinvoice.chkBill = ""

        '기재 상 '어음' 항목
        taxinvoice.note = ""

        '기재 상 '외상미수금' 항목
        taxinvoice.credit = ""

        ' 비고
        ' {invoiceeType}이 "외국인" 이면 remark1 필수
        ' - 외국인 등록번호 또는 여권번호 입력
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.businessLicenseYN = False

        ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.bankBookYN = False


        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
        '========================================================================='

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 국세청승인번호
        taxinvoice.orgNTSConfirmNum = ""

        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "20220513"                 '거래일자, yyyyMMdd
        detail.itemName = "품목명"                      '품목명
        detail.spec = "규격"                            '규격
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"                      '비고

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        '=========================================================================
        '                              추가담당자 정보
        ' - 세금계산서 발행안내 메일을 수신받을 공급받는자 담당자가 다수인 경우
        ' 담당자 정보를 추가하여 발행안내메일을 다수에게 전송할 수 있습니다.
        '=========================================================================

        taxinvoice.addContactList = New List(Of TaxinvoiceAddContact)

        Dim addContact As TaxinvoiceAddContact = New TaxinvoiceAddContact

        addContact.serialNum = 1                        '일련번호, 1부터 순차기재
        addContact.contactName = "추가담당자명"         '담당자 성명
        addContact.email = ""         '담당자 메일주소

        taxinvoice.addContactList.Add(addContact)


        Try
            Dim response As Response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' "임시저장" 상태의 역발행 세금계산서를 수정합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Update
    '=========================================================================
    Private Sub btnUpdate_Reverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUpdate_Reverse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '작성일자, 표시형식 (yyyyMMdd)
        taxinvoice.writeDate = "20220513"

        '발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "역발행"

        ' 과금방향, {정과금, 역과금} 중 기재
        ' └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
        ' -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
        taxinvoice.chargeDirection = "정과금"

        '영수/청구, [영수, 청구, 없음] 중 기재
        taxinvoice.purposeType = "영수"

        '과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"

        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = "8888888888"

        '공급자 종사업장 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = txtMgtKey.Text

        '공급자 대표자 성명
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"

        '공급자 주소
        taxinvoice.invoicerAddr = "공급자 주소"

        '공급자 종목
        taxinvoice.invoicerBizClass = "공급자 업종"

        '공급자 업태
        taxinvoice.invoicerBizType = "공급자 업태,업태2"

        '공급자 담당자명
        taxinvoice.invoicerContactName = "공급자 담당자명"

        '공급자 담당자 메일주소
        taxinvoice.invoicerEmail = ""

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = ""

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = ""

        ' 발행 안내 문자 전송여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송
        ' └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
        ' - 전송 시 포인트 차감되며, 전송실패시 환불처리
        taxinvoice.invoicerSMSSendYN = False


        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        ' 공급받는자 사업자번호
        ' - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
        ' - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
        ' - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
        taxinvoice.invoiceeCorpNum = txtCorpNum.Text

        '공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '[역발행시 필수] 공급받는자 문서번호(역발행시 필수), 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        taxinvoice.invoiceeMgtKey = ""

        '공급받는자 대표자 성명
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"

        '공급받는자 주소
        taxinvoice.invoiceeAddr = "공급받는자 주소"

        '공급받는자 종목
        taxinvoice.invoiceeBizClass = "공급받는자 종목"

        '공급받는자 업태
        taxinvoice.invoiceeBizType = "공급받는자 업태"

        '공급받는자 담당자명
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"

        '공급받는자 담당자 메일주소
        '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = ""

        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = ""

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = ""

        ' 역발행 안내 문자 전송여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송
        ' └ 공급자 담당자 휴대폰번호 {invoicerHP} 값으로 문자 전송
        ' - 전송 시 포인트 차감되며, 전송실패시 환불처리
        taxinvoice.invoiceeSMSSendYN = False

        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '세액 합계
        taxinvoice.taxTotal = "10000"

        '합계금액, 공급가액 합계 + 세액합계
        taxinvoice.totalAmount = "110000"

        '기재 상 '일련번호' 항목
        taxinvoice.serialNum = "123"

        '기재상 권 항목, 최대값 32767
        '미기재시 taxinvoice.kwon = Nothing
        taxinvoice.kwon = Nothing

        '기재상 호 항목, 최대값 32767
        '미기재시 taxinvoice.ho = Nothing
        taxinvoice.ho = Nothing

        '기재 상 '현금' 항목
        taxinvoice.cash = ""

        '기재 상 '수표' 항목
        taxinvoice.chkBill = ""

        '기재 상 '어음' 항목
        taxinvoice.note = ""

        '기재 상 '외상미수금' 항목
        taxinvoice.credit = ""

        ' 비고
        ' {invoiceeType}이 "외국인" 이면 remark1 필수
        ' - 외국인 등록번호 또는 여권번호 입력
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.businessLicenseYN = False

        ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.bankBookYN = False

        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
        '=========================================================================

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 국세청승인번호
        taxinvoice.orgNTSConfirmNum = ""

        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "20220513"                 '거래일자, yyyyMMdd
        detail.itemName = "품목명"                      '품목명
        detail.spec = "규격"                            '규격
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"                      '비고

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        Try
            Dim response As Response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 세금계산서를 발행(전자서명)하며, "발행완료" 상태로 처리합니다.
    ' - 세금계산서 국세청 전송정책 [https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/policy-of-send-to-nts]
    ' - "발행완료" 된 전자세금계산서는 국세청 전송 이전에 발행취소(CancelIssue API) 함수로 국세청 신고 대상에서 제외할 수 있습니다.
    ' - 세금계산서 발행을 위해서 공급자의 인증서가 팝빌 인증서버에 사전등록 되어야 합니다.
    '   └ 위수탁발행의 경우, 수탁자의 인증서 등록이 필요합니다.
    ' - 세금계산서 발행 시 공급받는자에게 발행 메일이 발송됩니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Issue
    '=========================================================================
    Private Sub btnIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnIssue.Click, btnIssue_Reverse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행 메모
        Dim memo As String = "발행메모"

        ' 지연발행 강제여부  (true / false 중 택 1)
        ' └ true = 가능 , false = 불가능
        ' - 미입력 시 기본값 false 처리
        ' - 발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
        ' - 가산세가 부과되더라도 발행을 해야하는 경우에는 forceIssue의 값을
        '   true로 선언하여 발행(Issue API)를 호출하시면 됩니다.
        Dim forceIssue As Boolean = False

        Try
            Dim response As IssueResponse = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, forceIssue)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message + vbCrLf + "ntsConfirmNum(국세청승인번호) : " + response.ntsConfirmNum)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' "(역)발행대기" 상태의 세금계산서를 발행(전자서명)하며, "발행완료" 상태로 처리합니다.
    ' - 세금계산서 국세청 전송정책 [https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/policy-of-send-to-nts]
    ' - "발행완료" 된 전자세금계산서는 국세청 전송 이전에 발행취소(CancelIssue API) 함수로 국세청 신고 대상에서 제외할 수 있습니다.
    ' - 세금계산서 발행을 위해서 공급자의 인증서가 팝빌 인증서버에 사전등록 되어야 합니다.
    '   └ 위수탁발행의 경우, 수탁자의 인증서 등록이 필요합니다.
    ' - 세금계산서 발행 시 공급받는자에게 발행 메일이 발송됩니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Issue
    '=========================================================================
    Private Sub btnIssue_Reverse_sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnIssue_Reverse_sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행 메모
        Dim memo As String = "발행메모"

        ' 지연발행 강제여부  (true / false 중 택 1)
        ' └ true = 가능 , false = 불가능
        ' - 미입력 시 기본값 false 처리
        ' - 발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
        ' - 가산세가 부과되더라도 발행을 해야하는 경우에는 forceIssue의 값을
        '   true로 선언하여 발행(Issue API)를 호출하시면 됩니다.
        Dim forceIssue As Boolean = False

        Try
            Dim response As IssueResponse = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, forceIssue)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message + vbCrLf + "ntsConfirmNum(국세청승인번호) : " + response.ntsConfirmNum)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 국세청 전송 이전 "발행완료" 상태의 전자세금계산서를 "발행취소"하고 국세청 신고대상에서 제외합니다.
    ' - Delete(삭제)함수를 호출하여 "발행취소" 상태의 전자세금계산서를 삭제하면, 문서번호 재사용이 가능합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#CancelIssue
    '=========================================================================
    Private Sub btnCancelIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelIssue.Click, btnCancelIssue_Reverse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행취소 메모
        Dim memo As String = "발행취소메모"

        Try
            Dim response As Response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 국세청 전송 이전 "발행완료" 상태의 전자세금계산서를 "발행취소"하고, 해당 건은 국세청 신고 대상에서 제외됩니다.
    ' - Delete(삭제)함수를 호출하여 "발행취소" 상태의 전자세금계산서를 삭제하면, 문서번호 재사용이 가능합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#CancelIssue
    '=========================================================================
    Private Sub btnCancelIssue_Sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelIssue_Sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행취소 메모
        Dim memo As String = "발행취소메모"

        Try
            Dim response As Response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 국세청 전송 이전 "발행완료" 상태의 전자세금계산서를 "발행취소"하고, 해당 건은 국세청 신고 대상에서 제외됩니다.
    ' - Delete(삭제)함수를 호출하여 "발행취소" 상태의 전자세금계산서를 삭제하면, 문서번호 재사용이 가능합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#CancelIssue
    '=========================================================================
    Private Sub btnCancelIssue_Reverse_sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelIssue_Reverse_sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행취소 메모
        Dim memo As String = "발행취소메모"

        Try
            Dim response As Response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 공급받는자가 작성한 세금계산서 데이터를 팝빌에 저장하고 공급자에게 송부하여 발행을 요청합니다.
    ' - 역발행 세금계산서 프로세스를 구현하기 위해서는 공급자/공급받는자가 모두 팝빌에 회원이여야 합니다.
    ' - 발행 요청된 세금계산서는 "(역)발행대기" 상태이며, 공급자가 팝빌 사이트 또는 함수를 호출하여 발행한 경우에만 국세청으로 전송됩니다.
    ' - 공급자는 팝빌 사이트의 "매출 발행 대기함"에서 발행대기 상태의 역발행 세금계산서를 확인할 수 있습니다.
    ' - 임시저장(Register API) 함수와 역발행 요청(Request API) 함수를 한 번의 프로세스로 처리합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#RegistRequest
    '=========================================================================
    Private Sub btnRegistRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRegistRequest.Click

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '작성일자, 표시형식 (yyyyMMdd)
        taxinvoice.writeDate = "20220513"

        '발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "역발행"

        ' 과금방향, {정과금, 역과금} 중 기재
        ' └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
        ' -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
        taxinvoice.chargeDirection = "정과금"

        '영수/청구, [영수, 청구, 없음] 중 기재
        taxinvoice.purposeType = "영수"

        '과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"


        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = "8888888888"

        '공급자 종사업장 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = ""

        '공급자 대표자 성명
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"

        '공급자 주소
        taxinvoice.invoicerAddr = "공급자 주소"

        '공급자 종목
        taxinvoice.invoicerBizClass = "공급자 업종"

        '공급자 업태
        taxinvoice.invoicerBizType = "공급자 업태,업태2"

        '공급자 담당자명
        taxinvoice.invoicerContactName = "공급자 담당자명"

        '공급자 담당자 메일주소
        taxinvoice.invoicerEmail = ""

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = ""

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = ""

        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        ' 공급받는자 사업자번호
        ' - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
        ' - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
        ' - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
        taxinvoice.invoiceeCorpNum = txtCorpNum.Text

        '공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '[역발행시 필수] 공급받는자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        taxinvoice.invoiceeMgtKey = txtMgtKey.Text

        '공급받는자 대표자 성명
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"

        '공급받는자 주소
        taxinvoice.invoiceeAddr = "공급받는자 주소"

        '공급받는자 종목
        taxinvoice.invoiceeBizClass = "공급받는자 종목"

        '공급받는자 업태
        taxinvoice.invoiceeBizType = "공급받는자 업태"

        '공급받는자 담당자명
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"

        '공급받는자 담당자 메일주소
        '팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = ""

        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = ""

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = ""

        ' 역발행 안내 문자 전송여부 (true / false 중 택 1)
        ' └ true = 전송 , false = 미전송
        ' └ 공급자 담당자 휴대폰번호 {invoicerHP} 값으로 문자 전송
        ' - 전송 시 포인트 차감되며, 전송실패시 환불처리
        taxinvoice.invoiceeSMSSendYN = False


        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '세액 합계
        taxinvoice.taxTotal = "10000"

        '합계금액, 공급가액 합계 + 세액합계
        taxinvoice.totalAmount = "110000"

        '기재 상 '일련번호' 항목
        taxinvoice.serialNum = ""

        '기재상 권 항목, 최대값 32767
        '미기재시 taxinvoice.kwon = Nothing
        taxinvoice.kwon = Nothing

        '기재상 호 항목, 최대값 32767
        '미기재시 taxinvoice.ho = Nothing
        taxinvoice.ho = Nothing

        '기재 상 '현금' 항목
        taxinvoice.cash = ""

        '기재 상 '수표' 항목
        taxinvoice.chkBill = ""

        '기재 상 '어음' 항목
        taxinvoice.note = ""

        '기재 상 '외상미수금' 항목
        taxinvoice.credit = ""

        ' 비고
        ' {invoiceeType}이 "외국인" 이면 remark1 필수
        ' - 외국인 등록번호 또는 여권번호 입력
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        ' 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.businessLicenseYN = False

        ' 통장사본 이미지 첨부여부 (true / false 중 택 1)
        ' └ true = 첨부 , false = 미첨부(기본값)
        ' - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
        taxinvoice.bankBookYN = False

        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
        '=========================================================================

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 국세청승인번호
        taxinvoice.orgNTSConfirmNum = ""

        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "20220513"                  '거래일자, yyyyMMdd
        detail.itemName = "품목명"                      '품목명
        detail.spec = "규격"                            '규격
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"                      '비고

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)


        '즉시요청 메모
        Dim Memo As String = "즉시요청 메모"

        Try
            Dim response As Response = taxinvoiceService.RegistRequest(txtCorpNum.Text, taxinvoice, Memo)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 공급받는자가 저장된 역발행 세금계산서를 공급자에게 송부하여 발행 요청합니다.
    ' - 역발행 세금계산서 프로세스를 구현하기 위해서는 공급자/공급받는자가 모두 팝빌에 회원이여야 합니다.
    ' - 역발행 요청된 세금계산서는 "(역)발행대기" 상태이며, 공급자가 팝빌 사이트 또는 함수를 호출하여 발행한 경우에만 국세청으로 전송됩니다.
    ' - 공급자는 팝빌 사이트의 "매출 발행 대기함"에서 발행대기 상태의 역발행 세금계산서를 확인할 수 있습니다.
    ' - 역발행 요청시 공급자에게 역발행 요청 메일이 발송됩니다.
    ' - 공급자가 역발행 세금계산서 발행시 포인트가 과금됩니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Request
    '=========================================================================
    Private Sub btnRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequest.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '역발행 요청 메모
        Dim Memo As String = "역발행 요청 메모"

        Try
            Dim response As Response = taxinvoiceService.Request(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 공급자가 요청받은 역발행 세금계산서를 발행하기 전, 공급받는자가 역발행요청을 취소합니다.
    ' - 함수 호출시 상태 값이 "취소"로 변경되고, 해당 역발행 세금계산서는 공급자에 의해 발행 될 수 없습니다.
    ' - [취소]한 세금계산서의 문서번호를 재사용하기 위해서는 삭제 (Delete API)를 호출해야 합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#CancelRequest
    '=========================================================================
    Private Sub btnCancelRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelRequest.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '역발행 요청 취소 메모
        Dim Memo As String = "역발행 요청 취소 메모"

        Try
            Dim response As Response = taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 공급자가 요청받은 역발행 세금계산서를 발행하기 전, 공급받는자가 역발행요청을 취소합니다.
    ' - 함수 호출시 상태 값이 "취소"로 변경되고, 해당 역발행 세금계산서는 공급자에 의해 발행 될 수 없습니다.
    ' - [취소]한 세금계산서의 문서번호를 재사용하기 위해서는 삭제 (Delete API)를 호출해야 합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#CancelRequest
    '=========================================================================
    Private Sub btnCancelRequest_sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelRequest_sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '역발행 요청 취소 메모
        Dim Memo As String = "역발행 요청 취소 메모"

        Try
            Dim response As Response = taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 공급자가 공급받는자에게 역발행 요청 받은 세금계산서의 발행을 거부합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Refuse
    '=========================================================================
    Private Sub btnRefuse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefuse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '역발행 요청 거부 메모
        Dim Memo As String = "역발행 요청 거부 메모"

        Try
            Dim response As Response = taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 공급자가 공급받는자에게 역발행 요청 받은 세금계산서의 발행을 거부합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Refuse
    '=========================================================================
    Private Sub btnRefuse_sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRefuse_sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '역발행 요청 거부 메모
        Dim Memo As String = "역발행 요청 거부 메모"

        Try
            Dim response As Response = taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 세금계산서를 삭제합니다.
    ' - 삭제 가능한 상태: "임시저장", "발행취소", "역발행거부", "역발행취소", "전송실패"
    ' - 세금계산서를 삭제해야만 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Delete
    '=========================================================================
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnDelete.Click, btnDelete_Reverse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 세금계산서를 삭제합니다.
    ' - 삭제 가능한 상태: "임시저장", "발행취소", "역발행거부", "역발행취소", "전송실패"
    ' - 세금계산서를 삭제해야만 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Delete
    '=========================================================================
    Private Sub btnDelete_Sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnDelete_Sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 삭제 가능한 상태의 세금계산서를 삭제합니다.
    ' - 삭제 가능한 상태: "임시저장", "발행취소", "역발행거부", "역발행취소", "전송실패"
    ' - 세금계산서를 삭제해야만 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Delete
    '=========================================================================
    Private Sub btnDelete_Reverse_sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnDelete_Reverse_sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "발행완료" 상태의 전자세금계산서를 국세청에 즉시 전송하며, 함수 호출 후 최대 30분 이내에 전송 처리가 완료됩니다.
    ' - 국세청 즉시전송을 호출하지 않은 세금계산서는 발행일 기준 다음 영업일 오후 3시에 팝빌 시스템에서 일괄적으로 국세청으로 전송합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#SendToNTS
    '=========================================================================
    Private Sub btnSendToNTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendToNTS.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendToNTS(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 1건의 상태 및 요약정보를 확인합니다.
    ' 리턴값 'TaxinvoiceInfo'의 변수 'stateCode'를 통해 세금계산서의 상태코드를 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetInfo
    '=========================================================================
    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim tiInfo As TaxinvoiceInfo = taxinvoiceService.GetInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp += "itemKey (팝빌번호) : " + tiInfo.itemKey + vbCrLf
            tmp += "taxType (과세형태) : " + tiInfo.taxType + vbCrLf
            tmp += "writeDate (작성일자) : " + tiInfo.writeDate + vbCrLf
            tmp += "regDT (임시저장 일자) : " + tiInfo.regDT + vbCrLf
            tmp += "issueType (발행형태) : " + tiInfo.issueType + vbCrLf
            tmp += "supplyCostTotal (공급가액 합계) : " + tiInfo.supplyCostTotal + vbCrLf
            tmp += "taxTotal (세액 합계) : " + tiInfo.taxTotal + vbCrLf
            tmp += "purposeType (영수/청구) : " + tiInfo.purposeType + vbCrLf
            tmp += "issueDT (발행일시) : " + tiInfo.issueDT + vbCrLf
            tmp += "lateIssueYN (지연발행 여부) : " + CStr(tiInfo.lateIssueYN) + vbCrLf
            tmp += "openYN (개봉 여부) : " + CStr(tiInfo.openYN) + vbCrLf
            tmp += "openDT (개봉 일시) : " + CStr(tiInfo.openDT) + vbCrLf
            tmp += "stateMemo (상태메모) : " + tiInfo.stateMemo + vbCrLf
            tmp += "stateCode (상태코드) : " + tiInfo.stateCode.ToString + vbCrLf
            tmp += "stateDT (상태 변경일시) : " + tiInfo.stateDT + vbCrLf
            tmp += "nstconfirmNum (국세청승인번호) : " + tiInfo.ntsconfirmNum + vbCrLf
            tmp += "ntsresult (국세청 전송결과) : " + tiInfo.ntsresult + vbCrLf
            tmp += "ntssendDT (국세청 전송일시) : " + tiInfo.ntssendDT + vbCrLf
            tmp += "ntsresultDT (국세청 결과 수신일시) : " + tiInfo.ntsresultDT + vbCrLf
            tmp += "ntssendErrCode (전송실패 사유코드) : " + tiInfo.ntssendErrCode + vbCrLf
            tmp += "modifyCode (수정 사유코드) : " + tiInfo.modifyCode.ToString + vbCrLf
            tmp += "interOPYN (연동문서 여부) : " + CStr(tiInfo.interOPYN) + vbCrLf
            tmp += "invoicerCorpName (공급자 상호) : " + tiInfo.invoicerCorpName + vbCrLf
            tmp += "invoicerCorpNum (공급자 사업자번호) : " + tiInfo.invoicerCorpNum + vbCrLf
            tmp += "invoicerMgtKey (공급자 문서번호) : " + tiInfo.invoicerMgtKey + vbCrLf
            tmp += "invoicerPrintYN (공급자 인쇄여부) : " + CStr(tiInfo.invoicerPrintYN) + vbCrLf
            tmp += "invoiceeCorpName (공급받는자 상호) : " + tiInfo.invoiceeCorpName + vbCrLf
            tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + tiInfo.invoiceeCorpNum + vbCrLf
            tmp += "invoiceePrintYN (공급받는자 문서번호) : " + CStr(tiInfo.invoiceePrintYN) + vbCrLf
            tmp += "closeDownState (공급받는자 휴폐업상태) : " + tiInfo.closeDownState.ToString + vbCrLf
            tmp += "closeDownStateDate (공급받는자 휴폐업일자) : " + CStr(tiInfo.closeDownStateDate) + vbCrLf
            tmp += "trusteeCorpName (수탁자 상호) : " + tiInfo.trusteeCorpName + vbCrLf
            tmp += "trusteeCorpNum (수탁자 사업자번호) : " + tiInfo.trusteeCorpNum + vbCrLf
            tmp += "trusteeMgtKey (수탁자 문서번호) : " + tiInfo.trusteeMgtKey + vbCrLf
            tmp += "trusteePrintYN (수탁자 인쇄여부) : " + CStr(tiInfo.trusteePrintYN) + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 다수건의 세금계산서 상태 및 요약 정보를 확인합니다. (1회 호출 시 최대 1,000건 확인 가능)
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetInfos
    '=========================================================================
    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서번호 배열, 최대 1000건
        MgtKeyList.Add("20220513-001")
        MgtKeyList.Add("20220513-002")

        Try
            Dim taxinvoiceInfoList As List(Of TaxinvoiceInfo) = taxinvoiceService.GetInfos(txtCorpNum.Text, KeyType, MgtKeyList)

            Dim tmp As String = ""

            For Each tiInfo As TaxinvoiceInfo In taxinvoiceInfoList

                tmp += "itemKey (팝빌번호) : " + tiInfo.itemKey + vbCrLf
                tmp += "taxType (과세형태) : " + tiInfo.taxType + vbCrLf
                tmp += "writeDate (작성일자) : " + tiInfo.writeDate + vbCrLf
                tmp += "regDT (임시저장 일자) : " + tiInfo.regDT + vbCrLf
                tmp += "issueType (발행형태) : " + tiInfo.issueType + vbCrLf
                tmp += "supplyCostTotal (공급가액 합계) : " + tiInfo.supplyCostTotal + vbCrLf
                tmp += "taxTotal (세액 합계) : " + tiInfo.taxTotal + vbCrLf
                tmp += "purposeType (영수/청구) : " + tiInfo.purposeType + vbCrLf
                tmp += "issueDT (발행일시) : " + tiInfo.issueDT + vbCrLf
                tmp += "lateIssueYN (지연발행 여부) : " + CStr(tiInfo.lateIssueYN) + vbCrLf
                tmp += "openYN (개봉 여부) : " + CStr(tiInfo.openYN) + vbCrLf
                tmp += "openDT (개봉 일시) : " + CStr(tiInfo.openDT) + vbCrLf
                tmp += "stateMemo (상태메모) : " + tiInfo.stateMemo + vbCrLf
                tmp += "stateCode (상태코드) : " + tiInfo.stateCode.ToString + vbCrLf
                tmp += "stateDT (상태 변경일시) : " + tiInfo.stateDT + vbCrLf
                tmp += "nstconfirmNum (국세청승인번호) : " + tiInfo.ntsconfirmNum + vbCrLf
                tmp += "ntsresult (국세청 전송결과) : " + tiInfo.ntsresult + vbCrLf
                tmp += "ntssendDT (국세청 전송일시) : " + tiInfo.ntssendDT + vbCrLf
                tmp += "ntsresultDT (국세청 결과 수신일시) : " + tiInfo.ntsresultDT + vbCrLf
                tmp += "ntssendErrCode (전송실패 사유코드) : " + tiInfo.ntssendErrCode + vbCrLf
                tmp += "modifyCode (수정 사유코드) : " + tiInfo.modifyCode.ToString + vbCrLf
                tmp += "interOPYN (연동문서 여부) : " + CStr(tiInfo.interOPYN) + vbCrLf
                tmp += "invoicerCorpName (공급자 상호) : " + tiInfo.invoicerCorpName + vbCrLf
                tmp += "invoicerCorpNum (공급자 사업자번호) : " + tiInfo.invoicerCorpNum + vbCrLf
                tmp += "invoicerMgtKey (공급자 문서번호) : " + tiInfo.invoicerMgtKey + vbCrLf
                tmp += "invoicerPrintYN (공급자 인쇄여부) : " + CStr(tiInfo.invoicerPrintYN) + vbCrLf
                tmp += "invoiceeCorpName (공급받는자 상호) : " + tiInfo.invoiceeCorpName + vbCrLf
                tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + tiInfo.invoiceeCorpNum + vbCrLf
                tmp += "invoiceePrintYN (공급받는자 문서번호) : " + CStr(tiInfo.invoiceePrintYN) + vbCrLf
                tmp += "closeDownState (공급받는자 휴폐업상태) : " + tiInfo.closeDownState.ToString + vbCrLf
                tmp += "closeDownStateDate (공급받는자 휴폐업일자) : " + CStr(tiInfo.closeDownStateDate) + vbCrLf
                tmp += "trusteeCorpName (수탁자 상호) : " + tiInfo.trusteeCorpName + vbCrLf
                tmp += "trusteeCorpNum (수탁자 사업자번호) : " + tiInfo.trusteeCorpNum + vbCrLf
                tmp += "trusteeMgtKey (수탁자 문서번호) : " + tiInfo.trusteeMgtKey + vbCrLf
                tmp += "trusteePrintYN (수탁자 인쇄여부) : " + CStr(tiInfo.trusteePrintYN) + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 1건의 상세정보를 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetDetailInfo
    '=========================================================================
    Private Sub btnGetDetailInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetDetailInfo.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim tiDetailInfo As Taxinvoice = taxinvoiceService.GetDetailInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text)


            Dim tmp As String = ""
            tmp += "ntsconfirmNum (국세청승인번호) : " + tiDetailInfo.ntsconfirmNum + vbCrLf
            tmp += "issueType (발행형태) : " + tiDetailInfo.issueType + vbCrLf
            tmp += "taxType (과세형태) : " + tiDetailInfo.taxType + vbCrLf
            tmp += "chargeDirection (과금방향) : " + tiDetailInfo.chargeDirection + vbCrLf
            tmp += "serialNum (일련번호) : " + tiDetailInfo.serialNum + vbCrLf
            tmp += "kwon (권) : " + CStr(tiDetailInfo.kwon) + vbCrLf
            tmp += "ho (호) : " + CStr(tiDetailInfo.ho) + vbCrLf
            tmp += "writeDate (작성일자) : " + tiDetailInfo.writeDate + vbCrLf
            tmp += "purposeType (영수/청구) : " + tiDetailInfo.purposeType + vbCrLf
            tmp += "supplyCostTotal (공급가액 합계) : " + tiDetailInfo.supplyCostTotal + vbCrLf
            tmp += "taxTotal (세액 합계) : " + tiDetailInfo.taxTotal + vbCrLf
            tmp += "totalAmount (합계금액) : " + tiDetailInfo.totalAmount + vbCrLf
            tmp += "cash (현금) : " + tiDetailInfo.cash + vbCrLf
            tmp += "chkBill (수표) : " + tiDetailInfo.chkBill + vbCrLf
            tmp += "credit (외상) : " + tiDetailInfo.credit + vbCrLf
            tmp += "note (어음) : " + tiDetailInfo.note + vbCrLf
            tmp += "remark1 (비고1) : " + tiDetailInfo.remark1 + vbCrLf
            tmp += "remark2 (비고2) : " + tiDetailInfo.remark2 + vbCrLf
            tmp += "remakr3 (비고3) : " + tiDetailInfo.remark3 + vbCrLf

            tmp += "invoicerMgtKey (공급자 문서번호) : " + tiDetailInfo.invoicerMgtKey + vbCrLf
            tmp += "invoicerCorpNum (공급자 사업자번호) : " + tiDetailInfo.invoicerCorpNum + vbCrLf
            tmp += "invoicerTaxRegID (공급자 종사업장 식별번호) : " + tiDetailInfo.invoicerTaxRegID + vbCrLf
            tmp += "invoicerCorpName (공급자 상호) : " + tiDetailInfo.invoicerCorpName + vbCrLf
            tmp += "invoicerCEOName (공급자 대표자성명) : " + tiDetailInfo.invoicerCEOName + vbCrLf
            tmp += "invoicerAddr (공급자 주소) : " + tiDetailInfo.invoicerAddr + vbCrLf
            tmp += "invoicerBizClass (공급자 종목) : " + tiDetailInfo.invoicerBizClass + vbCrLf
            tmp += "invoicerBizType (공급자 업태) : " + tiDetailInfo.invoicerBizType + vbCrLf
            tmp += "invoicerContactName (담당자 성명) : " + tiDetailInfo.invoicerContactName + vbCrLf
            tmp += "invoicerTEL (담당자 연락처) : " + tiDetailInfo.invoicerTEL + vbCrLf
            tmp += "invoicerHP (담당자 휴대폰) : " + tiDetailInfo.invoicerHP + vbCrLf
            tmp += "invoicerEmail (담당자 이메일) : " + tiDetailInfo.invoicerEmail + vbCrLf
            tmp += "invoicerSMSSendYN (문자전송 여부) : " + CStr(tiDetailInfo.invoicerSMSSendYN) + vbCrLf

            tmp += "invoiceeMgtKey (공급받는자 문서번호) : " + tiDetailInfo.invoiceeMgtKey + vbCrLf
            tmp += "invoiceeType (공급받는자 구분) : " + tiDetailInfo.invoiceeType + vbCrLf
            tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + tiDetailInfo.invoiceeCorpNum + vbCrLf
            tmp += "invoiceeTaxRegID (공급받는자 종사업장 식별번호) : " + tiDetailInfo.invoiceeTaxRegID + vbCrLf
            tmp += "invoiceeCorpName (공급받는자 상호) : " + tiDetailInfo.invoiceeCorpName + vbCrLf
            tmp += "invoiceeCEOName (공급받는자 대표자 성명) : " + tiDetailInfo.invoiceeCEOName + vbCrLf
            tmp += "invoiceeAddr (공급받는자 주소) : " + tiDetailInfo.invoiceeAddr + vbCrLf
            tmp += "invoiceeBizType (공급받는자 업태) : " + tiDetailInfo.invoiceeBizType + vbCrLf
            tmp += "invoiceeBizClass (공급받는자 종목) : " + tiDetailInfo.invoiceeBizClass + vbCrLf
            tmp += "closeDownState (휴폐업상태) : " + tiDetailInfo.closeDownState.ToString + vbCrLf
            tmp += "closeDownStateDate (휴폐업일자) : " + CStr(tiDetailInfo.closeDownStateDate) + vbCrLf
            tmp += "invoiceeContactName1 (담당자 성명) : " + tiDetailInfo.invoiceeContactName1 + vbCrLf
            tmp += "invoiceeTEL1 (담당자 연락처) : " + tiDetailInfo.invoiceeTEL1 + vbCrLf
            tmp += "invoiceeHP1 (담당자 휴대폰) : " + tiDetailInfo.invoiceeHP1 + vbCrLf
            tmp += "invoiceeEmail1 (담당자 이메일) : " + tiDetailInfo.invoiceeEmail1 + vbCrLf

            tmp += "orgNTSConfirmNum (원본 국세청승인번호) : " + tiDetailInfo.orgNTSConfirmNum + vbCrLf

            If Not tiDetailInfo.detailList Is Nothing Then
                For Each detailList As TaxinvoiceDetail In tiDetailInfo.detailList
                    tmp += "[상세항목(품목)]" + vbCrLf
                    tmp += "serialNum (일련번호) : " + CStr(detailList.serialNum) + vbCrLf
                    tmp += "purchaseDT (거래일자) : " + detailList.purchaseDT + vbCrLf
                    tmp += "itemName (품목명) : " + detailList.itemName + vbCrLf
                    tmp += "spec (규격) :" + detailList.spec + vbCrLf
                    tmp += "qty (수량) :" + detailList.qty + vbCrLf
                    tmp += "unitCost (단가) :" + detailList.unitCost + vbCrLf
                    tmp += "supplyCost (공급가액) : " + detailList.supplyCost + vbCrLf
                    tmp += "tax (세액) :" + detailList.tax + vbCrLf
                    tmp += "remark (비고) :" + detailList.remark + vbCrLf
                Next
                tmp += vbCrLf + vbCrLf
            End If

            If Not tiDetailInfo.addContactList Is Nothing Then
                For Each addContact As TaxinvoiceAddContact In tiDetailInfo.addContactList
                    tmp += "[추가담당자]" + vbCrLf
                    tmp += "serialNum (일련번호) : " + CStr(addContact.serialNum) + vbCrLf
                    tmp += "contactName (담당자 성명) : " + addContact.contactName + vbCrLf
                    tmp += "email (이메일주소) : " + addContact.email + vbCrLf
                Next
                tmp += vbCrLf + vbCrLf
            End If

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 1건의 상세정보를 XML로 반환합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetXML
    '=========================================================================
    Private Sub btnGetXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetXML.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try

            Dim tiXML As TaxinvoiceXML = taxinvoiceService.GetXML(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp += "code (응답코드) : " + CStr(tiXML.code) + vbCrLf
            tmp += "message (응답메시지) : " + tiXML.message + vbCrLf
            tmp += "retObject (전자세금계산서 XML 문서) : " + tiXML.retObject + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 검색조건에 해당하는 세금계산서를 조회합니다. (조회기간 단위 : 최대 6개월)
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim State(3) As String
        Dim TType(2) As String
        Dim taxType(3) As String
        Dim IssueType(3) As String
        Dim RegType(2) As String
        Dim CloseDownState(5) As String

        ' 일자유형 ("R" , "W" , "I" 중 택 1)
        ' - R = 등록일자 , W = 작성일자 , I = 발행일자
        Dim DType As String = "W"

        '시작일자, yyyyMMdd
        Dim SDate As String = "20220513"

        '종료일자, yyyyMMdd
        Dim EDate As String = "20220513"

        '세금계산서 상태코드 배열 (2,3번째 자리에 와일드카드(*) 사용 가능)
        '- 미입력시 전체조회
        State(0) = "3**"
        State(1) = "4**"
        State(1) = "6**"

        ' 문서유형 배열 ("N" , "M" 중 선택, 다중 선택 가능)
        ' - N = 일반세금계산서 , M = 수정세금계산서
        ' - 미입력시 전체조회
        TType(0) = "N"
        TType(1) = "M"

        ' 과세형태 배열 ("T" , "N" , "Z" 중 선택, 다중 선택 가능)
        ' - T = 과세 , N = 면세 , Z = 영세
        ' - 미입력시 전체조회
        taxType(0) = "T"
        taxType(1) = "N"
        taxType(2) = "Z"

        ' 발행형태 배열 ("N" , "R" , "T" 중 선택, 다중 선택 가능)
        ' - N = 정발행 , R = 역발행 , T = 위수탁발행
        ' - 미입력시 전체조회
        IssueType(0) = "N"
        IssueType(1) = "R"
        IssueType(2) = "T"

        ' 지연발행 여부 (null , true , false 중 택 1)
        ' - null = 전체조회 , true = 지연발행 , false = 정상발행
        Dim LateOnly As Boolean = Nothing

        ' 종사업장번호 유무 (null , "0" , "1" 중 택 1)
        ' - null = 전체 , 0 = 없음, 1 = 있음
        Dim TaxRegIDYN As String = ""

        ' 종사업장번호의 주체 ("S" , "B" , "T" 중 택 1)
        ' └ S = 공급자 , B = 공급받는자 , T = 수탁자
        ' - 미입력시 전체조회
        Dim TaxRegIDType As String = "S"

        ' 종사업장번호
        ' 다수기재시 콤마(",")로 구분하여 구성 ex ) "0001,0002"
        ' - 미입력시 전체조회
        Dim TaxRegID As String = ""

        ' 거래처 상호 / 사업자번호 (사업자) / 주민등록번호 (개인) / "9999999999999" (외국인) 중 검색하고자 하는 정보 입력
        ' └ 사업자번호 / 주민등록번호는 하이픈('-')을 제외한 숫자만 입력
        ' - 미입력시 전체조회
        Dim QString As String = ""

        '정렬방향, D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

        '페이지 번호
        Dim Page As Integer = 1

        '페이지 목록개수, 최대 1000건
        Dim PerPage As Integer = 10

        ' 연동문서 여부 (null , "0" , "1" 중 택 1)
        ' └ null = 전체조회 , 0 = 일반문서 , 1 = 연동문서
        ' - 일반문서 : 팝빌 사이트를 통해 저장 또는 발행한 세금계산서
        ' - 연동문서 : 팝빌 API를 통해 저장 또는 발행한 세금계산서
        Dim interOPYN As String = ""

        ' 등록유형 배열 ("P" , "H" 중 선택, 다중 선택 가능)
        ' - P = 팝빌에서 등록 , H = 홈택스 또는 외부ASP 등록
        ' - 미입력시 전체조회
        RegType(0) = "P"
        RegType(1) = "H"

        ' 공급받는자 휴폐업상태 배열 ("N" , "0" , "1" , "2" , "3" , "4" 중 선택, 다중 선택 가능)
        ' - N = 미확인 , 0 = 미등록 , 1 = 사업 , 2 = 폐업 , 3 = 휴업 , 4 = 확인실패
        ' - 미입력시 전체조회
        CloseDownState(0) = "N"
        CloseDownState(1) = "0"
        CloseDownState(2) = "1"
        CloseDownState(3) = "2"
        CloseDownState(4) = "3"

        '문서번호 또는 국세청승인번호 조회
        Dim MgtKey As String = ""

        Try
            Dim tiSearchList As TISearchResult = taxinvoiceService.Search(txtCorpNum.Text, KeyType, DType, SDate, EDate, State, TType, _
                                                                          taxType, IssueType, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, QString, Order, Page, _
                                                                          PerPage, interOPYN, txtUserId.Text, RegType, CloseDownState, MgtKey)


            Dim tmp As String

            tmp = "code (응답코드) : " + CStr(tiSearchList.code) + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + CStr(tiSearchList.total) + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + CStr(tiSearchList.perPage) + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + CStr(tiSearchList.pageNum) + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + CStr(tiSearchList.pageCount) + vbCrLf
            tmp = tmp + "message (응답메시지) : " + tiSearchList.message + vbCrLf + vbCrLf

            Dim tiInfo As TaxinvoiceInfo

            For Each tiInfo In tiSearchList.list
                tmp += "itemKey (팝빌번호) : " + tiInfo.itemKey + vbCrLf
                tmp += "taxType (과세형태) : " + tiInfo.taxType + vbCrLf
                tmp += "writeDate (작성일자) : " + tiInfo.writeDate + vbCrLf
                tmp += "regDT (임시저장 일자) : " + tiInfo.regDT + vbCrLf
                tmp += "issueType (발행형태) : " + tiInfo.issueType + vbCrLf
                tmp += "supplyCostTotal (공급가액 합계) : " + tiInfo.supplyCostTotal + vbCrLf
                tmp += "taxTotal (세액 합계) : " + tiInfo.taxTotal + vbCrLf
                tmp += "purposeType (영수/청구) : " + tiInfo.purposeType + vbCrLf
                tmp += "issueDT (발행일시) : " + tiInfo.issueDT + vbCrLf
                tmp += "lateIssueYN (지연발행 여부) : " + CStr(tiInfo.lateIssueYN) + vbCrLf
                tmp += "openYN (개봉 여부) : " + CStr(tiInfo.openYN) + vbCrLf
                tmp += "openDT (개봉 일시) : " + CStr(tiInfo.openDT) + vbCrLf
                tmp += "stateMemo (상태메모) : " + tiInfo.stateMemo + vbCrLf
                tmp += "stateCode (상태코드) : " + tiInfo.stateCode.ToString + vbCrLf
                tmp += "stateDT (상태 변경일시) : " + tiInfo.stateDT + vbCrLf
                tmp += "nstconfirmNum (국세청승인번호) : " + tiInfo.ntsconfirmNum + vbCrLf
                tmp += "ntsresult (국세청 전송결과) : " + tiInfo.ntsresult + vbCrLf
                tmp += "ntssendDT (국세청 전송일시) : " + tiInfo.ntssendDT + vbCrLf
                tmp += "ntsresultDT (국세청 결과 수신일시) : " + tiInfo.ntsresultDT + vbCrLf
                tmp += "ntssendErrCode (전송실패 사유코드) : " + tiInfo.ntssendErrCode + vbCrLf
                tmp += "modifyCode (수정 사유코드) : " + tiInfo.modifyCode.ToString + vbCrLf
                tmp += "interOPYN (연동문서 여부) : " + CStr(tiInfo.interOPYN) + vbCrLf
                tmp += "invoicerCorpName (공급자 상호) : " + tiInfo.invoicerCorpName + vbCrLf
                tmp += "invoicerCorpNum (공급자 사업자번호) : " + tiInfo.invoicerCorpNum + vbCrLf
                tmp += "invoicerMgtKey (공급자 문서번호) : " + tiInfo.invoicerMgtKey + vbCrLf
                tmp += "invoicerPrintYN (공급자 인쇄여부) : " + CStr(tiInfo.invoicerPrintYN) + vbCrLf
                tmp += "invoiceeCorpName (공급받는자 상호) : " + tiInfo.invoiceeCorpName + vbCrLf
                tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + tiInfo.invoiceeCorpNum + vbCrLf
                tmp += "invoiceePrintYN (공급받는자 문서번호) : " + CStr(tiInfo.invoiceePrintYN) + vbCrLf
                tmp += "closeDownState (공급받는자 휴폐업상태) : " + tiInfo.closeDownState.ToString + vbCrLf
                tmp += "closeDownStateDate (공급받는자 휴폐업일자) : " + CStr(tiInfo.closeDownStateDate) + vbCrLf
                tmp += "trusteeCorpName (수탁자 상호) : " + tiInfo.trusteeCorpName + vbCrLf
                tmp += "trusteeCorpNum (수탁자 사업자번호) : " + tiInfo.trusteeCorpNum + vbCrLf
                tmp += "trusteeMgtKey (수탁자 문서번호) : " + tiInfo.trusteeMgtKey + vbCrLf
                tmp += "trusteePrintYN (수탁자 인쇄여부) : " + CStr(tiInfo.trusteePrintYN) + vbCrLf

            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서의 상태에 대한 변경이력을 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetLogs
    '=========================================================================
    Private Sub btnGetLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetLogs.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim logList As List(Of TaxinvoiceLog) = taxinvoiceService.GetLogs(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            Dim tmp As String = ""
            tmp += "docType(로그타입) | log(이력정보) | procType(처리형태) | procContactName(처리담당자) |"
            tmp += "procMemo(처리메모) | regDT(등록일시) | ip(아이피)" + vbCrLf + vbCrLf

            For Each log As TaxinvoiceLog In logList
                tmp += log.docLogType.ToString + " | " + log.log + " | " + log.procType + " | " + log.procCorpName + " | " + log.procContactName + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 전자세금계산서 임시문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetURL_TBOX.Click

        'TBOX-임시문서함 / SBOX-매출문서함 / PBOX-매입문서함 / WRITE-정발행작성
        'SWBOX-매출 발행 대기함 / PWBOX-매입 발행 대기함
        Dim TOGO As String = "TBOX"

        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 전자세금계산서 매출서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetURL_SBOX.Click

        'TBOX-임시문서함 / SBOX-매출문서함 / PBOX-매입문서함 / WRITE-정발행작성
        'SWBOX-매출 발행 대기함 / PWBOX-매입 발행 대기함
        Dim TOGO As String = "SBOX"

        Try

            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 전자세금계산서 매입문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_PBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetURL_PBOX.Click

        'TBOX-임시문서함 / SBOX-매출문서함 / PBOX-매입문서함 / WRITE-정발행작성
        'SWBOX-매출 발행 대기함 / PWBOX-매입 발행 대기함
        Dim TOGO As String = "PBOX"

        Try

            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 전자세금계산서 매출문서작성 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetURL_WRITE.Click

        'TBOX-임시문서함 / SBOX-매출문서함 / PBOX-매입문서함 / WRITE-정발행작성
        'SWBOX-매출 발행 대기함 / PWBOX-매입 발행 대기함
        Dim TOGO As String = "WRITE"

        Try

            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 전자세금계산서 매출문서작성 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_SWBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetURL_SWBOX.Click

        'TBOX-임시문서함 / SBOX-매출문서함 / PBOX-매입문서함 / WRITE-정발행작성
        'SWBOX-매출 발행 대기함 / PWBOX-매입 발행 대기함
        Dim TOGO As String = "SWBOX"

        Try

            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub
    '=========================================================================
    ' 로그인 상태로 팝빌 사이트의 전자세금계산서 매출문서작성 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
    '=========================================================================
    Private Sub btnGetURL_PWBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetURL_PWBOX.Click

        'TBOX-임시문서함 / SBOX-매출문서함 / PBOX-매입문서함 / WRITE-정발행작성
        'SWBOX-매출 발행 대기함 / PWBOX-매입 발행 대기함
        Dim TOGO As String = "PWBOX"

        Try

            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 1건의 상세 정보 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetPopUpURL
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPopUpURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPopUpURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 1건의 상세정보 페이지(사이트 상단, 좌측 메뉴 및 버튼 제외)의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetViewURL
    '=========================================================================
    Private Sub btnGetViewURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetViewURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetViewURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 1건을 인쇄하기 위한 페이지의 팝업 URL을 반환하며, 페이지내에서 인쇄 설정값을 "공급자" / "공급받는자" / "공급자+공급받는자"용 중 하나로 지정할 수 있습니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetPrintURL
    '=========================================================================
    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPrintURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 1건을 구버전 양식으로 인쇄하기 위한 페이지의 팝업 URL을 반환하며, 페이지내에서 인쇄 설정값을 "공급자" / "공급받는자" / "공급자+공급받는자"용 중 하나로 지정할 수 있습니다..
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetOldPrintURL
    '=========================================================================
    Private Sub btnGetOldPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetOldPrintURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetOldPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "공급받는자" 용 세금계산서 1건을 인쇄하기 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetEPrintURL
    '=========================================================================
    Private Sub btnEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnEPrintURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 다수건의 세금계산서를 인쇄하기 위한 페이지의 팝업 URL을 반환합니다. (최대 100건)
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetMassPrintURL
    '=========================================================================
    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetMassPrintURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '문서번호 배열, 최대 100건
        Dim MgtKeyList As List(Of String) = New List(Of String)
        MgtKeyList.Add("20220513-001")
        MgtKeyList.Add("20220513-002")

        Try
            Dim url As String = taxinvoiceService.GetMassPrintURL(txtCorpNum.Text, KeyType, MgtKeyList, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 전자세금계산서 안내메일의 상세보기 링크 URL을 반환합니다.
    ' - 함수 호출로 반환 받은 URL에는 유효시간이 없습니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetMailURL
    '=========================================================================
    Private Sub btnGetEmailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetEmailURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetMailURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자세금계산서 PDF 파일을 다운 받을 수 있는 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetPDFURL
    '=========================================================================
    Private Sub btnGetPDFURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPDFURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPDFURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetAccessURL.Click

        Try
            Dim url As String = taxinvoiceService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서에 첨부할 인감, 사업자등록증, 통장사본을 등록하는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#GetSealURL
    '=========================================================================
    Private Sub btnGetSealURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetSealURL.Click
        Try
            Dim url As String = taxinvoiceService.GetSealURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 세금계산서에 1개의 파일을 첨부합니다. (최대 5개)
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#AttachFile
    '=========================================================================
    Private Sub btnAttachFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnAttachFile.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then
            Dim strFileName As String = fileDialog.FileName

            Try
                Dim response As Response = taxinvoiceService.AttachFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, strFileName, txtUserId.Text)

                MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
            Catch ex As PopbillException
                MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

            End Try

        End If
    End Sub

    '=========================================================================
    ' "임시저장" 상태의 세금계산서에 첨부된 1개의 파일을 삭제합니다.
    ' - 파일 식별을 위해 첨부 시 할당되는 'FileID'는 첨부파일 목록 확인(GetFiles API) 함수를 호출하여 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#DeleteFile
    '=========================================================================
    Private Sub btnDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnDeleteFile.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.DeleteFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtFileID.Text, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 세금계산서에 첨부된 파일목록을 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#GetFiles
    '=========================================================================
    Private Sub btnGetFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFiles.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim fileList As List(Of AttachedFile) = taxinvoiceService.GetFiles(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            Dim tmp As String = "serialNum(일련번호) | displayName(첨부파일명) | attachedFile(파일아이디) | regDT(등록일자)" + vbCrLf

            For Each file As AttachedFile In fileList
                tmp += file.serialNum.ToString + " | " + file.displayName + " | " + file.attachedFile + " | " + file.regDT + vbCrLf

            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 세금계산서와 관련된 안내 메일을 재전송 합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#SendEmail
    '=========================================================================
    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendEmail.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '수신자 이메일주소
        Dim Receiver As String = ""

        Try
            Dim response As Response = taxinvoiceService.SendEmail(txtCorpNum.Text, KeyType, txtMgtKey.Text, Receiver, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서와 관련된 안내 SMS(단문) 문자를 재전송하는 함수로, 팝빌 사이트 [문자·팩스] > [문자] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
    ' - 메시지는 최대 90byte까지 입력 가능하고, 초과한 내용은 자동으로 삭제되어 전송합니다. (한글 최대 45자)
    ' - 함수 호출시 포인트가 과금됩니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#SendSMS
    '=========================================================================
    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발신번호
        Dim sendNum As String = ""

        '수신번호
        Dim receiveNum As String = ""

        '메시지내용, 90byte(한글45자) 초과된 내용은 삭제되어 전송됨
        Dim contents As String = "발신문자 메시지 내용"

        Try
            Dim response As Response = taxinvoiceService.SendSMS(txtCorpNum.Text, KeyType, txtMgtKey.Text, sendNum, receiveNum, contents, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서를 팩스로 전송하는 함수로, 팝빌 사이트 [문자·팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
    ' - 함수 호출시 포인트가 과금됩니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#SendFAX
    '=========================================================================
    Private Sub btnSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFAX.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발신번호
        Dim sendNum As String = ""

        '수신번호
        Dim receiveNum As String = ""

        Try
            Dim response As Response = taxinvoiceService.SendFAX(txtCorpNum.Text, KeyType, txtMgtKey.Text, sendNum, receiveNum, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 전자명세서 API를 통해 발행한 전자명세서를 세금계산서에 첨부합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#AttachStatement
    '=========================================================================
    Private Sub btnAttachStatement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnAttachStatement.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '첨부 대상 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim docItemCode As Integer = 121

        '첨부 대상 전자명세서 문서번호
        Dim docMgtKey As String = "20220513-001"

        Try
            Dim response As Response = taxinvoiceService.AttachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text, docItemCode, docMgtKey)
            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서에 첨부된 전자명세서를 해제합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#DetachStatement
    '=========================================================================
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '첨부해제 대상 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim docItemCode As Integer = 121

        '첨부해제 대상 전자명세서 문서번호
        Dim docMgtKey As String = "20220513-001"

        Try
            Dim response As Response = taxinvoiceService.DetachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text, docItemCode, docMgtKey)
            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트를 통해 발행하여 문서번호가 할당되지 않은 세금계산서에 문서번호를 할당합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#AssignMgtKey
    '=========================================================================
    Private Sub btnAssignMgtKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnAssignMgtKey.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '팝빌번호, 목록조회(Search API) 함수의 반환항목 중 ItemKey 참조
        Dim itemKey As String = ""

        '문서번호가 없는 문서에 할당할 문서번호
        '- 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
        Dim mgtKey As String = "20220513-003"

        Try
            Dim response As Response = taxinvoiceService.AssignMgtKey(txtCorpNum.Text, KeyType, itemKey, mgtKey)
            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 관련 메일 항목에 대한 발송설정을 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#ListEmailConfig
    '=========================================================================
    Private Sub btnListEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnListEmailConfig.Click

        Try
            Dim emailConfigList As List(Of EmailConfig) = taxinvoiceService.ListEmailConfig(txtCorpNum.Text)

            Dim tmp As String = "메일전송유형 | 전송여부 " + vbCrLf

            For Each info As EmailConfig In emailConfigList
                If info.emailType = "TAX_ISSUE_INVOICER" Then _
                    tmp += "[정발행] TAX_ISSUE_INVOICER (공급자에게 전자세금계산서 발행 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_CHECK" Then _
                    tmp += "[정발행] TAX_CHECK (공급자에게 전자세금계산서 수신확인 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_CANCEL_ISSUE" Then _
                    tmp += "[정발행] TAX_CANCEL_ISSUE (공급받는자에게 전자세금계산서 발행취소 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_REQUEST" Then _
                    tmp += "[역발행] TAX_REQUEST (공급자에게 세금계산서를 발행요청 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_CANCEL_REQUEST" Then _
                    tmp += "[역발행] TAX_CANCEL_REQUEST (공급받는자에게 세금계산서 취소 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_REFUSE" Then _
                    tmp += "[역발행] TAX_REFUSE (공급받는자에게 세금계산서 거부 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_REVERSE_ISSUE" Then _
                    tmp += "[역발행] TAX_REVERSE_ISSUE (공급받는자에게 세금계산서 발행 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_TRUST_ISSUE" Then _
                    tmp += "[위수탁발행] TAX_TRUST_ISSUE (공급받는자에게 전자세금계산서 발행 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_TRUST_ISSUE_TRUSTEE" Then _
                    tmp += "[위수탁발행] TAX_TRUST_ISSUE_TRUSTEE (수탁자에게 전자세금계산서 발행 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_TRUST_ISSUE_INVOICER" Then _
                    tmp += "[위수탁발행] TAX_TRUST_ISSUE_INVOICER (공급자에게 전자세금계산서 발행 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_TRUST_CANCEL_ISSUE" Then _
                    tmp += "[위수탁발행] TAX_TRUST_CANCEL_ISSUE (공급받는자에게 전자세금계산서 발행취소 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_TRUST_CANCEL_ISSUE_INVOICER" Then _
                    tmp += "[위수탁발행] TAX_TRUST_CANCEL_ISSUE_INVOICER (공급자에게 전자세금계산서 발행취소 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_CLOSEDOWN" Then _
                    tmp += "[처리결과] TAX_CLOSEDOWN (거래처의 사업자등록상태조회 (휴폐업조회) 확인 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_NTSFAIL_INVOICER" Then _
                    tmp += "[처리결과] TAX_NTSFAIL_INVOICER (전자세금계산서 국세청 전송실패 안내) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "ETC_CERT_EXPIRATION" Then _
                    tmp += "[정기발송] ETC_CERT_EXPIRATION (팝빌에서 이용중인 공인인증서의 갱신 메일) | " + info.sendYN.ToString + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 관련 메일 항목에 대한 발송설정을 수정합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#UpdateEmailConfig
    '메일전송유형
    '[정발행]
    'TAX_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행 사실을 안내하는 메일
    'TAX_CHECK : 공급자에게 전자세금계산서 수신확인 사실을 안내하는 메일
    'TAX_CANCEL_ISSUE : 공급받는자에게 전자세금계산서 발행취소 사실을 안내하는 메일

    '[역발행]
    'TAX_REQUEST : 공급자에게 전자세금계산서를 발행을 요청하는 메일
    'TAX_CANCEL_REQUEST : 공급받는자에게 전자세금계산서 취소 사실을 안내하는 메일
    'TAX_REFUSE : 공급받는자에게 전자세금계산서 거부 사실을 안내하는 메일
    'TAX_REVERSE_ISSUE : 공급받는자에게 전자세금계산서 발행 사실을 안내하는 메일

    '[위수탁발행]
    'TAX_TRUST_ISSUE : 공급받는자에게 전자세금계산서 발행 사실을 안내하는 메일
    'TAX_TRUST_ISSUE_TRUSTEE : 수탁자에게 전자세금계산서 발행 사실을 안내하는 메일
    'TAX_TRUST_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행 사실을 안내하는 메일
    'TAX_TRUST_CANCEL_ISSUE : 공급받는자에게 전자세금계산서 발행취소 사실을 안내하는 메일
    'TAX_TRUST_CANCEL_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행취소 사실을 안내하는 메일

    '[처리결과]
    'TAX_CLOSEDOWN : 거래처의 사업자등록상태(휴폐업)를 확인하여 안내하는 메일
    'TAX_NTSFAIL_INVOICER : 전자세금계산서 국세청 전송실패를 안내하는 메일

    '[정기발송]
    'ETC_CERT_EXPIRATION : 팝빌에 등록된 인증서의 만료예정을 안내하는 메일
    '=========================================================================
    Private Sub btnUpdateEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUpdateEmailConfig.Click

        Try
            '메일전송유형
            Dim emailType As String = "TAX_ISSUE"

            '전송여부 (True-전송, False-미전송)
            Dim sendYN As Boolean = True

            Dim response As Response = taxinvoiceService.UpdateEmailConfig(txtCorpNum.Text, emailType, sendYN)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 국세청 전송 옵션 설정 상태를 확인합니다.
    ' - 국세청 전송 옵션 설정은 팝빌 사이트 [전자세금계산서] > [환경설정] > [세금계산서 관리] 메뉴에서 설정할 수 있으며, API로 설정은 불가능 합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#GetSendToNTSConfig
    '=========================================================================
    Private Sub btnGetSendToNTSConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSendToNTSConfig.Click
        Try
            Dim sendToNTSConfig As Boolean = taxinvoiceService.GetSendToNTSConfig(txtCorpNum.Text)

            MsgBox("국세청 전송 설정 확인 : " + sendToNTSConfig.ToString + vbCrLf + "True(발행 즉시 전송) False(익일 자동 전송)")
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자세금계산서 발행에 필요한 인증서를 팝빌 인증서버에 등록하기 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - 인증서 갱신/재발급/비밀번호 변경한 경우, 변경된 인증서를 팝빌 인증서버에 재등록 해야합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/cert#GetTaxCertURL
    '=========================================================================
    Private Sub btnGetTaxCertURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetTaxCertURL.Click

        Try
            Dim url As String = taxinvoiceService.GetTaxCertURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
            '=====================================================================
            '공인인증서 등록시에는 Internet Explorer 브라우저만 이용이 가능합니다.
            '- IE에서만 공인인증서 ActiveX 툴킷 구동가능
            '=====================================================================

            'Internet Explorer Process 선언
            Dim ie As New System.Diagnostics.ProcessStartInfo("iexplore")

            '팝빌로부터 반환받은 팝업 URL 주소 지정
            ie.Arguments = url

            '화면 기본위치 지정 (Normal-기본, Minimized-최소화, Maximized-최대화, Hidden-숨김화면)
            ie.WindowStyle = ProcessWindowStyle.Normal

            'Internet Explorer Process 호출
            System.Diagnostics.Process.Start(ie)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 인증서버에 등록된 인증서의 만료일을 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/cert#GetCertificateExpireDate
    '=========================================================================
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetCertificateExpireDate.Click

        Try
            Dim expiration As DateTime = taxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text)

            MsgBox("공인인증서 만료일시 : " + expiration.ToString)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 인증서버에 등록된 인증서의 유효성을 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/cert#CheckCertValidation
    '=========================================================================
    Private Sub btnCheckCertValidation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckCertValidation.Click

        Try
            Dim response As Response = taxinvoiceService.CheckCertValidation(txtCorpNum.Text)

            MessageBox.Show("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MessageBox.Show("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 인증서버에 등록된 인증서의 정보를 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/cert#GetTaxCertInfo
    '=========================================================================
    Private Sub btnGetTaxCertInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTaxCertInfo.Click
        Try
            Dim taxinvoiceCertificate As TaxinvoiceCertificate = taxinvoiceService.GetTaxCertInfo(txtCorpNum.Text)

            Dim tmp As String = ""
            tmp += "regDT (등록일시) : " + taxinvoiceCertificate.regDT + vbCrLf
            tmp += "expireDT (만료일시) : " + taxinvoiceCertificate.expireDT + vbCrLf
            tmp += "issuerDN (인증서 발급자 DN) : " + taxinvoiceCertificate.issuerDN + vbCrLf
            tmp += "subjectDN (등록된 인증서 DN) : " + taxinvoiceCertificate.subjectDN + vbCrLf
            tmp += "issuerName (인증서 종류) : " + taxinvoiceCertificate.issuerName + vbCrLf
            tmp += "oid (OID) : " + taxinvoiceCertificate.oid + vbCrLf
            tmp += "regContactName (등록 담당자 성명) : " + taxinvoiceCertificate.regContactName + vbCrLf
            tmp += "regContactID (등록 담당자 아이디) : " + taxinvoiceCertificate.regContactID + vbCrLf


            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetBalance.Click

        Try
            Dim remainPoint As Double = taxinvoiceService.GetBalance(txtCorpNum.Text)

            MsgBox("remainPoint(연동회원 잔여포인트) : " + remainPoint.ToString)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetChargeURL.Click

        Try
            Dim url As String = taxinvoiceService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPaymentURL.Click
        Try
            Dim url As String = taxinvoiceService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = taxinvoiceService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPartnerBalance.Click

        Try
            Dim remainPoint As Double = taxinvoiceService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("remainPoint(파트너 잔여포인트) : " + remainPoint.ToString)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPartnerURL.Click

        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = taxinvoiceService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자세금계산서 발행단가를 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click

        Try
            Dim unitCost As Single = taxinvoiceService.GetUnitCost(txtCorpNum.Text)

            MsgBox("unitCost(세금계산서 발행단가) : " + unitCost.ToString)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 전자세금계산서 API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetChargeInfo.Click

        Try
            Dim ChargeInfo As ChargeInfo = taxinvoiceService.GetChargeInfo(txtCorpNum.Text)

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
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckIsMember.Click

        Try
            Dim response As Response = taxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click

        Try
            Dim response As Response = taxinvoiceService.CheckID(txtCorpNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#JoinMember
    '=========================================================================
    Private Sub btnJoinMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnJoinMember.Click

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
            Dim response As Response = taxinvoiceService.JoinMember(joinInfo)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#GetCorpInfo
    '=========================================================================
    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetCorpInfo.Click

        Try
            Dim corpInfo As CorpInfo = taxinvoiceService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#UpdateCorpInfo
    '=========================================================================
    Private Sub btnUpdateCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUpdateCorpInfo.Click

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

            Dim response As Response = taxinvoiceService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#RegistContact
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRegistContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디 (6자이상 50자미만)
        joinData.id = "testkorea01"

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
            Dim response As Response = taxinvoiceService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = taxinvoiceService.GetContactInfo(txtCorpNum.Text, contactID)

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
    '  연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnListContact.Click

        Try
            Dim contactList As List(Of Contact) = taxinvoiceService.ListContact(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#UpdateContact
    '=========================================================================
    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUpdateContact.Click

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
            Dim response As Response = taxinvoiceService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' [임시저장] 상태의 세금계산서를 [공급자]가 [발행예정]합니다.
    ' - 발행예정이란 공급자와 공급받는자 사이에 세금계산서 확인 후 발행하는 방법입니다.
    '=========================================================================
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행예정 메모
        Dim Memo As String = "발행예정 메모"

        '발행예정 메일제목, 공백으로 처리시 기본메일 제목으로 전송ㄴ
        Dim EmailSubject As String = "발행예정 메일제목 테스트 dotent 3.5"

        Try
            Dim response As Response = taxinvoiceService.Send(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, EmailSubject, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException

            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' [승인대기] 상태의 세금계산서를 [공급자]가 [취소]합니다.
    ' - [취소]된 세금계산서를 삭제(Delete API)하면 등록된 문서번호를 재사용할 수 있습니다.
    '=========================================================================
    Private Sub btnCancelSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행예정 취소 메모
        Dim Memo As String = "발행예정 취소 메모"

        Try
            Dim response As Response = taxinvoiceService.CancelSend(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [승인대기] 상태의 세금계산서를 [공급받는자]가 [승인]합니다.
    '=========================================================================
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '승인대기 승인 메모
        Dim Memo As String = "승인 메모"

        Try
            Dim response As Response = taxinvoiceService.Accept(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' [승인대기] 상태의 세금계산서를 [공급받는자]가 [거부]합니다.
    ' - [거부]처리된 세금계산서를 삭제(Delete API)하면 등록된 문서번호를 재사용할 수 있습니다.
    '=========================================================================
    Private Sub btnDeny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '승인대기 거부 메모
        Dim Memo As String = "승인대기 거부 메모"

        Try
            Dim response As Response = taxinvoiceService.Deny(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#PaymentRequest
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
            Dim response As PaymentResponse = taxinvoiceService.PaymentRequest(txtCorpNum.Text, paymentForm, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message+ vbCrLf + "settleCode(정산코드) : " + response.settleCode)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetSettleResult
    '=========================================================================
    Private Sub btnGetSettleResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSettleResult.Click

        '정산코드
        Dim SettleCode As String = "202301160000000010"

        Try
            Dim response As PaymentHistory = taxinvoiceService.GetSettleResult (txtCorpNum.Text, SettleCode, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetPaymentHistory
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
            Dim result As PaymentHistoryResult = taxinvoiceService.GetPaymentHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetUseHistory
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
            Dim result As UseHistoryResult = taxinvoiceService.GetUseHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, Order, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#Refund
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
            Dim response As RefundResponse = taxinvoiceService.Refund(txtCorpNum.Text,refundForm, txtUserId.Text)

            Dim tmp As String = ""
            tmp += "code(응답코드) : " + response.code.ToString + vbCrLf
            tmp += "message(응답메시지) : " + response.Message + vbCrLf
            tmp += "refundCode(환불코드) : " +response.refundCode
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 환불신청내역을 확인합니다.
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetRefundHistory
    '=========================================================================
    Private Sub btnGetRefundHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundHistory.Click

        '목폭 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500


        Try
            Dim result As RefundHistoryResult  = taxinvoiceService.GetRefundHistory(txtCorpNum.Text,Page, PerPage, txtUserId.Text)

            Dim tmp As String = ""

            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "환불내역"+ vbCrLf

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
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetRefundInfo
    '=========================================================================
    Private Sub btnGetRefundInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '환불코드
        Dim refundCode As String = "023040000017"

        Try
            Dim history As RefundHistory  = taxinvoiceService.GetRefundInfo(txtCorpNum.Text,refundCode, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetRefundableBalance
    '=========================================================================
    Private Sub btnGetRefundableBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundableBalance.Click

        Try
            Dim refundableBalance As Double  = taxinvoiceService.GetRefundableBalance(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#QuitMember
    '=========================================================================
    Private Sub btnQuitMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitMember.Click

        '탈퇴사유
        Dim quitReason As String = "회원 탈퇴 사유"

        Try
            Dim response As Response  = taxinvoiceService.QuitMember(txtCorpNum.Text, quitReason, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.Message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub
End Class
