__all__ = ()

from os.path import dirname as get_directory_name, join as join_paths

from ... import __file__ as PACKAGE_ROOT_FILE


MARGIN = 1.2

MODEL_PATH = join_paths(get_directory_name(PACKAGE_ROOT_FILE), 'tracking', 'models', 'efficient_face_model.tflite')


SADNESS_REDUCTION = 0.75
SURPRISE_REDUCTION = 0.50



EXPRESSION_MULTIPLIER = 20.0

EXPRESSION_MIN = 0.0
EXPRESSION_MAX = 100.0
