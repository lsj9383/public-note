#确定性有限自动机

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

#code
rulebook = DFARulebook.new([
						FARule.new(1, 'a', 2),FARule.new(1, 'b', 1),
						FARule.new(2, 'a', 2),FARule.new(2, 'b', 3),
						FARule.new(3, 'a', 3),FARule.new(3, 'b', 3)])
rulebook.next_state(1, 'a')
rulebook.next_state(1, 'b')
rulebook.next_state(2, 'b')
dfa = DFA.new(1, [3], rulebook);dfa.accepting?
dfa.read_string('baaab');dfa.accepting?
dfa_design = DFADesign.new(1, [3], rulebook)
dfa_design.accepts?('a')
dfa_design.accepts?('baa')
dfa_design.accepts?('baba')