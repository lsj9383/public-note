import os
basedir = os.path.abspath(os.path.dirname(__file__))

class Config:
	SECRET_KEY = os.environ.get('SECRET_KEY') or 'hard to guess string';
	SQLALCHEMY_COMMIT_ON_TEARDOWN = True;
	FLASKY_MAIL_SUBJECT_PREFIX = '[Flasky]'
	
	@staticmethod
	def init_app(app):
		pass;
		
class DevelopmentConfig(Config):
	DEBUG=True;

class TestConfig(Config):
	TESTING=True;

class ProductionConfig(Config):
	pass;
	
config = {
	'development':DevelopmentConfig,
	'testing':TestConfig,
	'production':ProductionConfig,
	
	'default':DevelopmentConfig
};