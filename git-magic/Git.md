#Git
* workspace
* git init
* git add
* git commit
* git diff
* .gitignore

##worksapce
![](https://github.com/lsj9383/git-magic/blob/master/git-pic/Workspace.png)

##git diff
diff命令，可以比较文件的不同<br>
查看当前文件与stage间的不同
```
git diff
```
---
查看stage与commit间的不同
```
git diff --cached
```
---
查看当前文件与commit间的不同
```
git diff HEAD
```
---
查看两个版本之间的不同，例如要查看最近提交和上一次提交的不同:`git diff HEAD HEAD^`。
```
git diff <v1> <v2>
```
---

##.gitignore

