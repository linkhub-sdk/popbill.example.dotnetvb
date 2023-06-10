﻿'=========================================================================
'
' 팝빌 계좌조회 API VB.Net SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내 : https://developers.popbill.com/guide/easyfinbank/dotnet/getting-started/tutorial?fwn=vb
' - 업데이트 일자 : 2022-05-13
' - 연동 기술지원 연락처 : 1600-8536
' - 연동 기술지원 이메일 : code@linkhubcorp.com
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

        '연동환경 설정값, True-개발용, False-상업용
        easyFinBankService.IsTest = True

        '인증토큰 발급 IP 제한 On/Off, True-사용, False-미사용, 기본값(True)
        easyFinBankService.IPRestrictOnOff = True

        '팝빌 API 서비스 고정 IP 사용여부, True-사용, False-미사용, 기본값(False)
        easyFinBankService.UseStaticIP = False

        '로컬시스템 시간 사용여부, True-사용, False-미사용, 기본값(False)
        easyFinBankService.UseLocalTimeYN = False

    End Sub

    '=========================================================================
    ' 계좌조회 서비스를 이용할 계좌를 팝빌에 등록합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/manage#RegistBankAccount
    '=========================================================================
    Private Sub btnRegistBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistBankAccount.Click

        Dim accountInfo As New EasyFinBankAccountForm

        ' 기관코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        accountInfo.BankCode = ""

        ' 계좌번호, 하이픈('-') 제외
        accountInfo.AccountNumber = ""

        ' 계좌비밀번호
        accountInfo.AccountPWD = ""

        ' 계좌유형, "법인" 또는 "개인" 입력
        accountInfo.AccountType = ""

        ' 예금주 식별정보 ('-' 제외)
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

        ' 정액제 이용할 개월수, 1~12 입력가능
        ' - 미입력시 기본값 1개월 처리
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

    '=========================================================================
    ' 팝빌에 등록된 계좌정보를 수정합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/manage#UpdateBankAccount
    '=========================================================================
    Private Sub btnUpdateBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateBankAccount.Click

        Dim accountInfo As New EasyFinBankAccountForm

        ' 기관코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        accountInfo.BankCode = ""

        ' 계좌번호, 하이픈('-') 제외
        accountInfo.AccountNumber = ""

        ' 계좌비밀번호
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

    '=========================================================================
    ' 팝빌에 등록된 계좌 정보를 확인합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/manage#GetBankAccountInfo
    '=========================================================================
    Private Sub btnGetBankAccountInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBankAccountInfo.Click

        ' 기관코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        Dim BankCode = ""

        ' 계좌번호, 하이픈('-') 제외
        Dim AccountNumber = ""


        Try
            Dim bankAccountInfo As EasyFinBankAccount = easyFinBankService.GetBankAccountInfo(txtCorpNum.Text, BankCode, AccountNumber)

            Dim tmp As String = ""
            tmp += "accountNumber (계좌번호) : " + bankAccountInfo.accountNumber + vbCrLf
            tmp += "bankCode (기관코드) : " + bankAccountInfo.bankCode + vbCrLf
            tmp += "accountName (계좌별칭) : " + bankAccountInfo.accountName + vbCrLf
            tmp += "accountType (계좌유형) : " + bankAccountInfo.accountType + vbCrLf
            tmp += "state (계좌 상태) : " + bankAccountInfo.state.ToString + vbCrLf
            tmp += "regDT (등록일시) : " + bankAccountInfo.regDT + vbCrLf
            tmp += "contractDT (정액제 서비스 시작일시) : " + bankAccountInfo.contractDT + vbCrLf
            tmp += "useEndDate (정액제 서비스 종료일자) : " + bankAccountInfo.useEndDate + vbCrLf
            tmp += "baseDate (자동연장 결제일) : " + bankAccountInfo.baseDate.ToString + vbCrLf
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
        ''

    End Sub

    '=========================================================================
    ' 팝빌에 등록된 은행계좌 목록을 반환한다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/manage#ListBankAccount
    '=========================================================================
    Private Sub btnListBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListBankAccount.Click

        Try
            Dim bankAccountList As List(Of EasyFinBankAccount) = easyFinBankService.ListBankAccount(txtCorpNum.Text)

            Dim tmp As String = ""
            tmp += "accountNumber (계좌번호) | bankCode (기관코드) | accountName (계좌별칭) | accountType (계좌유형) | state (계좌 상태) |"
            tmp += " regDT (등록일시)  | contractDT (정액제 서비스 시작일시) | useEndDate (정액제 서비스 종료일자) | baseDate (자동연장 결제일) |"
            tmp += " contractState (정액제 서비스 상태) | closeRequestYN (정액제 해지신청 여부) | useRestrictYN (정액제 사용제한 여부) | closeOnExpired (정액제 만료시 해지여부) |"
            tmp += " unPaidYN (미수금 보유 여부) | memo (메모) " + vbCrLf + vbCrLf

            For Each info As EasyFinBankAccount In bankAccountList
                tmp += info.accountNumber + " | " + info.bankCode + " | " + info.accountName + " | " + info.accountType + " | " + info.state.ToString + " | "
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
    ' 계좌 등록, 수정 및 삭제할 수 있는 계좌 관리 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/manage#GetBankAccountMgtURL
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
    ' 계좌의 정액제 해지를 요청합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/manage#CloseBankAccount
    '=========================================================================
    Private Sub btnCloseBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBankAccount.Click

        ' 기관코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        Dim BankCode = ""

        ' 계좌번호, 하이픈('-') 제외
        Dim AccountNumber = ""

        ' 해지유형, "일반", "중도" 중 택 1
        ' 일반(일반해지) – 이용중인 정액제 기간 만료 후 해지
        ' 중도(중도해지) – 해지 요청일 기준으로 정지되고 팝빌 담당자가 승인시 해지
        ' └ 중도일 경우, 정액제 잔여기간은 일할로 계산되어 포인트 환불 (무료 이용기간 중 해지하면 전액 환불)
        Dim CloseType = "중도"


        Try
            Dim response As Response = easyFinBankService.CloseBankAccount(txtCorpNum.Text, BankCode, AccountNumber, CloseType)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 신청한 정액제 해지요청을 취소합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/manage#RevokeCloseBankAccount
    '=========================================================================
    Private Sub btnRevokeCloseBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevokeCloseBankAccount.Click

        ' 기관코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        Dim BankCode = ""

        ' 계좌번호, 하이픈('-') 제외
        Dim AccountNumber = ""

        Try
            Dim response As Response = easyFinBankService.RevokeCloseBankAccount(txtCorpNum.Text, BankCode, AccountNumber)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try


    End Sub

    '=========================================================================
    ' 등록된 계좌를 삭제합니다.
    ' - 정액제가 아닌 종량제 이용 시에만 등록된 계좌를 삭제할 수 있습니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/manage#DeleteBankAccount
    '=========================================================================
    Private Sub btnDeleteBankAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteBankAccount.Click

        ' 기관코드
        ' 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
        ' SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
        ' 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
        Dim BankCode = ""

        ' 계좌번호, 하이픈('-') 제외
        Dim AccountNumber = ""

        Try
            Dim response As Response = easyFinBankService.DeleteBankAccount(txtCorpNum.Text, BankCode, AccountNumber)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try

    End Sub


    '=========================================================================
    ' 계좌 거래내역을 확인하기 위해 팝빌에 수집요청을 합니다. (조회기간 단위 : 최대 1개월)
    ' - 조회일로부터 최대 3개월 이전 내역까지 조회할 수 있습니다.
    ' - 반환 받은 작업아이디는 함수 호출 시점부터 1시간 동안 유효합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/job#RequestJob
    '=========================================================================
    Private Sub btnRequestJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequestJob.Click

        '기관코드
        Dim BankCode As String = ""

        '은행 계좌번호
        Dim AccountNumber As String = ""

        ' 시작일자, 표시형식(yyyyMMdd)
        Dim SDate As String = "20220501"

        ' 종료일자, 표시형식(yyyyMMdd)
        Dim EDate As String = "20220513"

        Try

            Dim jobID As String = easyFinBankService.RequestJob(txtCorpNum.Text, BankCode, AccountNumber, SDate, EDate)

            MsgBox("작업아이디(JobID) : " + jobID)

            txtJobID.Text = jobID

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 수집 요청(RequestJob API) 함수를 통해 반환 받은 작업 아이디의 상태를 확인합니다.
    ' - 거래 내역 조회(Search API) 함수 또는 거래 요약 정보 조회(Summary API) 함수를 사용하기 전에
    '   수집 작업의 진행 상태, 수집 작업의 성공 여부를 확인해야 합니다.
    ' - 작업 상태(jobState) = 3(완료)이고 수집 결과 코드(errorCode) = 1(수집성공)이면
    '   거래 내역 조회(Search) 또는 거래 요약 정보 조회(Summary) 를 해야합니다.
    ' - 작업 상태(jobState)가 3(완료)이지만 수집 결과 코드(errorCode)가 1(수집성공)이 아닌 경우에는
    '   오류메시지(errorReason)로 수집 실패에 대한 원인을 파악할 수 있습니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/job#GetJobState
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
    ' 수집요청(RequestJob API) 함수를 통해 반환 받은 작업아이디의 목록을 확인합니다.
    ' - 수집 요청 후 1시간이 경과한 수집 요청건은 상태정보가 반환되지 않습니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/job#ListActiveJob
    '=========================================================================
    Private Sub btnListActiveJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListActiveJob.Click

        Try
            Dim jobList As List(Of EasyFinBankJobState) = easyFinBankService.ListACtiveJob(txtCorpNum.Text)

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
    ' 수집 상태 확인(GetJobState API) 함수를 통해 상태 정보가 확인된 작업아이디를 활용하여 계좌 거래 내역을 조회합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/search#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        ' 거래유형 배열 ("I" 와 "O" 중 선택, 다중 선택 가능)
        ' └ I = 입금 , O = 출금
        ' - 미입력 시 전체조회
        Dim tradeType(2) As String
        tradeType(0) = "I"
        tradeType(1) = "O"

        ' "입·출금액" / "메모" / "비고" 중 검색하고자 하는 값 입력
        ' - 메모 = 거래내역 메모저장(SaveMemo API) 함수를 사용하여 저장한 값
        ' - 비고 = EasyFinBankSearchDetail의 remark1, remark2, remark3 값
        ' - 미입력시 전체조회
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
                                                                              SearchString, Page, PerPage, Order)

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
    ' 수집 상태 확인(GetJobState API) 함수를 통해 상태 정보가 확인된 작업아이디를 활용하여 계좌 거래내역의 요약 정보를 조회합니다.
    ' - 요약 정보는 입·출 금액 합계, 입·출 거래 건수를 가리킵니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/search#Summary
    '=========================================================================
    Private Sub btnSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSummary.Click

        ' 거래유형 배열 ("I" 와 "O" 중 선택, 다중 선택 가능)
        ' └ I = 입금 , O = 출금
        ' - 미입력 시 전체조회
        Dim tradeType(2) As String
        tradeType(0) = "I"
        tradeType(1) = "O"

        ' "입·출금액" / "메모" / "비고" 중 검색하고자 하는 값 입력
        ' - 메모 = 거래내역 메모저장(SaveMemo API) 함수를 사용하여 저장한 값
        ' - 비고 = EasyFinBankSearchDetail의 remark1, remark2, remark3 값
        ' - 미입력시 전체조회
        Dim SearchString As String = ""

        Try

            Dim summaryInfo As EasyFinBankSummary = easyFinBankService.Summary(txtCorpNum.Text, txtJobID.Text, tradeType, _
                                                                              SearchString)

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
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/search#SaveMemo
    '=========================================================================
    Private Sub btnSaveMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveMemo.Click

        '거래내역 메모
        Dim Memo As String = "메모-테스트"

        Try

            Dim response As Response = easyFinBankService.SaveMemo(txtCorpNum.Text, txtTID.Text, Memo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub


    '=========================================================================
    ' 계좌조회 정액제 서비스 신청 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/point#GetFlatRatePopUpURL
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
    ' 계좌조회 정액제 서비스 상태를 확인합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/point#GetFlatRateState
    '=========================================================================
    Private Sub btnGetFlatRateState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFlatRateState.Click

        '기관코드
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

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/point#GetBalance
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
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/point#GetChargeURL
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
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/point#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPaymentURL.Click
        Try
            Dim url As String = easyFinBankService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/point#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = easyFinBankService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/point#GetPartnerBalance
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
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/point#GetPartnerURL
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
    ' 팝빌 계좌조회 API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/point#GetChargeInfo
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
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/member#CheckIsMember
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
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/member#CheckID
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
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/member#JoinMember
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
            Dim response As Response = easyFinBankService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/member#GetAccessURL
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
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/member#GetCorpInfo
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
    ' 연동회원의 회사정보를 수정합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/member#UpdateCorpInfo
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
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/member#RegistContact
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
        joinData.tel = ""

        '담당자 이메일 (최대 100자)
        joinData.email = ""

        '담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = easyFinBankService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/member#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = easyFinBankService.GetContactInfo(txtCorpNum.Text, contactID)

            tmp += "id (담당자 아이디) : " + contactInfo.id + vbCrLf
            tmp += "personName (담당자명) : " + contactInfo.personName + vbCrLf
            tmp += "email (담당자 이메일) : " + contactInfo.email + vbCrLf
            tmp += "searchRole (담당자 권한) : " + contactInfo.searchRole.ToString() + vbCrLf
            tmp += "tel (연락처) : " + contactInfo.tel + vbCrLf
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
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/member#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = easyFinBankService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자명) | email(메일주소) | tel(연락처) |"
            tmp += "regDT(등록일시) | searchRole(담당자 권한) | mgrYN(관리자 여부) | state(상태)" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.email + " | " + info.tel + " | "
                tmp += info.regDT.ToString() + " | " + info.searchRole.ToString() + " | " + info.mgrYN.ToString() + " | " + info.state + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
    ' - https://developers.popbill.com/reference/easyfinbank/dotnet/api/member#UpdateContact
    '=========================================================================
    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디 (6자이상 50자미만)
        joinData.id = "testkorea1120"

        '담당자 성명 (최대 100자)
        joinData.personName = "담당자명"

        '담당자 연락처 (최대 20자)
        joinData.tel = ""

        '담당자 이메일 (최대 100자)
        joinData.email = ""

        '담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = easyFinBankService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub
    '=========================================================================
    ' 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#PaymentRequest
    '=========================================================================
    Private Sub btnPaymentRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentRequest.Click

        '입금신청 객체정보
        Dim paymentForm As New PaymentForm

        '담당자명
        paymentForm.settlerName	="담당자명"
        '담당자 이메일
        paymentForm.settlerEmail	="담당자 이메일"
        '담당자 휴대폰
        paymentForm.notifyHP	="담당자 휴대폰"
        '입금자명
        paymentForm.paymentName	="입금자명"
        '결제금액
        paymentForm.settleCost	="결제금액"

        Try
            Dim response As PaymentResponse = easyFinBankService.PaymentRequest(txtCorpNum.Text, paymentForm, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message+vbCrLf + "정산코드(settleCode) : " + response.settleCode)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetSettleResult
    '=========================================================================
    Private Sub btnGetSettleResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSettleResult.Click

        '정산코드
        Dim SettleCode As String = "202301160000000010"

        Try
            Dim response As PaymentHistory = easyFinBankService.GetSettleResult (txtCorpNum.Text, SettleCode, txtUserId.Text)

            MsgBox(
                "결제 내용(productType) : " + response.productType + vbCrLf +
                "정액제 상품명(productName) : " + response.productName + vbCrLf +
                "결제 유형(settleType) : " + response.settleType + vbCrLf +
                "담당자명(settlerName) : " + response.settlerName + vbCrLf +
                "담당자메일(settlerEmail) : " + response.settlerEmail + vbCrLf +
                "결제 금액(settleCost) : " + response.settleCost + vbCrLf +
                "충전포인트(settlePoint) : " + response.settlePoint + vbCrLf +
                "결제 상태(settleState) : " + response.settleState.ToString() + vbCrLf +
                "등록일시(regDT) : " + response.regDT + vbCrLf +
                "상태일시(stateDT) : " + response.stateDT
                )

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 결제내역을 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetPaymentHistory
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
            Dim result As PaymentHistoryResult = easyFinBankService.GetPaymentHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, txtUserId.Text)

            Dim tmp As String = ""
            For Each history As PaymentHistory In result.list

            tmp += "결제 내용(productType) : " + history.productType + vbCrLf
            tmp += "정액제 상품명(productName) : " + history.productName + vbCrLf
            tmp += "결제 유형(settleType) : " + history.settleType + vbCrLf
            tmp += "담당자명(settlerName) : " + history.settlerName + vbCrLf
            tmp += "담당자메일(settlerEmail) : " + history.settlerEmail + vbCrLf
            tmp += "결제 금액(settleCost) : " + history.settleCost + vbCrLf
            tmp += "충전포인트(settlePoint) : " + history.settlePoint + vbCrLf
            tmp += "결제 상태(settleState) : " + history.settleState.ToString() + vbCrLf
            tmp += "등록일시(regDT) : " + history.regDT + vbCrLf
            tmp += "상태일시(stateDT) : " + history.stateDT + vbCrLf
            tmp += vbCrLf

            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 사용내역을 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetUseHistory
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
            Dim result As UseHistoryResult = easyFinBankService.GetUseHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, Order, txtUserId.Text)

            Dim tmp As String = ""
            For Each history As UseHistory In result.list

                tmp += "서비스 코드(itemCode) : " + history.itemCode + vbCrLf
                tmp += "포인트 증감 유형(txType) : " + history.txType + vbCrLf
                tmp += "결제 유형(txPoint) : " + history.txPoint + vbCrLf
                tmp += "담당자명(balance) : " + history.balance + vbCrLf
                tmp += "담당자메일(txDT) : " + history.txDT + vbCrLf
                tmp += "결제 금액(userID) : " + history.userID + vbCrLf
                tmp += "충전포인트(userName) : " + history.userName + vbCrLf
                tmp += vbCrLf

            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트를 환불 신청합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#Refund
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
            Dim response As RefundResponse = easyFinBankService.Refund(txtCorpNum.Text,refundForm, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf +
                        "응답메시지(message) : " + response.Message + vbCrLf +
                   "환불코드(refundCode) : " +response.refundCode )

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 환불신청내역을 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetRefundHistory
    '=========================================================================
    Private Sub btnGetRefundHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundHistory.Click

        '목폭 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500


        Try
            Dim result As RefundHistoryResult  = easyFinBankService.GetRefundHistory(txtCorpNum.Text,Page, PerPage, txtUserId.Text)

            Dim tmp As String = ""

            For Each history As RefundHistory In result.list
                tmp += "reqDT (신청일시) :" + history.reqDT + vbCrLf
                tmp += "requestPoint (환불 신청포인트) :" + history.requestPoint + vbCrLf
                tmp += "accountBank (환불계좌 은행명) :" + history.accountBank + vbCrLf
                tmp += "accountNum (환불계좌번호) :" + history.accountNum + vbCrLf
                tmp += "accountName (환불계좌 예금주명) :" + history.accountName + vbCrLf
                tmp += "state (상태) : " + history.state.ToString() + vbCrLf
                tmp += "reason (환불사유) : " + history.reason + vbCrLf
            Next

            MsgBox("응답코드(code) : " + result.code.ToString() + vbCrLf+
                   "총 검색결과 건수(total) : " + result.total.ToString() + vbCrLf+
                   "페이지당 검색개수(perPage) : " + result.perPage.ToString() +vbCrLf+
                   "페이지 번호(pageNum) : " + result.pageNum.ToString() +vbCrLf+
                   "페이지 개수(pageCount) : " + result.pageCount.ToString() +vbCrLf +
                   "사용내역"+vbCrLf+
                   tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 포인트 환불에 대한 상세정보 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetRefundInfo
    '=========================================================================
    Private Sub btnGetRefundInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '환불코드
        Dim refundCode As String = "023040000017"

        Try
            Dim history As RefundHistory  = easyFinBankService.GetRefundInfo(txtCorpNum.Text,refundCode, txtUserId.Text)

            MsgBox("reqDT (신청일시) :" + history.reqDT + vbCrLf+
                   "requestPoint (환불 신청포인트) :" + history.requestPoint + vbCrLf+
                   "accountBank (환불계좌 은행명) :" + history.accountBank + vbCrLf+
                   "accountNum (환불계좌번호) :" + history.accountNum + vbCrLf+
                   "accountName (환불계좌 예금주명) :" + history.accountName + vbCrLf+
                   "state (상태) : " + history.state.ToString() + vbCrLf+
                   "reason (환불사유) : " + history.reason + vbCrLf
                   )

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 환불 가능한 포인트를 확인합니다. (보너스 포인트는 환불가능포인트에서 제외됩니다.)
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetRefundableBalance
    '=========================================================================
    Private Sub btnGetRefundableBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        Try
            Dim refundableCode As Double  = easyFinBankService.GetRefundableBalance(txtCorpNum.Text, txtUserId.Text)

            MsgBox("환불 가능 포인트(refundableCode) : " + refundableCode)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 가입된 연동회원의 탈퇴를 요청합니다.
    ' - 회원탈퇴 신청과 동시에 팝빌의 모든 서비스 이용이 불가하며, 관리자를 포함한 모든 담당자 계정도 일괄탈퇴 됩니다.
    ' - 회원탈퇴로 삭제된 데이터는 복원이 불가능합니다.
    ' - 관리자 계정만 회원탈퇴가 가능합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#QuitMember
    '=========================================================================
    Private Sub btnQuitMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '탈퇴사유
        Dim quitReason As String = "회원 탈퇴 사유"

        Try
            Dim response As Response  = easyFinBankService.QuitMember(txtCorpNum.Text, quitReason, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.Message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

End Class
