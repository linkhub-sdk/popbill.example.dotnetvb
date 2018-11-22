'=========================================================================
'
' 팝빌 전자명세서 API VB.NET SDK Example
'
' - VB.NET SDK 연동환경 설정방법 안내 : http://blog.linkhub.co.kr/4453/
' - 업데이트 일자 : 2018-11-22
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
'
' <테스트 연동개발 준비사항>
' 1) 23, 26번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
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
    ' 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
    ' - LinkID는 인증정보로 설정되어 있는 링크아이디 값입니다.
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
    ' 팝빌 회원아이디 중복여부를 확인합니다.
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
            Dim response As Response = statementService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 전자명세서 API 서비스 과금정보를 확인합니다.
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
    ' 전자명세서 발행단가를 확인합니다.
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
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)
    '   를 통해 확인하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = statementService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전 URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click

        Try
            Dim url As String = statementService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를
    '   이용하시기 바랍니다.
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
    ' 파트너 포인트 충전 팝업 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            Dim url As String = statementService.GetPartnerURL(txtCorpNum.Text, "CHRG")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌(www.popbill.com)에 로그인된 팝빌 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = statementService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
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
            Dim url As String = statementService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "SEAL")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
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
            Dim response As Response = statementService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자 목록을 확인합니다.
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = statementService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "아이디 | 담당자명 | 메일주소 | 휴대폰번호 | 팩스 | 연락처 | 등록일시 | 회사조회 여부 | 관리자 여부 | 상태" + vbCrLf

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
            Dim response As Response = statementService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
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

            Dim response As Response = statementService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 관리번호 중복여부를 확인합니다.
    ' - 관리번호는 1~24자리로 숫자, 영문 '-', '_' 조합으로 구성할 수 있습니다.
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
    ' 1건의 전자명세서를 즉시발행 처리합니다.
    '=========================================================================
    Private Sub btnRegistIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistIssue.Click
        Dim statement As New Statement

        '[필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20171122"

        '[필수] {영수, 청구} 중 기재
        statement.purposeType = "영수"

        '[필수] 과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text

        '[필수] 전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '[필수] 문서관리번호, 숫자, 영문, '-', '_' 조합 (최대24자리)으로 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text


        '=========================================================================
        '                               공급자 정보
        '=========================================================================

        '공급자 사업자번호, '-' 제외 10자리
        statement.senderCorpNum = txtCorpNum.Text

        '공급자 종사업장 식별번호, 필요시 기재, 형식은 숫자 4자리
        statement.senderTaxRegID = ""

        '공급자 상호
        statement.senderCorpName = "공급자 상호"

        '공급자 대표자 성명
        statement.senderCEOName = "공급자 대표자 성명"

        '공급자 주소
        statement.senderAddr = "공급자 주소"

        '공급자 종목
        statement.senderBizClass = "공급자 종목"

        '공급자 업태
        statement.senderBizType = "공급자 업태,업태2"

        '공급자 담당자성명
        statement.senderContactName = "공급자 담당자명"

        '공급자 이메일
        statement.senderEmail = "test@test.com"

        '공급자 연락처
        statement.senderTEL = "070-7070-0707"

        '공급자 휴대전화 번호
        statement.senderHP = "010-000-2222"


        '=========================================================================
        '                        공급받는자 정보
        '=========================================================================

        '공급받는자 사업자번호, '-' 제외 10자리
        statement.receiverCorpNum = "8888888888"

        '공급받는자 상호
        statement.receiverCorpName = "공급받는자 상호"

        '공급받는자 대표자 성명
        statement.receiverCEOName = "공급받는자 대표자 성명"

        '공급받는자 주소
        statement.receiverAddr = "공급받는자 주소"

        '공급받는자 종목
        statement.receiverBizClass = "공급받는자 종목 "

        '공급받는자 업태
        statement.receiverBizType = "공급받는자 업태"

        '공급받는자 담당자명
        statement.receiverContactName = "공급받는자 담당자명"

        '공급받는자 담당자 휴대폰번호
        statement.receiverHP = "010-1111-2222"

        '공급받는자 담당자 연락처
        statement.receiverTEL = "070-1234-1234"

        '공급받는자 메일주소
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
        newDetail.purchaseDT = "20171122"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"
        newDetail.spec = "규격"
        newDetail.unit = "단위"
        newDetail.qty = "1" '수량           ' 소숫점 2자리까지 문자열로 기재가능
        newDetail.unitCost = "100000"       ' 소숫점 2자리까지 문자열로 기재가능
        newDetail.supplyCost = "100000"
        newDetail.tax = "10000"
        newDetail.remark = "비고"
        newDetail.spare1 = "spare1"
        newDetail.spare2 = "spare2"
        newDetail.spare3 = "spare3"
        newDetail.spare4 = "spare4"
        newDetail.spare5 = "spare5"

        statement.detailList.Add(newDetail)

        newDetail = New StatementDetail

        newDetail.serialNum = 2             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20171122"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"
        newDetail.spec = "규격"


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

        Try
            Dim response As Response = statementService.RegistIssue(txtCorpNum.Text, statement, memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try


    End Sub

    '=========================================================================
    ' 1건의 전자명세서를 [발행취소] 처리합니다.
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
    ' 1건의 전자명세서를 삭제합니다.
    ' - 전자명세서를 삭제하면 사용된 문서관리번호(mgtKey)를 재사용할 수 있습니다.
    ' - 삭제가능한 문서 상태 : [임시저장], [발행취소]
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
    ' 1건의 전자명세서를 임시저장 합니다.
    ' - 세금계산서 임시저장(Register API) 호출후에는 발행(Issue API)을 호출해야만
    '   공급받는자에게 메일로 전송됩니다.
    ' - 임시저장과 발행을 한번의 호출로 처리하는 즉시발행(RegistIssue API) 프로세스
    '   연동을 권장합니다.
    '=========================================================================
    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim statement As New Statement

        '[필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20171122"

        '[필수] {영수, 청구} 중 기재
        statement.purposeType = "영수"

        '[필수] 과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text

        '[필수] 전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '[필수] 문서관리번호, 숫자, 영문, '-', '_' 조합 (최대24자리)으로 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text


        '=========================================================================
        '                               공급자 정보
        '=========================================================================

        '공급자 사업자번호, '-' 제외 10자리
        statement.senderCorpNum = txtCorpNum.Text

        '공급자 종사업장 식별번호, 필요시 기재, 형식은 숫자 4자리
        statement.senderTaxRegID = ""

        '공급자 상호
        statement.senderCorpName = "공급자 상호"

        '공급자 대표자 성명
        statement.senderCEOName = "공급자 대표자 성명"

        '공급자 주소
        statement.senderAddr = "공급자 주소"

        '공급자 종목
        statement.senderBizClass = "공급자 종목"

        '공급자 업태
        statement.senderBizType = "공급자 업태,업태2"

        '공급자 담당자성명
        statement.senderContactName = "공급자 담당자명"

        '공급자 이메일
        statement.senderEmail = "test@test.com"

        '공급자 연락처
        statement.senderTEL = "070-7070-0707"

        '공급자 휴대전화 번호
        statement.senderHP = "010-000-2222"


        '=========================================================================
        '                        공급받는자 정보
        '=========================================================================

        '공급받는자 사업자번호, '-' 제외 10자리
        statement.receiverCorpNum = "8888888888"

        '공급받는자 상호
        statement.receiverCorpName = "공급받는자 상호"

        '공급받는자 대표자 성명
        statement.receiverCEOName = "공급받는자 대표자 성명"

        '공급받는자 주소
        statement.receiverAddr = "공급받는자 주소"

        '공급받는자 종목
        statement.receiverBizClass = "공급받는자 종목 "

        '공급받는자 업태
        statement.receiverBizType = "공급받는자 업태"

        '공급받는자 담당자명
        statement.receiverContactName = "공급받는자 담당자명"

        '공급받는자 담당자 휴대폰번호
        statement.receiverHP = "010-1111-2222"

        '공급받는자 담당자 연락처
        statement.receiverTEL = "070-1234-1234"

        '공급받는자 메일주소
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
        newDetail.purchaseDT = "20171122"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"
        newDetail.spec = "규격"
        newDetail.unit = "단위"
        newDetail.qty = "1" '수량           ' 소숫점 2자리까지 문자열로 기재가능
        newDetail.unitCost = "100000"       ' 소숫점 2자리까지 문자열로 기재가능
        newDetail.supplyCost = "100000"
        newDetail.tax = "10000"
        newDetail.remark = "비고"
        newDetail.spare1 = "spare1"
        newDetail.spare2 = "spare2"
        newDetail.spare3 = "spare3"
        newDetail.spare4 = "spare4"
        newDetail.spare5 = "spare5"

        statement.detailList.Add(newDetail)

        newDetail = New StatementDetail

        newDetail.serialNum = 2             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20171122"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"
        newDetail.spec = "규격"


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
    ' 1건의 [임시저장] 상태의 전자명세서를 발행처리합니다.
    ' - 발행시 포인트가 차감됩니다.
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
    ' 1건의 전자명세서를 [발행취소] 처리합니다.
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
    ' 1건의 전자명세서를 삭제합니다.
    ' - 전자명세서를 삭제하면 사용된 문서관리번호(mgtKey)를 재사용할 수 있습니다.
    ' - 삭제가능한 문서 상태 : [임시저장], [발행취소]
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
    ' 전자명세서에 첨부파일을 등록합니다.
    ' - 첨부파일 등록은 전자명세서가 [임시저장] 상태인 경우에만 가능합니다.
    ' - 첨부파일은 최대 5개까지 등록할 수 있습니다.
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
    ' 전자명세서에 첨부된 파일의 목록을 확인합니다.
    ' - 응답항목 중 파일아이디(AttachedFile) 항목은 파일삭제(DeleteFile API)
    '   호출시 이용할 수 있습니다.
    '=========================================================================
    Private Sub btnGetFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFiles.Click

        Try
            Dim fileList As List(Of AttachedFile) = statementService.GetFiles(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            Dim tmp As String = "일련번호 | 표시명 | 파일아이디 | 등록일자" + vbCrLf

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
    ' 전자명세서에 첨부된 파일의 목록을 확인합니다.
    ' - 응답항목 중 파일아이디(AttachedFile) 항목은 파일삭제(DeleteFile API)
    '   호출시 이용할 수 있습니다.
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
    ' 1건의 전자명세서 상태/요약 정보를 확인합니다.
    ' - 응답항목에 대한 자세한 정보는 "[전자명세서 API 연동매뉴얼] > 3.3.1.
    '   GetInfo (상태 확인)"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click

        Try
            Dim docInfo As StatementInfo = statementService.GetInfo(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp = tmp + "itemKey (팝빌관리번호) : " + docInfo.itemKey + vbCrLf
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
            tmp = tmp + "regDT (임시저장일시) : " + docInfo.regDT + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 다수건의 전자명세서 상태/요약 정보를 확인합니다.
    ' - 응답항목에 대한 자세한 정보는 "[전자명세서 API 연동매뉴얼] > 3.3.2.
    '   GetInfos (상태 대량 확인)"을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click


        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서관리번호 배열, 최대 1000건
        MgtKeyList.Add("20171122-01")
        MgtKeyList.Add("20171122-02")

        Try
            Dim statementInfoList As List(Of StatementInfo) = statementService.GetInfos(txtCorpNum.Text, selectedItemCode, MgtKeyList)

            Dim tmp As String = ""

            For Each docInfo As StatementInfo In statementInfoList
                tmp = tmp + "itemKey (팝빌관리번호) : " + docInfo.itemKey + vbCrLf
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
    ' 전자명세서 상태 변경이력을 확인합니다.
    ' - 상태 변경이력 확인(GetLogs API) 응답항목에 대한 자세한 정보는
    '   "[전자명세서 API 연동매뉴얼] > 3.3.4 GetLogs (상태 변경이력 확인)"
    '   을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetLogs.Click

        Dim tmp As String = ""

        Try
            Dim logList As List(Of StatementLog) = statementService.GetLogs(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            For Each log As StatementLog In logList
                tmp += log.docLogType.ToString + " | " + log.log + " | " + log.procType + " | " + log.procCorpName + " | " + _
                    log.procContactName + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 1건의 상세정보를 조회합니다.
    ' - 응답항목에 대한 자세한 사항은 "[전자명세서 API 연동매뉴얼] > 4.1.
    '   전자명세서 구성" 을 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetDetailInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDetailInfo.Click

        Dim tmp As String = ""

        Try

            Dim docDetailInfo As Statement = statementService.GetDetailInfo(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text)

            tmp = tmp + "writeDate : " + docDetailInfo.writeDate + vbCrLf
            tmp = tmp + "taxType : " + docDetailInfo.taxType + vbCrLf
            tmp = tmp + "senderCorpNum : " + docDetailInfo.senderCorpNum + vbCrLf
            tmp = tmp + "senderTaxRegID : " + docDetailInfo.senderTaxRegID + vbCrLf
            tmp = tmp + "senderCorpName : " + docDetailInfo.senderCorpName + vbCrLf
            tmp = tmp + "senderCEOName : " + docDetailInfo.senderCEOName + vbCrLf
            tmp = tmp + "senderAddr : " + docDetailInfo.senderAddr + vbCrLf
            tmp = tmp + "senderBizClass : " + docDetailInfo.senderBizClass + vbCrLf
            tmp = tmp + "senderBizType : " + docDetailInfo.senderBizType + vbCrLf
            tmp = tmp + "senderContactName : " + docDetailInfo.senderContactName + vbCrLf
            tmp = tmp + "senderDeptName : " + docDetailInfo.senderDeptName + vbCrLf
            tmp = tmp + "senderTEL : " + docDetailInfo.senderTEL + vbCrLf
            tmp = tmp + "senderHP : " + docDetailInfo.senderHP + vbCrLf
            tmp = tmp + "senderEmail : " + docDetailInfo.senderEmail + vbCrLf
            tmp = tmp + "receiverCorpNum : " + docDetailInfo.receiverCorpNum + vbCrLf
            tmp = tmp + "receiverTaxRegID : " + docDetailInfo.receiverTaxRegID + vbCrLf
            tmp = tmp + "receiverCorpName : " + docDetailInfo.receiverCorpName + vbCrLf
            tmp = tmp + "receiverCEOName : " + docDetailInfo.receiverCEOName + vbCrLf
            tmp = tmp + "receiverAddr : " + docDetailInfo.receiverAddr + vbCrLf
            tmp = tmp + "receiverBizClass : " + docDetailInfo.receiverBizClass + vbCrLf
            tmp = tmp + "receiverBizType : " + docDetailInfo.receiverBizType + vbCrLf
            tmp = tmp + "receiverContactName : " + docDetailInfo.receiverContactName + vbCrLf
            tmp = tmp + "receiverDeptName : " + docDetailInfo.receiverDeptName + vbCrLf
            tmp = tmp + "receiverTEL : " + docDetailInfo.receiverTEL + vbCrLf
            tmp = tmp + "receiverHP : " + docDetailInfo.receiverHP + vbCrLf
            tmp = tmp + "receiverEmail : " + docDetailInfo.receiverEmail + vbCrLf

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건을 사용하여 전자명세서 목록을 조회합니다.
    ' - 응답항목에 대한 자세한 사항은 "[전자명세서 API 연동매뉴얼] >
    '   3.3.3. Search (목록 조회)" 를 참조하시기 바랍니다.
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(3) As String
        Dim ItemCode(6) As Integer

        '[필수] 일자유형, R-등록일시 W-작성일자 I-발행일시 중 택1
        Dim DType As String = "W"

        '[필수] 시작일자, yyyyMMdd
        Dim SDate As String = "20170701"

        '[필수] 종료일자, yyyyMMdd
        Dim EDate As String = "20171231"

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
            Dim stmtSearchList As DocSearchResult = statementService.Search(txtCorpNum.Text, DType, SDate, EDate, State, _
                                                                             ItemCode, QString, Order, Page, PerPage)

            Dim tmp As String

            tmp = "code (응답코드) : " + stmtSearchList.code.ToString + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + stmtSearchList.total.ToString + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + stmtSearchList.perPage.ToString + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + stmtSearchList.pageNum.ToString + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + stmtSearchList.pageCount.ToString + vbCrLf
            tmp = tmp + "message (응답메시지) : " + stmtSearchList.message + vbCrLf + vbCrLf



            Dim docInfo As StatementInfo

            For Each docInfo In stmtSearchList.list
                tmp = tmp + "itemKey (팝빌관리번호) : " + docInfo.itemKey + vbCrLf
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
    ' 발행 안내메일을 재전송합니다.
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
    ' 알림문자를 전송합니다. (단문/SMS- 한글 최대 45자)
    ' - 알림문자 전송시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [전송내역] 탭에서
    '   전송결과를 확인할 수 있습니다.
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
    ' 전자세금계산서를 팩스전송합니다.
    ' - 팩스 전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [팩스] > [전송내역]
    '   메뉴에서 전송결과를 확인할 수 있습니다.
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
    ' 전자명세서를 팝빌에 등록하지 않고 팩스로 전송합니다.
    ' - 팩스 전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
    ' - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [팩스] > [전송내역]
    '   메뉴에서 전송결과를 확인할 수 있습니다.
    '=========================================================================
    Private Sub btnFAXSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFAXSend.Click

        '팩스 발신번호
        Dim sendNum As String = "070111222"

        '팩스 수신번호
        Dim receiveNum As String = "070111222"


        Dim statement As New Statement

        '[필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
        statement.writeDate = "20171122"

        '[필수] {영수, 청구} 중 기재
        statement.purposeType = "영수"

        '[필수] 과세형태, {과세, 영세, 면세} 중 기재
        statement.taxType = "과세"

        '맞춤양식코드, 공백처리시 기본양식으로 작성
        statement.formCode = txtFormCode.Text

        '[필수] 전자명세서 종류코드
        statement.itemCode = selectedItemCode()

        '[필수] 문서관리번호, 숫자, 영문, '-', '_' 조합 (최대24자리)으로 사업자별로 중복되지 않도록 구성
        statement.mgtKey = txtMgtKey.Text


        '=========================================================================
        '                               공급자 정보
        '=========================================================================

        '공급자 사업자번호, '-' 제외 10자리
        statement.senderCorpNum = txtCorpNum.Text

        '공급자 종사업장 식별번호, 필요시 기재, 형식은 숫자 4자리
        statement.senderTaxRegID = ""

        '공급자 상호
        statement.senderCorpName = "공급자 상호"

        '공급자 대표자 성명
        statement.senderCEOName = "공급자 대표자 성명"

        '공급자 주소
        statement.senderAddr = "공급자 주소"

        '공급자 종목
        statement.senderBizClass = "공급자 종목"

        '공급자 업태
        statement.senderBizType = "공급자 업태,업태2"

        '공급자 담당자성명
        statement.senderContactName = "공급자 담당자명"

        '공급자 이메일
        statement.senderEmail = "test@test.com"

        '공급자 연락처
        statement.senderTEL = "070-7070-0707"

        '공급자 휴대전화 번호
        statement.senderHP = "010-000-2222"


        '=========================================================================
        '                        공급받는자 정보
        '=========================================================================

        '공급받는자 사업자번호, '-' 제외 10자리
        statement.receiverCorpNum = "8888888888"

        '공급받는자 상호
        statement.receiverCorpName = "공급받는자 상호"

        '공급받는자 대표자 성명
        statement.receiverCEOName = "공급받는자 대표자 성명"

        '공급받는자 주소
        statement.receiverAddr = "공급받는자 주소"

        '공급받는자 종목
        statement.receiverBizClass = "공급받는자 종목 "

        '공급받는자 업태
        statement.receiverBizType = "공급받는자 업태"

        '공급받는자 담당자명
        statement.receiverContactName = "공급받는자 담당자명"

        '공급받는자 담당자 휴대폰번호
        statement.receiverHP = "010-1111-2222"

        '공급받는자 담당자 연락처
        statement.receiverTEL = "070-1234-1234"

        '공급받는자 메일주소
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
        newDetail.purchaseDT = "20171122"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"
        newDetail.spec = "규격"
        newDetail.unit = "단위"
        newDetail.qty = "1" '수량           ' 소숫점 2자리까지 문자열로 기재가능
        newDetail.unitCost = "100000"       ' 소숫점 2자리까지 문자열로 기재가능
        newDetail.supplyCost = "100000"
        newDetail.tax = "10000"
        newDetail.remark = "비고"
        newDetail.spare1 = "spare1"
        newDetail.spare2 = "spare2"
        newDetail.spare3 = "spare3"
        newDetail.spare4 = "spare4"
        newDetail.spare5 = "spare5"

        statement.detailList.Add(newDetail)

        newDetail = New StatementDetail

        newDetail.serialNum = 2             '일련번호 1부터 순차 기재
        newDetail.purchaseDT = "20171122"   '거래일자  yyyyMMdd
        newDetail.itemName = "품명"
        newDetail.spec = "규격"


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
    ' 전자명세서에 다른 전자명세서 1건을 첨부합니다.
    '=========================================================================
    Private Sub btnAttachStmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachStmt.Click

        '첨부할 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim subItemCode As Integer = 121

        '첨부할 전자명세서 관리번호
        Dim subMgtKey As String = "20171122-02"

        Try
            Dim response As Response = statementService.AttachStatement(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, subItemCode, subMgtKey)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서에 첨부된 다른 전자명세서를 첨부해제합니다.
    '=========================================================================
    Private Sub btnDetachStmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetachStmt.Click

        '첨부해제 대상 전자명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표,126-영수증
        Dim subItemCode As Integer = 121

        '첨부해제 대상 전자명세서 관리번호
        Dim subMgtKey As String = "20171122-02"

        Try
            Dim response As Response = statementService.DetachStatement(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, subItemCode, subMgtKey)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 전자명세서 보기 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        Try
            Dim url As String = statementService.GetPopUpURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 전자명세서 인쇄팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click

        Try
            Dim url As String = statementService.GetPrintURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 인쇄(공급받는자) URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEPrintURL.Click

        Try
            Dim url As String = statementService.GetEPrintURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 다수건의 전자명세서 인쇄팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMassPrintURL.Click

        Dim MgtKeyList As List(Of String) = New List(Of String)

        '문서관리번호 배열, 최대 1000건
        MgtKeyList.Add("20171122-01")
        MgtKeyList.Add("20171122-02")

        Try
            Dim url As String = statementService.GetMassPrintURL(txtCorpNum.Text, selectedItemCode, MgtKeyList, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 공급받는자 메일링크 URL을 반환합니다.
    ' - 메일링크 URL은 유효시간이 존재하지 않습니다.
    '=========================================================================
    Private Sub btnGetMailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMailURL.Click
        Try
            Dim url As String = statementService.GetMailURL(txtCorpNum.Text, selectedItemCode, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전자명세서 > 임시(연동)문서함 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TBOX.Click
        Try
            Dim url As String = statementService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 > 전자명세서 > 발행문서함 팝업 URL을 반환합니다.
    ' - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
    '=========================================================================
    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_SBOX.Click
        Try
            Dim url As String = statementService.GetURL(txtCorpNum.Text, txtUserId.Text, "SBOX")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '전자명세서 메일전송 항목에 대한 전송여부를 목록으로 반환한다.
    '=========================================================================
    Private Sub btnListEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListEmailConfig.Click
        Try
            Dim emailConfigList As List(Of EmailConfig) = statementService.ListEmailConfig(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "메일전송유형 | 전송여부 " + vbCrLf

            For Each info As EmailConfig In emailConfigList
                If info.emailType = "SMT_ISSUE" Then tmp += "SMT_ISSUE (공급받는자에게 전자명세서가 발행 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "SMT_ACCEPT" Then tmp += "SMT_ACCEPT (공급자에게 전자명세서가 승인 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "SMT_DENY" Then tmp += "SMT_DENY (공급자게에 전자명세서가 거부 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "SMT_CANCEL" Then tmp += "SMT_CANCEL (공급받는자게에 전자명세서가 취소 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
                If info.emailType = "SMT_CANCEL_ISSUE" Then tmp += "SMT_CANCEL_ISSUE (공급받는자에게 전자명세서가 발행취소 되었음을 알려주는 메일) | " + info.sendYN.ToString + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '전자명세서 메일전송 항목에 대한 전송여부를 수정한다.
    '메일전송유형
    'SMT_ISSUE : 공급받는자에게 전자명세서가 발행 되었음을 알려주는 메일입니다.
    'SMT_ACCEPT : 공급자에게 전자명세서가 승인 되었음을 알려주는 메일입니다.
    'SMT_DENY : 공급자게에 전자명세서가 거부 되었음을 알려주는 메일입니다.
    'SMT_CANCEL : 공급받는자게에 전자명세서가 취소 되었음을 알려주는 메일입니다.
    'SMT_CANCEL_ISSUE : 공급받는자에게 전자명세서가 발행취소 되었음을 알려주는 메일입니다.
    '=========================================================================
    Private Sub btnUpdateEmailConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateEmailConfig.Click
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
End Class
