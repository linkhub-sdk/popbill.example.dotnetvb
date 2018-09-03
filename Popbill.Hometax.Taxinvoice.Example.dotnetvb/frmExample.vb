'=========================================================================
'
' 팝빌 홈택스 전자세금계산서 매입매출 조회 API VB.Net SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내 : http://blog.linkhub.co.kr/4453/
' - 업데이트 일자 : 2018-09-03
' - 연동 기술지원 연락처 : 1600-8536 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
'
' <테스트 연동개발 준비사항>
' 1) 23, 26번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
' 3) 홈택스에서 이용가능한 공인인증서를 등록합니다.
'    - 팝빌로그인 > [홈택스연계] > [환경설정] > [공인인증서 관리] 메뉴
'    - 공인인증서 등록(GetCertificatePopUpURL API) 반환된 URL을 이용하여
'      팝업 페이지에서 공인인증서 등록
'=========================================================================

Public Class frmExample

    '링크아이디
    Private LinkID As String = "TESTER"

    '비밀키
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '홈택스 세금계산서 서비스 변수 선언
    Private htTaxinvoiceService As HTTaxinvoiceService


    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '홈택스 세금계산서 서비스 객체 초기화
        htTaxinvoiceService = New HTTaxinvoiceService(LinkID, SecretKey)

        '연동환경 설정값 (True-개발용, False-상업용)
        htTaxinvoiceService.IsTest = True
    End Sub

    '=========================================================================
    ' 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
    ' - LinkID는 인증정보로 설정되어 있는 링크아이디 값입니다.
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = htTaxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID)

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
            Dim response As Response = htTaxinvoiceService.CheckID(txtCorpNum.Text)

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
            Dim response As Response = htTaxinvoiceService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 홈택스 전자세금계산서 API 서비스 과금정보를 확인합니다.
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click
        Try
            Dim ChargeInfo As ChargeInfo = htTaxinvoiceService.GetChargeInfo(txtCorpNum.Text)

            Dim tmp As String = "unitCost (발행단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

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
            Dim remainPoint As Double = htTaxinvoiceService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전 URL을 반환합니다.
    ' - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_CHRG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_CHRG.Click
        Try
            Dim url As String = htTaxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG")

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
            Dim remainPoint As Double = htTaxinvoiceService.GetPartnerBalance(txtCorpNum.Text)

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
            Dim url As String = htTaxinvoiceService.GetPartnerURL(txtCorpNum.Text, "CHRG")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌(www.popbill.com)에 로그인된 팝빌 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPopbillURL_LOGIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopbillURL_LOGIN.Click
        Try
            Dim url As String = htTaxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN")

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자를 신규로 등록합니다.
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistContact.Click
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
            Dim response As Response = htTaxinvoiceService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

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
            Dim contactList As List(Of Contact) = htTaxinvoiceService.ListContact(txtCorpNum.Text, txtUserId.Text)

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
            Dim response As Response = htTaxinvoiceService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

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
            Dim corpInfo As CorpInfo = htTaxinvoiceService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

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

            Dim response As Response = htTaxinvoiceService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 전자(세금)계산서 매출/매입 내역 수집을 요청합니다
    ' - 매출/매입 연계 프로세스는 "[홈택스 전자(세금)계산서 연계 API 연동매뉴얼]
    '   > 1.2. 프로세스 흐름도" 를 참고하시기 바랍니다.
    ' - 수집 요청후 반환받은 작업아이디(JobID)의 유효시간은 1시간 입니다.
    '=========================================================================
    Private Sub btnRequestJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequestJob.Click

        '전자(세금)계산서 유형, SELL-매출, BUY-매입, TURSTEE-위수탁
        Dim tiKeyType As KeyType = KeyType.SELL

        '일자유형, W-작성일자, I-발행일자, S-전송일자
        Dim DType As String = "I"

        '시작일자, 표시형식(yyyyMMdd)
        Dim SDate As String = "20170101"

        '종료일자, 표시형식(yyyyMMdd)
        Dim EDate As String = "20170601"

        Try
            Dim jobID As String = htTaxinvoiceService.RequestJob(txtCorpNum.Text, tiKeyType, DType, SDate, EDate)

            txtJobID.Text = jobID
            MsgBox("작업아이디(jobID) : " + jobID)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 수집 요청 상태를 확인합니다.
    ' - 응답항목 관한 정보는 "[홈택스 전자(세금)계산서 연계 API 연동매뉴얼
    '   > 3.2.2. GetJobState(수집 상태 확인)" 을 참고하시기 바랍니다 .
    '=========================================================================
    Private Sub btnGetJobState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetJobState.Click
        Try
            Dim jobINfo As HTTaxinvoiceJobState = htTaxinvoiceService.GetJobState(txtCorpNum.Text, txtJobID.Text)

            Dim tmp As String = "jobID(작업아이디) : " + jobINfo.jobID + vbCrLf
            tmp += "jobState(수집상태) : " + CStr(jobINfo.jobState) + vbCrLf
            tmp += "queryType(수집유형) : " + jobINfo.queryType + vbCrLf
            tmp += "queryDateType(일자유형) : " + jobINfo.queryDateType + vbCrLf
            tmp += "queryStDate(시작일자) : " + jobINfo.queryStDate + vbCrLf
            tmp += "queryEnDate(종료일자) : " + jobINfo.queryEnDate + vbCrLf
            tmp += "errorCode(오류코드) : " + CStr(jobINfo.errorCode) + vbCrLf
            tmp += "errorReason(오류메시지) : " + jobINfo.errorReason + vbCrLf
            tmp += "jobStartDT(작업 시작일시) : " + jobINfo.jobStartDT + vbCrLf
            tmp += "jobEndDT(작업 종료일시) : " + jobINfo.jobEndDT + vbCrLf
            tmp += "collectCount(수집개수) : " + CStr(jobINfo.collectCount) + vbCrLf
            tmp += "regDT(수집 요청일시) : " + jobINfo.regDT + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 수집 요청건들에 대한 상태 목록을 확인합니다.
    ' - 수집 요청 작업아이디(JobID)의 유효시간은 1시간 입니다.
    ' - 응답항목에 관한 정보는 "[홈택스 전자(세금)계산서 연계 API 연동매뉴얼]
    '   > 3.2.3. ListActiveJob (수집 상태 목록 확인)" 을 참고하시기 바랍니다.
    '=========================================================================
    Private Sub btnListActiveJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListActiveJob.Click
        Try
            Dim jobList As List(Of HTTaxinvoiceJobState) = htTaxinvoiceService.ListActiveJob(txtCorpNum.Text)


            Dim tmp As String = "jobID | jobState | queryType | queryDateType | queryStDate | queryEnDate | errorCode | errorReason | jobStartDT | jobEndDT | collectCount | regDT " + vbCrLf

            For Each info As HTTaxinvoiceJobState In jobList
                tmp += CStr(info.jobID) + " | "
                tmp += CStr(info.jobState) + " | "
                tmp += info.queryType + " | "
                tmp += info.queryDateType + " | "
                tmp += info.queryStDate + " | "
                tmp += info.queryEnDate + " | "
                tmp += CStr(info.errorCode) + " | "
                tmp += info.errorReason + " | "
                tmp += info.jobStartDT + " | "
                tmp += info.jobEndDT + " | "
                tmp += CStr(info.collectCount) + " | "
                tmp += info.regDT
                tmp += vbCrLf

                txtJobID.Text = info.jobID
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 검색조건을 사용하여 수집결과를 조회합니다.
    ' - 응답항목에 관한 정보는 "[홈택스 전자(세금)계산서 연계 API 연동매뉴얼]
    '   > 3.3.1. Search (수집 결과 조회)" 을 참고하시기 바랍니다.
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        '문서형태 배열, N-일반, M-수정
        Dim tiType(2) As String
        tiType(0) = "N"
        tiType(1) = "M"

        '과세형태 배열, T-과세, N-면세, Z-영세
        Dim taxType(3) As String
        taxType(0) = "T"
        taxType(1) = "N"
        taxType(2) = "Z"

        '영수/청구 배열, R-영수, C-청구, N-없음
        Dim purposeType(3) As String
        purposeType(0) = "R"
        purposeType(1) = "C"
        purposeType(2) = "N"


        '종사업장 유무, 공백-전체조회, 0-종사업장번호 없음, 1-종사업장번호 조회
        Dim TaxRegIDYN As String = ""

        '종사업장번호 사업자 유형, S-공급자, B-공급받는자, T-수탁자
        Dim TaxRegIDTYpe As String = "S"

        '종사업장번호 콤마(,)로 구분하여 구성 ex) "0001,0002"
        Dim TaxRegID As String = ""

        '페이지 번호
        Dim Page As Integer = 1

        '페이지당 검색개수, 최대 1000건
        Dim PerPage As Integer = 10

        '정렬 방향, D-내림차순, A-오름차순
        Dim Order As String = "D"

        Try
            listBox1.Items.Clear()

            Dim searchList As HTTaxinvoiceSearch = htTaxinvoiceService.Search(txtCorpNum.Text, txtJobID.Text, tiType, _
                                                                              taxType, purposeType, TaxRegIDYN, TaxRegIDTYpe, TaxRegID, Page, PerPage, Order)

            Dim tmp As String = "code (응답코드) : " + CStr(searchList.code) + vbCrLf
            tmp += "message (응답메시지) : " + searchList.message + vbCrLf
            tmp += "total (총 검색결과 건수) : " + CStr(searchList.total) + vbCrLf
            tmp += "perPage (페이지당 검색개수) : " + CStr(searchList.perPage) + vbCrLf
            tmp += "pageNum (페이지 번호) : " + CStr(searchList.pageNum) + vbCrLf
            tmp += "pageCount (페이지 개수) : " + CStr(searchList.pageCount) + vbCrLf + vbCrLf

            MsgBox(tmp)

            Dim rowStr As String = "구분 | 작성일자 | 발행일자 | 전송일자 | 거래처 | 등록번호 | 과세형태 | 공급가액 | 문서형태 | 국세청승인번호 "

            listBox1.Items.Add(rowStr)

            For Each tiInfo As HTTaxinvoiceAbbr In searchList.list
                rowStr = tiInfo.invoiceType + " | "
                rowStr += tiInfo.writeDate + " | "
                rowStr += tiInfo.issueDate + " | "
                rowStr += tiInfo.sendDate + " | "
                rowStr += tiInfo.invoiceeCorpName + " | "
                rowStr += tiInfo.invoiceeCorpNum + " | "
                rowStr += tiInfo.taxType + " | "
                rowStr += tiInfo.supplyCostTotal + " | "

                If tiInfo.modifyYN Then
                    rowStr += "수정 | "
                Else
                    rowStr += "일반 | "
                End If

                rowStr += tiInfo.ntsconfirmNum

                listBox1.Items.Add(rowStr)
            Next

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 검색조건을 사용하여 수집 결과 요약정보를 조회합니다.
    ' - 응답항목에 관한 정보는 "[홈택스 전자(세금)계산서 연계 API 연동매뉴얼]
    '   > 3.3.2. Summary (수집 결과 요약정보 조회)" 을 참고하시기 바랍니다.
    '=========================================================================
    Private Sub btnSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSummary.Click
        '문서형태 배열, N-일반, M-수정
        Dim tiType(2) As String
        tiType(0) = "N"
        tiType(1) = "M"

        '과세형태 배열, T-과세, N-면세, Z-영세
        Dim taxType(3) As String
        taxType(0) = "T"
        taxType(1) = "N"
        taxType(2) = "Z"

        '영수/청구 배열, R-영수, C-청구, N-없음
        Dim purposeType(3) As String
        purposeType(0) = "R"
        purposeType(1) = "C"
        purposeType(2) = "N"


        '종사업장 유무, 공백-전체조회, 0-종사업장번호 없음, 1-종사업장번호 조회
        Dim TaxRegIDYN As String = ""

        '종사업장번호 사업자 유형, S-공급자, B-공급받는자, T-수탁자
        Dim TaxRegIDTYpe As String = "S"

        '종사업장번호 콤마(,)로 구분하여 구성 ex) "0001,0002"
        Dim TaxRegID As String = ""

        Try
            Dim summaryInfo As HTTaxinvoiceSummary = htTaxinvoiceService.Summary(txtCorpNum.Text, txtJobID.Text, tiType, taxType, purposeType, TaxRegIDYN, TaxRegIDTYpe, TaxRegID)

            Dim tmp As String = "count (수집결과건수) : " + CStr(summaryInfo.count) + vbCrLf
            tmp += "supplyCostTotal (공급가액 합계) : " + CStr(summaryInfo.supplyCostTotal) + vbCrLf
            tmp += "taxTotal (세액 합계) : " + CStr(summaryInfo.taxTotal) + vbCrLf
            tmp += "amountTotal (합계 금액) : " + CStr(summaryInfo.amountTotal) + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 수집된 전자(세금)계산서 1건의 상세정보를 확인합니다.
    ' - 응답항목에 관한 정보는 "[홈택스 전자(세금)계산서 연계 API 연동매뉴얼]
    '   > 4.1.2. GetTaxinvoice 응답전문 구성" 을 참고하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetTaxinvocie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTaxinvocie.Click
        Try
            Dim taxinvoiceInfo As HTTaxinvoice = htTaxinvoiceService.GetTaxinvoice(txtCorpNum.Text, txtNTSconfirmNum.Text)

            Dim tmp As String = "========전자(세금)계산서 정보=======" + vbCrLf
            tmp += "writeDate : " + taxinvoiceInfo.writeDate + vbCrLf
            tmp += "issueDT : " + taxinvoiceInfo.issueDT + vbCrLf
            tmp += "invoiceType : " + taxinvoiceInfo.invoiceType.ToString() + vbCrLf
            tmp += "taxType : " + taxinvoiceInfo.taxType + vbCrLf
            tmp += "taxTotal : " + taxinvoiceInfo.taxTotal + vbCrLf
            tmp += "supplyCostTotal : " + taxinvoiceInfo.supplyCostTotal + vbCrLf
            tmp += "totalAmount : " + taxinvoiceInfo.totalAmount + vbCrLf
            tmp += "purposeType : " + taxinvoiceInfo.purposeType + vbCrLf
            tmp += "cash : " + taxinvoiceInfo.cash + vbCrLf
            tmp += "chkBill : " + taxinvoiceInfo.chkBill + vbCrLf
            tmp += "credit : " + taxinvoiceInfo.credit + vbCrLf
            tmp += "note : " + taxinvoiceInfo.note + vbCrLf
            tmp += "remark1 : " + taxinvoiceInfo.remark1 + vbCrLf
            tmp += "remark2 : " + taxinvoiceInfo.remark2 + vbCrLf
            tmp += "remark3 : " + taxinvoiceInfo.remark3 + vbCrLf
            tmp += "ntsconfirmNum : " + taxinvoiceInfo.ntsconfirmNum + vbCrLf + vbCrLf

            tmp += "========공급자 정보=======" + vbCrLf
            tmp += "invoicerCorpNum : " + taxinvoiceInfo.invoicerCorpNum + vbCrLf
            tmp += "invoicerMgtKey : " + taxinvoiceInfo.invoicerMgtKey + vbCrLf
            tmp += "invoicerTaxRegID : " + taxinvoiceInfo.invoicerTaxRegID + vbCrLf
            tmp += "invoicerCorpName : " + taxinvoiceInfo.invoicerCorpName + vbCrLf
            tmp += "invoicerCEOName : " + taxinvoiceInfo.invoicerCEOName + vbCrLf
            tmp += "invoicerAddr : " + taxinvoiceInfo.invoicerAddr + vbCrLf
            tmp += "invoicerBizType : " + taxinvoiceInfo.invoicerBizType + vbCrLf
            tmp += "invoicerBizClass : " + taxinvoiceInfo.invoicerBizClass + vbCrLf
            tmp += "invoicerContactName : " + taxinvoiceInfo.invoicerContactName + vbCrLf
            tmp += "invoicerDeptName : " + taxinvoiceInfo.invoicerDeptName + vbCrLf
            tmp += "invoicerTEL : " + taxinvoiceInfo.invoicerTEL + vbCrLf
            tmp += "invoicerEmail : " + taxinvoiceInfo.invoicerEmail + vbCrLf + vbCrLf

            tmp += "========공급받는자 정보=======" + vbCrLf
            tmp += "invoiceeCorpNum : " + taxinvoiceInfo.invoiceeCorpNum + vbCrLf
            tmp += "invoiceeType : " + taxinvoiceInfo.invoiceeType + vbCrLf
            tmp += "invoiceeMgtKey : " + taxinvoiceInfo.invoiceeMgtKey + vbCrLf
            tmp += "invoiceeTaxRegID : " + taxinvoiceInfo.invoiceeTaxRegID + vbCrLf
            tmp += "invoiceeCorpName : " + taxinvoiceInfo.invoiceeCorpName + vbCrLf
            tmp += "invoiceeCEOName : " + taxinvoiceInfo.invoiceeCEOName + vbCrLf
            tmp += "invoiceeAddr : " + taxinvoiceInfo.invoiceeAddr + vbCrLf
            tmp += "invoiceeBizType : " + taxinvoiceInfo.invoiceeBizType + vbCrLf
            tmp += "invoiceeBizClass : " + taxinvoiceInfo.invoiceeBizClass + vbCrLf
            tmp += "invoiceeContactName1 : " + taxinvoiceInfo.invoiceeContactName1 + vbCrLf
            tmp += "invoiceeDeptName1 : " + taxinvoiceInfo.invoiceeDeptName1 + vbCrLf
            tmp += "invoiceeTEL1 : " + taxinvoiceInfo.invoiceeTEL1 + vbCrLf
            tmp += "invoiceeEmail1 : " + taxinvoiceInfo.invoiceeEmail1 + vbCrLf

            tmp += "========전자(세금)계산서 품목배열========" + vbCrLf

            For Each detailInfo In taxinvoiceInfo.detailList
                tmp += "serialNum : " + CStr(detailInfo.serialNum) + vbCrLf
                tmp += "purchaseDT : " + detailInfo.purchaseDT + vbCrLf
                tmp += "itemName : " + detailInfo.itemName + vbCrLf
                tmp += "spec : " + detailInfo.spec + vbCrLf
                tmp += "qty : " + detailInfo.qty + vbCrLf
                tmp += "unitCost : " + detailInfo.unitCost + vbCrLf
                tmp += "supplyCost : " + detailInfo.supplyCost + vbCrLf
                tmp += "tax : " + detailInfo.tax + vbCrLf
                tmp += "remark : " + detailInfo.remark + vbCrLf + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' XML형식의 전자(세금)계산서 상세정보를 1건을 확인합니다.
    ' - 응답항목에 관한 정보는 "[홈택스 전자(세금)계산서 연계 API 연동매뉴얼]
    '   > 3.3.4. GetXML (상세정보 확인 - XML)" 을 참고하시기 바랍니다.
    '=========================================================================
    Private Sub btnGetXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetXML.Click
        Try
            Dim taxinvoiceXML As HTTaxinvoiceXML = htTaxinvoiceService.GetXML(txtCorpNum.Text, txtNTSconfirmNum.Text)

            Dim tmp As String = "ResultCode (응답코드) : " + taxinvoiceXML.ResultCode.ToString() + vbCrLf
            tmp += "Message (국세청 승인번호) : " + taxinvoiceXML.Message + vbCrLf
            tmp += "retObject (XML문서) : " + taxinvoiceXML.retObject + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try

    End Sub

    '=========================================================================
    ' 정액제 신청 팝업 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetFlatRatePopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFlatRatePopUpURL.Click
        Try
            Dim url As String = htTaxinvoiceService.GetFlatRatePopUpURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 정액제 서비스 이용상태를 확인합니다.
    '=========================================================================
    Private Sub btnGetFlatRateState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFlatRateState.Click
        Try
            Dim flatRateInfo As HTFlatRate = htTaxinvoiceService.GetFlatRateState(txtCorpNum.Text)

            Dim tmp As String = "referencdeID (사업자번호) : " + flatRateInfo.referenceID + vbCrLf
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
    ' 홈택스연계 인증관리 팝업 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetCertificatePopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCertificatePopUpURL.Click
        Try
            Dim url As String = htTaxinvoiceService.GetCertificatePopUpURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 등록된 홈택스 공인인증서의 만료일자를 확인합니다.
    '=========================================================================
    Private Sub btnGetCertificateExpireDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCertificateExpireDate.Click
        Try
            Dim expireDate As String = htTaxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text)

            MsgBox("홈택스 공인인증서 만료일시 : " + expireDate)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 세금계산서 1건의 팝업 보기 URL을 반환합니다.. 
    ' - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        ' 조회할 전자세금계산서 국세청승인번호
        Dim NTSConfirmNum As String = txtNTSconfirmNum.Text

        Try
            Dim url As String = htTaxinvoiceService.GetPopUpURL(txtCorpNum.Text, NTSConfirmNum)

            MsgBox(url)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '  팝빌에 등록된 공인인증서의 홈택스 로그인을 테스트한다.
    '=========================================================================
    Private Sub btnCheckCertValidation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckCertValidation.Click
        Try
            Dim response As Response = htTaxinvoiceService.CheckCertValidation(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '  홈택스 전자세금계산서 부서사용자 계정을 등록한다.
    '=========================================================================
    Private Sub btnRegistDeptUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistDeptUser.Click
        ' 홈택스에서 생성한 전자세금계산서 부서사용자 아이디
        Dim deptUserID As String = "userid_test"

        ' 홈택스에서 생성한 전자세금계산서 부서사용자 비밀번호
        Dim deptUserPWD As String = "passwd_test"

        Try
            Dim response As Response = htTaxinvoiceService.RegistDeptUser(txtCorpNum.Text, deptUserID, deptUserPWD)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '  팝빌에 등록된 전자세금계산서 부서사용자 아이디를 확인한다.
    '=========================================================================
    Private Sub btnCheckDeptUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckDeptUser.Click
        Try
            Dim response As Response = htTaxinvoiceService.CheckDeptUser(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록된 전자세금계산서 부서사용자 계정정보를 이용하여 홈택스 로그인을 테스트한다.
    '=========================================================================
    Private Sub btnCheckLoginDeptUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckLoginDeptUser.Click
        Try
            Dim response As Response = htTaxinvoiceService.CheckLoginDeptUser(txtCorpNum.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    '  팝빌에 등록된 전자세금계산서 부서사용자 계정정보를 삭제한다.
    '=========================================================================
    Private Sub btnDeleteDeptUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteDeptUser.Click
        Try
            Dim response As Response = htTaxinvoiceService.DeleteDeptUser(txtCorpNum.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub
End Class
