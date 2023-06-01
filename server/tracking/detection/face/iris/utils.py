__all__ = ('accelerate_iris_left_x', 'accelerate_iris_right_x')

from .constants import IRIS_OUT_ACCELERATION


def accelerate_iris_left_x(iris_left_x):
    if (iris_left_x < 0.0):
        iris_left_x = -((-iris_left_x) ** IRIS_OUT_ACCELERATION)
    
    return iris_left_x


def accelerate_iris_right_x(iris_right_x):
    if (iris_right_x > 0.0):
        iris_right_x = iris_right_x ** IRIS_OUT_ACCELERATION
    
    return iris_right_x
