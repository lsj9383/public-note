#带自由移动的不确定性有限自动机

require 'set'

class FARule < Struct.new(:state, :character, :next_state)
	def applies_to?(state, character)	#判断状态和输入的字符 是否和该规则匹配
		self.state == state && self.character == character
	end
	
	def follow				#返回该rule对应的输出状态
		self.next_state
	end
	
	def inspect
		"#<FARule #{state.inspect} --#{character} --> #{next_state.inspect}>"
	end
end

class NFARulebook < Struct.new(:rules)
	def next_states(states, character)	#当前状态集合中的每个状态 都进行找下一个状态 并将组合成状态集合
		states.flat_map { |state| follow_rules_for(state, character) }.to_set	#使用flat_map是为了将生成的结果全以数组的形式连接.否则将会是个三维数组
	end
	
	def follow_free_moves(states)
		more_states = next_states(states, nil)
		
		if more_states.subset?(states)	#将states自由转移后得到的状态，和转移之前相比。若转移后的状态，是转移前的子集，说明已经自由转移完成了.(比如1->2,4  1,2,4->2,4，因为2和4没办法转移)
			states
		else
			follow_free_moves(states + more_states)	#继续转移
		end
	end
	
	def follow_rules_for(state, character)		#
		rules_for(state, character).map(){ |rule| rule.follow }
	end
	
	def rules_for(state, character)		#在一个状态，和一个输入下，找到对应的所有规则，并组成一个数组
		rules.select{ |rule| rule.applies_to?(state, character) }	#选择出满足条件的集合
	end
end

class NFA < Struct.new(:current_states, :accept_states, :rulebook)
	def accepting?
		(current_states & accept_states).any?		#求#是找当前状态集与可接收状态集的交集，用any?判断是否存在.
	end
	
	def read_character(character)	#读一个字符，并进行状态转移
		self.current_states = rulebook.next_states(rulebook.follow_free_moves(current_states), character)	#在进行下次判断前，先做一次自由移动
		self.current_states = rulebook.follow_free_moves(current_states)									#在做完后，应该来一次自由移动
	end
	
	def read_string(string)	#读入字符串，并对每个字符作处理
		string.chars.each { |character| read_character(character) }	#string.chars 得到字符串对应的字符数组
	end
end

class NFADesign < Struct.new(:start_state, :accept_states, :rulebook)
	def accepts?(string)
		nfa = NFA.new(Set[start_state], accept_states, rulebook)
		nfa.read_string(string)
		nfa.accepting?
	end
end

#regular
module Pattern
	def bracket(outer_precedence)
		if precedence < outer_precedence
			'(' + to_s + ')'
		else
			to_s
		end
	end
	
	def inspect
		"/#{self}/"
	end
	
	def matches?(string)
		to_nfa_design.accepts?(string)
	end
end

class Empty
	include Pattern
	def to_s
		''
	end
	
	def precedence
		3
	end
	
	def to_nfa_design
		start_state = Object.new
		accept_states = [start_state]
		rulebook = NFARulebook.new([])
		NFADesign.new(start_state, accept_states, rulebook)
	end
end

class Literal < Struct.new(:character)
	include Pattern
	def to_s
		character
	end
	
	def precedence
		3
	end
	
	#状态采用Object.new，这样相应状态永远是唯一的，便于进行组合。若用数字，则容易出现状态冲突。
	def to_nfa_design
		start_state = Object.new
		accept_state = Object.new
		rule = FARule.new(start_state, character, accept_state);
		rulebook = NFARulebook.new([rule])
		NFADesign.new(start_state, [accept_state], rulebook)
	end
end

class Concatenate < Struct.new(:first, :second)
	include Pattern
	def to_s
		[first, second].map{ | pattern | pattern.bracket(precedence) }.join		#显示每个模式
	end
	
	def precedence
		1
	end
	
	def to_nfa_design
		first_nfa_design = first.to_nfa_design		#生成第一个模式的nfa
		second_nfa_design = second.to_nfa_design	#生成第二个模式的nfa
		
		start_state = first_nfa_design.start_state
		accept_states = second_nfa_design.accept_states
		rules = first_nfa_design.rulebook.rules + second_nfa_design.rulebook.rules	#若之前的state用的数字，这里很有可能有重，因此之前该用object作为状态
		extra_rules = first_nfa_design.accept_states.map{ |state|					#将第一个nfa的接受状态，转化为到第二个nfa的开始状态的规则集，作为额外规则
			FARule.new(state, nil, second_nfa_design.start_state)
		}
		rulebook = NFARulebook.new(rules + extra_rules)
		NFADesign.new(start_state, accept_states, rulebook)
	end
