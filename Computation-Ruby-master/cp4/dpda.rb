#确定性自动下推机

class Stack < Struct.new(:contents)
	def push(character)
		Stack.new([character]+contents)	#push是增添数组首元素....
	end
	
	def pop
		Stack.new(contents.drop(1))		#pop是删除数组首元素...居然是首元素，醉了
	end
	
	def top
		contents.first					#栈顶是数组首元素
	end
	
	def inspect
		"<Stack (#{top})#{contents.drop(1).join}>"
	end
end

#PDAConfiguration 专门用来保存当前状态与栈
class PDAConfiguration < Struct.new(:state, :stack)
	STUCK_STATE = Object.new
	
	def stuck
		PDAConfiguration.new(STUCK_STATE, stack)
	end
	
	def stuck?
		state == STUCK_STATE
	end
end

class PDARule < Struct.new(:state, :character, :next_state, :pop_character, :push_characters)
	def applies_to?(configuration, character)	#判断给定的配置与输入的字符，是否与当前规则匹配
		self.state == configuration.state && self.pop_character == configuration.stack.top && self.character == character
	end
	
	def follow(configuration)					#规则拿到当前的配置，可以推出下一个配置
		PDAConfiguration.new(next_state, next_stack(configuration))
	end
	
	def next_stack(configuration)
		popped_stack = configuration.stack.pop
		push_characters.reverse.inject(popped_stack){ |stack, character| stack.push(character) }
	end
end

class DPDARulebook < Struct.new(:rules)	#rulebook的职责是，保存所有的rule，并且帮助客户跟进当前的配置和输入字符，找到下一个配置
	def next_configuration(configuration, character)	#rulebook，帮助客户找到下一个配置。
		rule_for(configuration, character).follow(configuration)
	end
	
	def rule_for(configuration, character)
		rules.detect{ |rule| rule.applies_to?(configuration, character) }
	end
	
	def applies_to?(configuration, character)
		!rule_for(configuration, character).nil?
	end
	
	def follow_free_moves(configuration)
		if applies_to?(configuration, nil)
			follow_free_moves(next_configuration(configuration, nil))
		else
			configuration
		end
	end
end

class DPDA < Struct.new(:current_configureation, :accept_states, :rulebook)
	def next_configuration(conf, char)
		if rulebook.applies_to?(conf, char)
			rulebook.next_configuration(conf, char)
		else
			conf.stuck
		end
	end
	
	def stuck?
		current_configureation.stuck?
	end
	
	def accepting?
		accept_states.include?(current_configureation.state)
	end
	
	def read_character(character)
		self.current_configureation = next_configuration( rulebook.follow_free_moves(current_configureation), character)
		self.current_configureation = rulebook.follow_free_moves(current_configureation)
	end
	
	def read_string(string)
		string.chars.each{ |character| 
			read_character(character)  unless stuck?
		}
	end
end

class DPDADesign < Struct.new(:start_state, :bottom_character, :accept_states, :rulebook)
	def accepts?(string)
		dpda = to_dpda
		dpda.read_string(string)
		dpda.accepting?
	end
	
	def to_dpda
		start_stack = Stack.new([bottom_character])
		start_configuration = PDAConfiguration.new(start_state, start_stack)
		DPDA.new(start_configuration, accept_states, rulebook)
	end
end

#code
stack = Stack.new(['a','b','c','d','e'])
rule = PDARule.new(1, '(', 2, '$', ['b', '$'])
configuration = PDAConfiguration.new(1, Stack.new(['$']))
rule.applies_to?(configuration, '(')
stack = Stack.new(['$']).push('x').push('y').push('z')
rulebook = DPDARulebook.new([
				PDARule.new(1, '(', 2, '$', ['b', '$']),
				PDARule.new(2, '(', 2, 'b', ['b', 'b']),
				PDARule.new(2, ')', 2, 'b', []),
				PDARule.new(2, nil, 1, '$', ['$'])
			])
dpda = DPDA.new(PDAConfiguration.new(1, Stack.new(['$'])), [1], rulebook)
dpda.accepting?
dpda.read_string('(()'); dpda.accepting?; dpda.current_configureation
dpda.read_string(')'); 
dpda.accepting?
dpda.current_configureation
dpda_design = DPDADesign.new(1, '$', [1], rulebook)
dpda_design.accepts?('()')
dpda_design.accepts?('(((()()))')
dpda_design.accepts?('(((()())))')
dpda_design.accepts?('(((()()))))')
dpda_design.accepts?('())')
dpda_design.accepts?('(()()()(()()))')