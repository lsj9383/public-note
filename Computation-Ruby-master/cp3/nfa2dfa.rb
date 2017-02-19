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

#确定性有限自动机
class DFARulebook < Struct.new(:rules)
	def next_state(state, character)		#根据状态和输入字符，找到相匹配的下一个状态
		rule_for(state, character).follow
	end
	
	def rule_for(state, character)			#在所有的规则中找到匹配的规则
		rules.detect{ |rule| rule.applies_to?(state, character) }
	end
end

class DFA < Struct.new(:current_state, :accept_states, :rulebook)
	def accepting?		#判断当前的状态是否为接受状态
		accept_states.include?(current_state)
	end
	
	def read_character(character)	#读一个字符，并进行状态转移
		self.current_state = rulebook.next_state(current_state, character)
	end
	
	def read_string(string)	#读入字符串，并对每个字符作处理
		string.chars.each { |character| read_character(character) }	#string.chars 得到字符串对应的字符数组
	end
end

class DFADesign < Struct.new(:start_state, :accept_states, :rulebook)
	def to_dfa
		DFA.new(start_state, accept_states, rulebook)
	end
	
	def accepts?(string)
		to_dfa.tap { |dfa| dfa.read_string(string) }.accepting?		#tap, 将对象传到代码块中求值，并返回这个对象。
	end
end

#NFASimulation