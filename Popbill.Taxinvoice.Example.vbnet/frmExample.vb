
'=========================================================================
'
' 팝빌 전자세금계산서 API VB.Net SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내
' - 업데이트 일자 : 2017-12-05
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
'
' <테스트 연동개발 준비사항>
' 1) 40, 43번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
' 3) 전자세금계산서 발행을 위해 공인인증서를 등록합니다.
'    - 팝빌사이트 로그인 > [전자세금계산서] > [환경설정]
'      > [공인인증서 관리]
'    - 공인인증서 등록 팝업 URL (GetPopbillURL API)을 이용하여 등록
'
'=========================================================================

Imports Popbill
Imports Popbill.Taxinvoice
Imports System.ComponentModel

Public Class frmExample

    '링크아이디
    Private LinkID As String = "TESTER"

    '비밀키
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '세금계산서 서비스 변수 선언
    Private taxinvoiceService As TaxinvoiceService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '세금계산서 서비스 객체 초기화
        taxinvoiceService = New TaxinvoiceService(LinkID, SecretKey)

        '연동환경 설정값 (True-개발용, False-상업용)
        taxinvoiceService.IsTest = True

    End Sub


    '=========================================================================
    ' 파트너의 연동회원으로 회원가입을 요청합니다.
    '=========================================================================
    Private Sub btnJoinMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJoinMember.Click
        Dim joinInfo As JoinForm = New JoinForm

        '링크아이디
        joinInfo.LinkID = LinkID

        '사업자번호, '-'제외 10자리
        joinInfo.CorpNum = "0000000105"

        '대표자성명
        joinInfo.CEOName = "대표자성명"

        '상호
        joinInfo.CorpName = "상호"

        '주소
        joinInfo.Addr = "주소"

        '업태
        joinInfo.BizType = "업태"

        '종목
        joinInfo.BizClass = "종목"

        '아이디
        joinInfo.ID = "userid1120"

        '비밀번호
        joinInfo.PWD = "pwd_must_be_long_enough"

        '담당자명
        joinInfo.ContactName = "담당자명"

        '담당자 연락처
        joinInfo.ContactTEL = "02-999-9999"

        '담당자 휴대폰번호
        joinInfo.ContactHP = "010-1234-5678"

        '담당자 메일주소
        joinInfo.ContactEmail = "test@test.com"

        Try
            Dim response As Response = taxinvoiceService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)
    '   를 통해 확인하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = taxinvoiceService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를
    '   이용하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = taxinvoiceService.GetPartnerBalance(txtCorpNum.Text)


            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())


        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
    ' - LinkID는 인증정보로 설정되어 있는 링크아이디 값입니다.
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = taxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록되어 있는 공인인증서의 만료일자를 확인합니다.
    ' - 공인인증서가 갱신/재발급/비밀번호 변경이 되는 경우 해당 인증서를
    '   재등록 하셔야 정상적으로 API를 이용하실 수 있습니다.
    '=========================================================================
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCertificateExpireDate.Click
        Try
            Dim expiration As DateTime = taxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text)


            MsgBox("공인인증서 만료일시 : " + expiration.ToString())


        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 관리번호 중복여부를 확인합니다.
    ' - 관리번호는 1~24자리로 숫자, 영문 '-', '_' 조합으로 구성할 수 있습니다.
    '=========================================================================
    Private Sub btnCheckMgtKeyInUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckMgtKeyInUse.Click
        Dim KeyType As MgtKeyType


        KeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        Try
            Dim InUse As Boolean = taxinvoiceService.CheckMgtKeyInUse(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox(IIf(InUse, "사용중", "미사용중"))

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 대용량 연계사업자 메일주소 목록을 반환합니다.
    '=========================================================================
    Private Sub btnGetEmailPublicKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEmailPublicKey.Click

        Try
            Dim KeyList As List(Of EmailPublicKey) = taxinvoiceService.GetEmailPublicKeys(txtCorpNum.Text)

            MsgBox(KeyList.Count.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 1건의 세금계산서를 임시저장 합니다.
    ' - 세금계산서 임시저장(Register API) 호출후에는 발행(Issue API)을 호출해야만
    '   국세청으로 전송됩니다.
    ' - 임시저장과 발행을 한번의 호출로 처리하는 즉시발행(RegistIssue API) 프로세스
    '   연동을 권장합니다.
    ' - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서
    '   구성"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20171120
        taxinvoice.writeDate = "20171120"

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
        taxinvoice.invoicerCorpNum = "1234567890"

        '[필수] 공급자 종사업자 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '[필수] 공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '[필수] 공급자 문서관리번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
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

        '[역발행시 필수] 공급받는자 문서관리번호(역발행시 필수)
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

        '기재 상 '권' 항목, 최대값 32767
        taxinvoice.kwon = 1

        '기재 상 '호' 항목, 최대값 32767
        taxinvoice.ho = 1

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
        ' - [참고] 수정세금계산서 작성방법 안내 - http://blog.linkhub.co.kr/650
        '========================================================================='

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 ItemKey, 문서확인 (GetInfo API)의 응답결과(ItemKey 항목) 확인
        taxinvoice.originalTaxinvoiceKey = ""


        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "20171121"                 '거래일자, yyyyMMdd
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
            Dim response As Response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text, False)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try

    End Sub

    '=========================================================================
    ' 1건의 전자세금계산서를 삭제합니다.
    ' - 세금계산서를 삭제해야만 문서관리번호(mgtKey)를 재사용할 수 있습니다.
    ' - 삭제가능한 문서 상태 : [임시저장], [발행취소], [발행예정 취소],
    '   [발행예정 거부]
    '=========================================================================
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click, btnDelete_Reverse.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        Try
            Dim response As Response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 1건의 [임시저장] 상태의 세금계산서를 [발행예정] 처리합니다.
    ' - 발행예정이란 공급자와 공급받는자 사이에 세금계산서 확인 후 발행하는
    '   방법입니다.
    ' - "[전자세금계산서 API 연동매뉴얼] > 1.3.1. 정발행 프로세스 흐름도
    '   > 다. 임시저장 발행예정" 의 프로세스를 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim Memo As String = "발행예정 메모"

        '발행예정 메일제목, 공백으로 처리시 기본메일 제목으로 전송ㄴ
        Dim EmailSubject As String = "발행예정 메일제목 테스트 dotent 3.5"

        Try
            Dim response As Response = taxinvoiceService.Send(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, EmailSubject, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 발행예정 세금계산서를 [취소] 처리 합니다.
    ' - [취소]된 세금계산서를 삭제(Delete API)하면 등록된 문서관리번호를
    '   재사용할 수 있습니다.
    '=========================================================================
    Private Sub btnCancelSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSend.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.CancelSend(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행예정 취소시 메모.", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 세금계산서 상세항목을 확인합니다.
    ' - 응답항목에 대한 자세한 사항은 "[전자세금계산서 API 연동매뉴얼]
    '   > 4.1 (세금)계산서 구성" 을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetDetailInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDetailInfo.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim tiDetailInfo As Taxinvoice = taxinvoiceService.GetDetailInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            '자세한 문세정보는 작성시 항목을 참조하거나, 연동메뉴얼 참조.

            Dim tmp As String = ""

            tmp += "writeDate (작성일자) : " + tiDetailInfo.writeDate + vbCrLf
            tmp += "chargeDirection (과금방향) : " + tiDetailInfo.chargeDirection + vbCrLf
            tmp += "issueType (발행형태) : " + tiDetailInfo.issueType + vbCrLf
            tmp += "issueTiming (발행시점) : " + tiDetailInfo.issueTiming + vbCrLf
            tmp += "taxType (과세형태) : " + tiDetailInfo.taxType + vbCrLf

            tmp += "invoicerCorpNum (공급자 사업자번호) : " + tiDetailInfo.invoicerCorpNum + vbCrLf
            tmp += "invoicerMgtKey (공급자 문서관리번호) : " + tiDetailInfo.invoicerMgtKey + vbCrLf
            tmp += "invoicerTaxRegID (공급자 종사업장 식별번호) : " + tiDetailInfo.invoicerTaxRegID + vbCrLf
            tmp += "invoicerCorpName (공급자 상호) : " + tiDetailInfo.invoicerCorpName + vbCrLf
            tmp += "invoicerCEOName (공급자 대표자명) : " + tiDetailInfo.invoicerCEOName + vbCrLf
            tmp += "invoicerAddr (공급자 주소) : " + tiDetailInfo.invoicerAddr + vbCrLf
            tmp += "invoicerBizClass (공급자 종목) : " + tiDetailInfo.invoicerBizClass + vbCrLf
            tmp += "invoicerBizType (공급자 업태) : " + tiDetailInfo.invoicerBizType + vbCrLf
            tmp += "invoicerContactName (공급자 담당자명) : " + tiDetailInfo.invoicerContactName + vbCrLf
            tmp += "invoicerTEL (공급자 담당자 연락처) : " + tiDetailInfo.invoicerTEL + vbCrLf
            tmp += "invoicerHP (공급자 담당자 휴대폰) : " + tiDetailInfo.invoicerHP + vbCrLf
            tmp += "invoicerEmail (공급자 담당자 이메일) : " + tiDetailInfo.invoicerEmail + vbCrLf
            tmp += "invoicerSMSSendYN (안내문자 전송여부) : " + CStr(tiDetailInfo.invoicerSMSSendYN) + vbCrLf

            tmp += "invoiceeType (공급받는자 구분) : " + tiDetailInfo.invoiceeType + vbCrLf
            tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + tiDetailInfo.invoiceeCorpNum + vbCrLf
            tmp += "invoiceeMgtKey (공급받는자 문서관리번호) : " + tiDetailInfo.invoiceeMgtKey + vbCrLf
            tmp += "invoiceeTaxRegID (공급받는자 종사업장 식별번호) : " + tiDetailInfo.invoiceeTaxRegID + vbCrLf
            tmp += "invoiceeCorpName (공급받는자 상호) : " + tiDetailInfo.invoiceeCorpName + vbCrLf
            tmp += "invoiceeCEOName (공급받는자 대표자성명) : " + tiDetailInfo.invoiceeCEOName + vbCrLf
            tmp += "invoiceeAddr (공급받는자 주소) : " + tiDetailInfo.invoiceeAddr + vbCrLf
            tmp += "invoiceeBizClass (공급받는자 종목) : " + tiDetailInfo.invoiceeBizClass + vbCrLf
            tmp += "invoiceeBizType (공급받는자 업태) : " + tiDetailInfo.invoiceeBizType + vbCrLf
            tmp += "invoiceeContactName1 (공급받는자 담당자명) : " + tiDetailInfo.invoiceeContactName1 + vbCrLf
            tmp += "invoiceeTEL1 (공급받는자 담당자 연락처) : " + tiDetailInfo.invoiceeTEL1 + vbCrLf
            tmp += "invoiceeHP1 (공급받는자 담당자 휴대폰) : " + tiDetailInfo.invoiceeHP1 + vbCrLf
            tmp += "invoiceeEmail1 (공급받는자 담당자 메일) : " + tiDetailInfo.invoiceeEmail1 + vbCrLf
            tmp += "closeDownState (공급받는자 휴폐업상태) : " + CStr(tiDetailInfo.closeDownState) + vbCrLf
            tmp += "closeDownStateDate (공급받는자 휴폐업일자) : " + tiDetailInfo.closeDownStateDate + vbCrLf

            ' 상세내역 생략 

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '1건의 세금계산서 상태/요약 정보를 확인합니다.
    ' - 세금계산서 상태정보(GetInfo API) 응답항목에 대한 자세한 정보는
    '  "[전자세금계산서 API 연동매뉴얼] > 4.2. (세금)계산서 상태정보 구성"
    '   을 참조하시기 바랍니다.
    '=========================================================================ㄴ
    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim tiInfo As TaxinvoiceInfo = taxinvoiceService.GetInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp += "itemKey (세금계산서 아이템키) : " + tiInfo.itemKey + vbCrLf
            tmp += "stateCode (상태코드) : " + CStr(tiInfo.stateCode) + vbCrLf
            tmp += "taxType (과세형태) : " + tiInfo.taxType + vbCrLf
            tmp += "purposeType (영수/청구) : " + tiInfo.purposeType + vbCrLf
            tmp += "modifyCode  (수정 사유코드) : " + tiInfo.modifyCode + vbCrLf
            tmp += "issueType (발행형태) : " + tiInfo.issueType + vbCrLf
            tmp += "lateIssueYN (지연발행 여부) : " + CStr(tiInfo.lateIssueYN) + vbCrLf
            tmp += "interOPYN (연동문서 여부) : " + CStr(tiInfo.interOPYN) + vbCrLf

            tmp += "writeDate (작성일자) : " + tiInfo.writeDate + vbCrLf

            tmp += "invoicerCorpName (공급자 상호) : " + tiInfo.invoicerCorpName + vbCrLf
            tmp += "invoicerCorpNum (공급자 사업자번호) : " + tiInfo.invoicerCorpNum + vbCrLf
            tmp += "invoicerMgtKey (공급자 문서관리번호) : " + tiInfo.invoicerMgtKey + vbCrLf
            tmp += "invoicerPrintYN (공급자 인쇄여부) : " + CStr(tiInfo.invoicerPrintYN) + vbCrLf

            tmp += "invoiceeCorpName (공급받는자 상호) : " + tiInfo.invoiceeCorpName + vbCrLf
            tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + tiInfo.invoiceeCorpNum + vbCrLf
            tmp += "invoiceeMgtKey (공급받는자 문서관리번호) : " + tiInfo.invoiceeMgtKey + vbCrLf
            tmp += "invoiceePrintYN (공급받는지 인쇄여부) : " + CStr(tiInfo.invoiceePrintYN) + vbCrLf
            tmp += "closeDownState (공급받는자 휴폐업상태) : " + CStr(tiInfo.closeDownState) + vbCrLf
            tmp += "closeDownStateDate (공급받는자 휴폐업일자) : " + tiInfo.closeDownStateDate + vbCrLf

            tmp += "trusteeCorpName (수탁자 상호) : " + tiInfo.trusteeCorpName + vbCrLf
            tmp += "trusteeCorpNum (수탁자 사업자번호) : " + tiInfo.trusteeCorpNum + vbCrLf
            tmp += "trusteeMgtKey (수탁자 문서관리번호) : " + tiInfo.trusteeMgtKey + vbCrLf
            tmp += "trusteePrintYN (수탁자 인쇄여부) : " + CStr(tiInfo.trusteePrintYN) + vbCrLf

            tmp += "supplyCostTotal (공급가액 합계) : " + tiInfo.supplyCostTotal + vbCrLf
            tmp += "taxTotal (세액 합계) : " + tiInfo.taxTotal + vbCrLf

            tmp += "issueDT (발행일시) : " + tiInfo.issueDT + vbCrLf
            tmp += "preIssueDT (발행예정일시) : " + tiInfo.preIssueDT + vbCrLf
            tmp += "stateDT (상태 변경일시) : " + tiInfo.stateDT + vbCrLf
            tmp += "openYN (개봉 여부) : " + CStr(tiInfo.openYN) + vbCrLf
            tmp += "openDT (개봉 일시) : " + tiInfo.openDT + vbCrLf

            tmp += "ntsresult (국세청 전송결과) : " + tiInfo.ntsresult + vbCrLf
            tmp += "ntsconfirmNum (국세청승인번호) : " + tiInfo.ntsconfirmNum + vbCrLf
            tmp += "ntssendDT (국세청 전송일시) : " + tiInfo.ntssendDT + vbCrLf
            tmp += "ntsresultDT (국세청 결과 수신일시) : " + tiInfo.ntsresultDT + vbCrLf
            tmp += "ntssendErrCode (전송실패 사유코드) : " + tiInfo.ntssendErrCode + vbCrLf

            tmp += "stateMemo (상태메모) : " + tiInfo.stateMemo + vbCrLf
            tmp += "regDT (임시저장 일시) : " + tiInfo.regDT + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 > 임시(연동)문서함 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TBOX.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 팝빌 > 매출 문서함 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_SBOX.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "SBOX")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 > 매입 문서함 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_PBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_PBOX.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 > 매출 문서작성 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_WRITE.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 상태 변경이력을 확인합니다.
    ' - 상태 변경이력 확인(GetLogs API) 응답항목에 대한 자세한 정보는
    '   "[전자세금계산서 API 연동매뉴얼] > 3.6.4 상태 변경이력 확인"
    '   을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetLogs.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim logList As List(Of TaxinvoiceLog) = taxinvoiceService.GetLogs(txtCorpNum.Text, KeyType, txtMgtKey.Text)


            Dim tmp As String = ""


            For Each log As TaxinvoiceLog In logList
                tmp += log.docLogType.ToString + " | " + log.log + " | " + log.procType + " | " + log.procCorpName + " | " + log.procContactName + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 다량의 세금계산서 상태/요약 정보를 확인합니다. (최대 1000건)
    ' - 세금계산서 상태정보(GetInfos API) 응답항목에 대한 자세한 정보는
    '  "[전자세금계산서 API 연동매뉴얼] > 4.2. (세금)계산서 상태정보 구성"
    '  을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서관리번호 배열, 최대 1000건
        MgtKeyList.Add("20171121-01")
        MgtKeyList.Add("20171121-02")

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
                tmp += "invoicerMgtKey (공급자 문서관리번호) : " + tiInfo.invoicerMgtKey + vbCrLf
                tmp += "invoicerPrintYN (공급자 인쇄여부) : " + tiInfo.invoicerPrintYN.ToString + vbCrLf

                tmp += "invoiceeCorpName (공급받는자 상호) : " + tiInfo.invoiceeCorpName + vbCrLf
                tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + tiInfo.invoiceeCorpNum + vbCrLf
                tmp += "invoiceeMgtKey (공급받는자 문서관리번호) : " + tiInfo.invoiceeMgtKey + vbCrLf
                tmp += "invoiceePrintYN (공급받는지 인쇄여부) : " + tiInfo.invoiceePrintYN.ToString + vbCrLf
                tmp += "closeDownState (공급받는자 휴폐업상태) : " + tiInfo.closeDownState.ToString + vbCrLf
                tmp += "closeDownStateDate (공급받는자 휴폐업일자) : " + tiInfo.closeDownStateDate + vbCrLf

                tmp += "trusteeCorpName (수탁자 상호) : " + tiInfo.trusteeCorpName + vbCrLf
                tmp += "trusteeCorpNum (수탁자 사업자번호) : " + tiInfo.trusteeCorpNum + vbCrLf
                tmp += "trusteeMgtKey (수탁자 문서관리번호) : " + tiInfo.trusteeMgtKey + vbCrLf
                tmp += "trusteePrintYN (수탁자 인쇄여부) : " + tiInfo.trusteePrintYN.ToString + vbCrLf

                tmp += "supplyCostTotal (공급가액 합계) : " + tiInfo.supplyCostTotal + vbCrLf
                tmp += "taxTotal (세액 합계) : " + tiInfo.taxTotal + vbCrLf

                tmp += "issueDT (발행일시) : " + tiInfo.issueDT + vbCrLf
                tmp += "preIssueDT (발행예정일시) : " + tiInfo.preIssueDT + vbCrLf
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try


    End Sub

    '=========================================================================
    ' 발행 안내메일을 재전송합니다.
    ' - 메일내용중 전자세금계산서 [보기] 버튼이 동작하지 않는 경우,
    '   키보드 왼쪽 Shift 키를 누르고 버튼을 클릭해보시기 바랍니다.
    '=========================================================================
    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        Try
            Dim response As Response = taxinvoiceService.SendEmail(txtCorpNum.Text, KeyType, txtMgtKey.Text, "test@test.com", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 알림문자를 전송합니다. (단문/SMS- 한글 최대 45자)
    ' - 알림문자 전송시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [전송내역] 탭에서
    '   전송결과를 확인할 수 있습니다.
    '=========================================================================
    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendSMS(txtCorpNum.Text, KeyType, txtMgtKey.Text, "1111-2222", "111-2222-4444", "발신문자 내용...", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 전자세금계산서를 팩스전송합니다.
    ' - 팩스 전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [팩스] > [전송내역]
    '   메뉴에서 전송결과를 확인할 수 있습니다.
    '=========================================================================
    Private Sub btnSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFAX.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendFAX(txtCorpNum.Text, KeyType, txtMgtKey.Text, "1111-2222", "000-2222-4444", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 전자세금계산서 보기 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPopUpURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 1건의 전자세금계산서 인쇄팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try

    End Sub

    '=========================================================================
    ' 세금계산서 인쇄(공급받는자) URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEPrintURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 공급받는자 메일링크 URL을 반환합니다.
    ' - 메일링크 URL은 유효시간이 존재하지 않습니다.
    '=========================================================================
    Private Sub btnGetEmailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEmailURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetMailURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 다수건의 전자세금계산서 인쇄팝업 URL을 반환합니다. (최대 100건)
    ' 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMassPrintURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        '문서관리번호 배열, 최대 100건
        Dim MgtKeyList As List(Of String) = New List(Of String)
        MgtKeyList.Add("1234")
        MgtKeyList.Add("12345")

        Try
            Dim url As String = taxinvoiceService.GetMassPrintURL(txtCorpNum.Text, KeyType, MgtKeyList, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' [발행완료] 상태의 세금계산서를 국세청으로 즉시전송합니다.
    ' - 국세청 즉시전송을 호출하지 않은 세금계산서는 발행일 기준 익일 오후 3시에
    '   팝빌 시스템에서 일괄적으로 국세청으로 전송합니다.
    ' - 익일전송시 전송일이 법정공휴일인 경우 다음 영업일에 전송됩니다.
    ' - 국세청 전송에 관한 사항은 "[전자세금계산서 API 연동매뉴얼] > 1.4 국세청
    '   전송 정책" 을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnSendToNTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendToNTS.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendToNTS(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [임시저장] 상태의 세금계산서를 [발행]처리 합니다.
    ' - 발행(Issue API)를 호출하는 시점에서 포인트가 차감됩니다.
    ' - [발행완료] 세금계산서는 연동회원의 국세청 전송설정에 따라
    '   익일/즉시전송 처리됩니다. 기본설정(익일전송)
    ' - 국세청 전송설정은 "팝빌 로그인" > [전자세금계산서] > [환경설정] >
    '   [전자세금계산서 관리] > [국세청 전송 및 지연발행 설정] 탭에서
    '   확인할 수 있습니다.
    ' - 국세청 전송정책에 대한 사항은 "[전자세금계산서 API 연동매뉴얼] >
    '   1.4. 국세청 전송 정책" 을 참조하시기 바랍니다
    '=========================================================================
    Private Sub btnIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIssue.Click, btnIssue_Reverse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행시 메모", False, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    '[발행완료] 상태의 세금계산서를 [발행취소] 처리합니다.
    ' - [발행취소]는 국세청 전송전에만 가능합니다.
    ' - 발행취소된 세금계산서는 국세청에 전송되지 않습니다.
    ' - 발행취소 세금계산서에 기재된 문서관리번호를 재사용 하기 위해서는
    '   삭제(Delete API)를 호출하여 [삭제] 처리 하셔야 합니다.
    '=========================================================================
    Private Sub btnCancelIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssue.Click, btnCancelIssue_Reverse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행취소시 메모.", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 발행예정 세금계산서를 [승인]처리합니다.
    '=========================================================================
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Accept(txtCorpNum.Text, KeyType, txtMgtKey.Text, "승인시 메모.", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 발행예정 세금계산서를 [거부]처리 합니다.
    ' - [거부]처리된 세금계산서를 삭제(Delete API)하면 등록된 문서관리번호를
    '   재사용할 수 있습니다.
    '=========================================================================
    Private Sub btnDeny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeny.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Deny(txtCorpNum.Text, KeyType, txtMgtKey.Text, "거부시 메모.", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequest.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Request(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청시 메모", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnCancelRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelRequest.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청 취소시 메모", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnRefuse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefuse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청 거부시 메모", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20171120
        taxinvoice.writeDate = "20171120"

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
        taxinvoice.invoicerCorpNum = "1234567890"

        '[필수] 공급자 종사업자 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '[필수] 공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '[필수] 공급자 문서관리번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
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

        '[역발행시 필수] 공급받는자 문서관리번호(역발행시 필수)
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

        '기재 상 '권' 항목, 최대값 32767
        taxinvoice.kwon = 1

        '기재 상 '호' 항목, 최대값 32767
        taxinvoice.ho = 1

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
        ' - [참고] 수정세금계산서 작성방법 안내 - http://blog.linkhub.co.kr/650
        '========================================================================='

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 ItemKey, 문서확인 (GetInfo API)의 응답결과(ItemKey 항목) 확인
        taxinvoice.originalTaxinvoiceKey = ""


        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "201711121"                 '거래일자, yyyyMMdd
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnRegister_Reverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister_Reverse.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20171120
        taxinvoice.writeDate = "20171120"

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

        '[필수] 공급자 문서관리번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
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

        '[역발행시 필수] 공급받는자 문서관리번호(역발행시 필수)
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

        '기재 상 '권' 항목, 최대값 32767
        taxinvoice.kwon = 1

        '기재 상 '호' 항목, 최대값 32767
        taxinvoice.ho = 1

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
        ' - [참고] 수정세금계산서 작성방법 안내 - http://blog.linkhub.co.kr/650
        '========================================================================='

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 ItemKey, 문서확인 (GetInfo API)의 응답결과(ItemKey 항목) 확인
        taxinvoice.originalTaxinvoiceKey = ""


        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "201711121"                 '거래일자, yyyyMMdd
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
            Dim response As Response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnUpdate_Reverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate_Reverse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20171120
        taxinvoice.writeDate = "20171120"

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

        '[필수] 공급자 문서관리번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
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

        '[역발행시 필수] 공급받는자 문서관리번호(역발행시 필수)
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

        '기재 상 '권' 항목, 최대값 32767
        taxinvoice.kwon = 1

        '기재 상 '호' 항목, 최대값 32767
        taxinvoice.ho = 1

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
        ' - [참고] 수정세금계산서 작성방법 안내 - http://blog.linkhub.co.kr/650
        '========================================================================='

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 ItemKey, 문서확인 (GetInfo API)의 응답결과(ItemKey 항목) 확인
        taxinvoice.originalTaxinvoiceKey = ""


        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "201711121"                 '거래일자, yyyyMMdd
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnAttachFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachFile.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        If fileDialog.ShowDialog(Me) = DialogResult.OK Then
            Dim strFileName As String = fileDialog.FileName

            Try
                Dim response As Response = taxinvoiceService.AttachFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, strFileName, txtUserId.Text)

                MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

            End Try

        End If

    End Sub

    Private Sub gtnGetFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gtnGetFiles.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim fileList As List(Of AttachedFile) = taxinvoiceService.GetFiles(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            Dim tmp As String = "일련번호 | 표시명 | 파일아이디 | 등록일자" + vbCrLf

            For Each file As AttachedFile In fileList
                tmp += file.serialNum.ToString() + " | " + file.displayName + " | " + file.attachedFile + " | " + file.regDT + vbCrLf

            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFile.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.DeleteFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtFileID.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 전자세금계산서 API 서비스 과금정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click
        Try
            Dim ChargeInfo As ChargeInfo = taxinvoiceService.GetChargeInfo(txtCorpNum.Text)

            Dim tmp As String = "unitCost (발행단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전 URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_CHRG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_CHRG.Click
        Try
            Dim url As String = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전 팝업 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            Dim url As String = taxinvoiceService.GetPartnerURL(txtCorpNum.Text, "CHRG")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 공인인증서 등록 URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_CERT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_CERT.Click
        Try
            Dim url As String = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CERT")
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌(www.popbill.com)에 로그인된 팝빌 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_LOGIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_LOGIN.Click
        Try
            Dim url As String = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 인감 및 첨부문서 등록 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_SEAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_SEAL.Click
        Try
            Dim url As String = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "SEAL")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자를 신규로 등록합니다.
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디
        joinData.id = "testkorea1120"

        '비밀번호
        joinData.pwd = "password"

        '담당자명
        joinData.personName = "담당자명"

        '연락처
        joinData.tel = "070-1111-2222"

        '휴대폰번호
        joinData.hp = "010-1234-1234"

        '이메일
        joinData.email = "test@test.com"

        '회사조회 권한여부, True-회사조회, False-개인조회
        joinData.searchAllAllowYN = False

        Try
            Dim response As Response = taxinvoiceService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자 목록을 확인합니다.
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = taxinvoiceService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "아이디 | 담당자명 | 연락처 | 휴대폰번호 | 메일주소 | 회사조회 여부" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.tel + " | " + info.hp + " | " + info.email + " | " + info.searchAllAllowYN.ToString() + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 연동회원의 담당자 정보를 수정합니다.
    '=========================================================================
    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContact.Click
        '담당자 정보객체
        Dim joinData As New Contact


        '아이디
        joinData.id = "testkorea1120"

        '담당자명
        joinData.personName = "담당자명"

        '연락처
        joinData.tel = "070-1111-2222"

        '휴대폰번호
        joinData.hp = "010-1234-1234"

        '이메일
        joinData.email = "test@test.com"

        '회사조회 권한여부, True-회사조회, False-개인조회
        joinData.searchAllAllowYN = False

        Try
            Dim response As Response = taxinvoiceService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCorpInfo.Click
        Try
            Dim corpInfo As CorpInfo = taxinvoiceService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "ceoname(대표자성명) : " + corpInfo.ceoname + vbCrLf
            tmp += "corpName(상호) : " + corpInfo.corpName + vbCrLf
            tmp += "addr(주소) : " + corpInfo.addr + vbCrLf
            tmp += "bizType(업태) : " + corpInfo.bizType + vbCrLf
            tmp += "bizClass(종목) : " + corpInfo.bizClass + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다
    '=========================================================================
    Private Sub btnUpdateCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCorpInfo.Click
        Dim corpInfo As New CorpInfo

        '대표자명
        corpInfo.ceoname = "대표자명_수정"

        '상호
        corpInfo.corpName = "상호_수정"

        '주소
        corpInfo.addr = "주소_수정"

        '업태
        corpInfo.bizType = "업태_수정"

        '종목
        corpInfo.bizClass = "종목_수정"

        Try

            Dim response As Response = taxinvoiceService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnCancelIssue_Sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssue_Sub.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행취소시 메모.", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnDelete_Sub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_Sub.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        Try
            Dim response As Response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistIssue.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        '[필수] 작성일자, 표시형식 (yyyyMMdd) ex) 20171120
        taxinvoice.writeDate = "20171120"

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
        taxinvoice.invoicerCorpNum = "1234567890"

        '[필수] 공급자 종사업자 식별번호. 필요시 숫자 4자리 기재
        taxinvoice.invoicerTaxRegID = ""

        '[필수] 공급자 상호
        taxinvoice.invoicerCorpName = "공급자 상호"

        '[필수] 공급자 문서관리번호, 1~24자리 (숫자, 영문, '-', '_') 조합으로
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

        '[역발행시 필수] 공급받는자 문서관리번호(역발행시 필수)
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

        '기재 상 '권' 항목, 최대값 32767
        taxinvoice.kwon = 1

        '기재 상 '호' 항목, 최대값 32767
        taxinvoice.ho = 1

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
        ' - [참고] 수정세금계산서 작성방법 안내 - http://blog.linkhub.co.kr/650
        '========================================================================='

        ' 수정사유코드, 수정사유에 따라 1~6중 선택기재
        taxinvoice.modifyCode = Nothing

        ' 원본세금계산서의 ItemKey, 문서확인 (GetInfo API)의 응답결과(ItemKey 항목) 확인
        taxinvoice.originalTaxinvoiceKey = ""


        '=========================================================================
        '                            상세항목(품목) 정보
        '=========================================================================

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호, 1부터 순차기재
        detail.purchaseDT = "20171121"                 '거래일자, yyyyMMdd
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
        Dim forceIssue As Boolean = False

        '메모
        Dim memo As String = "메모"

        Try
            Dim response As Response = taxinvoiceService.RegistIssue(txtCorpNum.Text, taxinvoice, forceIssue, memo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim State(3) As String
        Dim TType(2) As String
        Dim taxType(3) As String
        Dim IssueType(3) As String

        '[필수] 일자유형, R-등록일시 W-작성일자 I-발행일시 중 택1
        Dim DType As String = "W"

        '[필수] 시작일자, yyyyMMdd
        Dim SDate As String = "20171101"

        '[필수] 종료일자, yyyyMMdd
        Dim EDate As String = "20171231"

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


        '지연발행 여부, False - 정상발행분만 조회 / True - 지연발행분만조회 / Nothing- 전체조회
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
                                taxType, IssueType, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, QString, Order, Page, PerPage, _
                                interOPYN, txtUserId.Text)


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
                tmp = tmp + "preIssueDT (발행예정일시) : " + tiInfo.preIssueDT + vbCrLf
                tmp = tmp + "openYN (개봉 여부) : " + tiInfo.openYN.ToString + vbCrLf
                tmp = tmp + "openDT (개봉 일시) : " + tiInfo.openDT + vbCrLf
                tmp = tmp + "stateMemo (상태메모) : " + tiInfo.stateMemo + vbCrLf
                tmp = tmp + "stateCode (상태코드) : " + tiInfo.stateCode.ToString + vbCrLf

                tmp = tmp + "modifyCode  (수정 사유코드) : " + tiInfo.modifyCode.ToString + vbCrLf

                tmp = tmp + "interOPYN (연동문서 여부) : " + tiInfo.interOPYN.ToString + vbCrLf

                tmp = tmp + "invoicerCorpName (공급자 상호) : " + tiInfo.invoicerCorpName + vbCrLf
                tmp = tmp + "invoicerCorpNum (공급자 사업자번호) : " + tiInfo.invoicerCorpNum + vbCrLf
                tmp = tmp + "invoicerMgtKey (공급자 문서관리번호) : " + tiInfo.invoicerMgtKey + vbCrLf
                tmp = tmp + "invoicerPrintYN (공급자 인쇄여부) : " + tiInfo.invoicerPrintYN.ToString + vbCrLf

                tmp = tmp + "invoiceeCorpName (공급받는자 상호) : " + tiInfo.invoiceeCorpName + vbCrLf
                tmp = tmp + "invoiceeCorpNum (공급받는자 사업자번호) : " + tiInfo.invoiceeCorpNum + vbCrLf
                tmp = tmp + "invoiceeMgtKey (공급받는자 문서관리번호) : " + tiInfo.invoiceeMgtKey + vbCrLf
                tmp = tmp + "invoiceePrintYN (공급받는지 인쇄여부) : " + tiInfo.invoiceePrintYN.ToString + vbCrLf
                tmp = tmp + "closeDownState (공급받는자 휴폐업상태) : " + tiInfo.closeDownState.ToString + vbCrLf
                tmp = tmp + "closeDownStateDate (공급받는자 휴폐업일자) : " + tiInfo.closeDownStateDate + vbCrLf

                tmp = tmp + "trusteeCorpName (수탁자 상호) : " + tiInfo.trusteeCorpName + vbCrLf
                tmp = tmp + "trusteeCorpNum (수탁자 사업자번호) : " + tiInfo.trusteeCorpNum + vbCrLf
                tmp = tmp + "trusteeMgtKey (수탁자 문서관리번호) : " + tiInfo.trusteeMgtKey + vbCrLf
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    '1건의 전자명세서를 세금계산서에 첨부합니다.
    '=========================================================================
    Private Sub btnAttachStatement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachStatement.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '첨부 대상 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim docItemCode As Integer = 121

        '첨부 대상 전자명세서 관리번호
        Dim docMgtKey As String = "20171117-02"

        Try
            Dim response As Response = taxinvoiceService.AttachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text, docItemCode, docMgtKey)
            MsgBox("응답코드(code) : " + Response.code.ToString() + vbCrLf + "응답메시지(message) : " + Response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '세금계산서에 첨부된 전자명세서 1건을 첨부해제합니다.
    '=========================================================================
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        '첨부해제 대상 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim docItemCode As Integer = 121

        '첨부해제 대상 전자명세서 관리번호
        Dim docMgtKey As String = "20171117-02"

        Try
            Dim response As Response = taxinvoiceService.DetachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text, docItemCode, docMgtKey)
            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub
End Class
