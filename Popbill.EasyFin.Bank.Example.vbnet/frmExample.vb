'=========================================================================
'
' 팝빌 계좌조회 API VB.Net SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내 : https://docs.popbill.com/easyfinbank/tutorial/dotnet#vb
' - 업데이트 일자 : 2020-01-21
' - 연동 기술지원 연락처 : 1600-8536 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
'
'=========================================================================

Public Class frmExample

    '링크아이디, 연동신청시 메일로 발급받은 인증정보로 변경
    Private LinkID As String = "TESTER"

    '비밀키, 연동신청시 메일로 발급받은 인증정보로 변경 유출주의
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '계좌조회 서비스 클래스 변수 선언
    Private easyFinBankService As EasyFinBankService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '서비스 객체 초기화
        easyFinBankService = New EasyFinBankService(LinkID, SecretKey)

        '연동환경 설정값 (True-개발용, False-상업용)
        easyFinBankService.IsTest = True

        '인증토큰의 IP제한기능 사용여부, (True-권장)
        easyFinBankService.IPRestrictOnOff = True

    End Sub

    '=========================================================================
    ' 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
    ' - LinkID는 인증정보로 설정되어 있는 링크아이디 값입니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = easyFinBankService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 회원아이디 중복여부를 확인합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = easyFinBankService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 연동회원으로 회원가입을 요청합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#JoinMember
    '=========================================================================
    Private Sub btnJoinMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJoinMember.Click

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
            Dim response As Response = easyFinBankService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 계좌조회 API 서비스 과금정보를 확인합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click
        Try
            Dim ChargeInfo As ChargeInfo = easyFinBankService.GetChargeInfo(txtCorpNum.Text)

            Dim tmp As String = "unitCost (월정액요금) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)를 통해 확인하시기 바랍니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = easyFinBankService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전 URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = easyFinBankService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click

        Try
            Dim remainPoint As Double = easyFinBankService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전 팝업 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = easyFinBankService.GetPartnerURL(txtCorpNum.Text, TOGO)

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
            Dim url As String = easyFinBankService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자를 신규로 등록합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#RegistContact
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistContact.Click

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
            Dim response As Response = easyFinBankService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자 목록을 확인합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = easyFinBankService.ListContact(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#UpdateContact
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
            Dim response As Response = easyFinBankService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetCorpInfo
    '=========================================================================
    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCorpInfo.Click
        Try
            Dim corpInfo As CorpInfo = easyFinBankService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#UpdateCorpInfo
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

            Dim response As Response = easyFinBankService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 계좌 거래내역 수집을 요청한다
    ' - 검색기간은 현재일 기준 90일 이내로만 요청할 수 있다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#RequestJob
    '=========================================================================
    Private Sub btnRequestJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequestJob.Click

        '은행코드
        Dim BankCode As String = "0048"

        '은행 계좌번호
        Dim AccountNumber As String = "131020538645"

        ' 시작일자, 표시형식(yyyyMMdd)
        Dim SDate As String = "20191001"

        ' 종료일자, 표시형식(yyyyMMdd)
        Dim EDate As String = "20191230"

        Try

            Dim jobID As String = easyFinBankService.RequestJob(txtCorpNum.Text, BankCode, AccountNumber, SDate, EDate)

            MsgBox("작업아이디(JobID) : " + jobID)

            txtJobID.Text = jobID

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '계좌조회 수집 상태를 확인한다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetJobState
    '=========================================================================
    Private Sub btnGetJobState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetJobState.Click

        Try
            Dim JobState As EasyFinBankJobState = easyFinBankService.GetJobState(txtCorpNum.Text, txtJobID.Text)

            Dim tmp As String = "jobID (작업아이디) : " + JobState.jobID + vbCrLf
            tmp += "jobState (수집상태) : " + JobState.jobState.ToString() + vbCrLf
            tmp += "startDate (시작일자) : " + JobState.startDate + vbCrLf
            tmp += "endDate (종료일자) : " + JobState.endDate + vbCrLf
            tmp += "errorCode (오류코드) : " + JobState.errorCode.ToString() + vbCrLf
            tmp += "errorReason (오류메시지) : " + JobState.errorReason + vbCrLf
            tmp += "jobStartDT (작업 시작일시) : " + JobState.jobStartDT + vbCrLf
            tmp += "jobEndDT (작업 종료일시) : " + JobState.jobEndDT + vbCrLf
            tmp += "regDT (수집 요청일시) : " + JobState.regDT + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 1시간 이내 수집 요청 목록을 반환한다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#ListActiveJob
    '=========================================================================
    Private Sub btnListActiveJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListActiveJob.Click

        Try
            Dim jobList As List(Of EasyFinBankJobState) = easyFinBankService.ListACtiveJob(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "jobID (작업아이디) | jobState (수집상태) | startDate (시작일자) | endDate (종료일자) | "
            tmp += " errorCode (오류코드) | errorReason (오류메시지) |"
            tmp += "jobStartDT (작업 시작일시) | jobEndDT (작업 종료일시) | regDT (수집 요청일시) " + vbCrLf

            For Each info As EasyFinBankJobState In jobList
                tmp += info.jobID + " | " + info.jobState.ToString + " | " + info.startDate + " | " + info.endDate + " | "
                tmp += info.errorCode.ToString + " | " + info.errorReason + " | "
                tmp += info.jobStartDT + " | " + info.jobEndDT + " | " + info.regDT + vbCrLf
            Next

            If (jobList.Count > 0) Then
                txtJobID.Text = jobList.Item(0).jobID
            End If

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 계좌 거래내역을 조회한다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        '거래유형 배열 I-입금, O-출금
        Dim tradeType(2) As String
        tradeType(0) = "I"
        tradeType(1) = "O"

        '조회 검색어, 조회 검색어, 입금/출금액, 메모, 적요 like 검색
        Dim SearchString As String = ""

        '페이지 번호
        Dim Page As Integer = 1

        '페이지당 검색개수, 최대 1000건
        Dim PerPage As Integer = 10

        '정렬 방향, D-내림차순, A-오름차순
        Dim Order As String = "D"

        Try
            ListBox1.Items.Clear()

            Dim searchList As EasyFinBankSearchResult = easyFinBankService.Search(txtCorpNum.Text, txtJobID.Text, tradeType, _
                                                                              SearchString, Page, PerPage, Order, txtUserId.Text)

            Dim tmp As String = "code (응답코드) : " + CStr(searchList.code) + vbCrLf
            tmp += "message (응답메시지) : " + searchList.message + vbCrLf
            tmp += "total (총 검색결과 건수) : " + CStr(searchList.total) + vbCrLf
            tmp += "perPage (페이지당 검색개수) : " + CStr(searchList.perPage) + vbCrLf
            tmp += "pageNum (페이지 번호) : " + CStr(searchList.pageNum) + vbCrLf
            tmp += "pageCount (페이지 개수) : " + CStr(searchList.pageCount) + vbCrLf + vbCrLf

            MsgBox(tmp)

            Dim rowStr As String = "tid (거래내역 아이디) | trdate(거래일자) | trserial(거래일자별 일련번호) | trdt(거래일시) | accIn(입금액) | accOut(출금액) | "
            rowStr += "balance(잔액) | remark1(비고1) | remark2(비고2) | remark3(비고3) | memo(메모)" 

            ListBox1.Items.Add(rowStr)

            For Each tradeInfo As EasyFinBankSearchDetail In searchList.list
                rowStr = tradeInfo.tid + " | "
                rowStr += tradeInfo.trdate + " | "
                rowStr += tradeInfo.trserial.ToString + " | "
                rowStr += tradeInfo.trdt + " | "
                rowStr += tradeInfo.accIn + " | "
                rowStr += tradeInfo.accOut + " | "
                rowStr += tradeInfo.balance + " | "
                rowStr += tradeInfo.remark1 + " | "
                rowStr += tradeInfo.remark2 + " | "
                rowStr += tradeInfo.remark3 + " | "

                rowStr += tradeInfo.memo

                ListBox1.Items.Add(rowStr)
            Next

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 거래 내역 요약정보를 조회한다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#Summary
    '=========================================================================
    Private Sub btnSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSummary.Click

        '거래유형 배열 I-입금, O-출금
        Dim tradeType(2) As String
        tradeType(0) = "I"
        tradeType(1) = "O"

        '조회 검색어, 조회 검색어, 입금/출금액, 메모, 적요 like 검색
        Dim SearchString As String = ""

        Try

            Dim summaryInfo As EasyFinBankSummary = easyFinBankService.Summary(txtCorpNum.Text, txtJobID.Text, tradeType, _
                                                                              SearchString, txtUserId.Text)

            Dim tmp As String = "count (수집결과 건수) : " + summaryInfo.count.ToString + vbCrLf
            tmp += "cntAccIn (입금거래 건수) : " + summaryInfo.cntAccIn.ToString + vbCrLf
            tmp += "cntAccOut (출금거래 건수) : " + summaryInfo.cntAccOut.ToString + vbCrLf
            tmp += "totalAccIn (입금액 합계) : " + summaryInfo.totalAccIn.ToString + vbCrLf
            tmp += "totalAccOut (출금액 합계) : " + summaryInfo.totalAccOut.ToString + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 계좌 거래내역에 메모를 저장한다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#SaveMemo
    '=========================================================================
    Private Sub btnSaveMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveMemo.Click

        '거래내역 메모
        Dim Memo As String = "20191230-테스트"

        Try

            Dim response As Response = easyFinBankService.SaveMemo(txtCorpNum.Text, txtTID.Text, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 은행 계좌 관리 팝업 URL을 반환한다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetBankAccountMgtURL
    '=========================================================================
    Private Sub btnBankAccountMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBankAccountMgtURL.Click
        Try
            Dim url As String = easyFinBankService.GetBankAccountMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 정액제 서비스 신청 URL을 반환한다.  
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetFlatRatePopUpURL
    '=========================================================================
    Private Sub btnFlatRatePopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlatRatePopUpURL.Click
        Try
            Dim url As String = easyFinBankService.GetFlatRatePopUpURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록된 은행계좌 목록을 반환한다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#ListBankAccount
    '=========================================================================
    Private Sub btnListBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListBankAccount.Click

        Try
            Dim bankAccountList As List(Of EasyFinBankAccount) = easyFinBankService.ListBankAccount(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "bankCode (은행코드) | accountNumber (계좌번호) | accountName (계좌별칭) | accountType (계좌유형) | state (정액제 상태) |"
            tmp += " regDT (등록일시) | memo (메모) " + vbCrLf

            For Each info As EasyFinBankAccount In bankAccountList
                tmp += info.bankCode + " | " + info.accountNumber + " | " + info.accountName + " | " + info.accountType + " | "
                tmp += info.state.ToString + " | " + info.regDT + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 정액제 서비스 상태를 확인한다
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetFlatRateState
    '=========================================================================
    Private Sub btnGetFlatRateState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFlatRateState.Click

        '은행코드
        Dim BankCode As String = "0048"

        '은행 계좌번호
        Dim AccountNumber As String = "131020538645"

        Try
            Dim flatRateInfo As EasyFinBankFlatRate = easyFinBankService.GetFlatRateState(txtCorpNum.Text, BankCode, AccountNumber)

            Dim tmp As String = "referencdeID (계좌아이디) : " + flatRateInfo.referenceID + vbCrLf
            tmp += "contractDT (정액제 서비스 시작일시) : " + flatRateInfo.contractDT + vbCrLf
            tmp += "useEndDate (정액제 서비스 종료일) : " + flatRateInfo.useEndDate + vbCrLf
            tmp += "baseDate (자동연장 결제일) : " + CStr(flatRateInfo.baseDate) + vbCrLf
            tmp += "state (정액제 서비스 상태) : " + CStr(flatRateInfo.state) + vbCrLf
            tmp += "closeRequestYN (서비스 해지신청 여부) : " + CStr(flatRateInfo.closeRequestYN) + vbCrLf
            tmp += "useRestrictYN (서비스 사용제한 여부) : " + CStr(flatRateInfo.useRestrictYN) + vbCrLf
            tmp += "closeOnExpired (서비스만료시 해지여부 ) : " + CStr(flatRateInfo.closeOnExpired) + vbCrLf
            tmp += "unPaidYN (미수금 보유 여부) : " + CStr(flatRateInfo.unPaidYN) + vbCrLf

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub
End Class
