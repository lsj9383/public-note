#不确定性有限自动机
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
		self.current_states = rulebook.next_states(current_states, character)
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

#code
rulebook = NFARulebook.new([
						FARule.new(1, 'a', 1),FARule.new(1, 'b', 1),FARule.new(1, 'b', 2),
						FARule.new(2, 'a', 3),FARule.new(2, 'b', 3),
						FARule.new(3, 'a', 4),FARule.new(3, 'b', 4)	])
rulebook.next_states(Set[1], 'b')
rulebook.next_states(Set[1, 2], 'a')
rulebook.next_states(Set[1, 3], 'b')
nfa = NFA.new(Set[1], [4], rulebook)
nfa.read_string('bbbbbb');nfa.accepting?
nfa_design = NFADesign.new(1, [4], rulebook)
nfa_design.accepts?('bab')
nfa_design.accepts?('bbbbbb')
nfa_design.accepts?('bbabb')