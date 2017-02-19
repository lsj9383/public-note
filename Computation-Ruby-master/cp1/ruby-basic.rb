#基本元素
"hello world"
'hello world'
nil
:my_symbol

#数据结构
numbers = [1, 2, 3, 4, "shenme?"]
numbers[0]
numbers[4]

args = 18...30
args.entries[0]
args.entries
fruit = {'a'=>'apple', 'b'=>'banana', 'c'=>'coconut'}
rect = {:width=>100, :high=>200, :length=>300}
fruit
rect
fruit['b']
rect[:length]

#proc
add = ->x,y {x+y}
add.call(1, 2)
->x{x+1}.call(2)

#flow
if 2<3
  'less'
else
  'true'
end

quantify= 
	->number{
		case number
		when 1
			'one'
		when 2
			'two'
		when 3
			'three'
		else
			'other'
		end
	}
quantify.call(2)
quantify.call(10)

#object
o = Object.new
def o.add(x, y)
	x+y
end
def o.add_twice(x, y)
	add(x, y)+add(x, y)
end
o.add_twice(10, 2)

#class and module
class Calculator
	def divide(x, y)
		x/y
	end
end

class Calchild < Calculator
	def add(x, y)
		x+y
	end
end

c = Calculator.new
c.divide(10, 2)
chil = Calchild.new
chil.add(10, 2)
chil.divide(15, 3)

#string
o = Object.new
def o.to_s
	'a new object'
end
"here is #{o}"

#check object
def o.inspect
	'[my object]'
end
o
puts o

#variadic method
def join_with_commas(*words)
	words.join(',')
end
join_with_commas("lsj", "hjs", "lr")
['ni', 'hao', 'ma', '?'].join(':')

#block
def do_three_times
	yield(1,2)
	yield(2,3)
	yield(3,4)
end

do_three_times(){ |n1, n2| puts "hello #{n1} and #{n2}"}

#enumerable
[1,2,3,4,5].each(){|n| puts "hello #{n}"}
[1,2,3,4,5].map(){|n| n+1}

#struct
class Student < Struct.new(:name, :age)
	def to_s
		"my name is #{name} and I have #{age} years"
	end
end

lsj = Student.new('lsj', 23)
lr = Student.new('lr', 24)
puts "this is lsj : #{lsj}"
puts "this is lr  : #{lr}"
