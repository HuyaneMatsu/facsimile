__all__ = ('detect_expressions',)

from math import floor, ceil

from cv2 import resize
from numpy import float32 as f32, array as Array, moveaxis as move_axis

from ...helpers import get_point_average_2d, get_point_difference_2d, suppress_stdout_and_stderr
from ...points.face import POINT_FACE__CHIN, POINT_FACE__FOREHEAD

from .constants import MARGIN, MODEL_PATH
from .helpers import normalize_expressions


NORMALIZER_MEAN = Array([0.57535914, 0.44928582, 0.40079932], dtype = f32) * f32(255)
NORMALIZER_STANDARD = Array([0.20735591, 0.18981615, 0.18132027], dtype = f32) * f32(255)


with suppress_stdout_and_stderr(True):
    import tensorflow
    INTERPRETER = tensorflow.lite.Interpreter(model_path = MODEL_PATH)
    INTERPRETER.allocate_tensors()


INPUT_TENSOR = INTERPRETER.get_input_details()[0]['index']
OUTPUT_TENSOR = INTERPRETER.get_output_details()[0]['index']


def detect_expressions(landmarks, image):
    """
    Detects expressions from the given image.
    
    Parameters
    ----------
    landmarks : `google.protobuf.pyext._message.RepeatedCompositeContainer`
        The read landmarks.
    image : `numpy.ndarray`
        The read image.
    
    Returns
    -------
    happiness : `float`
    sadness : `float`
    surprise : `float`
    fear : `float`
    disgust : `float`
    anger : `float`
    """
    top_point = landmarks[POINT_FACE__FOREHEAD]
    bot_point = landmarks[POINT_FACE__CHIN]
    
    image_scale_y, image_scale_x, _ = image.shape
    
    middle_point = get_point_average_2d(top_point, bot_point)
    point_difference = get_point_difference_2d(top_point, bot_point) * MARGIN * 1.2
    image_radius = point_difference * 0.5
    
    point_x_low = floor((middle_point.x - image_radius * 0.5) * image_scale_x)
    if point_x_low < 0:
        point_x_low = 0
    
    point_x_high = ceil((middle_point.x + image_radius * 0.5) * image_scale_x)
    if point_x_high >= image_scale_x:
        point_x_high = image_scale_x - 1
    
    point_y_low = floor((middle_point.y - image_radius) * image_scale_y)
    if point_y_low < 0:
        point_y_low = 0
    
    point_y_high = ceil((middle_point.y + image_radius) * image_scale_y)
    if point_y_high >= image_scale_y:
        point_y_high = image_scale_y - 1
    
    image = image[point_y_low:point_y_high, point_x_low:point_x_high]
    
    
    image = resize(image, (224, 224))
    
    image = image - NORMALIZER_MEAN
    image /= NORMALIZER_STANDARD
    image = move_axis(image, -1, 0)
    
    image = image.reshape((1, *image.shape))
    
    INTERPRETER.set_tensor(INPUT_TENSOR, image)
    INTERPRETER.invoke()
    
    result = INTERPRETER.get_tensor(OUTPUT_TENSOR)
    return normalize_expressions(result[0])
