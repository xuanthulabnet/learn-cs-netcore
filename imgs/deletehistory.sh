#!/usr/bin/env bash
git checkout --orphan abcdefgh
git add -A
git commit -m"Learn C#"
git branch -D master                                # xóa nhánh master
git branch -m master                                # đổi tên nhánh hiện tại (temp_branch) thành master
git push -f github master                           # push thay đổi lên Remote
git push -f gitlab master                           # push thay đổi lên Remote
