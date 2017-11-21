'=========================================================================
'
' 팝빌 현금영수증 API VB.Net  SDK Example
'
' - VB.NET SDK 연동환경 설정방법 안내
' - 업데이트 일자 : 2017-11-21
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
'
' <테스트 연동개발 준비사항>
' 1) 27, 30번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
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

        '세금계산서 서비스 객체 초기화
        cashbillService = New CashbillService(LinkID, SecretKey)

        '연동환경 설정값 (True-개발용, False-상업용)
        cashbillService.IsTest = True

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim url As String = cashbillService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim remainPoint As Double = cashbillService.GetPartnerBalance(txtCorpNum.Text)


            MsgBox(remainPoint)


        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim unitCost As Single = cashbillService.GetUnitCost(txtCorpNum.Text)


            MsgBox(unitCost)


        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 관리번호 중복여부를 확인합니다.
    ' - 관리번호는 1~24자리로 숫자, 영문 '-', '_' 조합으로 구성할 수 있습니다.
    '=========================================================================
    Private Sub btnCheckMgtKeyInUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckMgtKeyInUse.Click

        Try
            Dim InUse As Boolean = cashbillService.CheckMgtKeyInUse(txtCorpNum.Text, txtMgtKey.Text)

            MsgBox(IIf(InUse, "사용중", "미사용중"))

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub



    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cashbill As Cashbill = New Cashbill

        cashbill.mgtKey = txtMgtKey.Text        '발행자별 고유번호 할당, 1~24자리 영문,숫자조합으로 중복없이 구성.
        cashbill.tradeType = "승인거래"         '승인거래 or 취소거래
        cashbill.franchiseCorpNum = txtCorpNum.Text
        cashbill.franchiseCorpName = "발행자 상호"
        cashbill.franchiseCEOName = "발행자 대표자"
        cashbill.franchiseAddr = "발행자 주소"
        cashbill.franchiseTEL = "070-1234-1234"
        cashbill.identityNum = "01041680206"
        cashbill.customerName = "고객명"
        cashbill.itemName = "상품명"
        cashbill.orderNumber = "주문번호"
        cashbill.email = "test@test.com"
        cashbill.hp = "111-1234-1234"
        cashbill.fax = "777-444-3333"
        cashbill.serviceFee = "0"
        cashbill.supplyCost = "10000"
        cashbill.tax = "1000"
        cashbill.totalAmount = "11000"
        cashbill.tradeUsage = "소득공제용"      '소득공제용 or 지출증빙용
        cashbill.taxationType = "과세"          '과세 or 비과세

        cashbill.smssendYN = False


        Try
            Dim response As Response = cashbillService.Register(txtCorpNum.Text, cashbill, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            Dim response As Response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 1건의 상세정보를 조회합니다.
    ' - 응답항목에 대한 자세한 사항은 "[현금영수증 API 연동매뉴얼] > 4.1.
    '   현금영수증 구성" 을 참조하시기 바랍니다.
    '=========================================================================

    Private Sub btnGetDetailInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDetailInfo.Click

        Try
            Dim cbDetailInfo As Cashbill = cashbillService.GetDetailInfo(txtCorpNum.Text, txtMgtKey.Text)

            '자세한 문세정보는 작성시 항목을 참조하거나, 연동메뉴얼 참조.

            Dim tmp As String = ""

            tmp += "mgtKey (관리번호) : " + cbDetailInfo.mgtKey + vbCrLf
            tmp += "confirmNum (국세청승인번호) : " + cbDetailInfo.confirmNum + vbCrLf
            tmp += "tradeDate (거래일자) : " + cbDetailInfo.tradeDate + vbCrLf
            tmp += "tradeUsage (거래유형) : " + cbDetailInfo.tradeUsage + vbCrLf
            tmp += "tradeType (현금영수증 형태) : " + cbDetailInfo.tradeType + vbCrLf
            tmp += "taxationType (과세형태) : " + cbDetailInfo.taxationType + vbCrLf
            tmp += "supplyCost (공급가액) : " + cbDetailInfo.supplyCost + vbCrLf
            tmp += "tax (세액) : " + cbDetailInfo.tax + vbCrLf
            tmp += "serviceFee (봉사료) : " + cbDetailInfo.serviceFee + vbCrLf
            tmp += "totalAmount (거래금액) : " + cbDetailInfo.totalAmount + vbCrLf

            tmp += "franchiseCorpNum (발행자 사업자번호) : " + cbDetailInfo.franchiseCorpNum + vbCrLf
            tmp += "franchiseCorpName (발행자 상호) : " + cbDetailInfo.franchiseCorpName + vbCrLf
            tmp += "franchiseCEOName (발행자 대표자명) : " + cbDetailInfo.franchiseCEOName + vbCrLf
            tmp += "franchiseAddr (발행자 주소) : " + cbDetailInfo.franchiseAddr + vbCrLf
            tmp += "franchiseTEL (발행자 연락처) : " + cbDetailInfo.franchiseTEL + vbCrLf

            tmp += "identityNum (거래처 식별번호) : " + cbDetailInfo.identityNum + vbCrLf
            tmp += "customerName (고객명) : " + cbDetailInfo.customerName + vbCrLf
            tmp += "itemName (상품명) : " + cbDetailInfo.itemName + vbCrLf
            tmp += "orderNumber (주문번호) : " + cbDetailInfo.orderNumber + vbCrLf
            tmp += "email (고객 이메일) : " + cbDetailInfo.email + vbCrLf
            tmp += "hp (고객 휴대폰번호) : " + cbDetailInfo.hp + vbCrLf
            tmp += "smssendYN (알림문자 전송여부) : " + cbDetailInfo.smssendYN.ToString + vbCrLf

            tmp += "orgConfirmNum (원본현금영수증 국세청승인번호) : " + cbDetailInfo.orgConfirmNum + vbCrLf
            tmp += "orgTradeDate (원본현금영수증 거래일자) : " + cbDetailInfo.orgTradeDate + vbCrLf
            tmp += "cancelType (취소사유) : " + cbDetailInfo.cancelType.ToString + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 현금영수증 상태/요약 정보를 확인합니다.
    ' - 응답항목에 대한 자세한 정보는 "[현금영수증 API 연동매뉴얼] > 4.2.
    '   현금영수증 상태정보 구성"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click

        Try
            Dim cbInfo As CashbillInfo = cashbillService.GetInfo(txtCorpNum.Text, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp += "itemKey (아이템키) : " + cbInfo.itemKey + vbCrLf
            tmp += "mgtKey (문서관리번호) : " + cbInfo.mgtKey + vbCrLf
            tmp += "tradeDate (거래일자) : " + cbInfo.tradeDate + vbCrLf
            tmp += "issueDT (발행일시) : " + cbInfo.issueDT + vbCrLf
            tmp += "regDT (등록일시) : " + cbInfo.regDT + vbCrLf
            tmp += "taxationType (과세형태) : " + cbInfo.taxationType + vbCrLf
            tmp += "totalAmount (거래금액) : " + cbInfo.totalAmount + vbCrLf
            tmp += "tradeUsage (거래용도) : " + cbInfo.tradeUsage + vbCrLf
            tmp += "tradeType (현금영수증 형태) : " + cbInfo.tradeType + vbCrLf
            tmp += "stateCode (상태코드) : " + cbInfo.stateCode.ToString + vbCrLf
            tmp += "stateDT (상태변경일시) : " + cbInfo.stateDT + vbCrLf

            tmp += "identityNum (거래처 식별번호) : " + cbInfo.identityNum + vbCrLf
            tmp += "itemName (상품명) : " + cbInfo.itemName + vbCrLf
            tmp += "customerName (고객명) : " + cbInfo.customerName + vbCrLf

            tmp += "confirmNum (국세청승인번호) : " + cbInfo.confirmNum + vbCrLf
            tmp += "ntssendDT (국세청 전송일시) : " + cbInfo.ntssendDT + vbCrLf
            tmp += "ntsresultDT (국세청 처리결과 수신일시) : " + cbInfo.ntsresultDT + vbCrLf
            tmp += "ntsresultCode (국세청 처리결과 상태코드) : " + cbInfo.ntsresultCode + vbCrLf
            tmp += "ntsresultMessage (국세청 처리결과 메시지) : " + cbInfo.ntsresultMessage + vbCrLf
            tmp += "orgConfirmNum (원본 현금영수증 국세청 승인번호) : " + cbInfo.orgConfirmNum + vbCrLf
            tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cbInfo.orgTradeDate + vbCrLf

            tmp += "printYN (인쇄여부) : " + cbInfo.printYN.ToString + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 > 현금영수증 > 임시(연동)문서함 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TBOX.Click
        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 팝빌 > 현금영수증 > 발행문서함 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_PBOX.Click
        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 > 현금영수증 > 현금영수증 작성 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_WRITE.Click
        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 상태 변경이력을 확인합니다.
    ' - 상태 변경이력 확인(GetLogs API) 응답항목에 대한 자세한 정보는
    '   "[현금영수증 API 연동매뉴얼] > 3.4.4 상태 변경이력 확인"
    '   을 참조하시기 바랍니다.
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

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 다수건의 현금영수증 상태/요약 정보를 확인합니다.
    ' - 응답항목에 대한 자세한 정보는 "[현금영수증 API 연동매뉴얼] > 4.2.
    '   현금영수증 상태정보 구성"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서관리번호 배열, 최대 1000건.
        MgtKeyList.Add("1234")
        MgtKeyList.Add("12345")

        Try
            Dim cashbillInfoList As List(Of CashbillInfo) = cashbillService.GetInfos(txtCorpNum.Text, MgtKeyList)

            Dim tmp As String = ""

            For Each cbInfo As CashbillInfo In cashbillInfoList

                tmp += "itemKey (아이템키) : " + cbInfo.itemKey + vbCrLf
                tmp += "mgtKey (문서관리번호) : " + cbInfo.mgtKey + vbCrLf
                tmp += "tradeDate (거래일자) : " + cbInfo.tradeDate + vbCrLf
                tmp += "issueDT (발행일시) : " + cbInfo.issueDT + vbCrLf
                tmp += "regDT (등록일시) : " + cbInfo.regDT + vbCrLf
                tmp += "taxationType (과세형태) : " + cbInfo.taxationType + vbCrLf
                tmp += "totalAmount (거래금액) : " + cbInfo.totalAmount + vbCrLf
                tmp += "tradeUsage (거래용도) : " + cbInfo.tradeUsage + vbCrLf
                tmp += "tradeType (현금영수증 형태) : " + cbInfo.tradeType + vbCrLf
                tmp += "stateCode (상태코드) : " + cbInfo.stateCode.ToString + vbCrLf
                tmp += "stateDT (상태변경일시) : " + cbInfo.stateDT + vbCrLf

                tmp += "identityNum (거래처 식별번호) : " + cbInfo.identityNum + vbCrLf
                tmp += "itemName (상품명) : " + cbInfo.itemName + vbCrLf
                tmp += "customerName (고객명) : " + cbInfo.customerName + vbCrLf

                tmp += "confirmNum (국세청승인번호) : " + cbInfo.confirmNum + vbCrLf
                tmp += "ntssendDT (국세청 전송일시) : " + cbInfo.ntssendDT + vbCrLf
                tmp += "ntsresultDT (국세청 처리결과 수신일시) : " + cbInfo.ntsresultDT + vbCrLf
                tmp += "ntsresultCode (국세청 처리결과 상태코드) : " + cbInfo.ntsresultCode + vbCrLf
                tmp += "ntsresultMessage (국세청 처리결과 메시지) : " + cbInfo.ntsresultMessage + vbCrLf
                tmp += "orgConfirmNum (원본 현금영수증 국세청 승인번호) : " + cbInfo.orgConfirmNum + vbCrLf
                tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cbInfo.orgTradeDate + vbCrLf
                tmp += "printYN (인쇄여부) : " + cbInfo.printYN.ToString + vbCrLf + vbCrLf

            Next

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try


    End Sub

    '=========================================================================
    ' 발행 안내메일을 재전송합니다.
    '=========================================================================
    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click

        '수신메일주소
        Dim receiverMail = "test@test.com"

        Try
            Dim response As Response = cashbillService.SendEmail(txtCorpNum.Text, txtMgtKey.Text, receiverMail, txtUserId.Text)

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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증을 팩스전송합니다.
    ' - 팩스 전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [팩스] > [전송내역]
    '   메뉴에서 전송결과를 확인할 수 있습니다.
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 현금영수증 보기 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        Try
            Dim url As String = cashbillService.GetPopUpURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 현금영수증 인쇄팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click
        Try
            Dim url As String = cashbillService.GetPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 현금영수증 인쇄(공급받는자) URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEPrintURL.Click

        Try
            Dim url As String = cashbillService.GetEPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

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
        Try
            Dim url As String = cashbillService.GetMailURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 다수건의 현금영수증 인쇄팝업 URL을 반환합니다.
    ' 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMassPrintURL.Click
        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서 관리번호 배열, 최대 100건.
        MgtKeyList.Add("1234")
        MgtKeyList.Add("12345")

        Try
            Dim url As String = cashbillService.GetMassPrintURL(txtCorpNum.Text, MgtKeyList, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try

    End Sub



    Private Sub btnIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            Dim response As Response = cashbillService.Issue(txtCorpNum.Text, txtMgtKey.Text, "발행시 메모", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [발행완료] 상태의 현금영수증을 [발행취소] 합니다.
    ' - 발행취소는 국세청 전송전에만 가능합니다.
    ' - 발행취소된 형금영수증은 국세청에 전송되지 않습니다.
    '=========================================================================
    Private Sub btnCancelIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim response As Response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, "발행취소시 메모.", txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim cashbill As Cashbill = New Cashbill

        cashbill.mgtKey = txtMgtKey.Text        '발행자별 고유번호 할당, 1~24자리 영문,숫자조합으로 중복없이 구성.
        cashbill.tradeType = "승인거래"         '승인거래 or 취소거래
        cashbill.franchiseCorpNum = txtCorpNum.Text
        cashbill.franchiseCorpName = "발행자 상호_수정"
        cashbill.franchiseCEOName = "발행자 대표자"
        cashbill.franchiseAddr = "발행자 주소"
        cashbill.franchiseTEL = "070-1234-1234"
        cashbill.identityNum = "01041680206"
        cashbill.customerName = "고객명"
        cashbill.itemName = "상품명"
        cashbill.orderNumber = "주문번호"
        cashbill.email = "test@test.com"
        cashbill.hp = "111-1234-1234"
        cashbill.fax = "777-444-3333"
        cashbill.serviceFee = "0"
        cashbill.supplyCost = "10000"
        cashbill.tax = "1000"
        cashbill.totalAmount = "11000"
        cashbill.tradeUsage = "소득공제용"     '소득공제용 or 지출증빙용
        cashbill.taxationType = "과세"          '과세 or 비과세

        cashbill.smssendYN = False


        Try
            Dim response As Response = cashbillService.Update(txtCorpNum.Text, txtMgtKey.Text, cashbill, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
    ' - LinkID는 인증정보로 설정되어 있는 링크아이디 값입니다.
    '=========================================================================
    Private Sub btnCheckIsMember_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = cashbillService.CheckIsMember(txtCorpNum.Text, LinkID)

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
            Dim response As Response = cashbillService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 연동회원으로 회원가입을 요청합니다.
    '=========================================================================
    Private Sub btnJoinMember_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJoinMember.Click
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
            Dim response As Response = cashbillService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 현금영수증 API 서비스 과금정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click
        Try
            Dim ChargeInfo As ChargeInfo = cashbillService.GetChargeInfo(txtCorpNum.Text)

            Dim tmp As String = "unitCost (발행단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 현금영수증 발행단가를 확인합니다.
    '=========================================================================
    Private Sub btnGetUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUnitCost.Click
        Try
            Dim unitCost As Single = cashbillService.GetUnitCost(txtCorpNum.Text)

            MsgBox("현금영수증 발행단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)
    '   를 통해 확인하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetBalance_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = cashbillService.GetBalance(txtCorpNum.Text)

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
    Private Sub btnGetPartnerPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerPoint.Click
        Try
            Dim remainPoint As Double = cashbillService.GetPartnerBalance(txtCorpNum.Text)


            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())


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
            Dim url As String = cashbillService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG")

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
            Dim url As String = cashbillService.GetPartnerURL(txtCorpNum.Text, "CHRG")

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
            Dim url As String = cashbillService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG")

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
        joinData.id = "testkorea"

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
            Dim response As Response = cashbillService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

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
            Dim contactList As List(Of Contact) = cashbillService.ListContact(txtCorpNum.Text, txtUserId.Text)

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
            Dim response As Response = cashbillService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

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
            Dim corpInfo As CorpInfo = cashbillService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

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

            Dim response As Response = cashbillService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 1건의 현금영수증을 즉시발행합니다.
    ' - 발행일 기준 오후 5시 이전에 발행된 현금영수증은 다음날 오후 2시에 국세청
    '   전송결과를 확인할 수 있습니다.
    ' - 현금영수증 국세청 전송 정책에 대한 정보는 "[현금영수증 API 연동매뉴얼]
    '   > 1.4. 국세청 전송정책"을 참조하시기 바랍니다.
    ' - 취소현금영수증 작성방법 안내 - http://blog.linkhub.co.kr/702
    '=========================================================================
    Private Sub btnRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistIssue.Click
        Dim cashbill As Cashbill = New Cashbill

        '메모
        Dim memo As String = "즉시발행 메모"

        '현금영수증 관리번호, 1~24자리 영문,숫자조합으로 사업자별로 중복되지 않도록 구성
        cashbill.mgtKey = txtMgtKey.Text

        '현금영수증 형태, [승인거래, 취소거래] 중 기재
        cashbill.tradeType = "승인거래"

        '[취소거래시 필수] 원본 국세청승인번호
        '문서정보(GetInfo API)의 응답항목중 국세청승인번호(confirmNum)를 확인하여 기재
        cashbill.orgConfirmNum = ""

        '[취소거래시 필수] 원본 현금영수증 거래일자
        '문서정보(GetInfo API)의 응답항목중 거래일자(tradeDate)를 확인하여 기재
        cashbill.orgTradeDate = ""

        '발행자 사업자번호, "-" 제외 10자리
        cashbill.franchiseCorpNum = txtCorpNum.Text

        '발행자 상호명
        cashbill.franchiseCorpName = "발행자 상호"

        '발행자 대표자 성명
        cashbill.franchiseCEOName = "발행자 대표자"

        '발행자 주소
        cashbill.franchiseAddr = "발행자 주소d"

        '발행자 연락처
        cashbill.franchiseTEL = "070-1234-1234"

        '거래유형, [소득공제용, 지출증빙용] 중 기재
        cashbill.tradeUsage = "소득공제용"

        '거래처 식별번호, 거래유형에 따라 작성
        '소득공제용 - 주민등록/휴대폰/카드번호 기재가능
        '지출증빙용 - 사업자번호/주민등록/휴대폰/카드번호 기재가능
        cashbill.identityNum = "0101112222"

        '과세형태, [과세, 비과세] 중 기재
        cashbill.taxationType = "과세"

        '공급가액
        cashbill.supplyCost = "10000"

        '봉사료
        cashbill.serviceFee = "0"

        '세액
        cashbill.tax = "1000"

        '합계금액, 공급가액 + 봉사료 + 세액
        cashbill.totalAmount = "11000"

        '주문고객명
        cashbill.customerName = "고객명"

        '상품명
        cashbill.itemName = "상품명"

        '주문번호
        cashbill.orderNumber = "주문번호"

        '고객이메일
        cashbill.email = "test@test.com"

        '고객휴대폰번호
        cashbill.hp = "010-111-222"

        '현금영수증 발행 알림문자 전송여부
        cashbill.smssendYN = False

        Try
            Dim response As Response = cashbillService.RegistIssue(txtCorpNum.Text, cashbill, memo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [발행완료] 상태의 현금영수증을 [발행취소] 합니다.
    ' - 발행취소는 국세청 전송전에만 가능합니다.
    ' - 발행취소된 형금영수증은 국세청에 전송되지 않습니다.
    '=========================================================================
    Private Sub btnCancelIssue02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssue02.Click
        '메모
        Dim memo As String = "발행취소 메모"

        Try
            Dim response As Response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [발행완료] 상태의 현금영수증을 [발행취소] 합니다.
    ' - 발행취소는 국세청 전송전에만 가능합니다.
    ' - 발행취소된 형금영수증은 국세청에 전송되지 않습니다.
    '=========================================================================
    Private Sub btnCancelIssueSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssueSub.Click
        '메모
        Dim memo As String = "발행취소 메모"

        Try
            Dim response As Response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 현금영수증을 삭제합니다.
    ' - 현금영수증을 삭제하면 사용된 문서관리번호(mgtKey)를 재사용할 수 있습니다.
    ' - 삭제가능한 문서 상태 : [임시저장], [발행취소]
    '=========================================================================
    Private Sub btnDeleteSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSub.Click
        

        Try
            Dim response As Response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 현금영수증을 삭제합니다.
    ' - 현금영수증을 삭제하면 사용된 문서관리번호(mgtKey)를 재사용할 수 있습니다.
    ' - 삭제가능한 문서 상태 : [임시저장], [발행취소]
    '=========================================================================
    Private Sub btnDelete02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete02.Click
        Try
            Dim response As Response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 취소현금영수증을 즉시발행합니다.
    ' - 발행일 기준 오후 5시 이전에 발행된 현금영수증은 다음날 오후 2시에 국세청
    '   전송결과를 확인할 수 있습니다.
    ' - 현금영수증 국세청 전송 정책에 대한 정보는 "[현금영수증 API 연동매뉴얼]
    '   > 1.4. 국세청 전송정책"을 참조하시기 바랍니다.
    ' - 취소현금영수증 작성방법 안내 - http://blog.linkhub.co.kr/702
    '=========================================================================
    Private Sub btnRevokRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevokRegistIssue.Click

        '원본현금영수증 국세청 승인번호
        Dim orgConfirmNum As String = "820116333"

        '원본현금영수증 거래일자
        Dim orgTradeDate As String = "20170711"

        '발행 안내문자 전송여부
        Dim smssendYN As Boolean = False

        '메모
        Dim memo As String = "취소현금영수증 발행 메모"

        Try
            Dim response As Response = cashbillService.RevokeRegistIssue(txtCorpNum.Text, txtMgtKey.Text, orgConfirmNum, orgTradeDate, smssendYN, memo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 (부분) 취소현금영수증을 즉시발행합니다.
    ' - 발행일 기준 오후 5시 이전에 발행된 현금영수증은 다음날 오후 2시에 국세청
    '   전송결과를 확인할 수 있습니다.
    ' - 현금영수증 국세청 전송 정책에 대한 정보는 "[현금영수증 API 연동매뉴얼]
    '   > 1.4. 국세청 전송정책"을 참조하시기 바랍니다.
    ' - 취소현금영수증 작성방법 안내 - http://blog.linkhub.co.kr/702
    '=========================================================================
    Private Sub btnRevokeRegistIssue_part_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevokeRegistIssue_part.Click

        '원본현금영수증 국세청 승인번호
        Dim orgConfirmNum As String = "820116333"

        '원본현금영수증 거래일자
        Dim orgTradeDate As String = "20170711"

        '발행안내문자 전송여부
        Dim smssendYN As Boolean = False

        '메모
        Dim memo As String = "부분취소 즉시발행 메모"


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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim State(3) As String
        Dim tradeType(2) As String
        Dim tradeUsage(2) As String
        Dim taxationType(2) As String

        '[필수] 일자유형, R-등록일자, T-거래일자 I-발행일자
        Dim DType As String = "T"

        '[필수] 시작일자, 형식(yyyyMMdd)
        Dim SDate As String = "20171101"

        '[필수] 종료일자, 형식(yyyyMMdd)
        Dim EDate As String = "20171231"

        '전송상태코드 배열, 미기재시 전체조회, 2,3번째 자리 와일드카드(*) 가능
        '[참조] 현금영수증 API 연동매뉴열 "5.1. 현금영수증 상태코드"
        State(0) = "2**"
        State(1) = "3**"
        State(2) = "4**"

        '현금영수증 형태 배열, N-일반 현금영수증, C-취소 현금영수증
        tradeType(0) = "N"
        tradeType(1) = "C"

        '거래유형 배열, P-소득공제, C-제출증빙
        tradeUsage(0) = "P"
        tradeUsage(1) = "C"

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
                                                                tradeType, tradeUsage, taxationType, QString, Order, Page, PerPage)

            Dim tmp As String

            tmp = "code (응답코드) : " + CStr(cbSearchList.code) + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + CStr(cbSearchList.total) + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + CStr(cbSearchList.perPage) + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + CStr(cbSearchList.pageNum) + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + CStr(cbSearchList.pageCount) + vbCrLf
            tmp = tmp + "message (응답메시지) : " + cbSearchList.message + vbCrLf + vbCrLf


            For Each cbInfo As CashbillInfo In cbSearchList.list
                tmp += "itemKey (아이템키) : " + cbInfo.itemKey + vbCrLf
                tmp += "mgtKey (문서관리번호) : " + cbInfo.mgtKey + vbCrLf
                tmp += "tradeDate (거래일자) : " + cbInfo.tradeDate + vbCrLf
                tmp += "issueDT (발행일시) : " + cbInfo.issueDT + vbCrLf
                tmp += "regDT (등록일시) : " + cbInfo.regDT + vbCrLf
                tmp += "taxationType (과세형태) : " + cbInfo.taxationType + vbCrLf
                tmp += "totalAmount (거래금액) : " + cbInfo.totalAmount + vbCrLf
                tmp += "tradeUsage (거래용도) : " + cbInfo.tradeUsage + vbCrLf
                tmp += "tradeType (현금영수증 형태) : " + cbInfo.tradeType + vbCrLf
                tmp += "stateCode (상태코드) : " + cbInfo.stateCode.ToString + vbCrLf
                tmp += "stateDT (상태변경일시) : " + cbInfo.stateDT + vbCrLf

                tmp += "identityNum (거래처 식별번호) : " + cbInfo.identityNum + vbCrLf
                tmp += "itemName (상품명) : " + cbInfo.itemName + vbCrLf
                tmp += "customerName (고객명) : " + cbInfo.customerName + vbCrLf

                tmp += "confirmNum (국세청승인번호) : " + cbInfo.confirmNum + vbCrLf
                tmp += "ntssendDT (국세청 전송일시) : " + cbInfo.ntssendDT + vbCrLf
                tmp += "ntsresultDT (국세청 처리결과 수신일시) : " + cbInfo.ntsresultDT + vbCrLf
                tmp += "ntsresultCode (국세청 처리결과 상태코드) : " + cbInfo.ntsresultCode + vbCrLf
                tmp += "ntsresultMessage (국세청 처리결과 메시지) : " + cbInfo.ntsresultMessage + vbCrLf
                tmp += "orgConfirmNum (원본 현금영수증 국세청 승인번호) : " + cbInfo.orgConfirmNum + vbCrLf
                tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cbInfo.orgTradeDate + vbCrLf
                tmp += "printYN (인쇄여부) : " + cbInfo.printYN.ToString + vbCrLf + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + " 응답메시지(message) : " + ex.Message)
        End Try
    End Sub
End Class
