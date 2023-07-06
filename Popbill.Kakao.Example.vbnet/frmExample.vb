'=========================================================================
'
' 팝빌 카카오톡 API VB.Net SDK Example
'
' - VB.Net 연동환경 설정방법 안내 : https://developers.popbill.com/guide/kakaotalk/dotnet/getting-started/tutorial?fwn=vb
' - 업데이트 일자 : 2023-07-03
' - 연동 기술지원 연락처 : 1600-9854
' - 연동 기술지원 이메일 : code@linkhubcorp.com
'
' <테스트 연동개발 준비사항>
' 1) 21, 24번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
'    링크허브 가입시 메일로 발급받은 인증정보를 참조하여 변경합니다.
' 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
' 3) 발신번호 사전등록을 합니다. (등록방법은 사이트/API 두가지 방식이 있습니다.)
'    - 1. 팝빌 사이트 로그인 > [문자/팩스] > [카카오톡] > [발신번호 사전등록] 메뉴에서 등록
'    - 2. getSenderNumberMgtURL API를 통해 반환된 URL을 이용하여 발신번호 등록
'=========================================================================

Public Class frmExample
    '링크아이디
    Private LinkID As String = "TESTER"

    '비밀키
    Private SecretKey As String = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I="

    '카카오톡 서비스 클래스 선언
    Private kakaoService As KakaoService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '카카오톡 서비스 클래스 초기화
        kakaoService = New KakaoService(LinkID, SecretKey)

        '연동환경 설정값, True-개발용, False-상업용
        kakaoService.IsTest = True

        '인증토큰 발급 IP 제한 On/Off, True-사용, False-미사용, 기본값(True)
        kakaoService.IPRestrictOnOff = True

        '팝빌 API 서비스 고정 IP 사용여부, True-사용, False-미사용, 기본값(False)
        kakaoService.UseStaticIP = False

        '로컬시스템 시간 사용여부, True-사용, False-미사용, 기본값(False)
        kakaoService.UseLocalTimeYN = False
    End Sub

    Private Function getReserveDT() As DateTime?
        If String.IsNullOrEmpty(txtReserveDT.Text) = False Then

            Return _
                DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
        End If
    End Function

    '=========================================================================
    ' 카카오톡 채널을 등록하고 내역을 확인하는 카카오톡 채널 관리 페이지 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/channel#GetPlusFriendMgtURL
    '=========================================================================
    Private Sub btnGetPlusFriendMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPlusFriendMgtURL.Click
        Try
            Dim url As String = kakaoService.GetPlusFriendMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록한 연동회원의 카카오톡 채널 목록을 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/channel#ListPlusFriendID
    '=========================================================================
    Private Sub btnListPlusFriendID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnListPlusFriendID.Click
        Try
            Dim plusFriendList As List(Of PlusFriend) = kakaoService.ListPlusFriendID(txtCorpNum.Text)

            Dim tmp As String = "카카오톡 채널 목록" + vbCrLf + vbCrLf

            For Each info As PlusFriend In plusFriendList
                tmp += "plusFriendID (검색용 아이디) : " + info.plusFriendID + vbCrLf
                tmp += "plusFriendName (채널명) : " + info.plusFriendName + vbCrLf
                tmp += "regDT (등록일시) : " + info.regDT + vbCrLf
                tmp += "state (채널 상태) : " + CStr(info.state) + vbCrLf
                tmp += "stateDT (채널 상태 일시) : " + info.stateDT + vbCrLf + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 카카오톡 발신번호 등록여부를 확인합니다.
    ' - 발신번호 상태가 '승인'인 경우에만 리턴값 'Response'의 변수 'code'가 1로 반환됩니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/sendnum#CheckSenderNumber
    '=========================================================================
    Private Sub btnCheckSenderNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckSenderNumber.Click
        Try
            Dim response As Response
            Dim senderNumber As String = ""

            response = kakaoService.CheckSenderNumber(txtCorpNum.Text, senderNumber)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 발신번호를 등록하고 내역을 확인하는 카카오톡 발신번호 관리 페이지 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/sendnum#GetSenderNumberMgtURL
    '=========================================================================
    Private Sub btnGetSenderNumberMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetSenderNumberMgtURL.Click
        Try
            Dim url As String = kakaoService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록한 연동회원의 카카오톡 발신번호 목록을 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/sendnum#GetSenderNumberList
    '=========================================================================
    Private Sub btnGetSenderNumberList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetSenderNumberList.Click
        Try
            Dim senderNumberList As List(Of SenderNumber) = kakaoService.GetSenderNumberList(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "number(발신번호) | representYN(대표번호여부) | state(인증상태) | memo(메모)" + vbCrLf

            For Each info As SenderNumber In senderNumberList
                tmp += info.number + " | " + CStr(info.state) + " | " + CStr(info.representYN) + " | " + info.memo + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 알림톡 템플릿을 신청하고 승인심사 결과를 확인하며 등록 내역을 확인하는 알림톡 템플릿 관리 페이지 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/template#GetATSTemplateMgtURL
    '=========================================================================
    Private Sub btnGetATSTemplateMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetATSTemplateMgtURL.Click
        Try
            Dim url As String = kakaoService.GetATSTemplateMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 승인된 알림톡 템플릿 정보를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/template#GetATSTemplate
    '=========================================================================
    Private Sub btnGetATSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetATSTemplate.Click

        '확인할 템플릿 코드
        Dim templateCode As String = "021010000076"

        Try
            Dim template As ATSTemplate = kakaoService.GetATSTemplate(txtCorpNum.Text, templateCode)

            Dim tmp As String = ""

            tmp += "[템플릿 정보]" + vbCrLf
            tmp += "templateCode(템플릿 코드) : " + template.templateCode + vbCrLf
            tmp += "templateName(템플릿 제목) : " + template.templateName + vbCrLf
            tmp += "template(템플릿 내용) : " + template.template + vbCrLf
            tmp += "plusFriendID(검색용 아이디) : " + template.plusFriendID + vbCrLf
            tmp += "ads(광고 메시지) : " + template.ads + vbCrLf
            tmp += "appendix(부가 메시지) : " + template.appendix + vbCrLf

            If Not template.btns Is Nothing Then
                For Each btnInfo As KakaoButton In template.btns
                    tmp += "[버튼정보]" + vbCrLf
                    tmp += "n(버튼명) : " + btnInfo.n + vbCrLf
                    tmp += "t(버튼유형) : " + btnInfo.t + vbCrLf
                    tmp += "u1(버튼링크1() : " + btnInfo.u1 + vbCrLf
                    tmp += "u2(버튼링크2() : " + btnInfo.u2 + vbCrLf
                Next
                tmp += vbCrLf
            End If

            tmp += "secureYN(보안템플릿 여부) : " + CStr(template.secureYN) + vbCrLf
            tmp += "state(템플릿 상태) : " + template.state + vbCrLf
            tmp += "stateDT(템플릿 상태 일시) : " + template.stateDT + vbCrLf

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 승인된 알림톡 템플릿 목록을 확인합니다.
    ' - 반환항목중 템플릿코드(templateCode)는 알림톡 전송시 사용됩니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/template#ListATSTemplate
    '=========================================================================
    Private Sub btnListATSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnListATSTemplate.Click
        Try
            Dim templateList As List(Of ATSTemplate) = kakaoService.ListATSTemplate(txtCorpNum.Text)

            Dim tmp As String = ""

            For Each info As ATSTemplate In templateList
                tmp += "[템플릿 정보]" + vbCrLf
                tmp += "templateCode(템플릿 코드) : " + info.templateCode + vbCrLf
                tmp += "templateName(템플릿 제목) : " + info.templateName + vbCrLf
                tmp += "template(템플릿 내용) : " + info.template + vbCrLf
                tmp += "plusFriendID(검색용 아이디) : " + info.plusFriendID + vbCrLf
                tmp += "ads(광고 메시지) : " + info.ads + vbCrLf
                tmp += "appendix(부가 메시지) : " + info.appendix + vbCrLf

                If Not info.btns Is Nothing Then
                    For Each btnInfo As KakaoButton In info.btns
                        tmp += "[버튼정보]" + vbCrLf
                        tmp += "n(버튼명) : " + btnInfo.n + vbCrLf
                        tmp += "t(버튼유형) : " + btnInfo.t + vbCrLf
                        tmp += "u1(버튼링크1() : " + btnInfo.u1 + vbCrLf
                        tmp += "u2(버튼링크2() : " + btnInfo.u2 + vbCrLf
                    Next
                    tmp += vbCrLf
                End If

                tmp += "secureYN(보안템플릿 여부) : " + CStr(info.secureYN) + vbCrLf
                tmp += "state(템플릿 상태) : " + info.state + vbCrLf
                tmp += "stateDT(템플릿 상태 일시) : " + info.stateDT + vbCrLf + vbCrLf
            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 승인된 템플릿의 내용을 작성하여 1건의 알림톡 전송을 팝빌에 접수합니다.
    ' - 사전에 승인된 템플릿의 내용과 알림톡 전송내용(content)이 다를 경우 전송실패 처리됩니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendATSOne
    '=========================================================================
    Private Sub btnSendATS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendATS_one.Click

        ' 승인된 알림톡 템플릿코드
        ' └ 알림톡 템플릿 관리 팝업 URL(GetATSTemplateMgtURL API) 함수, 알림톡 템플릿 목록 확인(ListATStemplate API) 함수를 호출하거나
        '    팝빌사이트에서 승인된 알림톡 템플릿 코드를  확인 가능.
        Dim templateCode As String = "019020000163"

        '팝빌에 사전등록된 발신번호
        '※ 대체문자를 전송하는 경우에만 필수 입력
        Dim senderNum As String = ""

        '알림톡 템플릿 내용 (최대 1000자)
        Dim content As String = "[ 팝빌 ]" + vbCrLf
        content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다." + vbCrLf
        content += "해당 템플릿으로 전송 가능합니다." + vbCrLf + vbCrLf
        content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf + vbCrLf
        content += "팝빌 파트너센터 : 1600-8536" + vbCrLf
        content += "support@linkhub.co.kr"

        ' 대체문자 제목
        ' - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
        Dim altSubject = "대체문자 제목"

        ' 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
        ' └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
        Dim altContent As String = "카카오톡이 실패하여 문자로 전송됩니다." + vbCrLf
        altContent += content

        ' 대체문자 유형 (null , "C" , "A" 중 택 1)
        ' null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
        Dim altSendType = "A"

        '수신번호
        Dim receiveNum = ""

        '수신자명
        Dim receiveName = "수신자명"

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        '버튼정보를 수정하지 않고 템플릿 신청시 기재한 정보로 전송하는 경우 null 처리
        Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

        '버튼링크 URL 에 #{템플릿변수}를 기재하여 승인받은경우 URL 수정하여 전송
        'Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)
        'Dim btnInfo As KakaoButton = New KakaoButton
        'btnInfo.n = "버튼명"                        '버튼명
        'btnInfo.t = "WL"                            '버튼유형 (DS - 배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
        'btnInfo.u1 = "https://www.linkhub.co.kr"     '버튼링크1 [앱링크] iOS / [웹링크] Mobile
        'btnInfo.u2 = "http://www.popbill.co.kr"     '버튼링크2 [앱링크] Android / [웹링크] PC URL
        'buttonList.Add(btnInfo)

        Try
            Dim receiptNum As String = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, altSubject, _
                                                            altSendType, getReserveDT(), receiveNum, receiveName, content, altContent, requestNum, buttonList)

            MsgBox("receiptNum(접수번호) : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 승인된 템플릿의 내용을 작성하여 다수건의 알림톡 전송을 팝빌에 접수하며, 수신자 별로 개별 내용을 전송합니다. (최대 1,000건)
    ' - 사전에 승인된 템플릿의 내용과 알림톡 전송내용(content)이 다를 경우 전송실패 처리됩니다.
    ' - 전송실패 시 사전에 지정한 변수 'altSendType' 값으로 대체문자를 전송할 수 있고, 이 경우 문자(SMS/LMS) 요금이 과금됩니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendATSMulti
    '=========================================================================
    Private Sub btnSendATS_multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendATS_multi.Click

        ' 승인된 알림톡 템플릿코드
        ' └ 알림톡 템플릿 관리 팝업 URL(GetATSTemplateMgtURL API) 함수, 알림톡 템플릿 목록 확인(ListATStemplate API) 함수를 호출하거나
        '   팝빌사이트에서 승인된 알림톡 템플릿 코드를  확인 가능.
        Dim templateCode As String = "019020000163"

        '알림톡 템플릿 내용 (최대 1000자)
        Dim content As String = "[ 팝빌 ]" + vbCrLf
        content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다." + vbCrLf
        content += "해당 템플릿으로 전송 가능합니다." + vbCrLf + vbCrLf
        content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf + vbCrLf
        content += "팝빌 파트너센터 : 1600-8536" + vbCrLf
        content += "support@linkhub.co.kr"

        '팝빌에 사전등록된 발신번호
        '※ 대체문자를 전송하는 경우에만 필수 입력
        Dim senderNum As String = ""

        ' 대체문자 유형 (null , "C" , "A" 중 택 1)
        ' null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
        Dim altSendType = "A"

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            msg.msg = content '알림톡 템플릿 내용 (최대 1000자)

            ' 대체문자 제목
            ' - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
            msg.altsjt = "대체문자 제목" + CStr(i)

            '대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
            ' └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
            msg.altmsg = "카카오톡이 실패하여 문자로 전송됩니다." + vbCrLf
            msg.altmsg += content

            '파트너 지정키, 대량전송시, 수신자 구별용 메모
            msg.interOPRefKey = "20220504-" + CStr(i) '파트너 지정키, 대량전송시, 수신자 구별용 메모

            '수신자별 개별 버튼정보 전송하는 경우
            '개별 버튼의 개수는 템플릿 신청 시 승인받은 버튼의 개수와 동일하게 생성, 다를경우 실패 처리
            '버튼링크URL에 #{템플릿변수}를 기재하여 승인받은 경우 URL 수정가능
            '버튼 표시명, 버튼 유형 수정 불가능
            'Dim btns As List(Of KakaoButton) = New List(Of KakaoButton) '수신자별 개별 버튼정보 리스트 생성

            'Dim btnInfo1 As KakaoButton = New KakaoButton     '개별 버튼정보 생성
            'btnInfo1.n = "템플릿 안내"                        '버튼명
            'btnInfo1.t = "WL"                                 '버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            'btnInfo1.u1 = "https://www.popbill.com"           '버튼링크1 [앱링크] Android / [웹링크] Mobile
            'btnInfo1.u2 = "http://test.popbill.com" + CStr(i) '버튼링크2 [앱링크] IOS / [웹링크] PC URL
            'btns.Add(btnInfo1)                                '개별 버튼정보 리스트에 개별 버튼정보 추가

            'Dim btnInfo2 As KakaoButton = New KakaoButton     '개별 버튼정보 생성
            'btnInfo2.n = "템플릿 안내"                        '버튼명
            'btnInfo2.t = "WL"                                 '버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            'btnInfo2.u1 = "https://www.test.com"              '버튼링크1 [앱링크] Android / [웹링크] Mobile
            'btnInfo2.u2 = "http://test.test.com" + CStr(i)    '버튼링크2 [앱링크] IOS / [웹링크] PC URL
            'btns.Add(btnInfo2)                                '개별 버튼정보 리스트에 개별 버튼정보 추가

            'msg.btns = btns '수신자 정보에 개별 버튼정보 리스트 추가

            receiverList.Add(msg)
        Next

        '버튼정보를 수정하지 않고 템플릿 신청시 기재한 정보로 전송하는 경우 null 처리
        '개별 버튼정보 전송하는 경우 null 처리
        Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

        '버튼링크 URL 에 #{템플릿변수}를 기재하여 승인받은경우 URL 수정하여 전송
        'Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)
        'Dim btnInfo As KakaoButton = New KakaoButton
        'btnInfo.n = "버튼명"                        '버튼명
        'btnInfo.t = "WL"                            '버튼유형 (DS - 배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
        'btnInfo.u1 = "https://www.linkhub.co.kr"     '버튼링크1 [앱링크] iOS / [웹링크] Mobile
        'btnInfo.u2 = "http://www.popbill.co.kr"     '버튼링크2 [앱링크] Android / [웹링크] PC URL
        'buttonList.Add(btnInfo)

        Try
            Dim receiptNum As String = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, _
                                                            altSendType, getReserveDT(), receiverList, txtUserId.Text, requestNum, buttonList)
            MsgBox("receiptNum(접수번호) : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 승인된 템플릿 내용을 작성하여 다수건의 알림톡 전송을 팝빌에 접수하며, 모든 수신자에게 동일 내용을 전송합니다. (최대 1,000건)
    ' - 사전에 승인된 템플릿의 내용과 알림톡 전송내용(content)이 다를 경우 전송실패 처리됩니다.
    ' - 전송실패시 사전에 지정한 변수 'altSendType' 값으로 대체문자를 전송할 수 있고, 이 경우 문자(SMS/LMS) 요금이 과금됩니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendATSSame
    '=========================================================================
    Private Sub btnSendATS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendATS_same.Click

        ' 승인된 알림톡 템플릿코드
        ' └ 알림톡 템플릿 관리 팝업 URL(GetATSTemplateMgtURL API) 함수, 알림톡 템플릿 목록 확인(ListATStemplate API) 함수를 호출하거나
        '   팝빌사이트에서 승인된 알림톡 템플릿 코드를  확인 가능.
        Dim templateCode As String = "019020000163"

        '팝빌에 사전등록된 발신번호
        '※ 대체문자를 전송하는 경우에만 필수 입력
        Dim senderNum As String = ""

        '알림톡 템플릿 내용 (최대 1000자)
        Dim content As String = "[ 팝빌 ]" + vbCrLf
        content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다." + vbCrLf
        content += "해당 템플릿으로 전송 가능합니다." + vbCrLf + vbCrLf
        content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf + vbCrLf
        content += "팝빌 파트너센터 : 1600-8536" + vbCrLf
        content += "support@linkhub.co.kr"

        ' 대체문자 제목
        ' - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
        Dim altSubject = "대체문자 제목"

        ' 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
        ' └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
        Dim altContent As String = "카카오톡이 실패하여 문자로 전송됩니다." + vbCrLf
        altContent += content

        ' 대체문자 유형 (null , "C" , "A" 중 택 1)
        ' null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
        Dim altSendType = "A"

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            msg.interOPRefKey = "20220504-" + CStr(i) '파트너 지정키, 수신자 구별용 메모.

            receiverList.Add(msg)
        Next

        '버튼정보를 수정하지 않고 템플릿 신청시 기재한 정보로 전송하는 경우 null 처리
        Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

        '버튼링크 URL 에 #{템플릿변수}를 기재하여 승인받은경우 URL 수정하여 전송
        'Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)
        'Dim btnInfo As KakaoButton = New KakaoButton
        'btnInfo.n = "버튼명"                        '버튼명
        'btnInfo.t = "WL"                            '버튼유형 (DS - 배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
        'btnInfo.u1 = "https://www.linkhub.co.kr"     '버튼링크1 [앱링크] iOS / [웹링크] Mobile
        'btnInfo.u2 = "http://www.popbill.co.kr"     '버튼링크2 [앱링크] Android / [웹링크] PC URL
        'buttonList.Add(btnInfo)

        Try
            Dim receiptNum As String = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, content, altSubject, _
                                                            altContent, altSendType, getReserveDT(), receiverList, txtUserId.Text, requestNum, buttonList)
            MsgBox("receiptNum(접수번호) : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 텍스트로 구성된 1건의 친구톡 전송을 팝빌에 접수합니다.
    ' - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
    ' - 전송실패시 사전에 지정한 변수 'altSendType' 값으로 대체문자를 전송할 수 있고, 이 경우 문자(SMS/LMS) 요금이 과금됩니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFTSOne
    '=========================================================================
    Private Sub btnSendFTS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFTS_one.Click

        '검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
        Dim plusFriendID As String = "@팝빌"

        '팝빌에 사전등록된 발신번호
        '※ 대체문자를 전송하는 경우에만 필수 입력
        Dim senderNum As String = ""

        '수신번호
        Dim receiverNum As String = ""

        '수신자명
        Dim receiverName As String = "수신자명"

        '친구톡 내용 (최대 1000자)
        Dim content As String = "친구톡 내용입니다."

        ' 대체문자 제목
        ' - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
        Dim altSubject = "대체문자 제목"

        ' 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
        ' └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
        Dim altContent As String = "카카오톡이 실패하여 문자로 전송됩니다." + vbCrLf
        altContent += "[팝빌]" + vbCrLf
        altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다." + vbCrLf
        altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다." + vbCrLf + vbCrLf
        altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf
        altContent += "팝빌 파트너센터 : 1600-8536" + vbCrLf

        ' 대체문자 유형 (null , "C" , "A" 중 택 1)
        ' null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
        Dim altSendType = "A"

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = True

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        '버튼 배열 최대 5개
        Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

        Dim btnInfo As KakaoButton = New KakaoButton
        btnInfo.n = "버튼명"                        '버튼명
        btnInfo.t = "WL"                            '버튼유형 (DS - 배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
        btnInfo.u1 = "http://www.linkhub.co.kr"     '버튼링크1 [앱링크] iOS / [웹링크] Mobile
        btnInfo.u2 = "http://www.popbill.co.kr"     '버튼링크2 [앱링크] Android / [웹링크] PC URL
        buttonList.Add(btnInfo)

        Try
            Dim receiptNum As String = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content, altSubject, altContent, altSendType, _
                                                            receiverNum, receiverName, adsYN, getReserveDT(), buttonList, txtUserId.Text, requestNum)
            MsgBox("접수번호(receiptNum접수번호(receiptNum접수번호(receiptNum)접수번호(receiptNum))) : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 텍스트로 구성된 다수건의 친구톡 전송을 팝빌에 접수하며, 수신자 별로 개별 내용을 전송합니다. (최대 1,000건)
    ' - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
    ' - 전송실패시 사전에 지정한 변수 'altSendType' 값으로 대체문자를 전송할 수 있고, 이 경우 문자(SMS/LMS) 요금이 과금됩니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFTSMulti
    '=========================================================================
    Private Sub btnSendFTS_multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFTS_multi.Click

        '검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
        Dim plusFriendID As String = "@팝빌"

        '팝빌에 사전등록된 발신번호
        '※ 대체문자를 전송하는 경우에만 필수 입력
        Dim senderNum As String = ""

        ' 대체문자 유형 (null , "C" , "A" 중 택 1)
        ' null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
        Dim altSendType = "A"

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = True

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            msg.msg = "친구톡 내용입니다." + CStr(i) '친구톡 내용 (최대 1000자)

            '대체문자 제목
            '- 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
            msg.altsjt = "대체문자 제목" + CStr(i)

            '대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
            '└ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
            Dim altContent = "[팝빌]" + vbCrLf
            altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다." + vbCrLf
            altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다." + vbCrLf + vbCrLf
            altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf
            altContent += "팝빌 파트너센터 : 1600-8536" + vbCrLf
            msg.altmsg = altContent

            '파트너 지정키, 대량전송시, 수신자 구별용 메모
            msg.interOPRefKey = "20220504-" + CStr(i)

            '수신자별 개별 버튼정보 전송하는 경우
            '생성 가능 개수 최대 5개
            'Dim btns As List(Of KakaoButton) = New List(Of KakaoButton) '수신자별 개별 버튼정보 리스트 생성

            'Dim btnInfo1 As KakaoButton = New KakaoButton     '개별 버튼정보 생성
            'btnInfo1.n = "템플릿 안내"                        '버튼명
            'btnInfo1.t = "WL"                                 '버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            'btnInfo1.u1 = "https://www.popbill.com"           '버튼링크1 [앱링크] Android / [웹링크] Mobile
            'btnInfo1.u2 = "http://test.popbill.com" + CStr(i) '버튼링크2 [앱링크] IOS / [웹링크] PC URL
            'btns.Add(btnInfo1)                                '개별 버튼정보 리스트에 개별 버튼정보 추가

            'Dim btnInfo2 As KakaoButton = New KakaoButton     '개별 버튼정보 생성
            'btnInfo2.n = "템플릿 안내"                        '버튼명
            'btnInfo2.t = "WL"                                 '버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            'btnInfo2.u1 = "https://www.test.com"              '버튼링크1 [앱링크] Android / [웹링크] Mobile
            'btnInfo2.u2 = "http://test.test.com" + CStr(i)    '버튼링크2 [앱링크] IOS / [웹링크] PC URL
            'btns.Add(btnInfo2)                                '개별 버튼정보 리스트에 개별 버튼정보 추가

            'msg.btns = btns '수신자 정보에 개별 버튼정보 리스트 추가

            receiverList.Add(msg)
        Next

        '동일 버튼정보, 수신자멸 동일 버튼정보 전송하는 경우
        '개별 버튼정보 전송하는 경우, null 처리
        Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)
        '생성 가능 개수 최대 5개
        Dim btnInfo As KakaoButton = New KakaoButton
        btnInfo.n = "버튼명"                        '버튼명
        btnInfo.t = "WL"                            '버튼유형 (DS - 배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
        btnInfo.u1 = "http://www.linkhub.co.kr"     '버튼링크1 [앱링크] iOS / [웹링크] Mobile
        btnInfo.u2 = "http://www.popbill.co.kr"     '버튼링크2 [앱링크] Android / [웹링크] PC URL
        buttonList.Add(btnInfo)

        Try
            Dim receiptNum As String = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, altSendType, adsYN, getReserveDT(), receiverList, buttonList, _
                                                            txtUserId.Text, requestNum)

            MsgBox("receiptNum(접수번호) : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 텍스트로 구성된 다수건의 친구톡 전송을 팝빌에 접수하며, 모든 수신자에게 동일 내용을 전송합니다. (최대 1,000건)
    ' - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
    ' - 전송실패시 사전에 지정한 변수 'altSendType' 값으로 대체문자를 전송할 수 있고, 이 경우 문자(SMS/LMS) 요금이 과금됩니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFTSSame
    '=========================================================================
    Private Sub btnSendFTS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFTS_same.Click

        '검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
        Dim plusFriendID As String = "@팝빌"

        '팝빌에 사전등록된 발신번호
        '※ 대체문자를 전송하는 경우에만 필수 입력
        Dim senderNum As String = ""

        '친구톡 내용 (최대 1000자)
        Dim content As String = "친구톡 내용입니다."

        '대체문자 제목
        '- 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
        Dim altSubject = "대체문자 제목"

        ' 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
        ' └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
        Dim altContent As String = "카카오톡이 실패하여 문자로 전송됩니다." + vbCrLf + vbCrLf
        altContent += "[팝빌]" + vbCrLf
        altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다." + vbCrLf
        altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다." + vbCrLf + vbCrLf
        altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf
        altContent += "팝빌 파트너센터 : 1600-8536" + vbCrLf

        ' 대체문자 유형 (null , "C" , "A" 중 택 1)
        ' null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
        Dim altSendType = "A"

        ' 광고성 메시지 여부 ( true , false 중 택 1)
        ' └ true = 광고 , false = 일반
        ' - 미입력 시 기본값 false 처리
        Dim adsYN As Boolean = True

        ' 전송요청번호
        ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
        ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
        Dim requestNum = ""

        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 2
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            receiverList.Add(msg)
        Next

        '버튼 배열 최대 5개
        Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

        Dim btnInfo As KakaoButton = New KakaoButton
        btnInfo.n = "버튼명"                        '버튼명
        btnInfo.t = "WL"                            '버튼유형 (DS - 배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
        btnInfo.u1 = "http://www.linkhub.co.kr"     '버튼링크1 [앱링크] iOS / [웹링크] Mobile
        btnInfo.u2 = "http://www.popbill.co.kr"     '버튼링크2 [앱링크] Android / [웹링크] PC URL
        buttonList.Add(btnInfo)

        Try
            Dim receiptNum As String = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content, altSubject, altContent, altSendType, _
                                                            adsYN, getReserveDT(), receiverList, buttonList, txtUserId.Text, requestNum)
            MsgBox("receiptNum(접수번호) : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 이미지가 첨부된 1건의 친구톡 전송을 팝빌에 접수합니다.
    ' - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
    ' - 이미지 파일 규격: 전송 포맷 – JPG 파일 (.jpg, .jpeg), 용량 – 최대 500 Kbyte, 크기 – 가로 500px 이상, 가로 기준으로 세로 0.5~1.3배 비율 가능
    ' - 전송실패시 사전에 지정한 변수 'altSendType' 값으로 대체문자를 전송할 수 있고, 이 경우 문자(SMS/LMS) 요금이 과금됩니다.
    ' - 대체문자의 경우, 포토문자(MMS) 형식은 지원하고 있지 않습니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFMSOne
    '=========================================================================
    Private Sub btnSendFMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFMS_one.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            '검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
            Dim plusFriendID As String = "@팝빌"

            '팝빌에 사전등록된 발신번호
            '※ 대체문자를 전송하는 경우에만 필수 입력
            Dim senderNum As String = ""

            '수신번호
            Dim receiverNum As String = ""

            '수신자명
            Dim receiverName As String = "수신자명"

            '친구톡 내용 (최대 400자)
            Dim content As String = "친구톡 내용입니다."

            ' 대체문자 제목
            ' - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
            Dim altSubject = "대체문자 제목"

            ' 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
            ' └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
            Dim altContent As String = "카카오톡이 실패하여 문자로 전송됩니다." + vbCrLf
            altContent += "[팝빌]" + vbCrLf
            altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다." + vbCrLf
            altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다." + vbCrLf + vbCrLf
            altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf
            altContent += "팝빌 파트너센터 : 1600-8536" + vbCrLf

            ' 대체문자 유형 (null , "C" , "A" 중 택 1)
            ' null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            Dim altSendType = "A"

            ' 광고성 메시지 여부 ( true , false 중 택 1)
            ' └ true = 광고 , false = 일반
            ' - 미입력 시 기본값 false 처리
            Dim adsYN As Boolean = True

            '첨부 이미지 파일경로
            Dim strFileName As String = fileDialog.FileName

            ' 이미지 링크 URL
            ' └ 수신자가 친구톡 상단 이미지 클릭시 호출되는 URL
            ' - 미입력시 첨부된 이미지를 링크 기능 없이 표시
            Dim imageURL As String = "https://www.popbill.com"

            ' 전송요청번호
            ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
            ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            Dim requestNum = ""

            '버튼 배열 최대 5개
            Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

            Dim btnInfo As KakaoButton = New KakaoButton
            btnInfo.n = "버튼명"                        '버튼명
            btnInfo.t = "WL"                            '버튼유형 (DS - 배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.u1 = "http://www.linkhub.co.kr"     '버튼링크1 [앱링크] iOS / [웹링크] Mobile
            btnInfo.u2 = "http://www.popbill.co.kr"     '버튼링크2 [앱링크] Android / [웹링크] PC URL
            buttonList.Add(btnInfo)

            Try
                Dim receiptNum As String = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, content, altSubject, altContent, altSendType, receiverNum, _
                                                                receiverName, adsYN, getReserveDT(), buttonList, strFileName, imageURL, _
                                                                txtUserId.Text, requestNum)
                MsgBox("receiptNum(접수번호) : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 이미지가 첨부된 다수건의 친구톡 전송을 팝빌에 접수하며, 수신자 별로 개별 내용을 전송합니다. (최대 1,000건)
    ' - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
    ' - 이미지 파일 규격: 전송 포맷 – JPG 파일 (.jpg, .jpeg), 용량 – 최대 500 Kbyte, 크기 – 가로 500px 이상, 가로 기준으로 세로 0.5~1.3배 비율 가능
    ' - 전송실패시 사전에 지정한 변수 'altSendType' 값으로 대체문자를 전송할 수 있고, 이 경우 문자(SMS/LMS) 요금이 과금됩니다.
    ' - 대체문자의 경우, 포토문자(MMS) 형식은 지원하고 있지 않습니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFMSMulti
    '=========================================================================
    Private Sub btnSendFMS_multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFMS_multi.Click

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            '검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
            Dim plusFriendID As String = "@팝빌"

            '팝빌에 사전등록된 발신번호
            '※ 대체문자를 전송하는 경우에만 필수 입력
            Dim senderNum As String = ""

            ' 대체문자 유형 (null , "C" , "A" 중 택 1)
            ' null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            Dim altSendType = "A"

            ' 광고성 메시지 여부 ( true , false 중 택 1)
            ' └ true = 광고 , false = 일반
            ' - 미입력 시 기본값 false 처리
            Dim adsYN As Boolean = True

            '첨부 이미지 파일경로
            Dim strFileName As String = fileDialog.FileName

            ' 이미지 링크 URL
            ' └ 수신자가 친구톡 상단 이미지 클릭시 호출되는 URL
            ' - 미입력시 첨부된 이미지를 링크 기능 없이 표시
            Dim imageURL As String = "https://www.popbill.com"

            ' 전송요청번호
            ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
            ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            Dim requestNum = ""

            '전송정보 배열, 최대 1000건
            Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

            For i As Integer = 0 To 5
                Dim msg As KakaoReceiver = New KakaoReceiver
                msg.rcv = "" '수신번호
                msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
                msg.msg = "친구톡 내용입니다." + CStr(i) '친구톡 내용 (최대 400자)

                '대체문자 제목
                '- 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
                msg.altsjt = "대체문자 제목" + CStr(i)

                '대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
                '└ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
                Dim altContent = "[팝빌]" + vbCrLf
                altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다." + vbCrLf
                altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다." + vbCrLf + vbCrLf
                altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf
                altContent += "팝빌 파트너센터 : 1600-8536" + vbCrLf
                msg.altmsg = altContent

                msg.interOPRefKey = "20220504-" + CStr(i) '파트너 지정키, 대량전송시, 수신자 구별용 메모

                '수신자별 개별 버튼정보 전송하는 경우
                '생성 가능 개수 최대 5개
                'Dim btns As List(Of KakaoButton) = New List(Of KakaoButton) '수신자별 개별 버튼정보 리스트 생성

                'Dim btnInfo1 As KakaoButton = New KakaoButton     '개별 버튼정보 생성
                'btnInfo1.n = "템플릿 안내"                        '버튼명
                'btnInfo1.t = "WL"                                 '버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
                'btnInfo1.u1 = "https://www.popbill.com"           '버튼링크1 [앱링크] Android / [웹링크] Mobile
                'btnInfo1.u2 = "http://test.popbill.com" + CStr(i) '버튼링크2 [앱링크] IOS / [웹링크] PC URL
                'btns.Add(btnInfo1)                                '개별 버튼정보 리스트에 개별 버튼정보 추가

                'Dim btnInfo2 As KakaoButton = New KakaoButton     '개별 버튼정보 생성
                'btnInfo2.n = "템플릿 안내"                        '버튼명
                'btnInfo2.t = "WL"                                 '버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
                'btnInfo2.u1 = "https://www.test.com"              '버튼링크1 [앱링크] Android / [웹링크] Mobile
                'btnInfo2.u2 = "http://test.test.com" + CStr(i)    '버튼링크2 [앱링크] IOS / [웹링크] PC URL
                'btns.Add(btnInfo2)                                '개별 버튼정보 리스트에 개별 버튼정보 추가

                'msg.btns = btns '수신자 정보에 개별 버튼정보 리스트 추가

                receiverList.Add(msg)
            Next


            '동일 버튼정보, 수신자멸 동일 버튼정보 전송하는 경우
            '개별 버튼정보 전송하는 경우, null 처리
            Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)
            '생성 가능 개수 최대 5개
            Dim btnInfo As KakaoButton = New KakaoButton
            btnInfo.n = "버튼명"                        '버튼명
            btnInfo.t = "WL"                            '버튼유형 (DS - 배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.u1 = "http://www.linkhub.co.kr"     '버튼링크1 [앱링크] iOS / [웹링크] Mobile
            btnInfo.u2 = "http://www.popbill.co.kr"     '버튼링크2 [앱링크] Android / [웹링크] PC URL
            buttonList.Add(btnInfo)

            Try
                Dim receiptNum As String = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, altSendType, adsYN, getReserveDT(), receiverList, _
                                                                buttonList, strFileName, imageURL, txtUserId.Text, requestNum)
                MsgBox("receiptNum(접수번호) : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 이미지가 첨부된 다수건의 친구톡 전송을 팝빌에 접수하며, 모든 수신자에게 동일 내용을 전송합니다. (최대 1,000건)
    ' - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
    ' - 이미지 파일 규격: 전송 포맷 – JPG 파일 (.jpg, .jpeg), 용량 – 최대 500 Kbyte, 크기 – 가로 500px 이상, 가로 기준으로 세로 0.5~1.3배 비율 가능
    ' - 전송실패시 사전에 지정한 변수 'altSendType' 값으로 대체문자를 전송할 수 있고, 이 경우 문자(SMS/LMS) 요금이 과금됩니다.
    ' - 대체문자의 경우, 포토문자(MMS) 형식은 지원하고 있지 않습니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFMSSame
    '=========================================================================
    Private Sub btnSendFMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFMS_same.Click

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            '검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
            Dim plusFriendID As String = "@팝빌"

            '팝빌에 사전등록된 발신번호
            '※ 대체문자를 전송하는 경우에만 필수 입력
            Dim senderNum As String = ""

            '친구톡 내용 (최대 400자)
            Dim content As String = "친구톡 내용입니다."

            '대체문자 제목
            '- 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
            Dim altSubject = "대체문자 제목"

            ' 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
            ' └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
            Dim altContent As String = "카카오톡이 실패하여 문자로 전송됩니다." + vbCrLf + vbCrLf
            altContent += "[팝빌]" + vbCrLf
            altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다." + vbCrLf
            altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다." + vbCrLf + vbCrLf
            altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf
            altContent += "팝빌 파트너센터 : 1600-8536" + vbCrLf

            ' 대체문자 유형 (null , "C" , "A" 중 택 1)
            ' null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            Dim altSendType = "A"

            ' 광고성 메시지 여부 ( true , false 중 택 1)
            ' └ true = 광고 , false = 일반
            ' - 미입력 시 기본값 false 처리
            Dim adsYN As Boolean = True

            '첨부 이미지 파일경로
            Dim strFileName As String = fileDialog.FileName

            ' 이미지 링크 URL
            ' └ 수신자가 친구톡 상단 이미지 클릭시 호출되는 URL
            ' - 미입력시 첨부된 이미지를 링크 기능 없이 표시
            Dim imageURL As String = "https://www.popbill.com"

            ' 전송요청번호
            ' 팝빌이 접수 단위를 식별할 수 있도록 파트너가 부여하는 식별번호.
            ' 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            Dim requestNum = ""

            '전송정보 배열, 최대 1000건
            Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

            For i As Integer = 0 To 2
                Dim msg As KakaoReceiver = New KakaoReceiver
                msg.rcv = "" '수신번호
                msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
                receiverList.Add(msg)
            Next

            '버튼 배열 최대 5개
            Dim buttonList As List(Of KakaoButton) = New List(Of KakaoButton)

            Dim btnInfo As KakaoButton = New KakaoButton
            btnInfo.n = "버튼명"                        '버튼명
            btnInfo.t = "WL"                            '버튼유형 (DS - 배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.u1 = "http://www.linkhub.co.kr"     '버튼링크1 [앱링크] iOS / [웹링크] Mobile
            btnInfo.u2 = "http://www.popbill.co.kr"     '버튼링크2 [앱링크] Android / [웹링크] PC URL
            buttonList.Add(btnInfo)

            Try
                Dim receiptNum As String = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, content, altSubject, altContent, altSendType, _
                                                                adsYN, getReserveDT(), receiverList, buttonList, strFileName, imageURL, txtUserId.Text, requestNum)
                MsgBox("receiptNum(접수번호) : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 예약접수된 카카오톡을 전송 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#CancelReserve
    '=========================================================================
    Private Sub btnCancelReserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelReserve.Click
        Try
            Dim response As Response

            response = kakaoService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 예약접수된 카카오톡을 전송 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#CancelReserveRN
    '=========================================================================
    Private Sub btnCancelReserveRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelReserveRN.Click
        Try
            Dim response As Response

            response = kakaoService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송 요청번호로 접수 건을 식별하여 수신번호에 예약된 카카오톡을 전송 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#CancelReserveRNbyRCV
    '=========================================================================
    Private Sub btnCancelReserveRNbyRCV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelReserveRNbyRCV.Click

        Dim RequestNum As String = "test_request_Num"

        Dim ReceiveNum As String = "01012341234"

        Try
            Dim response As Response

            response = kakaoService.CancelReserveRNbyRCV(txtCorpNum.Text, RequestNum, ReceiveNum, txtRequestNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 예약접수된 카카오톡을 전송 취소합니다. (예약시간 10분 전까지 가능)
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#CancelReserveRN
    '=========================================================================
    Private Sub btnCancelReservebyRCV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelReservebyRCV.Click

        Dim ReceiptNum As String = "023053016515500001"

        Dim ReceiveNum As String = "01012341234"
        Try
            Dim response As Response

            response = kakaoService.CancelReservebyRCV(txtCorpNum.Text, ReceiptNum, ReceiveNum, txtRequestNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에서 반환받은 접수번호를 통해 알림톡/친구톡 전송상태 및 결과를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/info#GetMessages
    '=========================================================================
    Private Sub btnGetMessages_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetMessages.Click
        ListBox1.Items.Clear()
        Try
            Dim sentInfo As KakaoSentResult = kakaoService.GetMessages(txtCorpNum.Text, txtReceiptNum.Text)

            Dim tmp As String = "contentType(카카오톡 유형) : " + sentInfo.contentType + vbCrLf
            tmp += "templateCode(템플릿 코드) : " + sentInfo.templateCode + vbCrLf
            tmp += "plusFriendID(카카오톡채널 아이디) : " + sentInfo.plusFriendID + vbCrLf
            tmp += "sendNum(발신번호) : " + sentInfo.sendNum + vbCrLf
            tmp += "altSubject(대체문자 제목) : " + sentInfo.altSubject + vbCrLf
            tmp += "altContent(대체문자 내용) : " + sentInfo.altContent + vbCrLf
            tmp += "altSendType(대체문자 유형) : " + sentInfo.altSendType + vbCrLf
            tmp += "reserveDT(예약일시) : " + sentInfo.reserveDT + vbCrLf
            tmp += "adsYN(광고전송 여부) : " + CStr(sentInfo.adsYN) + vbCrLf
            tmp += "imageURL(친구톡 이미지 URL) : " + sentInfo.imageURL + vbCrLf
            tmp += "sendCnt(전송건수) : " + sentInfo.sendCnt + vbCrLf
            tmp += "successCnt(성공건수) : " + sentInfo.successCnt + vbCrLf
            tmp += "failCnt(실패건수) : " + sentInfo.failCnt + vbCrLf
            tmp += "altCnt(대체문자 건수) : " + sentInfo.altCnt + vbCrLf
            tmp += "cancelCnt(취소건수) : " + sentInfo.cancelCnt + vbCrLf

            If Not sentInfo.btns Is Nothing Then
                For Each btnInfo As KakaoButton In sentInfo.btns
                    tmp += "[버튼정보]" + vbCrLf
                    tmp += "n(버튼명) : " + btnInfo.n + vbCrLf
                    tmp += "t(버튼유형) : " + btnInfo.t + vbCrLf
                    tmp += "u1(버튼링크1) : " + btnInfo.u1 + vbCrLf
                    tmp += "u2(버튼링크2) : " + btnInfo.u2 + vbCrLf
                Next
                tmp += vbCrLf
            End If

            MsgBox(tmp)

            '전송결과 정보 리스트
            Dim rowStr As String = "state(전송상태 코드) | sendDT(전송일시) | result(전송결과 코드) | resultDT(전송결과 수신일시) | receiveNum(수신번호) | receiveName(수신자명) | content(내용) | "
            rowStr += "altSubject(대체문자 제목) | altContnet(대체문자 내용) | altContentType(대체문자 전송유형) | altSendDT(대체문자 전송일시) | altResult(대체문자 전송결과 코드) | "
            rowStr += "altResultDT(대체문자 전송결과 수신일시) | receiptNum(접수번호) | requestNum(요청번호) | interOPRefKey (파트너 지정키)"

            ListBox1.Items.Add(rowStr)

            For Each Result As KakaoSentDetail In sentInfo.msgs
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.sendDT + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.resultDT + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.content + " | "
                rowStr += Result.altSubject + " | "
                rowStr += Result.altContent + " | "
                rowStr += Result.altContentType.ToString + " | "
                rowStr += Result.altSendDT + " | "
                rowStr += Result.altResult + " | "
                rowStr += Result.altResultDT + " | "
                rowStr += Result.receiptNum + " | "
                rowStr += Result.requestNum + " | "
                rowStr += Result.interOPRefKey

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너가 할당한 전송요청 번호를 통해 알림톡/친구톡 전송상태 및 결과를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/info#GetMessagesRN
    '=========================================================================
    Private Sub btnGetMessagesRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetMessagesRN.Click
        ListBox1.Items.Clear()
        Try
            Dim sentInfo As KakaoSentResult = kakaoService.GetMessages(txtCorpNum.Text, txtReceiptNum.Text)

            Dim tmp As String = "contentType(카카오톡 유형) : " + sentInfo.contentType + vbCrLf
            tmp += "templateCode(템플릿 코드) : " + sentInfo.templateCode + vbCrLf
            tmp += "plusFriendID(카카오톡채널 아이디) : " + sentInfo.plusFriendID + vbCrLf
            tmp += "sendNum(발신번호) : " + sentInfo.sendNum + vbCrLf
            tmp += "altSubject(대체문자 제목) : " + sentInfo.altSubject + vbCrLf
            tmp += "altContent(대체문자 내용) : " + sentInfo.altContent + vbCrLf
            tmp += "altSendType(대체문자 유형) : " + sentInfo.altSendType + vbCrLf
            tmp += "reserveDT(예약일시) : " + sentInfo.reserveDT + vbCrLf
            tmp += "adsYN(광고전송 여부) : " + CStr(sentInfo.adsYN) + vbCrLf
            tmp += "imageURL(친구톡 이미지 URL) : " + sentInfo.imageURL + vbCrLf
            tmp += "sendCnt(전송건수) : " + sentInfo.sendCnt + vbCrLf
            tmp += "successCnt(성공건수) : " + sentInfo.successCnt + vbCrLf
            tmp += "failCnt(실패건수) : " + sentInfo.failCnt + vbCrLf
            tmp += "altCnt(대체문자 건수) : " + sentInfo.altCnt + vbCrLf
            tmp += "cancelCnt(취소건수) : " + sentInfo.cancelCnt + vbCrLf

            If Not sentInfo.btns Is Nothing Then
                For Each btnInfo As KakaoButton In sentInfo.btns
                    tmp += "[버튼정보]" + vbCrLf
                    tmp += "n(버튼명) : " + btnInfo.n + vbCrLf
                    tmp += "t(버튼유형) : " + btnInfo.t + vbCrLf
                    tmp += "u1(버튼링크1) : " + btnInfo.u1 + vbCrLf
                    tmp += "u2(버튼링크2) : " + btnInfo.u2 + vbCrLf
                Next
                tmp += vbCrLf
            End If

            MsgBox(tmp)

            '전송결과 정보 리스트
            Dim rowStr As String = "state(전송상태 코드) | sendDT(전송일시) | result(전송결과 코드) | resultDT(전송결과 수신일시) | receiveNum(수신번호) | receiveName(수신자명) | content(내용) | "
            rowStr += "altSubject(대체문자 제목) | altContnet(대체문자 내용) | altContentType(대체문자 전송유형) | altSendDT(대체문자 전송일시) | altResult(대체문자 전송결과 코드) | "
            rowStr += "altResultDT(대체문자 전송결과 수신일시) | receiptNum(접수번호) | requestNum(요청번호) | interOPRefKey (파트너 지정키)"

            ListBox1.Items.Add(rowStr)

            For Each Result As KakaoSentDetail In sentInfo.msgs
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.sendDT + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.resultDT + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.content + " | "
                rowStr += Result.altSubject + " | "
                rowStr += Result.altContent + " | "
                rowStr += Result.altContentType.ToString + " | "
                rowStr += Result.altSendDT + " | "
                rowStr += Result.altResult + " | "
                rowStr += Result.altResultDT + " | "
                rowStr += Result.receiptNum + " | "
                rowStr += Result.requestNum + " | "
                rowStr += Result.interOPRefKey

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건에 해당하는 카카오톡 전송내역을 조회합니다. (조회기간 단위 : 최대 2개월)
    ' - 카카오톡 접수일시로부터 6개월 이내 접수건만 조회할 수 있습니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/info#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(6) As String
        Dim item(3) As String

        '최대 검색기간 : 6개월 이내
        '시작일자, yyyyMMdd
        Dim SDate As String = "20220501"

        '종료일자, yyyyMMdd
        Dim EDate As String = "20220531"

        ' 전송상태 배열 ("0" , "1" , "2" , "3" , "4" , "5" 중 선택, 다중 선택 가능)
        ' └ 0 = 전송대기 , 1 = 전송중 , 2 = 전송성공 , 3 = 대체문자 전송 , 4 = 전송실패 , 5 = 전송취소
        ' - 미입력 시 전체조회
        State(0) = "0"
        State(1) = "1"
        State(2) = "2"
        State(3) = "3"
        State(4) = "4"
        State(5) = "5"

        ' 검색대상 배열 ("ATS", "FTS", "FMS" 중 선택, 다중 선택 가능)
        ' └ ATS = 알림톡 , FTS = 친구톡(텍스트) , FMS = 친구톡(이미지)
        ' - 미입력 시 전체조회
        item(0) = "ATS"
        item(1) = "FTS"
        item(2) = "FMS"

        ' 전송유형별 조회 (null , "0" , "1" 중 택 1)
        ' └ null = 전체 , 0 = 즉시전송건 , 1 = 예약전송건
        ' - 미입력 시 전체조회
        Dim ReserveYN As String = ""

        ' 사용자권한별 조회 (true / false 중 택 1)
        ' └ false = 접수한 카카오톡 전체 조회 (관리자권한)
        ' └ true = 해당 담당자 계정으로 접수한 카카오톡만 조회 (개인권한)
        ' - 미입력시 기본값 false 처리
        Dim SenderYN As Boolean = False

        '페이지 번호
        Dim Page As Integer = 1

        '페이지 목록개수, 최대 1000건
        Dim PerPage As Integer = 1000

        '정렬방향, D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

        ' 조회하고자 하는 수신자명
        ' - 미입력시 전체조회
        Dim QString As String = ""

        ListBox1.Items.Clear()
        Try
            Dim msgSearchList As KakaoSearchResult = kakaoService.Search(txtCorpNum.Text, SDate, EDate, State, item, ReserveYN, SenderYN, Order, Page, PerPage, _
                                                                         txtUserId.Text, QString)

            Dim tmp As String = ""

            tmp += "code (응답코드) : " + CStr(msgSearchList.code) + vbCrLf
            tmp += "message (응답메시지) : " + msgSearchList.message + vbCrLf + vbCrLf
            tmp += "total (총 검색결과 건수) : " + CStr(msgSearchList.total) + vbCrLf
            tmp += "perPage (페이지당 검색개수) : " + CStr(msgSearchList.perPage) + vbCrLf
            tmp += "pageNum (페이지 번호) : " + CStr(msgSearchList.pageNum) + vbCrLf
            tmp += "pageCount (페이지 개수) : " + CStr(msgSearchList.pageCount) + vbCrLf

            MsgBox(tmp)

            Dim rowStr As String = "state(전송상태 코드) | sendDT(전송일시) | result(전송결과 코드) | resultDT(전송결과 수신일시) | receiveNum(수신번호) | receiveName(수신자명) | content(내용) | "
            rowStr += "altSubject(대체문자 제목) | altContnet(대체문자 내용) | altContentType(대체문자 전송유형) | altSendDT(대체문자 전송일시) | altResult(대체문자 전송결과 코드) | "
            rowStr += "altResultDT(대체문자 전송결과 수신일시) | receiptNum(접수번호) | requestNum(요청번호) | interOPRefKey (파트너 지정키)"

            ListBox1.Items.Add(rowStr)

            For Each Result As KakaoSentDetail In msgSearchList.list
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.sendDT + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.resultDT + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.content + " | "
                rowStr += Result.altSubject + " | "
                rowStr += Result.altContent + " | "
                rowStr += Result.altContentType.ToString + " | "
                rowStr += Result.altSendDT + " | "
                rowStr += Result.altResult + " | "
                rowStr += Result.altResultDT + " | "
                rowStr += Result.receiptNum + " | "
                rowStr += Result.requestNum + " | "
                rowStr += Result.interOPRefKey

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트와 동일한 카카오톡 전송내역을 확인하는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/info#GetSentListURL
    '=========================================================================
    Private Sub btnGetSentListURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetSentListURL.Click
        Try
            Dim url As String = kakaoService.GetSentListURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 잔여포인트를 확인합니다.
    ' - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetBalance.Click

        Try
            Dim remainPoint As Double = kakaoService.GetBalance(txtCorpNum.Text)

            MsgBox("remainPoint(연동회원 잔여포인트) : " + remainPoint.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetChargeURL.Click
        Try
            Dim url As String = kakaoService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetPaymentURL
    '=========================================================================
    Private Sub btnGetPaymentURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPaymentURL.Click
        Try
            Dim url As String = kakaoService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetUseHistoryURL
    '=========================================================================
    Private Sub btnGetUseHistoryURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetUseHistoryURL.Click
        Try
            Dim url As String = kakaoService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = kakaoService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("remainPoint(파트너 잔여포인트) : " + remainPoint.ToString)
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetPartnerURL
    '=========================================================================
    Private Sub btnGetPartnerURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPartnerURL.Click

        Try
            '파트너 포인트충전 URL
            Dim TOGO As String = "CHRG"

            Dim url As String = kakaoService.GetPartnerURL(txtCorpNum.Text, TOGO)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 카카오톡(ATS) 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_ATS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUnitCost_ATS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.ATS

        Try
            Dim unitCost As Single = kakaoService.GetUnitCost(txtCorpNum.Text, kType)

            MsgBox("unitCost(알림톡 전송단가) : " + unitCost.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 카카오톡(FTS) 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetUnitCost
    '=========================================================================
    Private Sub btnGetUnitCost_FTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetUnitCost_FTS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.FTS

        Try
            Dim unitCost As Single = kakaoService.GetUnitCost(txtCorpNum.Text, kType)

            MsgBox("unitCost(친구톡 텍스트 전송단가) : " + unitCost.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 카카오톡(FMS) 전송시 과금되는 포인트 단가를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetUnitCost
    '=========================================================================
    Private Sub btnGetUnitCost_FMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetUnitCost_FMS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.FMS

        Try
            Dim unitCost As Single = kakaoService.GetUnitCost(txtCorpNum.Text, kType)

            MsgBox("unitCost(친구톡 이미지 전송단가) : " + unitCost.ToString)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 카카오톡(ATS) API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_ATS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetChargeInfo_ATS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.ATS

        Try
            Dim ChargeInfo As ChargeInfo = kakaoService.GetChargeInfo(txtCorpNum.Text, kType)

            Dim tmp As String = "unitCost (전송단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 카카오톡(FTS) API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_FTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetChargeInfo_FTS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.FTS

        Try
            Dim ChargeInfo As ChargeInfo = kakaoService.GetChargeInfo(txtCorpNum.Text, kType)

            Dim tmp As String = "unitCost (전송단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 카카오톡(FMS) API 서비스 과금정보를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetChargeInfo
    '=========================================================================
    Private Sub btnGetChargeInfo_FMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetChargeInfo_FMS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.FMS

        Try
            Dim ChargeInfo As ChargeInfo = kakaoService.GetChargeInfo(txtCorpNum.Text, kType)

            Dim tmp As String = "unitCost (전송단가) : " + ChargeInfo.unitCost + vbCrLf
            tmp += "chargeMethod (과금유형) : " + ChargeInfo.chargeMethod + vbCrLf
            tmp += "rateSystem (과금제도) : " + ChargeInfo.rateSystem + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckIsMember.Click
        Try
            Dim response As Response = kakaoService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용하고자 하는 아이디의 중복여부를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = kakaoService.CheckID(txtCorpNum.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 사용자를 연동회원으로 가입처리합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#JoinMember
    '=========================================================================
    Private Sub btnJoinMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnJoinMember.Click
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
            Dim response As Response = kakaoService.JoinMember(joinInfo)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
    ' - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetAccessURL.Click
        Try
            Dim url As String = kakaoService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#GetCorpInfo
    '=========================================================================
    Private Sub btnGetCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetCorpInfo.Click
        Try
            Dim corpInfo As CorpInfo = kakaoService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#UpdateCorpInfo
    '=========================================================================
    Private Sub btnUpdateCorpInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUpdateCorpInfo.Click

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

            Dim response As Response = kakaoService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#RegistContact
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRegistContact.Click

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
            Dim response As Response = kakaoService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#GetContactInfo
    '=========================================================================
    Private Sub btnGetContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactInfo.Click

        '확인할 담당자 아이디
        Dim contactID As String = "DONETVB_CONTACT"

        Dim tmp As String = ""

        Try
            Dim contactInfo As Contact = kakaoService.GetContactInfo(txtCorpNum.Text, contactID)

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
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = kakaoService.ListContact(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#UpdateContact
    '=========================================================================
    Private Sub btnUpdateContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUpdateContact.Click

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
            Dim response As Response = kakaoService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#PaymentRequest
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
            Dim response As PaymentResponse = kakaoService.PaymentRequest(txtCorpNum.Text, paymentForm, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.message+ vbCrLf + "settleCode(정산코드) : " + response.settleCode)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetSettleResult
    '=========================================================================
    Private Sub btnGetSettleResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetSettleResult.Click

        '정산코드
        Dim SettleCode As String = "202301160000000010"

        Try
            Dim response As PaymentHistory = kakaoService.GetSettleResult (txtCorpNum.Text, SettleCode, txtUserId.Text)

            Dim tmp As String = ""

            tmp+ ="productType(결제 내용) : " + response.productType + vbCrLf
            tmp+ ="productName(결제 상품명) : " + response.productName + vbCrLf
            tmp+ ="settleType(결제 유형) : " + response.settleType + vbCrLf
            tmp+ ="settlerName(담당자명) : " + response.settlerName + vbCrLf
            tmp+ ="settlerEmail(담당자메일) : " + response.settlerEmail + vbCrLf
            tmp+ ="settleCost(결제 금액) : " + response.settleCost + vbCrLf
            tmp+ ="settlePoint(충전포인트) : " + response.settlePoint + vbCrLf
            tmp+ ="settleState(결제 상태) : " + response.settleState.ToString + vbCrLf
            tmp+ ="regDT(등록일시) : " + response.regDT + vbCrLf
            tmp+ ="stateDT(상태일시) : " + response.stateDT

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 결제내역을 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetPaymentHistory
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
            Dim result As PaymentHistoryResult = kakaoService.GetPaymentHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, txtUserId.Text)

            Dim tmp As String = ""
            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "결제내역"+ vbCrLf

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
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetUseHistory
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
            Dim result As UseHistoryResult = kakaoService.GetUseHistory(txtCorpNum.Text,SDate,EDate,Page,PerPage, Order, txtUserId.Text)

            Dim tmp As String = ""
            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "사용 내역" + vbCrLf

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
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#Refund
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
            Dim response As RefundResponse = kakaoService.Refund(txtCorpNum.Text,refundForm, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.Message + vbCrLf + "refundCode(환불코드) : " +response.refundCode )

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 포인트 환불신청내역을 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetRefundHistory
    '=========================================================================
    Private Sub btnGetRefundHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundHistory.Click

        '목폭 페이지 번호
        Dim Page As Integer = 1

        '페이지당 목록 개수
        Dim PerPage As Integer = 500


        Try
            Dim result As RefundHistoryResult  = kakaoService.GetRefundHistory(txtCorpNum.Text,Page, PerPage, txtUserId.Text)

            Dim tmp As String = ""

            tmp += "code(응답코드) : " + result.code.ToString + vbCrLf
            tmp += "total(총 검색결과 건수) : " + result.total.ToString + vbCrLf
            tmp += "perPage(페이지당 검색개수) : " + result.perPage.ToString + vbCrLf
            tmp += "pageNum(페이지 번호) : " + result.pageNum.ToString + vbCrLf
            tmp += "pageCount(페이지 개수) : " + result.pageCount.ToString + vbCrLf
            tmp += "환불내역"+ vbCrLf

            For Each history As RefundHistory In result.list
                tmp += "reqDT (신청일시) :" + history.reqDT + vbCrLf
                tmp += "requestPoint (환불 신청포인트) :" + history.requestPoint + vbCrLf
                tmp += "accountBank (환불계좌 은행명) :" + history.accountBank + vbCrLf
                tmp += "accountNum (환불계좌번호) :" + history.accountNum + vbCrLf
                tmp += "accountName (환불계좌 예금주명) :" + history.accountName + vbCrLf
                tmp += "state (상태) : " + history.state.ToString + vbCrLf
                tmp += "reason (환불사유) : " + history.reason + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 포인트 환불에 대한 상세정보 1건을 확인합니다.
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetRefundInfo
    '=========================================================================
    Private Sub btnGetRefundInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundInfo.Click

        '환불코드
        Dim refundCode As String = "023040000017"

        Try
            Dim history As RefundHistory  = kakaoService.GetRefundInfo(txtCorpNum.Text,refundCode, txtUserId.Text)

            Dim tmp As String = ""

            tmp += "reqDT (신청일시) :" + history.reqDT + vbCrLf
            tmp += "requestPoint (환불 신청포인트) :" + history.requestPoint + vbCrLf
            tmp += "accountBank (환불계좌 은행명) :" + history.accountBank + vbCrLf
            tmp += "accountNum (환불계좌번호) :" + history.accountNum + vbCrLf
            tmp += "accountName (환불계좌 예금주명) :" + history.accountName + vbCrLf
            tmp += "state (상태) : " + history.state.ToString + vbCrLf
            tmp += "reason (환불사유) : " + history.reason + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 환불 가능한 포인트를 확인합니다. (보너스 포인트는 환불가능포인트에서 제외됩니다.)
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/point#GetRefundableBalance
    '=========================================================================
    Private Sub btnGetRefundableBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRefundableBalance.Click

        Try
            Dim refundableBalance As Double  = kakaoService.GetRefundableBalance(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://developers.popbill.com/reference/kakaotalk/dotnet/api/member#QuitMember
    '=========================================================================
    Private Sub btnQuitMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitMember.Click

        '탈퇴사유
        Dim quitReason As String = "회원 탈퇴 사유"

        Try
            Dim response As Response  = kakaoService.QuitMember(txtCorpNum.Text, quitReason, txtUserId.Text)

            MsgBox("code(응답코드) : " + response.code.ToString + vbCrLf + "message(응답메시지) : " + response.Message)

        Catch ex As PopbillException
            MsgBox("code(응답코드) : " + ex.code.ToString + vbCrLf + "message(응답메시지) : " + ex.Message)

        End Try
    End Sub

    Private Sub GroupBox12_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox12.Enter

    End Sub
End Class
