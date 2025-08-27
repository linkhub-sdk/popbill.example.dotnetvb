
'=========================================================================
' 팝빌 예금주조회 API .NET SDK VB.NET Example
' VB.NET 연동 튜토리얼 안내 : https://developers.popbill.com/guide/accountcheck/dotnet/getting-started/tutorial?fwn=vb
'
' 업데이트 일자 : 2025-08-27
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


Public Class frmExample

    '링크아이디
    Private LinkID As String = "TESTER"

    '비밀키
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '예금주조회 서비스 변수 선언
    Private accountCheckService As AccountCheckService


    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '예금주조회 서비스 객체 초기화
        accountCheckService = New AccountCheckService(LinkID, SecretKey)

        '연동환경 설정, true-테스트, false-운영(Production), (기본값:true)
        accountCheckService.IsTest = True

        '인증토큰 IP 검증 설정, true-사용, false-미사용, (기본값:true)
        accountCheckService.IPRestrictOnOff = True

        '통신 IP 고정, true-사용, false-미사용, (기본값:false)
        accountCheckService.UseStaticIP = False

        '로컬시스템 시간 사용여부, true-사용, false-미사용, (기본값:true)
        accountCheckService.UseLocalTimeYN = False

    End Sub

    '=========================================================================
    ' 1건의 예금주성명을 조회합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/checkAccount#CheckAccountInfo
    '=========================================================================
    Private Sub btnCheckAccountInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckAccountInfo.Click

        Try
            Dim accountInfo As AccountCheckInfo = accountCheckService.CheckAccountInfo(txtCorpNum.Text, txtBankCode.Text, txtAccountNumber.Text)

            Dim tmp As String = ""

            tmp += "result (상태코드) : " + accountInfo.result + vbCrLf
            tmp += "resultMessage (상태메시지) : " + accountInfo.resultMessage + vbCrLf
            tmp += "accountName (예금주 성명) : " + accountInfo.accountName + vbCrLf
            tmp += "bankCode (기관코드) : " + accountInfo.bankCode + vbCrLf
            tmp += "accountNumber (계좌번호) : " + accountInfo.accountNumber + vbCrLf
            tmp += "checkDT (확인일시) : " + accountInfo.checkDT + vbCrLf
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 1건의 예금주실명을 조회합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/api/checkDepositor#CheckDepositorInfo
    '=========================================================================
    Private Sub btnCheckDepositorInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckDepositorInfo.Click
        Try
            Dim accountInfo As DepositorCheckInfo = accountCheckService.CheckDepositorInfo(txtCorpNum.Text, txtBankCode.Text, txtAccountNumber.Text, txtIdentityNumTypeDC.Text, txtIdentityNumDC.Text)

            Dim tmp As String = ""

            tmp += "result (상태코드) : " + accountInfo.result + vbCrLf
            tmp += "resultMessage (상태메시지) : " + accountInfo.resultMessage + vbCrLf
            tmp += "accountName (예금주 성명) : " + accountInfo.accountName + vbCrLf
            tmp += "accountNumber (계좌번호) : " + accountInfo.accountNumber + vbCrLf
            tmp += "bankCode (기관코드) : " + accountInfo.bankCode + vbCrLf
            tmp += "identityNumType (실명번호 유형) : " + accountInfo.identityNumType + vbCrLf
            tmp += "identityNum (실명번호) : " + accountInfo.identityNum + vbCrLf
            tmp += "checkDT (확인일시) : " + accountInfo.checkDT + vbCrLf
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub
    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click

        Try
            Dim remainPoint As Double = accountCheckService.GetBalance(txtCorpNum.Text)

            MsgBox("remainPoint(연동회원 잔여포인트) : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click

        Try
            Dim url As String = accountCheckService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPaymentURL.Click
        Try
            Dim url As String = accountCheckService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = accountCheckService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click

        Try
            Dim remainPoint As Double = accountCheckService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("remainPoint(파트너 잔여포인트) : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = accountCheckService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 예금주 성명/실명 조회시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click

        ' 서비스 유형(성명/실명)
        Dim serviceType As String = "성명"

        Try
            Dim unitCost As Single = accountCheckService.GetUnitCost(txtCorpNum.Text, serviceType, txtUserId.Text)

            MsgBox(serviceType + " unitCost(조회 단가) : " + unitCost.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try

    End Sub

    '=========================================================================
    ' 예금주조회 API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click

        ' 서비스 유형(성명/실명)
        Dim serviceType As String = "성명"

        Try
            Dim ChargeInfo As ChargeInfo = accountCheckService.GetChargeInfo(txtCorpNum.Text, txtUserId.Text, serviceType)

            Dim tmp As String = "unitCost (조회단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = accountCheckService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = accountCheckService.CheckID(txtCorpNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#JoinMember
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

        '담당자 메일 (최대 20자)
        joinInfo.ContactEmail = ""

        '담당자 휴대폰 (최대 20자)
        joinInfo.ContactTEL = ""

        Try
            Dim response As Response = accountCheckService.JoinMember(joinInfo)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click

        Try
            Dim url As String = accountCheckService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#GetCorpInfo
    '=========================================================================
    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCorpInfo.Click
        Try
            Dim corpInfo As CorpInfo = accountCheckService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

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
    ' 연동회원의 회사정보를 수정합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#UpdateCorpInfo
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

            Dim response As Response = accountCheckService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#RegistContact
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디 (6자이상 50자미만)
        joinData.id = "testkorea20250723_01"

        '비밀번호, 8자 이상 20자 이하(영문, 숫자, 특수문자 조합)
        joinData.Password = "asdf8536!@#"

        '담당자 성명 (최대 100자)
        joinData.personName = "담당자명"

        '담당자 휴대폰 (최대 20자)
        joinData.tel = "010-1234-1234"

        '담당자 메일 (최대 100자)
        joinData.email = "test@email.com"

        '권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = accountCheckService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = accountCheckService.GetContactInfo(txtCorpNum.Text, contactID)

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
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click

        Try
            Dim contactList As List(Of Contact) = accountCheckService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자 성명) | email(담당자 메일) | tel( 담당자 휴대폰 연락처) |"
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
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#UpdateContact
    '=========================================================================
    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContact.Click

        '담당자 정보객체
        Dim joinData As New Contact

        '아이디 (6자이상 50자미만)
        joinData.id = "testkorea1120"

        '담당자 성명 (최대 100자)
        joinData.personName = "담당자명"

        '담당자 휴대폰 (최대 20자)
        joinData.tel = "010-1234-1234"

        '담당자 메일 (최대 100자)
        joinData.email = "test@email.com"

        '권한 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = accountCheckService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#PaymentRequest
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
            Dim response As PaymentResponse = accountCheckService.PaymentRequest(txtCorpNum.Text, paymentForm, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message + vbCrLf + "settleCode(정산코드) : " + response.settleCode)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetSettleResult
    '=========================================================================
    Private Sub btnGetSettleResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSettleResult.Click

        '정산코드
        Dim SettleCode As String = "202301160000000010"

        Try
            Dim response As PaymentHistory = accountCheckService.GetSettleResult(txtCorpNum.Text, SettleCode, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetPaymentHistory
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
            Dim result As PaymentHistoryResult = accountCheckService.GetPaymentHistory(txtCorpNum.Text, SDate, EDate, Page, PerPage, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetUseHistory
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
            Dim result As UseHistoryResult = accountCheckService.GetUseHistory(txtCorpNum.Text, SDate, EDate, Page, PerPage, Order, txtUserId.Text)

            Dim tmp As String = ""
            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "사용내역" + vbCrLf

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
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#Refund
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
            Dim response As RefundResponse = accountCheckService.Refund(txtCorpNum.Text, refundForm, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.Message + vbCrLf + "refundCode(환불코드) : " + response.refundCode)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 환불신청내역을 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetRefundHistory
    '=========================================================================
    Private Sub btnGetRefundHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundHistory.Click

        '목폭 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500


        Try
            Dim result As RefundHistoryResult = accountCheckService.GetRefundHistory(txtCorpNum.Text, Page, PerPage, txtUserId.Text)

            Dim tmp As String = ""

            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "환불내역" + vbCrLf

            For Each history As RefundHistory In result.list
                tmp += "reqDT(신청일시) :" + history.reqDT + vbCrLf
                tmp += "requestPoint(환불 신청포인트) :" + history.requestPoint + vbCrLf
                tmp += "accountBank(환불계좌 은행명) :" + history.accountBank + vbCrLf
                tmp += "accountNum(환불계좌번호) :" + history.accountNum + vbCrLf
                tmp += "accountName(환불계좌 예금주명) :" + history.accountName + vbCrLf
                tmp += "state(상태) : " + history.state.ToString + vbCrLf
                tmp += "reason(환불사유) : " + history.reason + vbCrLf
            Next


            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 포인트 환불에 대한 상세정보 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetRefundInfo
    '=========================================================================
    Private Sub btnGetRefundInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '환불코드
        Dim refundCode As String = "023040000017"

        Try
            Dim history As RefundHistory = accountCheckService.GetRefundInfo(txtCorpNum.Text, refundCode, txtUserId.Text)

            Dim tmp As String = ""

            tmp += "reqDT(신청일시) :" + history.reqDT + vbCrLf
            tmp += "requestPoint(환불 신청포인트) :" + history.requestPoint + vbCrLf
            tmp += "accountBank(환불계좌 은행명) :" + history.accountBank + vbCrLf
            tmp += "accountNum(환불계좌번호) :" + history.accountNum + vbCrLf
            tmp += "accountName(환불계좌 예금주명) :" + history.accountName + vbCrLf
            tmp += "state(상태) : " + history.state.ToString + vbCrLf
            tmp += "reason(환불사유) : " + history.reason + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 환불 가능한 포인트를 확인합니다. (보너스 포인트는 환불가능포인트에서 제외됩니다.)
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/point#GetRefundableBalance
    '=========================================================================
    Private Sub btnGetRefundableBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundableBalance.Click

        Try
            Dim refundableBalance As Double = accountCheckService.GetRefundableBalance(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#QuitMember
    '=========================================================================
    Private Sub btnQuitMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitMember.Click

        '탈퇴사유
        Dim quitReason As String = "회원 탈퇴 사유"

        Try
            Dim response As Response = accountCheckService.QuitMember(txtCorpNum.Text, quitReason, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.Message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원에 추가된 담당자를 삭제합니다.
    ' - https://developers.popbill.com/reference/accountcheck/dotnet/common-api/member#DeleteContact
    '=========================================================================
    Private Sub btnDeleteContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteContact.Click

        '삭제할 담당자 아이디
        Dim targetUserID As String = "testkorea20250723_01"

        Try
            Dim response As Response = accountCheckService.DeleteContact(txtCorpNum.Text, targetUserID, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub
End Class