end

class Choose < Struct.new(:first, :second)
	include Pattern
	def to_s
		[first, second].map{ | pattern | pattern.bracket(precedence) }.join('|')		#显示每个模式
	end
	
	def precedence
		0
	end
	
	def to_nfa_design
		first_nfa_design = first.to_nfa_design		#生成第一个模式的nfa
		second_nfa_design = second.to_nfa_design	#生成第二个模式的nfa
		
		start_state = Object.new
		accept_states = first_nfa_design.accept_states + second_nfa_design.accept_states
		rules = first_nfa_design.rulebook.rules + second_nfa_design.rulebook.rules	#若之前的state用的数字，这里很有可能有重，因此之前该用object作为状态
		extra_rules = [first_nfa_design, second_nfa_design].map{ |nfa_design|
			FARule.new(start_state, nil, nfa_design.start_state)
		}
		rulebook = NFARulebook.new(rules + extra_rules)
		NFADesign.new(start_state, accept_states, rulebook)
	end
end

class Repeat < Struct.new(:pattern)
	include Pattern
	
	def to_s
		pattern.bracket(precedence) + '*'
	end
	
	def precedence
		2
	end
	
	def to_nfa_design
		pattern_nfa_design = pattern.to_nfa_design
		
		start_state = Object.new	#新的起始状态
		accept_states = pattern_nfa_design.accept_states + [start_state]	#旧nfa的接受状态和新的起始状态作为接收状态
		rules = pattern_nfa_design.rulebook.rules
		extra_rules = pattern_nfa_design.accept_states.map {|accept_state|
			FARule.new(accept_state, nil, pattern_nfa_design.start_state)	#将旧的nfa的接受状态，自由转移到旧nfa的起始状态
		} + [FARule.new(start_state, nil, pattern_nfa_design.start_state)]	#新的起始状态，自由转移到旧的起始状态
		rulebook = NFARulebook.new(rules + extra_rules)
		NFADesign.new(start_state, accept_states, rulebook)
	end
end



#code
pattern = Repeat.new(
			Choose.new(
				Concatenate.new(Literal.new('a'), Literal.new('b')),
				Literal.new('a')))
Empty.new.matches?('')
Empty.new.matches?('a')
Empty.new.matches?('b')
Literal.new('a').matches?('a')
Literal.new('a').matches?('b')
Literal.new('b').matches?('b')
Literal.new('b').matches?('bac')
Concatenate.new( Literal.new('a'), Concatenate.new( Literal.new('b'), Literal.new('c') )).matches?('a')
Concatenate.new( Literal.new('a'), Concatenate.new( Literal.new('b'), Literal.new('c') )).matches?('ab')
Concatenate.new( Literal.new('a'), Concatenate.new( Literal.new('b'), Literal.new('c') )).matches?('abc')
Concatenate.new( Literal.new('a'), Concatenate.new( Literal.new('b'), Literal.new('c') )).matches?('abcd')
Choose.new(Literal.new('c'), Choose.new(Literal.new('a'), Literal.new('b'))).matches?('a')
Choose.new(Literal.new('c'), Choose.new(Literal.new('a'), Literal.new('b'))).matches?('ab')
Choose.new(Literal.new('c'), Choose.new(Literal.new('a'), Literal.new('b'))).matches?('b')
Repeat.new(Literal.new('a')).matches?('')
Repeat.new(Literal.new('a')).matches?('a')
Repeat.new(Literal.new('a')).matches?('aa')
Repeat.new(Literal.new('a')).matches?('ab')
Repeat.new(Literal.new('a')).matches?('aaaaaaaaaaaaaaaaa')
pattern.matches?('ab')
pattern.matches?('aabc')