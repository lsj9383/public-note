from flask import Flask, render_template
from nameForm import NameForm

app = Flask(__name__);
app.config['SECRET_KEY'] = 'hard to guess string'		# 要使用表单类，就需要配置该密钥，强制解决CSRF问题。

# 若已经登陆，则显示表单+名字。若未登录，则只显示表单。
@app.route('/', methods=['GET', 'POST'])
def index():
	name = 'lsj';
	form = NameForm();
	print(form);
	if form.validate_on_submit():
		name=form.name.data;
		form.name.data='';
	return render_template('index.html', form=form, name=name);
	
@app.route('/user/<name>')
def user(name):
	return render_template('user.html', name=name);

if __name__ == '__main__':
	app.run();