from flask import Flask
from flask import request

app = Flask(__name__)

@app.route('/')
def hello_world():
    return request.form.get("name", "lsj")

if __name__ == '__main__':
    app.run()