:: 作用：创建文件的软连接

:: 是注释
:: echo表示输出到控制台，off表示命令本身不输出到控制台
:: @是不让本行命令输出到控制台
:: set 设置变量， 使用变量需要%%包起来, =前后不能有空格
:: 外部参数传递到bat内部，bat内部使用%1 ~ %9接收，最多9个参数
:: %cd% 表示执行bat文件时所在路径， 比如E盘下执行F盘下的一个bat文件， cd显示为E
:: %~dp0 当前执行的bat所在的路径，不包括bat名,也就是F盘
:: https://blog.csdn.net/albertsh/article/details/52807345

@echo off
set dest=%1
set src=%2

echo %dest%
echo %src%

:: mklink 默认产生文件的软连接， mklink /?帮助
mklink %dest% %src%

echo Press any key to exit
pause
