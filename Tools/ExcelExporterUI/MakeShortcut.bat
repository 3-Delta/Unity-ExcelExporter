:: ���ã������ļ���������

:: rem �� :: ��ע��
:: echo��ʾ���������̨��off��ʾ��������������̨
:: @�ǲ��ñ����������������̨
:: set ���ñ����� ʹ�ñ�����Ҫ%%������, =ǰ�����пո�
:: �ⲿ�������ݵ�bat�ڲ���bat�ڲ�ʹ��%1 ~ %9���գ����9������, %0�����������ļ�����
:: %cd% ��ʾִ��bat�ļ�ʱ����·���� ����E����ִ��F���µ�һ��bat�ļ��� cd��ʾΪE
:: %~dp0 ��ǰִ�е�bat���ڵ�·����������bat��,Ҳ����F��
:: �Ƽ� %~dp0��%cd%�����ڹ���ԱȨ��ִ���³�������

:: ĳЩ�����ִ��bat�ļ���Ҫ����ԱȨ�ޣ��������ǲ�����ÿ�ζ��Ҽ�����Ա���У�������������https://www.jb51.net/article/193692.htm

:: https://blog.csdn.net/albertsh/article/details/52807345
:: https://www.cnblogs.com/xpwi/p/9626959.html
:: ��һ��Ŀ¼��ֱ�Ӻ��\..\���ɣ����ַ������ӵ�һ���֣����� set parentPath=%cd%%\..\

@echo off

:: ��ȡ����ԱȨ�ޣ����������˺ܶ�Ĳ������ݵ�����
:: %1 mshta vbscript:CreateObject(��Shell.Application��).ShellExecute(��cmd.exe��,"/c %~s0 ::","",��runas��,1)(window.close)&&exit

set src=%1
set dest=%2

echo source: %src%
echo destination��%dest%

:: ���dest�ļ��Ѿ����ڣ�����ɾ���ϵ�
if exist %dest% (
	del %dest%
)

:: ������ �� ��ݷ�ʽ ����һ�������� ����һ����������dll��exe�����������ӾͲ��ܶ���ִ��
:: mklink Ĭ�ϲ����ļ��������ӣ� mklink /?����
:: mklink %dest% %src%

:: ��ݷ�ʽ https://superuser.com/questions/455364/how-to-create-a-shortcut-using-a-batch-script
set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"

echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%dest%.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "%src%" >> %SCRIPT%
echo oLink.Description = "��ݷ�ʽ"
echo oLink.Save >> %SCRIPT%

cscript /nologo %SCRIPT%
del %SCRIPT%

echo Press any key to exit
pause
