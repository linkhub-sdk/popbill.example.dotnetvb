﻿'=========================================================================
'
' 팝빌 카카오톡 API VB.Net SDK Example
'
' - VB.Net 연동환경 설정방법 안내 : https://docs.popbill.com/kakao/tutorial/dotnet#vb
' - 업데이트 일자 : 2020-10-23
' - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
' - 연동 기술지원 이메일 : code@linkhub.co.kr
'
' <테스트 연동개발 준비사항>
' 1) 22, 25번 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
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

        '연동환경 설정값 (True-개발용, False-상업용)
        kakaoService.IsTest = True

        '인증토큰의 IP제한기능 사용여부, (True-권장)
        kakaoService.IPRestrictOnOff = True

        '로컬PC 시간 사용 여부 True(사용), False(기본값) - 미사용
        kakaoService.UseLocalTimeYN = False
    End Sub

    Private Function getReserveDT() As DateTime?
        If String.IsNullOrEmpty(txtReserveDT.Text) = False Then

            Return _
                DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
        End If
    End Function

    '=========================================================================
    ' 카카오톡채널 계정관리 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetPlusFriendMgtURL
    '=========================================================================
    Private Sub btnGetPlusFriendMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPlusFriendMgtURL.Click
        Try
            Dim url As String = kakaoService.GetPlusFriendMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub


    '=========================================================================
    ' 팝빌에 등록된 카카오톡채널 계정 목록을 반환한다.
    ' - https://docs.popbill.com/kakao/dotnet/api#ListPlusFriendID
    '=========================================================================
    Private Sub btnListPlusFriendID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnListPlusFriendID.Click
        Try
            Dim plusFriendList As List(Of PlusFriend) = kakaoService.ListPlusFriendID(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = "plusFriendID(카카오톡채널 아이디) | plusFriendName(카카오톡채널 이름) | regDT(등록일시)" + vbCrLf

            For Each info As PlusFriend In plusFriendList
                tmp += info.plusFriendID + " | " + info.plusFriendName + " | " + info.regDT + vbCrLf
            Next

            MsgBox(tmp)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 발신번호 관리 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetSenderNumberMgtURL
    '=========================================================================
    Private Sub btnGetSenderNumberMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetSenderNumberMgtURL.Click
        Try
            Dim url As String = kakaoService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌에 등록된 발신번호 목록을 반환한다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetSenderNumberList
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 알림톡 템플릿 관리 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetATSTemplateMgtURL
    '=========================================================================
    Private Sub btnGetATSTemplateMgtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetATSTemplateMgtURL.Click
        Try
            Dim url As String = kakaoService.GetATSTemplateMgtURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' (주)카카오로부터 심사후 승인된 알림톡 템플릿 목록을 반환한다.
    ' - https://docs.popbill.com/kakao/dotnet/api#ListATSTemplate
    '=========================================================================
    Private Sub btnListATSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnListATSTemplate.Click
        Try
            Dim templateList As List(Of ATSTemplate) = kakaoService.ListATSTemplate(txtCorpNum.Text, txtUserId.Text)

            Dim tmp As String = ""

            For Each info As ATSTemplate In templateList
                tmp += "[템플릿 정보]" + vbCrLf
                tmp += "templateCode(템플릿 코드) : " + info.templateCode + vbCrLf
                tmp += "templateName(템플릿 제목) : " + info.templateName + vbCrLf
                tmp += "template(템플릿 내용) : " + info.template + vbCrLf
                tmp += "plusFriendID(카카오톡채널 아이디) : " + info.plusFriendID + vbCrLf

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
            Next
            MsgBox(tmp)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 알림톡 전송을 요청합니다.
    ' 사전에 승인된 템플릿의 내용과 알림톡 전송내용(content)이 다를 경우 전송실패 처리됩니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#SendATS
    '=========================================================================
    Private Sub btnSendATS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendATS_one.Click

        '알림톡 템플릿 코드, 알림톡 템플릿 목록확인(ListATSTemplate) API 반환항목중 templateCode로 확인
        Dim templateCode As String = "019020000163"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042991"

        '알림톡 템플릿 내용 (최대 1000자)
        Dim content As String = "[ 팝빌 ]" + vbCrLf
        content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다." + vbCrLf
        content += "해당 템플릿으로 전송 가능합니다." + vbCrLf + vbCrLf
        content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf + vbCrLf
        content += "팝빌 파트너센터 : 1600-8536" + vbCrLf
        content += "support@linkhub.co.kr"

        '대체문자 메시지 내용 (최대 2000byte)
        Dim altContent As String = "대체문자 메시지 내용"

        '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '수신번호
        Dim receiveNum = "010111222"

        '수신자명
        Dim receiveName = "수신자명"

        '전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        '최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
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
            Dim receiptNum As String = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, _
                                                            altSendType, getReserveDT(), receiveNum, receiveName, content, altContent, requestNum, buttonList)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [대량전송] 알림톡 전송을 요청합니다.
    ' 사전에 승인된 템플릿의 내용과 알림톡 전송내용(content)이 다를 경우 전송실패 처리됩니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#SendATS_Multi
    '=========================================================================
    Private Sub btnSendATS_multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendATS_multi.Click

        '알림톡 템플릿 코드, 알림톡 템플릿 목록확인(ListATSTemplate) API 반환항목중 templateCode로 확인
        Dim templateCode As String = "019020000163"

        '알림톡 템플릿 내용 (최대 1000자)
        Dim content As String = "[ 팝빌 ]" + vbCrLf
        content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다." + vbCrLf
        content += "해당 템플릿으로 전송 가능합니다." + vbCrLf + vbCrLf
        content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf + vbCrLf
        content += "팝빌 파트너센터 : 1600-8536" + vbCrLf
        content += "support@linkhub.co.kr"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042991"

        '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        '최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "010111222" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            msg.msg = content '알림톡 템플릿 내용 (최대 1000자)
            msg.altmsg = "대체문자 메시지 내용" '대체문자 내용 (최대 2000byte)
            msg.interOPRefKey = "20201023-" + CStr(i) '파트너 지정키, 대량전송시, 수신자 구별용 메모

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
            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [동보전송] 알림톡 전송을 요청합니다.
    ' 사전에 승인된 템플릿의 내용과 알림톡 전송내용(content)이 다를 경우 전송실패 처리됩니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#SendATS_Same
    '=========================================================================
    Private Sub btnSendATS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendATS_same.Click

        '알림톡 템플릿 코드, 알림톡 템플릿 목록확인(ListATSTemplate) API 반환항목중 templateCode로 확인
        Dim templateCode As String = "019020000163"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "01043245117"

        '알림톡 템플릿 내용 (최대 1000자)
        Dim content As String = "[ 팝빌 ]" + vbCrLf
        content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다." + vbCrLf
        content += "해당 템플릿으로 전송 가능합니다." + vbCrLf + vbCrLf
        content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다." + vbCrLf + vbCrLf
        content += "팝빌 파트너센터 : 1600-8536" + vbCrLf
        content += "support@linkhub.co.kr"

        '대체문자 메시지 내용 (최대 2000byte)
        Dim altContent As String = "대체문자 메시지 내용"

        '대체문자 유형, 공백-미전송, C-알림톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        '최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "010111222" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            msg.interOPRefKey = "20200806-" + CStr(i) '파트너 지정키, 수신자 구별용 메모.

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
            Dim receiptNum As String = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, content, _
                                                            altContent, altSendType, getReserveDT(), receiverList, txtUserId.Text, requestNum, buttonList)
            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 친구톡(텍스트) 전송을 요청합니다.
    ' - 친구톡은 심야 전송(20:00~08:00)이 제한됩니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#SendFTS
    '=========================================================================
    Private Sub btnSendFTS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFTS_one.Click

        '카카오톡채널 아이디, 카카오톡채널 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
        Dim plusFriendID As String = "@팝빌"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042991"

        '수신번호
        Dim receiverNum As String = "010111222"

        '수신자명
        Dim receiverName As String = "수신자명"

        '친구톡 내용 (최대 1000자)
        Dim content As String = "친구톡 내용입니다."

        '대체문자 메시지 내용 (최대 2000byte)
        Dim altContent As String = "대체문자 메시지 내용입니다."

        '대체문자 유형, 공백-미전송, C-친구톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '광고전송 여부
        Dim adsYN As Boolean = True

        '전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        '최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
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
            Dim receiptNum As String = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content, altContent, altSendType, receiverNum, _
                                                            receiverName, adsYN, getReserveDT(), buttonList, txtUserId.Text, requestNum)
            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [대량전송] 친구톡(텍스트) 전송을 요청합니다.
    ' - 친구톡은 심야 전송(20:00~08:00)이 제한됩니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#SendFTS_Multi
    '=========================================================================
    Private Sub btnSendFTS_multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFTS_multi.Click

        '카카오톡채널 아이디, 카카오톡채널 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
        Dim plusFriendID As String = "@팝빌"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042991"

        '대체문자 유형, 공백-미전송, C-친구톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '광고전송 여부
        Dim adsYN As Boolean = True

        '전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        '최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "010111222" '수신번호
            msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
            msg.msg = "친구톡 내용입니다." + CStr(i) '친구톡 내용 (최대 1000자)
            msg.altmsg = "대체문자 메시지 내용" + CStr(i) '대체문자 내용 (최대 2000byte)
            msg.interOPRefKey = "20201023-" + CStr(i) '파트너 지정키, 대량전송시, 수신자 구별용 메모

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

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' [동보전송] 친구톡(텍스트) 전송을 요청합니다.
    ' - 친구톡은 심야 전송(20:00~08:00)이 제한됩니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#SendFTS_Same
    '=========================================================================
    Private Sub btnSendFTS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFTS_same.Click

        '카카오톡채널 아이디, 카카오톡채널 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
        Dim plusFriendID As String = "@팝빌"

        '팝빌에 사전등록된 발신번호
        Dim senderNum As String = "07043042991"

        '친구톡 내용 (최대 1000자)
        Dim content As String = "친구톡 내용입니다."

        '대체문자 메시지 내용 (최대 2000byte)
        Dim altContent As String = "대체문자 메시지 내용입니다."

        '대체문자 유형, 공백-미전송, C-친구톡내용 전송, A-대체문자내용 전송
        Dim altSendType = "A"

        '광고전송 여부
        Dim adsYN As Boolean = True

        '전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
        '최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
        Dim requestNum = ""

        '전송정보 배열, 최대 1000건
        Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

        For i As Integer = 0 To 5
            Dim msg As KakaoReceiver = New KakaoReceiver
            msg.rcv = "010111222" '수신번호
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
            Dim receiptNum As String = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content, altContent, altSendType, _
                                                            adsYN, getReserveDT(), receiverList, buttonList, txtUserId.Text, requestNum)
            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 친구톡(이미지) 전송을 요청합니다.
    ' - 친구톡은 심야 전송(20:00~08:00)이 제한됩니다.
    ' - 이미지 전송규격 / jpg 포맷, 용량 최대 500KByte, 이미지 높이/너비 비율 1.333 이하, 1/2 이상
    ' - https://docs.popbill.com/kakao/dotnet/api#SendFMS
    '=========================================================================
    Private Sub btnSendFMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFMS_one.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            '카카오톡채널 아이디, 카카오톡채널 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
            Dim plusFriendID As String = "@팝빌"

            '팝빌에 사전등록된 발신번호
            Dim senderNum As String = "07043042991"

            '수신번호
            Dim receiverNum As String = "010111222"

            '수신자명
            Dim receiverName As String = "수신자명"

            '친구톡 내용 (최대 400자)
            Dim content As String = "친구톡 내용입니다."

            '대체문자 메시지 내용 (최대 2000byte)
            Dim altContent As String = "대체문자 메시지 내용입니다."

            '대체문자 유형, 공백-미전송, C-친구톡내용 전송, A-대체문자내용 전송
            Dim altSendType = "A"

            '광고전송 여부
            Dim adsYN As Boolean = True

            '첨부 이미지 파일경로
            Dim strFileName As String = fileDialog.FileName

            '이미지 링크 URL
            Dim imageURL As String = "https://www.popbill.com"

            '전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            '최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
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
                Dim receiptNum As String = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, content, altContent, altSendType, receiverNum, _
                                                                receiverName, adsYN, getReserveDT(), buttonList, strFileName, imageURL, _
                                                                txtUserId.Text, requestNum)
                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' [대량전송] 친구톡(이미지) 전송을 요청합니다.
    ' - 친구톡은 심야 전송(20:00~08:00)이 제한됩니다.
    ' - 이미지 전송규격 / jpg 포맷, 용량 최대 500KByte, 이미지 높이/너비 비율 1.333 이하, 1/2 이상
    ' - https://docs.popbill.com/kakao/dotnet/api#SendFMS_Multi
    '=========================================================================
    Private Sub btnSendFMS_multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFMS_multi.Click

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            '카카오톡채널 아이디, 카카오톡채널 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
            Dim plusFriendID As String = "@팝빌"

            '팝빌에 사전등록된 발신번호
            Dim senderNum As String = "07043042991"

            '대체문자 유형, 공백-미전송, C-친구톡내용 전송, A-대체문자내용 전송
            Dim altSendType = "A"

            '광고전송 여부
            Dim adsYN As Boolean = True

            '첨부 이미지 파일경로
            Dim strFileName As String = fileDialog.FileName

            '이미지 링크 URL
            Dim imageURL As String = "https://www.popbill.com"

            '전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            '최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            Dim requestNum = ""

            '전송정보 배열, 최대 1000건
            Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

            For i As Integer = 0 To 5
                Dim msg As KakaoReceiver = New KakaoReceiver
                msg.rcv = "010111222" '수신번호
                msg.rcvnm = "수신자명칭_" + CStr(i) '수신자명
                msg.msg = "친구톡 내용입니다." + CStr(i) '친구톡 내용 (최대 400자)
                msg.altmsg = "대체문자 메시지 내용" + CStr(i) '대체문자 내용 (최대 2000byte)
                msg.interOPRefKey = "20201023-" + CStr(i) '파트너 지정키, 대량전송시, 수신자 구별용 메모

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
                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum

            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' [동보전송] 친구톡(이미지) 전송을 요청합니다.
    ' - 친구톡은 심야 전송(20:00~08:00)이 제한됩니다.
    ' - 이미지 전송규격 / jpg 포맷, 용량 최대 500KByte, 이미지 높이/너비 비율 1.333 이하, 1/2 이상
    ' - https://docs.popbill.com/kakao/dotnet/api#SendFMS_Same
    '=========================================================================
    Private Sub btnSendFMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSendFMS_same.Click

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            '카카오톡채널 아이디, 카카오톡채널 목록 확인(LIstPlusFriend) API의 plusFriendID 항목 확인
            Dim plusFriendID As String = "@팝빌"

            '팝빌에 사전등록된 발신번호
            Dim senderNum As String = "07043042991"

            '친구톡 내용 (최대 400자)
            Dim content As String = "친구톡 내용입니다."

            '대체문자 메시지 내용 (최대 2000byte)
            Dim altContent As String = "대체문자 메시지 내용입니다."

            '대체문자 유형, 공백-미전송, C-친구톡내용 전송, A-대체문자내용 전송
            Dim altSendType = "A"

            '광고전송 여부
            Dim adsYN As Boolean = True

            '첨부 이미지 파일경로
            Dim strFileName As String = fileDialog.FileName

            '이미지 링크 URL
            Dim imageURL As String = "https://www.popbill.com"

            '전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            '최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            Dim requestNum = ""

            '전송정보 배열, 최대 1000건
            Dim receiverList As List(Of KakaoReceiver) = New List(Of KakaoReceiver)

            For i As Integer = 0 To 5
                Dim msg As KakaoReceiver = New KakaoReceiver
                msg.rcv = "010111222" '수신번호
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
                Dim receiptNum As String = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, content, altContent, altSendType, _
                                                                adsYN, getReserveDT(), receiverList, buttonList, strFileName, imageURL, txtUserId.Text, requestNum)
                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
            End Try

        End If
    End Sub

    '=========================================================================
    ' 알림톡/친구톡 전송요청시 발급받은 접수번호(receiptNum)로 예약전송건을 취소합니다.
    ' - 예약취소는 예약전송시간 10분전까지만 가능합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#CancelReserve
    '=========================================================================
    Private Sub btnCancelReserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelReserve.Click
        Try
            Dim response As Response

            response = kakaoService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전송요청번호(requestNum)를 할당한 알림톡/친구톡 예약전송건을 취소합니다.
    ' - 예약전송 취소는 예약시간 10분전까지만 가능합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#CancelReserveRN
    '=========================================================================
    Private Sub btnCancelReserveRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCancelReserveRN.Click
        Try
            Dim response As Response

            response = kakaoService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 알림톡/친구톡 전송요청시 발급받은 접수번호(receiptNum)로 전송결과를 확인합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetMessages
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
            Dim rowStr As String = "state(전송상태 코드) | sendDT(전송일시) | receiveNum(수신번호) | receiveName(수신자명) | content(내용) | "
            rowStr += "result(전송결과 코드) | resultDT(전송결과 수신일시) | altContnet(대체문자 내용) | altContentType(대체문자 전송유형) | "
            rowStr += "altSendDT(대체문자 전송일시) | altReult(대체문자 전송결과 코드) | altResultDT(대체문자 전송결과 수신일시) | receiptNum(접수번호) | requestNum(요청번호) | interOPRefKey (파트너 지정키)"

            ListBox1.Items.Add(rowStr)

            For Each Result As KakaoSentDetail In sentInfo.msgs
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.sendDT + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.content + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.resultDT + " | "
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 전송요청번호(requestNum)를 할당한 알림톡/친구톡 전송내역 및 전송상태를 확인합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetMessagesRN
    '=========================================================================
    Private Sub btnGetMessagesRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetMessagesRN.Click
        ListBox1.Items.Clear()
        Try
            Dim sentInfo As KakaoSentResult = kakaoService.GetMessagesRN(txtCorpNum.Text, txtRequestNum.Text)

            Dim tmp As String = "contentType(카카오톡 유형) : " + sentInfo.contentType + vbCrLf
            tmp += "templateCode(템플릿 코드) : " + sentInfo.templateCode + vbCrLf
            tmp += "plusFriendID(카카오톡채널 아이디) : " + sentInfo.plusFriendID + vbCrLf
            tmp += "sendNum(발신번호) : " + sentInfo.sendNum + vbCrLf
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
            Dim rowStr As String = "state(전송상태 코드) | sendDT(전송일시) | receiveNum(수신번호) | receiveName(수신자명) | content(내용) | "
            rowStr += "result(전송결과 코드) | resultDT(전송결과 수신일시) | altContnet(대체문자 내용) | altContentType(대체문자 전송유형) | "
            rowStr += "altSendDT(대체문자 전송일시) | altReult(대체문자 전송결과 코드) | altResultDT(대체문자 전송결과 수신일시) | receiptNum(접수번호) | requestNum(요청번호)"

            ListBox1.Items.Add(rowStr)

            For Each Result As KakaoSentDetail In sentInfo.msgs
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.sendDT + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.content + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.resultDT + " | "
                rowStr += Result.altContent + " | "
                rowStr += Result.altContentType.ToString + " | "
                rowStr += Result.altSendDT + " | "
                rowStr += Result.altResult + " | "
                rowStr += Result.altResultDT + " | "
                rowStr += Result.receiptNum + " | "
                rowStr += Result.requestNum

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 검색조건을 사용하여 알림톡/친구톡 전송 내역을 조회합니다.
    ' - 최대 검색기간 : 6개월 이내
    ' - https://docs.popbill.com/kakao/dotnet/api#Search
    '=========================================================================
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim State(6) As String
        Dim item(3) As String

        '최대 검색기간 : 6개월 이내
        '[필수] 시작일자, yyyyMMdd
        Dim SDate As String = "20190901"

        '[필수] 종료일자, yyyyMMdd
        Dim EDate As String = "20191231"

        '전송상태값 배열, 0-대기, 1-전송중, 2-성공, 3-대체, 4-실패, 5-취소
        State(0) = "0"
        State(1) = "1"
        State(2) = "2"
        State(3) = "3"
        State(4) = "4"
        State(5) = "5"

        '검색대상 배열, ATS(알림톡), FTS(친구톡 텍스트), FMS(친구톡 이미지)
        item(0) = "ATS"
        item(1) = "FTS"
        item(2) = "FMS"

        '예약문자 검색여부, 공백- 전체조회, 1-예약전송 조회, 0-즉시전송 조회
        Dim ReserveYN As String = ""

        '개인조회여부, True(개인조회), False(전체조회)
        Dim SenderYN As Boolean = False

        '페이지 번호
        Dim Page As Integer = 1

        '페이지 목록개수, 최대 1000건
        Dim PerPage As Integer = 1000

        '정렬방향, D-내림차순(기본값), A-오름차순
        Dim Order As String = "D"

        '조회 검색어, 카카오톡 전송시 기재한 수신자명 입력
        Dim QString As String = ""

        ListBox1.Items.Clear()
        Try
            Dim msgSearchList As KakaoSearchResult = kakaoService.Search(txtCorpNum.Text, SDate, EDate, State, item, ReserveYN, SenderYN, Order, Page, PerPage, _
                                                                         txtUserId.Text, QString)

            Dim tmp As String

            tmp = "code (응답코드) : " + CStr(msgSearchList.code) + vbCrLf
            tmp = tmp + "total (총 검색결과 건수) : " + CStr(msgSearchList.total) + vbCrLf
            tmp = tmp + "perPage (페이지당 검색개수) : " + CStr(msgSearchList.perPage) + vbCrLf
            tmp = tmp + "pageNum (페이지 번호) : " + CStr(msgSearchList.pageNum) + vbCrLf
            tmp = tmp + "pageCount (페이지 개수) : " + CStr(msgSearchList.pageCount) + vbCrLf
            tmp = tmp + "message (응답메시지) : " + msgSearchList.message + vbCrLf + vbCrLf

            MsgBox(tmp)

            Dim rowStr As String = "state(전송상태 코드) | sendDT(전송일시) | receiveNum(수신번호) | receiveName(수신자명) | content(내용) | "
            rowSTR += "result(전송결과 코드) | resultDT(전송결과 수신일시) | altContnet(대체문자 내용) | altContentType(대체문자 전송유형) | "
            rowStr += "altSendDT(대체문자 전송일시) | altReult(대체문자 전송결과 코드) | altResultDT(대체문자 전송결과 수신일시) | receiptNum(접수번호) | requestNum(요청번호)"

            ListBox1.Items.Add(rowStr)

            For Each Result As KakaoSentDetail In msgSearchList.list
                rowStr = ""
                rowStr += Result.state.ToString + " | "
                rowStr += Result.sendDT + " | "
                rowStr += Result.receiveNum + " | "
                rowStr += Result.receiveName + " | "
                rowStr += Result.content + " | "
                rowStr += Result.result.ToString + " | "
                rowStr += Result.resultDT + " | "
                rowStr += Result.altContent + " | "
                rowStr += Result.altContentType.ToString + " | "
                rowStr += Result.altSendDT + " | "
                rowStr += Result.altResult + " | "
                rowStr += Result.altResultDT + " | "
                rowStr += Result.receiptNum + " | "
                rowStr += Result.requestNum

                ListBox1.Items.Add(rowStr)
            Next
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 카카오톡 전송내역 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetSentListURL
    '=========================================================================
    Private Sub btnGetSentListURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetSentListURL.Click
        Try
            Dim url As String = kakaoService.GetSentListURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 잔여포인트를 확인합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetBalance
    '=========================================================================
    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetBalance.Click

        Try
            Dim remainPoint As Double = kakaoService.GetBalance(txtCorpNum.Text)

            MsgBox("연동회원 잔여포인트 : " + remainPoint.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원 포인트충전 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetChargeURL
    '=========================================================================
    Private Sub btnGetChargeURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetChargeURL.Click
        Try
            Dim url As String = kakaoService.GetChargeURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너의 잔여포인트를 확인합니다.
    ' - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetPartnerBalance
    '=========================================================================
    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = kakaoService.GetPartnerBalance(txtCorpNum.Text)

            MsgBox("파트너 잔여포인트 : " + remainPoint.ToString())
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 파트너 포인트충전 팝업 URL을 확인합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetPartnerURL
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 알림톡(ATS) 전송단가를 확인합니다
    ' - https://docs.popbill.com/kakao/dotnet/api#GetUnitCost
    '=========================================================================
    Private Sub btnUnitCost_SMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnUnitCost_ATS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.ATS

        Try
            Dim unitCost As Single = kakaoService.GetUnitCost(txtCorpNum.Text, kType)

            MsgBox("알림톡 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 친구톡 텍스트(FTS) 전송단가를 조회합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetUnitCost
    '=========================================================================
    Private Sub btnGetUnitCost_FTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetUnitCost_FTS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.FTS

        Try
            Dim unitCost As Single = kakaoService.GetUnitCost(txtCorpNum.Text, kType)

            MsgBox("친구톡 텍스트 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 친구톡 이미지(FMS) 전송단가를 조회합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetUnitCost
    '=========================================================================
    Private Sub btnGetUnitCost_FMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetUnitCost_FMS.Click

        '카카오톡 전송유형, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
        Dim kType As KakaoType = KakaoType.FMS

        Try
            Dim unitCost As Single = kakaoService.GetUnitCost(txtCorpNum.Text, kType)

            MsgBox("친구톡 이미지 전송단가(unitCost) : " + unitCost.ToString())

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 알림톡(ATS) 과금정보를 확인합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetChargeInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 친구톡 텍스트(FTS) 과금정보를 확인합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetChargeInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 친구톡 이미지(FMS) 과금정보를 확인합니다.
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 해당사업자의 회원가입 여부를 확인합니다.
    ' - 사업자번호는 '-'를 제외한 10자리 숫자 문자열입니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#CheckIsMember
    '=========================================================================
    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckIsMember.Click
        Try
            Dim response As Response = kakaoService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 팝빌 회원아이디 중복여부를 확인합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#CheckID
    '=========================================================================
    Private Sub btnCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckID.Click
        Try
            Dim response As Response = kakaoService.CheckID(txtCorpNum.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원 신규가입을 요청합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#JoinMember
    '=========================================================================
    Private Sub btnJoinMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnJoinMember.Click
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
            Dim response As Response = kakaoService.JoinMember(joinInfo)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 팝빌(www.popbill.com)에 로그인된 팝빌 URL을 반환합니다.
    ' - 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetAccessURL
    '=========================================================================
    Private Sub btnGetAccessURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGetAccessURL.Click
        Try
            Dim url As String = kakaoService.GetAccessURL(txtCorpNum.Text, txtUserId.Text)

            MsgBox(url)
            txtURL.Text = url
        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)
        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자를 추가로 등록합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#RegistContact
    '=========================================================================
    Private Sub btnRegistContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRegistContact.Click

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
            Dim response As Response = kakaoService.RegistContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 담당자 목록을 조회합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#ListContact
    '=========================================================================
    Private Sub btnListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnListContact.Click
        Try
            Dim contactList As List(Of Contact) = kakaoService.ListContact(txtCorpNum.Text, txtUserId.Text)

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
    ' - https://docs.popbill.com/kakao/dotnet/api#UpdateContact
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
            Dim response As Response = kakaoService.UpdateContact(txtCorpNum.Text, joinData, txtUserId.Text)

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 조회합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#GetCorpInfo
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
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub

    '=========================================================================
    ' 연동회원의 회사정보를 수정합니다.
    ' - https://docs.popbill.com/kakao/dotnet/api#UpdateCorpInfo
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

            MsgBox("응답코드(code) : " + response.code.ToString() + vbCrLf + "응답메시지(message) : " + response.message)

        Catch ex As PopbillException
            MsgBox("응답코드(code) : " + ex.code.ToString() + vbCrLf + "응답메시지(message) : " + ex.Message)

        End Try
    End Sub
End Class
