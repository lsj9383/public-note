import logging

formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')

#handler = logging.FileHandler("log.txt")
handler = logging.StreamHandler()
handler.setFormatter(formatter)
handler.setLevel(logging.CRITICAL)


logger = logging.getLogger("simpleLogger")
logger.addHandler(handler)
logger.setLevel(logging.DEBUG)

logger.critical("hello world")