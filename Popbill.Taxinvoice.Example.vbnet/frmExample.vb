
'************************************************************************************
' 팝빌 전자세금계산서 API DotNet SDK Example
' 
' - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
' - 업데이트 일자 : 2017-11-20
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
' 
' <테스트 연동개발 준비사항>
' 1) 26, 29 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를 
'    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
' 3) 전자세금계산서 발행을 위해 공인인증서를 등록합니다. 두가지 방법 중 선택 
'    - 팝빌사이트 로그인 > [전자세금계산서] > [환경설정] > [공인인증서 관리]
'    - 공인인증서 등록 팝업 URL (GetPopbillURL API)을 이용하여 등록
'************************************************************************************

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

    Private Sub btnGetEmailPublicKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEmailPublicKey.Click

        Try
            Dim KeyList As List(Of EmailPublicKey) = taxinvoiceService.GetEmailPublicKeys(txtCorpNum.Text)

            MsgBox(KeyList.Count.ToString())

        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
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

        taxinvoice.invoiceeAddr = "공급받는자 주소"
        taxinvoice.invoiceeBizClass = "공급받는자 업종"
        taxinvoice.invoiceeBizType = "공급받는자 업태"
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        taxinvoice.supplyCostTotal = "100000"           '필수 공급가액 합계"
        taxinvoice.taxTotal = "10000"                   '필수 세액 합계
        taxinvoice.totalAmount = "110000"               '필수 합계금액.  공급가액 + 세액

        taxinvoice.modifyCode = Nothing                  '수정세금계산서 작성시 1~6까지 선택기재.
        taxinvoice.originalTaxinvoiceKey = ""           '수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
        taxinvoice.serialNum = "123"
        taxinvoice.cash = ""                            '현금
        taxinvoice.chkBill = ""                         '수표
        taxinvoice.note = ""                            '어음
        taxinvoice.credit = ""                          '외상미수금
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"
        taxinvoice.kwon = 1
        taxinvoice.ho = 1

        taxinvoice.businessLicenseYN = False            '사업자등록증 이미지 첨부시 설정.
        taxinvoice.bankBookYN = False                   '통장사본 이미지 첨부시 설정.
        taxinvoice.faxreceiveNum = ""                   '발행시 Fax발송기능 사용시 수신번호 기재.
        taxinvoice.faxsendYN = False                    '발행시 Fax발송시 설정.

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호
        detail.purchaseDT = "20140319"                  '거래일자
        detail.itemName = "품목명"
        detail.spec = "규격"
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        taxinvoice.addContactList = New List(Of TaxinvoiceAddContact)

        Dim addContact As TaxinvoiceAddContact = New TaxinvoiceAddContact

        addContact.email = "test2@invoicee.com"
        addContact.contactName = "추가담당자명"

        taxinvoice.addContactList.Add(addContact)


        Try
            Dim response As Response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text, False)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click, btnDelete_Reverse.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        Try
            Dim response As Response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim Memo As String = "발행예정 메모"

        '발행예정 메일제목, 공백으로 처리시 기본메일 제목으로 전송ㄴ
        Dim EmailSubject As String = "발행예정 메일제목 테스트 dotent 3.5"

        Try
            Dim response As Response = taxinvoiceService.Send(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, EmailSubject, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnCancelSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSend.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.CancelSend(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행예정 취소시 메모.", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetDetailInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDetailInfo.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim taxinvoice As Taxinvoice = taxinvoiceService.GetDetailInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            '자세한 문세정보는 작성시 항목을 참조하거나, 연동메뉴얼 참조.

            Dim tmp As String = ""

            tmp += "InvoicerCorpNum : " + taxinvoice.invoicerCorpNum + vbCrLf
            tmp += "InvoicerCorpName : " + taxinvoice.invoicerCorpName + vbCrLf
            tmp += "InvoiceeCorpNum : " + taxinvoice.invoiceeCorpNum + vbCrLf
            tmp += "InvoiceeCorpName : " + taxinvoice.invoiceeCorpName + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim taxinvoiceInfo As TaxinvoiceInfo = taxinvoiceService.GetInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp += "itemKey : " + taxinvoiceInfo.itemKey + vbCrLf
            tmp += "taxType : " + taxinvoiceInfo.taxType + vbCrLf
            tmp += "writeDate : " + taxinvoiceInfo.writeDate + vbCrLf
            tmp += "regDT : " + taxinvoiceInfo.regDT + vbCrLf

            tmp += "invoicerCorpName : " + taxinvoiceInfo.invoicerCorpName + vbCrLf
            tmp += "invoicerCorpNum : " + taxinvoiceInfo.invoicerCorpNum + vbCrLf
            tmp += "invoicerMgtKey : " + taxinvoiceInfo.invoicerMgtKey + vbCrLf
            tmp += "invoiceeCorpName : " + taxinvoiceInfo.invoiceeCorpName + vbCrLf
            tmp += "invoiceeCorpNum : " + taxinvoiceInfo.invoiceeCorpNum + vbCrLf
            tmp += "invoiceeMgtKey : " + taxinvoiceInfo.invoiceeMgtKey + vbCrLf
            tmp += "trusteeCorpName : " + taxinvoiceInfo.trusteeCorpName + vbCrLf
            tmp += "trusteeCorpNum : " + taxinvoiceInfo.trusteeCorpNum + vbCrLf
            tmp += "trusteeMgtKey : " + taxinvoiceInfo.trusteeMgtKey + vbCrLf

            tmp += "supplyCostTotal : " + taxinvoiceInfo.supplyCostTotal + vbCrLf
            tmp += "taxTotal : " + taxinvoiceInfo.taxTotal + vbCrLf
            tmp += "purposeType : " + taxinvoiceInfo.purposeType + vbCrLf
            tmp += "modifyCode : " + taxinvoiceInfo.modifyCode.ToString + vbCrLf
            tmp += "issueType : " + taxinvoiceInfo.issueType + vbCrLf

            tmp += "issueDT : " + taxinvoiceInfo.issueDT + vbCrLf
            tmp += "preIssueDT : " + taxinvoiceInfo.preIssueDT + vbCrLf

            tmp += "stateCode : " + taxinvoiceInfo.stateCode.ToString + vbCrLf
            tmp += "stateDT : " + taxinvoiceInfo.stateDT + vbCrLf

            tmp += "openYN : " + taxinvoiceInfo.openYN.ToString + vbCrLf
            tmp += "openDT : " + taxinvoiceInfo.openDT + vbCrLf
            tmp += "ntsresult : " + taxinvoiceInfo.ntsresult + vbCrLf
            tmp += "ntsconfirmNum : " + taxinvoiceInfo.ntsconfirmNum + vbCrLf
            tmp += "ntssendDT : " + taxinvoiceInfo.ntssendDT + vbCrLf
            tmp += "ntsresultDT : " + taxinvoiceInfo.ntsresultDT + vbCrLf
            tmp += "ntssendErrCode : " + taxinvoiceInfo.ntssendErrCode + vbCrLf
            tmp += "stateMemo : " + taxinvoiceInfo.stateMemo

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TBOX.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_SBOX.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "SBOX")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetURL_PBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_PBOX.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetURL_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_WRITE.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

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

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim MgtKeyList As List(Of String) = New List(Of String)

        ''최대 1000건.
        MgtKeyList.Add("1234")
        MgtKeyList.Add("12345")

        Try
            Dim taxinvoiceInfoList As List(Of TaxinvoiceInfo) = taxinvoiceService.GetInfos(txtCorpNum.Text, KeyType, MgtKeyList)

            ''TOGO Describe it.

            MsgBox(taxinvoiceInfoList.Count.ToString())

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try


    End Sub

    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        Try
            Dim response As Response = taxinvoiceService.SendEmail(txtCorpNum.Text, KeyType, txtMgtKey.Text, "test@test.com", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendSMS(txtCorpNum.Text, KeyType, txtMgtKey.Text, "1111-2222", "111-2222-4444", "발신문자 내용...", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFAX.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendFAX(txtCorpNum.Text, KeyType, txtMgtKey.Text, "1111-2222", "000-2222-4444", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPopUpURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEPrintURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetEmailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEmailURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMassPrintURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim MgtKeyList As List(Of String) = New List(Of String)

        ''최대 1000건.
        MgtKeyList.Add("1234")
        MgtKeyList.Add("12345")

        Try
            Dim url As String = taxinvoiceService.GetMassPrintURL(txtCorpNum.Text, KeyType, MgtKeyList, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnSendToNTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendToNTS.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendToNTS(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIssue.Click, btnIssue_Reverse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행시 메모", False, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnCancelIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssue.Click, btnCancelIssue_Reverse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행취소시 메모.", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Accept(txtCorpNum.Text, KeyType, txtMgtKey.Text, "승인시 메모.", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnDeny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeny.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Deny(txtCorpNum.Text, KeyType, txtMgtKey.Text, "거부시 메모.", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequest.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Request(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청시 메모", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnCancelRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelRequest.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청 취소시 메모", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnRefuse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefuse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청 거부시 메모", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        taxinvoice.writeDate = "20140923"               '필수, 기재상 작성일자
        taxinvoice.chargeDirection = "정과금"           '필수, {정과금, 역과금}
        taxinvoice.issueType = "정발행"                 '필수, {정발행, 역발행, 위수탁}
        taxinvoice.purposeType = "영수"                 '필수, {영수, 청구}
        taxinvoice.issueTiming = "직접발행"             '필수, {직접발행, 승인시자동발행}
        taxinvoice.taxType = "과세"                     '필수, {과세, 영세, 면세}


        taxinvoice.invoicerCorpNum = "1231212312"
        taxinvoice.invoicerTaxRegID = ""                '종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
        taxinvoice.invoicerCorpName = "공급자 상호 수정"
        taxinvoice.invoicerMgtKey = txtMgtKey.Text      '문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"
        taxinvoice.invoicerAddr = "공급자 주소"
        taxinvoice.invoicerBizClass = "공급자 업종"
        taxinvoice.invoicerBizType = "공급자 업태,업태2"
        taxinvoice.invoicerContactName = "공급자 담당자명"
        taxinvoice.invoicerEmail = "test@test.com"
        taxinvoice.invoicerTEL = "070-7070-0707"
        taxinvoice.invoicerHP = "010-000-2222"
        taxinvoice.invoicerSMSSendYN = True             '발행시 문자발송기능 사용시 활용

        taxinvoice.invoiceeType = "사업자"
        taxinvoice.invoiceeCorpNum = "8888888888"
        taxinvoice.invoiceeCorpName = "공급받는자 상호"
        taxinvoice.invoiceeMgtKey = ""
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"
        taxinvoice.invoiceeAddr = "공급받는자 주소"
        taxinvoice.invoiceeBizClass = "공급받는자 업종"
        taxinvoice.invoiceeBizType = "공급받는자 업태"
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        taxinvoice.supplyCostTotal = "100000"           '필수 공급가액 합계"
        taxinvoice.taxTotal = "10000"                   '필수 세액 합계
        taxinvoice.totalAmount = "110000"               '필수 합계금액.  공급가액 + 세액

        taxinvoice.modifyCode = Nothing                  '수정세금계산서 작성시 1~6까지 선택기재.
        taxinvoice.originalTaxinvoiceKey = ""           '수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
        taxinvoice.serialNum = "123"
        taxinvoice.cash = ""                            '현금
        taxinvoice.chkBill = ""                         '수표
        taxinvoice.note = ""                            '어음
        taxinvoice.credit = ""                          '외상미수금
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"
        taxinvoice.kwon = 1
        taxinvoice.ho = 1

        taxinvoice.businessLicenseYN = False            '사업자등록증 이미지 첨부시 설정.
        taxinvoice.bankBookYN = False                   '통장사본 이미지 첨부시 설정.
        taxinvoice.faxreceiveNum = ""                   '발행시 Fax발송기능 사용시 수신번호 기재.
        taxinvoice.faxsendYN = False                    '발행시 Fax발송시 설정.

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호
        detail.purchaseDT = "20140319"                  '거래일자
        detail.itemName = "품목명"
        detail.spec = "규격"
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        taxinvoice.addContactList = New List(Of TaxinvoiceAddContact)

        Dim addContact As TaxinvoiceAddContact = New TaxinvoiceAddContact

        addContact.email = "test2@invoicee.com"
        addContact.contactName = "추가담당자명"

        taxinvoice.addContactList.Add(addContact)


        Try
            Dim response As Response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnRegister_Reverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister_Reverse.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        taxinvoice.writeDate = "20140923"               '필수, 기재상 작성일자
        taxinvoice.chargeDirection = "정과금"           '필수, {정과금, 역과금}
        taxinvoice.issueType = "역발행"                 '필수, {정발행, 역발행, 위수탁}
        taxinvoice.purposeType = "영수"                 '필수, {영수, 청구}
        taxinvoice.issueTiming = "직접발행"             '필수, {직접발행, 승인시자동발행}
        taxinvoice.taxType = "과세"                     '필수, {과세, 영세, 면세}


        taxinvoice.invoicerCorpNum = "8888888888"
        taxinvoice.invoicerTaxRegID = ""                '종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
        taxinvoice.invoicerCorpName = "공급자 상호"
        taxinvoice.invoicerMgtKey = ""                  '공급자 발행까지 API로 발행하고자 할경우 정발행과 동일한 형태로 추가 기재.
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"
        taxinvoice.invoicerAddr = "공급자 주소"
        taxinvoice.invoicerBizClass = "공급자 업종"
        taxinvoice.invoicerBizType = "공급자 업태,업태2"
        taxinvoice.invoicerContactName = "공급자 담당자명"
        taxinvoice.invoicerEmail = "test@test.com"
        taxinvoice.invoicerTEL = "070-7070-0707"
        taxinvoice.invoicerHP = "010-000-2222"
        taxinvoice.invoicerSMSSendYN = True             '발행시 문자발송기능 사용시 활용

        taxinvoice.invoiceeType = "사업자"
        taxinvoice.invoiceeCorpNum = "1231212312"
        taxinvoice.invoiceeCorpName = "공급받는자 상호"
        taxinvoice.invoiceeMgtKey = txtMgtKey.Text      '문서관리번호 1~24자리까지 공급받는자 사업자번호별 중복없는 고유번호 할당
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"
        taxinvoice.invoiceeAddr = "공급받는자 주소"
        taxinvoice.invoiceeBizClass = "공급받는자 업종"
        taxinvoice.invoiceeBizType = "공급받는자 업태"
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        taxinvoice.supplyCostTotal = "100000"           '필수 공급가액 합계"
        taxinvoice.taxTotal = "10000"                   '필수 세액 합계
        taxinvoice.totalAmount = "110000"               '필수 합계금액.  공급가액 + 세액

        taxinvoice.modifyCode = Nothing                  '수정세금계산서 작성시 1~6까지 선택기재.
        taxinvoice.originalTaxinvoiceKey = ""           '수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
        taxinvoice.serialNum = "123"
        taxinvoice.cash = ""                            '현금
        taxinvoice.chkBill = ""                         '수표
        taxinvoice.note = ""                            '어음
        taxinvoice.credit = ""                          '외상미수금
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"
        taxinvoice.kwon = 1
        taxinvoice.ho = 1

        taxinvoice.businessLicenseYN = False            '사업자등록증 이미지 첨부시 설정.
        taxinvoice.bankBookYN = False                   '통장사본 이미지 첨부시 설정.
        taxinvoice.faxreceiveNum = ""                   '발행시 Fax발송기능 사용시 수신번호 기재.
        taxinvoice.faxsendYN = False                    '발행시 Fax발송시 설정.

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호
        detail.purchaseDT = "20140319"                  '거래일자
        detail.itemName = "품목명"
        detail.spec = "규격"
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        Try
            Dim response As Response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Reverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate_Reverse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        taxinvoice.writeDate = "20140923"               '필수, 기재상 작성일자
        taxinvoice.chargeDirection = "정과금"           '필수, {정과금, 역과금}
        taxinvoice.issueType = "역발행"                 '필수, {정발행, 역발행, 위수탁}
        taxinvoice.purposeType = "영수"                 '필수, {영수, 청구}
        taxinvoice.issueTiming = "직접발행"             '필수, {직접발행, 승인시자동발행}
        taxinvoice.taxType = "과세"                     '필수, {과세, 영세, 면세}


        taxinvoice.invoicerCorpNum = "8888888888"
        taxinvoice.invoicerTaxRegID = ""                '종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
        taxinvoice.invoicerCorpName = "공급자 상호 수정"
        taxinvoice.invoicerMgtKey = ""                  '공급자 발행까지 API로 발행하고자 할경우 정발행과 동일한 형태로 추가 기재.
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"
        taxinvoice.invoicerAddr = "공급자 주소"
        taxinvoice.invoicerBizClass = "공급자 업종"
        taxinvoice.invoicerBizType = "공급자 업태,업태2"
        taxinvoice.invoicerContactName = "공급자 담당자명"
        taxinvoice.invoicerEmail = "test@test.com"
        taxinvoice.invoicerTEL = "070-7070-0707"
        taxinvoice.invoicerHP = "010-000-2222"
        taxinvoice.invoicerSMSSendYN = True             '발행시 문자발송기능 사용시 활용

        taxinvoice.invoiceeType = "사업자"
        taxinvoice.invoiceeCorpNum = "1231212312"
        taxinvoice.invoiceeCorpName = "공급받는자 상호"
        taxinvoice.invoiceeMgtKey = txtMgtKey.Text      '문서관리번호 1~24자리까지 공급받는자 사업자번호별 중복없는 고유번호 할당
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"
        taxinvoice.invoiceeAddr = "공급받는자 주소"
        taxinvoice.invoiceeBizClass = "공급받는자 업종"
        taxinvoice.invoiceeBizType = "공급받는자 업태"
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        taxinvoice.supplyCostTotal = "100000"           '필수 공급가액 합계"
        taxinvoice.taxTotal = "10000"                   '필수 세액 합계
        taxinvoice.totalAmount = "110000"               '필수 합계금액.  공급가액 + 세액

        taxinvoice.modifyCode = Nothing                  '수정세금계산서 작성시 1~6까지 선택기재.
        taxinvoice.originalTaxinvoiceKey = ""           '수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
        taxinvoice.serialNum = "123"
        taxinvoice.cash = ""                            '현금
        taxinvoice.chkBill = ""                         '수표
        taxinvoice.note = ""                            '어음
        taxinvoice.credit = ""                          '외상미수금
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"
        taxinvoice.kwon = 1
        taxinvoice.ho = 1

        taxinvoice.businessLicenseYN = False            '사업자등록증 이미지 첨부시 설정.
        taxinvoice.bankBookYN = False                   '통장사본 이미지 첨부시 설정.
        taxinvoice.faxreceiveNum = ""                   '발행시 Fax발송기능 사용시 수신번호 기재.
        taxinvoice.faxsendYN = False                    '발행시 Fax발송시 설정.

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호
        detail.purchaseDT = "20140319"                  '거래일자
        detail.itemName = "품목명"
        detail.spec = "규격"
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        Try
            Dim response As Response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnAttachFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachFile.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        If fileDialog.ShowDialog(Me) = DialogResult.OK Then
            Dim strFileName As String = fileDialog.FileName


            Try
                Dim response As Response = taxinvoiceService.AttachFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, strFileName, txtUserId.Text)

                MsgBox(response.message)
            Catch ex As PopbillException
                MsgBox(ex.code.ToString() + " | " + ex.Message)
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

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFile.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.DeleteFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtFileID.Text, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
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
            Dim url As String = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG")

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
End Class
