#basic object
class Variable < Struct.new(:name)
	def to_s
		name.to_s
	end

	def inspect
		"#{self}"
	end
	
	def reducible?
		true
	end
	
	def reduce(env)
		env[name]			#reduce is extract value from environment
	end
end

class Boolean < Struct.new(:value)
	def to_s
		value.to_s
	end

	def inspect
		"#{self}"
	end
	
	def reducible?
		false
	end
end

class Number < Struct.new(:value)
	def to_s
		value.to_s
	end

	def inspect
		"#{self}"
	end
	
	def reducible?
		false
	end
end

class DoNothing < Struct.new(:name, :expression)
	def to_s
		"do-nothing"
	end
	
	def inspect
		"#{self}"
	end
	
	def ==(other_statment)
		other_statment.instance_of?(DoNothing)
	end
	
	def reducible?
		false
	end
end

class Assign < Struct.new(:name, :expression)
	def to_s
		"#{name}=#{expression}"
	end
	
	def inspect
		"#{self}"
	end
	
	def reducible?
		true
	end
	
	def reduce(env)
		if expression.reducible?
			[Assign.new(name, expression.reduce(env)), env]
		else
			[DoNothing.new, env.merge({name=>expression})]
		end
	end
end

class If < Struct.new(:condition, :consequence, :alternative)
	def to_s
		"if (#{condition}) { #{consequence} } else { #{alternative} }"
	end
	
	def inspect
		"#{self}"
	end
	
	def reducible?
		true
	end
	
	def reduce(env)
		if condition.reducible?
			[If.new(condition.reduce(env), consequence, alternative), env]
		else
			case condition
			when Boolean.new(true)
				[consequence, env]
			when Boolean.new(false)
				[alternative, env]
			end
		end
	end
end

class Sequence < Struct.new(:first, :second)
	def to_s
		"#{first}; #{second}"
	end
	
	def inspect
		"#{self}"
	end
	
	def reducible?
		true
	end
	
	def reduce(env)
		case first
		when DoNothing.new
			[second, env]
		else
			reduced_first, reduced_env = first.reduce(env)			#reduce first
			[Sequence.new(reduced_first, second), reduced_env]		#retrun
		end
	end
end

class While < Struct.new(:condition, :body)
	def to_s
		"while(#{condition}){ #{body} }"
	end
	
	def inspect
		"#{self}"
	end
	
	def reducible?
		true
	end
	
	def reduce(env)
		[If.new(condition, Sequence.new(body, self), DoNothing.new), env]
	end
end

class LessThan < Struct.new(:left, :right)
	def to_s
		"#{left}<#{right}"
	end
	def inspect
		"#{self}"
	end
	def reducible?
		true
	end
	def reduce(env)
		if left.reducible?
			LessThan.new(left.reduce(env), right)
		elsif right.reducible?
			LessThan.new(left, right.reduce(env))
		else
			Boolean.new(left.value < right.value);
		end
	end
end

class Add < Struct.new(:left, :right)
	def to_s
		"#{left}+#{right}"
	end
	def inspect
		"#{self}"
	end
	def reducible?
		true
	end
	def reduce(env)
		if left.reducible?
			Add.new(left.reduce(env), right)
		elsif right.reducible?
			Add.new(left, right.reduce(env))
		else
			Number.new(left.value + right.value);
		end
	end
end

class Multiply < Struct.new(:left, :right)
	def to_s
		"#{left}*#{right}"
	end
	def inspect
		"#{self}"
	end
	def reducible?
		true
	end
	def reduce(env)
		if left.reducible?
			Multiply.new(left.reduce(env), right)
		elsif right.reducible?
			Multiply.new(left, right.reduce(env))
		else
			Number.new(left.value * right.value);
		end
	end
end

#machine
class Machine < Struct.new(:statement, :environment)
	def step
		self.statement, self.environment = statement.reduce(environment)		#one step
	end
	
	def run
		while statement.reducible?
			puts "#{statement}, #{environment}"							#print current expression
			step									#reduce
		end
		puts "#{statement}, #{environment}"
	end
end

#code
expression = Add.new( 	Multiply.new(Number.new(1), Number.new(2)),
						Multiply.new(Number.new(3), Number.new(4)))
Machine.new(expression, nil).run()
Machine.new(LessThan.new( Number.new(5), Add.new(Number.new(2), Number.new(2)) ), nil).run()
Machine.new(Add.new(Variable.new(:x), 
					Variable.new(:y)), 
			{x: Number.new(3), y: Number.new(4)}).run()
Machine.new(Assign.new(Variable.new(:x), Add.new(Variable.new(:x), Number.new(1))), {x:Number.new(2)}).run()
Machine.new(If.new(Variable.new(:x), Assign.new(:y, Number.new(1)), Assign.new(:y, Number.new(2))), {x:Boolean.new(true)}).run()
Machine.new(Sequence.new(
						Assign.new(:x, Add.new(Number.new(1), Number.new(2))),
						Assign.new(:y, Add.new(Variable.new(:x), Number.new(3)))),
			{}).run()
Machine.new(While.new( 	LessThan.new(Variable.new(:x), Number.new(5)),
						Assign.new(:x, Multiply.new(Variable.new(:x), Number.new(3)))), {x:Number.new(1)}).run()