﻿'=========================================================================
'
' 팝빌 기업정보조회 API VB.Net SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내 : https://developers.popbill.com/guide/bizinfocheck/dotnet/getting-started/tutorial?fwn=vb
' - 업데이트 일자 : 2022-09-27
' - 연동 기술지원 연락처 : 1600-9854
' - 연동 기술지원 이메일 : code@linkhubcorp.com
'
' <테스트 연동개발 준비사항>
' 1) 19, 22번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
'=========================================================================

Public Class frmExample

    '링크아이디
    Private LinkID As String = "TESTER"

    '비밀키
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '기업정보조회 서비스 변수 선언
    Private bizInfoCheckService As BizInfoCheckService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '기업정보조회 서비스 객체 초기화
        bizInfoCheckService = New BizInfoCheckService(LinkID, SecretKey)

        '연동환경 설정값, True-개발용, False-상업용
        bizInfoCheckService.IsTest = True

        '인증토큰 발급 IP 제한 On/Off, True-사용, False-미사용, 기본값(True)
        bizInfoCheckService.IPRestrictOnOff = True

        '팝빌 API 서비스 고정 IP 사용여부, True-사용, False-미사용, 기본값(False)
        bizInfoCheckService.UseStaticIP = False

        '로컬시스템 시간 사용여부, True-사용, False-미사용, 기본값(False)
        bizInfoCheckService.UseLocalTimeYN = False

    End Sub

    '=========================================================================
    ' 사업자번호 1건에 대한 기업정보정보를 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/check#CheckBizInfo
    '=========================================================================
    Private Sub btnCheckBizInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckBizInfo.Click

        Try
            Dim result As BizCheckInfo = bizInfoCheckService.checkBizInfo(txtCorpNum.Text, txtTargetCorpNum.Text)

            Dim tmp As String = ""

            tmp += "corpNum (사업자번호) : " + result.corpNum + vbCrLf
            tmp += "companyRegNum (법인번호): " + result.companyRegNum + vbCrLf
            tmp += "checkDT (확인일시) : " + result.checkDT + vbCrLf
            tmp += "corpName (상호): " + result.corpName + vbCrLf
            tmp += "corpCode (기업형태코드): " + nullToString(result.corpCode) + vbCrLf
            tmp += "corpScaleCode (기업규모코드): " + nullToString(result.corpScaleCode) + vbCrLf
            tmp += "personCorpCode (개인법인코드): " + nullToString(result.personCorpCode) + vbCrLf
            tmp += "headOfficeCode (본점지점코드) : " + nullToString(result.headOfficeCode) + vbCrLf
            tmp += "industryCode (산업코드) : " + result.industryCode + vbCrLf
            tmp += "establishCode (설립구분코드) : " + nullToString(result.establishCode) + vbCrLf
            tmp += "establishDate (설립일자) : " + result.establishDate + vbCrLf
            tmp += "CEOName (대표자명) : " + result.ceoname + vbCrLf
            tmp += "workPlaceCode (사업장구분코드): " + nullToString(result.workPlaceCode) + vbCrLf
            tmp += "addrCode (주소구분코드) : " + nullToString(result.addrCode) + vbCrLf
            tmp += "zipCode (우편번호) : " + result.zipCode + vbCrLf
            tmp += "addr (주소) : " + result.addr + vbCrLf
            tmp += "addrDetail (상세주소) : " + result.addrDetail + vbCrLf
            tmp += "enAddr (영문주소) : " + result.enAddr + vbCrLf
            tmp += "bizClass (업종) : " + result.bizClass + vbCrLf
            tmp += "bizType (업태) : " + result.bizType + vbCrLf
            tmp += "result (결과코드) : " + nullToString(result.result) + vbCrLf
            tmp += "resultMessage (결과메시지) : " + result.resultMessage + vbCrLf
            tmp += "closeDownTaxType (사업자과세유형) : " + nullToString(result.closeDownTaxType) + vbCrLf
            tmp += "closeDownTaxTypeDate (과세유형전환일자):" + result.closeDownTaxTypeDate + vbCrLf
            tmp += "closeDownState (휴폐업상태) : " + nullToString(result.closeDownState) + vbCrLf
            tmp += "closeDownStateDate (휴폐업일자) : " + result.closeDownStateDate

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    Private Function nullToString(ByVal target As Integer?) As String
        If target IsNot Nothing Then
            Return target.ToString
        Else
            Return ""
        End If
    End Function


    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = bizInfoCheckService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = bizInfoCheckService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPaymentURL.Click
        Try
            Dim url As String = bizInfoCheckService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = bizInfoCheckService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = bizInfoCheckService.GetPartnerBalance(txtCorpNum.Text)
            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = bizInfoCheckService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 기업정보 조회시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            Dim unitCost As Single = bizInfoCheckService.GetUnitCost(txtCorpNum.Text)

            MsgBox("기업정보조회 단가(unitCost) : " + unitCost.ToString)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 기업정보조회 API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click
        Try
            Dim ChargeInfo As ChargeInfo = bizInfoCheckService.GetChargeInfo(txtCorpNum.Text)

            Dim tmp As String = "unitCost (조회단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = bizInfoCheckService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = bizInfoCheckService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try

    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#JoinMember
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
            Dim response As Response = bizInfoCheckService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try

    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = bizInfoCheckService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#GetCorpInfo
    '=========================================================================
    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCorpInfo.Click
        Try
            Dim corpInfo As CorpInfo = bizInfoCheckService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "ceoname(대표자성명) : " + corpInfo.ceoname + vbCrLf
            tmp += "corpName(상호) : " + corpInfo.corpName + vbCrLf
            tmp += "addr(주소) : " + corpInfo.addr + vbCrLf
            tmp += "bizType(업태) : " + corpInfo.bizType + vbCrLf
            tmp += "bizClass(종목) : " + corpInfo.bizClass + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#UpdateCorpInfo
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

            Dim response As Response = bizInfoCheckService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#RegistContact
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
        joinData.tel = "010-1234-1234"

        '담당자 이메일 (최대 100자)
        joinData.email = "test@email.com"

        '담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
        joinData.searchRole = 3

        Try
            Dim response As Response = bizInfoCheckService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = bizInfoCheckService.GetContactInfo(txtCorpNum.Text, contactID)

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
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = bizInfoCheckService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자명) | email(메일주소) | tel(연락처) |"
            tmp += "regDT(등록일시) | searchRole(담당자 권한) | mgrYN(관리자 여부) | state(상태)" + vbCrLf

            For Each info As Contact In contactList
                tmp += info.id + " | " + info.personName + " | " + info.email + " | " + info.tel + " | "
                tmp += info.regDT.ToString + " | " + info.searchRole.ToString + " | " + info.mgrYN.ToString + " | " + info.state + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException

            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#UpdateContact
    '=========================================================================
    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContact.Click

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
            Dim response As Response = bizInfoCheckService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub
    '=========================================================================
    ' 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#PaymentRequest
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
        paymentForm.settleCost = "결제금액"

        Try
            Dim response As PaymentResponse = bizInfoCheckService.PaymentRequest(txtCorpNum.Text, paymentForm, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.message+vbCrLf + "정산코드(settleCode) : " + response.settleCode)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetSettleResult
    '=========================================================================
    Private Sub btnGetSettleResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSettleResult.Click

        '정산코드
        Dim SettleCode As String = "202301160000000010"

        Try
            Dim response As PaymentHistory = bizInfoCheckService.GetSettleResult (txtCorpNum.Text, SettleCode, txtUserId.Text)

            MsgBox(
                "결제 내용(productType) : " + response.productType + vbCrLf +
                "정액제 상품명(productName) : " + response.productName + vbCrLf +
                "결제 유형(settleType) : " + response.settleType + vbCrLf +
                "담당자명(settlerName) : " + response.settlerName + vbCrLf +
                "담당자메일(settlerEmail) : " + response.settlerEmail + vbCrLf +
                "결제 금액(settleCost) : " + response.settleCost + vbCrLf +
                "충전포인트(settlePoint) : " + response.settlePoint + vbCrLf +
                "결제 상태(settleState) : " + response.settleState.ToString + vbCrLf +
                "등록일시(regDT) : " + response.regDT + vbCrLf +
                "상태일시(stateDT) : " + response.stateDT
                )

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 결제내역을 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetPaymentHistory
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
            Dim result As PaymentHistoryResult = bizInfoCheckService.GetPaymentHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, txtUserId.Text)

            Dim tmp As String = ""
            For Each history As PaymentHistory In result.list

            tmp += "결제 내용(productType) : " + history.productType + vbCrLf
            tmp += "정액제 상품명(productName) : " + history.productName + vbCrLf
            tmp += "결제 유형(settleType) : " + history.settleType + vbCrLf
            tmp += "담당자명(settlerName) : " + history.settlerName + vbCrLf
            tmp += "담당자메일(settlerEmail) : " + history.settlerEmail + vbCrLf
            tmp += "결제 금액(settleCost) : " + history.settleCost + vbCrLf
            tmp += "충전포인트(settlePoint) : " + history.settlePoint + vbCrLf
            tmp += "결제 상태(settleState) : " + history.settleState.ToString + vbCrLf
            tmp += "등록일시(regDT) : " + history.regDT + vbCrLf
            tmp += "상태일시(stateDT) : " + history.stateDT + vbCrLf
            tmp += vbCrLf

            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 사용내역을 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetUseHistory
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
            Dim result As UseHistoryResult = bizInfoCheckService.GetUseHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, Order, txtUserId.Text)

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
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트를 환불 신청합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#Refund
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
            Dim response As RefundResponse = bizInfoCheckService.Refund(txtCorpNum.Text,refundForm, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf +
                        "응답메시지(message) : " + response.Message + vbCrLf +
                   "환불코드(refundCode) : " +response.refundCode )

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 환불신청내역을 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetRefundHistory
    '=========================================================================
    Private Sub btnGetRefundHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundHistory.Click

        '목폭 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500


        Try
            Dim result As RefundHistoryResult  = bizInfoCheckService.GetRefundHistory(txtCorpNum.Text,Page, PerPage, txtUserId.Text)

            Dim tmp As String = ""

            For Each history As RefundHistory In result.list
                tmp+ = "신청일시(reqDT) :" + history.reqDT + vbCrLf
                tmp+ = "환불 신청포인트(requestPoint) :" + history.requestPoint + vbCrLf
                tmp+ = "환불계좌 은행명(accountBank) :" + history.accountBank + vbCrLf
                tmp+ = "환불계좌번호(accountNum) :" + history.accountNum + vbCrLf
                tmp+ = "환불계좌 예금주명(accountName) :" + history.accountName + vbCrLf
                tmp+ = "상태(state) : " + history.state.ToString + vbCrLf
                tmp+ = "환불사유(reason) : " + history.reason + vbCrLf
            Next

            MsgBox("응답코드(code) : " + result.code.ToString + vbCrLf+
                   "총 검색결과 건수(total) : " + result.total.ToString + vbCrLf+
                   "페이지당 검색개수(perPage) : " + result.perPage.ToString +vbCrLf+
                   "페이지 번호(pageNum) : " + result.pageNum.ToString +vbCrLf+
                   "페이지 개수(pageCount) : " + result.pageCount.ToString +vbCrLf +
                   "사용내역"+vbCrLf+
                   tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 포인트 환불에 대한 상세정보 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetRefundInfo
    '=========================================================================
    Private Sub btnGetRefundInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '환불코드
        Dim refundCode As String = "023040000017"

        Try
            Dim history As RefundHistory  = bizInfoCheckService.GetRefundInfo(txtCorpNum.Text,refundCode, txtUserId.Text)

            MsgBox("신청일시(reqDT) :" + history.reqDT + vbCrLf+
                   "환불 신청포인트(requestPoint) :" + history.requestPoint + vbCrLf+
                   "환불계좌 은행명(accountBank) :" + history.accountBank + vbCrLf+
                   "환불계좌번호(accountNum) :" + history.accountNum + vbCrLf+
                   "환불계좌 예금주명(accountName) :" + history.accountName + vbCrLf+
                   "상태(state) : " + history.state.ToString + vbCrLf+
                   "환불사유(reason) : " + history.reason + vbCrLf
                   )

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 환불 가능한 포인트를 확인합니다. (보너스 포인트는 환불가능포인트에서 제외됩니다.)
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/point#GetRefundableBalance
    '=========================================================================
    Private Sub btnGetRefundableBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundableBalance.Click

        Try
            Dim refundableCode As Double  = bizInfoCheckService.GetRefundableBalance(txtCorpNum.Text, txtUserId.Text)

            MsgBox("환불 가능 포인트(refundableCode) : " + refundableCode)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 가입된 연동회원의 탈퇴를 요청합니다.
    ' - 회원탈퇴 신청과 동시에 팝빌의 모든 서비스 이용이 불가하며, 관리자를 포함한 모든 담당자 계정도 일괄탈퇴 됩니다.
    ' - 회원탈퇴로 삭제된 데이터는 복원이 불가능합니다.
    ' - 관리자 계정만 회원탈퇴가 가능합니다.
    ' - https://developers.popbill.com/reference/bizinfocheck/dotnet/api/member#QuitMember
    '=========================================================================
    Private Sub btnQuitMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitMember.Click

        '탈퇴사유
        Dim quitReason As String = "회원 탈퇴 사유"

        Try
            Dim response As Response  = bizInfoCheckService.QuitMember(txtCorpNum.Text, quitReason, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString + vbCrLf + "응답메시지(message) : " + response.Message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub
End Class
