class Person():
	count = 0;
	def __init__(self, name):
		self._name = name;
		self.__age = 18;
		self.count+=1;
		Person.count+=1;
		
	def get_name(self):
		print("read");
		return self._name;
		
	def set_name(self, name):
		print("write");
		self._name = name;
		
me = Person("lsj");
he = Person("hjs");
print(me.count);
print(he.count);