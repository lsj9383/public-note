#计算的本质
	————深入剖析程序和计算机
##章节
* chapter1: Ruby基础
	* [ruby基础语法](https://github.com/lsj9383/Computation-Ruby/blob/master/cp1/ruby-basic.rb)
* chapter2: 程序的含义
	* [小步语义](https://github.com/lsj9383/Computation-Ruby/blob/master/cp2/small-step.rb)
	* [大步语义](https://github.com/lsj9383/Computation-Ruby/blob/master/cp2/big-step.rb)
* chapter3: 最简单的计算机
	* [确定有限自动机](https://github.com/lsj9383/Computation-Ruby/blob/master/cp3/dfa.rb)
	* [非确定有限自动机](https://github.com/lsj9383/Computation-Ruby/blob/master/cp3/nfa.rb)
	* [带自由移动的非确定有限自动机](https://github.com/lsj9383/Computation-Ruby/blob/master/cp3/fm_nfa.rb)
	* [正则表达式](https://github.com/lsj9383/Computation-Ruby/blob/master/cp3/regular.rb)
	* [NFA转DFA](https://github.com/lsj9383/Computation-Ruby/blob/master/cp3/nfa2dfa.rb)
* chapter4: 增强计算能力
	* [确定性下推机](https://github.com/lsj9383/Computation-Ruby/blob/master/cp4/dpda.rb)
	* [非确定性下推机](https://github.com/lsj9383/Computation-Ruby/blob/master/cp4/npda.rb)
	* [语法解析]()
* chapter5: 终极机器
	* [图灵机](https://github.com/lsj9383/Computation-Ruby/blob/master/cp5/DTM.rb)
* chapter6: 从零开始编程
	* [ruby模拟lambda演算](https://github.com/lsj9383/Computation-Ruby/blob/master/cp6/ruby2lambda.rb)
	* [FizzBuzz](https://github.com/lsj9383/Computation-Ruby/blob/master/cp6/FizzBuzz.rb)
	* [lambda高级编程-Stream](https://github.com/lsj9383/Computation-Ruby/blob/master/cp6/lambda_advanced.rb)
	* [λ](https://github.com/lsj9383/Computation-Ruby/blob/master/cp6/lambda.rb)
	
###ruby镜像
以上代码均使用ruby安装，在使用ruby时，会涉及到一些包下载，但由于下载源在国外，无法下载，因此要将下载源改为国内镜像。在本例程中，由于涉及到了使用语法解析工具`treetop`，因此要配置下载源<br>
<br>
* 查看当前下载源:
```
gem sources
```
* 删除当前下载源:
```
gem sources -r <url>
```
* 添加新的下载源，目前国内由ruby-china维护:
```
gem sources --add https://gems.ruby-china.org/
gem sources -l
```
* 使用gem安装treetop:
```
gem install treetop
```
###Y组合子

![](https://github.com/lsj9383/Computation-Ruby/blob/master/pic/Y.jpg)

对于一个递归函数的定义，或许会首先想到：
```
F = ->x{ ->y{ F[t[x]][t[y]] } }
```
事实上，这种**伪递归**是一种欺骗。因为在定义F时又用了F，但是这时候F并未定义，在很多编程语言里，这不能解释或编译(如ruby)。<br>
作为匿名函数真正的递归的一种实现，可以考虑将函数自己作为参数传给自己，如下所示：
```
F = ->f { ->x { ->y { f[f][t[x]][t[y]] } } }
F[F][a][b]
```
这确实是真正的递归, F是3个函数的参数，第一个参数是一个高阶函数，该函数接受3个参数。第一个参数，可以将F自己代入进去，这样便能做到递归:
```
F[F][a][b] => F[F][t[a]][t[b]]
```
注意, 本文中的**P=>M**代表对P的调用，将会得到的某一步语句。<br>
但是，对于这个这种使用方法，觉得不够完美，甚至觉得丑陋。**F[F]**和**f[f]**是什么玩意儿?<br>
为了让形式更加优美，希望能够不存在**f[f][][]**这种形式，而是直接**f[][]**的形式:
```
F = ->f{ ->x { ->y { f[x][y] }} }
```
但是这种形式，该如何去调用呢？**F[a][b]**或者**F[F][a][b]**都始终不对。<br>
假设存在一个函数**P**，对于**F[P][a][b]**会得到**P[t[a]][t[b]]**。倘若**F[P][a][b]**是由**P[a][b]**调用所得。那么可以得到这样一个调用链:
```
P[a][b] => F[P][a][b] => P[t[a]][t[b]]
```
也就是说，若存在一个过程**P**,使得**P => F[P]**成立，那么**P[a][b]**就会是一次递归调用。现在可以正式引入Y组合子了。<br>
由于从**P**中要得到**F[P]**，那么必然P中包含了**F**。现在又假设存在一个过程**Y**，使得**P = Y[F]**，那么就可以得出只要满足:
**Y[F] = F[Y[F]]**
那么对**Y[F][a][b]**的调用，就是递归的。很明显:
```
Y[F][a][b] => F[Y[F]][a][b] => Y[F][t[a]][t[b]] => ...
```
最后，满足**Y[F] = F[Y[F]]**式子，就被称为Y组合子。由Haskell推导出的Y组合子，已经在如上图片中展示出来，它满足该式子。