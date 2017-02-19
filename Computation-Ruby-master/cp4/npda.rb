#非确定性自动下推机
require 'set'

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

class NPDARulebook < Struct.new(:rules)	#rulebook的职责是，保存所有的rule，并且帮助客户跟进当前的配置和输入字符，找到下一个配置
	def next_configurations(configurations, character)	#rulebook，帮助客户找到下一个配置。
		configurations.flat_map{ |config| follow_rules_for(config, character) }.to_set
	end
	
	def follow_rules_for(configuration, character)
		rules_for(configuration, character).map{ |rule| rule.follow(configuration) }
	end
	
	def rule_for(configuration, character)
		rules.select{ |rule| rule.applies_to?(configuration, character) }
	end
	
	def applies_to?(configuration, character)
		!rule_for(configuration, character).nil?
	end
	
	def follow_free_moves(configurations)
		more_configurations = next_configurations(configurations, nil)
		if more_configurations.subset?()
			configurations
		else
			follow_free_moves(configurations + more_configurations)
		end
	end
end

class NPDA < Struct.new(:current_configureation, :accept_states, :rulebook)
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
		current_configureation.any? { |config| accept_states.include?(config.state) }
	end
	
	def read_character(character)
		self.current_configureation = rulebook.next_configurations( rulebook.follow_free_moves(current_configureation), character)
		self.current_configureation = rulebook.follow_free_moves(current_configureation)
	end
	
	def read_string(string)
		string.chars.each{ |character| 
			read_character(character)
		}
	end
end

class NPDADesign < Struct.new(:start_state, :bottom_character, :accept_states, :rulebook)
	def accepts?(string)
		npda = to_npda
		npda.read_string(string)
		npda.accepting?
	end
	
	def to_npda
		start_stack = Stack.new([bottom_character])
		start_configuration = PDAConfiguration.new(start_state, start_stack)
		NPDA.new(start_configuration, accept_states, rulebook)
	end
end

#code
rulebook = NPDARulebook.new([
	PDARule.new(1, 'a', 1, '$', ['a', '$']),
	PDARule.new(1, 'a', 1, 'a', ['a', 'a']),
	PDARule.new(1, 'a', 1, 'b', ['a', 'b']),
	PDARule.new(1, 'b', 1, '$', ['b', '$']),
	PDARule.new(1, 'b', 1, 'a', ['b', 'a']),
	PDARule.new(1, 'b', 1, 'b', ['b', 'b']),
	PDARule.new(1, nil, 2, '$', ['$']),
	PDARule.new(1, nil, 2, 'a', ['a']),
	PDARule.new(1, nil, 2, 'b', ['b']),
	PDARule.new(2, 'a', 2, 'a', []),
	PDARule.new(2, 'b', 2, 'b', []),
	PDARule.new(2, nil, 3, '$', ['$'])
])
configuration = PDAConfiguration.new(1, Stack.new(['$']))
npda = NPDA.new(Set[configuration], [3], rulebook)
npda.accepting?
npda.read_string('abb')
npda.accepting?