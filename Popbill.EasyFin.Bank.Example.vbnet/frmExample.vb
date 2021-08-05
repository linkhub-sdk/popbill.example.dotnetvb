'=========================================================================
'
' 팝빌 계좌조회 API VB.Net SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내 : https://docs.popbill.com/easyfinbank/tutorial/dotnet#vb
' - 업데이트 일자 : 2020-10-23
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

        '로컬PC 시간 사용 여부 True(사용), False(기본값) - 미사용
        easyFinBankService.UseLocalTimeYN = False

    End Sub

    '=========================================================================
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
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
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
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
    ' 사용자를 연동회원으로 가입처리합니다.
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
    ' 팝빌 계좌조회 API 서비스 과금정보를 확인합니다.
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
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = easyFinBankService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
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
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = easyFinBankService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = easyFinBankService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
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
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
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
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
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
    ' 계좌 거래내역을 확인하기 위해 팝빌에 수집요청을 합니다. 조회기간은 당일 기준으로 90일 이내로만 지정 가능합니다.
    ' - 반환 받은 작업아이디는 함수 호출 시점부터 1시간 동안 유효합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#RequestJob
    '=========================================================================
    Private Sub btnRequestJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequestJob.Click

        '은행코드
        Dim BankCode As String = "0048"

        '은행 계좌번호
        Dim AccountNumber As String = "131020538645"

        ' 시작일자, 표시형식(yyyyMMdd)
        Dim SDate As String = "20200701"

        ' 종료일자, 표시형식(yyyyMMdd)
        Dim EDate As String = "20200730"

        Try

            Dim jobID As String = easyFinBankService.RequestJob(txtCorpNum.Text, BankCode, AccountNumber, SDate, EDate)

            MsgBox("작업아이디(JobID) : " + jobID)

            txtJobID.Text = jobID

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' RequestJob(수집 요청)를 통해 반환 받은 작업아이디의 상태를 확인합니다.
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
    ' RequestJob(수집 요청)를 통해 반환 받은 작업아이디의 목록을 확인합니다.
    ' - 수집 요청 후 1시간이 경과한 수집 요청건은 상태정보가 반환되지 않습니다.
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
    ' GetJobState(수집 상태 확인)를 통해 상태 정보가 확인된 작업아이디를 활용하여 계좌 거래 내역을 조회합니다.
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
            tmp += "pageCount (페이지 개수) : " + CStr(searchList.pageCount) + vbCrLf
            tmp += "lastScrapDT (최종 조회일시) : " + CStr(searchList.lastScrapDT) + vbCrLf + vbCrLf

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
    ' GetJobState(수집 상태 확인)를 통해 상태 정보가 확인된 작업아이디를 활용하여 계좌 거래내역의 요약 정보를 조회합니다.
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
    ' 한 건의 거래 내역에 메모를 저장합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#SaveMemo
    '=========================================================================
    Private Sub btnSaveMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveMemo.Click

        '거래내역 메모
        Dim Memo As String = "20200701-테스트"

        Try

            Dim response As Response = easyFinBankService.SaveMemo(txtCorpNum.Text, txtTID.Text, Memo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 계좌 등록, 수정 및 삭제할 수 있는 계좌 관리 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetBankAccountMgtURL
    '=========================================================================
    Private Sub btnBankAccountMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBankAccountMgtURL.Click
        Try
            Dim url As String = easyFinBankService.GetBankAccountMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 계좌조회 정액제 서비스 신청 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetFlatRatePopUpURL
    '=========================================================================
    Private Sub btnFlatRatePopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlatRatePopUpURL.Click
        Try
            Dim url As String = easyFinBankService.GetFlatRatePopUpURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
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
            tmp += " regDT (등록일시)  | contractDT (정액제 서비스 시작일시) | useEndDate (정액제 서비스 종료일자) | baseDate (자동연장 결제일) |"
            tmp += " contractState (정액제 서비스 상태) | closeRequestYN (정액제 해지신청 여부) | useRestrictYN (정액제 사용제한 여부) | closeOnExpired (정액제 만료시 해지여부) |"
            tmp += " unPaidYN (미수금 보유 여부) | memo (메모) " + vbCrLf + vbCrLf

            For Each info As EasyFinBankAccount In bankAccountList
                tmp += info.bankCode + " | " + info.accountNumber + " | " + info.accountName + " | " + info.accountType + " | " + info.state.ToString + " | "
                tmp += info.regDT + " | " + info.contractDT + " | " + info.useEndDate + " | " + info.baseDate.ToString + " | "
                tmp += info.contractState.ToString + " | " + info.closeRequestYN.ToString + " | " + info.useRestrictYN.ToString + " | " + info.closeOnExpired.ToString + " | "
                tmp += info.unPaidYN.ToString + " | " + info.memo + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 계좌조회 정액제 서비스 상태를 확인합니다.
    ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetFlatRateState
    '=========================================================================
    Private Sub btnGetFlatRateState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFlatRateState.Click

        '은행코드
        Dim BankCode As String = ""

        '은행 계좌번호
        Dim AccountNumber As String = ""

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

    Private Sub btnRegistBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistBankAccount.Click

        '=========================================================================
        ' 계좌조회 서비스를 이용할 계좌를 팝빌에 등록합니다.
        ' - https://docs.popbill.com/easyfinbank/dotnet/api#RegistBankAccount
        '=========================================================================

        Dim accountInfo As New EasyFinBankAccountForm

        ' [필수] 은행코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        accountInfo.BankCode = ""

        ' [필수] 계좌번호, 하이픈('-') 제외
        accountInfo.AccountNumber = ""

        ' [필수] 계좌비밀번호
        accountInfo.AccountPWD = ""

        ' [필수] 계좌유형, "법인" 또는 "개인" 입력
        accountInfo.AccountType = ""

        ' [필수] 예금주 식별정보 (‘-‘ 제외)
        ' 계좌유형이 “법인”인 경우 : 사업자번호(10자리)
        ' 계좌유형이 “개인”인 경우 : 예금주 생년월일 (6자리-YYMMDD)
        accountInfo.IdentityNumber = ""

        ' 계좌 별칭
        accountInfo.AccountName = ""

        ' 인터넷뱅킹 아이디 (국민은행 필수)
        accountInfo.BankID = ""

        ' 조회전용 계정 아이디 (대구은행, 신협, 신한은행 필수)
        accountInfo.FastID = ""

        ' 조회전용 계정 비밀번호 (대구은행, 신협, 신한은행 필수)
        accountInfo.FastPWD = ""

        ' 결제기간(개월), 1~12 입력가능, 미기재시 기본값(1) 처리
        ' - 파트너 과금방식의 경우 입력값에 관계없이 1개월 처리
        accountInfo.UsePeriod = "1"

        ' 메모
        accountInfo.Memo = ""
       

        Try

            Dim response As Response = easyFinBankService.RegistBankACcount(txtCorpNum.Text, accountInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    Private Sub btnUpdateBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateBankAccount.Click

        '=========================================================================
        ' 팝빌에 등록된 계좌정보를 수정합니다.
        ' - https://docs.popbill.com/easyfinbank/dotnet/api#UpdateBankAccount
        '=========================================================================

        Dim accountInfo As New EasyFinBankAccountForm

        ' [필수] 은행코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        accountInfo.BankCode = ""

        ' [필수] 계좌번호, 하이픈('-') 제외
        accountInfo.AccountNumber = ""

        ' [필수] 계좌비밀번호
        accountInfo.AccountPWD = ""

        ' 계좌 별칭
        accountInfo.AccountName = ""

        ' 인터넷뱅킹 아이디 (국민은행 필수)
        accountInfo.BankID = ""

        ' 조회전용 계정 아이디 (대구은행, 신협, 신한은행 필수)
        accountInfo.FastID = ""

        ' 조회전용 계정 비밀번호 (대구은행, 신협, 신한은행 필수)
        accountInfo.FastPWD = ""

        ' 메모
        accountInfo.Memo = ""


        Try

            Dim response As Response = easyFinBankService.UpdateBankAccount(txtCorpNum.Text, accountInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetBankAccountInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBankAccountInfo.Click

        '=========================================================================
        ' 팝빌에 등록된 계좌 정보를 확인합니다.
        ' - https://docs.popbill.com/easyfinbank/dotnet/api#GetBankAccountInfo
        '=========================================================================

        ' [필수] 은행코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        Dim BankCode = ""

        ' [필수] 계좌번호, 하이픈('-') 제외
        Dim AccountNumber = ""


        Try
            Dim bankAccountInfo As EasyFinBankAccount = easyFinBankService.GetBankAccountInfo(txtCorpNum.Text, BankCode, AccountNumber)

            Dim tmp As String = "bankCode (은행코드) : " + bankAccountInfo.bankCode + vbCrLf
            tmp += "accountNumber (계좌번호) : " + bankAccountInfo.accountNumber + vbCrLf
            tmp += "accountName (계좌별칭) : " + bankAccountInfo.accountName + vbCrLf
            tmp += "accountType (계좌유형) : " + bankAccountInfo.accountType + vbCrLf
            tmp += "state (정액제 상태) : " + bankAccountInfo.state.ToString + vbCrLf
            tmp += "regDT (등록일시) : " + bankAccountInfo.regDT + vbCrLf
            tmp += "contractDT (정액제 서비스 시작일시) : " + bankAccountInfo.contractDT + vbCrLf
            tmp += "baseDate (자동연장 결제일) : " + bankAccountInfo.baseDate.ToString + vbCrLf
            tmp += "useEndDate (정액제 서비스 종료일자) : " + bankAccountInfo.useEndDate + vbCrLf
            tmp += "contractState (정액제 서비스 상태) : " + bankAccountInfo.contractState.ToString + vbCrLf
            tmp += "closeRequestYN (정액제 해지신청 여부) : " + bankAccountInfo.closeRequestYN.ToString + vbCrLf
            tmp += "useRestrictYN (정액제 사용제한 여부) : " + bankAccountInfo.useRestrictYN.ToString + vbCrLf
            tmp += "closeOnExpired (정액제 만료시 해지여부) : " + bankAccountInfo.closeOnExpired.ToString + vbCrLf
            tmp += "unPaiedYN (미수금 보유 여부) : " + bankAccountInfo.unPaidYN.ToString + vbCrLf
            tmp += "memo (메모) : " + bankAccountInfo.memo

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
        '

    End Sub

    Private Sub btnCloseBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBankAccount.Click

        '=========================================================================
        ' 계좌의 정액제 해지를 요청합니다.
        ' - https://docs.popbill.com/easyfinbank/dotnet/api#CloseBankAccount
        '=========================================================================

        ' [필수] 은행코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        Dim BankCode = ""

        ' [필수] 계좌번호, 하이픈('-') 제외
        Dim AccountNumber = ""

        ' [필수] 해지유형, “일반”, “중도” 중 선택 기재
        ' 일반해지 – 이용중인 정액제 사용기간까지 이용후 정지
        ' 중도해지 – 요청일 기준으로 정지, 정액제 잔여기간은 일할로 계산되어 포인트 환불 (무료 이용기간 중 중도해지 시 전액 환불)
        Dim CloseType = "중도"


        Try
            Dim response As Response = easyFinBankService.CloseBankAccount(txtCorpNum.Text, BankCode, AccountNumber, CloseType)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    Private Sub btnRevokeCloseBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevokeCloseBankAccount.Click

        '=========================================================================
        ' 신청한 정액제 해지요청을 취소합니다.
        ' - https://docs.popbill.com/easyfinbank/dotnet/api#RevokeCloseBankAccount
        '=========================================================================

        ' [필수] 은행코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        Dim BankCode = ""

        ' [필수] 계좌번호, 하이픈('-') 제외
        Dim AccountNumber = ""

        Try
            Dim response As Response = easyFinBankService.RevokeCloseBankAccount(txtCorpNum.Text, BankCode, AccountNumber)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try


    End Sub

    Private Sub btnDeleteBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteBankAccount.Click

        '=========================================================================
        ' 등록된 계좌를 삭제합니다.
        ' - 정액제가 아닌 종량제 이용 시에만 등록된 계좌를 삭제할 수 있습니다.
        ' - https://docs.popbill.com/easyfinbank/dotnet/api#DeleteBankAccount
        '=========================================================================

        ' [필수] 은행코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        Dim BankCode = ""

        ' [필수] 계좌번호, 하이픈('-') 제외
        Dim AccountNumber = ""

        Try
            Dim response As Response = easyFinBankService.DeleteBankAccount(txtCorpNum.Text, BankCode, AccountNumber, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try

    End Sub
End Class
