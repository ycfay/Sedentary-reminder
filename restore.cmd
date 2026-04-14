@echo off
chcp 65001
CLS
:loop
set /p a=确定要执行吗？（Y继续，N退出）
if /i '%a%'=='Y' goto continue
if /i '%a%'=='N' goto end
echo 输入有误，请重新输入：&&goto loop 
 
:continue
git fetch --all
git reset --hard origin/master
git pull
pause
 
:end
