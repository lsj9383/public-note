from flask import Flask, url_for, request, redirect, abort, render_template
import logging

app = Flask(__name__)

@app.route('/')
def hello_world():
    return 'Hello World!'

@app.route('/user/<username>')
def usershow(username):
    return username+age

@app.route('/route/')
def urlshow():
    return url_for('usershow', username='abc')

@app.route('/post/', methods=["GET", "POST"])
def post():
    try:
        return request.form["name"]
    except:
        return "not found"

@app.route('/get/', methods=["GET", "POST"])
def get():
    return request.args.get("name", "4")

@app.route('/show/')
def show():
    return "{}".format(type(render_template("show.html")))

@app.route('/direct')
def direct():
    return url_for("hello_world")

@app.route('/redirect')
def red():
    return redirect(url_for("hello_world"))

@app.route('/error')
def err():
    abort(401)
    return redirect(url_for("hello_world"))

@app.errorhandler(401)
def page_not_found(error):
    app.logger.debug("error!")
    return "not found", 404

handler = logging.FileHandler('log', encoding='UTF-8')
handler.setLevel(logging.DEBUG)
logging_format = logging.Formatter('%(asctime)s - %(levelname)s - %(filename)s - %(funcName)s - %(lineno)s - %(message)s')
#handler.setFormatter(logging_format)
app.logger.addHandler(handler)
if __name__ == '__main__':
    app.run(debug=True)