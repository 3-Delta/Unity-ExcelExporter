:: ���ã������ļ���������

:: ��ע��
:: echo��ʾ���������̨��off��ʾ��������������̨
:: @�ǲ��ñ����������������̨
:: set ���ñ����� ʹ�ñ�����Ҫ%%������, =ǰ�����пո�
:: �ⲿ�������ݵ�bat�ڲ���bat�ڲ�ʹ��%1 ~ %9���գ����9������
:: %cd% ��ʾִ��bat�ļ�ʱ����·���� ����E����ִ��F���µ�һ��bat�ļ��� cd��ʾΪE
:: %~dp0 ��ǰִ�е�bat���ڵ�·����������bat��,Ҳ����F��
:: https://blog.csdn.net/albertsh/article/details/52807345

@echo off
set dest=%1
set src=%2

echo %dest%
echo %src%

:: mklink Ĭ�ϲ����ļ��������ӣ� mklink /?����
mklink %dest% %src%

echo Press any key to exit
pause
