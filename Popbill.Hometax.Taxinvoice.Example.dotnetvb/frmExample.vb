'=========================================================================
'
' 팝빌 홈택스 전자세금계산서 매입매출 조회 API VB.Net SDK Example
'
' - VB.Net SDK 연동환경 설정방법 안내 : https://developers.popbill.com/guide/httaxinvoice/dotnet/getting-started/tutorial?fwn=vb
' - 업데이트 일자 : 2023-06-11
' - 연동 기술지원 연락처 : 1600-8536
' - 연동 기술지원 이메일 : code@linkhubcorp.com
'
' <테스트 연동개발 준비사항>
' 1) 23, 26번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
' 3) 홈택스 인증 처리를 합니다. (부서사용자등록 / 공인인증서 등록)
'    - 팝빌로그인 > [홈택스연동] > [환경설정] > [인증 관리] 메뉴
'    - 홈택스연동 인증 관리 팝업 URL(GetCertificatePopUpURL API) 반환된 URL을 이용하여
'      홈택스 인증 처리를 합니다.
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

        '연동환경 설정값, True-개발용, False-상업용
        htTaxinvoiceService.IsTest = True

        '인증토큰 발급 IP 제한 On/Off, True-사용, False-미사용, 기본값(True)
        htTaxinvoiceService.IPRestrictOnOff = True

        '팝빌 API 서비스 고정 IP 사용여부, True-사용, False-미사용, 기본값(False)
        htTaxinvoiceService.UseStaticIP = False

        '로컬시스템 시간 사용여부, True-사용, False-미사용, 기본값(False)
        htTaxinvoiceService.UseLocalTimeYN = False

    End Sub

    '=========================================================================
    '  홈택스에 신고된 전자세금계산서 매입/매출 내역 수집을 팝빌에 요청합니다. (조회기간 단위 : 최대 3개월)
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/job#RequestJob
    '=========================================================================
    Private Sub btnRequestJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequestJob.Click

        '전자(세금)계산서 유형, SELL-매출, BUY-매입, TURSTEE-위수탁
        Dim tiKeyType As KeyType = KeyType.SELL

        '일자유형, W-작성일자, I-발행일자, S-전송일자
        Dim DType As String = "S"

        '시작일자, 표시형식(yyyyMMdd)
        Dim SDate As String = "20220501"

        '종료일자, 표시형식(yyyyMMdd)
        Dim EDate As String = "20220513"

        Try
            Dim jobID As String = htTaxinvoiceService.RequestJob(txtCorpNum.Text, tiKeyType, DType, SDate, EDate)

            txtJobID.Text = jobID
            MsgBox("jobID(작업아이디) : " + jobID)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 수집 요청(RequestJob API) 함수를 통해 반환 받은 작업 아이디의 상태를 확인합니다.
    ' - 수집 결과 조회(Search API) 함수 또는 수집 결과 요약 정보 조회(Summary API) 함수를 사용하기 전에
    '   수집 작업의 진행 상태, 수집 작업의 성공 여부를 확인해야 합니다.
    ' - 작업 상태(jobState) = 3(완료)이고 수집 결과 코드(errorCode) = 1(수집성공)이면
    '   수집 결과 내역 조회(Search) 또는 수집 결과 요약 정보 조회(Summary)를 해야합니다.
    ' - 작업 상태(jobState)가 3(완료)이지만 수집 결과 코드(errorCode)가 1(수집성공)이 아닌 경우에는
    '   오류메시지(errorReason)로 수집 실패에 대한 원인을 파악할 수 있습니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/job#GetJobState
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
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 전자세금계산서 매입/매출 내역 수집요청에 대한 상태 목록을 확인합니다.
    ' - 수집 요청 후 1시간이 경과한 수집 요청건은 상태정보가 반환되지 않습니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/job#ListActiveJob
    '=========================================================================
    Private Sub btnListActiveJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListActiveJob.Click
        Try
            Dim jobList As List(Of HTTaxinvoiceJobState) = htTaxinvoiceService.ListActiveJob(txtCorpNum.Text)


            Dim tmp As String = "jobID(작업아이디) | 수집상태(jobState) | 수집유형(queryType) | 일자유형(queryDateType) | 시작일자(queryStDate) | 종료일자(queryEnDate) | "
            tmp += "errorCode(오류코드) | 오류메시지(errorReason) | 작업 시작일시(jobStartDT) | 작업 종료일시(jobEndDT) | 수집개수(collectCount) | 수집 요청일시(regDT) " + vbCrLf

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
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 수집 상태 확인(GetJobState API) 함수를 통해 상태 정보가 확인된 작업아이디를 활용하여 수집된 전자세금계산서 매입/매출 내역을 조회합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        ' 문서형태 배열 ("N" 와 "M" 중 선택, 다중 선택 가능)
        ' └ N = 일반 , M = 수정
        ' - 미입력 시 전체조회
        Dim tiType(2) As String
        tiType(0) = "N"
        tiType(1) = "M"

        ' 과세형태 배열 ("T" , "N" , "Z" 중 선택, 다중 선택 가능)
        ' └ T = 과세, N = 면세, Z = 영세
        ' - 미입력 시 전체조회
        Dim taxType(3) As String
        taxType(0) = "T"
        taxType(1) = "N"
        taxType(2) = "Z"

        ' 발행목적 배열 ("R" , "C", "N" 중 선택, 다중 선택 가능)
        ' └ R = 영수, C = 청구, N = 없음
        ' - 미입력 시 전체조회
        Dim purposeType(3) As String
        purposeType(0) = "R"
        purposeType(1) = "C"
        purposeType(2) = "N"

        ' 종사업장번호 유무 (null , "0" , "1" 중 택 1)
        ' - null = 전체조회 , 0 = 없음, 1 = 있음
        Dim TaxRegIDYN As String = ""

        ' 종사업장번호의 주체 ("S" , "B" , "T" 중 택 1)
        ' - S = 공급자 , B = 공급받는자 , T = 수탁자
        Dim TaxRegIDTYpe As String = "S"

        ' 종사업장번호
        ' - 다수기재 시 콤마(",")로 구분. ex) "0001,0002"
        ' - 미입력 시 전체조회
        Dim TaxRegID As String = ""

        '페이지 번호
        Dim Page As Integer = 1

        '페이지당 검색개수, 최대 1000건
        Dim PerPage As Integer = 10

        '정렬 방향, D-내림차순, A-오름차순
        Dim Order As String = "D"

        ' 거래처 상호 / 사업자번호 (사업자) / 주민등록번호 (개인) / "9999999999999" (외국인) 중 검색하고자 하는 정보 입력
        ' - 사업자번호 / 주민등록번호는 하이픈('-')을 제외한 숫자만 입력
        ' - 미입력시 전체조회
        Dim SearchString As String = ""

        Try
            listBox1.Items.Clear()

            Dim searchList As HTTaxinvoiceSearch = htTaxinvoiceService.Search(txtCorpNum.Text, txtJobID.Text, tiType, _
                                                                              taxType, purposeType, TaxRegIDYN, TaxRegIDTYpe, _
                                                                                TaxRegID, Page, PerPage, Order, txtUserId.Text, SearchString)

            Dim tmp As String = "code (응답코드) : " + CStr(searchList.code) + vbCrLf
            tmp += "message (응답메시지) : " + searchList.message + vbCrLf
            tmp += "total (총 검색결과 건수) : " + CStr(searchList.total) + vbCrLf
            tmp += "perPage (페이지당 검색개수) : " + CStr(searchList.perPage) + vbCrLf
            tmp += "pageNum (페이지 번호) : " + CStr(searchList.pageNum) + vbCrLf
            tmp += "pageCount (페이지 개수) : " + CStr(searchList.pageCount) + vbCrLf + vbCrLf

            MsgBox(tmp)

            Dim rowStr As String = "invoiceType(구분) | writeDate(작성일자) | issueDate(발행일자) | sendDate(전송일자) | invoiceeCorpName(공급자 상호) | invoiceeCorpNum(공급자 사업자번호) | "
            rowStr += "taxType(과세형태) | supplyCostTotal(공급가액) | modifyYN(문서형태) | ntsconfirmNum(국세청승인번호)"

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
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 수집 상태 확인(GetJobState API) 함수를 통해 상태 정보가 확인된 작업아이디를 활용하여 수집된 전자세금계산서 매입/매출 내역의 요약 정보를 조회합니다.
    ' - 요약 정보 : 전자세금계산서 수집 건수, 공급가액 합계, 세액 합계, 합계 금액
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#Summary
    '=========================================================================
    Private Sub btnSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSummary.Click

        ' 문서형태 배열 ("N" 와 "M" 중 선택, 다중 선택 가능)
        ' └ N = 일반 , M = 수정
        ' - 미입력 시 전체조회
        Dim tiType(2) As String
        tiType(0) = "N"
        tiType(1) = "M"

        ' 과세형태 배열 ("T" , "N" , "Z" 중 선택, 다중 선택 가능)
        ' └ T = 과세, N = 면세, Z = 영세
        ' - 미입력 시 전체조회
        Dim taxType(3) As String
        taxType(0) = "T"
        taxType(1) = "N"
        taxType(2) = "Z"

        ' 발행목적 배열 ("R" , "C", "N" 중 선택, 다중 선택 가능)
        ' └ R = 영수, C = 청구, N = 없음
        ' - 미입력 시 전체조회
        Dim purposeType(3) As String
        purposeType(0) = "R"
        purposeType(1) = "C"
        purposeType(2) = "N"

        ' 종사업장번호 유무 (null , "0" , "1" 중 택 1)
        ' - null = 전체조회 , 0 = 없음, 1 = 있음
        Dim TaxRegIDYN As String = ""

        ' 종사업장번호의 주체 ("S" , "B" , "T" 중 택 1)
        ' - S = 공급자 , B = 공급받는자 , T = 수탁자
        Dim TaxRegIDTYpe As String = "S"

        ' 종사업장번호
        ' - 다수기재 시 콤마(",")로 구분. ex) "0001,0002"
        ' - 미입력 시 전체조회
        Dim TaxRegID As String = ""

        ' 거래처 상호 / 사업자번호 (사업자) / 주민등록번호 (개인) / "9999999999999" (외국인) 중 검색하고자 하는 정보 입력
        ' - 사업자번호 / 주민등록번호는 하이픈('-')을 제외한 숫자만 입력
        ' - 미입력시 전체조회
        Dim SearchString As String = ""

        Try
            Dim summaryInfo As HTTaxinvoiceSummary = htTaxinvoiceService.Summary(txtCorpNum.Text, txtJobID.Text, _
                                                     tiType, taxType, purposeType, TaxRegIDYN, TaxRegIDTYpe, TaxRegID, _
                                                     txtUserId.Text, SearchString)

            Dim tmp As String = "count (수집결과건수) : " + CStr(summaryInfo.count) + vbCrLf
            tmp += "supplyCostTotal (공급가액 합계) : " + CStr(summaryInfo.supplyCostTotal) + vbCrLf
            tmp += "taxTotal (세액 합계) : " + CStr(summaryInfo.taxTotal) + vbCrLf
            tmp += "amountTotal (합계 금액) : " + CStr(summaryInfo.amountTotal) + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 국세청 승인번호를 통해 수집한 전자세금계산서 1건의 상세정보를 반환합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#GetTaxinvoice
    '=========================================================================
    Private Sub btnGetTaxinvocie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTaxinvocie.Click
        Try
            Dim taxinvoiceInfo As HTTaxinvoice = htTaxinvoiceService.GetTaxinvoice(txtCorpNum.Text, txtNTSconfirmNum.Text)

            Dim tmp As String = "========전자(세금)계산서 정보=======" + vbCrLf
            tmp += "writeDate(작성일자) : " + taxinvoiceInfo.writeDate + vbCrLf
            tmp += "issueDT(발행일시) : " + taxinvoiceInfo.issueDT + vbCrLf
            tmp += "invoiceType(전자세금계산서 종류) : " + taxinvoiceInfo.invoiceType.ToString + vbCrLf
            tmp += "taxType(과세형태) : " + taxinvoiceInfo.taxType + vbCrLf
            tmp += "taxTotal(세액 합계) : " + taxinvoiceInfo.taxTotal + vbCrLf
            tmp += "supplyCostTotal(공급가액 합계) : " + taxinvoiceInfo.supplyCostTotal + vbCrLf
            tmp += "totalAmount(합계금액) : " + taxinvoiceInfo.totalAmount + vbCrLf
            tmp += "purposeType(영수/청구) : " + taxinvoiceInfo.purposeType + vbCrLf
            tmp += "serialNum(일련번호) : " + taxinvoiceInfo.serialNum + vbCrLf
            tmp += "cash(현금) : " + taxinvoiceInfo.cash + vbCrLf
            tmp += "chkBill(수표) : " + taxinvoiceInfo.chkBill + vbCrLf
            tmp += "credit(외상) : " + taxinvoiceInfo.credit + vbCrLf
            tmp += "note(어음) : " + taxinvoiceInfo.note + vbCrLf
            tmp += "remark1(비고1) : " + taxinvoiceInfo.remark1 + vbCrLf
            tmp += "remark2(비고2) : " + taxinvoiceInfo.remark2 + vbCrLf
            tmp += "remark3(비고3) : " + taxinvoiceInfo.remark3 + vbCrLf
            tmp += "ntsconfirmNum(국세청승인번호) : " + taxinvoiceInfo.ntsconfirmNum + vbCrLf + vbCrLf

            tmp += "========공급자 정보=======" + vbCrLf
            tmp += "invoicerCorpNum(사업자번호) : " + taxinvoiceInfo.invoicerCorpNum + vbCrLf
            tmp += "invoicerMgtKey(공급자 문서번호) : " + taxinvoiceInfo.invoicerMgtKey + vbCrLf
            tmp += "invoicerTaxRegID(종사업장번호) : " + taxinvoiceInfo.invoicerTaxRegID + vbCrLf
            tmp += "invoicerCorpName(상호) : " + taxinvoiceInfo.invoicerCorpName + vbCrLf
            tmp += "invoicerCEOName(대표자 성명) : " + taxinvoiceInfo.invoicerCEOName + vbCrLf
            tmp += "invoicerAddr(주소) : " + taxinvoiceInfo.invoicerAddr + vbCrLf
            tmp += "invoicerBizType(업태) : " + taxinvoiceInfo.invoicerBizType + vbCrLf
            tmp += "invoicerBizClass(종목) : " + taxinvoiceInfo.invoicerBizClass + vbCrLf
            tmp += "invoicerContactName(담당자 성명) : " + taxinvoiceInfo.invoicerContactName + vbCrLf
            tmp += "invoicerDeptName(담당자 부서명) : " + taxinvoiceInfo.invoicerDeptName + vbCrLf
            tmp += "invoicerTEL(담당자 연락처) : " + taxinvoiceInfo.invoicerTEL + vbCrLf
            tmp += "invoicerEmail(담당자 메일) : " + taxinvoiceInfo.invoicerEmail + vbCrLf + vbCrLf

            tmp += "========공급받는자 정보=======" + vbCrLf
            tmp += "invoiceeCorpNum(사업자번호) : " + taxinvoiceInfo.invoiceeCorpNum + vbCrLf
            tmp += "invoiceeType(공급받는자 구분) : " + taxinvoiceInfo.invoiceeType + vbCrLf
            tmp += "invoiceeMgtKey(공급받느자 문서번호) : " + taxinvoiceInfo.invoiceeMgtKey + vbCrLf
            tmp += "invoiceeTaxRegID(종사업장번호) : " + taxinvoiceInfo.invoiceeTaxRegID + vbCrLf
            tmp += "invoiceeCorpName(상호) : " + taxinvoiceInfo.invoiceeCorpName + vbCrLf
            tmp += "invoiceeCEOName(대표자 성명) : " + taxinvoiceInfo.invoiceeCEOName + vbCrLf
            tmp += "invoiceeAddr(주소) : " + taxinvoiceInfo.invoiceeAddr + vbCrLf
            tmp += "invoiceeBizType(업태) : " + taxinvoiceInfo.invoiceeBizType + vbCrLf
            tmp += "invoiceeBizClass(종목) : " + taxinvoiceInfo.invoiceeBizClass + vbCrLf
            tmp += "invoiceeContactName1(주)담당자 성명) : " + taxinvoiceInfo.invoiceeContactName1 + vbCrLf
            tmp += "invoiceeDeptName1(주)담당자 부서명) : " + taxinvoiceInfo.invoiceeDeptName1 + vbCrLf
            tmp += "invoiceeTEL1(주)담당자 연락처) : " + taxinvoiceInfo.invoiceeTEL1 + vbCrLf
            tmp += "invoiceeEmail1(주)담당자 이메일) : " + taxinvoiceInfo.invoiceeEmail1 + vbCrLf

            tmp += "========전자(세금)계산서 품목배열========" + vbCrLf

            For Each detailInfo In taxinvoiceInfo.detailList
                tmp += "serialNum(일련번호) : " + CStr(detailInfo.serialNum) + vbCrLf
                tmp += "purchaseDT(거래일자) : " + detailInfo.purchaseDT + vbCrLf
                tmp += "itemName(품명) : " + detailInfo.itemName + vbCrLf
                tmp += "spec(규격) : " + detailInfo.spec + vbCrLf
                tmp += "qty(수량) : " + detailInfo.qty + vbCrLf
                tmp += "unitCost(단가) : " + detailInfo.unitCost + vbCrLf
                tmp += "supplyCost(공급가액) : " + detailInfo.supplyCost + vbCrLf
                tmp += "tax(세액) : " + detailInfo.tax + vbCrLf
                tmp += "remark(비고) : " + detailInfo.remark + vbCrLf + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 국세청 승인번호를 통해 수집한 전자세금계산서 1건의 상세정보를 XML 형태의 문자열로 반환합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#GetXML
    '=========================================================================
    Private Sub btnGetXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetXML.Click
        Try
            Dim taxinvoiceXML As HTTaxinvoiceXML = htTaxinvoiceService.GetXML(txtCorpNum.Text, txtNTSconfirmNum.Text)

            Dim tmp As String = "ResultCode (응답코드) : " + taxinvoiceXML.ResultCode.ToString + vbCrLf
            tmp += "Message (국세청 승인번호) : " + taxinvoiceXML.Message + vbCrLf
            tmp += "retObject (XML문서) : " + taxinvoiceXML.retObject + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try

    End Sub

    '=========================================================================
    ' 수집된 전자세금계산서 1건의 상세내역을 확인하는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#GetPopUpURL
    '=========================================================================
    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        ' 조회할 전자세금계산서 국세청승인번호
        Dim NTSConfirmNum As String = txtNTSconfirmNum.Text

        Try
            Dim url As String = htTaxinvoiceService.GetPopUpURL(txtCorpNum.Text, NTSConfirmNum)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 수집된 전자세금계산서 1건의 상세내역을 인쇄하는 페이지의 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#GetPrintURL
    '=========================================================================
    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click

        ' 인쇄할 전자세금계산서 국세청승인번호
        Dim NTSConfirmNum As String = txtNTSconfirmNum.Text

        Try
            Dim url As String = htTaxinvoiceService.GetPrintURL(txtCorpNum.Text, NTSConfirmNum)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 홈택스연동 인증정보를 관리하는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#GetCertificatePopUpURL
    '=========================================================================
    Private Sub btnGetCertificatePopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCertificatePopUpURL.Click
        Try
            Dim url As String = htTaxinvoiceService.GetCertificatePopUpURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록된 인증서 만료일자를 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#GetCertificateExpireDate
    '=========================================================================
    Private Sub btnGetCertificateExpireDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCertificateExpireDate.Click
        Try
            Dim expireDate As String = htTaxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text)

            MsgBox("홈택스 공인인증서 만료일시 : " + expireDate)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 팝빌에 등록된 인증서로 홈택스 로그인 가능 여부를 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#CheckCertValidation
    '=========================================================================
    Private Sub btnCheckCertValidation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckCertValidation.Click
        Try
            Dim response As Response = htTaxinvoiceService.CheckCertValidation(txtCorpNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 홈택스연동 인증을 위해 팝빌에 전자세금계산서용 부서사용자 계정을 등록합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#RegistDeptUser
    '=========================================================================
    Private Sub btnRegistDeptUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistDeptUser.Click
        ' 홈택스에서 생성한 전자세금계산서 부서사용자 아이디
        Dim deptUserID As String = "userid_test"

        ' 홈택스에서 생성한 전자세금계산서 부서사용자 비밀번호
        Dim deptUserPWD As String = "passwd_test"

        Try
            Dim response As Response = htTaxinvoiceService.RegistDeptUser(txtCorpNum.Text, deptUserID, deptUserPWD)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 홈택스연동 인증을 위해 팝빌에 등록된 전자세금계산서용 부서사용자 계정을 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#CheckDeptUser
    '=========================================================================
    Private Sub btnCheckDeptUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckDeptUser.Click
        Try
            Dim response As Response = htTaxinvoiceService.CheckDeptUser(txtCorpNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록된 전자세금계산서용 부서사용자 계정 정보로 홈택스 로그인 가능 여부를 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#CheckLoginDeptUser
    '=========================================================================
    Private Sub btnCheckLoginDeptUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckLoginDeptUser.Click
        Try
            Dim response As Response = htTaxinvoiceService.CheckLoginDeptUser(txtCorpNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록된 홈택스 전자세금계산서용 부서사용자 계정을 삭제합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#DeleteDeptUser
    '=========================================================================
    Private Sub btnDeleteDeptUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteDeptUser.Click
        Try
            Dim response As Response = htTaxinvoiceService.DeleteDeptUser(txtCorpNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 홈택스연동 정액제 서비스 신청 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetFlatRatePopUpURL
    '=========================================================================
    Private Sub btnGetFlatRatePopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFlatRatePopUpURL.Click
        Try
            Dim url As String = htTaxinvoiceService.GetFlatRatePopUpURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 홈택스연동 정액제 서비스 상태를 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetFlatRateState
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
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = htTaxinvoiceService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeURL.Click
        Try
            Dim url As String = htTaxinvoiceService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPaymentURL.Click
        Try
            Dim url As String = htTaxinvoiceService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = htTaxinvoiceService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다. 
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click

        Try
            Dim remainPoint As Double = htTaxinvoiceService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerURL.Click
        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = htTaxinvoiceService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 홈택스연동(세금) API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetChargeInfo.Click
        Try
            Dim ChargeInfo As ChargeInfo = htTaxinvoiceService.GetChargeInfo(txtCorpNum.Text)

            Dim tmp As String = "unitCost (월정액요금) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = htTaxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = htTaxinvoiceService.CheckID(txtCorpNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#JoinMember
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
            Dim response As Response = htTaxinvoiceService.JoinMember(joinInfo)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAccessURL.Click
        Try
            Dim url As String = htTaxinvoiceService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#GetCorpInfo 
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
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#UpdateCorpInfo
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

            Dim response As Response = htTaxinvoiceService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#RegistContact
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
            Dim response As Response = htTaxinvoiceService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = htTaxinvoiceService.GetContactInfo(txtCorpNum.Text, contactID)

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
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = htTaxinvoiceService.ListContact(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "id(아이디) | personName(담당자명) | email(메일주소) | tel(연락처) |"
            tmp += "regDT(등록일시) | searchRole(담당자 권한) | mgrYN(관리자 여부) | state(상태)" + vbCrLf

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
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#UpdateContact
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
            Dim response As Response = htTaxinvoiceService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub
    
    '=========================================================================
    ' 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#PaymentRequest
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
            Dim response As PaymentResponse = htTaxinvoiceService.PaymentRequest(txtCorpNum.Text, paymentForm, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message+vbCrLf + "settleCode(정산코드) : " + response.settleCode)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetSettleResult
    '=========================================================================
    Private Sub btnGetSettleResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSettleResult.Click

        '정산코드
        Dim SettleCode As String = "202301160000000010"

        Try
            Dim response As PaymentHistory = htTaxinvoiceService.GetSettleResult (txtCorpNum.Text, SettleCode, txtUserId.Text)

            MsgBox(
                "productType(결제 내용) : " + response.productType + vbCrLf +
                "productName(정액제 상품명) : " + response.productName + vbCrLf +
                "settleType(결제 유형) : " + response.settleType + vbCrLf +
                "settlerName(담당자명) : " + response.settlerName + vbCrLf +
                "settlerEmail(담당자메일) : " + response.settlerEmail + vbCrLf +
                "settleCost(결제 금액) : " + response.settleCost + vbCrLf +
                "settlePoint(충전포인트) : " + response.settlePoint + vbCrLf +
                "settleState(결제 상태) : " + response.settleState.ToString + vbCrLf +
                "regDT(등록일시) : " + response.regDT + vbCrLf +
                "stateDT(상태일시) : " + response.stateDT
                )

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 결제내역을 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetPaymentHistory
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
            Dim result As PaymentHistoryResult = htTaxinvoiceService.GetPaymentHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, txtUserId.Text)

            Dim tmp As String = ""
            For Each history As PaymentHistory In result.list

            tmp += "productType(결제 내용) : " + history.productType + vbCrLf
            tmp += "productName(정액제 상품명) : " + history.productName + vbCrLf
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
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetUseHistory
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
            Dim result As UseHistoryResult = htTaxinvoiceService.GetUseHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, Order, txtUserId.Text)

            Dim tmp As String = ""
            For Each history As UseHistory In result.list

                tmp += "itemCode(서비스 코드) : " + history.itemCode + vbCrLf
                tmp += "txType(포인트 증감 유형) : " + history.txType + vbCrLf
                tmp += "txPoint(결제 유형) : " + history.txPoint + vbCrLf
                tmp += "balance(담당자명) : " + history.balance + vbCrLf
                tmp += "txDT(담당자메일) : " + history.txDT + vbCrLf
                tmp += "userID(결제 금액) : " + history.userID + vbCrLf
                tmp += "userName(충전포인트) : " + history.userName + vbCrLf
                tmp += vbCrLf

            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트를 환불 신청합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#Refund
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
            Dim response As RefundResponse = htTaxinvoiceService.Refund(txtCorpNum.Text,refundForm, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf +
                        "message(응답메시지) : " + response.Message + vbCrLf +
                   "refundCode(환불코드) : " +response.refundCode )

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 환불신청내역을 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetRefundHistory
    '=========================================================================
    Private Sub btnGetRefundHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundHistory.Click

        '목폭 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500


        Try
            Dim result As RefundHistoryResult  = htTaxinvoiceService.GetRefundHistory(txtCorpNum.Text,Page, PerPage, txtUserId.Text)

            Dim tmp As String = ""

            For Each history As RefundHistory In result.list
                tmp += "reqDT(신청일시) :" + history.reqDT + vbCrLf
                tmp += "requestPoint(환불 신청포인트) :" + history.requestPoint + vbCrLf
                tmp += "accountBank(환불계좌 은행명) :" + history.accountBank + vbCrLf
                tmp += "accountNum(환불계좌번호) :" + history.accountNum + vbCrLf
                tmp += "accountName(환불계좌 예금주명) :" + history.accountName + vbCrLf
                tmp += "state(상태) : " + history.state.ToString + vbCrLf
                tmp += "reason(환불사유) : " + history.reason + vbCrLf
            Next

            MsgBox("code(응답코드) : " + result.code.ToString + vbCrLf+
                   "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf+
                   "perPage(페이지당 검색개수) : " + result.perPage.ToString +vbCrLf+
                   "pageNum(페이지 번호) : " + result.pageNum.ToString +vbCrLf+
                   "pageCount(페이지 개수) : " + result.pageCount.ToString +vbCrLf +
                   "사용내역"+vbCrLf+
                   tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 포인트 환불에 대한 상세정보 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetRefundInfo
    '=========================================================================
    Private Sub btnGetRefundInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '환불코드
        Dim refundCode As String = "023040000017"

        Try
            Dim history As RefundHistory  = htTaxinvoiceService.GetRefundInfo(txtCorpNum.Text,refundCode, txtUserId.Text)

            MsgBox("reqDT(신청일시) :" + history.reqDT + vbCrLf+
                   "requestPoint(환불 신청포인트) :" + history.requestPoint + vbCrLf+
                   "accountBank(환불계좌 은행명) :" + history.accountBank + vbCrLf+
                   "accountNum(환불계좌번호) :" + history.accountNum + vbCrLf+
                   "accountName(환불계좌 예금주명) :" + history.accountName + vbCrLf+
                   "state(상태) : " + history.state.ToString + vbCrLf+
                   "reason(환불사유) : " + history.reason + vbCrLf
                   )

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 환불 가능한 포인트를 확인합니다. (보너스 포인트는 환불가능포인트에서 제외됩니다.)
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/point#GetRefundableBalance
    '=========================================================================
    Private Sub btnGetRefundableBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundableBalance.Click

        Try
            Dim refundableCode As Double  = htTaxinvoiceService.GetRefundableBalance(txtCorpNum.Text, txtUserId.Text)

            MsgBox("refundableCode(환불 가능 포인트) : " + refundableCode)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 가입된 연동회원의 탈퇴를 요청합니다.
    ' - 회원탈퇴 신청과 동시에 팝빌의 모든 서비스 이용이 불가하며, 관리자를 포함한 모든 담당자 계정도 일괄탈퇴 됩니다.
    ' - 회원탈퇴로 삭제된 데이터는 복원이 불가능합니다.
    ' - 관리자 계정만 회원탈퇴가 가능합니다.
    ' - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/member#QuitMember
    '=========================================================================
    Private Sub btnQuitMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitMember.Click

        '탈퇴사유
        Dim quitReason As String = "회원 탈퇴 사유"

        Try
            Dim response As Response  = htTaxinvoiceService.QuitMember(txtCorpNum.Text, quitReason, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.Message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub
End Class
