__all__ = ('get_head_y_rotation',)

from math import atan2, tau

from ....points.face import POINT_FACE__FOREHEAD, POINT_FACE__NOSE_TIP


def get_head_y_rotation(landmarks):
    right = landmarks[POINT_FACE__FOREHEAD]
    left = landmarks[POINT_FACE__NOSE_TIP]
    
    return atan2(left.z - right.z, left.y - right.y) * 180.0 / tau
