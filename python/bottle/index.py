from bottle import route, run

@route('/index')
def index():
    return "index page"

@route('/hello/<name>')
def index(name='lsj'):
    return "hello "+name

run(host='localhost', port=8000, debug=True)

print("hello")