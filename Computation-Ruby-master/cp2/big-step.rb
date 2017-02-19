#expression
class Number < Struct.new(:val)
	def to_s
		val.to_s
	end
	
	def inspect
		"#{self}"
	end
	
	def evaluate(env)
		self
	end
end

class Boolean < Struct.new(:val)
	def to_s
		val.to_s
	end
	
	def inspect
		"#{self}"
	end
	
	def evaluate(env)
		self
	end
end

class Variable < Struct.new(:name)
	def to_s
		name.to_s
	end
	
	def inspect
		"#{self}"
	end
	
	def evaluate(env)
		env[name]
	end
end

class Add < Struct.new(:left, :right)
	def to_s
		"#{left}+#{right}"
	end
	
	def inspect
		"#{self}"
	end
	
	def evaluate(env)
		Number.new(left.evaluate(env).val + right.evaluate(env).val)	#evaluate返回的是Number或者Boolea，要用.val取得数据
	end
end

class Multiply < Struct.new(:left, :right)
	def to_s
		"#{left}*#{right}"
	end
	
	def inspect
		"#{self}"
	end
	
	def evaluate(env)
		Number.new(left.evaluate(env).val * right.evaluate(env).val)
	end
end

class LessThan < Struct.new(:left, :right)
	def to_s
		"#{left}<#{right}"
	end
	
	def inspect
		"#{self}"
	end
	
	def evaluate(env)
		Boolean.new(left.evaluate(env).val < right.evaluate(env).val)
	end
end

#statement
class DoNothing
	def evaluate(env)
		env
	end
end

class Assign < Struct.new(:name, :expression)
	def evaluate(env)
		env.merge({name=>expression.evaluate(env)})
	end
end

class If < Struct.new(:conditioin, :consequence, :alternative)
	def evaluate(env)
		case conditioin.evaluate(env)
		when Boolean.new(true)
			consequence.evaluate(env)
		when Boolean.new(false)
			alternative.evaluate(env)
		end
	end
end

class Sequence < Struct.new(:first, :second)
	def evaluate(env)
		second.evaluate(first.evaluate(env))
	end
end

class While < Struct.new(:conditioin, :body)
	def evaluate(env)
		case conditioin.evaluate(env)
		when Boolean.new(true)
			evaluate(body.evaluate(env))			#递归了一手
		when Boolean.new(false)
			env
		end
	end
end

#code
Number.new(3).evaluate({})
Add.new(Multiply.new(Number.new(2), Number.new(2)), Multiply.new(Number.new(3), Number.new(3))).evaluate({})
LessThan.new( Add.new(Variable.new(:x), Number.new(2)), Variable.new(:y) ).evaluate({x:Number.new(2), y:Number.new(5)})
Sequence.new(	Assign.new(:x, Add.new(Number.new(1), Number.new(2))),
				Assign.new(:y, Add.new(Variable.new(:x), Number.new(3)))).evaluate({})