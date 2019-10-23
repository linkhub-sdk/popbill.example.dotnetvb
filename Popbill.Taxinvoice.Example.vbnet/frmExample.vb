'=========================================================================
'
' 팝빌 전자세금계산서 API VB.Net SDK Example
'
' - VB.NET SDK 연동환경 설정방법 안내 : https://docs.popbill.com/taxinvoice/tutorial/dotnet_vb
' - 업데이트 일자 : 2019-10-23
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
'
' <테스트 연동개발 준비사항>
' 1) 27, 30번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
' 3) 전자세금계산서 발행을 위해 공인인증서를 등록합니다.
'    - 팝빌사이트 로그인 > [전자세금계산서] > [환경설정] > [공인인증서 관리]
'    - 공인인증서 등록 팝업 URL (GetTaxCertURL API)을 이용하여 등록
'
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

        '연동환경 설정값 (True-개발용, False-상업용)
        taxinvoiceService.IsTest = True

        '인증토큰의 IP제한기능 사용여부, (True-권장)
        taxinvoiceService.IPRestrictOnOff = True
    End Sub

    '=========================================================================
    ' 세금계산서 관리번호 중복여부를 확인합니다.
    ' - 관리번호는 1~24자리로 숫자, 영문 '-', '_' 조합으로 구성할 수 있습니다.
    '=========================================================================
    Private Sub btnCheckMgtKeyInUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckMgtKeyInUse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim InUse As Boolean = taxinvoiceService.CheckMgtKeyInUse(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox(IIf(InUse, "사용중", "미사용중"))
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 세금계산서를 [즉시발행]합니다.
    ' - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRegistIssue.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20190111
        taxinvoice.writeDate = "20191023"

        '[필수] 발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "정발행"

        '[필수] {정과금, 역과금} 중 기재, '역과금'은 역발행 프로세스에서만 이용가능
        '- 정과금(공급자 과금), 역과금(공급받는자 과금)
        taxinvoice.chargeDirection = "정과금"

        '[필수] 영수/청구, [영수, 청구] 중 기재
        taxinvoice.purposeType = "영수"

        '[필수] 발행시점, [직접발행, 승인시자동발행] 중 기재
        ' 발행예정(Send API) 프로세스를 구현하지 않는경우 '직접발행' 기재
        taxinvoice.issueTiming = "직접발행"

        '[필수] 과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"

        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '[필수] 공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = txtCorpNum.Text

        '[필수] 공급자 종사업자 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '[필수] 공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '[필수] 공급자 문서번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
        '사업자 별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = txtMgtKey.Text

        '[필수] 공급자 대표자 성명
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
        taxinvoice.invoicerEmail = "test@test.com"

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = "070-7070-0707"

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = "010-000-2222"

        '정발행시 공급받는자에게 발행안내문자 전송여부
        '- 안내문자 전송기능 이용시 포인트가 차감됩니다.
        taxinvoice.invoicerSMSSendYN = True

        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '[필수] 공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        '[필수] 공급받는자 사업자번호, '-' 제외 10자리
        taxinvoice.invoiceeCorpNum = "8888888888"

        '[필수] 공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '[역발행시 필수] 공급받는자 문서번호(역발행시 필수)
        taxinvoice.invoiceeMgtKey = ""

        '[필수] 공급받는자 대표자 성명
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
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"


        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = "070-111-222"

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = "010-111-2222"


        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '[필수] 공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '[필수] 세액 합계
        taxinvoice.taxTotal = "10000"

        '[필수] 합계금액, 공급가액 합계 + 세액합계
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

        '기재 상 '비고' 항목
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        '사업자등록증 이미지 첨부여부
        taxinvoice.businessLicenseYN = False

        '통장사본 이미지 첨부여부
        taxinvoice.bankBookYN = False


        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
        ' - [참고] 수정세금계산서 작성방법 안내 - https://docs.popbill.com/taxinvoice/modify?lang=dotnet
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
        detail.purchaseDT = "20190111"                 '거래일자, yyyyMMdd
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
        addContact.email = "test2@invoicee.com"         '담당자 메일주소

        taxinvoice.addContactList.Add(addContact)


        '지연발행 강제여부
        '지연발행 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
        '지연발행 세금계산서를 신고해야 하는 경우 forceIssue 값을 true 선언하여 발행하실 수 있습니다.
        Dim forceIssue As Boolean = False

        '발행시 메모
        Dim memo As String = "즉시발행 메모"

        Try
            Dim response As IssueResponse = taxinvoiceService.RegistIssue(txtCorpNum.Text, taxinvoice, forceIssue, memo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message + vbCrLf + "국세청승인번호(ntsConfirmNum) : " + response.ntsConfirmNum)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 세금계산서를 [임시저장]합니다.
    ' - 세금계산서 임시저장(Register API) 호출후에는 발행(Issue API)을 호출해야만 국세청으로 전송됩니다.
    ' - 정발행시 임시저장(Register)과 발행(Issue)을 한번의 호출로 처리하는 즉시발행(RegistIssue API) 프로세스 연동을 권장합니다.
    ' - 역발행시 임시저장(Register)과 역발행요청(Request)을 한번의 호출로 처리하는 즉시요청(RegistRequest API) 프로세스 연동을 권장합니다.
    ' - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20190111
        taxinvoice.writeDate = "20190226"

        '[필수] 발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "정발행"

        '[필수] {정과금, 역과금} 중 기재, '역과금'은 역발행 프로세스에서만 이용가능
        '- 정과금(공급자 과금), 역과금(공급받는자 과금)
        taxinvoice.chargeDirection = "정과금"

        '[필수] 영수/청구, [영수, 청구] 중 기재
        taxinvoice.purposeType = "영수"

        '[필수] 발행시점, [직접발행, 승인시자동발행] 중 기재
        ' 발행예정(Send API) 프로세스를 구현하지 않는경우 '직접발행' 기재
        taxinvoice.issueTiming = "직접발행"

        '[필수] 과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"


        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '[필수] 공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = txtCorpNum.Text

        '[필수] 공급자 종사업자 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '[필수] 공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '[필수] 공급자 문서번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
        '사업자 별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = txtMgtKey.Text

        '[필수] 공급자 대표자 성명
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
        taxinvoice.invoicerEmail = "test@test.com"

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = "070-7070-0707"

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = "010-000-2222"

        '정발행시 공급받는자에게 발행안내문자 전송여부
        '- 안내문자 전송기능 이용시 포인트가 차감됩니다.
        taxinvoice.invoicerSMSSendYN = True


        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '[필수] 공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        '[필수] 공급받는자 사업자번호, '-' 제외 10자리
        taxinvoice.invoiceeCorpNum = "8888888888"

        '[필수] 공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '[역발행시 필수] 공급받는자 문서번호(역발행시 필수)
        taxinvoice.invoiceeMgtKey = ""

        '[필수] 공급받는자 대표자 성명
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
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = "070-111-222"

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = "010-111-2222"


        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '[필수] 공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '[필수] 세액 합계
        taxinvoice.taxTotal = "10000"

        '[필수] 합계금액, 공급가액 합계 + 세액합계
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

        '기재 상 '비고' 항목
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        '사업자등록증 이미지 첨부여부
        taxinvoice.businessLicenseYN = False

        '통장사본 이미지 첨부여부
        taxinvoice.bankBookYN = False


        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
        ' - [참고] 수정세금계산서 작성방법 안내 - https://docs.popbill.com/taxinvoice/modify?lang=dotnet
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
        detail.purchaseDT = "20190111"                  '거래일자, yyyyMMdd
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
        addContact.email = "test2@invoicee.com"         '담당자 메일주소

        taxinvoice.addContactList.Add(addContact)

        '전자거래명세서 동시작성 여부
        Dim writeSpecification As Boolean = False

        Try
            Dim response As Response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text, writeSpecification)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 세금계산서를 [임시저장]합니다.
    ' - 세금계산서 임시저장(Register API) 호출후에는 발행(Issue API)을 호출해야만 국세청으로 전송됩니다.
    ' - 정발행시 임시저장(Register)과 발행(Issue)을 한번의 호출로 처리하는 즉시발행(RegistIssue API) 프로세스 연동을 권장합니다.
    ' - 역발행시 임시저장(Register)과 역발행요청(Request)을 한번의 호출로 처리하는 즉시요청(RegistRequest API) 프로세스 연동을 권장합니다.
    ' - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnRegister_Reverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRegister_Reverse.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20190111
        taxinvoice.writeDate = "20190111"

        '[필수] 발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "역발행"

        '[필수] {정과금, 역과금} 중 기재, '역과금'은 역발행 프로세스에서만 이용가능
        '- 정과금(공급자 과금), 역과금(공급받는자 과금)
        taxinvoice.chargeDirection = "정과금"

        '[필수] 영수/청구, [영수, 청구] 중 기재
        taxinvoice.purposeType = "영수"

        '[필수] 발행시점, [직접발행, 승인시자동발행] 중 기재
        ' 발행예정(Send API) 프로세스를 구현하지 않는경우 '직접발행' 기재
        taxinvoice.issueTiming = "직접발행"

        '[필수] 과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"


        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '[필수] 공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = "8888888888"

        '[필수] 공급자 종사업자 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '[필수] 공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '공급자 문서번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
        '사업자 별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = ""

        '[필수] 공급자 대표자 성명
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
        taxinvoice.invoicerEmail = "test@test.com"

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = "070-7070-0707"

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = "010-000-2222"

        '정발행시 공급받는자에게 발행안내문자 전송여부
        '- 안내문자 전송기능 이용시 포인트가 차감됩니다.
        taxinvoice.invoicerSMSSendYN = True


        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '[필수] 공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        '[필수] 공급받는자 사업자번호, '-' 제외 10자리
        taxinvoice.invoiceeCorpNum = txtCorpNum.Text

        '[필수] 공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '[역발행시 필수] 공급받는자 문서번호(역발행시 필수)
        taxinvoice.invoiceeMgtKey = txtMgtKey.Text

        '[필수] 공급받는자 대표자 성명
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
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = "070-111-222"

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = "010-111-2222"


        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '[필수] 공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '[필수] 세액 합계
        taxinvoice.taxTotal = "10000"

        '[필수] 합계금액, 공급가액 합계 + 세액합계
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

        '기재 상 '비고' 항목
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        '사업자등록증 이미지 첨부여부
        taxinvoice.businessLicenseYN = False

        '통장사본 이미지 첨부여부
        taxinvoice.bankBookYN = False


        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
        ' - [참고] 수정세금계산서 작성방법 안내 - https://docs.popbill.com/taxinvoice/modify?lang=dotnet
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
        detail.purchaseDT = "20190111"                 '거래일자, yyyyMMdd
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
            Dim response As Response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [임시저장] 상태의 세금계산서의 항목을 [수정]합니다.
    ' - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20190111
        taxinvoice.writeDate = "20190111"

        '[필수] 발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "정발행"

        '[필수] {정과금, 역과금} 중 기재, '역과금'은 역발행 프로세스에서만 이용가능
        '- 정과금(공급자 과금), 역과금(공급받는자 과금)
        taxinvoice.chargeDirection = "정과금"

        '[필수] 영수/청구, [영수, 청구] 중 기재
        taxinvoice.purposeType = "영수"

        '[필수] 발행시점, [직접발행, 승인시자동발행] 중 기재
        ' 발행예정(Send API) 프로세스를 구현하지 않는경우 '직접발행' 기재
        taxinvoice.issueTiming = "직접발행"

        '[필수] 과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"

        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '[필수] 공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = txtCorpNum.Text

        '[필수] 공급자 종사업자 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '[필수] 공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '[필수] 공급자 문서번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
        '사업자 별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = txtMgtKey.Text

        '[필수] 공급자 대표자 성명
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
        taxinvoice.invoicerEmail = "test@test.com"

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = "070-7070-0707"

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = "010-000-2222"

        '정발행시 공급받는자에게 발행안내문자 전송여부
        '- 안내문자 전송기능 이용시 포인트가 차감됩니다.
        taxinvoice.invoicerSMSSendYN = True


        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '[필수] 공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        '[필수] 공급받는자 사업자번호, '-' 제외 10자리
        taxinvoice.invoiceeCorpNum = "8888888888"

        '[필수] 공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '[역발행시 필수] 공급받는자 문서번호(역발행시 필수)
        taxinvoice.invoiceeMgtKey = ""

        '[필수] 공급받는자 대표자 성명
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
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = "070-111-222"

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = "010-111-2222"


        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '[필수] 공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '[필수] 세액 합계
        taxinvoice.taxTotal = "10000"

        '[필수] 합계금액, 공급가액 합계 + 세액합계
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

        '기재 상 '비고' 항목
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        '사업자등록증 이미지 첨부여부
        taxinvoice.businessLicenseYN = False

        '통장사본 이미지 첨부여부
        taxinvoice.bankBookYN = False


        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
        ' - [참고] 수정세금계산서 작성방법 안내 - https://docs.popbill.com/taxinvoice/modify?lang=dotnet
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
        detail.purchaseDT = "20190111"                 '거래일자, yyyyMMdd
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
        addContact.email = "test2@invoicee.com"         '담당자 메일주소

        taxinvoice.addContactList.Add(addContact)


        Try
            Dim response As Response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' [임시저장] 상태의 세금계산서의 항목을 [수정]합니다.
    ' - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnUpdate_Reverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUpdate_Reverse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20190111
        taxinvoice.writeDate = "20190111"

        '[필수] 발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "역발행"

        '[필수] {정과금, 역과금} 중 기재, '역과금'은 역발행 프로세스에서만 이용가능
        '- 정과금(공급자 과금), 역과금(공급받는자 과금)
        taxinvoice.chargeDirection = "정과금"

        '[필수] 영수/청구, [영수, 청구] 중 기재
        taxinvoice.purposeType = "영수"

        '[필수] 발행시점, [직접발행, 승인시자동발행] 중 기재
        ' 발행예정(Send API) 프로세스를 구현하지 않는경우 '직접발행' 기재
        taxinvoice.issueTiming = "직접발행"

        '[필수] 과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"

        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '[필수] 공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = "8888888888"

        '[필수] 공급자 종사업자 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '[필수] 공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '[필수] 공급자 문서번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
        '사업자 별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = txtMgtKey.Text

        '[필수] 공급자 대표자 성명
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
        taxinvoice.invoicerEmail = "test@test.com"

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = "070-7070-0707"

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = "010-000-2222"

        '정발행시 공급받는자에게 발행안내문자 전송여부
        '- 안내문자 전송기능 이용시 포인트가 차감됩니다.
        taxinvoice.invoicerSMSSendYN = True


        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '[필수] 공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        '[필수] 공급받는자 사업자번호, '-' 제외 10자리
        taxinvoice.invoiceeCorpNum = txtCorpNum.Text

        '[필수] 공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '[역발행시 필수] 공급받는자 문서번호(역발행시 필수)
        taxinvoice.invoiceeMgtKey = ""

        '[필수] 공급받는자 대표자 성명
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
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = "070-111-222"

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = "010-111-2222"


        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '[필수] 공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '[필수] 세액 합계
        taxinvoice.taxTotal = "10000"

        '[필수] 합계금액, 공급가액 합계 + 세액합계
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

        '기재 상 '비고' 항목
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        '사업자등록증 이미지 첨부여부
        taxinvoice.businessLicenseYN = False

        '통장사본 이미지 첨부여부
        taxinvoice.bankBookYN = False


        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
        ' - [참고] 수정세금계산서 작성방법 안내 - https://docs.popbill.com/taxinvoice/modify?lang=dotnet
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
        detail.purchaseDT = "20190111"                 '거래일자, yyyyMMdd
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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [임시저장] 또는 [발행대기] 상태의 세금계산서를 [공급자]가 [발행]합니다.
    ' - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnIssue.Click, btnIssue_Reverse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행 메모
        Dim memo As String = "발행메모"

        '지연발행 강제여부, 기본값 - False
        '발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
        '지연발행 세금계산서를 신고해야 하는 경우 forceIssue 값을 True로 선언하여 발행(Issue API)을 호출할 수 있습니다.
        Dim forceIssue As Boolean = False

        Try
            Dim response As IssueResponse = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, forceIssue, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message + vbCrLf + "국세청승인번호(ntsConfirmNum) : " + response.ntsConfirmNum)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' [(역)발행대기] 상태의 세금계산서를 [발행] 합니다.
    ' - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnIssue_Reverse_sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnIssue_Reverse_sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행 메모
        Dim memo As String = "발행메모"

        '지연발행 강제여부, 기본값 - False
        '발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
        '지연발행 세금계산서를 신고해야 하는 경우 forceIssue 값을 True로 선언하여 발행(Issue API)을 호출할 수 있습니다.
        Dim forceIssue As Boolean = False

        Try
            Dim response As IssueResponse = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, forceIssue, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message + vbCrLf + "국세청승인번호(ntsConfirmNum) : " + response.ntsConfirmNum)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' [발행완료] 상태의 세금계산서를 [공급자]가 [발행취소]합니다.
    ' - [발행취소]는 국세청 전송전에만 가능합니다.
    ' - 발행취소된 세금계산서는 국세청에 전송되지 않습니다.
    ' - 발행취소 세금계산서에 사용된 문서번호를 재사용 하기 위해서는 삭제(Delete API)를 호출하여 해당세금계산서를 삭제해야 합니다.
    '=========================================================================
    Private Sub btnCancelIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelIssue.Click, btnCancelIssue_Reverse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행취소 메모
        Dim memo As String = "발행취소메모"

        Try
            Dim response As Response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' [발행완료] 상태의 세금계산서를 [공급자]가 [발행취소]합니다.
    ' - [발행취소]는 국세청 전송전에만 가능합니다.
    ' - 발행취소된 세금계산서는 국세청에 전송되지 않습니다.
    ' - 발행취소 세금계산서에 사용된 문서번호를 재사용 하기 위해서는 삭제(Delete API)를 호출하여 해당세금계산서를 삭제해야 합니다.
    '=========================================================================
    Private Sub btnCancelIssue_Sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelIssue_Sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행취소 메모
        Dim memo As String = "발행취소메모"

        Try
            Dim response As Response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' [발행완료] 상태의 세금계산서를 [공급자]가 [발행취소]합니다.
    ' - [발행취소]는 국세청 전송전에만 가능합니다.
    ' - 발행취소된 세금계산서는 국세청에 전송되지 않습니다.
    ' - 발행취소 세금계산서에 사용된 문서번호를 재사용 하기 위해서는 삭제(Delete API)를 호출하여 해당세금계산서를 삭제해야 합니다.
    '=========================================================================
    Private Sub btnCancelIssue_Reverse_sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelIssue_Reverse_sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발행취소 메모
        Dim memo As String = "발행취소메모"

        Try
            Dim response As Response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' [임시저장] 상태의 세금계산서를 [공급자]가 [발행예정]합니다.
    ' - 발행예정이란 공급자와 공급받는자 사이에 세금계산서 확인 후 발행하는 방법입니다.
    ' - "[전자세금계산서 API 연동매뉴얼] > 1.2.1. 정발행 > 다. 임시저장 발행예정" 의 프로세스를 참조하시기 바랍니다.
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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' [공급받는자]가 공급자에게 역발행 세금계산서를 [즉시 요청]합니다.
    ' - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
    ' - 역발행 세금계산서 프로세스를 구현하기 위해서는 공급자/공급받는자가 모두 팝빌에 회원이여야 합니다.
    ' - 역발행 즉시요청후 공급자가 [발행] 처리시 포인트가 차감되며 역발행 세금계산서 항목중 과금방향(ChargeDirection)에 기재한 값에 따라
    '   정과금(공급자과금) 또는 역과금(공급받는자과금) 처리됩니다.
    '=========================================================================
    Private Sub btnRegistRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRegistRequest.Click

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20190111
        taxinvoice.writeDate = "20190111"

        '[필수] 발행형태, [정발행, 역발행, 위수탁] 중 기재
        taxinvoice.issueType = "역발행"

        '[필수] {정과금, 역과금} 중 기재, '역과금'은 역발행 프로세스에서만 이용가능
        '- 정과금(공급자 과금), 역과금(공급받는자 과금)
        taxinvoice.chargeDirection = "정과금"

        '[필수] 영수/청구, [영수, 청구] 중 기재
        taxinvoice.purposeType = "영수"

        '[필수] 발행시점, [직접발행, 승인시자동발행] 중 기재
        ' 발행예정(Send API) 프로세스를 구현하지 않는경우 '직접발행' 기재
        taxinvoice.issueTiming = "직접발행"

        '[필수] 과세형태, [과세, 영세, 면세] 중 기재
        taxinvoice.taxType = "과세"


        '=========================================================================
        '                              공급자 정보
        '=========================================================================

        '[필수] 공급자 사업자번호, '-' 제외 10자리
        taxinvoice.invoicerCorpNum = "8888888888"

        '[필수] 공급자 종사업자 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '[필수] 공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '공급자 문서번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
        '사업자 별로 중복되지 않도록 구성
        taxinvoice.invoicerMgtKey = ""

        '[필수] 공급자 대표자 성명
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
        taxinvoice.invoicerEmail = "test@test.com"

        '공급자 담당자 연락처
        taxinvoice.invoicerTEL = "070-7070-0707"

        '공급자 담당자 휴대폰번호
        taxinvoice.invoicerHP = "010-000-2222"

        '=========================================================================
        '                            공급받는자 정보
        '=========================================================================

        '[필수] 공급받는자 구분, [사업자, 개인, 외국인] 중 기재
        taxinvoice.invoiceeType = "사업자"

        '[필수] 공급받는자 사업자번호, '-' 제외 10자리
        taxinvoice.invoiceeCorpNum = txtCorpNum.Text

        '[필수] 공급자받는자 상호
        taxinvoice.invoiceeCorpName = "공급받는자 상호"

        '[역발행시 필수] 공급받는자 문서번호
        taxinvoice.invoiceeMgtKey = txtMgtKey.Text

        '[필수] 공급받는자 대표자 성명
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
        '팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
        '실제 거래처의 메일주소가 기재되지 않도록 주의
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        '공급받는자 담당자 연락처
        taxinvoice.invoiceeTEL1 = "070-111-222"

        '공급받는자 담당자 휴대폰번호
        taxinvoice.invoiceeHP1 = "010-111-2222"

        '역발행 요청시 알림문자 전송여부 (역발행에서만 사용가능)
        ' - 공급자 담당자 휴대폰번호(invoicerHP)로 전송
        ' - 전송시 포인트가 차감되며 전송실패하는 경우 포인트 환불처리
        taxinvoice.invoiceeSMSSendYN = False


        '=========================================================================
        '                            세금계산서 정보
        '=========================================================================

        '[필수] 공급가액 합계
        taxinvoice.supplyCostTotal = "100000"

        '[필수] 세액 합계
        taxinvoice.taxTotal = "10000"

        '[필수] 합계금액, 공급가액 합계 + 세액합계
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

        '기재 상 '비고' 항목
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"

        '사업자등록증 이미지 첨부여부
        taxinvoice.businessLicenseYN = False

        '통장사본 이미지 첨부여부
        taxinvoice.bankBookYN = False


        '=========================================================================
        '         수정세금계산서 정보 (수정세금계산서 작성시에만 기재
        ' - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
        ' - [참고] 수정세금계산서 작성방법 안내 - https://docs.popbill.com/taxinvoice/modify?lang=dotnet
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
        detail.purchaseDT = "20190111"                  '거래일자, yyyyMMdd
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
            Dim response As Response = taxinvoiceService.RegistRequest(txtCorpNum.Text, taxinvoice, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' [공급받는자]가 임시저장 상태의 역발행 세금계산서를 공급자에게 [발행요청] 합니다. 
    ' - 역발행 세금계산서 프로세스를 구현하기 위해서는 공급자/공급받는자가 모두 팝빌에 회원이여야 합니다.
    ' - 역발행 요청후 공급자가 [발행] 처리시 포인트가 차감되며 역발행 세금계산서 항목중 과금방향(ChargeDirection)에 기재한 값에 따라
    '   정과금(공급자과금) 또는 역과금(공급받는자과금) 처리됩니다.
    '=========================================================================
    Private Sub btnRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequest.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '역발행 요청 메모
        Dim Memo As String = "역발행 요청 메모"

        Try
            Dim response As Response = taxinvoiceService.Request(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [공급받는자]가 역)발행대기 상태의 세금계산서를 [취소]합니다. 
    ' - [취소]한 세금계산서의 문서번호를 재사용하기 위해서는 삭제 (Delete API)를 호출해야 합니다.
    '=========================================================================
    Private Sub btnCancelRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelRequest.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '역발행 요청 취소 메모
        Dim Memo As String = "역발행 요청 취소 메모"

        Try
            Dim response As Response = taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' [공급받는자]가 역)발행대기 상태의 세금계산서를 [취소]합니다. 
    ' - [취소]한 세금계산서의 문서번호를 재사용하기 위해서는 삭제 (Delete API)를 호출해야 합니다.
    '=========================================================================
    Private Sub btnCancelRequest_sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelRequest_sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '역발행 요청 취소 메모
        Dim Memo As String = "역발행 요청 취소 메모"

        Try
            Dim response As Response = taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 공급받는자에게 요청받은 역)발행대기 상태의 세금계산서를 [공급자]가 [거부]합니다.
    ' - 세금계산서의 문서번호를 재사용하기 위해서는 삭제 (Delete API)를 호출하여 [삭제] 처리해야 합니다.
    '=========================================================================
    Private Sub btnRefuse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefuse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '역발행 요청 거부 메모
        Dim Memo As String = "역발행 요청 거부 메모"

        Try
            Dim response As Response = taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 공급받는자에게 요청받은 역)발행대기 상태의 세금계산서를 [공급자]가 [거부]합니다.
    ' - 세금계산서의 문서번호를 재사용하기 위해서는 삭제 (Delete API)를 호출하여 [삭제] 처리해야 합니다.
    '=========================================================================
    Private Sub btnRefuse_sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRefuse_sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '역발행 요청 거부 메모
        Dim Memo As String = "역발행 요청 거부 메모"

        Try
            Dim response As Response = taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 전자세금계산서를 [삭제]합니다.
    ' - 세금계산서를 삭제해야만 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - 삭제가능한 문서 상태 : [임시저장], [발행취소], [발행예정 취소], [발행예정 거부]
    '=========================================================================
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnDelete.Click, btnDelete_Reverse.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 전자세금계산서를 [삭제]합니다.
    ' - 세금계산서를 삭제해야만 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - 삭제가능한 문서 상태 : [임시저장], [발행취소], [발행예정 취소], [발행예정 거부]
    '=========================================================================
    Private Sub btnDelete_Sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnDelete_Sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 전자세금계산서를 [삭제]합니다.
    ' - 세금계산서를 삭제해야만 문서번호(mgtKey)를 재사용할 수 있습니다.
    ' - 삭제가능한 문서 상태 : [임시저장], [발행취소], [발행예정 취소], [발행예정 거부]
    '=========================================================================
    Private Sub btnDelete_Reverse_sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnDelete_Reverse_sub.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [발행완료] 상태의 세금계산서를 국세청으로 [즉시전송]합니다.
    ' - 국세청 즉시전송을 호출하지 않은 세금계산서는 발행일 기준 익일 오후 3시에 팝빌 시스템에서 일괄적으로 국세청으로 전송합니다.
    ' - 익일전송시 전송일이 법정공휴일인 경우 다음 영업일에 전송됩니다.
    ' - 국세청 전송에 관한 사항은 "[전자세금계산서 API 연동매뉴얼] > 1.3 국세청 전송 정책" 을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnSendToNTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendToNTS.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendToNTS(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 세금계산서 상태/요약 정보를 확인합니다.
    ' - 세금계산서 상태정보(GetInfo API) 응답항목에 대한 자세한 정보는
    '   "[전자세금계산서 API 연동매뉴얼] > 4.2. (세금)계산서 상태정보 구성" 을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim tiInfo As TaxinvoiceInfo = taxinvoiceService.GetInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp += "itemKey (팝빌 관리번호) : " + tiInfo.itemKey + vbCrLf
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
            tmp += "stateCode (상태코드) : " + CStr(tiInfo.stateCode) + vbCrLf
            tmp += "nstconfirmNum (국세청승인번호) : " + tiInfo.ntsconfirmNum + vbCrLf
            tmp += "ntsresult (국세청 전송결과) : " + tiInfo.ntsresult + vbCrLf
            tmp += "ntssendDT (국세청 전송일시) : " + tiInfo.ntssendDT + vbCrLf
            tmp += "ntsresultDT (국세청 결과 수신일시) : " + tiInfo.ntsresultDT + vbCrLf
            tmp += "ntssendErrCode (전송실패 사유코드) : " + tiInfo.ntssendErrCode + vbCrLf
            tmp += "interOPYN (연동문서 여부) : " + CStr(tiInfo.interOPYN) + vbCrLf
            tmp += "invoicerCorpName (공급자 상호) : " + tiInfo.invoicerCorpName + vbCrLf
            tmp += "invoicerCorpNum (공급자 사업자번호) : " + tiInfo.invoicerCorpNum + vbCrLf
            tmp += "invoicerMgtKey (공급자 문서번호) : " + tiInfo.invoicerMgtKey + vbCrLf
            tmp += "invoicerPrintYN (공급자 인쇄여부) : " + CStr(tiInfo.invoicerPrintYN) + vbCrLf
            tmp += "invoiceeCorpName (공급받는자 상호) : " + tiInfo.invoiceeCorpName + vbCrLf
            tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + tiInfo.invoiceeCorpNum + vbCrLf
            tmp += "invoiceePrintYN (공급받는자 문서번호) : " + CStr(tiInfo.invoiceePrintYN) + vbCrLf
            tmp += "closeDownState (공급받는자 휴폐업상태) : " + CStr(tiInfo.closeDownState) + vbCrLf
            tmp += "closeDownStateDate (공급받는자 휴폐업일자) : " + CStr(tiInfo.closeDownStateDate) + vbCrLf
            tmp += "trusteeCorpName (수탁자 상호) : " + tiInfo.trusteeCorpName + vbCrLf
            tmp += "trusteeCorpNum (수탁자 사업자번호) : " + tiInfo.trusteeCorpNum + vbCrLf
            tmp += "trusteeMgtKey (수탁자 문서번호) : " + tiInfo.trusteeMgtKey + vbCrLf
            tmp += "trusteePrintYN (수탁자 인쇄여부) : " + CStr(tiInfo.trusteePrintYN) + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 대량의 세금계산서 상태/요약 정보를 확인합니다. (최대 1000건)
    ' - 세금계산서 상태정보(GetInfos API) 응답항목에 대한 자세한 정보는
    '   "[전자세금계산서 API 연동매뉴얼]  > 4.2. (세금)계산서 상태정보 구성" 을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서번호 배열, 최대 1000건
        MgtKeyList.Add("20190111-001")
        MgtKeyList.Add("20190111-002")

        Try
            Dim taxinvoiceInfoList As List(Of TaxinvoiceInfo) = taxinvoiceService.GetInfos(txtCorpNum.Text, KeyType, MgtKeyList)

            Dim tmp As String = ""

            For Each tiInfo As TaxinvoiceInfo In taxinvoiceInfoList

                tmp += "itemKey (세금계산서 아이템키) : " + tiInfo.itemKey + vbCrLf
                tmp += "stateCode (상태코드) : " + tiInfo.stateCode.ToString + vbCrLf
                tmp += "taxType (과세형태) : " + tiInfo.taxType + vbCrLf
                tmp += "purposeType (영수/청구) : " + tiInfo.purposeType + vbCrLf
                tmp += "modifyCode  (수정 사유코드) : " + tiInfo.modifyCode.ToString + vbCrLf
                tmp += "issueType (발행형태) : " + tiInfo.issueType + vbCrLf
                tmp += "lateIssueYN (지연발행 여부) : " + tiInfo.lateIssueYN.ToString + vbCrLf
                tmp += "interOPYN (연동문서 여부) : " + tiInfo.interOPYN.ToString + vbCrLf
                tmp += "writeDate (작성일자) : " + tiInfo.writeDate + vbCrLf
                tmp += "invoicerCorpName (공급자 상호) : " + tiInfo.invoicerCorpName + vbCrLf
                tmp += "invoicerCorpNum (공급자 사업자번호) : " + tiInfo.invoicerCorpNum + vbCrLf
                tmp += "invoicerMgtKey (공급자 문서번호) : " + tiInfo.invoicerMgtKey + vbCrLf
                tmp += "invoicerPrintYN (공급자 인쇄여부) : " + tiInfo.invoicerPrintYN.ToString + vbCrLf
                tmp += "invoiceeCorpName (공급받는자 상호) : " + tiInfo.invoiceeCorpName + vbCrLf
                tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + tiInfo.invoiceeCorpNum + vbCrLf
                tmp += "invoiceeMgtKey (공급받는자 문서번호) : " + tiInfo.invoiceeMgtKey + vbCrLf
                tmp += "invoiceePrintYN (공급받는지 인쇄여부) : " + tiInfo.invoiceePrintYN.ToString + vbCrLf
                tmp += "closeDownState (공급받는자 휴폐업상태) : " + tiInfo.closeDownState.ToString + vbCrLf
                tmp += "closeDownStateDate (공급받는자 휴폐업일자) : " + tiInfo.closeDownStateDate + vbCrLf
                tmp += "trusteeCorpName (수탁자 상호) : " + tiInfo.trusteeCorpName + vbCrLf
                tmp += "trusteeCorpNum (수탁자 사업자번호) : " + tiInfo.trusteeCorpNum + vbCrLf
                tmp += "trusteeMgtKey (수탁자 문서번호) : " + tiInfo.trusteeMgtKey + vbCrLf
                tmp += "trusteePrintYN (수탁자 인쇄여부) : " + tiInfo.trusteePrintYN.ToString + vbCrLf
                tmp += "supplyCostTotal (공급가액 합계) : " + tiInfo.supplyCostTotal + vbCrLf
                tmp += "taxTotal (세액 합계) : " + tiInfo.taxTotal + vbCrLf
                tmp += "issueDT (발행일시) : " + tiInfo.issueDT + vbCrLf
                tmp += "stateDT (상태 변경일시) : " + tiInfo.stateDT + vbCrLf
                tmp += "openYN (개봉 여부) : " + tiInfo.openYN.ToString + vbCrLf
                tmp += "openDT (개봉 일시) : " + tiInfo.openDT + vbCrLf
                tmp += "ntsresult (국세청 전송결과) : " + tiInfo.ntsresult + vbCrLf
                tmp += "ntsconfirmNum (국세청승인번호) : " + tiInfo.ntsconfirmNum + vbCrLf
                tmp += "ntssendDT (국세청 전송일시) : " + tiInfo.ntssendDT + vbCrLf
                tmp += "ntsresultDT (국세청 결과 수신일시) : " + tiInfo.ntsresultDT + vbCrLf
                tmp += "ntssendErrCode (전송실패 사유코드) : " + tiInfo.ntssendErrCode + vbCrLf
                tmp += "stateMemo (상태메모) : " + tiInfo.stateMemo + vbCrLf
                tmp += "regDT (임시저장 일시) : " + tiInfo.regDT + vbCrLf + vbCrLf + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 세금계산서 상세정보를 확인합니다.
    ' - 응답항목에 대한 자세한 사항은 "[전자세금계산서 API 연동매뉴얼] > 4.1 세금계산서 구성" 을 참조하시기 바랍니다.
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
            tmp += "issueTiming (발행시점) : " + tiDetailInfo.issueTiming + vbCrLf
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

            tmp += "invoicerMgtKey (공급자 관리번호) : " + tiDetailInfo.invoicerMgtKey + vbCrLf
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
            tmp += "closeDownState (휴폐업상태) : " + CStr(tiDetailInfo.closeDownState) + vbCrLf
            tmp += "closeDownStateDate (휴폐업일자) : " + CStr(tiDetailInfo.closeDownStateDate) + vbCrLf
            tmp += "invoiceeContactName1 (담당자 성명) : " + tiDetailInfo.invoiceeContactName1 + vbCrLf
            tmp += "invoiceeTEL1 (담당자 연락처) : " + tiDetailInfo.invoiceeTEL1 + vbCrLf
            tmp += "invoiceeHP1 (담당자 휴대폰) : " + tiDetailInfo.invoiceeHP1 + vbCrLf
            tmp += "invoiceeEmail1 (담당자 이메일) : " + tiDetailInfo.invoiceeEmail1 + vbCrLf

            tmp += "orgNTSConfirmNum (원본 국세청승인번호) : " + tiDetailInfo.orgNTSConfirmNum + vbCrLf
            tmp += "originalTaxinvoiceKey (원본 팝빌 관리번호) : " + tiDetailInfo.originalTaxinvoiceKey + vbCrLf

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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건을 사용하여 세금계산서 목록을 조회합니다.
    ' - 응답항목에 대한 자세한 사항은 "[전자세금계산서 API 연동매뉴얼] > 4.2. (세금)계산서 상태정보 구성" 을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim State(3) As String
        Dim TType(2) As String
        Dim taxType(3) As String
        Dim IssueType(3) As String

        '[필수] 일자유형, R-등록일시 W-작성일자 I-발행일시 중 택1
        Dim DType As String = "W"

        '[필수] 시작일자, yyyyMMdd
        Dim SDate As String = "20181201"

        '[필수] 종료일자, yyyyMMdd
        Dim EDate As String = "20190110"

        '상태코드 배열, 미기재시 전체상태조회, 문서상태값 3자리숫자 작성
        '2,3번째 와일드카드 가능
        State(0) = "3**"
        State(1) = "4**"
        State(1) = "6**"

        '문서유형 배열, N-일반 M-수정 중 선택, 미기재시 전체조회
        TType(0) = "N"
        TType(1) = "M"

        '과세형태 배열, T-과세, N-면세 Z-영세 중 선택, 미기재시 전체조회
        taxType(0) = "T"
        taxType(1) = "N"
        taxType(2) = "Z"

        '발행형태 배열, N-정발행, R-역발행, T-위수탁
        IssueType(0) = "N"
        IssueType(1) = "R"
        IssueType(2) = "T"

        '지연발행 여부, False - 정상발행분만 조회 / True - 지연발행분만조회 / Nothing - 전체조회
        Dim LateOnly As Boolean = Nothing

        '페이지 번호
        Dim Page As Integer = 1

        '페이지 목록개수, 최대 1000건
        Dim PerPage As Integer = 10

        '정렬방향, D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

        '종사업장번호 유형 S-공급자, B-공급받는자, T-수탁자
        Dim TaxRegIDType As String = "S"

        '종사업장번호, 콤마(,)로 구분하여 구성 ex) 0001,0002
        Dim TaxRegID As String = ""

        '종사업장 유무, 공백-전체조회, 0-종사업장번호 없는경우만 조회, 1-종사업장번호 조건 조회
        Dim TaxRegIDYN As String = ""

        '거래처 조회, 거래처 상호 또는 거래처 사업자등록번호 조회, 공백처리시 전체조회
        Dim QString As String = ""

        '연동문서 여부, 공백-전체조회, 0-일반문서 조회, 1-연동문서 조회
        Dim interOPYN As String = ""

        Try
            Dim tiSearchList As TISearchResult = taxinvoiceService.Search(txtCorpNum.Text, KeyType, DType, SDate, EDate, State, TType, _
                                                                          taxType, IssueType, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, QString, Order, Page, _
                                                                          PerPage, interOPYN, txtUserId.Text)


            Dim tmp As String

            tmp = "code (응답코드) : " + CStr(tiSearchList.code) + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + CStr(tiSearchList.total) + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + CStr(tiSearchList.perPage) + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + CStr(tiSearchList.pageNum) + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + CStr(tiSearchList.pageCount) + vbCrLf
            tmp = tmp + "message (응답메시지) : " + tiSearchList.message + vbCrLf + vbCrLf

            Dim tiInfo As TaxinvoiceInfo

            For Each tiInfo In tiSearchList.list
                tmp = tmp + "itemKey (세금계산서 아이템키) : " + tiInfo.itemKey + vbCrLf
                tmp = tmp + "taxType (과세형태) : " + tiInfo.taxType + vbCrLf
                tmp = tmp + "writeDate (작성일자) : " + tiInfo.writeDate + vbCrLf
                tmp = tmp + "regDT (임시저장 일자) : " + tiInfo.regDT + vbCrLf
                tmp = tmp + "issueType (발행형태) : " + tiInfo.issueType + vbCrLf
                tmp = tmp + "supplyCostTotal (공급가액 합계) : " + tiInfo.supplyCostTotal + vbCrLf
                tmp = tmp + "taxTotal (세액 합계) : " + tiInfo.taxTotal + vbCrLf
                tmp = tmp + "purposeType (영수/청구) : " + tiInfo.purposeType + vbCrLf
                tmp = tmp + "issueDT (발행일시) : " + tiInfo.issueDT + vbCrLf
                tmp = tmp + "lateIssueYN (지연발행 여부) : " + tiInfo.lateIssueYN.ToString + vbCrLf
                tmp = tmp + "openYN (개봉 여부) : " + tiInfo.openYN.ToString + vbCrLf
                tmp = tmp + "openDT (개봉 일시) : " + tiInfo.openDT + vbCrLf
                tmp = tmp + "stateMemo (상태메모) : " + tiInfo.stateMemo + vbCrLf
                tmp = tmp + "stateCode (상태코드) : " + tiInfo.stateCode.ToString + vbCrLf
                tmp = tmp + "modifyCode  (수정 사유코드) : " + tiInfo.modifyCode.ToString + vbCrLf
                tmp = tmp + "interOPYN (연동문서 여부) : " + tiInfo.interOPYN.ToString + vbCrLf
                tmp = tmp + "invoicerCorpName (공급자 상호) : " + tiInfo.invoicerCorpName + vbCrLf
                tmp = tmp + "invoicerCorpNum (공급자 사업자번호) : " + tiInfo.invoicerCorpNum + vbCrLf
                tmp = tmp + "invoicerMgtKey (공급자 문서번호) : " + tiInfo.invoicerMgtKey + vbCrLf
                tmp = tmp + "invoicerPrintYN (공급자 인쇄여부) : " + tiInfo.invoicerPrintYN.ToString + vbCrLf
                tmp = tmp + "invoiceeCorpName (공급받는자 상호) : " + tiInfo.invoiceeCorpName + vbCrLf
                tmp = tmp + "invoiceeCorpNum (공급받는자 사업자번호) : " + tiInfo.invoiceeCorpNum + vbCrLf
                tmp = tmp + "invoiceeMgtKey (공급받는자 문서번호) : " + tiInfo.invoiceeMgtKey + vbCrLf
                tmp = tmp + "invoiceePrintYN (공급받는지 인쇄여부) : " + tiInfo.invoiceePrintYN.ToString + vbCrLf
                tmp = tmp + "closeDownState (공급받는자 휴폐업상태) : " + tiInfo.closeDownState.ToString + vbCrLf
                tmp = tmp + "closeDownStateDate (공급받는자 휴폐업일자) : " + tiInfo.closeDownStateDate + vbCrLf
                tmp = tmp + "trusteeCorpName (수탁자 상호) : " + tiInfo.trusteeCorpName + vbCrLf
                tmp = tmp + "trusteeCorpNum (수탁자 사업자번호) : " + tiInfo.trusteeCorpNum + vbCrLf
                tmp = tmp + "trusteeMgtKey (수탁자 문서번호) : " + tiInfo.trusteeMgtKey + vbCrLf
                tmp = tmp + "trusteePrintYN (수탁자 인쇄여부) : " + tiInfo.trusteePrintYN.ToString + vbCrLf
                tmp = tmp + "stateDT (상태 변경일시) : " + tiInfo.stateDT + vbCrLf
                tmp = tmp + "ntsresult (국세청 전송결과) : " + tiInfo.ntsresult + vbCrLf
                tmp = tmp + "ntsconfirmNum (국세청승인번호) : " + tiInfo.ntsconfirmNum + vbCrLf
                tmp = tmp + "ntssendDT (국세청 전송일시) : " + tiInfo.ntssendDT + vbCrLf
                tmp = tmp + "ntsresultDT (국세청 결과 수신일시) : " + tiInfo.ntsresultDT + vbCrLf
                tmp = tmp + "ntssendErrCode (전송실패 사유코드) : " + tiInfo.ntssendErrCode + vbCrLf

            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 상태 변경이력을 확인합니다.
    ' - 상태 변경이력 확인(GetLogs API) 응답항목에 대한 자세한 정보는
    '   "[전자세금계산서 API 연동매뉴얼] > 3.5.5 상태 변경이력 확인" 을 참조하시기 바랍니다.
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 > 전자세금계산서 > 임시(연동)문서함 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetURL_TBOX.Click

        'TBOX-임시문서함 / SBOX-매출문서함 / PBOX-매입문서함 / WRITE-매출문서함
        Dim TOGO As String = "TBOX"

        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 > 전자세금계산서 > 매출 문서함 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetURL_SBOX.Click

        'TBOX-임시문서함 / SBOX-매출문서함 / PBOX-매입문서함 / WRITE-매출문서함
        Dim TOGO As String = "SBOX"

        Try

            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 > 전자세금계산서 > 매입 문서함 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_PBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetURL_PBOX.Click

        'TBOX-임시문서함 / SBOX-매출문서함 / PBOX-매입문서함 / WRITE-매출문서함
        Dim TOGO As String = "PBOX"

        Try

            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 > 전자세금계산서 > 매출 문서작성 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetURL_WRITE.Click

        'TBOX-임시문서함 / SBOX-매출문서함 / PBOX-매입문서함 / WRITE-매출문서함
        Dim TOGO As String = "WRITE"

        Try

            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, TOGO)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 전자세금계산서 보기 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPopUpURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPopUpURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 전자세금계산서 인쇄팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPrintURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 인쇄(공급받는자) URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnEPrintURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 대량의 세금계산서 인쇄팝업 URL을 반환합니다. (최대 100건)
    ' - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetMassPrintURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '문서번호 배열, 최대 100건
        Dim MgtKeyList As List(Of String) = New List(Of String)
        MgtKeyList.Add("1234")
        MgtKeyList.Add("12345")

        Try
            Dim url As String = taxinvoiceService.GetMassPrintURL(txtCorpNum.Text, KeyType, MgtKeyList, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 공급받는자 메일링크 URL을 반환합니다.
    ' - 메일링크 URL은 유효시간이 존재하지 않습니다.
    '=========================================================================
    Private Sub btnGetEmailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetEmailURL.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetMailURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 로그인 상태로 접근할 수 있는 팝업 URL을 반환합니다.
    ' - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetAccessURL.Click
        Try
            Dim url As String = taxinvoiceService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 인감 및 첨부문서 등록 URL을 반환합니다.
    ' - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetSealURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetSealURL.Click
        Try
            Dim url As String = taxinvoiceService.GetSealURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서에 첨부파일을 등록합니다.
    ' - [임시저장] 상태의 세금계산서만 파일을 첨부할수 있습니다.
    ' - 첨부파일은 최대 5개까지 등록할 수 있습니다.
    '=========================================================================
    Private Sub btnAttachFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnAttachFile.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then
            Dim strFileName As String = fileDialog.FileName

            Try
                Dim response As Response = taxinvoiceService.AttachFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, strFileName, txtUserId.Text)

                MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

            End Try

        End If
    End Sub

    '=========================================================================
    ' 세금계산서에 첨부된 파일을 삭제합니다.
    ' - 파일을 식별하는 파일아이디는 첨부파일 목록(GetFiles API) 의 응답항목 중 파일아이디(AttachedFile) 값을 통해 확인할 수 있습니다.
    '=========================================================================
    Private Sub btnDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnDeleteFile.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.DeleteFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtFileID.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 첨부파일 목록을 확인합니다.
    ' - 응답항목 중 파일아이디(AttachedFile) 항목은 파일삭제(DeleteFile API) 호출시 이용할 수 있습니다.
    '=========================================================================
    Private Sub btnGetFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFiles.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim fileList As List(Of AttachedFile) = taxinvoiceService.GetFiles(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            Dim tmp As String = "serialNum(일련번호) | displayName(첨부파일명) | attachedFile(파일아이디) | regDT(등록일자)" + vbCrLf

            For Each file As AttachedFile In fileList
                tmp += file.serialNum.ToString() + " | " + file.displayName + " | " + file.attachedFile + " | " + file.regDT + vbCrLf

            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub


    '=========================================================================
    ' 세금계산서 발행안내 메일을 재전송합니다.
    '=========================================================================
    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendEmail.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '수신자 이메일주소
        Dim Receiver As String = "test@test.com"

        Try
            Dim response As Response = taxinvoiceService.SendEmail(txtCorpNum.Text, KeyType, txtMgtKey.Text, Receiver, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 알림문자를 전송합니다. (단문/SMS - 한글 최대 45자)
    ' - 알림문자 전송시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [문자] > [전송내역] 메뉴에서 전송결과를 확인할 수 있습니다.
    '=========================================================================
    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발신번호
        Dim sendNum As String = "070-1234-1234"

        '수신번호
        Dim receiveNum As String = "010-1111-2222"

        '메시지내용, 90byte(한글45자) 초과된 내용은 삭제되어 전송됨
        Dim contents As String = "발신문자 메시지 내용"

        Try
            Dim response As Response = taxinvoiceService.SendSMS(txtCorpNum.Text, KeyType, txtMgtKey.Text, sendNum, receiveNum, contents, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자세금계산서를 팩스전송합니다.
    ' - 팩스 전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인할 수 있습니다.
    '=========================================================================
    Private Sub btnSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFAX.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '발신번호
        Dim sendNum As String = "070-1234-1234"

        '수신번호
        Dim receiveNum As String = "010-1111-2222"

        Try
            Dim response As Response = taxinvoiceService.SendFAX(txtCorpNum.Text, KeyType, txtMgtKey.Text, sendNum, receiveNum, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 전자명세서를 세금계산서에 첨부합니다.
    '=========================================================================
    Private Sub btnAttachStatement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnAttachStatement.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '첨부 대상 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim docItemCode As Integer = 121

        '첨부 대상 전자명세서 관리번호
        Dim docMgtKey As String = "20171117-02"

        Try
            Dim response As Response = taxinvoiceService.AttachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text, docItemCode, docMgtKey)
            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서에 첨부된 전자명세서 1건을 첨부해제합니다.
    '=========================================================================
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '첨부해제 대상 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim docItemCode As Integer = 121

        '첨부해제 대상 전자명세서 관리번호
        Dim docMgtKey As String = "20171117-02"

        Try
            Dim response As Response = taxinvoiceService.DetachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text, docItemCode, docMgtKey)
            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 대용량 연계사업자 유통메일주소 목록을 반환합니다.
    '=========================================================================
    Private Sub btnGetEmailPublicKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetEmailPublicKey.Click

        Try
            Dim KeyList As List(Of EmailPublicKey) = taxinvoiceService.GetEmailPublicKeys(txtCorpNum.Text)

            MsgBox(KeyList.Count.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌사이트에서 작성된 세금계산서에 파트너 문서번호를 할당합니다.
    '=========================================================================
    Private Sub btnAssignMgtKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnAssignMgtKey.Click

        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '세금계산서 아이템키, 목록조회(Search) API의 반환항목중 ItemKey 참조
        Dim itemKey As String = "018041823295700001"

        '관리번호가 없는 문서에 할당할 문서번호
        '- 관리번호는 1~24자리로 숫자, 영문 '-', '_' 조합으로 구성할 수 있습니다.
        Dim mgtKey As String = "20190111-A00"

        Try
            Dim response As Response = taxinvoiceService.AssignMgtKey(txtCorpNum.Text, KeyType, itemKey, mgtKey, txtUserId.Text)
            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '전자세금계산서 메일전송 항목에 대한 전송여부를 목록으로 반환합니다.
    '=========================================================================
    Private Sub btnListEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnListEmailConfig.Click
        Try
            Dim emailConfigList As List(Of EmailConfig) = taxinvoiceService.ListEmailConfig(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "메일전송유형 | 전송여부 " + vbCrLf

            For Each info As EmailConfig In emailConfigList
                If info.emailType = "TAX_ISSUE" Then _
                    tmp += "[정발행] TAX_ISSUE (공급받는자에게 전자세금계산서 발행 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_ISSUE_INVOICER" Then _
                    tmp += "[정발행] TAX_ISSUE_INVOICER (공급자에게 전자세금계산서 발행 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_CHECK" Then _
                    tmp += "[정발행] TAX_CHECK (공급자에게 전자세금계산서 수신확인 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_CANCEL_ISSUE" Then _
                    tmp += "[정발행] TAX_CANCEL_ISSUE (공급받는자에게 전자세금계산서 발행취소 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_SEND" Then _
                    tmp += "[발행예정] TAX_SEND (공급받는자에게 [발행예정] 세금계산서 발송 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_ACCEPT" Then _
                    tmp += "[발행예정] TAX_ACCEPT (공급자에게 [발행예정] 세금계산서 승인 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_ACCEPT_ISSUE" Then _
                    tmp += "[발행예정] TAX_ACCEPT_ISSUE (공급자에게 [발행예정] 세금계산서 자동발행 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_DENY" Then _
                    tmp += "[발행예정] TAX_DENY (공급자에게 [발행예정] 세금계산서 거부 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_CANCEL_SEND" Then _
                    tmp += "[발행예정] TAX_CANCEL_SEND (공급받는자에게 [발행예정] 세금계산서 취소 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_REQUEST" Then _
                    tmp += "[역발행] TAX_REQUEST (공급자에게 세금계산서를 발행요청 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_CANCEL_REQUEST" Then _
                    tmp += "[역발행] TAX_CANCEL_REQUEST (공급받는자에게 세금계산서 취소 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_REFUSE" Then _
                    tmp += "[역발행] TAX_REFUSE (공급받는자에게 세금계산서 거부 메일) | " + info.sendYN.ToString + vbCrLf
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
                If info.emailType = "TAX_TRUST_SEND" Then _
                    tmp += "[위수탁 발행예정] TAX_TRUST_SEND (공급받는자에게 [발행예정] 세금계산서 발송 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_TRUST_ACCEPT" Then _
                    tmp += "[위수탁 발행예정] TAX_TRUST_ACCEPT (수탁자에게 [발행예정] 세금계산서 승인 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_TRUST_ACCEPT_ISSUE" Then _
                    tmp += "[위수탁 발행예정] TAX_TRUST_ACCEPT_ISSUE (수탁자에게 [발행예정] 세금계산서 자동발행 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_TRUST_DENY" Then _
                    tmp += "[위수탁 발행예정] TAX_TRUST_DENY (수탁자에게 [발행예정] 세금계산서 거부 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_TRUST_CANCEL_SEND" Then _
                    tmp += "[위수탁 발행예정] TAX_TRUST_CANCEL_SEND (공급받는자에게 [발행예정] 세금계산서 취소 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_CLOSEDOWN" Then _
                    tmp += "[처리결과] TAX_CLOSEDOWN (거래처의 휴폐업 여부 확인 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_NTSFAIL_INVOICER" Then _
                    tmp += "[처리결과] TAX_NTSFAIL_INVOICER (전자세금계산서 국세청 전송실패 안내) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "TAX_SEND_INFO" Then _
                    tmp += "[정기발송] TAX_SEND_INFO (전월 귀속분 [매출 발행 대기] 세금계산서 발행 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "ETC_CERT_EXPIRATION" Then _
                    tmp += "[정기발송] ETC_CERT_EXPIRATION (팝빌에서 이용중인 공인인증서의 갱신 메일) | " + info.sendYN.ToString + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '전자세금계산서 메일전송 항목에 대한 전송여부를 수정합니다.
    '메일전송유형
    '[정발행]
    'TAX_ISSUE : 공급받는자에게 전자세금계산서 발행 메일 입니다.
    'TAX_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행 메일 입니다.
    'TAX_CHECK : 공급자에게 전자세금계산서 수신확인 메일 입니다.
    'TAX_CANCEL_ISSUE : 공급받는자에게 전자세금계산서 발행취소 메일 입니다.

    '[발행예정]
    'TAX_SEND : 공급받는자에게 [발행예정] 세금계산서 발송 메일 입니다.
    'TAX_ACCEPT : 공급자에게 [발행예정] 세금계산서 승인 메일 입니다.
    'TAX_ACCEPT_ISSUE : 공급자에게 [발행예정] 세금계산서 자동발행 메일 입니다.
    'TAX_DENY : 공급자에게 [발행예정] 세금계산서 거부 메일 입니다.
    'TAX_CANCEL_SEND : 공급받는자에게 [발행예정] 세금계산서 취소 메일 입니다.

    '[역발행]
    'TAX_REQUEST : 공급자에게 세금계산서를 발행요청 메일 입니다.
    'TAX_CANCEL_REQUEST : 공급받는자에게 세금계산서 취소 메일 입니다.
    'TAX_REFUSE : 공급받는자에게 세금계산서 거부 메일 입니다.

    '[위수탁발행]
    'TAX_TRUST_ISSUE : 공급받는자에게 전자세금계산서 발행 메일 입니다.
    'TAX_TRUST_ISSUE_TRUSTEE : 수탁자에게 전자세금계산서 발행 메일 입니다.
    'TAX_TRUST_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행 메일 입니다.
    'TAX_TRUST_CANCEL_ISSUE : 공급받는자에게 전자세금계산서 발행취소 메일 입니다.
    'TAX_TRUST_CANCEL_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행취소 메일 입니다.

    '[위수탁 발행예정]
    'TAX_TRUST_SEND : 공급받는자에게 [발행예정] 세금계산서 발송 메일 입니다.
    'TAX_TRUST_ACCEPT : 수탁자에게 [발행예정] 세금계산서 승인 메일 입니다.
    'TAX_TRUST_ACCEPT_ISSUE : 수탁자에게 [발행예정] 세금계산서 자동발행 메일 입니다.
    'TAX_TRUST_DENY : 수탁자에게 [발행예정] 세금계산서 거부 메일 입니다.
    'TAX_TRUST_CANCEL_SEND : 공급받는자에게 [발행예정] 세금계산서 취소 메일 입니다.

    '[처리결과]
    'TAX_CLOSEDOWN : 거래처의 휴폐업 여부 확인 메일 입니다.
    'TAX_NTSFAIL_INVOICER : 전자세금계산서 국세청 전송실패 안내 메일 입니다.

    '[정기발송]
    'TAX_SEND_INFO : 전월 귀속분 [매출 발행 대기] 세금계산서 발행 메일 입니다.
    'ETC_CERT_EXPIRATION : 팝빌에서 이용중인 공인인증서의 갱신 메일 입니다.
    '=========================================================================
    Private Sub btnUpdateEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUpdateEmailConfig.Click
        Try
            '메일전송유형
            Dim emailType As String = "TAX_ISSUE"

            '전송여부 (True-전송, False-미전송)
            Dim sendYN As Boolean = True

            Dim response As Response = taxinvoiceService.UpdateEmailConfig(txtCorpNum.Text, emailType, sendYN,txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 회원의 공인인증서를 등록하는 팝업 URL을 반환합니다.
    ' - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
    ' - 팝빌에 등록된 공인인증서가 유효하지 않은 경우 (비밀번호 변경, 인증서 재발급/갱신, 만료일 경과)
    '   인증서를 재등록해야 정상적으로 전자세금계산서 발행이 가능합니다.
    '=========================================================================
    Private Sub btnGetTaxCertURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetTaxCertURL.Click
        Try
            Dim url As String = taxinvoiceService.GetTaxCertURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)

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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록되어 있는 공인인증서의 만료일자를 확인합니다.
    ' - 공인인증서가 갱신/재발급/비밀번호 변경이 되는 경우 해당 인증서를
    '   재등록 하셔야 정상적으로 세금계산서를 발행할 수 있습니다.
    '=========================================================================
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetCertificateExpireDate.Click
        Try
            Dim expiration As DateTime = taxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text)

            MsgBox("공인인증서 만료일시 : " + expiration.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록된 공인인증서의 유효성을 확인합니다.
    '=========================================================================
    Private Sub btnCheckCertValidation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckCertValidation.Click
        Try
            Dim response As Response = taxinvoiceService.CheckCertValidation(txtCorpNum.Text, txtUserId.Text)

            MessageBox.Show("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MessageBox.Show("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)를 통해 확인하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = taxinvoiceService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전 URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetChargeURL.Click
        Try
            Dim url As String = taxinvoiceService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = taxinvoiceService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전 팝업 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = taxinvoiceService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자세금계산서 발행단가를 확인합니다.
    '=========================================================================
    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            Dim unitCost As Single = taxinvoiceService.GetUnitCost(txtCorpNum.Text)

            MsgBox("세금계산서 발행단가(unitCost) : " + unitCost.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 전자세금계산서 API 서비스 과금정보를 확인합니다.
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
    ' - LinkID는 인증정보로 설정되어 있는 링크아이디 값입니다.
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckIsMember.Click
        Try
            Dim response As Response = taxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 회원아이디 중복여부를 확인합니다.
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = taxinvoiceService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 연동회원으로 회원가입을 요청합니다.
    '=========================================================================
    Private Sub btnJoinMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnJoinMember.Click
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
            Dim response As Response = taxinvoiceService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다
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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자를 신규로 등록합니다.
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRegistContact.Click

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
            Dim response As Response = taxinvoiceService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자 목록을 확인합니다.
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = taxinvoiceService.ListContact(txtCorpNum.Text, txtUserId.Text)

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
            Dim response As Response = taxinvoiceService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 1건의 전자세금계산서 보기 팝업 URL을 반환합니다. (메뉴/버튼 출력되지 않음)
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetViewURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetViewURL.Click
        '세금계산서 발행유형, MgtKeyType [SELL-매출 /  BUY-매입 / TRUSTEE-위수탁]
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetViewURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub
End Class